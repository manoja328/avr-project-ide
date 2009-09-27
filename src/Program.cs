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
