using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Xml;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace AVRProjectIDE
{
    public partial class DebuggerPanel : DockContent
    {
        private IDEWindow parent;

        private Process procAVaRICE;
        private Process procGDB;

        private List<string> chipList;

        private static readonly int[] ALLOWEDJTAGFREQ = new int[] { 1000, 500, 250, 125, };

        public DebuggerPanel(IDEWindow parent)
        {
            this.parent = parent;

            InitializeComponent();

            this.Icon = GraphicsResx.debug;

            this.numServerPort.Minimum = 1;
            this.numServerPort.Maximum = UInt16.MaxValue;
            numServerPort.Value = Math.Max(numServerPort.Minimum, Math.Min(numServerPort.Maximum, Convert.ToDecimal(SettingsManagement.LastGDBPort)));

            this.dropHardwareSelection.SelectedIndex = 0;
            this.dropHardwareSelection.SelectedIndex = dropHardwareSelection.Items.IndexOf(SettingsManagement.LastICEHardware);
            if (this.dropHardwareSelection.SelectedIndex < 0)
                this.dropHardwareSelection.SelectedIndex = 0;
            this.dropHardwareSelection.SelectedIndexChanged += new EventHandler(dropHardwareSelection_SelectedIndexChanged);

            numBitRate.Value = Math.Max(numBitRate.Minimum, Math.Min(numBitRate.Maximum, Convert.ToDecimal(SettingsManagement.LastJTAGFreq)));

            dropHardwareSelection_SelectedIndexChanged(null, null);

            this.txtAVaRICEOtherOpts.Text = SettingsManagement.LastICEOptions;
            this.txtJTAGPort.Text = SettingsManagement.LastJTAGPort;

            DisableButtons();

            if (CheckForAVARICE() == false)
            {
                this.Enabled = false;
            }
        }

        private void dropHardwareSelection_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.dropHardwareSelection.SelectedIndex < 0)
                this.dropHardwareSelection.SelectedIndex = 0;

            string debugger = ((string)this.dropHardwareSelection.Items[this.dropHardwareSelection.SelectedIndex]).Trim().ToLowerInvariant();

            if (debugger.Contains("debugwire"))
            {
                numBitRate.Enabled = false;
            }
            else
            {
                numBitRate.Enabled = true;

                if (debugger.EndsWith("mki"))
                {
                    int jtagFreq = Convert.ToInt32(numBitRate.Value);
                    for (int i = 0; i < ALLOWEDJTAGFREQ.Length; i++)
                    {
                        if (ALLOWEDJTAGFREQ[i] <= jtagFreq)
                        {
                            jtagFreq = ALLOWEDJTAGFREQ[i];
                            break;
                        }
                    }

                    numBitRate.Value = Convert.ToDecimal(jtagFreq);
                }
            }
        }

        [System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.Demand, Name = "FullTrust")]
        protected override void WndProc(ref Message m)
        {
            try
            {
                base.WndProc(ref m);
            }
            catch (Exception ex)
            {
                ErrorReportWindow.Show(ex, "Error In Debugger Control Panel");

            }
        }

        private delegate void TextBoxCallback(TextBox box, string text, TextBoxChangeMode mode);

        private void TextBoxModify(TextBox box, string text, TextBoxChangeMode mode)
        {
            if (box.InvokeRequired)
            {
                box.Invoke(new TextBoxCallback(TextBoxModify), new object[] { box, text, mode, });
            }
            else
            {
                if (mode == TextBoxChangeMode.Append)
                {
                    if (box.Text.Length + text.Length >= box.MaxLength)
                        box.Text = box.Text.Substring(box.Text.Length / 2);

                    box.Text += text;
                    box.SelectionStart = box.Text.Length;
                    box.SelectionLength = 0;
                    box.ScrollToCaret();
                }
                else if (mode == TextBoxChangeMode.AppendNewLine)
                {
                    if (box.Text.Length + text.Length >= box.MaxLength)
                        box.Text = box.Text.Substring(box.Text.Length / 2);

                    box.Text += Environment.NewLine + text;
                    box.SelectionStart = box.Text.Length;
                    box.SelectionLength = 0;
                    box.ScrollToCaret();
                }
                else if (mode == TextBoxChangeMode.Prepend)
                {
                    if (box.Text.Length + text.Length >= box.MaxLength)
                        box.Text = box.Text.Substring(0, box.Text.Length / 2);

                    box.Text = text + box.Text;
                    box.SelectionStart = 0;
                    box.SelectionLength = 0;
                    box.ScrollToCaret();
                }
                else if (mode == TextBoxChangeMode.PrependNewLine)
                {
                    if (box.Text.Length + text.Length >= box.MaxLength)
                        box.Text = box.Text.Substring(0, box.Text.Length / 2);

                    box.Text = text + Environment.NewLine + box.Text;
                    box.SelectionStart = 0;
                    box.SelectionLength = 0;
                    box.ScrollToCaret();
                }
                else if (mode == TextBoxChangeMode.Set)
                    box.Text = text;
                else if (mode == TextBoxChangeMode.SetNewLine)
                    box.Text = text + Environment.NewLine;
            }
        }

        private void TryKillAll()
        {
            try
            {
                this.procAVaRICE.Kill();
            }
            catch { }

            try
            {
                this.procGDB.Kill();
            }
            catch { }
        }

        private bool CheckForAVARICE()
        {
            try
            {
                Process proc = new Process();
                ProjectBuilder.SetEnviroVarsForProc(proc.StartInfo);
                proc.StartInfo.FileName = "avarice";
                proc.StartInfo.Arguments = "--known-devices";
                proc.StartInfo.UseShellExecute = false;
                proc.StartInfo.RedirectStandardError = true;
                proc.StartInfo.RedirectStandardOutput = true;
                proc.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                proc.StartInfo.CreateNoWindow = true;
                if (proc.Start() == false)
                {
                    TextBoxModify(this.txtOutput, "WinAVR's AVARICE could not be found", TextBoxChangeMode.SetNewLine);
                    return false;
                }
                else
                {
                    if (chipList == null)
                        chipList = new List<string>();

                    chipList.Clear();

                    string listStr = proc.StandardOutput.ReadToEnd();
                    if (listStr == null)
                        listStr = "";

                    listStr += "\r\n";
                    
                    string t = proc.StandardError.ReadToEnd();
                    if (t == null)
                        t = "";

                    listStr += t;

                    string[] lines = listStr.Split('\n');

                    bool listRdy = false;

                    foreach (string line_ in lines)
                    {
                        string line = line_.Trim();
                        
                        if (line.Contains("----") && listRdy == false)
                            listRdy = true;
                        else if (listRdy)
                        {
                            string[] parts = line.Split(new char[] { ' ', '\t', '\r', '\n', '\0', });
                            if (parts.Length >= 1)
                            {
                                if (string.IsNullOrEmpty(parts[0].Trim()) == false)
                                {
                                    if (chipList.Contains(parts[0].Trim()) == false)
                                        chipList.Add(parts[0].Trim());
                                }
                            }
                        }
                    }

                    chipList.Sort((x, y) => string.Compare(x, y));

                    foreach (string c in chipList)
                    {
                        this.dropAVRChips.Items.Add(c);
                    }

                    try
                    {
                        proc.Kill();
                    }
                    catch { }

                    return true;
                }
            }
            catch
            {
                TextBoxModify(this.txtOutput, "WinAVR's AVARICE could not be found", TextBoxChangeMode.SetNewLine);
                return false;
            }
        }

        private void DebuggerPanel_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveAllSettings();
        }

        private void SaveAllSettings()
        {
            SettingsManagement.LastGDBPort = Convert.ToInt32(this.numServerPort.Value);
            SettingsManagement.LastJTAGFreq = Convert.ToInt32(this.numBitRate.Value);
            SettingsManagement.LastICEHardware = ((string)this.dropHardwareSelection.Items[this.dropHardwareSelection.SelectedIndex]).Trim();
            SettingsManagement.LastICEOptions = this.txtAVaRICEOtherOpts.Text.Trim();
            SettingsManagement.LastJTAGPort = this.txtJTAGPort.Text.Trim();
        }

        public void EnableButtons()
        {
            foreach (ToolStripItem i in toolStrip1.Items)
            {
                i.Enabled = true;
            }
        }

        public void DisableButtons()
        {
            foreach (ToolStripItem i in toolStrip1.Items)
            {
                i.Enabled = false;
            }
        }

        public void BreakpointChanged(bool isAdding, string fileName, int lineNum)
        {
            throw new NotImplementedException();
        }

        private void SendToGDB(string text)
        {
            if (procGDB == null)
                return;

            if (procGDB.HasExited == true)
                return;

            if (text == null)
                return;

            try
            {
                procGDB.StandardInput.WriteLine(text.TrimEnd());
            }
            catch { }
        }

        private void tmrReader_Tick(object sender, EventArgs e)
        {
            string line = "";
            try
            {
                line = procGDB.StandardOutput.ReadLine().TrimEnd();
            }
            catch
            { }

            if (string.IsNullOrEmpty(line))
            {
                try
                {
                    line = procGDB.StandardError.ReadLine().TrimEnd();
                }
                catch { }
            }

            if (string.IsNullOrEmpty(line))
                return;

            if (txtOutput.Text.Length > txtOutput.MaxLength * 3 / 4)
                txtOutput.Text = txtOutput.Text.Substring(txtOutput.Text.Length / 2);

            HandleGDBOutput(line);

            txtOutput.Text += line + Environment.NewLine;

            txtOutput.Select(txtOutput.Text.Length, 0);
            txtOutput.ScrollToCaret();
        }

        private void HandleGDBOutput(string line)
        {
            throw new NotImplementedException();
        }
    }
}
