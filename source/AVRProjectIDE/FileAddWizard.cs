using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AVRProjectIDE
{
    public partial class FileAddWizard : Form
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
                ErrorReportWindow erw = new ErrorReportWindow(ex, "Error In File Add Wizard");
                erw.ShowDialog();
            }
        }

        private AVRProject project;

        private string SelectedExtension
        {
            get
            {
                if (this.dropFileExt.Items.Contains(this.dropFileExt.Text))
                {
                    this.dropFileExt.SelectedIndex = this.dropFileExt.Items.IndexOf(this.dropFileExt.Text);
                    return (string)this.dropFileExt.Items[this.dropFileExt.SelectedIndex];
                }
                else if (this.dropFileExt.SelectedIndex >= 0)
                    return (string)this.dropFileExt.Items[this.dropFileExt.SelectedIndex];
                else
                    return (string)this.dropFileExt.Text;
            }

            set
            {
                if (this.dropFileExt.Items.Contains(value))
                {
                    this.dropFileExt.SelectedIndex = this.dropFileExt.Items.IndexOf(value);
                }
                else
                    this.dropFileExt.Text = value;
            }
        }

        private ProjectFile createdFile = null;
        public ProjectFile CreatedFile
        {
            get { return createdFile; }
        }

        public FileAddWizard(AVRProject project)
        {
            InitializeComponent();

            this.project = project;

            if (project.FileList.Count == 0)
            {
                this.txtFileName.Text = project.FileNameNoExt;
            }

            this.SelectedExtension = SettingsManagement.LastFileExt;

            this.dropTemplates.Items.Clear();
            this.dropTemplates.Items.Add("none");
            this.dropTemplates.SelectedIndex = this.dropTemplates.Items.IndexOf("none");

            foreach (FileInfo f in new DirectoryInfo(FileTemplate.TemplateFolderPath).GetFiles())
            {
                if (f.Name.ToLowerInvariant().Trim().EndsWith(".txt"))
                    this.dropTemplates.Items.Add(Path.GetFileNameWithoutExtension(f.Name));
            }

            if (project.FileList.Count > 0)
            {
                if (this.SelectedExtension == "c" || this.SelectedExtension == "cpp" || this.SelectedExtension == "pde")
                {
                    if (this.dropTemplates.Items.Contains("defaultcode"))
                        this.dropTemplates.SelectedIndex = this.dropTemplates.Items.IndexOf("defaultcode");
                }
                else if (this.SelectedExtension == "h" || this.SelectedExtension == "hpp")
                {
                    if (this.dropTemplates.Items.Contains("defaultheader"))
                        this.dropTemplates.SelectedIndex = this.dropTemplates.Items.IndexOf("defaultheader");
                }
            }
            else
            {
                if (this.SelectedExtension == "c" || this.SelectedExtension == "cpp")
                {
                    if (this.dropTemplates.Items.Contains("initialmain"))
                        this.dropTemplates.SelectedIndex = this.dropTemplates.Items.IndexOf("initialmain");
                }
                else if (this.SelectedExtension == "pde")
                {
                    if (this.dropTemplates.Items.Contains("initialpde"))
                        this.dropTemplates.SelectedIndex = this.dropTemplates.Items.IndexOf("initialpde");
                }
                else if (this.SelectedExtension == "h" || this.SelectedExtension == "hpp")
                {
                    if (this.dropTemplates.Items.Contains("defaultheader"))
                        this.dropTemplates.SelectedIndex = this.dropTemplates.Items.IndexOf("defaultheader");
                }
            }

            this.txtDirLoc.Text = project.DirPath.TrimEnd(new char[] { Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar, }) + Path.DirectorySeparatorChar;
            this.txtDirLoc.SelectionStart = this.txtDirLoc.Text.Length;
            this.txtDirLoc.SelectionLength = 0;
            this.txtDirLoc.ScrollToCaret();
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.SelectedPath = project.DirPath;
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                this.txtDirLoc.Text = fbd.SelectedPath.TrimEnd(new char[] { Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar, }) + Path.DirectorySeparatorChar;
                this.txtDirLoc.SelectionStart = this.txtDirLoc.Text.Length;
                this.txtDirLoc.SelectionLength = 0;
                this.txtDirLoc.ScrollToCaret();
            }
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            string fileName = txtFileName.Text + "." + this.SelectedExtension;

            if (
                fileName.Contains(" ") ||
                fileName.Contains("\t") ||
                fileName.Contains("\r") ||
                fileName.Contains("\n") ||
                fileName.Contains("\0") ||
                fileName.Contains("\\") ||
                fileName.Contains("/")
                )
            {
                MessageBox.Show("Invalid Character in File Name");
                return;
            }

            foreach (char c in Path.GetInvalidFileNameChars())
            {
                if (fileName.Contains(char.ToString(c)))
                {
                    MessageBox.Show("Invalid Character '" + c + "' in File Name");
                    return;
                }
            }

            string fileAbsPath = txtDirLoc.Text + fileName;
            foreach (char c in Path.GetInvalidPathChars())
            {
                if (fileAbsPath.Contains(char.ToString(c)))
                {
                    MessageBox.Show("Invalid Character '" + c + "' in File Path");
                    return;
                }
            }

            if (project.FileList.ContainsKey(fileName))
            {
                if (project.FileList[fileName].Exists)
                {
                    MessageBox.Show("File already exists in the project");
                    return;
                }
            }
            else
            {
                project.FileList.Add(fileName, new ProjectFile(fileAbsPath, project));
            }

            if (project.FileList[fileName].Exists == false)
            {
                if (((string)dropTemplates.Items[dropTemplates.SelectedIndex]) != "none")
                {
                    try
                    {
                        File.WriteAllText(fileAbsPath, FileTemplate.CreateFile(fileName, project.FileNameNoExt, ((string)dropTemplates.Items[dropTemplates.SelectedIndex]) + ".txt"));
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error writing file from template, " + ex.Message);
                    }
                }
                else
                {
                    try
                    {
                        File.WriteAllText(fileAbsPath, "");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error creating file, " + ex.Message);
                    }
                }
            }

            createdFile = project.FileList[fileName];

            if (project.FileList.Count == 1)
                project.FileList[fileName].IsOpen = true;

            if (project.Save() == SaveResult.Failed)
            {
                MessageBox.Show("Error saving project");
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
