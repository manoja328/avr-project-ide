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
        private AVRProject project;

        private bool overrideFolder = false;
        private bool overrideFilename = false;

        public Wizard(AVRProject project)
        {
            this.project = project;

            InitializeComponent();

            this.DialogResult = DialogResult.Cancel;

            dropFileType.SelectedIndex = 0;

            string[] templateList = ProjTemplate.GetTemplateNames();
            foreach (string tempName in templateList)
            {
                dropTemplates.Items.Add(tempName);
            }
            if (dropTemplates.Items.Count == 0)
            {
                dropTemplates.Items.Add("No Templates Available");
            }
            dropTemplates.SelectedIndex = 0;

            txtFolderPath.Text = SettingsManagement.FavFolder + Path.DirectorySeparatorChar;

            string newFileName = DateTime.Now.ToLongDateString().Replace(' ', '_');
            char[] illegalChars = Path.GetInvalidFileNameChars();
            foreach (char c in illegalChars)
            {
                newFileName.Replace(c, '_');
            }

            txtProjName.Text = newFileName;
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
            {
                MessageBox.Show("Error: Could Not Create Folder");
                return;
            }

            string projFilePath = folderPath + Path.DirectorySeparatorChar + projFilename + ".avrproj";
            string ext = "c";
            if (((string)dropFileType.Items[dropFileType.SelectedIndex]).Contains("(.c)"))
                ext = "c";
            else if (((string)dropFileType.Items[dropFileType.SelectedIndex]).Contains("(.cpp)"))
                ext = "cpp";
            else if (((string)dropFileType.Items[dropFileType.SelectedIndex]).Contains("(.pde)"))
                ext = "pde";

            string iniFilePath = folderPath + Path.DirectorySeparatorChar + iniFilename + "." + ext;

            if (File.Exists(projFilePath))
            {
                if (MessageBox.Show("Project File Already Exists at the Location, Overwrite it?", "Overwrite?", MessageBoxButtons.YesNo) == DialogResult.No)
                {
                    return;
                }
            }

            if (hasIniFile && File.Exists(iniFilePath))
            {
                if (MessageBox.Show("Initial File Already Exists at the Location, Overwrite it?", "Overwrite?", MessageBoxButtons.YesNo) == DialogResult.No)
                {
                    return;
                }
            }

            if (hasIniFile)
            {
                try
                {
                    StreamWriter writer = new StreamWriter(iniFilePath);
                    if (ext != "pde")
                    {
                        writer.Write("\r\n\r\nint main()\r\n{\r\n\t\r\n\treturn 0;\r\n}\r\n");
                    }
                    else
                    {
                        writer.Write("\r\n\r\nvoid setup()\r\n{\r\n\t\r\n}\r\n\r\nvoid loop()\r\n{\r\n\t\r\n}\r\n");
                    }
                    writer.WriteLine();
                    writer.Close();
                    ProjectFile newFile = new ProjectFile(iniFilePath);
                    newFile.IsOpen = true;
                    project.FileList.Add(newFile.FileName, newFile);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error While Creating Initial File, " + ex.Message);
                }
            }

            ProjTemplate.ApplyTemplate((string)dropTemplates.Items[dropTemplates.SelectedIndex], project);

            project.FilePath = projFilePath;

            if (project.Save(projFilePath) == false)
            {
                MessageBox.Show("Error While Saving Project");
            }

            if (project.Open(projFilePath) == false)
            {
                MessageBox.Show("Error While Opening Project");
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
    }
}
