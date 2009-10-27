using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Diagnostics;
using System.Windows.Forms;

namespace AVRProjectIDE
{
    public partial class AvrDudeWindow : Form
    {
        private AVRProject project;
        private AVRProject originalProject;
        private BurnerPanel burnerPanel;

        private Process avrdude = new Process();

        public AvrDudeWindow(AVRProject project)
        {
            InitializeComponent();

            this.originalProject = project;
            this.project = (AVRProject)project.Clone();

            burnerPanel = new BurnerPanel(this.project);
            grpboxBurnerPanel.Controls.Add(burnerPanel);
            burnerPanel.Dock = DockStyle.Fill;

            burnerPanel.ProjToForm();

            dropDetectionType.SelectedIndex = 0;
            dropMemoryType.SelectedIndex = 0;

            avrdude.StartInfo.FileName = "cmd";
        }

        private void AvrDudeWindow_FormClosed(object sender, FormClosedEventArgs e)
        {
            burnerPanel.FormToProj();

            TryKill();
        }

        private void TryKill()
        {
            try
            {
                avrdude.Kill();
            }
            catch { }
        }

        private void TryRun()
        {
            try
            {
                avrdude.Start();
            }
            catch (Exception ex)
            {
                ErrorReportWindow erw = new ErrorReportWindow(ex, "Error using AVRDUDE GUI");
                erw.ShowDialog();
            }
        }

        private string GetArgs(string memType, string overrides, string operationChar, string filePath, string fileFormat, bool verify)
        {
            return "/k avrdude " + String.Format(
                "-c {0} -p {1} {2} {3} {4}:{5}:\"{6}\":{7} {8}",
                project.BurnProgrammer, project.BurnPart, project.BurnOptions,
                overrides, memType, operationChar, filePath, fileFormat, verify ? "" : "-V");
        }

        private string GetMemType()
        {
            string memType = "flash";

            if (((string)dropMemoryType.Items[dropMemoryType.SelectedIndex]).ToLowerInvariant().Contains("flash"))
                memType = "flash";
            else if (((string)dropMemoryType.Items[dropMemoryType.SelectedIndex]).ToLowerInvariant().Contains("eeprom"))
                memType = "eeprom";

            return memType;
        }

        private string GetFileFormat()
        {
            string fileFormat = "a";

            if (((string)dropDetectionType.Items[dropDetectionType.SelectedIndex]).ToLowerInvariant().Contains("auto"))
                fileFormat = "a";
            else if (((string)dropDetectionType.Items[dropDetectionType.SelectedIndex]).ToLowerInvariant().Contains("intel"))
                fileFormat = "i";
            else if (((string)dropDetectionType.Items[dropDetectionType.SelectedIndex]).ToLowerInvariant().Contains("moto"))
                fileFormat = "s";
            else if (((string)dropDetectionType.Items[dropDetectionType.SelectedIndex]).ToLowerInvariant().Contains("raw"))
                fileFormat = "r";

            return fileFormat;
        }

        private string GetOverrides()
        {
            string overrides = "";

            if (string.IsNullOrEmpty(project.BurnPort) == false)
                overrides += "-P " + project.BurnPort;

            if (project.BurnBaud != 0)
                overrides += " -b " + project.BurnBaud.ToString("0");

            return overrides;
        }

        private void btnWrite_Click(object sender, EventArgs e)
        {
            TryKill();

            OpenFileDialog ofd = new OpenFileDialog();

            if (ofd.ShowDialog() != DialogResult.OK)
                return;

            avrdude.StartInfo.Arguments = GetArgs(GetMemType(), GetOverrides(), "w", ofd.FileName, GetFileFormat(), true);

            TryRun();
        }

        private void btnWriteNoVerify_Click(object sender, EventArgs e)
        {
            TryKill();

            OpenFileDialog ofd = new OpenFileDialog();

            if (ofd.ShowDialog() != DialogResult.OK)
                return;

            burnerPanel.FormToProj();

            avrdude.StartInfo.Arguments = GetArgs(GetMemType(), GetOverrides(), "w", ofd.FileName, GetFileFormat(), false);

            TryRun();
        }

        private void btnRead_Click(object sender, EventArgs e)
        {
            TryKill();

            SaveFileDialog sfd = new SaveFileDialog();

            if (sfd.ShowDialog() != DialogResult.OK)
                return;

            burnerPanel.FormToProj();

            avrdude.StartInfo.Arguments = GetArgs(GetMemType(), GetOverrides(), "r", sfd.FileName, GetFileFormat(), true);

            TryRun();
        }

        private void btnVerify_Click(object sender, EventArgs e)
        {
            TryKill();

            OpenFileDialog ofd = new OpenFileDialog();

            if (ofd.ShowDialog() != DialogResult.OK)
                return;

            burnerPanel.FormToProj();

            avrdude.StartInfo.Arguments = GetArgs(GetMemType(), GetOverrides(), "v", ofd.FileName, GetFileFormat(), true);

            TryRun();
        }

        private void btnFuseTool_Click(object sender, EventArgs e)
        {
            TryKill();

            burnerPanel.FormToProj();

            FuseCalculator fc = new FuseCalculator(project);
            fc.ShowDialog();

            burnerPanel.ProjToForm();
        }

        private void btnInteractive_Click(object sender, EventArgs e)
        {
            burnerPanel.FormToProj();

            if (ProjectBuilder.CheckForWinAVR())
            {
                System.Diagnostics.Process p = new System.Diagnostics.Process();
                p.StartInfo.FileName = "cmd";
                p.StartInfo.Arguments = "/k avrdude ";

                string overrides = "";

                if (string.IsNullOrEmpty(project.BurnPort) == false)
                    overrides += "-P " + project.BurnPort;

                if (project.BurnBaud != 0)
                    overrides += " -b " + project.BurnBaud.ToString("0");

                p.StartInfo.Arguments += String.Format("-c {0} -p {1} {2} {3} -t", project.BurnProgrammer, project.BurnPart, overrides, project.BurnOptions);
                p.Start();
            }
        }

        private void btnSaveAndClose_Click(object sender, EventArgs e)
        {
            burnerPanel.FormToProj();
            this.originalProject = this.project.CopyProperties(this.originalProject);
            this.Close();
        }

        private void btnDiscardAndClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
