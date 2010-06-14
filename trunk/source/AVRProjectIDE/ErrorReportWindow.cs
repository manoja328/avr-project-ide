using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AVRProjectIDE
{
    public partial class ErrorReportWindow : Form
    {
        public static void Show(Exception ex, string msg)
        {
            new ErrorReportWindow(ex, msg).ShowDialog();
        }

        private ErrorReportWindow(Exception ex, string msg)
        {
            InitializeComponent();

            try
            {
                txtException.Text = msg + Environment.NewLine + Environment.NewLine;
                txtException.Text += "Modified code files should have been backed up occasionally automatically, go look for them if the editor crashes." + Environment.NewLine;
                txtException.Text += "If this is an issue or bug, please report this to http://code.google.com/p/avr-project-ide/issues/list" + Environment.NewLine;
            }
            catch { }
            try { txtException.Text += Environment.NewLine + "Version: " + Properties.Resources.BuildID + Environment.NewLine; }
            catch { }
            try { txtException.Text += Environment.NewLine + "Date: " + DateTime.Now.ToString("MMMM d yyyy") + Environment.NewLine; }
            catch { }
            try { txtException.Text += Environment.NewLine + "OS: " + Environment.OSVersion + Environment.NewLine; }
            catch { }
            try { txtException.Text += Environment.NewLine + "Environment Version: " + Environment.Version + Environment.NewLine; }
            catch { }
            try { txtException.Text += Environment.NewLine + "Message: " + ex.Message + Environment.NewLine; }
            catch { }
            try { txtException.Text += Environment.NewLine + "Target Site: " + ex.TargetSite + Environment.NewLine; }
            catch { }
            try { txtException.Text += Environment.NewLine + "Stack Trace: " + ex.StackTrace + Environment.NewLine; }
            catch { }
            try { txtException.Text += Environment.NewLine + "Source: " + ex.Source + Environment.NewLine; }
            catch { }
            try
            {
                string ie = ex.InnerException.ToString();
                if (ie != null)
                {
                    if (string.IsNullOrEmpty(ie.Trim()) == false)
                    {
                        txtException.Text += Environment.NewLine + "Inner Exception: " + ie.Trim() + Environment.NewLine;
                    }
                }
            }
            catch { }
        }
    }
}
