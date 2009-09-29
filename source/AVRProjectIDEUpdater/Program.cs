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

                if (args.Length == 3)
                {
                    Application.Run(new Form1(args[0], args[1], args[2]));
                }
                else
                {
                    string args_ = "";
                    foreach (string s in args)
                    {
                        args_ += s + " ";
                    }
                    MessageBox.Show("Error, Invalid Arguments: " + args_);
                }
            }
            catch (Exception ex)
            {
                ErrorReportWindow erw = new ErrorReportWindow(ex, "Updater Error");
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

        static public void CheckForDLLs()
        {
            string curdir = CleanFilePath(Directory.GetCurrentDirectory()) + Path.DirectorySeparatorChar;

            try
            {
                File.WriteAllBytes(curdir + "SciLexer.dll", Properties.Resources.SciLexer);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error, SciLexer.dll could not be unpacked, " + ex.Message + ", program may not run");
            }

            try
            {
                File.WriteAllBytes(curdir + "ScintillaNet.dll", Properties.Resources.ScintillaNet);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error, ScintillaNet.dll could not be unpacked, " + ex.Message + ", program may not run");
            }

            try
            {
                File.WriteAllBytes(curdir + "WeifenLuo.WinFormsUI.Docking.dll", Properties.Resources.WeifenLuo_WinFormsUI_Docking);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error, WeifenLuo.WinFormsUI.Docking.dll could not be unpacked, " + ex.Message + ", program may not run");
            }
        }
    }
}
