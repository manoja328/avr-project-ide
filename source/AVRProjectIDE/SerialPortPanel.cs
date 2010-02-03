using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.IO.Ports;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Threading;
using WeifenLuo.WinFormsUI.Docking;

namespace AVRProjectIDE
{
    public partial class SerialPortPanel : DockContent
    {
        [System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.Demand, Name = "FullTrust")]
        protected override void WndProc(ref Message m)
        {
            try
            {
                base.WndProc(ref m);
            }
            catch (Exception ex)
            {
                ErrorReportWindow.Show(ex, "Error In Serial Port Panel");
                
            }
        }

        public SerialPortPanel()
        {
            InitializeControl();
        }

        public SerialPortPanel(string preferedPortName, int preferedBaudRate)
        {
            InitializeControl();
            SetPreferences(preferedPortName, preferedBaudRate);
        }

        private void InitializeControl()
        {
            InitializeComponent();

            try
            {
                try
                {
                    this.txtTx.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)), true);
                    this.txtRx.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)), true);
                }
                catch
                {
                    try
                    {
                        this.txtTx.Font = new System.Drawing.Font("Courier", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)), true);
                        this.txtRx.Font = new System.Drawing.Font("Courier", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)), true);
                    }
                    catch
                    {
                        this.txtTx.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)), true);
                        this.txtRx.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)), true);
                    }
                }
            }
            catch
            {
                this.txtTx.Font = new System.Drawing.Font(System.Drawing.FontFamily.GenericMonospace, 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)), true);
                this.txtRx.Font = new System.Drawing.Font(System.Drawing.FontFamily.GenericMonospace, 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)), true);
            }

            this.Icon = GraphicsResx.serial;

            // get a list of available ports and populate the list
            string[] portList = SerialPort.GetPortNames();
            dropPorts.Items.Clear();
            dropPorts.Items.Add("None");
            dropPorts.Items.Add("Search for Ports");
            foreach (string portName in portList)
            {
                dropPorts.Items.Add(portName);
            }

            dropPorts.SelectedIndex = 0;
            dropBaud.SelectedIndex = 5;

            Disconnect();

            txtTx.MaxLength = serialPort1.WriteBufferSize;

            textboxBuffer = "";
            textChanged = true;

            StartThread();
        }

        #region Public Methods and Properties

        public string CurrentPort
        {
            get
            {
                if (dropPorts.SelectedIndex >= 0)
                    return (string)dropPorts.Items[dropPorts.SelectedIndex];
                else
                    return lastUsedPort;
            }
        }

        public int CurrentBaud
        {
            get { return int.Parse((string)dropBaud.Items[dropBaud.SelectedIndex]); }
        }

        public bool IsConnected
        {
            get
            {
                bool isOpen = false;
                try
                {
                    isOpen = serialPort1.IsOpen;
                }
                catch (Exception ex)
                {
                    // weird huh? but this may actually raise an exception
                    RaiseException(ex);
                }
                return isOpen;
            }
        }

        public event SerialPortErrorHandler SerialPortException;
        public delegate void SerialPortErrorHandler(Exception ex);

        public void SetPreferences(string preferedPortName, int preferedBaudRate)
        {
            // match strings to set preferences

            for (int i = 0; i < dropPorts.Items.Count; i++)
            {
                if (((string)dropPorts.Items[i]).ToLowerInvariant().Trim() == preferedPortName.ToLowerInvariant().Trim())
                {
                    dropPorts.SelectedIndex = i;
                    break;
                }
            }

            for (int i = 0; i < dropBaud.Items.Count; i++)
            {
                if (int.Parse(((string)dropBaud.Items[i]).Trim()) == preferedBaudRate)
                {
                    dropBaud.SelectedIndex = i;
                    break;
                }
            }
        }

        public void ClearRxTextBox()
        {
            txtRx.Text = "";
            textboxBuffer = "";
        }

        public void Disconnect()
        {
            // wait until transmission has ended
            while (bgTxWorker.IsBusy) ;

            try
            {
                serialPort1.Close();
                Thread.Sleep(250);
            }
            catch (Exception ex)
            {
                RaiseException(ex);
            }

            btnConnect.Text = "Connect";
            btnSend.Enabled = false;
            dropPorts.Enabled = true;
            dropBaud.Enabled = true;
            barSerPortTick.Visible = false;
            pendingDisconnect = false;
            btnConnect.Enabled = true;
        }

        public void Connect()
        {
            try
            {
                if (dropPorts.SelectedIndex >= 0)
                    serialPort1.PortName = (string)dropPorts.Items[dropPorts.SelectedIndex];
                else
                    serialPort1.PortName = lastUsedPort;

                serialPort1.BaudRate = int.Parse((string)dropBaud.Items[dropBaud.SelectedIndex]);
                serialPort1.Open();
                barRxStatus.Maximum = serialPort1.ReadBufferSize;
                btnConnect.Text = "Disonnect";
                btnSend.Enabled = true;
                dropPorts.Enabled = false;
                dropBaud.Enabled = false;
                attemptedDisconnect = false;

                SettingsManagement.SaveSerialPortPrefs(serialPort1.PortName, serialPort1.BaudRate);
            }
            catch (Exception ex)
            {
                RaiseException(ex);
            }
        }

        public void StartThread()
        {
            // if cancelled, wait until it ends before restarting
            if (bgRxWorker.WorkerSupportsCancellation)
            {
                if (bgRxWorker.CancellationPending && bgRxWorker.IsBusy)
                {
                    while (bgRxWorker.IsBusy) ;
                }
            }

            if (bgRxWorker.IsBusy == false)
            {
                bgRxWorker.RunWorkerAsync();
            }

            timerStatusChecker.Enabled = true;
        }

        public void KillThread()
        {
            if (bgRxWorker.WorkerSupportsCancellation)
            {
                bgRxWorker.CancelAsync();
            }
        }

        public void Send(string text)
        {
            if (serialPort1.IsOpen && bgTxWorker.IsBusy == false)
            {
                bgTxWorker.RunWorkerAsync(text);
            }
        }

        #endregion

        #region Async Form Control Modification

        // these are delegates for thread safe calls

        private delegate void SetProgBarCallback(int bytesToRead);

        private void SetRxBufferStatus(int bytesToRead)
        {
            if (InvokeRequired)
            {
                Invoke(new SetProgBarCallback(SetRxBufferStatus), new object[] { bytesToRead, });
            }
            else
            {
                barRxStatus.Value = bytesToRead;

                bool isOpen = false;
                try
                {
                    isOpen = serialPort1.IsOpen;
                }
                catch (Exception ex)
                {
                    // this is a weird place to get exceptions but it has happened
                    RaiseException(ex);
                }

                if (isOpen)
                {
                    // display either buffer % or the ticker depending on busy-ness
                    if (bytesToRead != 0)
                    {
                        barSerPortTick.Visible = false;
                    }
                    else
                    {
                        barSerPortTick.Visible = true;
                    }
                }

                if (bytesToRead > (serialPort1.ReadBufferSize * 3) / 4)
                {
                    Disconnect();
                    MessageBox.Show("Serial Port was Closed Due to Massive Amount of Traffic");
                }
            }
        }

        private string textboxBuffer = ""; // calling textBox.Text all the time has a high overhead, so this is used instead
        private bool textChanged = true;

        private delegate void AppendToRxCallback(string text);

        private void AppendToRxTxt(string text)
        {
            // doing it by invoking a delegate makes sure that textboxBuffer is modified in a thread safe way
            if (InvokeRequired)
            {
                Invoke(new AppendToRxCallback(AppendToRxTxt), new object[] { text, });
            }
            else
            {
                textboxBuffer += text;
                textChanged = true;
            }
        }

        private void timerTextBoxUpdater_Tick(object sender, EventArgs e)
        {
            // calling textBox.Text all the time has a high overhead, so this is used instead
            try
            {
                if (textChanged)
                {
                    bool scroll = txtRx.SelectionLength < 2;
                    txtRx.Text = textboxBuffer;
                    if (scroll)
                    {
                        txtRx.Select(textboxBuffer.Length, 0);
                        txtRx.ScrollToCaret();
                    }
                    textChanged = false;
                }
            }
            catch { }
        }

        private void RaiseException(Exception ex)
        {
            if (InvokeRequired)
            {
                Invoke(new SerialPortErrorHandler(RaiseException), new object[] { ex, });
            }
            else
            {
                //SerialPortException(ex);
                txtRx.Text = ex.Message + Environment.NewLine + textboxBuffer;
                textChanged = false;
            }
        }

        #endregion

        #region Status Related

        private bool attemptedDisconnect = false;
        private bool pendingDisconnect = false;

        private void timerStatusChecker_Tick(object sender, EventArgs e)
        {
            try
            {
                bool isOpen = false;
                try
                {
                    isOpen = serialPort1.IsOpen;
                }
                catch (Exception ex)
                {
                    RaiseException(ex);
                }

                if (isOpen)
                {
                    btnConnect.Text = "Disconnect";
                    btnSend.Enabled = true;
                    dropPorts.Enabled = false;
                    dropBaud.Enabled = false;
                    attemptedDisconnect = false;
                }
                else
                {
                    btnConnect.Text = "Connect";
                    btnSend.Enabled = false;
                    dropPorts.Enabled = true;
                    dropBaud.Enabled = true;
                    barSerPortTick.Visible = false;

                    if (attemptedDisconnect == false)
                    {
                        attemptedDisconnect = true;
                        try
                        {
                            serialPort1.Close();
                        }
                        catch (Exception ex)
                        {
                            RaiseException(ex);
                        }
                    }

                    pendingDisconnect = false;
                }

                // textbox too full, clear 2/3 of it automatically
                string fromTxt = txtRx.Text;
                if (fromTxt.Length > txtRx.MaxLength / 2)
                {
                    fromTxt = fromTxt.Substring(txtRx.MaxLength / 3);
                    txtRx.Text = fromTxt;
                    txtRx.Select(fromTxt.Length, 0);
                    txtRx.ScrollToCaret();
                }

                if (pendingDisconnect)
                {
                    if (serialPort1.IsOpen)
                    {
                        if (bgTxWorker.IsBusy || serialPort1.BytesToRead > 0)
                        {
                            pendingDisconnect = true;
                            btnConnect.Enabled = false;
                            btnSend.Enabled = false;
                            return;
                        }

                        Disconnect();
                    }
                    else
                    {
                        btnConnect.Enabled = true;
                        pendingDisconnect = false;
                    }
                }
                else
                {
                    btnConnect.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                RaiseException(ex);
            }
        }

        #endregion

        #region Button Press Events

        private void btnConnect_Click(object sender, EventArgs e)
        {
            if (serialPort1.IsOpen)
            {
                if (bgTxWorker.IsBusy || serialPort1.BytesToRead > 0)
                {
                    pendingDisconnect = true;
                    btnConnect.Enabled = false;
                    btnSend.Enabled = false;
                    return;
                }

                Disconnect();
            }
            else
            {
                Connect();
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearRxTextBox();
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            Send(txtTx.Text);
        }

        private void txtTx_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && pendingDisconnect == false && serialPort1.IsOpen && btnSend.Enabled)
                Send(txtTx.Text);
        }

        #endregion

        #region Background Workers

        private void bgRxWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            byte[] bArr = new byte[2048];
            int bCnt;

            while (bgRxWorker.CancellationPending == false)
            {
                string resStr = "";
                bCnt = 0;

                bool isOpen = false;
                try
                {
                    isOpen = serialPort1.IsOpen;
                }
                catch (Exception ex)
                {
                    RaiseException(ex);
                }

                if (isOpen)
                {
                    try
                    {
                        bCnt = serialPort1.BytesToRead;
                        SetRxBufferStatus(bCnt);

                        Thread.Sleep(0);

                        if (bCnt > 0)
                        {
                            bCnt = serialPort1.Read(bArr, 0, 1024);
                        }
                    }
                    catch (Exception ex)
                    {
                        RaiseException(ex);
                    }
                }
                else
                {
                    Thread.Sleep(250);
                }

                for (int i = 0; i < bCnt && i < 1024; i++)
                {
                    if (bArr[i] == '\r' || bArr[i] == '\n' || bArr[i] == '\t' || (bArr[i] >= 0x20 && bArr[i] <= 0x7E))
                    {
                        resStr += Convert.ToChar(bArr[i]);
                    }
                    else if (bArr[i] == '\v')
                    {
                        resStr += Environment.NewLine + Environment.NewLine + Environment.NewLine;
                    }
                    else
                    {
                        string hexStr = Convert.ToString(bArr[i], 16).ToUpper();
                        while (hexStr.Length % 2 == 1)
                            hexStr = "0" + hexStr;
                        resStr += "{\\x" + hexStr + "}";
                    }
                }

                if (bCnt > 0)
                {
                    AppendToRxTxt(resStr);
                    Thread.Sleep(0);
                }
                else
                {
                    SetRxBufferStatus(bCnt);
                    Thread.Sleep(100);
                }
            }
        }

        private void bgTxWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                ASCIIEncoding encoding = new ASCIIEncoding();
                byte[] bArr = encoding.GetBytes((string)(e.Argument));

                Queue<byte> bQueue = new Queue<byte>();
                for (int i = 0; i < bArr.Length; i++)
                {
                    if (Convert.ToChar(bArr[i]) == '\\')
                    {
                        if (i < bArr.Length - 1)
                        {
                            char nextChar = Convert.ToChar(bArr[i + 1]);
                            if (nextChar == 'x' && i + 3 < bArr.Length)
                            {
                                int hexRes = -1;
                                string hexStr = "0x" + Convert.ToChar(bArr[i + 2]) + Convert.ToChar(bArr[i + 3]);
                                try
                                {
                                    hexRes = Convert.ToInt32(hexStr, 16);
                                }
                                catch
                                {
                                }
                                if (hexRes >= 0)
                                {
                                    bQueue.Enqueue(Convert.ToByte(hexRes));
                                    i += 3;
                                }
                                else
                                {
                                    bQueue.Enqueue(Convert.ToByte('\\'));
                                }
                            }
                            else
                            {
                                if (nextChar == '\\')
                                    bQueue.Enqueue(Convert.ToByte('\\'));
                                else if (nextChar == 'r')
                                    bQueue.Enqueue(Convert.ToByte('\r'));
                                else if (nextChar == 'n')
                                    bQueue.Enqueue(Convert.ToByte('\n'));
                                else if (nextChar == 'a')
                                    bQueue.Enqueue(Convert.ToByte('\a'));
                                else if (nextChar == 'b')
                                    bQueue.Enqueue(Convert.ToByte('\b'));
                                else if (nextChar == 't')
                                    bQueue.Enqueue(Convert.ToByte('\t'));
                                else if (nextChar == 'v')
                                    bQueue.Enqueue(Convert.ToByte('\v'));
                                else
                                {
                                    bQueue.Enqueue(Convert.ToByte('\\'));
                                    i--;
                                }
                                i++;
                            }
                        }
                        else
                        {
                            bQueue.Enqueue(bArr[i]);
                        }
                    }
                    else
                    {
                        bQueue.Enqueue(bArr[i]);
                    }
                }

                int cnt;
                for (cnt = 0; bQueue.Count > 0; cnt++)
                {
                    bArr[cnt] = bQueue.Dequeue();
                }

                e.Result = bArr;

                if (serialPort1.IsOpen)
                {
                    serialPort1.Write(bArr, 0, cnt);
                }
            }
            catch (Exception ex)
            {
                RaiseException(ex);
            }
        }

        #endregion

        #region Other Events

        private void SerialPortPanel_FormClosing(object sender, FormClosingEventArgs e)
        {
            Disconnect();
            KillThread();
        }

        #endregion

        private string lastUsedPort = "";

        private bool blockReselect = false;

        private void dropPorts_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (blockReselect)
                return;

            blockReselect = true;

            if (dropPorts.SelectedIndex >= 0)
            {
                string s = (string)dropPorts.Items[dropPorts.SelectedIndex];

                string oldPort = s;

                if (oldPort.StartsWith("COM") == false)
                    oldPort = lastUsedPort;
                else
                    lastUsedPort = oldPort;

                if (s == "Search for Ports")
                {
                    // get a list of available ports and populate the list
                    string[] portList = SerialPort.GetPortNames();
                    dropPorts.Items.Clear();

                    dropPorts.Items.Add("None");
                    dropPorts.Items.Add("Search for Ports");

                    foreach (string portName in portList)
                    {
                        dropPorts.Items.Add(portName);
                    }

                    // no ports available, disable everything
                    if (dropPorts.Items.Count <= 0)
                    {
                        this.Enabled = false;
                    }
                    else if (oldPort != null)
                    {
                        // if previous exists, reselect it
                        if (dropPorts.Items.Contains(oldPort))
                        {
                            dropPorts.SelectedIndex = dropPorts.Items.IndexOf(oldPort);
                        }
                        else
                        {
                            dropPorts.SelectedIndex = 0;
                        }
                    }
                    else
                    {
                        dropPorts.SelectedIndex = 0;
                    }
                }
            }

            blockReselect = false;
        }
    }
}