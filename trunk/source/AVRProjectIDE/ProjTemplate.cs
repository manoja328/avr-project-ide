using System;
using System.Windows.Forms;
using System.IO;
using System.Xml;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AVRProjectIDE
{
    public class ProjTemplate
    {
        static XmlDocument tempDoc;
        static Dictionary<string, XmlElement> templates = new Dictionary<string, XmlElement>();

        static public bool Load()
        {
            if (File.Exists(SettingsManagement.AppDataPath + "templates.xml") == false)
            {
                try
                {
                    StreamWriter writer = new StreamWriter(SettingsManagement.AppDataPath + "templates.xml");
                    writer.Write(Properties.Resources.template);
                    writer.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error while creating templates, " + ex.Message);
                    return false;
                }
            }

            try
            {
                tempDoc = new XmlDocument();
                tempDoc.Load(SettingsManagement.AppDataPath + "templates.xml");

                XmlElement docEle = tempDoc.DocumentElement;

                templates.Clear();
                XmlNodeList tempList = docEle.GetElementsByTagName("template");
                foreach (XmlElement temp in tempList)
                {
                    string name = temp.GetAttribute("name");
                    if (string.IsNullOrEmpty(name) == false)
                    {
                        templates.Add(name, temp);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error while loading templates, " + ex.Message);
                return false;
            }

            return true;
        }

        static public string[] GetTemplateNames()
        {
            List<string> res = new List<string>();
            foreach (KeyValuePair<string, XmlElement> i in templates)
            {
                res.Add(i.Key);
            }
            return res.ToArray();
        }

        static public bool ApplyTemplate(string name, AVRProject proj)
        {
            XmlElement template;
            if (templates.TryGetValue(name, out template))
            {
                XmlElement docx = template;
                XmlElement param;
                if (docx.GetElementsByTagName("ClockFreq").Count > 0)
                {
                    param = (XmlElement)docx.GetElementsByTagName("ClockFreq")[0];
                    proj.ClockFreq = decimal.Parse(param.InnerText);
                }
                if (docx.GetElementsByTagName("Device").Count > 0)
                {
                    param = (XmlElement)docx.GetElementsByTagName("Device")[0];
                    proj.Device = param.InnerText;
                }
                if (docx.GetElementsByTagName("LinkerOpt").Count > 0)
                {
                    param = (XmlElement)docx.GetElementsByTagName("LinkerOpt")[0];
                    proj.LinkerOptions = param.InnerText;
                }
                if (docx.GetElementsByTagName("OtherOpt").Count > 0)
                {
                    param = (XmlElement)docx.GetElementsByTagName("OtherOpt")[0];
                    proj.OtherOptions = param.InnerText;
                }
                if (docx.GetElementsByTagName("Optimization").Count > 0)
                {
                    param = (XmlElement)docx.GetElementsByTagName("Optimization")[0];
                    proj.Optimization = param.InnerText;
                }
                if (docx.GetElementsByTagName("UseInitStack").Count > 0)
                {
                    param = (XmlElement)docx.GetElementsByTagName("UseInitStack")[0];
                    proj.UseInitStack = param.InnerText.ToLowerInvariant().Trim() == "true";
                }
                if (docx.GetElementsByTagName("InitStackAddr").Count > 0)
                {
                    param = (XmlElement)docx.GetElementsByTagName("InitStackAddr")[0];
                    try
                    {
                        if (param.InnerText.ToLowerInvariant().StartsWith("0x"))
                        {
                            proj.InitStackAddr = Convert.ToUInt32(param.InnerText, 16);
                        }
                        else
                        {
                            proj.InitStackAddr = Convert.ToUInt32("0x" + param.InnerText, 16);
                        }
                    }
                    catch { }
                }
                if (docx.GetElementsByTagName("PackStructs").Count > 0)
                {
                    param = (XmlElement)docx.GetElementsByTagName("PackStructs")[0];
                    proj.PackStructs = param.InnerText.ToLowerInvariant().Trim() == "true";
                }
                if (docx.GetElementsByTagName("ShortEnums").Count > 0)
                {
                    param = (XmlElement)docx.GetElementsByTagName("ShortEnums")[0];
                    proj.ShortEnums = param.InnerText.ToLowerInvariant().Trim() == "true";
                }
                if (docx.GetElementsByTagName("UnsignedBitfields").Count > 0)
                {
                    param = (XmlElement)docx.GetElementsByTagName("UnsignedBitfields")[0];
                    proj.UnsignedBitfields = param.InnerText.ToLowerInvariant().Trim() == "true";
                }
                if (docx.GetElementsByTagName("UnsignedChars").Count > 0)
                {
                    param = (XmlElement)docx.GetElementsByTagName("UnsignedChars")[0];
                    proj.UnsignedChars = param.InnerText.ToLowerInvariant().Trim() == "true";
                }
                if (docx.GetElementsByTagName("FunctionSections").Count > 0)
                {
                    param = (XmlElement)docx.GetElementsByTagName("FunctionSections")[0];
                    proj.FunctionSections = param.InnerText.ToLowerInvariant().Trim() == "true";
                }
                if (docx.GetElementsByTagName("DataSections").Count > 0)
                {
                    param = (XmlElement)docx.GetElementsByTagName("DataSections")[0];
                    proj.DataSections = param.InnerText.ToLowerInvariant().Trim() == "true";
                }

                if (docx.GetElementsByTagName("BurnPart").Count > 0)
                {
                    param = (XmlElement)docx.GetElementsByTagName("BurnPart")[0];
                    proj.BurnPart = param.InnerText;
                }
                if (docx.GetElementsByTagName("BurnProgrammer").Count > 0)
                {
                    param = (XmlElement)docx.GetElementsByTagName("BurnProgrammer")[0];
                    proj.BurnProgrammer = param.InnerText;
                }
                if (docx.GetElementsByTagName("BurnOptions").Count > 0)
                {
                    param = (XmlElement)docx.GetElementsByTagName("BurnOptions")[0];
                    proj.BurnOptions = param.InnerText;
                }
                if (docx.GetElementsByTagName("BurnBaud").Count > 0)
                {
                    param = (XmlElement)docx.GetElementsByTagName("BurnBaud")[0];
                    try { proj.BurnBaud = int.Parse(param.InnerText); }
                    catch { }
                }

                XmlElement container;
                XmlNodeList list;

                if (docx.GetElementsByTagName("IncludeDirList").Count > 0)
                {
                    proj.IncludeDirList.Clear();
                    container = (XmlElement)docx.GetElementsByTagName("IncludeDirList")[0];
                    list = container.GetElementsByTagName("DirPath");
                    foreach (XmlElement i in list)
                    {
                        proj.IncludeDirList.Add(i.InnerText);
                    }
                }

                if (docx.GetElementsByTagName("LibraryDirList").Count > 0)
                {
                    proj.LibraryDirList.Clear();
                    container = (XmlElement)docx.GetElementsByTagName("LibraryDirList")[0];
                    list = container.GetElementsByTagName("DirPath");
                    foreach (XmlElement i in list)
                    {
                        proj.LibraryDirList.Add(i.InnerText);
                    }
                }

                if (docx.GetElementsByTagName("LinkObjList").Count > 0)
                {
                    proj.LinkObjList.Clear();
                    container = (XmlElement)docx.GetElementsByTagName("LinkObjList")[0];
                    list = container.GetElementsByTagName("Obj");
                    foreach (XmlElement i in list)
                    {
                        proj.LinkObjList.Add(i.InnerText);
                    }
                }

                if (docx.GetElementsByTagName("LinkLibList").Count > 0)
                {
                    proj.LinkLibList.Clear();
                    container = (XmlElement)docx.GetElementsByTagName("LinkLibList")[0];
                    list = container.GetElementsByTagName("Lib");
                    foreach (XmlElement i in list)
                    {
                        proj.LinkLibList.Add(i.InnerText);
                    }
                }

                if (docx.GetElementsByTagName("MemorySegList").Count > 0)
                {
                    proj.MemorySegList.Clear();
                    container = (XmlElement)docx.GetElementsByTagName("MemorySegList")[0];
                    list = container.GetElementsByTagName("Segment");
                    foreach (XmlElement i in list)
                    {
                        try
                        {
                            XmlElement type = (XmlElement)i.GetElementsByTagName("Type")[0];
                            XmlElement name_ = (XmlElement)i.GetElementsByTagName("Name")[0];
                            XmlElement addr = (XmlElement)i.GetElementsByTagName("Addr")[0];
                            uint address;
                            if (addr.InnerText.ToLowerInvariant().StartsWith("0x"))
                            {
                                address = Convert.ToUInt32(addr.InnerText, 16);
                            }
                            else
                            {
                                address = Convert.ToUInt32("0x" + addr.InnerText, 16);
                            }
                            proj.MemorySegList.Add(new MemorySegment(type.InnerText, name_.InnerText, address));
                        }
                        catch { }
                    }
                }

                return true;
            }
            else
                return false;
        }
    }
}
