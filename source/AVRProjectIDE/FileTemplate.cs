using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace AVRProjectIDE
{
    public class FileTemplate
    {
        public static string TemplateFolderPath
        {
            get { return SettingsManagement.AppDataPath + "file_templates" + Path.DirectorySeparatorChar; }
        }

        private static string PrepFinalTemplate(string template)
        {
            List<string> alreadyIncluded = new List<string>();
            bool doneWork = true;

            while (doneWork)
            {
                doneWork = false;

                Regex r = new Regex("%INCTEMPLATEFILE:(.*)%", RegexOptions.Multiline);
                Match m = r.Match(template);

                if (m.Success)
                {
                    try
                    {
                        string fileName = m.Groups[1].Value;
                        string filePath = TemplateFolderPath + fileName;
                        if (alreadyIncluded.Contains(filePath) == false)
                        {
                            doneWork = true;

                            alreadyIncluded.Add(filePath);

                            string fileContents = File.ReadAllText(filePath);

                            template = template.Replace(m.Value, fileContents);
                        }
                    }
                    catch { }
                }

                r = new Regex("%INCFILE:(.*)%", RegexOptions.Multiline);
                m = r.Match(template);

                if (m.Success)
                {
                    try
                    {
                        string filePath = m.Groups[1].Value;
                        if (alreadyIncluded.Contains(filePath) == false)
                        {
                            doneWork = true;

                            alreadyIncluded.Add(filePath);

                            string fileContents = File.ReadAllText(filePath);

                            template = template.Replace(m.Value, fileContents);
                        }
                    }
                    catch { }
                }
            }

            while (true)
            {
                Regex r = new Regex("%DATETIME:(.*)%", RegexOptions.Multiline);
                Match m = r.Match(template);

                if (m.Success)
                {
                    try
                    {
                        string formatStr = m.Groups[1].Value;
                        template = template.Replace(m.Value, DateTime.Now.ToString(formatStr));
                    }
                    catch { }
                }
                else
                    break;
            }

            return template;
        }

        private static string FillTemplate(string template, string fileName, string projName)
        {
            template = PrepFinalTemplate(template);

            string fileNameNoExt = fileName;
            string fileExt = "";
            if (fileName.Contains("."))
            {
                fileNameNoExt = fileName.Substring(0, fileName.LastIndexOf('.'));
                fileExt = fileName.Substring(fileName.LastIndexOf('.') + 1);
            }
            template = template.Replace("%FILENAME%", fileName);
            template = template.Replace("%FILENAMENOEXT%", fileNameNoExt);
            template = template.Replace("%FILEEXT%", fileExt);
            template = template.Replace("%PROJNAME%", projName);
            template = template.Replace("%USERNAME%", Environment.UserName);

            return template;
        }

        public static string CreateFile(string fileName, string projName, string templateName)
        {
            string templatePath = TemplateFolderPath + templateName;

            try
            {
                return FillTemplate(File.ReadAllText(templatePath), fileName, projName);
            }
            catch
            {
                return "";
            }
        }

        public static bool Unpack()
        {
            if (Program.MakeSurePathExists(TemplateFolderPath) == false)
                return false;

            try
            {
                string fpath = TemplateFolderPath + "defaultcode.txt";
                if (File.Exists(fpath) == false)
                    File.WriteAllText(fpath, filetemplates.defaultcode);

                fpath = TemplateFolderPath + "defaultheader.txt";
                if (File.Exists(fpath) == false)
                    File.WriteAllText(fpath, filetemplates.defaultheader);

                fpath = TemplateFolderPath + "copyright.txt";
                if (File.Exists(fpath) == false)
                    File.WriteAllText(fpath, filetemplates.copyright);

                fpath = TemplateFolderPath + "initialmain.txt";
                if (File.Exists(fpath) == false)
                    File.WriteAllText(fpath, filetemplates.initialmain);

                fpath = TemplateFolderPath + "initialpde.txt";
                if (File.Exists(fpath) == false)
                    File.WriteAllText(fpath, filetemplates.initialpde);

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
