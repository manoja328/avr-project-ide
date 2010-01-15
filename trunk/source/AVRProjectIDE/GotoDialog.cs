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
    public partial class GotoDialog : Form
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
                ErrorReportWindow.Show(ex, "Error In Goto Dialog");
                
            }
        }

        public int SelectedLineNum
        {
            get { return Convert.ToInt32(numLine.Value); }
        }

        public GotoDialog(int current, int max)
        {
            InitializeComponent();

            DialogResult = DialogResult.Cancel;

            numLine.Minimum = 1;
            numLine.Maximum = max;
            numLine.Value = Math.Max(numLine.Minimum, Math.Min(current, numLine.Maximum));
            numLine.Focus();
            numLine.Select(0, 99);
        }

        private void btnGo_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void numLine_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                DialogResult = DialogResult.OK;
                this.Close();
            }
        }
    }
}
