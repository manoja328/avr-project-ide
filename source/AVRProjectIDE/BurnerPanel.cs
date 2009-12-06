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

            dropPort.Items.Clear();
            dropPort.Items.Add("No Override");
            dropPort.Items.Add("LPT1");
            dropPort.Items.Add("LPT2");
            dropPort.Items.Add("LPT3");
            string[] portList = System.IO.Ports.SerialPort.GetPortNames();
            foreach (string port in portList)
            {
                dropPort.Items.Add(port);
            }
            dropPort.SelectedIndex = 0;
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

            if (dropPort.Items.Count > 0)
            {
                dropPort.SelectedIndex = 0;
                if (dropPort.Items.Contains(project.BurnPort))
                {
                    dropPort.SelectedIndex = dropPort.Items.IndexOf(project.BurnPort);
                }
                else
                {
                    if (string.IsNullOrEmpty(project.BurnPort) == false)
                    {
                        int i = dropPort.Items.Add(project.BurnPort);
                        dropPort.SelectedIndex = i;
                    }
                }
            }

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

            selectedText = (string)dropPort.Items[dropPort.SelectedIndex];
            if (selectedText == "No Override")
            {
                project.BurnPort = "";
            }
            else
            {
                project.BurnPort = selectedText;
            }
        }

        private void btnBurnOnlyOpt_Click(object sender, EventArgs e)
        {
            FormToProj();
            projBurner.BurnCMD(true, false, this);
        }
    }
}
