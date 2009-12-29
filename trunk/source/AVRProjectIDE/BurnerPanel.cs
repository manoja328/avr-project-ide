using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AVRProjectIDE
{
    public partial class BurnerPanel : UserControl
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
                ErrorReportWindow erw = new ErrorReportWindow(ex, "Error In Burner Panel");
                erw.ShowDialog();
            }
        }

        private AVRProject project;
        private ProjectBurner projBurner;

        public BurnerPanel(AVRProject project)
        {
            InitializeComponent();

            this.BackColor = System.Drawing.SystemColors.Control;

            this.project = project;

            projBurner = new ProjectBurner(project);

            dropProg.Items.Clear();
            dropProg.Items.AddRange(ProjectBurner.GetAvailProgrammers(true));

            dropPart.Items.Clear();
            if (dropProg.Items.Count > 0)
                dropPart.Items.AddRange(ProjectBurner.GetAvailParts((string)dropProg.Items[0], true));

            dropBaud.SelectedIndex = 0;

            ProjToForm();
        }

        public void ProjToForm()
        {
            if (dropPart.Items.Count > 0)
            {
                dropPart.SelectedIndex = 0;
                if (dropPart.Items.Contains(project.BurnPart))
                    dropPart.SelectedIndex = dropPart.Items.IndexOf(project.BurnPart);
                else
                    dropPart.SelectedIndex = dropPart.Items.Add(project.BurnPart);
            }
            else
                dropPart.SelectedIndex = dropPart.Items.Add(project.BurnPart);

            if (dropProg.Items.Count > 0)
            {
                dropProg.SelectedIndex = 0;
                if (dropProg.Items.Contains(project.BurnProgrammer))
                    dropProg.SelectedIndex = dropProg.Items.IndexOf(project.BurnProgrammer);
                else
                    dropProg.SelectedIndex = dropProg.Items.Add(project.BurnProgrammer);
            }
            else
                dropProg.SelectedIndex = dropProg.Items.Add(project.BurnProgrammer);

            txtPortOverride.Text = project.BurnPort.Trim();

            dropBaud.SelectedIndex = 0;
            if (dropBaud.Items.Contains(project.BurnBaud.ToString("0")))
            {
                dropBaud.SelectedIndex = dropBaud.Items.IndexOf(project.BurnBaud.ToString("0"));
            }
            else
            {
                if (project.BurnBaud != 0)
                {
                    int i = dropBaud.Items.Add(project.BurnBaud.ToString("0"));
                    dropBaud.SelectedIndex = i;
                }
            }

            txtBurnOpt.Text = project.BurnOptions;
            chkAutoReset.Checked = project.BurnAutoReset;
        }

        public void FormToProj()
        {
            project.BurnOptions = txtBurnOpt.Text;
            project.BurnPart = (string)dropPart.Items[dropPart.SelectedIndex];
            project.BurnProgrammer = (string)dropProg.Items[dropProg.SelectedIndex];
            project.BurnAutoReset = chkAutoReset.Checked;

            int baud = 0;
            string selectedText = (string)dropBaud.Items[dropBaud.SelectedIndex];
            if (int.TryParse(selectedText, out baud))
            {
                project.BurnBaud = int.Parse(selectedText);
            }
            else
            {
                project.BurnBaud = 0;
            }

            project.BurnPort = txtPortOverride.Text.Trim();
        }

        private void btnBurnOnlyOpt_Click(object sender, EventArgs e)
        {
            FormToProj();
            projBurner.BurnCMD(true, false, this);
        }

        private void dropProg_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (dropProg.SelectedIndex < 0)
                return;

            string prog = ((string)dropProg.Items[dropProg.SelectedIndex]).ToLowerInvariant();

            if (prog == "avrdoper")
            {
                if (string.IsNullOrEmpty(txtPortOverride.Text))
                    txtPortOverride.Text = "avrdoper";
            }
        }
    }
}
