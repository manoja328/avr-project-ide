using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace AVRProjectIDE
{
    class ExternalTool
    {
        private static List<ExternalTool> toolList = new List<ExternalTool>();

        private ToolStripMenuItem mbtn = new ToolStripMenuItem();

        private string cmdStr;
        private string argsStr;
        private string dirStr;

        private IDEWindow wind;

        public ExternalTool(string text, string cmd, string args, string dir, IDEWindow editor)
        {
            this.wind = editor;

            this.mbtn.Text = text;

            this.cmdStr = cmd;
            this.argsStr = args;
            this.dirStr = dir;

            this.mbtn.Click += new EventHandler(mbtn_Click);
        }

        void mbtn_Click(object sender, EventArgs e)
        {
            Run(wind.CurrentFile, wind.CurrentProj);
        }

        public void Run(ProjectFile file, AVRProject proj)
        {
            try
            {
                string cmd = this.cmdStr;
                if (string.IsNullOrEmpty(cmd))
                    return;

                Process p = new Process();
                p.StartInfo.FileName = cmd;

                if (proj != null)
                {
                    if (proj.IsReady)
                    {
                        p.StartInfo.WorkingDirectory = proj.DirPath;
                    }
                }

                string args = this.argsStr;

                if (string.IsNullOrEmpty(args) == false)
                {
                    if (proj != null)
                    {
                        if (proj.IsReady)
                        {
                            args = args.Replace("%PROJNAME%", proj.FileNameNoExt);
                            args = args.Replace("%PROJDIR%", proj.DirPath);
                            args = args.Replace("%PROJOUTFOLDER%", proj.OutputDir);

                            if (file != null)
                            {
                                args = args.Replace("%FILENAMENOEXT%", file.FileNameNoExt);
                                args = args.Replace("%FILEEXT%", file.FileNameNoExt);
                                args = args.Replace("%FILEDIR%", file.FileDir);
                            }
                        }
                    }

                    p.StartInfo.Arguments = args;
                }

                string dir = this.dirStr;
                if (string.IsNullOrEmpty(dir))
                {
                    if (proj != null)
                    {
                        if (proj.IsReady)
                        {
                            dir = dir.Replace("%PROJDIR%", proj.DirPath);
                            dir = dir.Replace("%PROJOUTFOLDER%", proj.OutputDir);

                            if (file != null)
                            {
                                dir = dir.Replace("%FILEDIR%", file.FileDir);
                            }
                        }
                    }

                    p.StartInfo.WorkingDirectory = dir;
                }

                if (p.Start())
                {
                    //
                }
                else
                {
                    MessageBox.Show(String.Format("Process '{0} {1}' failed to start.", cmd, args));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error during execution: " + ex.Message);
            }
            
        }
    }
}
