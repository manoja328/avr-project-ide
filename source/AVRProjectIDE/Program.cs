using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Windows.Forms;

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
                ErrorReportWindow erw = new ErrorReportWindow(ex, "Initialization Error");
                erw.ShowDialog();
            }

            try
            {
                UpdateMech.CheckForUpdates();
            }
            catch (Exception ex)
            {
                ErrorReportWindow erw = new ErrorReportWindow(ex, "Error Checking Updates");
                erw.ShowDialog();
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

                KeywordScanner.Initialize();

                Application.Run(new IDEWindow(newProject));
            }
            catch (Exception ex)
            {
                ErrorReportWindow erw = new ErrorReportWindow(ex, "Main IDE Error");
                erw.ShowDialog();
            }

            try
            {
                if (UpdateMech.HasFinishedChecking)
                {
                    if (UpdateMech.UpdateAvailable)
                    {
                        try
                        {
                            if (MessageBox.Show("An Updated Version of AVRProjectIDE is Available (" + SettingsManagement.BuildID + " to " + UpdateMech.NewBuildID + "), Download it?", "Update Available", MessageBoxButtons.YesNo) == DialogResult.Yes)
                            {
                                System.Diagnostics.Process.Start(Properties.Resources.WebsiteURL);
                            }
                        }
                        catch (Exception ex)
                        {
                            ErrorReportWindow erw = new ErrorReportWindow(ex, "Updater Error");
                            erw.ShowDialog();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorReportWindow erw = new ErrorReportWindow(ex, "Error Checking Updates");
                erw.ShowDialog();
            }
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

        public static bool TryParseText(string text, out int num)
        {
            num = 0;
            try
            {
                text = text.Trim().ToLowerInvariant();

                if (string.IsNullOrEmpty(text))
                    return false;

                bool isHex = false;
                if (text.Contains("$") || text.Contains("0x"))
                    isHex = true;

                foreach (char c in text)
                {
                    if ("abcdef".Contains(char.ToString(c)))
                    {
                        isHex = true;
                    }
                }

                if (isHex)
                {
                    text = text.Replace("$", "0x");
                    num = Convert.ToInt32(text, 16);
                }
                else
                {
                    num = Convert.ToInt32(text);
                }

                return true;
            }
            catch { return false; }
        }
    }
}
