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
    public partial class CopyCatWizard : Form
    {
        AVRProject project;

        private CopyCatWizard(AVRProject project)
        {
            InitializeComponent();

            this.project = project;

            txtPath.Text = project.DirPath;
        }

        protected override void WndProc(ref Message m)
        {
            try
            {
                base.WndProc(ref m);
            }
            catch (Exception ex)
            {
                ErrorReportWindow.Show(ex, "Error from CopyCatWizard");
            }
        }

        public static void Show(AVRProject project)
        {
            CopyCatWizard ccw = new CopyCatWizard(project);
            ccw.Show();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnClone_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtPath.Text))
            {
                MessageBox.Show("The path cannot be empty");
                return;
            }

            if (Program.MakeSurePathExists(txtPath.Text))
            {
                string targetDir = txtPath.Text + Path.DirectorySeparatorChar + project.FileNameNoExt;
                if (Program.MakeSurePathExists(targetDir))
                {
                    AVRProject newProject = new AVRProject();
                    newProject = project.CopyProperties(newProject);
                    newProject.FileList.Clear();
                    newProject.FilePath = targetDir + Path.DirectorySeparatorChar + project.FileNameNoExt + ".avrproj";
                    foreach (KeyValuePair<string, ProjectFile> file in project.FileList)
                    {
                        try
                        {
                            string nfpath;
                            if (file.Value.FileRelProjPath.StartsWith(".."))
                            {
                                if (radCopyAll.Checked)
                                {
                                    nfpath = targetDir + Path.DirectorySeparatorChar + file.Value.FileName;
                                }
                                else
                                {
                                    nfpath = file.Value.FileAbsPath;
                                }
                            }
                            else
                            {
                                nfpath = targetDir + Path.DirectorySeparatorChar + file.Value.FileRelProjPath;
                            }

                            ProjectFile npf = new ProjectFile(nfpath, newProject);
                            npf.Options = file.Value.Options;
                            npf.ToCompile = file.Value.ToCompile;

                            if (file.Value.FileAbsPath != nfpath)
                            {
                                if (Program.MakeSurePathExists(npf.FileDir))
                                {
                                    try
                                    {
                                        File.Copy(file.Value.FileAbsPath, nfpath, true);
                                        newProject.FileList.Add(file.Key, npf);
                                    }
                                    catch (Exception ex)
                                    {
                                        MessageBox.Show("Error while copying '" + file.Key + "' to '" + nfpath + "' : " + ex.Message);
                                        return;
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Error while creating '" + npf.FileDir + "'");
                                    return;
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error during the cloning process: " + ex.Message);
                            return;
                        }
                    }

                    try
                    {
                        if (chkCopyUnmanaged.Checked)
                        {
                            Program.CopyAll(new DirectoryInfo(project.DirPath), new DirectoryInfo(targetDir));
                        }

                        if (newProject.Save() == SaveResult.Successful)
                        {
                            if (chkOpenAfterClone.Checked)
                            {
                                try
                                {
                                    System.Diagnostics.Process.Start(newProject.DirPath);
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show("Could not open folder: " + ex.Message);
                                }
                            }

                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("An error occured, the project XML (.avrproj) file did not save.");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error during the cloning process: " + ex.Message);
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("The path cannot be found or created");
                    return;
                }
            }
            else
            {
                MessageBox.Show("The path cannot be found or created");
                return;
            }

            this.Close();
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.SelectedPath = txtPath.Text;
            if (fbd.ShowDialog() == DialogResult.OK)
                txtPath.Text = fbd.SelectedPath.TrimEnd(Path.DirectorySeparatorChar);
        }
    }
}
