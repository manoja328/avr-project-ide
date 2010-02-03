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

            txtException.Text = msg + Environment.NewLine + Environment.NewLine;
            txtException.Text += "Modified code files should have been backed up occasionally automatically, go look for them if the editor crashes." + Environment.NewLine;
            txtException.Text += "If this is an issue or bug, please report this to http://code.google.com/p/avr-project-ide/issues/list" + Environment.NewLine;
            txtException.Text += Environment.NewLine + "Message: " + ex.Message + Environment.NewLine;
            txtException.Text += Environment.NewLine + "Target Site: " + ex.TargetSite + Environment.NewLine;
            txtException.Text += Environment.NewLine + "Stack Trace: " + ex.StackTrace + Environment.NewLine;
            txtException.Text += Environment.NewLine + "Source: " + ex.Source + Environment.NewLine;
            txtException.Text += Environment.NewLine + "Inner Exception: " + ex.InnerException + Environment.NewLine;
        }
    }
}
