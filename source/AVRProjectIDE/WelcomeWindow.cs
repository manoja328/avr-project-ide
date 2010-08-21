using System;
using System.Net;
using System.Web;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace AVRProjectIDE
{
    public partial class WelcomeWindow : Form
    {
        //[System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.Demand, Name = "FullTrust")]
        protected override void WndProc(ref Message m)
        {
            try
            {
                base.WndProc(ref m);
            }
            catch (Exception ex)
            {
                ErrorReportWindow.Show(ex, "Error In Welcome Window");
                
            }
        }

        private AVRProject project;

        public WelcomeWindow(AVRProject project)
        {
            InitializeComponent();

            SettingsManagement.FillListBox(listRecentFiles);

            this.project = project;

            this.Icon = GraphicsResx.mainicon;

            chkShowWelcomeAtStart.Checked = SettingsManagement.WelcomeWindowAtStart;

            backgroundWorker1.RunWorkerAsync();
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
            else
            {
                btnFind_Click(sender, e);
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
                    MessageBox.Show("Import was Successful" + Environment.NewLine + "Please Save It");
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

            SettingsManagement.WelcomeWindowAtStart = chkShowWelcomeAtStart.Checked;
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

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                WebRequest wReq = WebRequest.Create("http://code.google.com/p/avr-project-ide/wiki/NewsFeedPage");
                wReq.Credentials = CredentialCache.DefaultCredentials;
                HttpWebResponse wResp = (HttpWebResponse)wReq.GetResponse();
                Stream wStream = wResp.GetResponseStream();
                StreamReader reader = new StreamReader(wStream);
                string content = reader.ReadToEnd();
                
                Regex r = new Regex(
                    "(<h3>)" + 
                    "(<a name=\".*?\"\\/>)" +
                    "(.*?)" + 
                    "(<\\/a>)" +
                    "(.*?)" + 
                    "(<\\/h3>)",
                    RegexOptions.Multiline | RegexOptions.IgnoreCase);

                e.Result = "";

                Match m = r.Match(content);
                while (m.Success)
                {
                    string txt = m.Groups[3].Value;
                    if (txt.Contains('<'))
                        txt = txt.Substring(0, txt.IndexOf('<'));

                    e.Result += System.Web.HttpUtility.HtmlDecode(txt) + Environment.NewLine + Environment.NewLine;
                    m = m.NextMatch();
                }

                reader.Close();
                wStream.Close();
                wResp.Close();
            }
            catch (Exception ex)
            {
                e.Result = "Error retrieving news, " + ex.Message;
            }
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Result != null)
                if (string.IsNullOrEmpty((string)e.Result) == false)
                    txtNews.Text = (string)e.Result;
        }

        private void btnDonate_Click(object sender, EventArgs e)
        {
            Program.LaunchDonate();
        }
    }
}
