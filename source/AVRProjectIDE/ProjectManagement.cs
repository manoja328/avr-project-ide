using System;
using System.IO;
using System.Xml;
using System.Xml.XPath;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Reflection;

namespace AVRProjectIDE
{
    public class ProjectFile : ICloneable
    {
        #region Fields and Properties

        private AVRProject project;

        private string fileAbsPath;
        public string FileAbsPath
        {
            get { return Program.CleanFilePath(fileAbsPath); }
            set { fileAbsPath = Program.CleanFilePath(value); }
        }

        public string FileName
        {
            get { return Path.GetFileName(FileAbsPath); }
            set { FileAbsPath = FileDir + Path.DirectorySeparatorChar + value.Trim(); }
        }

        public string FileNameNoExt
        {
            get
            {
                if (Path.HasExtension(FileAbsPath))
                    return Path.GetFileNameWithoutExtension(FileAbsPath);
                else
                    return FileName;
            }

            set { FileAbsPath = FileDir + Path.DirectorySeparatorChar + value.Trim() + "." + FileExt; }
        }

        public string FileRelProjPath
        {
            get
            {
                return FileRelPathTo(project.DirPath);
            }
        }

        public string FileRelPathTo(string projDirPath)
        {
            return Program.RelativePath(projDirPath, FileAbsPath);
        }

        public string FileDir
        {
            get { return FileAbsPath.Substring(0, FileAbsPath.LastIndexOf(Path.DirectorySeparatorChar)); }
        }

        public string FileExt
        {
            get
            {
                if (Path.HasExtension(FileAbsPath))
                    return Path.GetExtension(FileAbsPath).ToLowerInvariant().TrimStart('.');
                else
                    return FileName;
            }

            set { FileAbsPath = Path.ChangeExtension(FileAbsPath, value.Trim()); }
        }

        public string BackupPath
        {
            get { return FileAbsPath + ".backup"; }
        }

        public bool BackupExists
        {
            get { return File.Exists(BackupPath); }
        }

        private string options;
        public string Options
        {
            get { return options.Trim(); }
            set { options = value.Trim(); }
        }

        private bool toCompile;
        public bool ToCompile
        {
            get { return toCompile; }
            set { toCompile = value; }
        }

        public bool Exists
        {
            get
            {
                return File.Exists(FileAbsPath);
            }
        }

        private bool isOpen;
        public bool IsOpen
        {
            get { return isOpen; }
            set { isOpen = value; }
        }

        private List<int> bookMarks = new List<int>();
        public List<int> Bookmarks
        {
            get { return bookMarks; }
        }

        private Dictionary<int, int> foldedLines = new Dictionary<int, int>();
        public Dictionary<int, int> FoldedLines
        {
            get { return foldedLines; }
        }

        private int cursorPos = 0;
        public int CursorPos
        {
            get { return cursorPos; }
            set { cursorPos = value; }
        }

        private TreeNode treeNode;
        public TreeNode Node
        {
            get { return treeNode; }
        }

        #endregion

        public ProjectFile(string fileAbsPath, AVRProject project)
        {
            this.project = project;

            FileAbsPath = fileAbsPath;
            if (FileExt == "c" || FileExt == "s" || FileExt == "cpp" || FileExt == "cxx" || FileExt == "pde")
                ToCompile = true;
            options = "";
            isOpen = false;

            treeNode = new TreeNode(FileName);

            treeNode.ImageKey = "file.ico";
            treeNode.SelectedImageKey = "file.ico";
            treeNode.StateImageKey = "file.ico";

            treeNode.Checked = toCompile;
        }

        public void DeleteBackup()
        {
            if (BackupExists)
            {
                try
                {
                    File.Delete(BackupPath);
                }
                catch (Exception ex)
                {
                    ErrorReportWindow erw = new ErrorReportWindow(ex, "Error Deleting Backup");
                    erw.ShowDialog();
                }
            }
        }

        #region ICloneable Members

        /// <summary>
        /// Makes a copy of ProjectFile, used by the background worker of the project builder
        /// </summary>
        /// <returns>Reference to the Cloned ProjectFile</returns>
        public object Clone()
        {
            ProjectFile newFile = new ProjectFile(fileAbsPath, this.project);
            newFile.IsOpen = this.IsOpen;
            newFile.Options = this.Options;
            newFile.ToCompile = this.ToCompile;

            newFile.Bookmarks.Clear();
            foreach (int i in this.bookMarks)
                newFile.Bookmarks.Add(i);

            newFile.FoldedLines.Clear();
            foreach (KeyValuePair<int, int> i in this.foldedLines)
                newFile.FoldedLines.Add(i.Key, i.Value);

            return newFile;
        }

        #endregion
    }

    public class AVRProject : ICloneable
    {
        #region Project File Fields and Properties

        private bool shouldReloadFiles = false;
        public bool ShouldReloadFiles
        {
            get { return shouldReloadFiles; }
            set { shouldReloadFiles = value; }
        }

        private bool shouldReloadDevice = false;
        public bool ShouldReloadDevice
        {
            get { return shouldReloadDevice; }
            set { shouldReloadDevice = value; }
        }

        private bool shouldReloadClock = false;
        public bool ShouldReloadClock
        {
            get { return shouldReloadClock; }
            set { shouldReloadClock = value; }
        }

        private bool hasBeenConfigged = false;
        public bool HasBeenConfigged
        {
            get { return hasBeenConfigged; }
            set { hasBeenConfigged = value; }
        }

        private string filePath;
        public string FilePath
        {
            get
            {
                if (filePath == null)
                    return null;
                else
                    return Program.CleanFilePath(filePath);
            }

            set
            {
                filePath = Program.CleanFilePath(value);
                dirPath = filePath.Substring(0, filePath.LastIndexOf(Path.DirectorySeparatorChar));
            }
        }

        private string dirPath;
        public string DirPath
        {
            get
            {
                string path = Program.CleanFilePath(filePath);
                if (path == null)
                    return null;
                else
                    return path.Substring(0, path.LastIndexOf(Path.DirectorySeparatorChar));
            }
        }

        public string FileName
        {
            get { return Path.GetFileName(filePath); }
        }

        public string FileNameNoExt
        {
            get
            {
                if (Path.HasExtension(filePath))
                    return Path.GetFileNameWithoutExtension(filePath);
                else
                    return FileName;
            }
        }

        public string SafeFileNameNoExt
        {
            get
            {
                return FileNameNoExt.Replace(" ", "_");
            }
        }

        private string lastFile;
        public string LastFile
        {
            get
            {
                if (lastFile == null)
                    return null;
                else
                    return lastFile.Trim();
            }
            set { lastFile = value.Trim(); }
        }

        public bool Exportable
        {
            get
            {
                foreach (ProjectFile file in fileList.Values)
                {
                    if (file.FileExt == "cpp" || file.FileExt == "cxx" || file.FileExt == "pde")
                        return false;
                }

                return true;
            }
        }

        public bool ContainsAPS
        {
            get { return APSXmlElementList.Count > 0; }
        }

        public bool HasArduino
        {
            get { return UseArduinoMakefile; }
        }

        public bool UseArduinoMakefile
        {
            get
            {
                foreach (ProjectFile file in fileList.Values)
                {
                    if (file.FileExt == "pde" && file.ToCompile)
                        return true;
                }
                return false;
            }
        }

        private bool isReady;
        public bool IsReady
        {
            get { return isReady; }
            set { isReady = value; }
        }

        #endregion

        #region Compilation Settings Fields and Properties

        private bool unsignedChars;
        public bool UnsignedChars
        {
            get { return unsignedChars; }
            set { unsignedChars = value; }
        }

        private bool unsignedBitfields;
        public bool UnsignedBitfields
        {
            get { return unsignedBitfields; }
            set { unsignedBitfields = value; }
        }

        private bool packStructs;
        public bool PackStructs
        {
            get { return packStructs; }
            set { packStructs = value; }
        }

        private bool shortEnums;
        public bool ShortEnums
        {
            get { return shortEnums; }
            set { shortEnums = value; }
        }

        private bool functSects;
        public bool FunctionSections
        {
            get { return functSects; }
            set { functSects = value; }
        }

        private bool dataSects;
        public bool DataSections
        {
            get { return dataSects; }
            set { dataSects = value; }
        }

        private bool useInitStack;
        public bool UseInitStack
        {
            get { return useInitStack; }
            set { useInitStack = value; }
        }

        private uint initStackAddr;
        public uint InitStackAddr
        {
            get { return initStackAddr; }
            set { initStackAddr = value; }
        }

        private string otherOpt;
        public string OtherOptions
        {
            get { return otherOpt.Trim(); }
            set { otherOpt = value.Trim(); }
        }

        private string otherOptForC;
        public string OtherOptionsForC
        {
            get { return otherOptForC.Trim(); }
            set { otherOptForC = value.Trim(); }
        }

        private string otherOptForCPP;
        public string OtherOptionsForCPP
        {
            get { return otherOptForCPP.Trim(); }
            set { otherOptForCPP = value.Trim(); }
        }

        private string otherOptForS;
        public string OtherOptionsForS
        {
            get { return otherOptForS.Trim(); }
            set { otherOptForS = value.Trim(); }
        }

        private string linkerOpt;
        public string LinkerOptions
        {
            get { return linkerOpt.Trim(); }
            set { linkerOpt = value.Trim(); }
        }

        private string optimization;
        public string Optimization
        {
            get { return optimization; }
            set
            {
                char c = value[value.IndexOf('O') + 1];
                if (c != '0' || c != '1' || c != '2' || c != '3')
                {
                    c = 's';
                }
                optimization = "-O" + c;
            }
        }

        private decimal clkFreq;
        public decimal ClockFreq
        {
            get { return clkFreq; }
            set { clkFreq = value; }
        }

        private string device;
        public string Device
        {
            get { return device.Trim(); }
            set { device = value.Trim(); }
        }

        private string outputDir;
        public string OutputDir
        {
            get { return outputDir; }
            set
            {
                string str = Program.CleanFilePath(value);
                if (string.IsNullOrEmpty(str))
                {
                    str = ".";
                }
                outputDir = str.Replace(' ', '_');
            }
        }

        #endregion

        #region AVRDUDE Fields and Properties

        private string burnPart;
        public string BurnPart
        {
            get { return burnPart.Trim(); }
            set { burnPart = value.Trim(); }
        }

        private string burnProg;
        public string BurnProgrammer
        {
            get { return burnProg.Trim(); }
            set { burnProg = value.Trim(); }
        }

        private string burnOpt;
        public string BurnOptions
        {
            get { return burnOpt.Trim(); }
            set { burnOpt = value.Trim(); }
        }

        private string burnPort;
        public string BurnPort
        {
            get { return burnPort.Trim(); }
            set { burnPort = value.Trim(); }
        }

        private int burnBaud;
        public int BurnBaud
        {
            get { return burnBaud; }
            set { burnBaud = value; }
        }

        private bool burnAutoReset;
        public bool BurnAutoReset
        {
            get { return burnAutoReset; }
            set { burnAutoReset = value; }
        }

        private string burnFuseBox;
        public string BurnFuseBox
        {
            get { return burnFuseBox.Trim(); }
            set { burnFuseBox = value.Trim(); }
        }

        #endregion

        #region Lists

        private Dictionary<string, ProjectFile> fileList = new Dictionary<string, ProjectFile>();
        public Dictionary<string, ProjectFile> FileList
        {
            get { return fileList; }
            set { fileList = value; }
        }

        private List<string> includeDirList = new List<string>();
        public List<string> IncludeDirList
        {
            get { return includeDirList; }
            set { includeDirList = value; }
        }

        private List<string> libraryDirList = new List<string>();
        public List<string> LibraryDirList
        {
            get { return libraryDirList; }
            set { libraryDirList = value; }
        }

        private List<string> linkObjList = new List<string>();
        public List<string> LinkObjList
        {
            get { return linkObjList; }
            set { linkObjList = value; }
        }

        private List<string> linkLibList = new List<string>();
        public List<string> LinkLibList
        {
            get { return linkLibList; }
            set { linkLibList = value; }
        }

        private Dictionary<string, MemorySegment> memorySegList = new Dictionary<string, MemorySegment>();
        public Dictionary<string, MemorySegment> MemorySegList
        {
            get { return memorySegList; }
            set { memorySegList = value; }
        }

        private List<XmlElement> APSXmlElementList = new List<XmlElement>();

        #endregion

        public AVRProject()
        {
            Reset();
        }

        #region XML Saving and Loading

        public SaveResult Save()
        {
            if (string.IsNullOrEmpty(filePath) == false)
            {
                if (Save(filePath))
                {
                    return SaveResult.Successful;
                }
                else
                {
                    return SaveResult.Failed;
                }
            }
            else
            {
                SaveResult res = CreateNew();
                if (res == SaveResult.Successful)
                {
                    return Save();
                }
                else
                {
                    return res;
                }
            }
        }

        public bool Save(string path)
        {
            bool success = true;
            XmlTextWriter xml = null;

            //LoadWaitWindow lww = new LoadWaitWindow();
            //lww.Show();

            try
            {
                xml = new XmlTextWriter(path, null);

                xml.Indentation = 4;
                xml.Formatting = Formatting.Indented;
                xml.WriteStartDocument();

                xml.WriteStartElement("Project");

                xml.WriteElementString("DirPath", DirPath);

                xml.WriteElementString("HasBeenConfigged", HasBeenConfigged.ToString().ToLowerInvariant().Trim());

                xml.WriteElementString("Device", Device);
                SettingsManagement.LastChipChoosen = Device;
                xml.WriteElementString("ClockFreq", ClockFreq.ToString("0.00"));
                SettingsManagement.LastClockChoosen = ClockFreq;
                xml.WriteElementString("LinkerOpt", LinkerOptions);
                xml.WriteElementString("OtherOpt", OtherOptions);
                xml.WriteElementString("OtherOptionsForC", OtherOptionsForC);
                xml.WriteElementString("OtherOptionsForCPP", OtherOptionsForCPP);
                xml.WriteElementString("OtherOptionsForS", OtherOptionsForS);
                xml.WriteElementString("OutputDir", OutputDir);
                xml.WriteElementString("Optimization", Optimization);

                xml.WriteElementString("UseInitStack", UseInitStack.ToString().ToLowerInvariant());
                xml.WriteElementString("InitStackAddr", "0x" + Convert.ToString(InitStackAddr, 16).ToUpper());

                xml.WriteElementString("PackStructs", PackStructs.ToString().ToLowerInvariant());
                xml.WriteElementString("ShortEnums", ShortEnums.ToString().ToLowerInvariant());
                xml.WriteElementString("UnsignedBitfields", UnsignedBitfields.ToString().ToLowerInvariant());
                xml.WriteElementString("UnsignedChars", UnsignedChars.ToString().ToLowerInvariant());
                xml.WriteElementString("FunctionSections", FunctionSections.ToString().ToLowerInvariant());
                xml.WriteElementString("DataSections", DataSections.ToString().ToLowerInvariant());

                xml.WriteStartElement("IncludeDirList");
                foreach (string i in IncludeDirList)
                {
                    xml.WriteElementString("DirPath", i);
                }
                xml.WriteEndElement();

                xml.WriteStartElement("LibraryDirList");
                foreach (string i in LibraryDirList)
                {
                    xml.WriteElementString("DirPath", i);
                }
                xml.WriteEndElement();

                xml.WriteStartElement("LinkObjList");
                foreach (string i in LinkObjList)
                {
                    xml.WriteElementString("Obj", i);
                }
                xml.WriteEndElement();

                xml.WriteStartElement("LinkLibList");
                foreach (string i in LinkLibList)
                {
                    xml.WriteElementString("Lib", i);
                }
                xml.WriteEndElement();

                xml.WriteStartElement("MemorySegList");
                foreach (MemorySegment i in MemorySegList.Values)
                {
                    xml.WriteStartElement("Segment");
                    xml.WriteElementString("Name", i.Name);
                    xml.WriteElementString("Type", i.Type);
                    xml.WriteElementString("Addr", "0x" + Convert.ToString(i.Addr, 16).ToUpper());
                    xml.WriteEndElement();
                }
                xml.WriteEndElement();

                xml.WriteElementString("BurnPart", BurnPart);
                xml.WriteElementString("BurnProgrammer", BurnProgrammer);
                SettingsManagement.LastProgChoosen = BurnProgrammer;
                xml.WriteElementString("BurnOptions", BurnOptions);
                xml.WriteElementString("BurnPort", BurnPort);
                SettingsManagement.LastProgPortChoosen = BurnPort;
                xml.WriteElementString("BurnFuseBox", BurnFuseBox);
                xml.WriteElementString("BurnBaud", BurnBaud.ToString("0"));
                SettingsManagement.LastProgBaud = BurnBaud;
                xml.WriteElementString("BurnAutoReset", BurnAutoReset.ToString().ToLowerInvariant());
                SettingsManagement.LastProgAutoReset = BurnAutoReset;

                xml.WriteElementString("LastFile", LastFile);

                xml.WriteStartElement("FileList");
                foreach (KeyValuePair<string, ProjectFile> file in FileList)
                {
                    xml.WriteStartElement("File");
                    xml.WriteElementString("AbsPath", file.Value.FileAbsPath);
                    xml.WriteElementString("RelPath", file.Value.FileRelPathTo(DirPath));
                    xml.WriteElementString("ToCompile", file.Value.ToCompile.ToString().ToLowerInvariant());
                    xml.WriteElementString("Options", file.Value.Options);
                    xml.WriteElementString("WasOpen", file.Value.IsOpen.ToString().ToLowerInvariant());

                    xml.WriteElementString("CursorPos", file.Value.CursorPos.ToString("0"));

                    xml.WriteStartElement("Bookmarks");
                    foreach (int i in file.Value.Bookmarks)
                        xml.WriteString(i.ToString("0") + ",");
                    xml.WriteEndElement();

                    //xml.WriteStartElement("FoldedLines");
                    //foreach (KeyValuePair<int, int> i in file.Value.FoldedLines)
                    //    xml.WriteString(i.Key.ToString("0") + ":" + i.Value + ",");
                    //xml.WriteEndElement();

                    xml.WriteEndElement();
                }
                xml.WriteEndElement();

                xml.WriteStartElement("AVRStudioXMLStuff");
                foreach (XmlElement xEle in APSXmlElementList)
                {
                    xEle.WriteTo(xml);
                }
                xml.WriteEndElement();

                xml.WriteEndElement();

                xml.WriteEndDocument();
            }
            catch
            {
                success = false;
            }
            try
            {
                xml.Close();
            }
            catch
            {
                success = false;
            }

            //lww.Close();

            return success;
        }

        public bool Open(string path)
        {
            if (string.IsNullOrEmpty(path))
                return false;

            path = Program.CleanFilePath(path);

            if (File.Exists(path) == false)
                return false;

            //LoadWaitWindow lww = new LoadWaitWindow();
            //lww.Show();

            bool success = true;

            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(path);

                XmlElement docx = doc.DocumentElement;

                string xDirPath = path.Substring(0, path.LastIndexOf(Path.DirectorySeparatorChar));
                foreach (XmlElement param in docx.GetElementsByTagName("DirPath"))
                {
                    xDirPath = Program.CleanFilePath(param.InnerText);
                }

                foreach (XmlElement param in docx.GetElementsByTagName("HasBeenConfigged"))
                {
                    HasBeenConfigged = Program.StringToBool(param);
                }

                foreach (XmlElement param in docx.GetElementsByTagName("OutputDir"))
                {
                    OutputDir = param.InnerText;
                }

                LibraryDirList.Clear();
                LinkLibList.Clear();
                IncludeDirList.Clear();
                LinkObjList.Clear();
                MemorySegList.Clear();

                int appCnt = 0;

                LoadTemplateCommonProperties(ref appCnt, docx, this);

                foreach (XmlElement param in docx.GetElementsByTagName("BurnPort"))
                {
                    BurnPort = param.InnerText;
                }

                foreach (XmlElement param in docx.GetElementsByTagName("LastFile"))
                {
                    LastFile = param.InnerText;
                }

                FileList.Clear();
                foreach (XmlElement container in docx.GetElementsByTagName("FileList"))
                {
                    string dirPath = Program.CleanFilePath(path);
                    dirPath = dirPath.Substring(0, dirPath.LastIndexOf('\\'));

                    List<ProjectFile> flistOld = new List<ProjectFile>();
                    List<ProjectFile> flistNew = new List<ProjectFile>();

                    foreach (XmlElement i in container.GetElementsByTagName("File"))
                    {
                        foreach (XmlElement relPath in i.GetElementsByTagName("RelPath"))
                        {
                            string newPath = dirPath + Path.DirectorySeparatorChar + Program.CleanFilePath(relPath.InnerText);
                            string oldPath = xDirPath + Path.DirectorySeparatorChar + Program.CleanFilePath(relPath.InnerText);

                            ProjectFile newFile = new ProjectFile(newPath, this);

                            foreach (XmlElement toComp in i.GetElementsByTagName("ToCompile"))
                            {
                                newFile.ToCompile = Program.StringToBool(toComp);
                            }

                            foreach (XmlElement opt in i.GetElementsByTagName("Options"))
                            {
                                newFile.Options = opt.InnerText.Trim();
                            }

                            foreach (XmlElement wasOpen in i.GetElementsByTagName("WasOpen"))
                            {
                                newFile.IsOpen = Program.StringToBool(wasOpen);
                            }

                            foreach (XmlElement cursorPos in i.GetElementsByTagName("CursorPos"))
                            {
                                int curPos = 0;
                                if (int.TryParse(cursorPos.InnerText, out curPos))
                                    newFile.CursorPos = curPos;
                            }

                            newFile.Bookmarks.Clear();
                            foreach (XmlElement bookMarks in i.GetElementsByTagName("Bookmarks"))
                            {
                                string tmp = bookMarks.InnerText;
                                string[] bkmkListStr = tmp.Split(new char[] {',', ' ', '\t', '\n', '\r', '\0', });
                                foreach (string s in bkmkListStr)
                                {
                                    if (string.IsNullOrEmpty(s) == false)
                                    {
                                        int lineNum = -1;
                                        if (int.TryParse(s, out lineNum))
                                            newFile.Bookmarks.Add(lineNum);
                                    }
                                }
                            }

                            newFile.FoldedLines.Clear();
                            foreach (XmlElement foldedLines in i.GetElementsByTagName("FoldedLines"))
                            {
                                try
                                {
                                    string tmp = foldedLines.InnerText;
                                    string[] foldListStr = tmp.Split(new char[] { ',', ' ', '\t', '\n', '\r', '\0', });
                                    foreach (string s in foldListStr)
                                    {
                                        if (string.IsNullOrEmpty(s) == false)
                                        {
                                            string[] kv = s.Split(':');

                                            int lineNum = -1;
                                            int foldLvl = -1;
                                            if (int.TryParse(kv[0], out lineNum) && int.TryParse(kv[1], out foldLvl))
                                                newFile.FoldedLines.Add(lineNum, foldLvl);
                                        }
                                    }
                                }
                                catch { }
                            }

                            flistNew.Add(newFile);

                            if (xDirPath != dirPath)
                            {
                                ProjectFile oldFile = new ProjectFile(oldPath, this);

                                oldFile.ToCompile = newFile.ToCompile;
                                oldFile.Options = newFile.Options;
                                oldFile.IsOpen = newFile.IsOpen;
                                oldFile.CursorPos = newFile.CursorPos;

                                oldFile.Bookmarks.Clear();
                                foreach (int j in newFile.Bookmarks)
                                    oldFile.Bookmarks.Add(j);

                                oldFile.FoldedLines.Clear();
                                foreach (KeyValuePair<int, int> j in newFile.FoldedLines)
                                    oldFile.FoldedLines.Add(j.Key, j.Value);

                                flistOld.Add(oldFile);
                            }

                            break;
                        }
                    }

                    int newCnt = 0;
                    int oldCnt = 0;
                    int total = flistNew.Count;

                    if (flistOld.Count > 0)
                    {
                        for (int i = 0; i < total && newCnt < (total + 1) / 2 && oldCnt < (total + 1) / 2; i++)
                        {
                                newCnt++;
                            if (flistNew[i].Exists)
                            if (flistOld[i].Exists)
                                oldCnt++;
                        }
                    }
                    else
                    {
                        newCnt = total;
                    }

                    if (newCnt >= oldCnt)
                    {
                        foreach (ProjectFile file in flistNew)
                        {
                            if (fileList.ContainsKey(file.FileName) == false)
                                fileList.Add(file.FileName, file);
                        }
                        xDirPath = dirPath;
                    }
                    else
                    {
                        foreach (ProjectFile file in flistOld)
                        {
                            if (fileList.ContainsKey(file.FileName) == false)
                                fileList.Add(file.FileName, file);
                        }
                        dirPath = xDirPath;
                    }
                }

                APSXmlElementList.Clear();
                foreach (XmlElement container in docx.GetElementsByTagName("AVRStudioXMLStuff"))
                {
                    foreach (XmlElement i in container.ChildNodes)
                    {
                        APSXmlElementList.Add(i);
                    }
                }
            }
            catch { success = false; }

            if (success)
            {
                filePath = path;
                isReady = true;
                dirPath = filePath.Substring(0, filePath.LastIndexOf(Path.DirectorySeparatorChar));

                if (string.IsNullOrEmpty(SettingsManagement.FavFolder))
                {
                    SettingsManagement.FavFolder = dirPath.Substring(0, dirPath.LastIndexOf(Path.DirectorySeparatorChar));
                }
            }

            //lww.Close();

            return success;
        }

        public bool ImportAPS(string path)
        {
            bool success = true;

            try
            {
                path = Program.CleanFilePath(path);
                string pathDir = path.Substring(0, path.LastIndexOf(Path.DirectorySeparatorChar));

                dirPath = pathDir;

                XmlDocument doc = new XmlDocument();
                doc.Load(path);

                XmlElement docx = doc.DocumentElement;

                if (docx.GetElementsByTagName("AVRGCCPLUGIN").Count > 0)
                {
                    foreach (XmlElement param in docx.GetElementsByTagName("PART"))
                    {
                        Device = param.InnerText;
                        BurnPart = Device;
                    }

                    IncludeDirList.Clear();
                    foreach (XmlElement container in docx.GetElementsByTagName("INCDIRS"))
                    {
                        foreach (XmlElement i in container.GetElementsByTagName("INCLUDE"))
                        {
                            IncludeDirList.Add(Program.AbsPathFromRel(pathDir, i.InnerText));
                        }
                    }

                    LibraryDirList.Clear();
                    foreach (XmlElement container in docx.GetElementsByTagName("LIBDIRS"))
                    {
                        foreach (XmlElement i in container.GetElementsByTagName("LIBDIR"))
                        {
                            LibraryDirList.Add(Program.AbsPathFromRel(pathDir, i.InnerText));
                        }
                    }

                    LinkObjList.Clear();
                    foreach (XmlElement container in docx.GetElementsByTagName("LINKOBJECTS"))
                    {
                        foreach (XmlElement i in container.GetElementsByTagName("LINKOBJECT"))
                        {
                            LinkObjList.Add(Program.AbsPathFromRel(pathDir, i.InnerText));
                        }
                    }

                    LinkLibList.Clear();
                    foreach (XmlElement container in docx.GetElementsByTagName("LIBS"))
                    {
                        foreach (XmlElement i in container.GetElementsByTagName("LIB"))
                        {
                            LinkLibList.Add(i.InnerText);
                        }
                    }

                    MemorySegList.Clear();
                    foreach (XmlElement container in docx.GetElementsByTagName("SEGMENTS"))
                    {
                        foreach (XmlElement i in container.GetElementsByTagName("SEGMENT"))
                        {
                            if (i.HasChildNodes)
                            {
                                try
                                {
                                    XmlElement type = (XmlElement)i.GetElementsByTagName("SEGMENT")[0];
                                    XmlElement name = (XmlElement)i.GetElementsByTagName("NAME")[0];
                                    XmlElement addr = (XmlElement)i.GetElementsByTagName("ADDRESS")[0];
                                    uint address;
                                    if (addr.InnerText.ToLowerInvariant().StartsWith("0x"))
                                    {
                                        address = Convert.ToUInt32(addr.InnerText, 16);
                                    }
                                    else
                                    {
                                        address = Convert.ToUInt32("0x" + addr.InnerText, 16);
                                    }

                                    string nameStr = name.InnerText.Trim();
                                    string typeStr = type.InnerText.Trim();

                                    if (string.IsNullOrEmpty(nameStr) == false && string.IsNullOrEmpty(typeStr) == false)
                                    {
                                        MemorySegment seg = new MemorySegment(typeStr, nameStr, address);

                                        if (MemorySegList.ContainsKey(nameStr) == false)
                                            MemorySegList.Add(nameStr, seg);
                                    }
                                }
                                catch { }
                            }
                        }
                    }

                    if (docx.GetElementsByTagName("OPTIONSFORALL").Count > 0)
                    {
                        UnsignedChars = false;
                        UnsignedBitfields = false;
                        PackStructs = false;
                        ShortEnums = false;
                        OtherOptions = "";
                        XmlElement param = (XmlElement)docx.GetElementsByTagName("OPTIONSFORALL")[0];
                        string OPTIONSFORALL = param.InnerText.Trim();
                        string[] opts = OPTIONSFORALL.Split(new char[] { ' ', '\t', '\r', '\n', });
                        foreach (string o in opts)
                        {
                            if (string.IsNullOrEmpty(o) == false)
                            {
                                if (o == "-funsigned-char")
                                {
                                    UnsignedChars = true;
                                }
                                else if (o == "-funsigned-bitfields")
                                {
                                    UnsignedBitfields = true;
                                }
                                else if (o == "-fpack-struct")
                                {
                                    PackStructs = true;
                                }
                                else if (o == "-fshort-enums")
                                {
                                    ShortEnums = true;
                                }
                                else if (o == "-ffunction-sections")
                                {
                                    FunctionSections = true;
                                }
                                else if (o == "-fdata-sections")
                                {
                                    DataSections = true;
                                }
                                else if (o.StartsWith("-DF_CPU="))
                                {
                                    string oo = o.Substring("-DF_CPU=".Length).ToUpperInvariant();
                                    oo = oo.TrimEnd('L').TrimEnd('L');
                                    oo = oo.TrimEnd('I');
                                    oo = oo.TrimEnd('S');
                                    oo = oo.TrimEnd('C');
                                    oo = oo.TrimEnd('F');
                                    oo = oo.TrimEnd('D');
                                    oo = oo.TrimEnd('U');
                                    if (decimal.TryParse(oo, out clkFreq) == false)
                                    {
                                        ClockFreq = 8000000;
                                    }
                                }
                                else if (o.StartsWith("-O"))
                                {
                                    Optimization = o;
                                }
                                else
                                {
                                    OtherOptions += " " + o;
                                }
                            }
                        }
                    }

                    foreach (XmlElement param in docx.GetElementsByTagName("LINKEROPTIONS"))
                    {
                        UseInitStack = false;
                        string[] linkerOptList = param.InnerText.Split(new char[] { ' ', '\t', '\r', '\n', });
                        foreach (string o in linkerOptList)
                        {
                            if (o.StartsWith("-Wl,--defsym=__stack="))
                            {
                                UseInitStack = true;
                                if (uint.TryParse(o.Substring("-Wl,--defsym=__stack=".Length), out initStackAddr) == false)
                                    initStackAddr = uint.MaxValue;
                            }
                            else
                            {
                                LinkerOptions += " " + param.InnerText;
                            }
                        }
                    }

                    foreach (XmlElement param in docx.GetElementsByTagName("OUTPUTDIR"))
                    {
                        OutputDir = Program.CleanFilePath(param.InnerText);
                    }
                }

                foreach (XmlElement param in docx.GetElementsByTagName("SaveFolder"))
                {
                    dirPath = Program.CleanFilePath(param.InnerText);
                }

                fileList.Clear();
                if (docx.GetElementsByTagName("ProjectFiles").Count > 0)
                {
                    XmlElement xProjFilesContainer = (XmlElement)docx.GetElementsByTagName("ProjectFiles")[0];

                    foreach (XmlElement i in xProjFilesContainer.GetElementsByTagName("Name"))
                    {
                        ProjectFile file = new ProjectFile(i.InnerText, this);
                        fileList.Add(file.FileName, file);
                    }
                }
                else if (docx.GetElementsByTagName("AVRGCCPLUGIN").Count > 0)
                {
                    XmlElement pluginContainer = (XmlElement)docx.GetElementsByTagName("AVRGCCPLUGIN")[0];

                    foreach (XmlElement filesContainer in pluginContainer.GetElementsByTagName("FILES"))
                    {
                        foreach (XmlElement i in filesContainer.ChildNodes)
                        {
                            ProjectFile file = new ProjectFile(Program.AbsPathFromRel(dirPath, i.InnerText), this);
                            fileList.Add(file.FileName, file);
                        }
                    }
                }

                foreach (XmlElement pluginContainer in docx.GetElementsByTagName("AVRGCCPLUGIN"))
                {
                    foreach (XmlElement optionsContainer in pluginContainer.GetElementsByTagName("OPTIONS"))
                    {
                        foreach (XmlElement i in optionsContainer.GetElementsByTagName("OPTION"))
                        {
                            if (i.GetElementsByTagName("FILE").Count > 0 && i.GetElementsByTagName("OPTIONLIST").Count > 0)
                            {
                                XmlElement param = (XmlElement)i.GetElementsByTagName("FILE")[0];
                                string fileName = Program.CleanFilePath(param.InnerText);
                                param = (XmlElement)i.GetElementsByTagName("OPTIONLIST")[0];
                                string optionsForFile = param.InnerText;

                                ProjectFile file;
                                if (fileList.TryGetValue(fileName, out file))
                                {
                                    file.Options = optionsForFile;
                                }
                            }
                        }
                    }
                }

                APSXmlElementList.Clear();
                string[] otherXEles = new string[] {"AVRSimulator", "DEBUG_TARGET", "Debugger", "IOView", "Events", "Trace", };
                foreach (string xEleName in otherXEles)
                {
                    if (docx.GetElementsByTagName(xEleName).Count > 0)
                    {
                        APSXmlElementList.Add((XmlElement)docx.GetElementsByTagName(xEleName)[0]);
                    }
                }

            }
            catch { success = false; }

            if (success)
            {
                isReady = true;
            }

            return success;
        }

        public bool ExportAPS(string path)
        {
            bool success = true;
            XmlTextWriter xml = null;

            //LoadWaitWindow lww = new LoadWaitWindow();
            //lww.Show();

            try
            {
                xml = new XmlTextWriter(path, null);

                xml.Indentation = 4;
                xml.Formatting = Formatting.Indented;
                xml.WriteStartDocument();

                xml.WriteStartElement("AVRStudio");

                xml.WriteStartElement("MANAGEMENT");
                {
                    xml.WriteElementString("ProjectName", this.SafeFileNameNoExt);
                    xml.WriteElementString("ProjectType", "0");
                    xml.WriteElementString("ProjectTypeName", "AVR GCC");
                }
                xml.WriteEndElement();

                xml.WriteStartElement("CODE_CREATION");
                {
                    xml.WriteElementString("ObjectFile", OutputDir + Path.DirectorySeparatorChar + this.FileNameNoExt + ".elf");
                    xml.WriteElementString("EntryFile", "");
                    xml.WriteElementString("SaveFolder", DirPath + Path.DirectorySeparatorChar);
                }
                xml.WriteEndElement();

                xml.WriteStartElement("AVRGCCPLUGIN");
                {
                    xml.WriteStartElement("FILES");
                    {
                        foreach (ProjectFile file in fileList.Values)
                        {
                            if (file.FileExt == "c" && file.ToCompile)
                            {
                                xml.WriteElementString("SOURCEFILE", file.FileRelPathTo(DirPath));
                            }
                            else if (file.FileExt == "h" || file.FileExt == "hpp")
                            {
                                xml.WriteElementString("HEADERFILE", file.FileRelPathTo(DirPath));
                            }
                            else
                            {
                                xml.WriteElementString("OTHERFILE", file.FileRelPathTo(DirPath));
                            }
                        }
                    }
                    xml.WriteEndElement();

                    xml.WriteStartElement("CONFIGS");
                    {
                        xml.WriteStartElement("CONFIG");
                        {
                            xml.WriteElementString("NAME", "default");
                            xml.WriteElementString("USESEXTERNALMAKEFILE", "NO");
                            xml.WriteElementString("EXTERNALMAKEFILE", "");
                            xml.WriteElementString("PART", Device);
                            xml.WriteElementString("HEX", "1");
                            xml.WriteElementString("LIST", "1");
                            xml.WriteElementString("MAP", "1");
                            xml.WriteElementString("OUTPUTFILENAME", this.FileNameNoExt + ".elf");
                            xml.WriteElementString("OUTPUTDIR", OutputDir);
                            xml.WriteElementString("ISDIRTY", "0");

                            xml.WriteStartElement("OPTIONS");
                            {
                                foreach (ProjectFile file in fileList.Values)
                                {
                                    if (file.FileExt == "c" && file.ToCompile && string.IsNullOrEmpty(file.Options) == false)
                                    {
                                        xml.WriteStartElement("OPTION");
                                        {
                                            xml.WriteElementString("FILE", file.FileRelPathTo(DirPath));
                                            xml.WriteElementString("OPTIONLIST", file.Options);
                                        }
                                        xml.WriteEndElement();
                                    }
                                }
                            }
                            xml.WriteEndElement();

                            xml.WriteStartElement("INCDIRS");
                            {
                                foreach (string dir in IncludeDirList)
                                {
                                    xml.WriteElementString("INCLUDE", dir);
                                }
                            }
                            xml.WriteEndElement();

                            xml.WriteStartElement("LIBDIRS");
                            {
                                foreach (string dir in LibraryDirList)
                                {
                                    xml.WriteElementString("LIBDIR", dir);
                                }
                            }
                            xml.WriteEndElement();

                            xml.WriteStartElement("LIBS");
                            {
                                foreach (string dir in LinkLibList)
                                {
                                    xml.WriteElementString("LIB", dir);
                                }
                            }
                            xml.WriteEndElement();

                            xml.WriteStartElement("LINKOBJECTS");
                            {
                                foreach (string dir in LinkObjList)
                                {
                                    xml.WriteElementString("LINKOBJECTS", dir);
                                }
                            }
                            xml.WriteEndElement();

                            string checkList = "";

                            if (UnsignedChars)
                                checkList += "-funsigned-char ";
                            if (UnsignedBitfields)
                                checkList += "-funsigned-bitfields ";
                            if (PackStructs)
                                checkList += "-fpack-struct ";
                            if (ShortEnums)
                                checkList += "-fshort-enums ";
                            if (FunctionSections)
                                checkList += "-ffunction-sections ";
                            if (DataSections)
                                checkList += "-fdata-sections ";

                            string optForAll = String.Format("{0} -DF_CPU={1:0}UL {2} {3}", (OtherOptions + " " + OtherOptionsForC).Trim(), ClockFreq, Optimization, checkList.Trim()).Replace("  ", " ");
                            xml.WriteElementString("OPTIONSFORALL", optForAll);

                            string linkerOpts = "";
                            if (UseInitStack)
                                linkerOpts += "-Wl,--defsym=__stack=0x" + Convert.ToString(InitStackAddr, 16) + " ";
                            linkerOpts += LinkerOptions;
                            xml.WriteElementString("LINKEROPTIONS", linkerOpts.Trim());

                            xml.WriteStartElement("SEGMENTS");
                            {
                                foreach (MemorySegment seg in MemorySegList.Values)
                                {
                                    xml.WriteStartElement("SEGMENT");
                                    {
                                        xml.WriteElementString("NAME", seg.Name);
                                        xml.WriteElementString("SEGMENT", seg.Type.ToUpperInvariant());
                                        xml.WriteElementString("ADDRESS", "0x" + Convert.ToString(seg.Addr, 16));
                                    }
                                }
                            }
                            xml.WriteEndElement();
                        }
                        xml.WriteEndElement();
                    }
                    xml.WriteEndElement();

                    xml.WriteElementString("LASTCONFIG", "default");
                    xml.WriteElementString("USE_WINAVR", "1");
                }
                xml.WriteEndElement();

                xml.WriteStartElement("ProjectFiles");
                {
                    xml.WriteStartElement("Files");
                    {
                        foreach (ProjectFile file in fileList.Values)
                        {
                            if (file.FileExt == "c" && file.ToCompile)
                            {
                                xml.WriteElementString("Name", file.FileAbsPath);
                            }
                            else if (file.FileExt != "c")
                            {
                                xml.WriteElementString("Name", file.FileAbsPath);
                            }
                        }
                    }
                }
                xml.WriteEndElement();

                xml.WriteEndElement();

                foreach (XmlElement xEle in APSXmlElementList)
                {
                    xEle.WriteTo(xml);
                }

                xml.WriteEndDocument();

                xml.Close();
            }
            catch { success = false; }

            //lww.Close();

            return success;
        }

        #endregion

        public void Reset()
        {
            hasBeenConfigged = false;
            clkFreq = SettingsManagement.LastClockChoosen;
            device = SettingsManagement.LastChipChoosen;
            packStructs = true;
            shortEnums = true;
            unsignedBitfields = true;
            unsignedChars = true;
            functSects = true;
            dataSects = true;
            useInitStack = false;
            initStackAddr = ushort.MaxValue;
            otherOpt = "-Wall -gdwarf-2";
            otherOptForC = "-std=gnu99";
            otherOptForCPP = "-std=c99";
            otherOptForS = "";
            linkerOpt = "";
            outputDir = "output";
            optimization = "-Os";

            burnPart = device;
            burnProg = SettingsManagement.LastProgChoosen;
            burnOpt = "";
            burnBaud = SettingsManagement.LastProgBaud;
            if (SettingsManagement.LastProgPortChoosen.Trim().ToLowerInvariant() == "nooverride")
                burnPort = "";
            else
                burnPort = SettingsManagement.LastProgPortChoosen.Trim();
            burnFuseBox = "";
            burnAutoReset = SettingsManagement.LastProgAutoReset;

            lastFile = "";

            FileList.Clear();
            IncludeDirList.Clear();
            LibraryDirList.Clear();
            LinkObjList.Clear();
            LinkLibList.Clear();
            MemorySegList.Clear();
            APSXmlElementList.Clear();

            IsReady = false;
        }

        public SaveResult CreateNew()
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "AVR Project (*.avrproj)|*.avrproj";
            if (string.IsNullOrEmpty(DirPath) == false)
            {
                sfd.InitialDirectory = DirPath + Path.DirectorySeparatorChar;
            }
            else if (string.IsNullOrEmpty(SettingsManagement.FavFolder) == false)
            {
                sfd.InitialDirectory = SettingsManagement.FavFolder + Path.DirectorySeparatorChar;
            }

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                filePath = sfd.FileName;
                dirPath = filePath.Substring(0, filePath.LastIndexOf(Path.DirectorySeparatorChar));
                isReady = true;
                Save();
                return SaveResult.Successful;
            }
            return SaveResult.Cancelled;
        }

        #region ICloneable Members
        
        /// <summary>
        /// Makes a copy of AVRProject, used by the background worker of the project builder
        /// </summary>
        /// <returns>Reference to the Cloned AVRProject</returns>
        public object Clone()
        {
            AVRProject newObject = new AVRProject();

            newObject = CopyProperties(newObject);

            return newObject;
        }

        public AVRProject CopyProperties(AVRProject project)
        {
            //LoadWaitWindow lww = new LoadWaitWindow();
            //lww.Show();

            project.IsReady = this.IsReady;
            project.FilePath = this.FilePath;
            project.BurnOptions = this.BurnOptions;
            project.BurnPart = this.BurnPart;
            project.BurnProgrammer = this.BurnProgrammer;
            project.BurnBaud = this.BurnBaud;
            project.BurnPort = this.BurnPort;
            project.BurnFuseBox = this.BurnFuseBox;
            project.BurnAutoReset = this.BurnAutoReset;
            project.ClockFreq = this.ClockFreq;
            project.Device = this.Device;
            project.dirPath = this.DirPath;
            project.FilePath = this.FilePath;
            project.InitStackAddr = this.InitStackAddr;
            project.LinkerOptions = this.LinkerOptions;
            project.Optimization = this.Optimization;
            project.OtherOptions = this.OtherOptions;
            project.OtherOptionsForC = this.OtherOptionsForC;
            project.OtherOptionsForCPP = this.OtherOptionsForCPP;
            project.OtherOptionsForS = this.OtherOptionsForS;
            project.OutputDir = this.OutputDir;
            project.PackStructs = this.PackStructs;
            project.ShortEnums = this.ShortEnums;
            project.UnsignedBitfields = this.UnsignedBitfields;
            project.UnsignedChars = this.UnsignedChars;
            project.FunctionSections = this.FunctionSections;
            project.DataSections = this.DataSections;
            project.UseInitStack = this.UseInitStack;
            project.LastFile = this.LastFile;

            project.HasBeenConfigged = this.HasBeenConfigged;
            project.ShouldReloadDevice = this.ShouldReloadDevice;
            project.ShouldReloadFiles = this.ShouldReloadFiles;
            project.ShouldReloadClock = this.ShouldReloadClock;

            project.fileList = new Dictionary<string, ProjectFile>();
            project.fileList.Clear();
            foreach (KeyValuePair<string, ProjectFile> file in this.FileList)
            {
                ProjectFile newFile = (ProjectFile)file.Value.Clone();
                project.fileList.Add(file.Key, newFile);
            }

            project.includeDirList = new List<string>();
            project.includeDirList.Clear();
            foreach (string dir in this.IncludeDirList)
            {
                project.includeDirList.Add((string)dir.Clone());
            }

            project.libraryDirList = new List<string>();
            project.libraryDirList.Clear();
            foreach (string dir in this.LibraryDirList)
            {
                project.libraryDirList.Add((string)dir.Clone());
            }

            project.linkLibList = new List<string>();
            project.linkLibList.Clear();
            foreach (string obj in this.LinkLibList)
            {
                project.linkLibList.Add((string)obj.Clone());
            }

            project.linkObjList = new List<string>();
            project.linkObjList.Clear();
            foreach (string obj in this.LinkObjList)
            {
                project.linkObjList.Add((string)obj.Clone());
            }

            project.memorySegList = new Dictionary<string, MemorySegment>();
            project.memorySegList.Clear();
            foreach (MemorySegment obj in this.MemorySegList.Values)
            {
                project.memorySegList.Add(obj.Name, new MemorySegment(obj.Type, obj.Name, obj.Addr));
            }

            project.APSXmlElementList = new List<XmlElement>();
            project.APSXmlElementList.Clear();
            foreach (XmlElement obj in this.APSXmlElementList)
            {
                project.APSXmlElementList.Add((XmlElement)obj.Clone());
            }

            //lww.Close();

            return project;
        }

        #endregion

        internal static void LoadTemplateCommonProperties(ref int appCnt, XmlElement docx, AVRProject proj)
        {
            foreach (XmlElement param in docx.GetElementsByTagName("ClockFreq"))
            {
                proj.ClockFreq = decimal.Parse(param.InnerText);
                appCnt++;
            }
            foreach (XmlElement param in docx.GetElementsByTagName("Device"))
            {
                proj.Device = param.InnerText;
                appCnt++;
            }
            foreach (XmlElement param in docx.GetElementsByTagName("LinkerOpt"))
            {
                proj.LinkerOptions = param.InnerText;
            }
            foreach (XmlElement param in docx.GetElementsByTagName("OtherOpt"))
            {
                proj.OtherOptions = param.InnerText;
            }
            foreach (XmlElement param in docx.GetElementsByTagName("OtherOptionsForC"))
            {
                proj.OtherOptionsForC = param.InnerText;
            }
            foreach (XmlElement param in docx.GetElementsByTagName("OtherOptionsForCPP"))
            {
                proj.OtherOptionsForCPP = param.InnerText;
            }
            foreach (XmlElement param in docx.GetElementsByTagName("OtherOptionsForS"))
            {
                proj.OtherOptionsForS = param.InnerText;
            }
            foreach (XmlElement param in docx.GetElementsByTagName("Optimization"))
            {
                proj.Optimization = param.InnerText;
            }
            foreach (XmlElement param in docx.GetElementsByTagName("UseInitStack"))
            {
                proj.UseInitStack = Program.StringToBool(param);
            }
            foreach (XmlElement param in docx.GetElementsByTagName("InitStackAddr"))
            {
                try
                {
                    if (param.InnerText.ToLowerInvariant().StartsWith("0x"))
                    {
                        proj.InitStackAddr = Convert.ToUInt32(param.InnerText, 16);
                    }
                    else
                    {
                        proj.InitStackAddr = Convert.ToUInt32("0x" + param.InnerText, 16);
                    }
                }
                catch { }
            }
            foreach (XmlElement param in docx.GetElementsByTagName("PackStructs"))
            {
                proj.PackStructs = Program.StringToBool(param);
            }
            foreach (XmlElement param in docx.GetElementsByTagName("ShortEnums"))
            {
                proj.ShortEnums = Program.StringToBool(param);
            }
            foreach (XmlElement param in docx.GetElementsByTagName("UnsignedBitfields"))
            {
                proj.UnsignedBitfields = Program.StringToBool(param);
            }
            foreach (XmlElement param in docx.GetElementsByTagName("UnsignedChars"))
            {
                proj.UnsignedChars = Program.StringToBool(param);
            }
            foreach (XmlElement param in docx.GetElementsByTagName("FunctionSections"))
            {
                proj.FunctionSections = Program.StringToBool(param);
            }
            foreach (XmlElement param in docx.GetElementsByTagName("DataSections"))
            {
                proj.DataSections = Program.StringToBool(param);
            }

            foreach (XmlElement param in docx.GetElementsByTagName("BurnPart"))
            {
                proj.BurnPart = param.InnerText;
            }
            foreach (XmlElement param in docx.GetElementsByTagName("BurnProgrammer"))
            {
                proj.BurnProgrammer = param.InnerText;
            }
            foreach (XmlElement param in docx.GetElementsByTagName("BurnOptions"))
            {
                proj.BurnOptions = param.InnerText;
            }
            foreach (XmlElement param in docx.GetElementsByTagName("BurnBaud"))
            {
                try { proj.BurnBaud = int.Parse(param.InnerText); }
                catch { }
            }
            foreach (XmlElement param in docx.GetElementsByTagName("BurnAutoReset"))
            {
                proj.BurnAutoReset = Program.StringToBool(param);
            }
            foreach (XmlElement param in docx.GetElementsByTagName("BurnFuseBox"))
            {
                proj.BurnFuseBox = param.InnerText;
            }

            foreach (XmlElement container in docx.GetElementsByTagName("IncludeDirList"))
            {
                foreach (XmlElement i in container.GetElementsByTagName("DirPath"))
                {
                    if (proj.IncludeDirList.Contains(i.InnerText) == false)
                        proj.IncludeDirList.Add(i.InnerText);
                }
            }

            foreach (XmlElement container in docx.GetElementsByTagName("LibraryDirList"))
            {
                foreach (XmlElement i in container.GetElementsByTagName("DirPath"))
                {
                    if (proj.LibraryDirList.Contains(i.InnerText) == false)
                        proj.LibraryDirList.Add(i.InnerText);
                }
            }

            foreach (XmlElement container in docx.GetElementsByTagName("LinkObjList"))
            {
                foreach (XmlElement i in container.GetElementsByTagName("Obj"))
                {
                    if (proj.LinkObjList.Contains(i.InnerText) == false)
                        proj.LinkObjList.Add(i.InnerText);
                }
            }

            foreach (XmlElement container in docx.GetElementsByTagName("LinkLibList"))
            {
                foreach (XmlElement i in container.GetElementsByTagName("Lib"))
                {
                    if (proj.LinkLibList.Contains(i.InnerText) == false)
                        proj.LinkLibList.Add(i.InnerText);
                }
            }

            foreach (XmlElement container in docx.GetElementsByTagName("MemorySegList"))
            {
                foreach (XmlElement i in container.GetElementsByTagName("Segment"))
                {
                    try
                    {
                        XmlElement type = (XmlElement)i.GetElementsByTagName("Type")[0];
                        XmlElement name = (XmlElement)i.GetElementsByTagName("Name")[0];
                        XmlElement addr = (XmlElement)i.GetElementsByTagName("Addr")[0];
                        uint address;
                        if (addr.InnerText.ToLowerInvariant().StartsWith("0x"))
                        {
                            address = Convert.ToUInt32(addr.InnerText, 16);
                        }
                        else
                        {
                            address = Convert.ToUInt32("0x" + addr.InnerText, 16);
                        }

                        string nameStr = name.InnerText.Trim();
                        string typeStr = type.InnerText.Trim();

                        if (string.IsNullOrEmpty(nameStr) == false && string.IsNullOrEmpty(typeStr) == false)
                        {
                            MemorySegment seg = new MemorySegment(typeStr, nameStr, address);

                            if (proj.MemorySegList.ContainsKey(nameStr) == false)
                                proj.MemorySegList.Add(nameStr, seg);
                        }
                    }
                    catch { }
                }
            }
        }
    }
}