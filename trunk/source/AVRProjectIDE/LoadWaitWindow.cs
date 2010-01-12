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
    public partial class LoadWaitWindow : Form
    {
        public LoadWaitWindow()
        {
            InitializeComponent();

            this.progressBar1.Style = ProgressBarStyle.Marquee;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                this.BringToFront();
            }
            catch { }
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
                ErrorReportWindow erw = new ErrorReportWindow(ex, "Error In Loading Wait Window");
                erw.ShowDialog();
            }
        }
    }
}
