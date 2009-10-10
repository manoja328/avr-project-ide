namespace AVRProjectIDE
{
    partial class SerialPortPanel
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                try
                {
                    components.Dispose();
                }
                catch { }
            }
            try
            {
                base.Dispose(disposing);
            }
            catch { }
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.dropPorts = new System.Windows.Forms.ComboBox();
            this.dropBaud = new System.Windows.Forms.ComboBox();
            this.barRxStatus = new System.Windows.Forms.ProgressBar();
            this.barSerPortTick = new System.Windows.Forms.ProgressBar();
            this.txtTx = new System.Windows.Forms.TextBox();
            this.btnSend = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnConnect = new System.Windows.Forms.Button();
            this.txtRx = new System.Windows.Forms.TextBox();
            this.bgRxWorker = new System.ComponentModel.BackgroundWorker();
            this.timerTextBoxUpdater = new System.Windows.Forms.Timer(this.components);
            this.bgTxWorker = new System.ComponentModel.BackgroundWorker();
            this.serialPort1 = new System.IO.Ports.SerialPort(this.components);
            this.timerStatusChecker = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // dropPorts
            // 
            this.dropPorts.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.dropPorts.FormattingEnabled = true;
            this.dropPorts.Location = new System.Drawing.Point(1, 1);
            this.dropPorts.Name = "dropPorts";
            this.dropPorts.Size = new System.Drawing.Size(75, 21);
            this.dropPorts.TabIndex = 1;
            this.dropPorts.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.dropPorts_MouseDoubleClick);
            // 
            // dropBaud
            // 
            this.dropBaud.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.dropBaud.FormattingEnabled = true;
            this.dropBaud.Items.AddRange(new object[] {
            "300",
            "600",
            "1200",
            "2400",
            "4800",
            "9600",
            "14400",
            "19200",
            "28800",
            "38400",
            "57600",
            "76800",
            "115200",
            "230400"});
            this.dropBaud.Location = new System.Drawing.Point(77, 1);
            this.dropBaud.Name = "dropBaud";
            this.dropBaud.Size = new System.Drawing.Size(79, 21);
            this.dropBaud.TabIndex = 2;
            // 
            // barRxStatus
            // 
            this.barRxStatus.Location = new System.Drawing.Point(157, 1);
            this.barRxStatus.Name = "barRxStatus";
            this.barRxStatus.Size = new System.Drawing.Size(86, 21);
            this.barRxStatus.TabIndex = 6;
            // 
            // barSerPortTick
            // 
            this.barSerPortTick.Location = new System.Drawing.Point(157, 1);
            this.barSerPortTick.MarqueeAnimationSpeed = 10;
            this.barSerPortTick.Name = "barSerPortTick";
            this.barSerPortTick.Size = new System.Drawing.Size(86, 21);
            this.barSerPortTick.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.barSerPortTick.TabIndex = 7;
            this.barSerPortTick.Visible = false;
            // 
            // txtTx
            // 
            this.txtTx.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTx.BackColor = System.Drawing.Color.Black;
            this.txtTx.Font = new System.Drawing.Font(System.Drawing.FontFamily.GenericMonospace, 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)), true);
            this.txtTx.ForeColor = System.Drawing.Color.White;
            this.txtTx.Location = new System.Drawing.Point(243, 2);
            this.txtTx.Name = "txtTx";
            this.txtTx.Size = new System.Drawing.Size(412, 20);
            this.txtTx.TabIndex = 3;
            this.txtTx.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtTx_KeyUp);
            // 
            // btnSend
            // 
            this.btnSend.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSend.Location = new System.Drawing.Point(655, 0);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(76, 23);
            this.btnSend.TabIndex = 4;
            this.btnSend.Text = "Send";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // btnClear
            // 
            this.btnClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClear.Location = new System.Drawing.Point(804, 0);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(75, 23);
            this.btnClear.TabIndex = 6;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnConnect
            // 
            this.btnConnect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnConnect.Location = new System.Drawing.Point(730, 0);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(75, 23);
            this.btnConnect.TabIndex = 5;
            this.btnConnect.Text = "Connect";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // txtRx
            // 
            this.txtRx.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtRx.BackColor = System.Drawing.Color.Black;
            this.txtRx.Font = new System.Drawing.Font(System.Drawing.FontFamily.GenericMonospace, 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)), true);
            this.txtRx.ForeColor = System.Drawing.Color.White;
            this.txtRx.Location = new System.Drawing.Point(0, 23);
            this.txtRx.Multiline = true;
            this.txtRx.Name = "txtRx";
            this.txtRx.ReadOnly = true;
            this.txtRx.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtRx.Size = new System.Drawing.Size(879, 269);
            this.txtRx.TabIndex = 7;
            this.txtRx.WordWrap = false;
            // 
            // bgRxWorker
            // 
            this.bgRxWorker.WorkerSupportsCancellation = true;
            this.bgRxWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgRxWorker_DoWork);
            // 
            // timerTextBoxUpdater
            // 
            this.timerTextBoxUpdater.Enabled = true;
            this.timerTextBoxUpdater.Interval = 250;
            this.timerTextBoxUpdater.Tick += new System.EventHandler(this.timerTextBoxUpdater_Tick);
            // 
            // bgTxWorker
            // 
            this.bgTxWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgTxWorker_DoWork);
            // 
            // timerStatusChecker
            // 
            this.timerStatusChecker.Enabled = true;
            this.timerStatusChecker.Tick += new System.EventHandler(this.timerStatusChecker_Tick);
            // 
            // SerialPortPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(879, 292);
            this.CloseButton = false;
            this.CloseButtonVisible = false;
            this.Controls.Add(this.txtRx);
            this.Controls.Add(this.btnSend);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.btnConnect);
            this.Controls.Add(this.txtTx);
            this.Controls.Add(this.barSerPortTick);
            this.Controls.Add(this.dropBaud);
            this.Controls.Add(this.dropPorts);
            this.Controls.Add(this.barRxStatus);
            this.DockAreas = ((WeifenLuo.WinFormsUI.Docking.DockAreas)(((WeifenLuo.WinFormsUI.Docking.DockAreas.Float | WeifenLuo.WinFormsUI.Docking.DockAreas.DockTop)
                        | WeifenLuo.WinFormsUI.Docking.DockAreas.DockBottom)));
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SerialPortPanel";
            this.TabText = "Serial Port";
            this.Text = "Serial Port";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SerialPortPanel_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox dropPorts;
        private System.Windows.Forms.ComboBox dropBaud;
        private System.Windows.Forms.ProgressBar barRxStatus;
        private System.Windows.Forms.ProgressBar barSerPortTick;
        private System.Windows.Forms.TextBox txtTx;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.TextBox txtRx;
        private System.ComponentModel.BackgroundWorker bgRxWorker;
        private System.Windows.Forms.Timer timerTextBoxUpdater;
        private System.ComponentModel.BackgroundWorker bgTxWorker;
        private System.IO.Ports.SerialPort serialPort1;
        private System.Windows.Forms.Timer timerStatusChecker;
    }
}