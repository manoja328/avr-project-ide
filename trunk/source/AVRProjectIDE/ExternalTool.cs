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

            if (text == null)
                text = "";

            if (cmd == null)
                cmd = "";

            if (args == null)
                args = "";

            if (dir == null)
                dir = "";

            this.mbtn.Text = text;

            this.cmdStr = cmd;
            this.argsStr = args;
            this.dirStr = dir;

            this.mbtn.Click += new EventHandler(mbtn_Click);
        }

        void mbtn_Click(object sender, EventArgs e)
        {
            try
            {
                Run(wind.CurrentFile, wind.CurrentProj);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error while running external tool: " + ex.Message);
            }
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
                            args = args.Replace("%PROJCHIP%", proj.Device);

                            if (file != null)
                            {
                                args = args.Replace("%FILENAMENOEXT%", file.FileNameNoExt);
                                args = args.Replace("%FILEEXT%", file.FileExt);
                                args = args.Replace("%FILEDIR%", file.FileDir);
                            }
                        }
                    }

                    ProjectBuilder.SetEnviroVarsForProc(p.StartInfo);
                    p.StartInfo.Arguments = args;
                }

                string dir = this.dirStr;
                if (string.IsNullOrEmpty(dir) == false)
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

        public static ToolStripMenuItem GetExternalToolsRoot(IDEWindow wind)
        {
            ToolStripMenuItem root = new ToolStripMenuItem("External Tools");

            root.Image = GraphicsResx.tool_icon_png;

            XmlDocument xDoc = new XmlDocument();

            if (File.Exists(SettingsManagement.AppDataPath + "ext_tools.xml") == false)
            {
                try
                {
                    File.WriteAllText(SettingsManagement.AppDataPath + "ext_tools.xml", Properties.Resources.ext_tools);
                    xDoc.Load(SettingsManagement.AppDataPath + "ext_tools.xml");
                }
                catch
                {
                    xDoc.LoadXml(Properties.Resources.ext_tools);
                }
            }
            else
            {
                try
                {
                    xDoc.Load(SettingsManagement.AppDataPath + "ext_tools.xml");
                }
                catch (XmlException ex)
                {
                    MessageBox.Show("Error while reading ext_tools.xml: " + ex.Message);
                    return root;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error while reading ext_tools.xml: " + ex.Message);
                    return root;
                }
            }

            try
            {
                XmlElement xDocEle = xDoc.DocumentElement;

                foreach (XmlElement xEle in xDocEle.GetElementsByTagName("Tool"))
                {
                    ExternalTool link = new ExternalTool(xEle.GetAttribute("text"), xEle.GetAttribute("cmd"), xEle.GetAttribute("args"), xEle.GetAttribute("dir"), wind);
                    link.mbtn.Image = GraphicsResx.tool_icon_png;
                    toolList.Add(link);
                    root.DropDownItems.Add(link.mbtn);
                }
            }
            catch (Exception ex)
            {
                ErrorReportWindow.Show(ex, "Error while creating external tool buttons");
            }

            return root;
        }
    }
}
