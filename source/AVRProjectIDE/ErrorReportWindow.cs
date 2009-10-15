﻿using System;
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
        public ErrorReportWindow(Exception ex, string msg)
        {
            InitializeComponent();

            txtException.Text = msg + "\r\n\r\n";
            txtException.Text += "Modified code files should have been backed up occasionally automatically, go look for them if the editor crashes.\r\n";
            txtException.Text += "If This is an Issue, Please Report This To http://code.google.com/p/avr-project-ide/issues/list\r\n";
            txtException.Text += "\r\nMessage: " + ex.Message + "\r\n";
            txtException.Text += "\r\nTarget Site: " + ex.TargetSite + "\r\n";
            txtException.Text += "\r\nStack Trace: " + ex.StackTrace + "\r\n";
            txtException.Text += "\r\nSource: " + ex.Source + "\r\n";
            txtException.Text += "\r\nInner Exception: " + ex.InnerException + "\r\n";
        }
    }
}