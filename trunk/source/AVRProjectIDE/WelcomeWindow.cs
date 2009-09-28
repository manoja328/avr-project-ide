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
    public partial class WelcomeWindow : Form
    {
        private AVRProject project;

        public WelcomeWindow(AVRProject project)
        {
            InitializeComponent();

            SettingsManagement.FillListBox(listRecentFiles);

            this.project = project;
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            if (listRecentFiles.SelectedIndex >= 0)
            {
                string file = Program.CleanFilePath(SettingsManagement.FilePathFromListBoxIndex(listRecentFiles.SelectedIndex));
                if (project.Open(file))
                {
                    SettingsManagement.AddFileAsMostRecent(file);
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Error, Could Not Load Project");
                }
            }
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            SaveResult res = project.CreateNew();
            if (res == SaveResult.Successful)
            {
                SettingsManagement.AddFileAsMostRecent(project.FilePath);
                this.Close();
            }
            else if (res == SaveResult.Failed)
            {
                MessageBox.Show("Error While Saving Project Configuration");
            }
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(SettingsManagement.FavFolder) == false)
            {
                openFileDialog1.InitialDirectory = SettingsManagement.FavFolder + Path.DirectorySeparatorChar;
            }

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                if (project.Open(openFileDialog1.FileName))
                {
                    SettingsManagement.AddFileAsMostRecent(openFileDialog1.FileName);
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Error, Could Not Load Project");
                }
            }
        }

        private void btnImportAPS_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "AVR Studio Project (*.aps)|*.aps";

            if (string.IsNullOrEmpty(SettingsManagement.FavFolder) == false)
            {
                ofd.InitialDirectory = SettingsManagement.FavFolder + Path.DirectorySeparatorChar;
            }

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                if (project.ImportAPS(ofd.FileName))
                {
                    MessageBox.Show("Import was Successful\r\nPlease Save It");
                    SaveResult res = project.Save();
                    if (res == SaveResult.Failed)
                    {
                        MessageBox.Show("Error During Save");
                    }
                    else if (res == SaveResult.Cancelled)
                    {
                        project.IsReady = false;
                    }
                    else
                    {
                        SettingsManagement.AddFileAsMostRecent(project.FilePath);
                        this.Close();
                    }
                }
                else
                {
                    MessageBox.Show("Error During Import");
                }
            }
        }

        private void listRecentFiles_DoubleClick(object sender, EventArgs e)
        {
            if (listRecentFiles.SelectedIndex >= 0)
            {
                string file = Program.CleanFilePath(SettingsManagement.FilePathFromListBoxIndex(listRecentFiles.SelectedIndex));
                if (project.Open(file))
                {
                    SettingsManagement.AddFileAsMostRecent(file);
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Error, Could Not Load Project");
                }
            }
        }

        private void frmWelcome_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (project.IsReady)
            {
                if (SettingsManagement.SaveRecentList() == false)
                {
                    MessageBox.Show("Error, Could Not Save Recent File List");
                }
            }
        }

        private void btnWizard_Click(object sender, EventArgs e)
        {
            Wizard wiz = new Wizard(project);
            if (wiz.ShowDialog() == DialogResult.OK)
            {
                SettingsManagement.AddFileAsMostRecent(project.FilePath);
                this.Close();
            }
        }
    }
}
