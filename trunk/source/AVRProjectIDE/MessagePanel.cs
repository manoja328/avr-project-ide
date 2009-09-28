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
                if (mode == TextBoxChangeMode.Append)
                    txtMessages.Text += text;
                else if (mode == TextBoxChangeMode.AppendNewLine)
                    txtMessages.Text += "\r\n" + text;
                else if (mode == TextBoxChangeMode.Prepend)
                    txtMessages.Text = text + txtMessages.Text;
                else if (mode == TextBoxChangeMode.PrependNewLine)
                    txtMessages.Text = text + "\r\n" + txtMessages.Text;
                else if (mode == TextBoxChangeMode.Set)
                    txtMessages.Text = text;
                else if (mode == TextBoxChangeMode.SetNewLine)
                    txtMessages.Text = text + "\r\n";
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

            ListViewItem lvi = new ListViewItem(new string[] { fileName, line.ToString(), location, type, message, });
            listErrorsWarnings.Items.Add(lvi);
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
    }
}
