using System;
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

namespace AVRProjectIDE
{
    public partial class HardwareExplorer : DockContent
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
                ErrorReportWindow.Show(ex, "Error In Hardware Explorer");
                
            }
        }

        private string chipName;
        private Dictionary<string, Dictionary<string, IntVect>> intVectListAVRLibc = new Dictionary<string, Dictionary<string, IntVect>>();
        private Dictionary<string, string> intVectListAtmelXML = new Dictionary<string, string>();

        public decimal ClockFreq
        {
            set { numClkFreq.Value = Math.Max(Math.Min(numClkFreq.Maximum, value), numClkFreq.Minimum); }
        }

        public HardwareExplorer()
        {
            InitializeComponent();

            this.Icon = GraphicsResx.chip;

            numTimerOverflows.Maximum = decimal.MaxValue;
            numTimerRealTime.Maximum = decimal.MaxValue;
            numTimerTotalTicks.Maximum = decimal.MaxValue;
            numTimerRemainingTicks.Maximum = decimal.MaxValue;
            numADCRef.Maximum = decimal.MaxValue;
            numADCInput.Maximum = decimal.MaxValue;
            numADC8BitRes.Maximum = decimal.MaxValue;
            numADC10BitRes.Maximum = decimal.MaxValue;

            listTimerModes.SelectedIndex = 0;
            listTimerPrescaler.SelectedIndex = 0;
        }

        public void LoadDataForChip(string chipName)
        {
            this.chipName = Program.ProperChipName(chipName);

            treeIOModules.Nodes.Clear();
            treePins.Nodes.Clear();
            treeXML.Nodes.Clear();
            listInterruptsAVRLibc.Items.Clear();
            txtChipInfo.Text = "";
            txtInterruptInfoAVRLibc.Text = "";
            txtIOModuleInfo.Text = "";
            txtPinsInfo.Text = "";
            txtXMLInfo.Text = "";

            XmlDocument xDoc = new XmlDocument();
            try
            {
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
                        {
                            txtChipInfo.Text = "Error: chip data xml (" + chipName + ".xml) was not found in " + SettingsManagement.AppInstallPath + "chip_xml or " + SettingsManagement.AppDataPath + "chip_xml";

                            intVectListAVRLibc = LoadInterruptList(intVectListAVRLibc);
                            GetInterruptsForChip(chipName);

                            return;
                        }

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
            catch (XmlException ex)
            {
                txtChipInfo.Text = "Error in chip description file: " + ex.Message;
                return;
            }
            catch (Exception ex)
            {
                txtChipInfo.Text = "Error while loading chip data: " + ex.Message;

                intVectListAVRLibc = LoadInterruptList(intVectListAVRLibc);
                GetInterruptsForChip(chipName);

                return;
            }

            try
            {
                XmlElement docEle = xDoc.DocumentElement;

                LoadChipInfo(txtChipInfo, docEle);
                LoadAllXMLTree(treeXML, docEle);
                LoadPinTree(treePins, docEle);
                LoadIOModuleTree(treeIOModules, docEle);

                intVectListAVRLibc = LoadInterruptList(intVectListAVRLibc);
                intVectListAtmelXML = LoadInterruptListFromAtmelXML(docEle, intVectListAtmelXML);

                GetInterruptsForChip(chipName);
            }
            catch (Exception ex)
            {
                txtChipInfo.Text += Environment.NewLine + Environment.NewLine + "Error while loading chip data: " + ex.Message;
            }
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
                                    moduleNode.ToolTipText += Environment.NewLine + xText.InnerText;                                
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
                                                regNode.ToolTipText += Environment.NewLine + xText.InnerText + Environment.NewLine;
                                            else if (xText.Name == "IO_ADDR")
                                            {
                                                long num = 0;
                                                if (Program.TryParseText(xText.InnerText, out num, false))
                                                    regNode.ToolTipText += String.Format(Environment.NewLine + "IO Address: 0x{0:X4} ( {0:0} )", num);
                                                else if (string.IsNullOrEmpty(xText.InnerText.Trim()) == false)
                                                    regNode.ToolTipText += Environment.NewLine + "IO Address: " + xText.InnerText.Trim();
                                            }
                                            else if (xText.Name == "MEM_ADDR")
                                            {
                                                long num = 0;
                                                if (Program.TryParseText(xText.InnerText, out num, false))
                                                    regNode.ToolTipText += String.Format(Environment.NewLine + "Memory Address: 0x{0:X4} ( {0:0} )", num);
                                                else if (string.IsNullOrEmpty(xText.InnerText.Trim()) == false)
                                                    regNode.ToolTipText += Environment.NewLine + "Memory Address: " + xText.InnerText.Trim();
                                            }
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
                                                    bitNode.ToolTipText += Environment.NewLine + j.InnerText + Environment.NewLine;
                                                }

                                                foreach (XmlElement j in xBit.GetElementsByTagName("DESCRIPTION"))
                                                {
                                                    bitNode.ToolTipText += Environment.NewLine + "Description: " + j.InnerText + Environment.NewLine;
                                                }

                                                foreach (XmlElement j in xBit.GetElementsByTagName("TEXT"))
                                                {
                                                    bitNode.ToolTipText += Environment.NewLine + j.InnerText + Environment.NewLine;
                                                }

                                                foreach (XmlElement j in xBit.GetElementsByTagName("ACCESS"))
                                                {
                                                    bool read = j.InnerText.Trim().ToLowerInvariant().Contains('r');
                                                    bool write = j.InnerText.Trim().ToLowerInvariant().Contains('w');

                                                    if (read && !write)
                                                        bitNode.ToolTipText += Environment.NewLine + "Read Only";
                                                    else if (read && write)
                                                        bitNode.ToolTipText += Environment.NewLine + "Readable and Writable";
                                                    else if (!read && write)
                                                        bitNode.ToolTipText += Environment.NewLine + "Write Only";
                                                    else if (!read && !write)
                                                        bitNode.ToolTipText += Environment.NewLine + "Unreadable and Unwritable";
                                                }

                                                foreach (XmlElement j in xBit.GetElementsByTagName("INIT_VAL"))
                                                {
                                                    bitNode.ToolTipText += Environment.NewLine + "Initial Value: " + j.InnerText;
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

        private void GetInterruptsForChip(string chipName)
        {
            listInterruptsAtmelXML.Items.Clear();

            foreach (KeyValuePair<string, string> i in intVectListAtmelXML)
            {
                listInterruptsAtmelXML.Items.Add(i.Key);
            }

            if (intVectListAVRLibc.ContainsKey(chipName) == false)
                return;

            listInterruptsAVRLibc.Items.Clear();

            foreach (IntVect v in intVectListAVRLibc[chipName].Values)
            {
                listInterruptsAVRLibc.Items.Add(v.Description);
            }
        }

        private Dictionary<string, string> LoadInterruptListFromAtmelXML(XmlElement docEle, Dictionary<string, string> intVectList)
        {
            try
            {
                foreach (XmlElement intContainer in docEle.GetElementsByTagName("INTERRUPT_VECTOR"))
                {
                    intVectListAtmelXML.Clear();

                    for (int i = 0; i < 256; i++)
                    {
                        foreach (XmlElement vectEle in intContainer.GetElementsByTagName("VECTOR" + i.ToString("0")))
                        {
                            string name = "";
                            string desc = "";

                            foreach (XmlElement xEle in vectEle.GetElementsByTagName("DEFINITION"))
                            {
                                name = xEle.InnerText;
                                desc += xEle.InnerText + Environment.NewLine;
                            }

                            foreach (XmlElement xEle in vectEle.GetElementsByTagName("SOURCE"))
                            {
                                desc += "Source: " + xEle.InnerText + Environment.NewLine;
                            }

                            long progAddr = -1;
                            foreach (XmlElement xProgAddr in vectEle.GetElementsByTagName("PROGRAM_ADDRESS"))
                            {
                                if (Program.TryParseText(xProgAddr.InnerText, out progAddr, false))
                                {
                                    desc += String.Format("Addr: 0x{0:X4}" + Environment.NewLine, progAddr);
                                }
                            }

                            if (string.IsNullOrEmpty(name) == false)
                            {
                                while (intVectListAtmelXML.ContainsKey(name))
                                    name += "_";

                                intVectListAtmelXML.Add(name, desc);
                            }
                        }
                    }
                }
            }
            catch (XmlException ex)
            {
                MessageBox.Show("XML error while loading interrupt vector data from Atmel's chip description file: " + ex.Message);
                return intVectList;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error while loading interrupt vector data from Atmel's chip description file: " + ex.Message);
                return intVectList;
            }

            return intVectList;
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
            catch (XmlException ex)
            {
                MessageBox.Show("XML error while loading interrupt vector data from interruptvectors.xml : " + ex.Message);
                return intVectList;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error while loading interrupt vector data from interruptvectors.xml : " + ex.Message);
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
            string result = chipName.ToUpperInvariant();

            foreach (XmlElement i in docEle.GetElementsByTagName("MEMORY"))
            {
                result += Environment.NewLine;

                foreach (XmlElement j in i.GetElementsByTagName("PROG_FLASH"))
                {
                    long num = 0;
                    if (Program.TryParseText(j.InnerText, out num, false))
                        result += String.Format("Flash Memory: {0} bytes ( 0x{0:X4} )" + Environment.NewLine, num);
                    else if (string.IsNullOrEmpty(j.InnerText.Trim()) == false)
                        result += String.Format("Flash Memory: {0}" + Environment.NewLine, j.InnerText.Trim());
                }

                foreach (XmlElement j in i.GetElementsByTagName("EEPROM"))
                {
                    long num = 0;
                    if (Program.TryParseText(j.InnerText, out num, false))
                        result += String.Format("EEPROM Memory: {0} bytes ( 0x{0:X4} )" + Environment.NewLine, num);
                    else if (string.IsNullOrEmpty(j.InnerText.Trim()) == false)
                        result += String.Format("EEPROM Memory: {0}" + Environment.NewLine, j.InnerText.Trim());
                }

                foreach (XmlElement j in i.GetElementsByTagName("INT_SRAM"))
                {
                    result += Environment.NewLine;

                    foreach (XmlElement k in j.GetElementsByTagName("SIZE"))
                    {
                        long num = 0;
                        if (Program.TryParseText(k.InnerText, out num, false))
                            result += String.Format("SRAM Size: {0} bytes ( 0x{0:X4} )" + Environment.NewLine, num);
                        else if (string.IsNullOrEmpty(k.InnerText.Trim()) == false)
                            result += String.Format("SRAM Size: {0}" + Environment.NewLine, k.InnerText.Trim());
                    }

                    foreach (XmlElement k in j.GetElementsByTagName("START_ADDR"))
                    {
                        long num = 0;
                        if (Program.TryParseText(k.InnerText, out num, false))
                            result += String.Format("SRAM Starting Address: {0} ( 0x{0:X4} )" + Environment.NewLine, num);
                    }
                }

                foreach (XmlElement j in i.GetElementsByTagName("BOOT_CONFIG"))
                {
                    result += Environment.NewLine + "Remember that all flash addresses are WORD addresses, not BYTE addresses." + Environment.NewLine + Environment.NewLine;

                    string startAddrStr = null;
                    string endAddrStr = null;
                    foreach (XmlElement k in j.GetElementsByTagName("NRWW_START_ADDR"))
                        startAddrStr = k.InnerText;
                    foreach (XmlElement k in j.GetElementsByTagName("NRWW_STOP_ADDR"))
                        endAddrStr = k.InnerText;

                    long startAddr = 0;
                    long endAddr = 0;

                    if (Program.TryParseText(startAddrStr, out startAddr, false) && Program.TryParseText(endAddrStr, out endAddr, false))
                        result += String.Format("NRWW Addr: 0x{0:X4} ( {0:0} ) to 0x{1:X4} ( {1:0} )" + Environment.NewLine, startAddr, endAddr);

                    foreach (XmlElement k in j.GetElementsByTagName("RWW_START_ADDR"))
                        startAddrStr = k.InnerText;
                    foreach (XmlElement k in j.GetElementsByTagName("RWW_STOP_ADDR"))
                        endAddrStr = k.InnerText;

                    if (Program.TryParseText(startAddrStr, out startAddr, false) && Program.TryParseText(endAddrStr, out endAddr, false))
                        result += String.Format("RWW Addr: 0x{0:X4} ( {0:0} ) to 0x{1:X4} ( {1:0} )" + Environment.NewLine, startAddr, endAddr);


                    foreach (XmlElement k in j.GetElementsByTagName("PAGESIZE"))
                    {
                        long num = 0;
                        if (Program.TryParseText(k.InnerText, out num, false))
                            result += String.Format("Page Size: {0} ( 0x{0:X4} )" + Environment.NewLine, num);
                        else if (string.IsNullOrEmpty(k.InnerText.Trim()) == false)
                            result += String.Format("Page Size: {0}" + Environment.NewLine, k.InnerText.Trim());
                    }

                    for (int k = 0; k < 16; k++)
                    {
                        foreach (XmlElement l in j.GetElementsByTagName("BOOTSZMODE" + k.ToString("0")))
                        {
                            result += Environment.NewLine;

                            long appStart = -1;
                            long bootStart = -1;
                            long bootReset = -1;

                            foreach (XmlElement m in l.GetElementsByTagName("BOOTSIZE"))
                            {
                                long num = 0;
                                if (Program.TryParseText(m.InnerText, out num, false))
                                    result += String.Format("Boot Mode {0} Boot Size: {1} words ( 0x{1:X4} )" + Environment.NewLine, k, num);
                                else if (string.IsNullOrEmpty(m.InnerText.Trim()) == false)
                                    result += String.Format("Boot Mode {0} Boot Size: {1}" + Environment.NewLine, k, m.InnerText.Trim());
                            }

                            foreach (XmlElement m in l.GetElementsByTagName("PAGES"))
                            {
                                long num = 0;
                                if (Program.TryParseText(m.InnerText, out num, false))
                                    result += String.Format("Boot Mode {0} Pages: {1}" + Environment.NewLine, k, num);
                                else if (string.IsNullOrEmpty(m.InnerText.Trim()) == false)
                                    result += String.Format("Boot Mode {0} Pages: {1}" + Environment.NewLine, k, m.InnerText.Trim());
                            }

                            foreach (XmlElement m in l.GetElementsByTagName("APPSTART"))
                            {
                                long num = 0;
                                if (Program.TryParseText(m.InnerText, out num, false))
                                {
                                    appStart = num;
                                    result += String.Format("Boot Mode {0} App Start Addr: 0x{1:X4} ( {1:0} )" + Environment.NewLine, k, num);
                                }
                                else if (string.IsNullOrEmpty(m.InnerText.Trim()) == false)
                                    result += String.Format("Boot Mode {0} App Start Addr: {1}" + Environment.NewLine, k, m.InnerText.Trim());
                            }

                            foreach (XmlElement m in l.GetElementsByTagName("BOOTSTART"))
                            {
                                long num = 0;
                                if (Program.TryParseText(m.InnerText, out num, false))
                                {
                                    bootStart = num;
                                }
                            }

                            foreach (XmlElement m in l.GetElementsByTagName("BOOTRESET"))
                            {
                                long num = 0;
                                if (Program.TryParseText(m.InnerText, out num, false))
                                {
                                    bootReset = num;
                                }
                            }

                            if (bootStart == bootReset && bootStart >= 0)
                                result += String.Format("Boot Mode {0} Boot Start & Reset Addr: 0x{1:X4} ( {1:0} )" + Environment.NewLine, k, bootStart);
                            else
                            {
                                if (bootStart >= 0)
                                    result += String.Format("Boot Mode {0} Boot Start Addr: 0x{1:X4} ( {1:0} )" + Environment.NewLine, k, bootStart);

                                if (bootReset >= 0)
                                    result += String.Format("Boot Mode {0} Boot Reset Addr: 0x{1:X4} ( {1:0} )" + Environment.NewLine, k, bootReset);
                            }

                            if (bootReset < bootStart && bootReset >= 0)
                                bootStart = bootReset;

                            if (appStart >= 0 && bootStart >= appStart)
                            {
                                result += String.Format("Boot Mode {0} App Size: {1} bytes ( 0x{1:X4} )" + Environment.NewLine, k, (bootStart - appStart) * 2);
                            }
                        }
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
                    //string txt = j.InnerText.Trim().Trim(new char[] { '[', ']', });
                    //string[] packages = txt.Split(':');
                    string[] packages = new string[] { "PDIP", "DIP", "MLF", "QFN", "SOIC", "TQFP", "VQFN", "BGA", "CBGA", };

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

        private void listInterruptsAVRLibc_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listInterruptsAVRLibc.SelectedIndex < 0)
                return;
            string chipName = this.chipName.ToLowerInvariant();
            string desc = (string)listInterruptsAVRLibc.Items[listInterruptsAVRLibc.SelectedIndex];

            if (intVectListAVRLibc.ContainsKey(chipName) == false)
                return;

            if (intVectListAVRLibc[chipName].ContainsKey(desc) == false)
                return;

            txtInterruptInfoAVRLibc.Text = "New Vector Name: " + intVectListAVRLibc[chipName][desc].NewName;
            if (string.IsNullOrEmpty(intVectListAVRLibc[chipName][desc].OldName) == false)
                txtInterruptInfoAVRLibc.Text += Environment.NewLine + "Old Vector Name: " + intVectListAVRLibc[chipName][desc].OldName;
        }

        private void listInterruptsAtmelXML_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listInterruptsAtmelXML.SelectedIndex < 0)
                return;

            string desc = (string)listInterruptsAtmelXML.Items[listInterruptsAtmelXML.SelectedIndex];

            if (intVectListAtmelXML.ContainsKey(desc) == false)
                return;

            txtInterruptInfoAtmelXML.Text = intVectListAtmelXML[desc];
        }

        private void treeXML_AfterSelect(object sender, TreeViewEventArgs e)
        {
            txtXMLInfo.Text = e.Node.ToolTipText;
        }

        private void treeIOModules_AfterSelect(object sender, TreeViewEventArgs e)
        {
            txtIOModuleInfo.Text = e.Node.ToolTipText;
        }

        private decimal GetTimerMultiplier()
        {
            string bitStr = (string)listTimerModes.Items[listTimerModes.SelectedIndex];
            string[] bitStrParts = bitStr.Split(new char[] { ' ', '\0', '\t', '\r', '\n', });
            int bits = int.Parse(bitStrParts[0]);
            return Convert.ToDecimal(Math.Pow(2, bits));
        }

        private decimal GetTimerPrescaler()
        {
            string str = (string)listTimerPrescaler.Items[listTimerPrescaler.SelectedIndex];
            string[] parts = str.Split(new char[] { '/', '\\', ' ', '\0', '\t', '\r', '\n', });
            return decimal.Parse(parts[parts.Length - 1]);
        }

        private decimal GetTimerActualClock()
        {
            return numClkFreq.Value / GetTimerPrescaler();
        }

        private void btnTimerUseOverflowRemainder_Click(object sender, EventArgs e)
        {
            try
            {
                numTimerTotalTicks.Value = numTimerOverflows.Value * GetTimerMultiplier() + numTimerRemainingTicks.Value;

                numTimerRealTime.Value = (numTimerTotalTicks.Value / GetTimerActualClock());
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error during calculation: " + ex.Message);
            }
        }

        private void btnTimerUseTotalTicks_Click(object sender, EventArgs e)
        {
            try
            {
                decimal mult = GetTimerMultiplier();

                numTimerRealTime.Value = (numTimerTotalTicks.Value / GetTimerActualClock());

                ulong ovf = Convert.ToUInt64(Math.Floor(numTimerTotalTicks.Value / mult));
                numTimerOverflows.Value = Convert.ToDecimal(ovf);
                numTimerRemainingTicks.Value = numTimerTotalTicks.Value - numTimerOverflows.Value * mult;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error during calculation: " + ex.Message);
            }
        }

        private void btnTimerUseRealTime_Click(object sender, EventArgs e)
        {
            try
            {
                decimal mult = GetTimerMultiplier();

                numTimerTotalTicks.Value = numTimerRealTime.Value * GetTimerActualClock();

                ulong ovf = Convert.ToUInt64(Math.Floor(numTimerTotalTicks.Value / mult));
                numTimerOverflows.Value = Convert.ToDecimal(ovf);
                numTimerRemainingTicks.Value = numTimerTotalTicks.Value - numTimerOverflows.Value * mult;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error during calculation: " + ex.Message);
            }
        }

        private void listBaud_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (listBaud.SelectedIndex < 0)
                    return;

                decimal baud = decimal.Parse((string)(listBaud.Items[listBaud.SelectedIndex]));
                decimal multi = 16;
                if (chkBaudDoubleSpeed.Checked)
                    multi = 8;

                if (chkBaudSPIMode.Checked)
                {
                    txtBaudResults.Text = "Synchronous Mode Results:" + Environment.NewLine;
                    multi = 2;
                }
                else
                    txtBaudResults.Text = "Asynchronous Mode Results:" + Environment.NewLine;

                decimal ubrrRaw = (numClkFreq.Value / (multi * baud)) - 1;
                int ubrrH = Convert.ToInt32(Math.Floor(Convert.ToDouble(ubrrRaw) / Math.Pow(2, 8)));
                decimal ubrrL = ubrrRaw - Convert.ToDecimal((ubrrH * Convert.ToInt32(Math.Pow(2, 8))));

                decimal newBaud = numClkFreq.Value / (multi * (Math.Round(ubrrRaw) + 1));

                decimal err = (Math.Abs(baud / newBaud) - 1) * 100;

                txtBaudResults.Text += string.Format(
                    "UBRR = {0:0}" + Environment.NewLine +
                    "UBRR Higher 8 bits = {1:0}" + Environment.NewLine +
                    "UBRR Lower 8 bits = {2:0}" + Environment.NewLine +
                    "Actual Baud Rate = {3:0.00}" + Environment.NewLine +
                    "% Error = {4:0.00}" + Environment.NewLine,
                    Math.Round(ubrrRaw),
                    ubrrH,
                    Math.Round(ubrrL),
                    newBaud,
                    Math.Abs(err)
                    );
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error during calculation: " + ex.Message);
            }
        }

        private void chkBaudDoubleSpeed_CheckedChanged(object sender, EventArgs e)
        {
            listBaud_SelectedIndexChanged(sender, e);
        }

        private void listBaud_Click(object sender, EventArgs e)
        {
            listBaud_SelectedIndexChanged(sender, e);
        }

        private void numClkFreq_ValueChanged(object sender, EventArgs e)
        {
            listBaud_SelectedIndexChanged(sender, e);
            listSPIPrescaler_SelectedIndexChanged(sender, e);
            listTWIPrescaler_SelectedIndexChanged(sender, e);
            listADCPrescaler_SelectedIndexChanged(sender, e);
        }

        private void listSPIPrescaler_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (listSPIPrescaler.SelectedIndex < 0)
                    return;

                string sel = (string)listSPIPrescaler.Items[listSPIPrescaler.SelectedIndex];
                string[] parts = sel.Split(new char[] { ' ', '\0', '\t', '\r', '\n', });
                int div = 1;
                if (int.TryParse(parts[0], out div))
                {
                    txtSPIFreq.Text = String.Format("{0:0}", numClkFreq.Value / Convert.ToDecimal(div));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error during calculation: " + ex.Message);
            }
        }

        private void chkBaudSPIMode_CheckedChanged(object sender, EventArgs e)
        {
            if (chkBaudSPIMode.Checked)
                chkBaudDoubleSpeed.Enabled = false;
            else
                chkBaudDoubleSpeed.Enabled = true;

            listBaud_SelectedIndexChanged(sender, e);
        }

        private void listTWIPrescaler_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (listTWIPrescaler.SelectedIndex < 0)
                    return;

                string sel = (string)listTWIPrescaler.Items[listTWIPrescaler.SelectedIndex];
                string[] parts = sel.Split(new char[] { ' ', '\0', '\t', '\r', '\n', });
                int div;
                if (int.TryParse(parts[0], out div))
                {
                    int TWBR = Convert.ToInt32(Math.Round(((numClkFreq.Value / numTWIFreq.Value) - Convert.ToDecimal(16)) / Convert.ToDecimal(2 * div)));

                    decimal actualFreq = numClkFreq.Value / Convert.ToDecimal(16 + (2 * TWBR * div));

                    txtTWIResults.Text = string.Format(
                        "Closest TWBR Value: {0}" + Environment.NewLine +
                        "Resulting TWI Frequency: {1:0.0} Hz" + Environment.NewLine,
                        TWBR,
                        actualFreq
                        );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error during calculation: " + ex.Message);
            }
        }

        private void numTWIFreq_ValueChanged(object sender, EventArgs e)
        {
            listTWIPrescaler_SelectedIndexChanged(sender, e);
        }

        private void listADCPrescaler_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (listADCPrescaler.SelectedIndex < 0)
                    return;

                string sel = (string)listADCPrescaler.Items[listADCPrescaler.SelectedIndex];
                string[] parts = sel.Split(new char[] { ' ', '\0', '\t', '\r', '\n', });
                int div;
                if (int.TryParse(parts[0], out div))
                {

                    txtADCTimeRes.Text = string.Format(
                        "ADC Clock Frequency: {0:0.0} Hz" + Environment.NewLine +
                        "Time for one sample in milliseconds (13 ADC Clock Ticks): {1}" + Environment.NewLine,
                        numClkFreq.Value / Convert.ToDecimal(div),
                        Convert.ToDecimal(13 * div * 1000) / numClkFreq.Value
                        );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error during calculation: " + ex.Message);
            }
        }

        private void btnADCUseInput_Click(object sender, EventArgs e)
        {
            try
            {
                decimal percent = numADCInput.Value / numADCRef.Value;
                numADC10BitRes.Value = Convert.ToDecimal(Math.Pow(2, 10)) * percent;
                numADC8BitRes.Value = Math.Floor(Math.Floor(numADC10BitRes.Value) / 4);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error during calculation: " + ex.Message);
            }
        }

        private void btnADCUse10BitRes_Click(object sender, EventArgs e)
        {
            try
            {
                decimal percent = numADC10BitRes.Value / Convert.ToDecimal(Math.Pow(2, 10));
                numADCInput.Value = numADCRef.Value * percent;
                numADC8BitRes.Value = Math.Floor(Math.Floor(numADC10BitRes.Value) / 4);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error during calculation: " + ex.Message);
            }
        }

        private void btnADCUse8BitRes_Click(object sender, EventArgs e)
        {
            try
            {
                numADC10BitRes.Value = numADC8BitRes.Value * 4;
                decimal percent = numADC10BitRes.Value / Convert.ToDecimal(Math.Pow(2, 10));
                numADCInput.Value = numADCRef.Value * percent;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error during calculation: " + ex.Message);
            }
        }
    }
}
