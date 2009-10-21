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
        private Dictionary<int, Dictionary<string, int>> maskList = new Dictionary<int, Dictionary<string, int>>();
        private AVRProject project;

        public FuseCalculator(AVRProject project)
        {
            InitializeComponent();

            this.project = project;

            txtSuggestedFusebox.Text = project.BurnFuseBox;
            txtYourFusebox.Text = project.BurnFuseBox;
        }

        private void FuseCalculator_Load(object sender, EventArgs e)
        {
            XmlDocument xDoc = new XmlDocument();

            try
            {
                string chipName = project.Device;
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
            }
            catch (Exception ex)
            {
                ErrorReportWindow erw = new ErrorReportWindow(ex, "Fuse Calculator Error");
                erw.Show();
                this.Close();
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
    }
}
