using System;
using System.Diagnostics;
using System.Net;
using System.IO;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Ini;
using AVRProjectIDEUpdater;

namespace AVRProjectIDEUpdater
{
    public partial class Form1 : Form
    {
        static public string AppDataPath
        {
            get { return Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + Path.DirectorySeparatorChar + "AVRProjectIDE" + Path.DirectorySeparatorChar; }
        }

        static public string CurDirPath
        {
            get { return Directory.GetCurrentDirectory().Trim().TrimEnd(Path.DirectorySeparatorChar).Trim() + Path.DirectorySeparatorChar; }
        }

        private string buildID;
        private IniFile ini;

        public Form1(string buildID)
        {
            InitializeComponent();

            this.buildID = buildID;

            if (Program.MakeSurePathExists(AppDataPath) == false)
            {
                MessageBox.Show("Error Creating AppData Path");
                this.Close();
            }

            if (File.Exists(AppDataPath + "settings.ini") == false)
            {
                try
                {
                    StreamWriter writer = new StreamWriter(AppDataPath + "settings.ini");
                    writer.WriteLine();
                    writer.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error Creating settings.ini, " + ex.Message);
                    this.Close();
                }
            }

            ini = new IniFile(AppDataPath + "settings.ini");
        }

        void wc_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new AsyncCompletedEventHandler(wc_DownloadFileCompleted), new object[] { sender, e, });
            }
            else
            {
                if (e.Cancelled == false && e.Error == null)
                {
                    try
                    {
                        if (File.Exists(CurDirPath + "updatedexe.exe"))
                        {
                            File.Copy(CurDirPath + "updatedexe.exe", CurDirPath + "AVRProjectIDE.exe", true);
                            ini.Write("Updater", "BuildID", buildID);
                            MessageBox.Show("Update Complete!");
                            File.Delete(CurDirPath + "updatedexe.exe");
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("Error After Download Completion: Cannot Find Downloaded File");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error After Download Completion: " + ex.Message);
                    }
                }
                else if (e.Error != null)
                {
                    MessageBox.Show("Error During Download: " + e.Error.Message);
                    this.Close();
                }
            }
        }

        void wc_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new DownloadProgressChangedEventHandler(wc_DownloadProgressChanged), new object[] { sender, e });
            }
            else
            {
                progressBar1.Value = e.ProgressPercentage;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            bool start = true;
            Process self = Process.GetCurrentProcess();
            Process[] procList = Process.GetProcesses();
            foreach (Process proc in procList)
            {
                if (self.Id != proc.Id)
                {
                    if (proc.ProcessName.ToLowerInvariant().Contains("avrprojectide.exe"))
                    {
                        if (proc.HasExited == false)
                        {
                            start = false;
                        }
                    }
                }
            }

            if (start)
            {
                timer1.Enabled = false;
                progressBar1.Style = ProgressBarStyle.Blocks;
                WebClient wc = new WebClient();
                wc.DownloadFileAsync(new Uri("http://avr-project-ide.googlecode.com/files/" + buildID + ".exe"), CurDirPath + "updatedexe.exe");
                wc.DownloadProgressChanged += new DownloadProgressChangedEventHandler(wc_DownloadProgressChanged);
                wc.DownloadFileCompleted += new AsyncCompletedEventHandler(wc_DownloadFileCompleted);
            }
            else
            {
                progressBar1.Style = ProgressBarStyle.Marquee;
            }
        }
    }
}

namespace Ini
{
    /// <summary>
    /// Create a New INI file to store or load data
    /// </summary>

    public class IniFile
    {
        public string FilePath;

        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(
            string section, string key, string val,
            string filePath
        );

        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(
            string section, string key,
            string def, StringBuilder retVal, int size,
            string filePath
        );

        /// <summary>
        /// INIFile Constructor.
        /// </summary>
        /// <PARAM name="filePath">File path to .ini file</PARAM>
        public IniFile(string filePath)
        {
            FilePath = filePath;
        }

        /// <summary>
        /// Write data to .ini file
        /// </summary>
        /// <param name="Section">Section to write to</param>
        /// <param name="Key">Key to write to</param>
        /// <param name="Value">Value to write</param>
        /// <returns>True if successful</returns>
        public bool Write(string Section, string Key, string Value)
        {
            if (WritePrivateProfileString(Section, Key, Value, this.FilePath) != 0)
                return true;
            else
                return false;
        }

        /// <summary>
        /// Read data from .ini file
        /// </summary>
        /// <param name="Section">Section to read from</param>
        /// <param name="Key">Key to read</param>
        /// <returns>String value of key, or null if failed</returns>
        public string Read(string Section, string Key)
        {
            StringBuilder temp = new StringBuilder(255);

            int i = GetPrivateProfileString(
                Section, Key,
                "", temp, 255,
                this.FilePath
            );

            if (i != 0)
                return temp.ToString();
            else
                return null;

        }

        public bool Exists
        {
            get { return File.Exists(FilePath); }
        }
    }
}