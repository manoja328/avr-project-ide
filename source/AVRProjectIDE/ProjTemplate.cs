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
            string templatePath = SettingsManagement.AppDataPath + "templates.xml";
            if (File.Exists(templatePath) == false)
            {
                try
                {
                    File.WriteAllText(templatePath, Properties.Resources.templates);
                }
                catch (Exception ex)
                {
                    ErrorReportWindow erw = new ErrorReportWindow(ex, "Error while creating templates");
                    erw.ShowDialog();
                    return false;
                }
            }

            try
            {
                tempDoc = new XmlDocument();
                tempDoc.Load(templatePath);

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
                ErrorReportWindow erw = new ErrorReportWindow(ex, "Error while loading templates");
                erw.ShowDialog();
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
            int appCnt = 0;

            XmlElement template;
            if (templates.TryGetValue(name, out template))
            {
                XmlElement docx = (XmlElement)template.CloneNode(true);
                
                List<string> alreadyInherited = new List<string>();
                alreadyInherited.Add(name);
                bool foundNew;
                do
                {
                    foundNew = false;
                    string inheritedInnerXml = "";
                    foreach (XmlElement param in docx.GetElementsByTagName("Inherit"))
                    {
                        string inheritName = param.InnerText.Trim();
                        if (alreadyInherited.Contains(inheritName) == false)
                        {
                            XmlElement inherited;
                            if (templates.TryGetValue(inheritName, out inherited))
                            {
                                inheritedInnerXml += inherited.InnerXml;
                                alreadyInherited.Add(inheritName);
                                foundNew = true;
                            }
                        }
                    }
                    docx.InnerXml += inheritedInnerXml;
                } while (foundNew);

                foreach (XmlElement param in docx.GetElementsByTagName("ClockFreq"))
                {
                    proj.ClockFreq = decimal.Parse(param.InnerText);
                    appCnt++;
                }
                foreach (XmlElement param in docx.GetElementsByTagName("Device"))
                {
                    proj.Device = param.InnerText;
                    appCnt++;
                }
                foreach (XmlElement param in docx.GetElementsByTagName("LinkerOpt"))
                {
                    proj.LinkerOptions = param.InnerText;
                }
                foreach (XmlElement param in docx.GetElementsByTagName("OtherOpt"))
                {
                    proj.OtherOptions = param.InnerText;
                }
                foreach (XmlElement param in docx.GetElementsByTagName("OtherOptionsForC"))
                {
                    proj.OtherOptionsForC = param.InnerText;
                }
                foreach (XmlElement param in docx.GetElementsByTagName("OtherOptionsForCPP"))
                {
                    proj.OtherOptionsForCPP = param.InnerText;
                }
                foreach (XmlElement param in docx.GetElementsByTagName("OtherOptionsForS"))
                {
                    proj.OtherOptionsForS = param.InnerText;
                }
                foreach (XmlElement param in docx.GetElementsByTagName("Optimization"))
                {
                    proj.Optimization = param.InnerText;
                }
                foreach (XmlElement param in docx.GetElementsByTagName("UseInitStack"))
                {
                    proj.UseInitStack = param.InnerText.ToLowerInvariant().Trim() == true.ToString().Trim().ToLowerInvariant();
                }
                foreach (XmlElement param in docx.GetElementsByTagName("InitStackAddr"))
                {
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
                foreach (XmlElement param in docx.GetElementsByTagName("PackStructs"))
                {
                    proj.PackStructs = param.InnerText.ToLowerInvariant().Trim() == true.ToString().Trim().ToLowerInvariant();
                }
                foreach (XmlElement param in docx.GetElementsByTagName("ShortEnums"))
                {
                    proj.ShortEnums = param.InnerText.ToLowerInvariant().Trim() == true.ToString().Trim().ToLowerInvariant();
                }
                foreach (XmlElement param in docx.GetElementsByTagName("UnsignedBitfields"))
                {
                    proj.UnsignedBitfields = param.InnerText.ToLowerInvariant().Trim() == true.ToString().Trim().ToLowerInvariant();
                }
                foreach (XmlElement param in docx.GetElementsByTagName("UnsignedChars"))
                {
                    proj.UnsignedChars = param.InnerText.ToLowerInvariant().Trim() == true.ToString().Trim().ToLowerInvariant();
                }
                foreach (XmlElement param in docx.GetElementsByTagName("FunctionSections"))
                {
                    proj.FunctionSections = param.InnerText.ToLowerInvariant().Trim() == true.ToString().Trim().ToLowerInvariant();
                }
                foreach (XmlElement param in docx.GetElementsByTagName("DataSections"))
                {
                    proj.DataSections = param.InnerText.ToLowerInvariant().Trim() == true.ToString().Trim().ToLowerInvariant();
                }

                foreach (XmlElement param in docx.GetElementsByTagName("BurnPart"))
                {
                    proj.BurnPart = param.InnerText;
                }
                foreach (XmlElement param in docx.GetElementsByTagName("BurnProgrammer"))
                {
                    proj.BurnProgrammer = param.InnerText;
                }
                foreach (XmlElement param in docx.GetElementsByTagName("BurnOptions"))
                {
                    proj.BurnOptions = param.InnerText;
                }
                foreach (XmlElement param in docx.GetElementsByTagName("BurnFuseBox"))
                {
                    proj.BurnFuseBox = param.InnerText;
                }
                foreach (XmlElement param in docx.GetElementsByTagName("BurnBaud"))
                {
                    try { proj.BurnBaud = int.Parse(param.InnerText); }
                    catch { }
                }
                foreach (XmlElement param in docx.GetElementsByTagName("BurnAutoReset"))
                {
                    proj.BurnAutoReset = param.InnerText.Trim().ToLowerInvariant() == true.ToString().Trim().ToLowerInvariant();
                }

                foreach (XmlElement container in docx.GetElementsByTagName("IncludeDirList"))
                {
                    foreach (XmlElement i in container.GetElementsByTagName("DirPath"))
                    {
                        if (proj.IncludeDirList.Contains(i.InnerText) == false)
                            proj.IncludeDirList.Add(i.InnerText);
                    }
                }

                foreach (XmlElement container in docx.GetElementsByTagName("LibraryDirList"))
                {
                    foreach (XmlElement i in container.GetElementsByTagName("DirPath"))
                    {
                        if (proj.LibraryDirList.Contains(i.InnerText) == false)
                            proj.LibraryDirList.Add(i.InnerText);
                    }
                }

                foreach (XmlElement container in docx.GetElementsByTagName("LinkObjList"))
                {
                    foreach (XmlElement i in container.GetElementsByTagName("Obj"))
                    {
                        if (proj.LinkObjList.Contains(i.InnerText) == false)
                            proj.LinkObjList.Add(i.InnerText);
                    }
                }

                foreach (XmlElement container in docx.GetElementsByTagName("LinkLibList"))
                {
                    foreach (XmlElement i in container.GetElementsByTagName("Lib"))
                    {
                        if (proj.LinkLibList.Contains(i.InnerText) == false)
                            proj.LinkLibList.Add(i.InnerText);
                    }
                }

                foreach (XmlElement container in docx.GetElementsByTagName("MemorySegList"))
                {
                    foreach (XmlElement i in container.GetElementsByTagName("Segment"))
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
                            MemorySegment seg = new MemorySegment(type.InnerText, name_.InnerText, address);
                            
                            if (proj.MemorySegList.Contains(seg) == false)
                                proj.MemorySegList.Add(seg);
                        }
                        catch { }
                    }
                }

                if (appCnt >= 2)
                    proj.HasBeenConfigged = true;

                try
                {
                    foreach (XmlElement i in docx.GetElementsByTagName("CreateFile"))
                    {
                        string fname = i.GetAttribute("name");
                        if (string.IsNullOrEmpty(fname) == false)
                        {
                            try
                            {
                                ProjectFile f = new ProjectFile(Program.AbsPathFromRel(proj.DirPath, fname), proj);
                                if (proj.FileList.ContainsKey(fname) == false)
                                {
                                    proj.FileList.Add(fname, f);
                                    proj.ShouldReloadFiles = true;

                                    if (f.Exists == false)
                                    {
                                        Program.MakeSurePathExists(f.FileDir);
                                        File.WriteAllText(f.FileAbsPath, " ");

                                        foreach (XmlElement k in i.GetElementsByTagName("Template"))
                                        {
                                            File.WriteAllText(f.FileAbsPath, FileTemplate.CreateFile(f.FileName, proj.FileNameNoExt, k.InnerText));
                                            break;
                                        }
                                    }
                                }
                            }
                            catch { }
                        }
                    }
                }
                catch
                {
                    return false;
                }

                return true;
            }
            else
                return false;
        }
    }
}
