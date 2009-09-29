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
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        public static void Main(string[] args)
        {
            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                SettingsManagement.Load();
                ProjTemplate.Load();

                try
                {
                    UpdateMech.CheckForUpdates();
                }
                catch (Exception ex)
                {
                    ErrorReportWindow erw = new ErrorReportWindow(ex, "Error Checking Updates");
                    erw.ShowDialog();
                }

                AVRProject newProject = new AVRProject();

                if (args.Length > 0)
                {
                    string fname = args[0];

                    if (newProject.Open(fname) == true)
                    {
                        SettingsManagement.AddFileAsMostRecent(fname);
                    }
                }

                if (newProject.IsReady == false)
                {
                    Application.Run(new WelcomeWindow(newProject));
                }

                if (newProject.IsReady)
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
                            UpdateUpdater();
                            if (MessageBox.Show("An Updated Version of AVRProjectIDE is Available (" + UpdateMech.NewBuildID + "), Run the Updater?", "Update Available", MessageBoxButtons.YesNo) == DialogResult.Yes)
                            {
                                System.Diagnostics.Process updater = new System.Diagnostics.Process();
                                updater.StartInfo = new System.Diagnostics.ProcessStartInfo("AVRProjectIDEUpdater.exe", UpdateMech.NewBuildID + " http://avr-project-ide.googlecode.com/files/" + UpdateMech.NewBuildID + ".exe" + " AVRProjectIDE.exe");
                                updater.Start();
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

        private static void UpdateUpdater()
        {
            bool rewrite = false;
            byte[] newBArr = Properties.Resources.AVRProjectIDEUpdater;
            string path = Program.CleanFilePath(Directory.GetCurrentDirectory()) + Path.DirectorySeparatorChar + "AVRProjectIDEUpdater.exe";
            if (File.Exists(path))
            {
                byte[] oldBArr = File.ReadAllBytes(path);
                for (int i = 0; i < oldBArr.Length && i < newBArr.Length; i += 16)
                    if (oldBArr[i] != newBArr[i])
                    {
                        rewrite = true;
                        break;
                    }
            }
            else
                rewrite = true;

            if (rewrite)
            {
                File.WriteAllBytes(path, newBArr);

                MessageBox.Show("Your Automatic Updater has been Updated");
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
    }
}
