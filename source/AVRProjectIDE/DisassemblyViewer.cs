using System;
using System.Diagnostics;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using WeifenLuo.WinFormsUI.Docking;
using ScintillaNet;

namespace AVRProjectIDE
{
    public partial class DisassemblyViewer : DockContent
    {
        //[System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.Demand, Name = "FullTrust")]
        protected override void WndProc(ref Message m)
        {
            try
            {
                base.WndProc(ref m);
            }
            catch (Exception ex)
            {
                ErrorReportWindow.Show(ex, "Error In Disassembly Viewer");

            }
        }

        public string HelpText
        {
            get
            {
                return "-a, --archive-headers    Display archive header information" + Environment.NewLine +
                        "-f, --file-headers       Display the contents of the overall file header" + Environment.NewLine +
                        "-p, --private-headers    Display object format specific file header contents" + Environment.NewLine +
                        "-h, --[section-]headers  Display the contents of the section headers" + Environment.NewLine +
                        "-x, --all-headers        Display the contents of all headers" + Environment.NewLine +
                        "-d, --disassemble        Display assembler contents of executable sections" + Environment.NewLine +
                        "-D, --disassemble-all    Display assembler contents of all sections" + Environment.NewLine +
                        "-S, --source             Intermix source code with disassembly" + Environment.NewLine +
                        "-s, --full-contents      Display the full contents of all sections requested" + Environment.NewLine +
                        "-g, --debugging          Display debug information in object file" + Environment.NewLine +
                        "-e, --debugging-tags     Display debug information using ctags style" + Environment.NewLine +
                        "-G, --stabs              Display (in raw form) any STABS info in the file" + Environment.NewLine +
                        "-W, --dwarf              Display DWARF info in the file" + Environment.NewLine +
                        "-t, --syms               Display the contents of the symbol table(s)" + Environment.NewLine +
                        "-T, --dynamic-syms       Display the contents of the dynamic symbol table" + Environment.NewLine +
                        "-r, --reloc              Display the relocation entries in the file" + Environment.NewLine +
                        "-R, --dynamic-reloc      Display the dynamic relocation entries in the file" + Environment.NewLine +
                        "@<file>                  Read options from <file>" + Environment.NewLine +
                        "-v, --version            Display this program's version number" + Environment.NewLine +
                        "-i, --info               List object formats and architectures supported" + Environment.NewLine;
            }
        }

        public DisassemblyViewer()
        {
            InitializeComponent();

            string oldOpts = SettingsManagement.LastDisassemblyOptions;
            if (string.IsNullOrEmpty(oldOpts) == false)
                txtOptions.Text = oldOpts;
            else
                txtOptions.Text = "-h -D -S";

            SettingsManagement.SetScintSettings(scintilla1, false, true);

            mbtnInvisible.Visible = false;
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            if (IDEWindow.CurrentProject == null)
            {
                scintilla1.Text = "Error: Project Not Loaded";
                return;
            }

            if (IDEWindow.CurrentProject.IsReady == false)
            {
                scintilla1.Text = "Error: Project Not Loaded";
                return;
            }

            if (txtOptions.Text.Trim().Length == 0)
                txtOptions.Text = "-h -D -S";

            SettingsManagement.LastDisassemblyOptions = txtOptions.Text;

            string dirPath = IDEWindow.CurrentProject.DirPath + Path.DirectorySeparatorChar + IDEWindow.CurrentProject.OutputDir + Path.DirectorySeparatorChar;

            if (Program.MakeSurePathExists(dirPath) == false)
            {
                scintilla1.Text = "Error: Could not create output directory";
                return;
            }

            string elfPath = dirPath + IDEWindow.CurrentProject.FileNameNoExt + ".elf";

            if (File.Exists(elfPath) == false)
            {
                scintilla1.Text = "Error: ELF file not found at '" + elfPath + "'";
                return;
            }

            string fileNameAddon = txtOptions.Text.Replace(" -", ".").Replace("-", ".").Replace(" ", ".");
            while (fileNameAddon.Contains(".."))
                fileNameAddon = fileNameAddon.Replace("..", ".");

            fileNameAddon = fileNameAddon.Trim('.');

            string fileName = IDEWindow.CurrentProject.FileNameNoExt + ".disasm." + fileNameAddon + ".lst";
            string filePath = dirPath + fileName;

            if (File.Exists(filePath))
            {
                try
                {
                    File.Delete(filePath);
                }
                catch (Exception ex)
                {
                    scintilla1.Text = "Error: Could not delete existing file";
                    return;
                }
            }

            Process avrobjdump = new Process();
            ProjectBuilder.SetEnviroVarsForProc(avrobjdump.StartInfo);
            avrobjdump.StartInfo.FileName = "cmd";
            avrobjdump.StartInfo.Arguments = "/C avr-objdump " + txtOptions.Text.Trim() + " " + IDEWindow.CurrentProject.FileNameNoExt + ".elf > " + fileName;
            avrobjdump.StartInfo.WorkingDirectory = dirPath;
            avrobjdump.StartInfo.UseShellExecute = false;
            avrobjdump.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            avrobjdump.StartInfo.CreateNoWindow = true;

            try
            {
                avrobjdump.Start();
                avrobjdump.WaitForExit(10000);
            }
            catch (Exception ex)
            {
                scintilla1.Text = "Error running avr-objdump: " + ex.Message;
                return;
            }

            if (File.Exists(filePath) == false)
            {
                scintilla1.Text = "Error: LST file was not generated at '" + filePath + "'";
                return;
            }

            int oldCaret = scintilla1.CurrentPos;
            scintilla1.IsReadOnly = false;
            scintilla1.Text = File.ReadAllText(filePath);
            scintilla1.IsReadOnly = true;
            if (oldCaret >= 0 && oldCaret <= scintilla1.Text.Length)
                scintilla1.GoTo.Position(oldCaret);
            else if (oldCaret > scintilla1.Text.Length)
                scintilla1.GoTo.Position(scintilla1.Text.Length);
        }

        public void FindWindow()
        {
            scintilla1.FindReplace.ShowFind();

            string findWinTitleStr = " (Disassembly Viewer)";
            if (scintilla1.FindReplace.Window.Text.Contains(findWinTitleStr) == false)
                scintilla1.FindReplace.Window.Text += findWinTitleStr;
        }

        public void FindNext()
        {
            if (string.IsNullOrEmpty(scintilla1.FindReplace.LastFindString))
            {
                if (string.IsNullOrEmpty(scintilla1.Selection.Text))
                    FindWindow();
                else
                {
                    Range r = scintilla1.FindReplace.FindNext(scintilla1.Selection.Text, true);
                    if (r != null)
                        scintilla1.Selection.Range = r;
                }
            }
            else
            {
                Range r = scintilla1.FindReplace.FindNext(scintilla1.FindReplace.LastFindString, true);
                if (r != null)
                    scintilla1.Selection.Range = r;
            }
        }

        private void mbtnReplace_Click(object sender, EventArgs e)
        {
            return;
            EditorPanel.OnlyFindReplaceWindow = scintilla1.FindReplace.Window;

            scintilla1.FindReplace.ShowFind();
            EditorPanel.OnlyFindReplaceWindow = scintilla1.FindReplace.Window;

            if (scintilla1.FindReplace.Window.Text.Contains("Disassembly Viewer") == false)
                scintilla1.FindReplace.Window.Text += " for " + "Disassembly Viewer";
        }

        private void mbtnSelectAll_Click(object sender, EventArgs e)
        {
            scintilla1.Selection.SelectAll();
        }

        private void mbtnCopy_Click(object sender, EventArgs e)
        {
            scintilla1.Clipboard.Copy();
        }

        private void mbtnFind_Click(object sender, EventArgs e)
        {
            EditorPanel.OnlyFindReplaceWindow = scintilla1.FindReplace.Window;

            scintilla1.FindReplace.ShowFind();
            EditorPanel.OnlyFindReplaceWindow = scintilla1.FindReplace.Window;

            if (scintilla1.FindReplace.Window.Text.Contains("Disassembly Viewer") == false)
                scintilla1.FindReplace.Window.Text += " for " + "Disassembly Viewer";
        }

        private void btnHelp_Click(object sender, EventArgs e)
        {
            MessageBox.Show("avrobjdump help: " + Environment.NewLine + HelpText);
        }
    }
}
