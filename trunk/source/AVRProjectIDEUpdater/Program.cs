using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

using AVRProjectIDE;

namespace AVRProjectIDEUpdater
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                if (args.Length > 0)
                {
                    Application.Run(new Form1(args[0]));
                }
                else
                {
                    MessageBox.Show("You are not supposed to run this alone");
                }
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
    }
}
