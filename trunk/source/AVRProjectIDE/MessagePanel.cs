using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ScintillaNet;
using WeifenLuo.WinFormsUI.Docking;

namespace AVRProjectIDE
{
    public partial class MessagePanel : DockContent
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
                ErrorReportWindow erw = new ErrorReportWindow(ex, "Error In Message Panel");
                erw.ShowDialog();
            }
        }

        public MessagePanel()
        {
            InitializeComponent();

            this.Icon = Properties.Resources.warningIco;
        }

        public TextBox MyTextBox
        {
            get { return txtMessages; }
        }

        public ListView MyListView
        {
            get { return listErrorsWarnings; }
        }

        private delegate void BoxModifyCallback(TextBoxChangeMode mode, string text);

        public void MessageBoxModify(TextBoxChangeMode mode, string text)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new BoxModifyCallback(MessageBoxModify), new object[] { mode, text, });
            }
            else
            {
                TextBox box = txtMessages;

                if (ProjectBuilder.ReverseOutput)
                {
                    if (mode == TextBoxChangeMode.Append)
                        mode = TextBoxChangeMode.Prepend;
                    else if (mode == TextBoxChangeMode.AppendNewLine)
                        mode = TextBoxChangeMode.PrependNewLine;
                    else if (mode == TextBoxChangeMode.Prepend)
                        mode = TextBoxChangeMode.Append;
                    else if (mode == TextBoxChangeMode.PrependNewLine)
                        mode = TextBoxChangeMode.AppendNewLine;
                }

                if (mode == TextBoxChangeMode.Append)
                {
                    box.Text += text;
                    box.SelectionStart = box.Text.Length;
                    box.SelectionLength = 0;
                    box.ScrollToCaret();
                }
                else if (mode == TextBoxChangeMode.AppendNewLine)
                {
                    box.Text += Environment.NewLine + text;
                    box.SelectionStart = box.Text.Length;
                    box.SelectionLength = 0;
                    box.ScrollToCaret();
                }
                else if (mode == TextBoxChangeMode.Prepend)
                {
                    box.Text = text + box.Text;
                    box.SelectionStart = 0;
                    box.SelectionLength = 0;
                    box.ScrollToCaret();
                }
                else if (mode == TextBoxChangeMode.PrependNewLine)
                {
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

        public void ClearErrors()
        {
            listErrorsWarnings.Items.Clear();
        }

        public void AddErrorWarning(string fileName, int line, string location, string type, string message)
        {
            if (location.Contains('\''))
            {
                int start = location.IndexOf('\'') + 1;
                int end = location.LastIndexOf('\'');
                location = location.Substring(start, end - start);
            }

            string lineStr = "";
            if (line >= 0)
                line.ToString("0");

            ListViewItem lvi = new ListViewItem(new string[] { fileName, lineStr, location, type, message, });
            if (ProjectBuilder.ReverseOutput == false)
            {
                listErrorsWarnings.Items.Insert(0, lvi);
            }
            else
            {
                listErrorsWarnings.Items.Add(lvi);
            }
        }

        public event OnClickError GotoError;
        public delegate void OnClickError(string fileName, int line);

        private void listErrorsWarnings_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (listErrorsWarnings.SelectedIndices.Count > 0)
            {
                try
                {
                    string fileName = listErrorsWarnings.SelectedItems[0].SubItems[0].Text;
                    int line = int.Parse(listErrorsWarnings.SelectedItems[0].SubItems[1].Text);
                    GotoError(fileName, line);
                }
                catch { }
            }
        }

        private delegate void NothingDelegate();

        public void SwitchToListView()
        {
            if (tabControl1.InvokeRequired)
            {
                tabControl1.Invoke(new NothingDelegate(SwitchToListView));
            }
            else
            {
                tabControl1.SelectedIndex = 1;
            }
        }

        public void SwitchToMessageBox()
        {
            if (tabControl1.InvokeRequired)
            {
                tabControl1.Invoke(new NothingDelegate(SwitchToMessageBox));
            }
            else
            {
                tabControl1.SelectedIndex = 0;
            }
        }
    }
}
