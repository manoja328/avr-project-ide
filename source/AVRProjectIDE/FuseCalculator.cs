using System;
using System.IO;
using System.Xml;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AVRProjectIDE
{
    public partial class FuseCalculator : Form
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
                ErrorReportWindow.Show(ex, "Error In Fuse Calculator");
                
            }
        }

        private static bool isOpen = false;

        private Dictionary<int, Dictionary<string, int>> maskList = new Dictionary<int, Dictionary<string, int>>();
        private AVRProject project;
        private AVRProject originalProject;
        private BurnerPanel burnerPanel;

        public BurnerPanel BurnerPanel
        {
            get { return burnerPanel; }
        }

        public AVRProject Project
        {
            get { return project; }
        }

        public FuseCalculator(AVRProject project)
        {
            InitializeComponent();

            this.originalProject = project;
            this.project = project.Clone();

            burnerPanel = new BurnerPanel(this.project);

            tabAVRDUDE.Controls.Add(burnerPanel);

            this.Text = "Fuse Calculator for " + project.Device.ToUpperInvariant();
            txtYourFusebox.Text = project.BurnFuseBox;
        }

        private void FuseCalculator_Load(object sender, EventArgs e)
        {
            // avoid having two fuse calculators open since this tool can be
            // opened from two places
            if (isOpen)
            {
                this.Close();
                return;
            }

            isOpen = true;

            burnerPanel.ProjToForm();

            XmlDocument xDoc = new XmlDocument();

            try
            {
                string chipName = Program.ProperChipName(project.Device);
                string xmlFilePathA;
                string xmlFilePathB;
                int cnt = chipName.Length;
                do
                {
                    xmlFilePathA = SettingsManagement.AppInstallPath + "chip_xml" + Path.DirectorySeparatorChar + chipName.Substring(0, cnt) + ".xml";
                    xmlFilePathB = SettingsManagement.AppDataPath + "chip_xml" + Path.DirectorySeparatorChar + chipName.Substring(0, cnt) + ".xml";

                    if (cnt == 0)
                        throw new Exception("chip data xml (" + chipName + ".xml) was not found in " + SettingsManagement.AppInstallPath + "chip_xml or " + SettingsManagement.AppDataPath + "chip_xml");

                    if (File.Exists(xmlFilePathB) == false)
                    {
                        if (File.Exists(xmlFilePathA))
                        {
                            File.Copy(xmlFilePathA, xmlFilePathB, true);
                            break;
                        }
                        else
                        {
                            cnt--;
                        }
                    }
                    else
                    {
                        break;
                    }
                }
                while (true);

                xDoc.Load(xmlFilePathB);
            }
            catch (FileNotFoundException)
            {
                MessageBox.Show("Could not find matching XML file for your device");
                this.Close();
                return;
            }
            catch (XmlException ex)
            {
                MessageBox.Show("Error in Chip Description XML File: " + ex.Message);
                this.Close();
                return;
            }
            catch (Exception ex)
            {
                ErrorReportWindow.Show(ex, "Fuse Calculator Error");
                this.Close();
                return;
            }

            XmlElement docEle = xDoc.DocumentElement;

            foreach (XmlElement xFuseContainer in docEle.GetElementsByTagName("FUSE"))
            {
                foreach (XmlElement xList in xFuseContainer.GetElementsByTagName("LIST"))
                {
                    string t = xList.InnerText.Trim().Trim(new char[]{'[', ']', }).Trim();
                    string[] fuseList = t.Split(':');
                    foreach (string fuse in fuseList)
                    {
                        if (string.IsNullOrEmpty(fuse) == false)
                        {
                            foreach (XmlElement xFuse in xFuseContainer.GetElementsByTagName(fuse))
                            {
                                FusePanel fp = new FusePanel(xFuse, project.BurnFuseBox, this);
                                TabPage tp = new TabPage(fuse);
                                tp.Controls.Add(fp);
                                fp.Dock = DockStyle.Fill;
                                tabControl1.TabPages.Add(tp);
                            }
                        }
                    }

                    break;
                }

                break;
            }

            foreach (XmlElement xLockBitContainer in docEle.GetElementsByTagName("LOCKBIT"))
            {
                FusePanel fp = new FusePanel(xLockBitContainer, project.BurnFuseBox, this);
                TabPage tp = new TabPage("Lock Bits");
                tp.Controls.Add(fp);
                fp.Dock = DockStyle.Fill;
                tabControl1.TabPages.Add(tp);

                break;
            }
        }

        private void FuseCalculator_FormClosed(object sender, FormClosedEventArgs e)
        {
            isOpen = false;
        }

        private void btnSaveAndClose_Click(object sender, EventArgs e)
        {
            burnerPanel.FormToProj();
            project.BurnFuseBox = txtYourFusebox.Text.Trim();

            this.originalProject = this.project.CopyProperties(this.originalProject);
            this.Close();
        }

        private void btnDiscardAndClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
