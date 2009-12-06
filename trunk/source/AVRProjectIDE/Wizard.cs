using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AVRProjectIDE
{
    public partial class Wizard : Form
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
                ErrorReportWindow erw = new ErrorReportWindow(ex, "Error In Project Wizard");
                erw.ShowDialog();
            }
        }

        private AVRProject project;

        private bool overrideFolder = false;
        private bool overrideFilename = false;

        public Wizard(AVRProject project)
        {
            this.project = project;

            InitializeComponent();

            this.DialogResult = DialogResult.Cancel;

            string[] templateList = ProjTemplate.GetTemplateNames();
            foreach (string tempName in templateList)
            {
                dropTemplates.Items.Add(tempName);
            }
            if (dropTemplates.Items.Count == 0)
            {
                dropTemplates.Items.Add("No Templates Available");
            }

            string lastTemp = SettingsManagement.LastTemplate;
            if (lastTemp == null) lastTemp = "";
            if (dropTemplates.Items.Contains(lastTemp))
                dropTemplates.SelectedIndex = dropTemplates.Items.IndexOf(lastTemp);
            else
                dropTemplates.SelectedIndex = 0;

            // warning, hack
            string lastFileType = SettingsManagement.LastInitialFileType;
            if (lastFileType == null) lastFileType = "";
            if (dropFileType.Items.Contains(lastFileType))
                dropFileType.SelectedIndex = dropFileType.Items.IndexOf(lastFileType);
            else
                dropFileType.SelectedIndex = 0;

            if (dropFileType.SelectedIndex < 0) dropFileType.SelectedIndex = 0;
            if (dropTemplates.SelectedIndex < 0) dropTemplates.SelectedIndex = 0;


            if (string.IsNullOrEmpty(SettingsManagement.FavFolder))
            {
                string mydocs = Program.CleanFilePath(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)) + Path.DirectorySeparatorChar + "Projects";
                SettingsManagement.FavFolder = mydocs;
            }

            txtFolderPath.Text = SettingsManagement.FavFolder + Path.DirectorySeparatorChar;

            string newFileName = DateTime.Now.ToString("MM_dd_yyyy").Replace(' ', '_').Replace('-', '_').Replace(Path.AltDirectorySeparatorChar, '_').Replace(Path.DirectorySeparatorChar, '_'); ;
            char[] illegalChars = Path.GetInvalidFileNameChars();
            foreach (char c in illegalChars)
            {
                newFileName.Replace(c, '_');
            }

            Random r = new Random();

            txtProjName.Text = "Sketch_" + newFileName + "_" + r.Next(15).ToString("X");
        }

        private void txtProjName_TextChanged(object sender, EventArgs e)
        {
            if (overrideFolder == false)
            {
                txtFolderPath.Text = SettingsManagement.FavFolder + Path.DirectorySeparatorChar + txtProjName.Text + Path.DirectorySeparatorChar;
            }
            if (overrideFilename == false)
            {
                txtInitialFilename.Text = txtProjName.Text;
            }
        }

        private void btnFindFolder_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.SelectedPath = txtFolderPath.Text;
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                string newFolderPath = Program.CleanFilePath(fbd.SelectedPath)+ Path.DirectorySeparatorChar;
                if (newFolderPath != txtFolderPath.Text)
                {
                    overrideFolder = true;
                }
                txtFolderPath.Text = newFolderPath;
            }
        }

        private void txtInitialFilename_TextChanged(object sender, EventArgs e)
        {
            if (txtInitialFilename.Text != txtProjName.Text)
                overrideFilename = true;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            string iniFilename = txtInitialFilename.Text.Trim();
            bool hasIniFile = !string.IsNullOrEmpty(iniFilename);
            hasIniFile = false;
            string projFilename = txtProjName.Text.Trim();
            char[] forbidChars = Path.GetInvalidFileNameChars();
            foreach (char c in forbidChars)
            {
                if (hasIniFile && iniFilename.Contains(c))
                {
                    MessageBox.Show("Illegal Character in Initial File Name");
                    return;
                }

                if (projFilename.Contains(c))
                {
                    MessageBox.Show("Illegal Character in Project File Name");
                    return;
                }
            }

            if (hasIniFile)
            {
                if (iniFilename.Contains('/') || iniFilename.Contains('\\') || iniFilename.Contains(Path.DirectorySeparatorChar) || iniFilename.Contains(Path.AltDirectorySeparatorChar))
                {
                    MessageBox.Show("Illegal Character in Initial File Name");
                    return;
                }

                if (iniFilename.Contains('.') || iniFilename.Contains(' ') || iniFilename.Contains('\t'))
                {
                    MessageBox.Show("No Spaces or Dots are Allowed in Initial File Name");
                    return;
                }
            }

            if (projFilename.Contains('/') || projFilename.Contains('\\') || projFilename.Contains(Path.DirectorySeparatorChar) || projFilename.Contains(Path.AltDirectorySeparatorChar))
            {
                MessageBox.Show("Illegal Character in Project File Name");
                return;
            }

            if (projFilename.Contains('.'))
            {
                MessageBox.Show("No Dots are Allowed in Project File Name");
                return;
            }

            string folderPath = Program.CleanFilePath(txtFolderPath.Text);

            if (Program.MakeSurePathExists(folderPath) == false)
            //if (Directory.Exists(folderPath))
            {
                MessageBox.Show("Error Creating Folder");
                //MessageBox.Show("Error: Folder Invalid");
                return;
            }

            string projFilePath = folderPath + Path.DirectorySeparatorChar + projFilename + ".avrproj";

            project.FilePath = projFilePath;

            string ext = "c";
            if (((string)dropFileType.Items[dropFileType.SelectedIndex]).Contains("C++"))
                ext = "cpp";
            else if (((string)dropFileType.Items[dropFileType.SelectedIndex]).Contains("C"))
                ext = "c";
            else if (((string)dropFileType.Items[dropFileType.SelectedIndex]).Contains("Arduino"))
                ext = "pde";

            string iniFilePath = folderPath + Path.DirectorySeparatorChar + iniFilename + "." + ext;

            if (File.Exists(projFilePath))
            {
                if (MessageBox.Show("Project File Already Exists at the Location, Overwrite it?", "Overwrite?", MessageBoxButtons.YesNo) == DialogResult.No)
                {
                    return;
                }
            }

            bool merge = false;

            if (hasIniFile && File.Exists(iniFilePath))
            {
                if (MessageBox.Show("Initial File Already Exists at the Location, Merge it with your project?", "Merge File?", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    merge = true;
                }
                else if (MessageBox.Show("Maybe you'd rather overwrite it with a blank file?", "Overwrite File?", MessageBoxButtons.YesNo) == DialogResult.No)
                {
                    return;
                }
            }

            if (hasIniFile)
            {
                if (merge == false)
                {
                    try
                    {
                        StreamWriter writer = new StreamWriter(iniFilePath);
                        if (ext == "pde")
                        {
                            writer.Write(FileTemplate.CreateFile(iniFilename + "." + ext, projFilename, "initialpde.txt"));
                        }
                        else
                        {
                            writer.Write(FileTemplate.CreateFile(iniFilename + "." + ext, projFilename, "initialmain.txt"));
                        }
                        writer.WriteLine();
                        writer.Close();
                    }
                    catch (Exception ex)
                    {
                        ErrorReportWindow erw = new ErrorReportWindow(ex, "Error while creating initial file");
                        erw.ShowDialog();
                    }
                }

                ProjectFile newFile = new ProjectFile(iniFilePath, project);
                newFile.IsOpen = true;
                project.FileList.Add(newFile.FileName, newFile);
            }

            ProjTemplate.ApplyTemplate((string)dropTemplates.Items[dropTemplates.SelectedIndex], project);

            project.FilePath = projFilePath;

            FileAddWizard faw = new FileAddWizard(project);
            faw.ShowDialog();

            if (project.Save(projFilePath) == false)
            {
                MessageBox.Show("Error While Saving Project");
            }
            else
            {
                if (project.Open(projFilePath) == false)
                {
                    MessageBox.Show("Error While Opening Project");
                }
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void txtFolderPath_TextChanged(object sender, EventArgs e)
        {
            txtFolderPath.SelectionStart = txtFolderPath.Text.Length;
            txtFolderPath.SelectionLength = 0;
            txtFolderPath.ScrollToCaret();
        }

        private void dropFileType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (dropFileType.SelectedIndex >= 0)
                SettingsManagement.LastInitialFileType = (string)dropFileType.Items[dropFileType.SelectedIndex];
        }

        private void dropTemplates_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (dropTemplates.SelectedIndex >= 0)
                SettingsManagement.LastTemplate = (string)dropTemplates.Items[dropTemplates.SelectedIndex];
        }
    }
}
