using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace AVRProjectIDE
{
    public static class Program
    {
        private static SplashScreen splash;
        public static SplashScreen SplashScreen
        {
            get { return splash; }
        }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        public static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            splash = new SplashScreen();
            splash.Show();

            try
            {
                SettingsManagement.Load();
                FileTemplate.Unpack();
                ProjTemplate.Load();

                if (SettingsManagement.AutocompleteEnable)
                    KeywordImageGen.GenerateKeywordImages();
            }
            catch (Exception ex)
            {
                ErrorReportWindow.Show(ex, "Initialization Error");
                
            }

            try
            {
                UpdateMech.CheckForUpdates();
            }
            catch (Exception ex)
            {
                ErrorReportWindow.Show(ex, "Error Checking Updates");
                
            }

            try
            {
                AVRProject newProject = new AVRProject();

                if (args.Length > 0)
                {
                    string fname = args[0];

                    if (newProject.Open(fname) == true)
                    {
                        SettingsManagement.AddFileAsMostRecent(fname);
                    }
                    else
                    {
                        MessageBox.Show("Error, failed to open file");
                    }
                }
                else if (SettingsManagement.OpenLastProject)
                {
                    if (string.IsNullOrEmpty(SettingsManagement.LastProjectPath) == false)
                    {
                        if (newProject.Open(SettingsManagement.LastProjectPath) == true)
                        {
                            SettingsManagement.AddFileAsMostRecent(SettingsManagement.LastProjectPath);
                        }
                        else
                        {
                            MessageBox.Show("Error, failed to open file");
                        }
                    }
                }

                KeywordScanner.Initialize();

                Application.Run(new IDEWindow(newProject));

                if (newProject.IsReady)
                {
                    if (SettingsManagement.SaveRecentList() == false)
                    {
                        MessageBox.Show("Error, Could Not Save Recent File List");
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorReportWindow.Show(ex, "Main IDE Error");
                
            }

            try
            {
                if (UpdateMech.HasFinishedChecking)
                {
                    if (UpdateMech.UpdateAvailable)
                    {
                        try
                        {
                            if (MessageBox.Show("An Updated Version of AVR Project IDE is Available (" + SettingsManagement.BuildID + " to " + UpdateMech.NewBuildID + "). Would you like to download it?", "Update Available", MessageBoxButtons.YesNo) == DialogResult.Yes)
                            {
                                System.Diagnostics.Process.Start(Properties.Resources.WebsiteURL);
                            }
                        }
                        catch (Exception ex)
                        {
                            ErrorReportWindow.Show(ex, "Updater Error");
                            
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorReportWindow.Show(ex, "Error Checking Updates");
            }

            try
            {
                if (SettingsManagement.LastRunVersion != SettingsManagement.BuildID)
                {
                    NotifyOfUserAction();
                }

                SettingsManagement.LastRunVersion = SettingsManagement.BuildID;
            }
            catch { }
        }

        private static void NotifyOfUserAction()
        {
            bool showMsg = false;
            string msg = "It appears that you have recently updated AVR Project IDE. There may be some files that may have been updated but you already have a different version." + Environment.NewLine;
            msg += "You may have customized these files yourself, in this case you can choose not to update these files." + Environment.NewLine;
            msg += "If you wish to update these files, you can delete them and they will be updated when you start AVR Project IDE again." + Environment.NewLine;
            msg += "You can also save a backup of your own version, and then delete the originals to receive the updated version, and then merge the changes manually." + Environment.NewLine;
            msg += "These are the files:" + Environment.NewLine;

            try
            {
                if (File.ReadAllText(SettingsManagement.AppDataPath + "templates.xml") != Properties.Resources.templates)
                {
                    msg += "Project configuration templates file at '" + SettingsManagement.AppDataPath + "templates.xml" + "'" + Environment.NewLine;
                    showMsg = true;
                }

                if (File.Exists(SettingsManagement.AppDataPath + "chip_xml" + Path.DirectorySeparatorChar + "interruptvectors.xml"))
                {
                    if (File.ReadAllText(SettingsManagement.AppDataPath + "chip_xml" + Path.DirectorySeparatorChar + "interruptvectors.xml") != Properties.Resources.interruptvectors)
                    {
                        msg += "Interrupt vector list file at '" + SettingsManagement.AppDataPath + "chip_xml" + Path.DirectorySeparatorChar + "interruptvectors.xml" + "'" + Environment.NewLine;
                        showMsg = true;
                    }
                }

                if (File.ReadAllText(SettingsManagement.AppDataPath + "helplinks.xml") != Properties.Resources.helplinks)
                {
                    msg += "Help links collection file at '" + SettingsManagement.AppDataPath + "helplinks.xml" + "'" + Environment.NewLine;
                    showMsg = true;
                }

                if (SettingsManagement.AutocompleteEnable)
                {
                    if (File.ReadAllText(SettingsManagement.AppDataPath + "autocomplete.xml") != Properties.Resources.autocomplete)
                    {
                        msg += "Autocomplete keyword collection file at '" + SettingsManagement.AppDataPath + "autocomplete.xml" + "'" + Environment.NewLine;
                        showMsg = true;
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorReportWindow.Show(ex, "Error while scanning for updated files");
            }

            msg += "Also various other files may have changed, such as the files under '" + SettingsManagement.AppDataPath + "chip_xml" + Path.DirectorySeparatorChar + "' and '" + SettingsManagement.AppDataPath + "file_templates" + Path.DirectorySeparatorChar + "'" + Environment.NewLine;

            if (showMsg)
                MessageBox.Show(msg);
        }

        public static bool MakeSurePathExists(string dirPath)
        {
            dirPath = CleanFilePath(dirPath);
            string[] folders = dirPath.Split(Path.DirectorySeparatorChar);
            string curDir = "";
            foreach (string folder in folders)
            {
                curDir += folder + Path.DirectorySeparatorChar;
                try
                {
                    if (Directory.Exists(curDir) == false)
                    {
                        Directory.CreateDirectory(curDir);
                    }
                }
                catch
                {
                }
            }
            return Directory.Exists(dirPath);
        }

        public static string CleanFilePath(string path)
        {
            string res = null;

            try
            {
                res = path.Trim().Replace('/', Path.DirectorySeparatorChar).Trim(Path.DirectorySeparatorChar).Trim();
            }
            catch { return null; }

            return res;
        }

        public static string AbsPathFromRel(string dirPath, string relPath)
        {
            dirPath = Program.CleanFilePath(dirPath);
            relPath = Program.CleanFilePath(relPath);

            string res = dirPath + Path.DirectorySeparatorChar + relPath;

            try
            {
                if (relPath[1] == ':')
                {
                    return relPath;
                }

                string[] pathDirList = CleanFilePath(relPath).Split(new char[] { Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar, });
                foreach (string d in pathDirList)
                {
                    if (d == "..")
                    {
                        dirPath = dirPath.Substring(0, dirPath.LastIndexOf(Path.DirectorySeparatorChar));
                    }
                    else if (d != ".")
                    {
                        dirPath += Path.DirectorySeparatorChar + d;
                    }
                }

                res = dirPath;
            }
            catch { return res; }

            return res;
        }

        public static string RelativePath(string dirPath, string filePath)
        {
            string relPath = "";
            string[] curDirList = CleanFilePath(dirPath).Split(new char[] { Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar, });
            string[] pathDirList = CleanFilePath(filePath).Split(new char[] { Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar, });
            int i = 0;
            for (i = 0; i < curDirList.Length && i < pathDirList.Length; i++)
            {
                if (curDirList[i] != pathDirList[i])
                {
                    break;
                }
            }
            for (int j = curDirList.Length; j > i; j--)
            {
                relPath += ".." + Path.DirectorySeparatorChar;
            }
            for (int j = i; i < pathDirList.Length; i++)
            {
                relPath += pathDirList[i] + Path.DirectorySeparatorChar;
            }
            return CleanFilePath(relPath);
        }

        public static void CopyAll(DirectoryInfo source, DirectoryInfo target)
        {
            // Check if the target directory exists, if not, create it.
            if (Directory.Exists(target.FullName) == false)
            {
                Directory.CreateDirectory(target.FullName);
            }

            // Copy each file into it’s new directory.
            foreach (FileInfo fi in source.GetFiles())
            {
                //Console.WriteLine(@"Copying {0}\{1}", target.FullName, fi.Name);
                fi.CopyTo(Path.Combine(target.ToString(), fi.Name), true);
            }

            // Copy each subdirectory using recursion.
            foreach (DirectoryInfo diSourceSubDir in source.GetDirectories())
            {
                DirectoryInfo nextTargetSubDir =
                    target.CreateSubdirectory(diSourceSubDir.Name);
                CopyAll(diSourceSubDir, nextTargetSubDir);
            }
        }

        public static bool TryParseText(string text, out long num, bool allowBin)
        {
            num = 0;
            try
            {
                if (string.IsNullOrEmpty(text))
                    return false;

                text = text.Trim().ToLowerInvariant().Replace("$", "0x");

                if (string.IsNullOrEmpty(text))
                    return false;

                try
                {
                    if (allowBin && text.StartsWith("0b") && text.Length >= 3)
                        num = Convert.ToInt64(text.Substring(2), 2);
                    else
                        num = Convert.ToInt64(text, 10);
                }
                catch
                {
                    try
                    {
                        num = Convert.ToInt64(text, 10);
                    }
                    catch
                    {
                        try
                        {
                            num = Convert.ToInt64(text, 16);
                        }
                        catch
                        {
                            return false;
                        }
                    }
                }

                return true;
            }
            catch { return false; }
        }

        public static bool StringToBool(string s)
        {
            if (string.IsNullOrEmpty(s))
                return false;

            s = s.ToLowerInvariant().Trim();

            if (string.IsNullOrEmpty(s))
                return false;

            if (s == "true" || s == "yes" || s == "y" || s == true.ToString().ToLowerInvariant().Trim())
                return true;

            try
            {
                if (Convert.ToInt64(s) == 0)
                    return false;
                else
                    return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool StringToBool(System.Xml.XmlElement x)
        {
            return StringToBool(x.InnerText);
        }

        public static bool StringToBool(System.Xml.XmlAttribute x)
        {
            return StringToBool(x.Value);
        }

        public static void LaunchDonate()
        {
            System.Windows.Forms.MessageBox.Show("There is a donate button on my website");
            System.Diagnostics.Process.Start(Properties.Resources.FranksSiteURL);
        }

        public static string ProperChipName(string chipName)
        {
            if (string.IsNullOrEmpty(chipName))
                chipName = "";

            return chipName.Trim().ToUpperInvariant().Replace("ATXMEGA", "ATxmega").Replace("ATMEGA", "ATmega").Replace("ATTINY", "ATtiny");
        }
    }
}