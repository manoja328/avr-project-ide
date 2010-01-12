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

        const string BUTTON_TEXT = "Run '{0}'";
        private AVRProject project;
        private ProjectBurner projBurner;
        private bool allowEvents = false;

        public BurnerPanel(AVRProject project)
        {
            allowEvents = false;

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

        /// <summary>
        /// Load form fields with project properties
        /// </summary>
        public void ProjToForm()
        {
            allowEvents = false;
            readyForEvents = false;

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

            SetButtonText();

            allowEvents = true;
            readyForEvents = true;
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
                project.BurnBaud = baud;
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

        public static void GetPortOverride(ref string args, AVRProject project)
        {
            string res = "";

            try
            {
                if (string.IsNullOrEmpty(project.BurnPort) == false)
                {
                    res = "-P " + project.BurnPort;
                    if (project.BurnPort.Length > 3)
                    {
                        int portNum;
                        if (project.BurnPort.StartsWith("COM") && int.TryParse(project.BurnPort.Substring(3), out portNum))
                        {
                            res = "-P //./" + project.BurnPort;
                        }
                    }
                }
            }
            catch { }

            args += res;

            res = "";

            if (project.BurnBaud != 0)
                res = " -b " + project.BurnBaud.ToString("0");

            args += res;
        }

        private string GetArgs()
        {
            return BurnerPanel.GetArgs(this.project);
        }

        public static string GetArgs(AVRProject project)
        {
            string overrides = "";
            BurnerPanel.GetPortOverride(ref overrides, project);
            string res = String.Format("-p {0} -c {1} {2} {3}", project.BurnPart.ToUpperInvariant(), project.BurnProgrammer, overrides, project.BurnOptions);

            while (res.Contains("  "))
                res = res.Replace("  ", " ");

            return res;
        }

        private void txt_TextChanged(object sender, EventArgs e)
        {
            if (allowEvents == false)
                return;

            if (readyForEvents == false)
                return;

            FormToProj();
            SetButtonText();
        }

        private void SetButtonText()
        {
            btnBurnOnlyOpt.Text = String.Format(BUTTON_TEXT, BurnerPanel.GetArgs(this.project));
        }

        bool readyForEvents = false;
        private void drop_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (readyForEvents)
                txt_TextChanged(sender, e);
        }

        private void txtPortOverride_Validating(object sender, CancelEventArgs e)
        {
            if (txtPortOverride.Text.Length > 3)
            {
                int portNum;
                if (txtPortOverride.Text.ToUpperInvariant().StartsWith("COM") && int.TryParse(txtPortOverride.Text.Substring(3), out portNum))
                {
                    txtPortOverride.Text = "COM" + portNum.ToString("0");
                }
            }
        }
    }
}
