using System;
using System.Resources;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Xml;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using System.Resources;

namespace AVRProjectIDE
{
    public partial class HardwareExplorer : DockContent
    {
        private string chipName;
        private Dictionary<string, Dictionary<string, IntVect>> intVectList = new Dictionary<string, Dictionary<string, IntVect>>();

        public HardwareExplorer()
        {
            InitializeComponent();
        }

        public void LoadDataForChip(string chipName)
        {
            this.chipName = chipName.Trim().ToLowerInvariant();

            treeIOModules.Nodes.Clear();
            treePins.Nodes.Clear();
            treeXML.Nodes.Clear();
            listInterrupts.Items.Clear();
            txtChipInfo.Text = "";
            txtInterruptInfo.Text = "";
            txtIOModuleInfo.Text = "";
            txtPinsInfo.Text = "";
            txtXMLInfo.Text = "";

            XmlDocument xDoc = new XmlDocument();
            try
            {
                //ResourceManager resxMan = new ResourceManager(SettingsManagement.AssemblyTitle + ".chip_xml", System.Reflection.Assembly.GetExecutingAssembly());

                if (Program.MakeSurePathExists(SettingsManagement.AppDataPath + "chip_xml"))
                {
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
            }
            catch (Exception ex)
            {
                txtChipInfo.Text = "Error while loading chip data: " + ex.Message;

                intVectList = LoadInterruptList(intVectList);
                GetInterruptsForChip(listInterrupts, chipName);

                return;
            }

            XmlElement docEle = xDoc.DocumentElement;

            LoadChipInfo(txtChipInfo, docEle);
            LoadAllXMLTree(treeXML, docEle);
            LoadPinTree(treePins, docEle);
            LoadIOModuleTree(treeIOModules, docEle);

            intVectList = LoadInterruptList(intVectList);
            GetInterruptsForChip(listInterrupts, chipName);
        }

        private void LoadIOModuleTree(TreeView treeIOModules, XmlElement docEle)
        {
            foreach (XmlElement xIO_MODULE in docEle.GetElementsByTagName("IO_MODULE"))
            {
                foreach (XmlElement xMODULE_LIST in xIO_MODULE.GetElementsByTagName("MODULE_LIST"))
                {
                    treeIOModules.Nodes.Clear();

                    string mlist = xMODULE_LIST.InnerText.Trim(new char[] { ' ', '\t', '\r', '\n', '[', ']', ':', });

                    string[] modules = mlist.Split(new char[] { ' ', '\t', '\r', '\n', '[', ']', ':', });

                    foreach (string module in modules)
                    {
                        if (string.IsNullOrEmpty(module))
                            continue;

                        TreeNode moduleNode = new TreeNode(module);
                        moduleNode.ToolTipText = module;

                        foreach (XmlElement xModule in xIO_MODULE.GetElementsByTagName(module))
                        {
                            foreach (XmlNode xText in xIO_MODULE.ChildNodes)
                            {
                                if (xText.Name == "TEXT")
                                    moduleNode.ToolTipText += "\r\n" + xText.InnerText;                                
                            }

                            foreach (XmlElement xLIST in xModule.GetElementsByTagName("LIST"))
                            {
                                string regList = xLIST.InnerText.Trim(new char[] { ' ', '\t', '\r', '\n', '[', ']', ':', });
                                string[] regs = regList.Split(new char[] { ' ', '\t', '\r', '\n', '[', ']', ':', });

                                foreach (string reg in regs)
                                {
                                    if (string.IsNullOrEmpty(reg))
                                        continue;

                                    TreeNode regNode = new TreeNode(reg);
                                    regNode.ToolTipText = reg;

                                    foreach (XmlElement xReg in xModule.GetElementsByTagName(reg))
                                    {
                                        bool displayBits = false;

                                        foreach (XmlNode xText in xReg.ChildNodes)
                                        {
                                            if (xText.Name == "TEXT" || xText.Name == "DESCRIPTION")
                                                regNode.ToolTipText += "\r\n" + xText.InnerText + "\r\n";
                                            else if (xText.Name == "IO_ADDR")
                                                regNode.ToolTipText += "\r\nIO Address:" + xText.InnerText;
                                            else if (xText.Name == "MEM_ADDR")
                                                regNode.ToolTipText += "\r\nMemory Address:" + xText.InnerText;
                                            else if (xText.Name == "DISPLAY_BITS")
                                                displayBits = xText.InnerText.Trim().ToLowerInvariant() == "y";
                                        }

                                        //if (displayBits == false)
                                        //    continue;

                                        for (int i = 0; i < 32; i++)
                                            foreach (XmlElement xBit in xReg.GetElementsByTagName("BIT" + i.ToString("0")))
                                            {
                                                TreeNode bitNode = new TreeNode(xBit.Name);
                                                bitNode.ToolTipText = "Bit Index " + i.ToString("0");

                                                foreach (XmlElement j in xBit.GetElementsByTagName("NAME"))
                                                {
                                                    bitNode.Text = j.InnerText;
                                                    bitNode.ToolTipText += "\r\n" + j.InnerText + "\r\n";
                                                }

                                                foreach (XmlElement j in xBit.GetElementsByTagName("DESCRIPTION"))
                                                {
                                                    bitNode.ToolTipText += "\r\nDescription: " + j.InnerText + "\r\n";
                                                }

                                                foreach (XmlElement j in xBit.GetElementsByTagName("TEXT"))
                                                {
                                                    bitNode.ToolTipText += "\r\n" + j.InnerText + "\r\n";
                                                }

                                                foreach (XmlElement j in xBit.GetElementsByTagName("ACCESS"))
                                                {
                                                    bool read = j.InnerText.Trim().ToLowerInvariant().Contains('r');
                                                    bool write = j.InnerText.Trim().ToLowerInvariant().Contains('w');

                                                    if (read && !write)
                                                        bitNode.ToolTipText += "\r\nRead Only";
                                                    else if (read && write)
                                                        bitNode.ToolTipText += "\r\nReadable and Writable";
                                                    else if (!read && write)
                                                        bitNode.ToolTipText += "\r\nWrite Only";
                                                    else if (!read && !write)
                                                        bitNode.ToolTipText += "\r\nUnreadable and Unwritable";
                                                }

                                                foreach (XmlElement j in xBit.GetElementsByTagName("INIT_VAL"))
                                                {
                                                    bitNode.ToolTipText += "\r\nInitial Value: " + j.InnerText;
                                                }

                                                regNode.Nodes.Add(bitNode);
                                            }
                                    }

                                    moduleNode.Nodes.Add(regNode);
                                }

                                break;
                            }

                            break;
                        }

                        treeIOModules.Nodes.Add(moduleNode);
                    }

                    break;
                }
                break;
            }
        }

        private void GetInterruptsForChip(ListBox listInterrupts, string chipName)
        {
            if (intVectList.ContainsKey(chipName) == false)
                return;

            listInterrupts.Items.Clear();

            foreach (IntVect v in intVectList[chipName].Values)
            {
                listInterrupts.Items.Add(v.Description);
            }
        }

        private Dictionary<string, Dictionary<string, IntVect>> LoadInterruptList(Dictionary<string, Dictionary<string, IntVect>> intVectList)
        {
            XmlDocument xDoc = new XmlDocument();

            try
            {
                if (Program.MakeSurePathExists(SettingsManagement.AppDataPath + "chip_xml"))
                {
                    string xmlFilePath = SettingsManagement.AppDataPath + "chip_xml\\interruptvectors.xml";

                    if (File.Exists(xmlFilePath) == false)
                        File.WriteAllText(xmlFilePath, Properties.Resources.interruptvectors);

                    xDoc.Load(xmlFilePath);
                }
                else
                {
                    xDoc.LoadXml(Properties.Resources.interruptvectors);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error while loading interrupt vector data: " + ex.Message);
                return intVectList;
            }

            foreach (XmlElement i in xDoc.DocumentElement.GetElementsByTagName("Chip"))
            {
                string chip = i.GetAttribute("Name").Trim().ToLowerInvariant();

                if (string.IsNullOrEmpty(chip))
                    continue;

                if (intVectList.ContainsKey(chip) == false)
                    intVectList.Add(chip, new Dictionary<string, IntVect>());

                foreach (XmlElement j in i.GetElementsByTagName("HasVector"))
                {
                    string newName = j.GetAttribute("NewName").Trim();
                    string oldName = j.GetAttribute("OldName").Trim();
                    string desc = j.InnerText.Trim();

                    if (string.IsNullOrEmpty(newName) || string.IsNullOrEmpty(desc))
                        continue;

                    IntVect v = new IntVect(newName, oldName, desc);

                    if (intVectList[chip].ContainsKey(v.Description) == false)
                        intVectList[chip].Add(v.Description, v);
                }
            }

            return intVectList;
        }

        private void LoadAllXMLTree(TreeView tree, XmlElement docEle)
        {
            tree.Nodes.Clear();

            string nodeText = docEle.Name;

            foreach (XmlAttribute attrib in docEle.Attributes)
            {
                nodeText += String.Format(" {0}=\"{1}\"", attrib.Name, attrib.Value);
            }
            TreeNode node = new TreeNode(nodeText);

            foreach (XmlNode xEle in docEle.ChildNodes)
            {
                LoadXMLToTree(node, xEle);
            }

            tree.Nodes.Add(node);
        }

        private void LoadChipInfo(TextBox textbox, XmlElement docEle)
        {
            string result = chipName.ToUpperInvariant() + "\r\n";

            foreach (XmlElement i in docEle.GetElementsByTagName("MEMORY"))
            {
                foreach (XmlElement j in i.GetElementsByTagName("PROG_FLASH"))
                {
                    result += String.Format("Flash Memory: {0} bytes\r\n", j.InnerText);
                }

                foreach (XmlElement j in i.GetElementsByTagName("EEPROM"))
                {
                    result += String.Format("EEPROM Memory: {0} bytes\r\n", j.InnerText);
                }

                foreach (XmlElement j in i.GetElementsByTagName("INT_SRAM"))
                {
                    foreach (XmlElement k in j.GetElementsByTagName("SIZE"))
                    {
                        result += String.Format("SRAM Size: {0} bytes\r\n", k.InnerText);
                    }

                    foreach (XmlElement k in j.GetElementsByTagName("START_ADDR"))
                    {
                        result += String.Format("SRAM Starting Address: {0}\r\n", k.InnerText.Replace("$", "0x"));
                    }
                }
            }

            textbox.Text = result;
        }

        private void LoadXMLToTree(TreeNode node, XmlNode xEle)
        {
            string nodeText = "";

            if (xEle.Name == "#text")
            {
                nodeText = xEle.InnerText;
            }
            else
            {
                nodeText = xEle.Name;
            }

            if (xEle.Attributes != null)
            {
                foreach (XmlAttribute attrib in xEle.Attributes)
                {
                    nodeText += String.Format(" {0}=\"{1}\"", attrib.Name, attrib.Value);
                }
            }

            TreeNode newNode = new TreeNode(nodeText);
            newNode.ToolTipText = xEle.InnerText;

            if (xEle.ChildNodes != null)
            {
                foreach (XmlNode xCEle in xEle.ChildNodes)
                {
                    LoadXMLToTree(newNode, xCEle);
                }
            }

            node.Nodes.Add(newNode);
        }

        private void LoadPinTree(TreeView tree, XmlElement docEle)
        {

            foreach (XmlElement i in docEle.GetElementsByTagName("PACKAGE"))
            {
                tree.Nodes.Clear();

                foreach (XmlElement j in i.GetElementsByTagName("PACKAGES"))
                {
                    string txt = j.InnerText.Trim(new char[] { '[', ']', });
                    string[] packages = txt.Split(':');

                    foreach (string pkg_ in packages)
                    {
                        string pkg = pkg_.Trim();

                        if (string.IsNullOrEmpty(pkg))
                            continue;

                        foreach (XmlElement k in i.GetElementsByTagName(pkg))
                        {
                            string nodeText = pkg;

                            if (k.Attributes != null)
                            {
                                foreach (XmlAttribute attrib in k.Attributes)
                                {
                                    nodeText += String.Format(" {0}=\"{1}\"", attrib.Name, attrib.Value);
                                }
                            }

                            TreeNode pkgNode = new TreeNode(nodeText);
                            pkgNode.ToolTipText = "";

                            foreach (XmlElement nmb_pin in k.GetElementsByTagName("NMB_PIN"))
                            {
                                int cnt = 0;
                                if (int.TryParse(nmb_pin.InnerText, out cnt))
                                {
                                    for (int pinNum = 0; pinNum <= cnt; pinNum++)
                                    {
                                        foreach (XmlElement pin in k.GetElementsByTagName("PIN" + pinNum.ToString("0")))
                                        {
                                            TreeNode pinNode = new TreeNode("Pin " + pinNum.ToString("0"));
                                            pinNode.ToolTipText = "";

                                            foreach (XmlElement pinFunct in pin.GetElementsByTagName("NAME"))
                                            {
                                                pinNode.Text += " " + pinFunct.InnerText;
                                            }

                                            foreach (XmlElement pinDesc in pin.GetElementsByTagName("TEXT"))
                                            {
                                                pinNode.ToolTipText += pinDesc.InnerText + " ";
                                            }

                                            pkgNode.Nodes.Add(pinNode);
                                        }
                                    }

                                    break;
                                }
                            }

                            tree.Nodes.Add(pkgNode);
                        }
                    }

                    break;
                }
                break;
            }
        }

        private void tree_AfterLabelEditCancel(object sender, NodeLabelEditEventArgs e)
        {
            e.CancelEdit = true;
        }

        private void treePins_AfterSelect(object sender, TreeViewEventArgs e)
        {
            txtPinsInfo.Text = e.Node.ToolTipText;
        }

        private void listInterrupts_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listInterrupts.SelectedIndex < 0)
                return;

            string desc = (string)listInterrupts.Items[listInterrupts.SelectedIndex];

            if (intVectList.ContainsKey(chipName) == false)
                return;

            if (intVectList[chipName].ContainsKey(desc) == false)
                return;

            txtInterruptInfo.Text = "New Vector Name: " + intVectList[chipName][desc].NewName;
            if (string.IsNullOrEmpty(intVectList[chipName][desc].OldName) == false)
                txtInterruptInfo.Text += "\r\n" + "Old Vector Name: " + intVectList[chipName][desc].OldName;
        }

        private void treeXML_AfterSelect(object sender, TreeViewEventArgs e)
        {
            txtXMLInfo.Text = e.Node.ToolTipText;
        }

        private void treeIOModules_AfterSelect(object sender, TreeViewEventArgs e)
        {
            txtIOModuleInfo.Text = e.Node.ToolTipText;
        }
    }
}
