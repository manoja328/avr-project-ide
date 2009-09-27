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
#if CHECKFORDLLS
            if (CheckForDLLs() == false) return;
#endif

            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                SettingsManagement.Load();
                ProjTemplate.Load();

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
                ErrorReportWindow erw = new ErrorReportWindow(ex);
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

#if CHECKFORDLLS
        static private bool CheckForDLLs()
        {
            try
            {
                string curdir = CleanFilePath(Directory.GetCurrentDirectory()) + Path.DirectorySeparatorChar;

                if (File.Exists(curdir + "SciLexer.dll") == false)
                {
                    try
                    {
                        StreamWriter writer = new StreamWriter(curdir + "SciLexer.dll");
                        writer.Write(Properties.Resources.SciLexer);
                        writer.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error, SciLexer.dll is missing and could not be unpacked, " + ex.Message + ", program may not run");
                        return false;
                    }
                }

                if (File.Exists(curdir + "ScintillaNet.dll") == false)
                {
                    try
                    {
                        StreamWriter writer = new StreamWriter(curdir + "ScintillaNet.dll");
                        writer.Write(Properties.Resources.ScintillaNet);
                        writer.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error, ScintillaNet.dll is missing and could not be unpacked, " + ex.Message + ", program may not run");
                        return false;
                    }
                }

                if (File.Exists(curdir + "WeifenLuo.WinFormsUI.Docking.dll") == false)
                {
                    try
                    {
                        StreamWriter writer = new StreamWriter(curdir + "WeifenLuo.WinFormsUI.Docking.dll");
                        writer.Write(Properties.Resources.WeifenLuo_WinFormsUI_Docking);
                        writer.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error, WeifenLuo.WinFormsUI.Docking.dll is missing and could not be unpacked, " + ex.Message + ", program may not run");
                        return false;
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                ErrorReportWindow erw = new ErrorReportWindow(ex);
                erw.ShowDialog();
                return false;
            }
        }
#endif

    }
}
