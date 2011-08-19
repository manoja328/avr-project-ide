using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Diagnostics;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.IO.Ports;
using System.Collections.Specialized;

namespace AVRProjectIDE
{
    public class ProjectBuilder
    {
        #region Fields and Properties

        private static bool AvrToolsInstalled = false;

        private static bool reverseOutput;
        public static bool ReverseOutput
        {
            get { return reverseOutput; }
            set { reverseOutput = value; }
        }

        private Dictionary<string, ProjectFile> origFileList;
        private AVRProject project;
        private Dictionary<string, ProjectFile> workingFileList;
        private AVRProject workingProject;
        private TextBox outputTextbox;
        private ListView errorList;
        private ListView errorOnlyList;
        private BackgroundWorker worker;
        private BackgroundWorker makefileWorker;

        private int hasError = 0;

        #endregion

        public int HasError
        {
            get { return hasError; }
            set { hasError = value; }
        }

        #region Event Handler and Delegate

        public event EventHandler DoneWork;
        public delegate void EventHandler(bool success);

        #endregion

        public ProjectBuilder(AVRProject project, TextBox outputTextbox, ListView errorList, ListView errorOnlyList)
        {
            this.project = project;
            this.origFileList = project.FileList;
            this.outputTextbox = outputTextbox;
            this.errorList = errorList;
            this.errorOnlyList = errorOnlyList;

            this.worker = new BackgroundWorker();
            this.worker.DoWork += new DoWorkEventHandler(worker_DoWork);
            this.worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(worker_RunWorkerCompleted);

            this.makefileWorker = new BackgroundWorker();
            this.makefileWorker.DoWork += new DoWorkEventHandler(makefileWorker_DoWork);
            this.makefileWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(makefileWorker_RunWorkerCompleted);

            PrepProject();
        }

        

        #region Background Worker

        void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            ListViewModify(errorList, new ListViewItem(new string[] { "", "", "", ((bool)e.Result) ? "OK" : "Failed","# of Errors: " + hasError.ToString("0"), }), ListViewChangeMode.AddToTop);
            ListViewModify(errorOnlyList, new ListViewItem(new string[] { "", "", "", ((bool)e.Result) ? "OK" : "Failed", "# of Errors: " + hasError.ToString("0"), }), ListViewChangeMode.AddToTop);
            DoneWork((bool)e.Result);
        }

        void makefileWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            throw new NotImplementedException();
        }

        void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            e.Result = Build();
        }

        void makefileWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            if (File.Exists(workingProject.DirPath + Path.DirectorySeparatorChar + "Makefile") == false && File.Exists(workingProject.DirPath + Path.DirectorySeparatorChar + "makefile") == false)
            {
                TextBoxModify(outputTextbox, "####Error: Makefile not found in " + workingProject.DirPath, TextBoxChangeMode.PrependNewLine);
                return;
            }

            string batchPath = workingProject.DirPath + Path.DirectorySeparatorChar + "makecmds.bat";

            try
            {
                StreamWriter writer = new StreamWriter(batchPath);
                writer.WriteLine("REM Makefile Dry Run");
                writer.Close();
            }
            catch (Exception ex)
            {
                TextBoxModify(outputTextbox, "####Error: unable to create dry run batch file, " + ex.Message, TextBoxChangeMode.PrependNewLine);
                return;
            }

            ProcessStartInfo psi = new ProcessStartInfo("cmd", "/C make --dry-run >> makecmds.bat");
            SetEnviroVarsForProc(psi);
            psi.WorkingDirectory = workingProject.DirPath;
            psi.UseShellExecute = false;
            psi.RedirectStandardError = true;
            psi.RedirectStandardOutput = true;
            psi.RedirectStandardInput = true;
            psi.WindowStyle = ProcessWindowStyle.Hidden;
            psi.CreateNoWindow = true;
            Process makedryrun = new Process();
            makedryrun.StartInfo = psi;
            try
            {
                if (makedryrun.Start())
                {
                    StreamReader stderr = makedryrun.StandardError;
                    ReadErrAndWarnings(stderr, true);
                    StreamReader stdout = makedryrun.StandardOutput;
                    ReadErrAndWarnings(stdout, true);
                    makedryrun.WaitForExit(10000);
                }
                else
                {
                    TextBoxModify(outputTextbox, "####Error: unable to dry run make", TextBoxChangeMode.PrependNewLine);
                }
            }
            catch (Exception ex)
            {
                TextBoxModify(outputTextbox, "####Error: unable to dry run make, " + ex.Message, TextBoxChangeMode.PrependNewLine);
                return;
            }

            if (File.Exists(batchPath) == false)
            {
                TextBoxModify(outputTextbox, "####Error: unable to create dry run batch file at " + batchPath, TextBoxChangeMode.PrependNewLine);
                return;
            }

            Queue<string> commandList = new Queue<string>();
            try
            {
                StreamReader reader = new StreamReader(batchPath);
                string line = reader.ReadLine();
                while (line != null)
                {
                    if (line.Trim().StartsWith("REM ") == false)
                        commandList.Enqueue(line);

                    line = reader.ReadLine();
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                TextBoxModify(outputTextbox, "####Error: unable to read dry run, " + ex.Message, TextBoxChangeMode.PrependNewLine);
                return;
            }

            while (commandList.Count > 0)
            {
                string command = commandList.Dequeue().Trim();
                string exe = command;
                string args = "";
                if (command.Contains(' '))
                {
                    exe = command.Substring(0, command.IndexOf(' '));
                    args = command.Substring(command.IndexOf(' ') + 1);
                }
                psi = new ProcessStartInfo(exe, args);
                ProjectBuilder.SetEnviroVarsForProc(psi);
                psi.WorkingDirectory = workingProject.DirPath;
                psi.UseShellExecute = false;
                psi.RedirectStandardError = true;
                psi.RedirectStandardOutput = true;
                psi.RedirectStandardInput = true;
                psi.WindowStyle = ProcessWindowStyle.Hidden;
                psi.CreateNoWindow = true;
                makedryrun = new Process();
                makedryrun.StartInfo = psi;
                TextBoxModify(outputTextbox, "Execute: " + command, TextBoxChangeMode.PrependNewLine);
                try
                {
                    if (makedryrun.Start())
                    {
                        StreamReader stderr = makedryrun.StandardError;
                        ReadErrAndWarnings(stderr, false);
                        StreamReader stdout = makedryrun.StandardOutput;
                        ReadErrAndWarnings(stdout, false);
                        makedryrun.WaitForExit(10000);
                    }
                    else
                    {
                        TextBoxModify(outputTextbox, "####Error: unable to execute command", TextBoxChangeMode.PrependNewLine);
                    }
                }
                catch (Exception ex)
                {
                    TextBoxModify(outputTextbox, "####Error: unable to execute command, " + ex.Message, TextBoxChangeMode.PrependNewLine);
                }

            }
        }

        #endregion

        #region Form Control Modification

        private delegate void TextBoxCallback(TextBox box, string text, TextBoxChangeMode mode);

        private void TextBoxModify(TextBox box, string text, TextBoxChangeMode mode)
        {
            if (box.InvokeRequired)
            {
                box.Invoke(new TextBoxCallback(TextBoxModify), new object[] { box, text, mode, });
            }
            else
            {
                if (ReverseOutput)
                {
                    if (mode == TextBoxChangeMode.Append)
                        mode = TextBoxChangeMode.Prepend;
                    else if (mode == TextBoxChangeMode.AppendNewLine)
                        mode = TextBoxChangeMode.PrependNewLine;
                    else if (mode == TextBoxChangeMode.Prepend)
                        mode = TextBoxChangeMode.Append;
                    else if (mode == TextBoxChangeMode.PrependNewLine)
                        mode = TextBoxChangeMode.AppendNewLine;
                }

                if (mode == TextBoxChangeMode.Append)
                {
                    box.Text += text;
                    box.SelectionStart = box.Text.Length;
                    box.SelectionLength = 0;
                    box.ScrollToCaret();
                }
                else if (mode == TextBoxChangeMode.AppendNewLine)
                {
                    box.Text += Environment.NewLine + text;
                    box.SelectionStart = box.Text.Length;
                    box.SelectionLength = 0;
                    box.ScrollToCaret();
                }
                else if (mode == TextBoxChangeMode.Prepend)
                {
                    box.Text = text + box.Text;
                    box.SelectionStart = 0;
                    box.SelectionLength = 0;
                    box.ScrollToCaret();
                }
                else if (mode == TextBoxChangeMode.PrependNewLine)
                {
                    box.Text = text + Environment.NewLine + box.Text;
                    box.SelectionStart = 0;
                    box.SelectionLength = 0;
                    box.ScrollToCaret();
                }
                else if (mode == TextBoxChangeMode.Set)
                    box.Text = text;
                else if (mode == TextBoxChangeMode.SetNewLine)
                    box.Text = text + Environment.NewLine;
            }
        }

        private delegate void ListViewCallback(ListView box, ListViewItem item, ListViewChangeMode mode);

        private void ListViewModify(ListView box, ListViewItem item, ListViewChangeMode mode)
        {
            if (box.InvokeRequired)
            {
                box.Invoke(new ListViewCallback(ListViewModify), new object[] { box, item, mode, });
            }
            else
            {
                if (ReverseOutput)
                {
                    if (mode == ListViewChangeMode.AddToTop)
                        mode = ListViewChangeMode.AddToBottom;
                    else if (mode == ListViewChangeMode.AddToBottom)
                        mode = ListViewChangeMode.AddToTop;
                }

                if (mode == ListViewChangeMode.AddToBottom && item != null)
                    box.Items.Add((ListViewItem)item.Clone());
                else if (mode == ListViewChangeMode.AddToTop && item != null)
                    box.Items.Insert(0, (ListViewItem)item.Clone());
                else if (mode == ListViewChangeMode.Clear)
                    box.Items.Clear();
            }
        }

        #endregion

        #region Public Methods

        public void StartBuild()
        {
            // don't do anything if a build is still in progress
            if (worker.IsBusy || makefileWorker.IsBusy)
                return;

            hasError = 0;

            TextBoxModify(outputTextbox, "Build Starting...", TextBoxChangeMode.Set);
            ListViewModify(errorList, null, ListViewChangeMode.Clear);
            ListViewModify(errorOnlyList, null, ListViewChangeMode.Clear);

            PrepProject();

            // clear the relevent form elements
            TextBoxModify(outputTextbox, "", TextBoxChangeMode.Set);


            // start the build
            worker.RunWorkerAsync();
        }

        public void StartMake()
        {
            // don't do anything if a build is still in progress
            if (worker.IsBusy || makefileWorker.IsBusy)
                return;

            PrepProject();

            // start the build
            makefileWorker.RunWorkerAsync();
        }

        #endregion

        #region Private Build Methods

        private bool Build()
        {
            bool result = true;

            workingProject.IncludeDirList.Add(workingProject.DirPath);

            // a space delimited string keeps all the object file names to be used by the linker
            string objFiles = "";
            string avr_ar_targets = "";

            string mainFileName = "";

            // compile all the source files in the project
            foreach (ProjectFile file in workingFileList.Values)
            {
                if (file.Exists && file.ToCompile && (file.FileExt == "c" || file.FileExt == "cpp" || file.FileExt == "cxx" || file.FileExt == "s"))
                {
                    bool fileHasMain = IsFileMain(file);
                    bool objRes = Compile(file);
                    result &= objRes;
                    if (objRes)
                    {
                        objFiles += file.FileNameNoExt + ".o ";
                        if (fileHasMain == false)
                        {
                            avr_ar_targets += file.FileNameNoExt + ".o ";
                            //objFileLoc.Add(outputAbsPath + Path.DirectorySeparatorChar + file.FileNameNoExt + ".o");
                        }
                        else
                        {
                            mainFileName = file.FileNameNoExt + ".o ";
                        }
                    }
                }
            }

            if (project.HasArduino)
            {
                // compile arduino sketch
                bool ardRes = HandleArduino(ref objFiles, ref avr_ar_targets);
                result &= ardRes;
                if (ardRes)
                {
                    mainFileName = "arduino_temp_main.o";
                }
            }

            // all object files have been made, link them together into a .elf file
            bool elfRes = false;

            if (string.IsNullOrEmpty(mainFileName))
            {
                elfRes = CreateELF(objFiles, false);
            }
            else
            {
                bool archiveRes = CreateLib(avr_ar_targets);
                if (archiveRes)
                {
                    workingProject.LinkerOptions += " " + "lib" + workingProject.SafeFileNameNoExt + ".a";
                }

                elfRes = CreateELF(objFiles, false);
            }

            result &= elfRes;

            // delete all the junk files
            CleanOD(objFiles + " lib" + workingProject.SafeFileNameNoExt + ".a");

            // if successful, generate hex to be burnt
            if (elfRes)
            {
                result &= CreateHex();
                CreateEEP();
                

                //CreateLST();

                ReadSize();
            }

            return result;
        }

        private bool CreateLib(string avr_ar_targets)
        {
            string outputAbsPath = workingProject.DirPath + Path.DirectorySeparatorChar + workingProject.OutputDir;

            // create the directory
            if (Program.MakeSurePathExists(outputAbsPath) == false)
                return false;

            string libFilePath = outputAbsPath + Path.DirectorySeparatorChar + "lib" + workingProject.SafeFileNameNoExt + ".a";

            // delete object file if existing
            if (File.Exists(libFilePath))
            {
                try
                {
                    File.Delete(libFilePath);
                }
                catch (Exception ex)
                {
                    TextBoxModify(outputTextbox, "####Error: library file could not be deleted at " + libFilePath + ", " + ex.Message, TextBoxChangeMode.PrependNewLine);
                }
            }

            // construct options and arguments for avr-gcc

            string args = "rcs lib" + workingProject.SafeFileNameNoExt + ".a " + avr_ar_targets;
            TextBoxModify(outputTextbox, "Execute: avr-ar " + args, TextBoxChangeMode.PrependNewLine);
            ProcessStartInfo psi = new ProcessStartInfo("avr-ar", args);
            ProjectBuilder.SetEnviroVarsForProc(psi);
            psi.WorkingDirectory = outputAbsPath + Path.DirectorySeparatorChar;
            psi.UseShellExecute = false;
            psi.RedirectStandardError = true;
            psi.RedirectStandardOutput = true;
            psi.RedirectStandardInput = true;
            psi.WindowStyle = ProcessWindowStyle.Hidden;
            psi.CreateNoWindow = true;
            Process avrar = new Process();
            avrar.StartInfo = psi;
            try
            {
                if (avrar.Start())
                {
                    StreamReader stderr = avrar.StandardError;
                    ReadErrAndWarnings(stderr, true);
                    StreamReader stdout = avrar.StandardOutput;
                    ReadErrAndWarnings(stdout, true);
                    avrar.WaitForExit(10000);
                }
                else
                {
                    TextBoxModify(outputTextbox, "####Error: unable to start avr-ar", TextBoxChangeMode.PrependNewLine);
                    return false;
                }
            }
            catch (Exception ex)
            {
                TextBoxModify(outputTextbox, "####Error: unable to start avr-ar," + ex.Message, TextBoxChangeMode.PrependNewLine);
                return false;
            }

            if (File.Exists(libFilePath))
            {
                return true;
            }
            else
            {
                TextBoxModify(outputTextbox, "####Error: library file not created at " + libFilePath, TextBoxChangeMode.PrependNewLine);
                return false;
            }
        }

        private bool IsFileMain(ProjectFile file)
        {
            string fileContents = GetFileContents(file);

            try
            {
                fileContents = CodePreProcess.StripComments(fileContents);
            }
            catch (Exception ex)
            {
                TextBoxModify(outputTextbox, "####Error: could not strip comments from " + file.FileName + ", " + ex.Message, TextBoxChangeMode.PrependNewLine);
            }

            fileContents = fileContents.Replace('\r', ' ').Replace('\n', ' ');

            string re1 = "((?:[a-z][a-z0-9_]*))";	// return type
            string re2 = "(\\s+)";	// White Space
            string re3 = "(main)";	// main
            string re4 = "(\\s*)";	// optional white space
            string re5 = "(\\()";	// (
            string re6 = "([^)]*)";	// anything goes
            string re7 = "(\\))";	// )
            string re8 = "(\\s*)";	// optional white space
            string re9 = "(\\{)";	// {

            Regex r = new Regex(re1 + re2 + re3 + re4 + re5 + re6 + re7 + re8 + re9, RegexOptions.IgnoreCase | RegexOptions.Multiline);
            Match m = r.Match(fileContents);
            if (m.Success)
            {
                return true;
            }

            return false;
        }

        public bool Compile(ProjectFile file)
        {
            string outputAbsPath = workingProject.DirPath + Path.DirectorySeparatorChar + workingProject.OutputDir;

            // create the directory
            if (Program.MakeSurePathExists(outputAbsPath) == false)
                return false;

            string objectFileAbsPath = outputAbsPath + Path.DirectorySeparatorChar + file.FileNameNoExt + ".o";

            // delete object file if existing
            if (File.Exists(objectFileAbsPath))
            {
                try
                {
                    File.Delete(objectFileAbsPath);
                }
                catch (Exception ex)
                {
                    TextBoxModify(outputTextbox, "####Error: object file could not be deleted at " + objectFileAbsPath + ", " + ex.Message, TextBoxChangeMode.PrependNewLine);
                }
            }

            // construct options and arguments for avr-gcc

            string args = "";
            foreach (string path in workingProject.IncludeDirList)
            {
                if(string.IsNullOrEmpty(path) == false)
                    args += "-I\"" + path + "\" ";
            }
            args += "-I\"" + file.FileDir + "\" ";

            string checklist = "";
            
            if (project.PackStructs)
                checklist += "-fpack-struct ";

            if (project.ShortEnums)
                checklist += "-fshort-enums ";

            if (project.UnsignedChars)
                checklist += "-funsigned-char ";

            if (project.UnsignedBitfields)
                checklist += "-funsigned-bitfields ";

            if (project.FunctionSections)
                checklist += "-ffunction-sections ";

            if (project.DataSections)
                checklist += "-fdata-sections ";

            string asmflags = "";

            if (file.FileExt == "s")
                asmflags += "-x assembler-with-cpp -Wa,-gdwarf2";

            string fileTypeOptions = "";
            if (file.FileExt == "c")
                fileTypeOptions = workingProject.OtherOptionsForC;
            else if (file.FileExt == "cpp" || file.FileExt == "cxx")
                fileTypeOptions = workingProject.OtherOptionsForCPP;
            else if (file.FileExt == "s")
                fileTypeOptions = workingProject.OtherOptionsForS;

            string clkStr = " ";
            if (project.ClockFreq != 0)
                clkStr = String.Format(" -DF_CPU={0:0}UL ", Math.Round(project.ClockFreq));

            args += String.Format(" -mmcu={0}{1}{2} {3} {4} -MD -MP -MT {5}.o {6} -c {7} {8} \"{9}\"",
                workingProject.Device.ToLowerInvariant(),
                clkStr,
                workingProject.Optimization,
                checklist,
                workingProject.OtherOptions,
                file.FileNameNoExt,
                asmflags,
                file.Options,
                fileTypeOptions,
                file.FileAbsPath.Replace('\\', '/')
            );

            ProcessStartInfo psi = null;

            // c++ uses avr-g++ while c uses avr-gcc, duh
            if (file.FileExt == "cpp" || file.FileExt == "cxx")
            {
                TextBoxModify(outputTextbox, "Execute: avr-g++ " + args, TextBoxChangeMode.PrependNewLine);
                psi = new ProcessStartInfo("avr-g++", args);
            }
            else
            {
                TextBoxModify(outputTextbox, "Execute: avr-gcc " + args, TextBoxChangeMode.PrependNewLine);
                psi = new ProcessStartInfo("avr-gcc", args);
            }

            ProjectBuilder.SetEnviroVarsForProc(psi);
            psi.WorkingDirectory = outputAbsPath + Path.DirectorySeparatorChar;
            psi.UseShellExecute = false;
            psi.RedirectStandardError = true;
            psi.RedirectStandardOutput = true;
            psi.RedirectStandardInput = true;
            psi.WindowStyle = ProcessWindowStyle.Hidden;
            psi.CreateNoWindow = true;
            Process avrgcc = new Process();
            avrgcc.StartInfo = psi;
            try
            {
                if (avrgcc.Start())
                {
                    StreamReader stderr = avrgcc.StandardError;
                    ReadErrAndWarnings(stderr, false);
                    StreamReader stdout = avrgcc.StandardOutput;
                    ReadErrAndWarnings(stdout, false);
                    avrgcc.WaitForExit(10000);
                }
                else
                {
                    TextBoxModify(outputTextbox, "####Error: unable to start avr-gcc", TextBoxChangeMode.PrependNewLine);
                    return false;
                }
            }
            catch (Exception ex)
            {
                TextBoxModify(outputTextbox, "####Error: unable to start avr-gcc, " + ex.Message, TextBoxChangeMode.PrependNewLine);
                return false;
            }

            if (File.Exists(objectFileAbsPath))
            {
                return true;
            }
            else
            {
                TextBoxModify(outputTextbox, "####Error: object file not created at " + objectFileAbsPath, TextBoxChangeMode.PrependNewLine);
                return false;
            }
        }

        private bool CreateELF(string OBJECTS, bool suppressErrors)
        {
            string outputAbsPath = workingProject.DirPath + Path.DirectorySeparatorChar + workingProject.OutputDir;

            // make sure folder exists, delete existing files

            if (Program.MakeSurePathExists(outputAbsPath) == false)
                return false;

            string elfFileAbsPath = outputAbsPath + Path.DirectorySeparatorChar + workingProject.SafeFileNameNoExt + ".elf";

            if (File.Exists(elfFileAbsPath))
            {
                try
                {
                    File.Delete(elfFileAbsPath);
                }
                catch (Exception ex)
                {
                    TextBoxModify(outputTextbox, "####Error: ELF file could not be deleted at " + elfFileAbsPath + ", " + ex.Message, TextBoxChangeMode.PrependNewLine);
                }
            }

            string mapFileAbsPath = outputAbsPath + Path.DirectorySeparatorChar + workingProject.SafeFileNameNoExt + ".map";

            if (File.Exists(mapFileAbsPath))
            {
                try
                {
                    File.Delete(mapFileAbsPath);
                }
                catch (Exception ex)
                {
                    TextBoxModify(outputTextbox, "####Error: MAP file could not be deleted at " + mapFileAbsPath + ", " + ex.Message, TextBoxChangeMode.PrependNewLine);
                }
            }

            // construct options and arguments

            string LDSFLAGS = "-mmcu=" + workingProject.Device.ToLowerInvariant() + " ";
            foreach (MemorySegment seg in workingProject.MemorySegList.Values)
            {
                int addr = (int)seg.Addr;
                if (seg.Type.ToLowerInvariant() == "sram")
                {
                    addr += 0x800000;
                }
                else if (seg.Type.ToLowerInvariant() == "eeprom")
                {
                    addr += 0x810000;
                }
                LDSFLAGS += "-Wl,-section-start=" + seg.Name + "=0x" + Convert.ToString(addr, 16) + " ";
            }

            if (workingProject.UseInitStack)
            {
                LDSFLAGS += "-Wl,--defsym=__stack=0x" + Convert.ToString(workingProject.InitStackAddr, 16) + " ";
            }

            LDSFLAGS += "-Wl,-Map=" + workingProject.SafeFileNameNoExt + ".map ";
            LDSFLAGS += "-Wl,--gc-sections ";
            LDSFLAGS += workingProject.Optimization + " ";
            LDSFLAGS += workingProject.LinkerOptions;

            string LINKONLYOBJECTS = "";
            foreach (string obj in workingProject.LinkObjList)
            {
                if (string.IsNullOrEmpty(obj) == false)
                {
                    LINKONLYOBJECTS += "\"" + obj + "\" ";
                }
            }
            
            string LIBS = "";
            foreach (string obj in workingProject.LinkLibList)
            {
                if (string.IsNullOrEmpty(obj) == false)
                {
                    if (obj.StartsWith("lib"))
                    {
                        LIBS += "-l" + obj.Substring(3).TrimEnd('a').TrimEnd('.') + " ";
                    }
                    else
                    {
                        string libName = Path.GetFileNameWithoutExtension(obj);

                        if (libName.StartsWith("lib"))
                            libName = libName.Substring(3);

                        string libPath = Path.GetDirectoryName(obj);

                        LIBS += "-l" + libName + " ";

                        if (workingProject.LibraryDirList.Contains(libPath) == false)
                            workingProject.LibraryDirList.Add(libPath);
                    }
                }
            }

            string LIBDIRS = "";
            foreach (string obj in workingProject.LibraryDirList)
            {
                if (string.IsNullOrEmpty(obj) == false)
                {
                    LIBDIRS += "-L\"" + obj + "\" ";
                }
            }

            string args = String.Format("{0} {1} {2} {3} {4} -o {5}.elf",
                LDSFLAGS.Trim(),
                OBJECTS.Trim(),
                LINKONLYOBJECTS.Trim(),
                LIBDIRS.Trim(),
                LIBS.Trim(),
                workingProject.SafeFileNameNoExt
            );

            TextBoxModify(outputTextbox, "Execute: avr-gcc " + args, TextBoxChangeMode.PrependNewLine);

            // link object files together to get .elf file

            ProcessStartInfo psi = new ProcessStartInfo("avr-gcc", args);
            ProjectBuilder.SetEnviroVarsForProc(psi);
            psi.WorkingDirectory = outputAbsPath + Path.DirectorySeparatorChar;
            psi.UseShellExecute = false;
            psi.RedirectStandardError = true;
            psi.RedirectStandardOutput = true;
            psi.RedirectStandardInput = true;
            psi.WindowStyle = ProcessWindowStyle.Hidden;
            psi.CreateNoWindow = true;
            Process avrgcc = new Process();
            avrgcc.StartInfo = psi;
            try
            {
                if (avrgcc.Start())
                {

                    StreamReader stderr = avrgcc.StandardError;
                    if (suppressErrors == false)
                        ReadErrAndWarnings(stderr, true);
                    StreamReader stdout = avrgcc.StandardOutput;
                    if (suppressErrors == false)
                        ReadErrAndWarnings(stdout, true);
                    
                    //TextBoxModify(outputTextbox, stderr.ReadToEnd(), TextBoxChangeMode.PrependNewLine);
                    //TextBoxModify(outputTextbox, stdout.ReadToEnd(), TextBoxChangeMode.PrependNewLine);

                    avrgcc.WaitForExit(10000);
                }
                else
                {
                    TextBoxModify(outputTextbox, "####Error: unable to start avr-gcc", TextBoxChangeMode.PrependNewLine);
                    return false;
                }
            }
            catch (Exception ex)
            {
                TextBoxModify(outputTextbox, "####Error: unable to start avr-gcc, " + ex.Message, TextBoxChangeMode.PrependNewLine);
                return false;
            }

            if (File.Exists(elfFileAbsPath))
            {
                if (File.Exists(mapFileAbsPath) == false)
                {
                    if (suppressErrors == false)
                        TextBoxModify(outputTextbox, "####Error: MAP file not created at " + elfFileAbsPath, TextBoxChangeMode.PrependNewLine);
                }

                return true;
            }
            else
            {
                if (suppressErrors == false)
                    TextBoxModify(outputTextbox, "####Error: ELF file not created at " + elfFileAbsPath, TextBoxChangeMode.PrependNewLine);
                return false;
            }
        }

        private void CleanOD(string OBJECTS)
        {
            string outputAbsPath = workingProject.DirPath + Path.DirectorySeparatorChar + workingProject.OutputDir;

            // .d files are also generated by avr-gcc, so delete those too
            string args = OBJECTS + " " + (OBJECTS + " ").Replace(".o ", ".d ");

            List<string> deleted = new List<string>();

            TextBoxModify(outputTextbox, "Deleting: " + args, TextBoxChangeMode.PrependNewLine);

            string[] fList = args.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string file in fList)
            {
                if (file != null && string.IsNullOrEmpty(file.Trim()) == false)
                {
                    if (File.Exists(workingProject.DirPath + Path.DirectorySeparatorChar + file))
                    {
                        try
                        {
                            File.Delete(workingProject.DirPath + Path.DirectorySeparatorChar + file);
                            deleted.Add(file);
                        }
                        catch (Exception ex)
                        {
                            TextBoxModify(outputTextbox, "Error Deleting '" + args + "', " + ex.Message, TextBoxChangeMode.PrependNewLine);
                        }
                    }

                    if (File.Exists(outputAbsPath + Path.DirectorySeparatorChar + file))
                    {
                        try
                        {
                            File.Delete(outputAbsPath + Path.DirectorySeparatorChar + file);
                            deleted.Add(file);
                        }
                        catch (Exception ex)
                        {
                            TextBoxModify(outputTextbox, "Error Deleting '" + args + "', " + ex.Message, TextBoxChangeMode.PrependNewLine);
                        }
                    }
                }
            }

            string deletedStr = "";
            foreach (string d in deleted)
            {
                deletedStr += "'" + d + "', ";
            }

            if (string.IsNullOrEmpty(deletedStr))                   
                TextBoxModify(outputTextbox, "Deleted Nothing", TextBoxChangeMode.PrependNewLine);
            else
                TextBoxModify(outputTextbox, "Deleted: " + deletedStr, TextBoxChangeMode.PrependNewLine);

            return;

            TextBoxModify(outputTextbox, "Execute: del " + args, TextBoxChangeMode.PrependNewLine);

            ProcessStartInfo psi = new ProcessStartInfo("del", args);
            psi.WorkingDirectory = outputAbsPath + Path.DirectorySeparatorChar;
            psi.UseShellExecute = false;
            psi.RedirectStandardError = true;
            psi.RedirectStandardOutput = true;
            psi.RedirectStandardInput = true;
            psi.WindowStyle = ProcessWindowStyle.Hidden;
            psi.CreateNoWindow = true;
            Process rm = new Process();
            rm.StartInfo = psi;
            try
            {
                if (rm.Start())
                {
                    rm.WaitForExit(10000);
                }
                else
                {
                    TextBoxModify(outputTextbox, "####Error: unable to clean .o and .d files", TextBoxChangeMode.PrependNewLine);
                }
            }
            catch (Exception ex)
            {
                TextBoxModify(outputTextbox, "####Error: unable to clean .o and .d files, " + ex.Message, TextBoxChangeMode.PrependNewLine);
            }
        }

        private bool CreateHex()
        {
            string outputAbsPath = workingProject.DirPath + Path.DirectorySeparatorChar + workingProject.OutputDir;

            if (Program.MakeSurePathExists(outputAbsPath) == false)
                return false;

            string hexFileAbsPath = outputAbsPath + Path.DirectorySeparatorChar + workingProject.SafeFileNameNoExt + ".hex";

            if (File.Exists(hexFileAbsPath))
            {
                try
                {
                    File.Delete(hexFileAbsPath);
                }
                catch (Exception ex)
                {
                    TextBoxModify(outputTextbox, "####Error: HEX file could not be deleted at " + hexFileAbsPath + ", " + ex.Message, TextBoxChangeMode.PrependNewLine);
                }
            }

            string HEX_FLASH_FLAGS = "-R .eeprom -R .fuse -R .lock -R .signature ";
            string args = HEX_FLASH_FLAGS + "-O ihex " + workingProject.SafeFileNameNoExt + ".elf " + workingProject.SafeFileNameNoExt + ".hex";

            TextBoxModify(outputTextbox, "Execute: avr-objcopy " + args, TextBoxChangeMode.PrependNewLine);

            ProcessStartInfo psi = new ProcessStartInfo("avr-objcopy", args);
            ProjectBuilder.SetEnviroVarsForProc(psi);
            psi.WorkingDirectory = outputAbsPath + Path.DirectorySeparatorChar;
            psi.UseShellExecute = false;
            psi.RedirectStandardError = true;
            psi.RedirectStandardOutput = true;
            psi.RedirectStandardInput = true;
            psi.WindowStyle = ProcessWindowStyle.Hidden;
            psi.CreateNoWindow = true;
            Process avrobjcopy = new Process();
            avrobjcopy.StartInfo = psi;
            try
            {
                if (avrobjcopy.Start())
                {
                    StreamReader stderr = avrobjcopy.StandardError;
                    ReadErrAndWarnings(stderr, true);
                    StreamReader stdout = avrobjcopy.StandardOutput;
                    ReadErrAndWarnings(stdout, true);
                    avrobjcopy.WaitForExit(10000);
                }
                else
                {
                    TextBoxModify(outputTextbox, "####Error: unable to start avr-objcopy", TextBoxChangeMode.PrependNewLine);
                    return false;
                }
            }
            catch (Exception ex)
            {
                TextBoxModify(outputTextbox, "####Error: unable to start avr-objcopy, " + ex.Message, TextBoxChangeMode.PrependNewLine);
                return false;
            }

            if (File.Exists(hexFileAbsPath))
            {
                return true;
            }
            else
            {
                TextBoxModify(outputTextbox, "####Error: HEX file not created at " + hexFileAbsPath, TextBoxChangeMode.PrependNewLine);
                return false;
            }
        }

        private bool CreateEEP()
        {
            string outputAbsPath = workingProject.DirPath + Path.DirectorySeparatorChar + workingProject.OutputDir;

            if (Program.MakeSurePathExists(outputAbsPath) == false)
                return false;

            string eepFileAbsPath = outputAbsPath + Path.DirectorySeparatorChar + workingProject.SafeFileNameNoExt + ".eep";

            if (File.Exists(eepFileAbsPath))
            {
                try
                {
                    File.Delete(eepFileAbsPath);
                }
                catch (Exception ex)
                {
                    TextBoxModify(outputTextbox, "####Error: EEP file could not be deleted at " + eepFileAbsPath + ", " + ex.Message, TextBoxChangeMode.PrependNewLine);
                }
            }

            string HEX_EEPROM_FLAGS = "-j .eeprom --set-section-flags=.eeprom=\"alloc,load\" --change-section-lma .eeprom=0 --no-change-warnings ";

            foreach (MemorySegment seg in workingProject.MemorySegList.Values)
            {
                if (seg.Type == "eeprom")
                    HEX_EEPROM_FLAGS += "--change-section-lma " + seg.Name + "=0x" + Convert.ToString(seg.Addr, 16) + " ";
            }

            string args = HEX_EEPROM_FLAGS + " -O ihex " + workingProject.SafeFileNameNoExt + ".elf " + workingProject.SafeFileNameNoExt + ".eep";

            TextBoxModify(outputTextbox, "Execute: avr-objcopy " + args, TextBoxChangeMode.PrependNewLine);

            ProcessStartInfo psi = new ProcessStartInfo("avr-objcopy", args);
            SetEnviroVarsForProc(psi);
            psi.WorkingDirectory = outputAbsPath + Path.DirectorySeparatorChar;
            psi.UseShellExecute = false;
            psi.RedirectStandardError = true;
            psi.RedirectStandardOutput = true;
            psi.RedirectStandardInput = true;
            psi.WindowStyle = ProcessWindowStyle.Hidden;
            psi.CreateNoWindow = true;
            Process avrobjcopy = new Process();
            avrobjcopy.StartInfo = psi;
            try
            {
                if (avrobjcopy.Start())
                {
                    StreamReader stderr = avrobjcopy.StandardError;
                    ReadErrAndWarnings(stderr, true);
                    StreamReader stdout = avrobjcopy.StandardOutput;
                    ReadErrAndWarnings(stdout, true);
                    avrobjcopy.WaitForExit(10000);
                }
                else
                {
                    TextBoxModify(outputTextbox, "####Error: unable to start avr-objcopy", TextBoxChangeMode.PrependNewLine);
                    return false;
                }
            }
            catch (Exception ex)
            {
                TextBoxModify(outputTextbox, "####Error: unable to start avr-objcopy, " + ex.Message, TextBoxChangeMode.PrependNewLine);
                return false;
            }

            if (File.Exists(eepFileAbsPath))
            {
                return true;
            }
            else
            {
                TextBoxModify(outputTextbox, "####Error: EEP file not created at " + eepFileAbsPath, TextBoxChangeMode.PrependNewLine);
                return false;
            }
        }

        private bool CreateLST()
        {
            string outputAbsPath = workingProject.DirPath + Path.DirectorySeparatorChar + workingProject.OutputDir;

            if (Program.MakeSurePathExists(outputAbsPath) == false)
                return false;

            string lstFileAbsPath = outputAbsPath + Path.DirectorySeparatorChar + workingProject.SafeFileNameNoExt + ".lst";

            if (File.Exists(lstFileAbsPath))
            {
                try
                {
                    File.Delete(lstFileAbsPath);
                }
                catch (Exception ex)
                {
                    TextBoxModify(outputTextbox, "####Error: LSS file could not be deleted at " + lstFileAbsPath + ", " + ex.Message, TextBoxChangeMode.PrependNewLine);
                }
            }

            string args = "-h -S " + workingProject.SafeFileNameNoExt + ".elf > " + workingProject.SafeFileNameNoExt + ".lst";

            TextBoxModify(outputTextbox, "Execute: avr-objdump " + args, TextBoxChangeMode.PrependNewLine);

            string allText = "";

            ProcessStartInfo psi = new ProcessStartInfo("cmd", "/C avr-objdump " + args);
            SetEnviroVarsForProc(psi);
            psi.WorkingDirectory = outputAbsPath + Path.DirectorySeparatorChar;
            psi.UseShellExecute = false;
            psi.RedirectStandardError = false;
            psi.RedirectStandardOutput = false;
            psi.RedirectStandardInput = false;
            psi.WindowStyle = ProcessWindowStyle.Hidden;
            psi.CreateNoWindow = true;
            Process avrobjcopy = new Process();
            avrobjcopy.StartInfo = psi;
            try
            {
                if (avrobjcopy.Start())
                {
                    //allText += avrobjcopy.StandardError.ReadLine();
                    avrobjcopy.WaitForExit(10000);
                }
                else
                {
                    TextBoxModify(outputTextbox, "####Error: unable to start avr-objdump", TextBoxChangeMode.PrependNewLine);
                    return false;
                }
            }
            catch (Exception ex)
            {
                TextBoxModify(outputTextbox, "####Error: unable to start avr-objdump, " + ex.Message, TextBoxChangeMode.PrependNewLine);
                return false;
            }

            if (File.Exists(lstFileAbsPath))
            {
                return true;
            }
            else
            {
                TextBoxModify(outputTextbox, "####Error: LST file not created at " + lstFileAbsPath, TextBoxChangeMode.PrependNewLine);
                TextBoxModify(outputTextbox, "####avr-objdump output: " + allText, TextBoxChangeMode.PrependNewLine);
                return false;
            }
        }

        private void ReadSize()
        {
            string outputAbsPath = workingProject.DirPath + Path.DirectorySeparatorChar + workingProject.OutputDir;

            string args = "-C --mcu=" + workingProject.Device.ToLowerInvariant() + " " + workingProject.SafeFileNameNoExt + ".elf";

            TextBoxModify(outputTextbox, "Execute: avr-size " + args, TextBoxChangeMode.PrependNewLine);

            ProcessStartInfo psi = new ProcessStartInfo("avr-size", args);
            SetEnviroVarsForProc(psi);
            psi.WorkingDirectory = outputAbsPath + Path.DirectorySeparatorChar;
            psi.UseShellExecute = false;
            psi.RedirectStandardError = true;
            psi.RedirectStandardOutput = true;
            psi.RedirectStandardInput = true;
            psi.WindowStyle = ProcessWindowStyle.Hidden;
            psi.CreateNoWindow = true;
            Process avrsize = new Process();
            avrsize.StartInfo = psi;
            try
            {
                if (avrsize.Start())
                {
                    StreamReader stderr = avrsize.StandardError;
                    ReadErrAndWarnings(stderr, true);
                    StreamReader stdout = avrsize.StandardOutput;
                    ReadErrAndWarnings(stdout, true);
                    avrsize.WaitForExit(10000);
                }
                else
                {
                    TextBoxModify(outputTextbox, "####Error: unable to get the memory usage info", TextBoxChangeMode.PrependNewLine);
                }
            }
            catch (Exception ex)
            {
                TextBoxModify(outputTextbox, "####Error: unable to get the memory usage info, " + ex.Message, TextBoxChangeMode.PrependNewLine);
            }
        }

        private void ReadErrAndWarnings(StreamReader reader, bool outputElse)
        {
            string loc = "";
            string lastLoc = "";
            bool locIsFunct = false;
            string lastFile = "";

            string line = reader.ReadLine();

            while (line != null)
            {

                string re1 = "([a-z0-9_]+)";   // Variable Name 1
                string re2 = "(\\.)";	// Any Single Character 1
                string re3 = "([a-z0-9_]+)";	// Variable Name 2
                string re4 = "(:)";	// Any Single Character 2
                string re5 = "(\\d+)";	// Integer Number 1
                string re6 = "(:)";	// Any Single Character 3
                string re7 = "( )";	// White Space 1
                string re8 = "((?:[a-z][a-z]+))";	// Word 2
                string re9 = "(:)";	// Any Single Character 4
                string re10 = "( )";	// White Space 2
                string re11 = "(.*)";	// The Rest

                Regex r = new Regex(re1 + re2 + re3 + re4 + re5 + re6 + re7 + re8 + re9 + re10 + re11, RegexOptions.IgnoreCase | RegexOptions.Singleline);
                Match m = r.Match(line);
                if (m.Success)
                {
                    string fileName = m.Groups[1].Value + m.Groups[2].Value + m.Groups[3].Value;
                    string type = m.Groups[8].Value.Trim();
                    string lineNum = m.Groups[5].Value.Trim();
                    string msg = m.Groups[11].Value.Trim();

                    if (string.IsNullOrEmpty(msg) == false)
                    {
                        if (lastFile != fileName && loc == lastLoc)
                        {
                            loc = "";
                            lastLoc = "";
                        }

                        if (type.ToLowerInvariant().Contains("error"))
                            hasError++;

                        ListViewItem lvi = new ListViewItem(new string[] { fileName, lineNum, loc, type, msg });
                        ListViewModify(errorList, lvi, ListViewChangeMode.AddToTop);

                        if (type.ToLowerInvariant().Contains("warn") == false && msg.Trim().ToLowerInvariant() != "from")
                            ListViewModify(errorOnlyList, lvi, ListViewChangeMode.AddToTop);

                        lastLoc = loc;

                        lastFile = fileName;

                        if (locIsFunct == false)
                        {
                            loc = "";
                            lastLoc = "";
                        }
                    }

                    line = reader.ReadLine();
                    continue;
                }

                re1 = "(.*)";	// Non-greedy match on filler
                re2 = "(\\/|\\\\)";	// Any Single Character 1
                re3 = "([a-z0-9_]+)";	// Variable Name 1
                re4 = "(\\.)";	// Any Single Character 2
                re5 = "([a-z0-9_]+)";	// Variable Name 2
                re6 = "(:)";	// Any Single Character 4
                re7 = "(\\d+)";	// Integer Number 1
                re8 = "(:)";	// Any Single Character 5
                re9 = "(\\d+)";	// Integer Number 2
                re10 = "(:)";	// Any Single Character 6
                re11 = "(\\s+)";	// White Space 1
                string re12 = "(.*)";	// Variable Name 3
                string re13 = "(:)";	// Any Single Character 7
                string re14 = "(.*)";	// Any Single Character 7

                r = new Regex(re1 + re2 + re3 + re4 + re5 + re6 + re7 + re8 + re9 + re10 + re11 + re12 + re13 + re14, RegexOptions.IgnoreCase | RegexOptions.Singleline);
                m = r.Match(line);
                if (m.Success)
                {
                    string fileName = m.Groups[3].Value + m.Groups[4].Value + m.Groups[5].Value;
                    string type = m.Groups[12].Value.Trim();
                    string lineNum = m.Groups[7].Value.Trim();
                    string msg = m.Groups[14].Value.Trim();

                    if (string.IsNullOrEmpty(msg) == false)
                    {
                        if (lastFile != fileName && loc == lastLoc)
                        {
                            loc = "";
                            lastLoc = "";
                        }

                        if (type.ToLowerInvariant().Contains("error"))
                            hasError++;

                        ListViewItem lvi = new ListViewItem(new string[] { fileName, lineNum, loc, type, msg });
                        ListViewModify(errorList, lvi, ListViewChangeMode.AddToTop);

                        if (type.ToLowerInvariant().Contains("warn") == false && msg.Trim().ToLowerInvariant() != "from")
                            ListViewModify(errorOnlyList, lvi, ListViewChangeMode.AddToTop);

                        lastLoc = loc;

                        lastFile = fileName;

                        if (locIsFunct == false)
                        {
                            loc = "";
                            lastLoc = "";
                        }
                    }

                    line = reader.ReadLine();
                    continue;
                }

                re1 = "(.*)";	// Non-greedy match on filler
                re2 = "(\\/|\\\\)";	// Any Single Character 1
                re3 = "([a-z0-9_]+)";	// Variable Name 1
                re4 = "(\\.)";	// Any Single Character 2
                re5 = "([a-z0-9_]+)";	// Variable Name 2
                re6 = "(:)";	// Any Single Character 4
                re7 = "(\\d+)";	// Integer Number 1
                re8 = "(:)";	// Any Single Character 5
                re9 = "(.*)";

                r = new Regex(re1 + re2 + re3 + re4 + re5 + re6 + re7 + re8 + re9, RegexOptions.IgnoreCase | RegexOptions.Singleline);
                m = r.Match(line);
                if (m.Success)
                {
                    string fileName = m.Groups[3].Value + m.Groups[4].Value + m.Groups[5].Value;
                    string type = m.Groups[12].Value.Trim();
                    string lineNum = m.Groups[7].Value.Trim();
                    string msg = m.Groups[9].Value.Trim();

                    if (string.IsNullOrEmpty(msg) == false)
                    {
                        if (lastFile != fileName && loc == lastLoc)
                        {
                            loc = "";
                            lastLoc = "";
                        }

                        if (type.ToLowerInvariant().Contains("error"))
                            hasError++;

                        ListViewItem lvi = new ListViewItem(new string[] { fileName, lineNum, loc, type, msg });
                        ListViewModify(errorList, lvi, ListViewChangeMode.AddToTop);

                        if (type.ToLowerInvariant().Contains("warn") == false && msg.Trim().ToLowerInvariant() != "from")
                            ListViewModify(errorOnlyList, lvi, ListViewChangeMode.AddToTop);

                        lastLoc = loc;

                        lastFile = fileName;

                        if (locIsFunct == false)
                        {
                            loc = "";
                            lastLoc = "";
                        }
                    }

                    line = reader.ReadLine();
                    continue;
                }

                re1 = "([a-z0-9_]+)";	// Variable Name 1
                re2 = "(\\.)";	// Any Single Character 1
                re3 = "([a-z0-9_]+)";	// Variable Name 2
                re4 = "(:)";	// Any Single Character 2
                re5 = "(\\s+)";	// The Rest
                re6 = "(.*)";	// The Rest

                r = new Regex(re1 + re2 + re3 + re4 + re5 + re6, RegexOptions.IgnoreCase | RegexOptions.Singleline);
                m = r.Match(line);
                if (m.Success)
                {
                    string loc_ = m.Groups[6].Value.Trim().Trim(':').Trim();
                    if (loc_.ToLowerInvariant().Contains("function"))
                    {
                        loc = loc_.Substring(loc_.ToLowerInvariant().IndexOf("function") + "function".Length).Trim();
                        loc = loc.Trim('\'');
                        locIsFunct = true;
                    }
                    else if (loc_.ToLowerInvariant().Contains("top level"))
                    {
                        loc = "top level";
                    }
                    else
                    {
                        TextBoxModify(outputTextbox, line, TextBoxChangeMode.PrependNewLine);
                    }

                    line = reader.ReadLine();
                    continue;
                }

                re1 = "(from)";
                re2 = "(\\s*)";
                re3 = "([a-z0-9_]+)";	// Variable Name 1
                re4 = "(\\.)";	// Any Single Character 1
                re5 = "([a-z0-9_]+)";	// Variable Name 2
                re6 = "(\\s*)";
                re7 = "(:)";
                re8 = "(\\d+)";	// Integer Number 1
                //re9 = "(:|,)";

                r = new Regex(re1 + re2 + re3 + re4 + re5 + re6 + re7 + re8, RegexOptions.IgnoreCase | RegexOptions.Singleline);
                m = r.Match(line);
                if (m.Success)
                {
                    string type = "";
                    string lineNum = m.Groups[8].Value.Trim();
                    string msg = m.Groups[1].Value.Trim();
                    loc = m.Groups[3].Value + m.Groups[4].Value + m.Groups[5].Value;
                    string fileName = loc;
                    locIsFunct = false;

                    if (string.IsNullOrEmpty(msg) == false)
                    {
                        if (type.ToLowerInvariant().Contains("error"))
                            hasError++;

                        ListViewItem lvi = new ListViewItem(new string[] { fileName, lineNum, loc, type, msg });
                        ListViewModify(errorList, lvi, ListViewChangeMode.AddToTop);

                        if (type.ToLowerInvariant().Contains("warn") == false && msg.Trim().ToLowerInvariant() != "from")
                            ListViewModify(errorOnlyList, lvi, ListViewChangeMode.AddToTop);
                    }

                    line = reader.ReadLine();
                    continue;
                }

                re1 = "(from)";
                re2 = "(\\s*)";
                re3 = "(.*)";
                re4 = "(\\\\|\\/)";
                re5 = "([a-z0-9_]+)";	// Variable Name 1
                re6 = "(\\.)";	// Any Single Character 1
                re7 = "([a-z0-9_]+)";	// Variable Name 2
                re8 = "(\\s*)";
                re9 = "(:)";
                re10 = "(\\d+)";	// Integer Number 1
                //re11 = "(:|,)";

                r = new Regex(re1 + re2 + re3 + re4 + re5 + re6 + re7 + re8 + re9 + re10, RegexOptions.IgnoreCase | RegexOptions.Singleline);
                m = r.Match(line);
                if (m.Success)
                {
                    string type = "";
                    string lineNum = m.Groups[10].Value.Trim();
                    string msg = m.Groups[1].Value.Trim();
                    loc = m.Groups[5].Value + m.Groups[6].Value + m.Groups[7].Value;
                    string fileName = loc;
                    locIsFunct = false;

                    if (string.IsNullOrEmpty(msg) == false)
                    {
                        if (type.ToLowerInvariant().Contains("error"))
                            hasError++;

                        ListViewItem lvi = new ListViewItem(new string[] { fileName, lineNum, loc, type, msg });
                        ListViewModify(errorList, lvi, ListViewChangeMode.AddToTop);

                        if (type.ToLowerInvariant().Contains("warn") == false && msg.Trim().ToLowerInvariant() != "from")
                            ListViewModify(errorOnlyList, lvi, ListViewChangeMode.AddToTop);
                    }

                    line = reader.ReadLine();
                    continue;
                }

                // undefined reference
                //   EXAMPLE: Serial.o:(.rodata._ZTV6Serial+0x12): undefined reference to `Stream::skipUntil(unsigned char)'
                re1 = "(.*?)"; // object filename
                re2 = "(:\\()";
                re3 = "(.*?)";
                re4 = "(: undefined reference to)";
                re5 = "(.*)"; // message

                r = new Regex(re1 + re2 + re3 + re4 + re5, RegexOptions.IgnoreCase | RegexOptions.Singleline);
                m = r.Match(line);
                if (m.Success)
                {
                    string type = "";
                    string lineNum = "";
                    string msg = "undefined reference to" + m.Groups[5].Value.Trim();
                    loc = m.Groups[1].Value;
                    string fileName = loc;
                    locIsFunct = false;

                    hasError++;
                    ListViewItem lvi = new ListViewItem(new string[] { fileName, lineNum, loc, type, msg });
                    ListViewModify(errorList, lvi, ListViewChangeMode.AddToTop);
                    ListViewModify(errorOnlyList, lvi, ListViewChangeMode.AddToTop);

                    line = reader.ReadLine();
                    continue;
                }

                if (outputElse)
                {
                    if (string.IsNullOrEmpty(line) == false)
                        TextBoxModify(outputTextbox, line, TextBoxChangeMode.PrependNewLine);
                }
                else
                {
                    ListViewItem lvi = new ListViewItem(new string[] { "", "", "", "", line });
                    ListViewModify(errorList, lvi, ListViewChangeMode.AddToTop);

                    ListViewModify(errorOnlyList, lvi, ListViewChangeMode.AddToTop);
                }

                line = reader.ReadLine();
            }
        }

        #endregion

        #region Arduino Build Helpers

        private bool HandleArduino(ref string objFiles, ref string avr_ar_targets)
        {
            bool result = true;

            bool ardJoinStarted = false;
            ProjectFile ardFile = null;
            StreamWriter ardWriter = null;
            List<string> ardLibList = new List<string>();

            List<string> functProto = GetAllFunctProto(); // use regex to gather a list of function prototypes
            string allPrototypes = "";
            // gather all the prototypes in one line
            foreach (string proto in functProto)
            {
                allPrototypes += proto;
            }

            foreach (ProjectFile file in workingFileList.Values)
            {
                if (file.Exists && file.ToCompile && file.FileExt == "pde")
                {
                    if (ardJoinStarted == false)
                    {
                        // this is the first .pde file, do the stuff that must be done once here

                        // join all the .pde files into one cpp file
                        string tempArduinoPath = SettingsManagement.AppDataPath + "temp" + Path.DirectorySeparatorChar + "arduino_temp_main.cpp";
                        ardFile = new ProjectFile(tempArduinoPath, workingProject);

                        Program.MakeSurePathExists(SettingsManagement.AppDataPath + "temp");

                        try
                        {
                            ardWriter = new StreamWriter(ardFile.FileAbsPath);

                            ardWriter.WriteLine("#include <WProgram.h>"); // required for arduino functions to work
                            ardWriter.WriteLine("extern \"C\" void __cxa_pure_virtual() {}"); // required to prevent a compile error
                            ardWriter.Flush();

                            if (workingProject.OverrideArduinoCore)
                                workingProject.IncludeDirList.Add(workingProject.ArduinoCoreOverride);
                            else
                                workingProject.IncludeDirList.Add(SettingsManagement.ArduinoCorePath);

                            ardLibList.Clear();

                            ardJoinStarted = true;
                        }
                        catch (Exception ex)
                        {
                            TextBoxModify(outputTextbox, "####Error while writing " + ardFile.FileName + ", " + ex.Message, TextBoxChangeMode.PrependNewLine);
                        }
                    }

                    // join all the .pde files into one cpp file
                    // while tracking all the libraries being used and inserting function prototypes
                    JoinPDEFiles(file, ardWriter, allPrototypes);
                }
            }

            if (ardJoinStarted) // if arduino files exist, or else skip this since the main.cxx will interfer
            {
                List<ProjectFile> ardExtList = new List<ProjectFile>();

                try
                {
                    // append the final bit
                    ardWriter.WriteLine("#line 1 \"arduinomain.cpp\"");
                    ardWriter.WriteLine(GetPDEMain()); // get from either existing file or internal resource
                    ardWriter.Close();
                }
                catch (Exception ex)
                {
                    TextBoxModify(outputTextbox, "####Error while writing " + ardFile.FileName + ", " + ex.Message, TextBoxChangeMode.PrependNewLine);
                }

                // since arduino sketches needs its core files, gather all the core files
                if (workingProject.OverrideArduinoCore)
                    GetCompilableFiles(workingProject.ArduinoCoreOverride, ardExtList, false);
                else
                    GetCompilableFiles(SettingsManagement.ArduinoCorePath, ardExtList, false);

                try
                {
                    // gather all the library files
                    ardLibList = CodePreProcess.GetAllLibraries(GetFileContents(ardFile), ardLibList);
                }
                catch (Exception ex)
                {
                    TextBoxModify(outputTextbox, "####Error while geting list of libraries, " + ex.Message, TextBoxChangeMode.PrependNewLine);
                }

                foreach (string lib in ardLibList)
                {
                    string folderPath = SettingsManagement.ArduinoLibPath + Path.DirectorySeparatorChar + lib;
                    if (Directory.Exists(folderPath))
                    {
                        GetCompilableFiles(folderPath, ardExtList, true);
                    }
                    else
                    {
                        //if (workingFileList.ContainsKey(lib + ".h"))
                        //{
                        //    ProjectFile f = workingFileList[lib + ".h"];
                        //    try
                        //    {
                        //        File.Copy(f.FileAbsPath, SettingsManagement.AppDataPath + "temp" + Path.DirectorySeparatorChar + lib + ".h", true);
                        //    }
                        //    catch (Exception ex)
                        //    {
                        //        TextBoxModify(outputTextbox, "####Error while copying " + f.FileName + ", " + ex.Message, TextBoxChangeMode.PrependNewLine);
                        //    }
                        //}
                    }
                }

                workingProject.IncludeDirList.Add(SettingsManagement.AppDataPath + "temp");

                bool objRes;

                // compile all the needed files
                foreach (ProjectFile file in ardExtList)
                {
                    objRes = Compile(file);
                    result &= objRes;
                    if (objRes)
                    {
                        avr_ar_targets += file.FileNameNoExt + ".o ";
                        objFiles += file.FileNameNoExt + ".o ";
                    }
                }

                // finally compile the sketch
                objRes = Compile(ardFile);
                result &= objRes;
                if (objRes)
                {
                    objFiles += ardFile.FileNameNoExt + ".o ";
                }
            }

            return result;
        }

        private List<string> GetAllFunctProto()
        {
            List<string> resultList = new List<string>();

            // scan all pde files for functions
            foreach (ProjectFile file in workingFileList.Values)
            {
                if (file.Exists && file.ToCompile && file.FileExt == "pde")
                {
                    string fileContents = GetFileContents(file);

                    try
                    {
                        resultList = CodePreProcess.GetAllPrototypes(fileContents, resultList);
                    }
                    catch (Exception ex)
                    {
                        TextBoxModify(outputTextbox, "####Error getting prototypes from " + file.FileName + ", " + ex.Message, TextBoxChangeMode.PrependNewLine);
                    }
                }
            }

            return resultList;
        }

        private void GetCompilableFiles(string folder, List<ProjectFile> fileList, bool isLibrary)
        {
            Program.MakeSurePathExists(SettingsManagement.AppDataPath + "temp");

            // look for files which can be compiled
            if (Directory.Exists(folder))
            {
                DirectoryInfo dnfo = new DirectoryInfo(folder);
                foreach (FileInfo fnfo in dnfo.GetFiles())
                {
                    string ext = fnfo.FullName.ToLowerInvariant();
                    if (ext.EndsWith(".c") || ext.EndsWith(".cpp") || ext.EndsWith(".s") || ext.EndsWith(".h") || ext.EndsWith(".hpp"))
                    {
                        string newLoc = SettingsManagement.AppDataPath + "temp" + Path.DirectorySeparatorChar + fnfo.Name;

                        if (fnfo.Name != "main.cpp")
                        {
                            try
                            {
                                File.Copy(fnfo.FullName, newLoc, true);
                            }
                            catch (Exception ex)
                            {
                                TextBoxModify(outputTextbox, "####Error while copying " + fnfo.Name + ", " + ex.Message, TextBoxChangeMode.PrependNewLine);
                            }

                            if (ext.EndsWith(".h") == false && ext.EndsWith(".hpp") == false)
                            {

                                ProjectFile newFile = new ProjectFile(newLoc, workingProject);
                                fileList.Add(newFile);

                            }
                        }
                    }
                }

                // recursively find files within subdirectories
                foreach (DirectoryInfo nextDir in dnfo.GetDirectories())
                {
                    if (isLibrary)
                    {
                        if (nextDir.Name.ToLowerInvariant() == "utility")
                            GetCompilableFiles(nextDir.FullName, fileList, true);
                        else
                        {
                            try
                            {
                                if (nextDir.Name != "examples")
                                    Program.CopyAll(nextDir, new DirectoryInfo(SettingsManagement.AppDataPath + "temp" + Path.DirectorySeparatorChar + nextDir.Name));
                            }
                            catch (Exception ex)
                            {
                                TextBoxModify(outputTextbox, "####Error while copying " + nextDir.Name + ", " + ex.Message, TextBoxChangeMode.PrependNewLine);
                            }
                        }
                    }
                    else
                        GetCompilableFiles(nextDir.FullName, fileList, false);
                }
            }
        }

        private string GetPDEMain()
        {
            // finds the main.cxx file included for arduino's core
            // if can't be found, use the template stored in the project resource

            string contents = Properties.Resources.arduinomain;
            string filePath = SettingsManagement.ArduinoCorePath + Path.DirectorySeparatorChar + "main.cpp";
            if (workingProject.OverrideArduinoCore)
                filePath = workingProject.ArduinoCoreOverride + Path.DirectorySeparatorChar + "main.cpp";

            try
            {
                contents = File.ReadAllText(filePath);
            }
            catch (Exception ex)
            {
                TextBoxModify(outputTextbox, "####Error while reading the core main.cpp file, embedded version used instead. " + ex.Message, TextBoxChangeMode.PrependNewLine);
            }

            return contents;
        }

        private void JoinPDEFiles(ProjectFile file, StreamWriter writer, string prototypes)
        {
            try
            {
                writer.WriteLine("#line 1 \"{0}\"", file.FileName);
                string fileContent = File.ReadAllText(file.FileAbsPath);
                
                // function prototypes need to be inserted before the first non-preprocessor statement
                fileContent = InsertAtFirstStatement(fileContent, prototypes);

                writer.WriteLine(fileContent);
            }
            catch (Exception ex)
            {
                TextBoxModify(outputTextbox, "####Error while joining " + file.FileName + ", " + ex.Message, TextBoxChangeMode.PrependNewLine);
            }
        }

        private string InsertAtFirstStatement(string fileContent, string prototypes)
        {
            Regex r = new Regex(
                // whitespace
                "\\s+" + "|" +
                // multi-line comment
                "(/\\*[^*]*(?:\\*(?!/)[^*]*)*\\*/)" + "|" +
                // single-line comment
                "(//.*?$)" + "|" +
                // pre-processor directive
                "(#(?:\\\\\\n|.)*)"
                , RegexOptions.Multiline | RegexOptions.IgnoreCase
                );

            // the hack below finds the first C/C++ statement by looking for discontinuities
            // between regex matches, if comments and whitespace and preprocessor
            // directives are right next to each other, then subsequent match locations
            // should equal the end of the previous match
            int index = 0;
            Match m = r.Match(fileContent);
            while (m.Success)
            {
                if (m.Index == index)
                {
                    index = m.Index + m.Length;
                    m = r.Match(fileContent, index);
                }
                else
                    break;
            }

            return fileContent.Insert(index, prototypes);
        }

        #endregion

        #region Other Utilities

        private string GetFileContents(ProjectFile file)
        {
            string res = "";
            try
            {
                StreamReader reader = new StreamReader(file.FileAbsPath);
                res = reader.ReadToEnd();
                reader.Close();
            }
            catch (Exception ex)
            {
                TextBoxModify(outputTextbox, "####Error while retrieving file contents of " + file.FileName + ", " + ex.Message, TextBoxChangeMode.PrependNewLine);
            }
            return res;
        }

        private void PrepProject()
        {
            // clone the project
            workingProject = (AVRProject)project.Clone();

            // make a clone of the file list
            workingFileList = workingProject.FileList;
            //new Dictionary<string, ProjectFile>();
            //workingFileList.Clear();
            //foreach (KeyValuePair<string, ProjectFile> file in workingProject.FileList)
            //{
            //    ProjectFile newFile = (ProjectFile)file.Value.Clone();
            //    workingFileList.Add(file.Key, newFile);
            //}

            // all this cloning is to make sure the background worker thread used for the build
            // doesn't access the same resources as the main thread
            return;
        }

        #endregion

        public static bool CheckForWinAVR()
        {
            Process avrgcc = new Process();
            // trick avrdude to list supported parts by using a malformed argument
            avrgcc.StartInfo = new ProcessStartInfo("avr-gcc");
            //SetEnviroVarsForProc(avrgcc.StartInfo);
            avrgcc.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            avrgcc.StartInfo.CreateNoWindow = true;
            avrgcc.StartInfo.UseShellExecute = false;
            try
            {
                if (avrgcc.Start())
                {
                    return true;
                    AvrToolsInstalled = true;
                }
                else
                {
                    return CheckForWinAVR2();
                }
            }
            catch
            {
                return CheckForWinAVR2();
            }
        }

        private static bool PathBeenSet = false;

        public static bool CheckForWinAVR2()
        {
            Process avrgcc = new Process();
            // trick avrdude to list supported parts by using a malformed argument
            avrgcc.StartInfo = new ProcessStartInfo("avr-gcc");

            if (PathBeenSet == false)
            {
                string path = Environment.GetEnvironmentVariable("path");
                if (path == null)
                    path = "";

                path = path.TrimEnd(';', ' ', '\r', '\n', '\t') + ";" + Path.GetDirectoryName(Application.ExecutablePath).TrimEnd(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar) + Path.DirectorySeparatorChar + "avrtools" + Path.DirectorySeparatorChar + "bin";

                Environment.SetEnvironmentVariable("path", path);
            }

            PathBeenSet = true;

            //SetEnviroVarsForProc(avrgcc.StartInfo);
            avrgcc.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            avrgcc.StartInfo.CreateNoWindow = true;
            avrgcc.StartInfo.UseShellExecute = false;
            try
            {
                if (avrgcc.Start())
                {
                    return true;
                }
                else
                {
                    AvrToolsInstalled = false;
                    DialogResult res = MessageBox.Show("I don't think you have any AVR tools installed, do you want to go download it?", "AVR Tools Not Found", MessageBoxButtons.YesNo);
                    if (res == DialogResult.Yes)
                        System.Diagnostics.Process.Start(Properties.Resources.WinAVRURL);

                    return false;
                }
            }
            catch
            {
                AvrToolsInstalled = false;
                DialogResult res = MessageBox.Show("I don't think you have any AVR tools installed, do you want to go download it?", "AVR Tools Not Found", MessageBoxButtons.YesNo);
                if (res == DialogResult.Yes)
                    System.Diagnostics.Process.Start(Properties.Resources.WinAVRURL);

                return false;
            }
        }

        public static void SetEnviroVarsForProc(ProcessStartInfo psi)
        {
            return;
            if (AvrToolsInstalled)
                return;

            string p = Path.GetDirectoryName(Application.ExecutablePath).TrimEnd(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar) + Path.DirectorySeparatorChar + "avrtools" + Path.DirectorySeparatorChar + "bin" + Path.DirectorySeparatorChar;

            if (Directory.Exists(p) == false)
                return;

            p = p.TrimEnd(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);

            string dictKey = null;
            if (psi.EnvironmentVariables.ContainsKey("path"))
                dictKey = "path";
            else if (psi.EnvironmentVariables.ContainsKey("PATH"))
                dictKey = "PATH";
            else if (psi.EnvironmentVariables.ContainsKey("Path"))
                dictKey = "Path";

            if (dictKey == null)
                return;

            if ((psi.EnvironmentVariables[dictKey].ToLowerInvariant().TrimEnd(';', ' ', '\r', '\n', '\t') + ";").Contains(p.ToLowerInvariant() + ";") == false)
            {
                if (psi.EnvironmentVariables[dictKey].TrimEnd().EndsWith(";") == false)
                    psi.EnvironmentVariables[dictKey] += ";";

                psi.EnvironmentVariables[dictKey] += p + ";";
            }
        }

        public static string GetToolPath(string toolName)
        {
            string pathStart = Path.GetDirectoryName(Application.ExecutablePath).TrimEnd(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar) + Path.DirectorySeparatorChar + "avrtools" + Path.DirectorySeparatorChar + "bin" + Path.DirectorySeparatorChar + toolName;

            if (File.Exists(pathStart + ".exe"))
            {
                return pathStart + ".exe";
            }
            else if (File.Exists(pathStart + ".bat"))
            {
                return pathStart + ".bat";
            }
            else if (File.Exists(pathStart + ".cmd"))
            {
                return pathStart + ".cmd";
            }
            else if (File.Exists(pathStart))
            {
                return pathStart;
            }

            return toolName;
        }
    }

    public class ProjectBurner
    {
        [DllImport("User32.dll", ExactSpelling = true, CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        private static extern bool MoveWindow(IntPtr hWnd, int x, int y, int cx, int cy, bool repaint);

        private AVRProject project;
        private Process avrdude;

        public ProjectBurner(AVRProject project)
        {
            this.project = project;

            // a reference to this proccess is kept so it can be killed
            avrdude = new Process();
        }

        public void BurnCMD(bool onlyOptions, bool burnFuses, Control formObj)
        {
            try
            {
                // kill if still open, this prevents multiple CMD windows from filling the screen
                avrdude.Kill();
            }
            catch
            {
            }

            // construct appropriate arguments

            string fileStr = "";
            if (onlyOptions == false && burnFuses == false)
                fileStr = String.Format("-U flash:w:\"{0}\\{1}\\{2}.hex\":a", project.DirPath, project.OutputDir, project.SafeFileNameNoExt);

            if (burnFuses)
                fileStr = project.BurnFuseBox;

            string overrides = "";

            BurnerPanel.GetPortOverride(ref overrides, project);

            string args = String.Format("avrdude -p {0} -c {1} {2} {3} {4}", project.BurnPart.ToUpperInvariant(), project.BurnProgrammer, overrides, project.BurnOptions, fileStr);
            avrdude.StartInfo = new ProcessStartInfo("cmd", "/k " + args);
            ProjectBuilder.SetEnviroVarsForProc(avrdude.StartInfo);
            if (project.BurnAutoReset && string.IsNullOrEmpty(project.BurnPort) == false)
            {
                ToggleRTS(project.BurnPort);
            }

            try
            {
                if (avrdude.Start() == false)
                {
                    MessageBox.Show("Error, Unable to Start AVRDUDE");
                }
                else if (formObj != null)
                {
                    //System.Drawing.Rectangle r = formObj.RectangleToScreen(new System.Drawing.Rectangle(0, 0, formObj.Width, formObj.Height));

                    //MoveWindow(avrdude.MainWindowHandle, r.X, r.Y, r.Width, r.Height, true);
                }
            }
            catch (Exception ex)
            {
                ErrorReportWindow.Show(ex, "Error, Unable to Start AVRDUDE");
                
            }

            // note that avrdude's output can't be redirected easily, as the text based progress bar is annoying to deal with
        }

        private void ToggleRTS(string p)
        {
            if (p.StartsWith("COM") == false && p.StartsWith("//./COM") == false && p.StartsWith("\\\\.\\COM") == false)
            {
                MessageBox.Show("Error: Cannot perform Arduino auto-reset without a valid COM port defined");
                return;
            }

            try
            {
                UnmanagedSerialPort usp = new UnmanagedSerialPort(p);
                usp.Open();
                usp.Off();
                usp.On();
                usp.Close();
            }
            catch (Exception ex)
            {
                ErrorReportWindow.Show(ex, "Error Performing AutoReset");
                
            }
        }

        public static string[] GetAvailParts(string progname, bool suppressErr)
        {
            List<string> res = new List<string>();

            Process avrdude = new Process();
            // trick avrdude to list supported parts by using a malformed argument
            avrdude.StartInfo = new ProcessStartInfo("avrdude", "-c " + progname + " -p " + Guid.NewGuid().ToString());
            ProjectBuilder.SetEnviroVarsForProc(avrdude.StartInfo);
            avrdude.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            avrdude.StartInfo.CreateNoWindow = true;
            avrdude.StartInfo.UseShellExecute = false;
            avrdude.StartInfo.RedirectStandardError = true;
            avrdude.StartInfo.RedirectStandardOutput = true;
            avrdude.StartInfo.RedirectStandardInput = true;
            try
            {
                if (avrdude.Start())
                {
                    res = GetPartWords(avrdude.StandardError.ReadToEnd(), res);
                }
                else
                {
                    if (suppressErr == false)
                    {
                        MessageBox.Show("Error, Unable to Start AVRDUDE");
                    }
                }
            }
            catch (Exception ex)
            {
                if (suppressErr == false)
                {
                    ErrorReportWindow.Show(ex, "Error: Unable to start AVRDUDE");
                    
                }
            }

            return res.ToArray();
        }

        private static List<string> GetPartWords(string output, List<string> res)
        {
            if (res == null) res = new List<string>();
            string[] lines = output.Split('\n');
            Regex r = new Regex("([0-9a-zA-Z_]+)(\\s+)(=)(\\s+)(.*)(\\s+)(\\[.*\\])", RegexOptions.IgnoreCase);
            foreach (string line in lines)
            {
                Match m = r.Match(line.Trim());
                if (m.Success && m.Index == 0)
                    res.Add(m.Groups[5].Value.Trim().ToLowerInvariant());
            }

            res.Sort((x, y) => string.Compare(x, y));

            return res;
        }

        public static string[] GetAvailProgrammers(bool suppressErr)
        {
            List<string> res = new List<string>();

            Process avrdude = new Process();
            // trick avrdude to list supported programmers by using a malformed argument
            avrdude.StartInfo = new ProcessStartInfo("avrdude", "-c blarg");
            ProjectBuilder.SetEnviroVarsForProc(avrdude.StartInfo);
            avrdude.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            avrdude.StartInfo.CreateNoWindow = true;
            avrdude.StartInfo.UseShellExecute = false;
            avrdude.StartInfo.RedirectStandardError = true;
            avrdude.StartInfo.RedirectStandardOutput = true;
            avrdude.StartInfo.RedirectStandardInput = true;
            try
            {
                if (avrdude.Start())
                {
                    res = GetFirstWords(avrdude.StandardError.ReadToEnd(), res);
                }
                else
                {
                    if (suppressErr == false)
                    {
                        MessageBox.Show("Error, Unable to Start AVRDUDE");
                    }
                }
            }
            catch (Exception ex)
            {
                if (suppressErr == false)
                {
                    ErrorReportWindow.Show(ex, "Error, Unable to Start AVRDUDE");
                    
                }
            }

            res.Sort((x, y) => string.Compare(x, y));

            return res.ToArray();
        }

        private static List<string> GetFirstWords(string output, List<string> res)
        {
            if (res == null) res = new List<string>();
            string[] lines = output.Split('\n');
            Regex r = new Regex("([0-9a-zA-Z_]+)(\\s+)(=)(\\s+)(.*)(\\s+)(\\[.*\\])", RegexOptions.IgnoreCase);
            foreach (string line in lines)
            {
                Match m = r.Match(line.Trim());
                if (m.Success && m.Index == 0)
                    res.Add(m.Groups[1].Value.Trim().ToLowerInvariant());
            }
            return res;
        }
    }

    public class Makefile
    {
        /// <summary>
        /// generates a makefile in the same way AVRStudio generates one
        /// </summary>
        /// <param name="proj">the project containing the settings</param>
        /// <returns>returns true if successful</returns>
        public static bool GenerateNormal(AVRProject projOrig)
        {
            AVRProject proj = projOrig.Clone();

            bool success = true;

            StreamWriter writer = null;
            try
            {
                writer = new StreamWriter(proj.DirPath + Path.DirectorySeparatorChar + "Makefile");

                writer.WriteLine("##################################");
                writer.WriteLine("## Makefile for project: {0}", proj.SafeFileNameNoExt);
                writer.WriteLine("## Generated by AVR Project IDE: {0}", Properties.Resources.WebsiteURL);
                writer.WriteLine("##################################");
                writer.WriteLine();
                writer.WriteLine("## General Flags");
                writer.WriteLine("PROJECT = {0}", proj.SafeFileNameNoExt);
                writer.WriteLine("MCU = {0}", proj.Device.ToLowerInvariant());
                writer.WriteLine("BURNMCU = {0}", proj.BurnPart.ToLowerInvariant());
                writer.WriteLine("BURNPROGRAMMER = {0}", proj.BurnProgrammer.ToLowerInvariant());
                writer.WriteLine("OUTDIR = {0}", proj.OutputDir.Replace('\\', '/'));
                writer.WriteLine("TARGET = $(OUTDIR)/$(PROJECT).elf");
                writer.WriteLine("CC = avr-gcc");
                writer.WriteLine("CCXX = avr-g++");
                writer.WriteLine();
                writer.WriteLine("## Flags common to C, ASM, and Linker");
                writer.WriteLine("COMMON = -mmcu=$(MCU)");
                writer.WriteLine();
                writer.WriteLine("## Flags common to C only");
                writer.WriteLine("CFLAGS = $(COMMON)");
                writer.WriteLine("CONLYFLAGS = {0}", proj.OtherOptionsForC);

                string cflags = proj.OtherOptions.Trim();

                if (proj.ClockFreq != 0)
                    cflags += " -DF_CPU=" + Math.Round(proj.ClockFreq).ToString("0") + "UL ";

                cflags += proj.Optimization + " ";

                if (proj.UnsignedChars)
                    cflags += "-funsigned-char ";

                if (proj.UnsignedBitfields)
                    cflags += "-funsigned-bitfields ";

                if (proj.PackStructs)
                    cflags += "-fpack-struct ";

                if (proj.ShortEnums)
                    cflags += "-fshort-enums ";

                if (proj.FunctionSections)
                    cflags += "-ffunction-sections ";

                if (proj.DataSections)
                    cflags += "-fdata-sections ";

                writer.WriteLine("CFLAGS += {0}", cflags.Trim());
                writer.WriteLine("CFLAGS += -MD -MP -MT $(*F).o");

                writer.WriteLine();
                writer.WriteLine("## Flags common to ASM only");
                writer.WriteLine("ASMFLAGS = $(COMMON)");
                writer.WriteLine("ASMFLAGS += $(CFLAGS)");
                writer.WriteLine("ASMFLAGS += -x assembler-with-cpp -Wa,-gdwarf2");
                writer.WriteLine("ASMFLAGS += {0}", proj.OtherOptionsForS);

                writer.WriteLine();
                writer.WriteLine("## Flags common to CPP/CXX only");
                writer.WriteLine("CXXFLAGS = $(COMMON)");
                writer.WriteLine("CXXFLAGS += $(CFLAGS)");
                writer.WriteLine("CXXFLAGS += {0}", proj.OtherOptionsForCPP);

                writer.WriteLine();
                writer.WriteLine("## Flags common to Linker only");
                writer.WriteLine("LDFLAGS = $(COMMON)");
                writer.WriteLine("LDFLAGS += -Wl,-Map=$(OUTDIR)/$(PROJECT).map");

                writer.WriteLine("LDFLAGS += -Wl,--gc-sections");

                if (proj.UseInitStack)
                {
                    writer.WriteLine("LDFLAGS += -Wl,--defsym=__stack=0x{0:X}", proj.InitStackAddr);
                }

                foreach (MemorySegment seg in proj.MemorySegList.Values)
                {
                    int addr = (int)seg.Addr;
                    if (seg.Type.ToLowerInvariant() == "sram")
                    {
                        addr += 0x800000;
                    }
                    else if (seg.Type.ToLowerInvariant() == "eeprom")
                    {
                        addr += 0x810000;
                    }
                    writer.WriteLine("LDFLAGS += -Wl,-section-start={0}=0x{1:X}", seg.Name, addr);
                }

                if (string.IsNullOrEmpty(proj.LinkerOptions) == false)
                {
                    writer.WriteLine("LDFLAGS += {0}", proj.LinkerOptions);
                }

                writer.WriteLine();
                writer.WriteLine("## Flags for Intel HEX file production");
                writer.WriteLine("HEX_FLASH_FLAGS = -R .eeprom -R .fuse -R .lock -R .signature");
                writer.WriteLine();
                writer.WriteLine("HEX_EEPROM_FLAGS = -j .eeprom");
                writer.WriteLine("HEX_EEPROM_FLAGS += --set-section-flags=.eeprom=\"alloc,load\"");
                writer.WriteLine("HEX_EEPROM_FLAGS += --change-section-lma .eeprom=0 --no-change-warnings");

                foreach (MemorySegment seg in proj.MemorySegList.Values)
                {
                    if (seg.Type.ToLowerInvariant() == "eeprom")
                    {
                        writer.WriteLine("HEX_EEPROM_FLAGS += --change-section-lma {0}=0x{1}", seg.Name, seg.Addr);
                    }
                }

                string incdirs = "-I\".\" ";
                foreach (string s in proj.IncludeDirList)
                {
                    if (string.IsNullOrEmpty(s) == false)
                        incdirs += "-I\"" + s + "\" ";
                }
                incdirs = incdirs.Trim();
                if (string.IsNullOrEmpty(incdirs) == false)
                {
                    writer.WriteLine();
                    writer.WriteLine("## Include Directories");
                    writer.WriteLine("INCLUDES = {0}", incdirs);
                }

                string linklibstr = "";
                foreach (string obj in proj.LinkLibList)
                {
                    if (string.IsNullOrEmpty(obj) == false)
                    {
                        if (obj.StartsWith("lib"))
                        {
                            linklibstr += "-l" + obj.Substring(3).TrimEnd('a').TrimEnd('.') + " ";
                        }
                        else
                        {
                            string libName = Path.GetFileNameWithoutExtension(obj);

                            if (libName.StartsWith("lib"))
                                libName = libName.Substring(3);

                            string libPath = Path.GetDirectoryName(obj);

                            linklibstr += "-l" + libName + " ";

                            if (proj.LibraryDirList.Contains(libPath) == false)
                                proj.LibraryDirList.Add(libPath);
                        }
                    }
                }

                linklibstr = linklibstr.Trim();
                if (string.IsNullOrEmpty(linklibstr) == false)
                {
                    writer.WriteLine();
                    writer.WriteLine("## Libraries");
                    writer.WriteLine("LIBS = {0}", linklibstr);
                }

                string libdirs = "";
                foreach (string s in proj.LibraryDirList)
                {
                    if (string.IsNullOrEmpty(s) == false)
                        libdirs += "-L\"" + s + "\" ";
                }
                libdirs = libdirs.Trim();
                if (string.IsNullOrEmpty(libdirs) == false)
                {
                    writer.WriteLine();
                    writer.WriteLine("## Library Directories");
                    writer.WriteLine("LIBDIRS = {0}", libdirs);
                }

                string ofiles = "";
                string compileStr = "";

                foreach (KeyValuePair<string, ProjectFile> file in proj.FileList)
                {
                    if (file.Value.ToCompile && file.Value.FileExt != "h" && file.Value.FileExt != "hpp" && file.Value.FileExt != "pde")
                    {
                        ofiles += file.Value.FileNameNoExt + ".o ";

                        compileStr += file.Value.FileNameNoExt + ".o: ./" + file.Value.FileRelPathTo(proj.DirPath).Replace('\\', '/');
                        compileStr += Environment.NewLine;
                        if (file.Value.FileExt == "s")
                        {
                            compileStr += "\t $(CC) $(INCLUDES) ";
                            compileStr += "$(ASMFLAGS)";
                        }
                        else if (file.Value.FileExt == "c")
                        {
                            compileStr += "\t $(CC) $(INCLUDES) ";
                            compileStr += "$(CFLAGS) $(CONLYFLAGS)";
                        }
                        else if (file.Value.FileExt == "cpp" || file.Value.FileExt == "cxx")
                        {
                            compileStr += "\t $(CCXX) $(INCLUDES) ";
                            compileStr += "$(CXXFLAGS)";
                        }
                        compileStr += " -c ";
                        compileStr += file.Value.Options.Trim();
                        compileStr += " $<" + Environment.NewLine + Environment.NewLine;
                    }
                }

                ofiles = ofiles.Trim();

                writer.WriteLine();
                writer.WriteLine("## Link these object files to be made");
                writer.WriteLine("OBJECTS = {0}", ofiles);

                string linkobjstr = "";
                foreach (string s in proj.LinkObjList)
                {
                    if (string.IsNullOrEmpty(s) == false)
                        linkobjstr += "\"" + s + "\" ";
                }

                writer.WriteLine();
                writer.WriteLine("## Link objects specified by users");
                writer.WriteLine("LINKONLYOBJECTS = {0}", linkobjstr.Trim());

                writer.WriteLine();
                writer.WriteLine("## Compile");
                writer.WriteLine();
                writer.WriteLine("all: $(TARGET)");
                writer.WriteLine();
                writer.WriteLine(compileStr);

                writer.WriteLine();
                writer.WriteLine();
                writer.WriteLine("$(OUTDIR):");
                writer.WriteLine("\t mkdir $@");
                writer.WriteLine();
                writer.WriteLine();
                writer.WriteLine("## Link");
                writer.WriteLine("$(TARGET): $(OBJECTS) $(OUTDIR)");

                writer.WriteLine("\t-rm -rf $(TARGET) $(OUTDIR)/$(PROJECT).map");

                writer.WriteLine("\t $(CC) $(LDFLAGS) $(OBJECTS) $(LINKONLYOBJECTS) $(LIBDIRS) $(LIBS) -o $(TARGET)");

                writer.WriteLine("\t-rm -rf $(OBJECTS) {0}", (ofiles + " ").Replace(".o ", ".d "));
                writer.WriteLine("\t-rm -rf $(OUTDIR)/$(PROJECT).hex $(OUTDIR)/$(PROJECT).eep $(OUTDIR)/$(PROJECT).lss");

                writer.WriteLine("\tavr-objcopy -O ihex $(HEX_FLASH_FLAGS) $(TARGET) $(OUTDIR)/$(PROJECT).hex");
                writer.WriteLine("\tavr-objcopy $(HEX_FLASH_FLAGS) -O ihex $(TARGET) $(OUTDIR)/$(PROJECT).eep || exit 0");
                writer.WriteLine("\tavr-objdump -h -S $(TARGET) >> $(OUTDIR)/$(PROJECT).lss");
                writer.WriteLine("\t@avr-size -C --mcu=${MCU} ${TARGET}");

                writer.WriteLine();
                writer.WriteLine("## Program");
                writer.WriteLine("burn:");
                string overrides = "";
                BurnerPanel.GetPortOverride(ref overrides, proj);
                writer.WriteLine("\tavrdude -p $(BURNMCU) -c $(BURNPROGRAMMER) {2} -U flash:w:$(OUTDIR)/$(PROJECT).hex:a {4}", proj.BurnPart, proj.BurnProgrammer, overrides, proj.FileNameNoExt, proj.BurnOptions);
                writer.WriteLine();
                writer.WriteLine("burnfuses:");
                BurnerPanel.GetPortOverride(ref overrides, proj);
                writer.WriteLine("\tavrdude -p $(BURNMCU) -c $(BURNPROGRAMMER) {2} {3} {4}", proj.BurnPart, proj.BurnProgrammer, overrides, proj.BurnFuseBox, proj.BurnOptions);

                writer.WriteLine();
                writer.WriteLine("## Clean target");
                writer.WriteLine(".PHONY: clean");
                writer.WriteLine("clean:");
                writer.WriteLine("\t-rm -rf $(OBJECTS) {0} $(OUTDIR)/$(PROJECT).elf $(OUTDIR)/$(PROJECT).map $(OUTDIR)/$(PROJECT).lss $(OUTDIR)/$(PROJECT).hex $(OUTDIR)/$(PROJECT).eep $(OUTDIR)", (ofiles + " ").Replace(".o ", ".d "));
            }
            catch
            {
                success = false;
            }
            try
            {
                writer.Close();
            }
            catch
            {
                success = false;
            }
            return success;
        }

        /// <summary>
        /// generates a makefile that's meant for arduino
        /// this uses the makefile that arduino provided but replacing some placeholders
        /// with custom options
        /// </summary>
        /// <param name="proj">project with settings</param>
        /// <returns>true if successful</returns>
        public static bool GenerateArduino(AVRProject proj)
        {
            bool success = true;

            StreamWriter writer = null;
            try
            {
                writer = new StreamWriter(proj.DirPath + "\\Makefile");

                string makefileTemplate = ASCIIEncoding.ASCII.GetString(Properties.Resources.arduinomakefile);

                makefileTemplate = makefileTemplate.Replace("###@@@INSTALL_DIR@@@###", "");
                makefileTemplate = makefileTemplate.Replace("###@@@AVRDUDE_PART@@@###", proj.BurnPart);
                makefileTemplate = makefileTemplate.Replace("###@@@AVRDUDE_OPTIONS@@@###", proj.BurnOptions);
                makefileTemplate = makefileTemplate.Replace("###@@@AVRDUDE_PROGRAMMER@@@###", proj.BurnProgrammer);
                makefileTemplate = makefileTemplate.Replace("###@@@MCU@@@###", proj.Device.ToLowerInvariant());
                makefileTemplate = makefileTemplate.Replace("###@@@F_CPU@@@###", proj.ClockFreq.ToString());

                string cinc = "";
                foreach (string s in proj.IncludeDirList)
                {
                    if (string.IsNullOrEmpty(s) == false)
                        cinc += "-I\"" + s + "\" ";
                }

                makefileTemplate = makefileTemplate.Replace("###@@@MORE_CINC@@@###", cinc);

                string checklist = "";
            
                if (proj.PackStructs)
                    checklist += "-fpack-struct ";

                if (proj.ShortEnums)
                    checklist += "-fshort-enums ";

                if (proj.UnsignedChars)
                    checklist += "-funsigned-char ";

                if (proj.UnsignedBitfields)
                    checklist += "-funsigned-bitfields ";

                makefileTemplate = makefileTemplate.Replace("###@@@CTUNING@@@###", checklist);

                string moreLDSFLAGS = "";

                if (proj.UseInitStack)
                {
                    moreLDSFLAGS += String.Format(Environment.NewLine + "LDFLAGS += -Wl,--defsym=__stack=0x{0:X}", proj.InitStackAddr);
                }

                foreach (MemorySegment seg in proj.MemorySegList.Values)
                {
                    int addr = (int)seg.Addr;
                    if (seg.Type.ToLowerInvariant() == "sram")
                    {
                        addr += 0x800000;
                    }
                    else if (seg.Type.ToLowerInvariant() == "eeprom")
                    {
                        addr += 0x810000;
                    }
                     moreLDSFLAGS += String.Format(Environment.NewLine + "LDFLAGS += -Wl,-section-start={0}=0x{1:X}", seg.Name, addr);
                }

                if (string.IsNullOrEmpty(proj.LinkerOptions) == false)
                {
                     moreLDSFLAGS += String.Format(Environment.NewLine + "LDFLAGS += {0}", proj.LinkerOptions);
                }

                string libdirs = "";
                foreach (string s in proj.LibraryDirList)
                {
                    if (string.IsNullOrEmpty(s) == false)
                        libdirs += "-L\"" + s + "\" ";
                }
                libdirs = libdirs.Trim();
                if (string.IsNullOrEmpty(libdirs) == false)
                {
                    moreLDSFLAGS += " " + libdirs;
                }

                string linklibstr = "";
                foreach (string s in proj.LinkLibList)
                {
                    if (string.IsNullOrEmpty(s) == false)
                    {
                        if (s.StartsWith("lib"))
                        {
                            linklibstr += "-l" + s.Substring(3).TrimEnd('a').TrimEnd('.') + " ";
                        }
                        else
                        {
                            linklibstr += "-l\"" + s.TrimEnd('a').TrimEnd('.') + "\" ";
                        }
                    }
                }
                linklibstr = linklibstr.Trim();
                if (string.IsNullOrEmpty(linklibstr) == false)
                {
                    moreLDSFLAGS += " " + linklibstr;
                }

                makefileTemplate = makefileTemplate.Replace("###@@@MORE_LDFLAGS@@@###", moreLDSFLAGS);

                string addEEPROM = "";

                foreach (MemorySegment seg in proj.MemorySegList.Values)
                {
                    if (seg.Type.ToLowerInvariant() == "eeprom")
                    {
                        addEEPROM += String.Format("--change-section-lma {0}=0x{1} ", seg.Name, seg.Addr);
                    }
                }

                makefileTemplate = makefileTemplate.Replace("###@@@ADD_EEPROM_ADDRS@@@###", addEEPROM);

                writer.Write(makefileTemplate);
            }
            catch
            {
                success = false;
            }
            try
            {
                writer.Close();
            }
            catch
            {
                success = false;
            }
            return success;
        }

        /// <summary>
        /// determines what kind of makefile to generate
        /// works by checking if the project includes pde files
        /// </summary>
        /// <param name="proj">project with settings and file list</param>
        /// <returns>true if makefile is generated successfully</returns>
        public static bool Generate(AVRProject proj)
        {
            if (proj.UseArduinoMakefile)
                return GenerateArduino(proj);
            else
                return GenerateNormal(proj);
            
        }
    }

    /// <summary>
    /// see http://www.hanselman.com/blog/content/binary/UnmanagedIRSerialPort.txt
    /// </summary>
    public class UnmanagedSerialPort
    {
        IntPtr portHandle;
        DCB dcb = new DCB();

        string port = String.Empty;

        public UnmanagedSerialPort(string portString)
        {
            port = portString;
        }

        public void Open()
        {
            portHandle = CreateFile(port,
                  EFileAccess.GenericRead | EFileAccess.GenericWrite,
                  EFileShare.None,
                  IntPtr.Zero,
                  ECreationDisposition.OpenExisting,
                  EFileAttributes.Overlapped, IntPtr.Zero);

            GetCommState(portHandle, ref dcb);
            dcb.RtsControl = RtsControl.Enable;
            dcb.DtrControl = DtrControl.Disable;
            dcb.BaudRate = 115200;
            SetCommState(portHandle, ref dcb);
        }

        public void On()
        {
            EscapeCommFunction(portHandle, SETDTR);
            EscapeCommFunction(portHandle, SETRTS);
        }

        public void Off()
        {
            EscapeCommFunction(portHandle, CLRDTR);
            EscapeCommFunction(portHandle, CLRRTS);
        }

        public void Close()
        {
            CloseHandle(portHandle);
        }

        #region Interop Serial Port Stuff
        const int SETDTR = 5;
        const int CLRDTR = 6;
        const int SETRTS = 3;
        const int CLRRTS = 4;

        [DllImport("kernel32.dll")]
        static extern bool GetCommState(IntPtr hFile, ref DCB lpDCB);

        [DllImport("kernel32.dll")]
        static extern bool SetCommState(IntPtr hFile, [In] ref DCB lpDCB);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool CloseHandle(IntPtr handle);

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool EscapeCommFunction(IntPtr hFile, int dwFunc);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern IntPtr CreateFile(
            string lpFileName,
            EFileAccess dwDesiredAccess,
            EFileShare dwShareMode,
            IntPtr lpSecurityAttributes,
            ECreationDisposition dwCreationDisposition,
            EFileAttributes dwFlagsAndAttributes,
            IntPtr hTemplateFile);

        [Flags]
        public enum EFileAccess : uint
        {
            GenericRead = 0x80000000,
            GenericWrite = 0x40000000,
            GenericExecute = 0x20000000,
            GenericAll = 0x10000000,
        }

        [Flags]
        public enum EFileShare : uint
        {
            None = 0x00000000,
            Read = 0x00000001,
            Write = 0x00000002,
            Delete = 0x00000004,
        }

        public enum ECreationDisposition : uint
        {
            New = 1,
            CreateAlways = 2,
            OpenExisting = 3,
            OpenAlways = 4,
            TruncateExisting = 5,
        }

        [Flags]
        public enum EFileAttributes : uint
        {
            Readonly = 0x00000001,
            Hidden = 0x00000002,
            System = 0x00000004,
            Directory = 0x00000010,
            Archive = 0x00000020,
            Device = 0x00000040,
            Normal = 0x00000080,
            Temporary = 0x00000100,
            SparseFile = 0x00000200,
            ReparsePoint = 0x00000400,
            Compressed = 0x00000800,
            Offline = 0x00001000,
            NotContentIndexed = 0x00002000,
            Encrypted = 0x00004000,
            Write_Through = 0x80000000,
            Overlapped = 0x40000000,
            NoBuffering = 0x20000000,
            RandomAccess = 0x10000000,
            SequentialScan = 0x08000000,
            DeleteOnClose = 0x04000000,
            BackupSemantics = 0x02000000,
            PosixSemantics = 0x01000000,
            OpenReparsePoint = 0x00200000,
            OpenNoRecall = 0x00100000,
            FirstPipeInstance = 0x00080000
        }


        public enum RtsControl : int
        {
            Disable = 0,
            Enable = 1,
            Handshake = 2,
            Toggle = 3
        };
        public enum DtrControl : int
        {
            Disable = 0,
            Enable = 1,
            Handshake = 2
        };

        [StructLayout(LayoutKind.Sequential)]
        public struct DCB
        {

            internal uint DCBLength;
            internal uint BaudRate;
            private BitVector32 Flags;

            //I've missed some members...
            private uint wReserved;        // not currently used
            internal uint XonLim;           // transmit XON threshold
            internal uint XoffLim;          // transmit XOFF threshold             

            internal byte ByteSize;
            internal Parity Parity;
            internal StopBits StopBits;

            //...and some more
            internal char XonChar;          // Tx and Rx XON character
            internal char XoffChar;         // Tx and Rx XOFF character
            internal char ErrorChar;        // error replacement character
            internal char EofChar;          // end of input character
            internal char EvtChar;          // received event character
            private uint wReserved1;       // reserved; do not use     

            private static readonly int fBinary;
            private static readonly int fParity;
            private static readonly int fOutxCtsFlow;
            private static readonly int fOutxDsrFlow;
            private static readonly BitVector32.Section fDtrControl;
            private static readonly int fDsrSensitivity;
            private static readonly int fTXContinueOnXoff;
            private static readonly int fOutX;
            private static readonly int fInX;
            private static readonly int fErrorChar;
            private static readonly int fNull;
            private static readonly BitVector32.Section fRtsControl;
            private static readonly int fAbortOnError;

            static DCB()
            {
                // Create Boolean Mask
                int previousMask;
                fBinary = BitVector32.CreateMask();
                fParity = BitVector32.CreateMask(fBinary);
                fOutxCtsFlow = BitVector32.CreateMask(fParity);
                fOutxDsrFlow = BitVector32.CreateMask(fOutxCtsFlow);
                previousMask = BitVector32.CreateMask(fOutxDsrFlow);
                previousMask = BitVector32.CreateMask(previousMask);
                fDsrSensitivity = BitVector32.CreateMask(previousMask);
                fTXContinueOnXoff = BitVector32.CreateMask(fDsrSensitivity);
                fOutX = BitVector32.CreateMask(fTXContinueOnXoff);
                fInX = BitVector32.CreateMask(fOutX);
                fErrorChar = BitVector32.CreateMask(fInX);
                fNull = BitVector32.CreateMask(fErrorChar);
                previousMask = BitVector32.CreateMask(fNull);
                previousMask = BitVector32.CreateMask(previousMask);
                fAbortOnError = BitVector32.CreateMask(previousMask);

                // Create section Mask
                BitVector32.Section previousSection;
                previousSection = BitVector32.CreateSection(1);
                previousSection = BitVector32.CreateSection(1, previousSection);
                previousSection = BitVector32.CreateSection(1, previousSection);
                previousSection = BitVector32.CreateSection(1, previousSection);
                fDtrControl = BitVector32.CreateSection(2, previousSection);
                previousSection = BitVector32.CreateSection(1, fDtrControl);
                previousSection = BitVector32.CreateSection(1, previousSection);
                previousSection = BitVector32.CreateSection(1, previousSection);
                previousSection = BitVector32.CreateSection(1, previousSection);
                previousSection = BitVector32.CreateSection(1, previousSection);
                previousSection = BitVector32.CreateSection(1, previousSection);
                fRtsControl = BitVector32.CreateSection(3, previousSection);
                previousSection = BitVector32.CreateSection(1, fRtsControl);
            }

            public bool Binary
            {
                get { return Flags[fBinary]; }
                set { Flags[fBinary] = value; }
            }

            public bool CheckParity
            {
                get { return Flags[fParity]; }
                set { Flags[fParity] = value; }
            }

            public bool OutxCtsFlow
            {
                get { return Flags[fOutxCtsFlow]; }
                set { Flags[fOutxCtsFlow] = value; }
            }

            public bool OutxDsrFlow
            {
                get { return Flags[fOutxDsrFlow]; }
                set { Flags[fOutxDsrFlow] = value; }
            }

            public DtrControl DtrControl
            {
                get { return (DtrControl)Flags[fDtrControl]; }
                set { Flags[fDtrControl] = (int)value; }
            }

            public bool DsrSensitivity
            {
                get { return Flags[fDsrSensitivity]; }
                set { Flags[fDsrSensitivity] = value; }
            }

            public bool TxContinueOnXoff
            {
                get { return Flags[fTXContinueOnXoff]; }
                set { Flags[fTXContinueOnXoff] = value; }
            }

            public bool OutX
            {
                get { return Flags[fOutX]; }
                set { Flags[fOutX] = value; }
            }

            public bool InX
            {
                get { return Flags[fInX]; }
                set { Flags[fInX] = value; }
            }

            public bool ReplaceErrorChar
            {
                get { return Flags[fErrorChar]; }
                set { Flags[fErrorChar] = value; }
            }

            public bool Null
            {
                get { return Flags[fNull]; }
                set { Flags[fNull] = value; }
            }

            public RtsControl RtsControl
            {
                get { return (RtsControl)Flags[fRtsControl]; }
                set { Flags[fRtsControl] = (int)value; }
            }

            public bool AbortOnError
            {
                get { return Flags[fAbortOnError]; }
                set { Flags[fAbortOnError] = value; }
            }

        }

        #endregion
    }
}
