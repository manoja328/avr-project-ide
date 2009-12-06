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

                AVRProject.LoadTemplateCommonProperties(ref appCnt, docx, proj);

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
