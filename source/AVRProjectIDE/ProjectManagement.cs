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

        public string FileRelPath(string projDirPath)
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

        #endregion

        public ProjectFile(string fileAbsPath)
        {
            FileAbsPath = fileAbsPath;
            if (FileExt == "c" || FileExt == "s" || FileExt == "cpp" || FileExt == "cxx" || FileExt == "pde")
                ToCompile = true;
            options = "";
            isOpen = false;
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
            ProjectFile newFile = new ProjectFile(fileAbsPath);
            newFile.IsOpen = this.IsOpen;
            newFile.Options = this.Options;
            newFile.ToCompile = this.ToCompile;
            return newFile;
        }

        #endregion
    }

    public class AVRProject : ICloneable
    {
        #region Project File Fields and Properties
        
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
            set { filePath = Program.CleanFilePath(value); }
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

        private List<MemorySegment> memorySegList = new List<MemorySegment>();
        public List<MemorySegment> MemorySegList
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
            try
            {
                xml = new XmlTextWriter(path, null);

                xml.Indentation = 4;
                xml.Formatting = Formatting.Indented;
                xml.WriteStartDocument();

                xml.WriteStartElement("Project");

                xml.WriteElementString("DirPath", DirPath);

                xml.WriteElementString("Device", Device);
                xml.WriteElementString("ClockFreq", ClockFreq.ToString());
                xml.WriteElementString("LinkerOpt", LinkerOptions);
                xml.WriteElementString("OtherOpt", OtherOptions);
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
                foreach (MemorySegment i in MemorySegList)
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
                xml.WriteElementString("BurnOptions", BurnOptions);
                xml.WriteElementString("BurnPort", BurnPort);
                xml.WriteElementString("BurnFuseBox", BurnFuseBox);
                xml.WriteElementString("BurnBaud", BurnBaud.ToString("0"));
                xml.WriteElementString("BurnAutoReset", BurnAutoReset.ToString().ToLowerInvariant());

                xml.WriteElementString("LastFile", LastFile);

                xml.WriteStartElement("FileList");
                foreach (KeyValuePair<string, ProjectFile> file in FileList)
                {
                    xml.WriteStartElement("File");
                    xml.WriteElementString("AbsPath", file.Value.FileAbsPath);
                    xml.WriteElementString("RelPath", file.Value.FileRelPath(DirPath));
                    xml.WriteElementString("ToCompile", file.Value.ToCompile.ToString().ToLowerInvariant());
                    xml.WriteElementString("Options", file.Value.Options);
                    xml.WriteElementString("WasOpen", file.Value.IsOpen.ToString().ToLowerInvariant());
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
            return success;
        }

        public bool Open(string path)
        {
            path = Program.CleanFilePath(path);

            bool success = true;

            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(path);

                XmlElement docx = doc.DocumentElement;

                //XmlElement param;
                string xDirPath = path.Substring(0, path.LastIndexOf(Path.DirectorySeparatorChar));
                foreach (XmlElement param in docx.GetElementsByTagName("DirPath"))
                {
                    xDirPath = Program.CleanFilePath(param.InnerText);
                }
                foreach (XmlElement param in docx.GetElementsByTagName("ClockFreq"))
                {
                    ClockFreq = decimal.Parse(param.InnerText);
                }
                foreach (XmlElement param in docx.GetElementsByTagName("Device"))
                {
                    Device = param.InnerText;
                }
                foreach (XmlElement param in docx.GetElementsByTagName("LinkerOpt"))
                {
                    LinkerOptions = param.InnerText;
                }
                foreach (XmlElement param in docx.GetElementsByTagName("OtherOpt"))
                {
                    OtherOptions = param.InnerText;
                }
                foreach (XmlElement param in docx.GetElementsByTagName("OutputDir"))
                {
                    OutputDir = param.InnerText;
                }
                foreach (XmlElement param in docx.GetElementsByTagName("Optimization"))
                {
                    Optimization = param.InnerText;
                }
                foreach (XmlElement param in docx.GetElementsByTagName("UseInitStack"))
                {
                    UseInitStack = param.InnerText.ToLowerInvariant().Trim() == "true";
                }
                foreach (XmlElement param in docx.GetElementsByTagName("InitStackAddr"))
                {
                    try
                    {
                        if (param.InnerText.ToLowerInvariant().StartsWith("0x"))
                        {
                            InitStackAddr = Convert.ToUInt32(param.InnerText, 16);
                        }
                        else
                        {
                            InitStackAddr = Convert.ToUInt32("0x" + param.InnerText, 16);
                        }
                    }
                    catch { }
                }
                foreach (XmlElement param in docx.GetElementsByTagName("PackStructs"))
                {
                    PackStructs = param.InnerText.ToLowerInvariant().Trim() == "true";
                }
                foreach (XmlElement param in docx.GetElementsByTagName("ShortEnums"))
                {
                    ShortEnums = param.InnerText.ToLowerInvariant().Trim() == "true";
                }
                foreach (XmlElement param in docx.GetElementsByTagName("UnsignedBitfields"))
                {
                    UnsignedBitfields = param.InnerText.ToLowerInvariant().Trim() == "true";
                }
                foreach (XmlElement param in docx.GetElementsByTagName("UnsignedChars"))
                {
                    UnsignedChars = param.InnerText.ToLowerInvariant().Trim() == "true";
                }
                foreach (XmlElement param in docx.GetElementsByTagName("FunctionSections"))
                {
                    FunctionSections = param.InnerText.ToLowerInvariant().Trim() == "true";
                }
                foreach (XmlElement param in docx.GetElementsByTagName("DataSections"))
                {
                    DataSections = param.InnerText.ToLowerInvariant().Trim() == "true";
                }

                foreach (XmlElement param in docx.GetElementsByTagName("BurnPart"))
                {
                    BurnPart = param.InnerText;
                }
                foreach (XmlElement param in docx.GetElementsByTagName("BurnProgrammer"))
                {
                    BurnProgrammer = param.InnerText;
                }
                foreach (XmlElement param in docx.GetElementsByTagName("BurnOptions"))
                {
                    BurnOptions = param.InnerText;
                }
                foreach (XmlElement param in docx.GetElementsByTagName("BurnPort"))
                {
                    BurnPort = param.InnerText;
                }
                foreach (XmlElement param in docx.GetElementsByTagName("BurnBaud"))
                {
                    try { BurnBaud = int.Parse(param.InnerText); }
                    catch { }
                }
                foreach (XmlElement param in docx.GetElementsByTagName("BurnAutoReset"))
                {
                    BurnAutoReset = param.InnerText.Trim().ToLowerInvariant() == "true";
                }
                foreach (XmlElement param in docx.GetElementsByTagName("BurnFuseBox"))
                {
                    BurnFuseBox = param.InnerText;
                }

                IncludeDirList.Clear();
                foreach (XmlElement container in docx.GetElementsByTagName("IncludeDirList"))
                {
                    foreach (XmlElement i in container.GetElementsByTagName("DirPath"))
                    {
                        IncludeDirList.Add(i.InnerText);
                    }
                }

                LibraryDirList.Clear();
                foreach (XmlElement container in docx.GetElementsByTagName("LibraryDirList"))
                {
                    foreach (XmlElement i in container.GetElementsByTagName("DirPath"))
                    {
                        LibraryDirList.Add(i.InnerText);
                    }
                }

                LinkObjList.Clear();
                foreach (XmlElement container in docx.GetElementsByTagName("LinkObjList"))
                {
                    foreach (XmlElement i in container.GetElementsByTagName("Obj"))
                    {
                        LinkObjList.Add(i.InnerText);
                    }
                }

                LinkLibList.Clear();
                foreach (XmlElement container in docx.GetElementsByTagName("LinkLibList"))
                {
                    foreach (XmlElement i in container.GetElementsByTagName("Lib"))
                    {
                        LinkLibList.Add(i.InnerText);
                    }
                }

                MemorySegList.Clear();
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
                            MemorySegList.Add(new MemorySegment(type.InnerText, name.InnerText, address));
                        }
                        catch { }
                    }
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

                            ProjectFile newFile = new ProjectFile(newPath);

                            foreach (XmlElement toComp in i.GetElementsByTagName("ToCompile"))
                            {
                                newFile.ToCompile = toComp.InnerText.ToLowerInvariant().Trim() == "true";
                            }

                            foreach (XmlElement opt in i.GetElementsByTagName("Options"))
                            {
                                newFile.Options = opt.InnerText.Trim();
                            }

                            foreach (XmlElement wasOpen in i.GetElementsByTagName("WasOpen"))
                            {
                                newFile.IsOpen = wasOpen.InnerText.ToLowerInvariant().Trim() == "true";
                            }

                            flistNew.Add(newFile);

                            if (xDirPath != dirPath)
                            {
                                ProjectFile oldFile = new ProjectFile(oldPath);

                                oldFile.ToCompile = newFile.ToCompile;
                                oldFile.Options = newFile.Options;
                                oldFile.IsOpen = newFile.IsOpen;

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
                                    MemorySegList.Add(new MemorySegment(type.InnerText, name.InnerText, address));
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
                        ProjectFile file = new ProjectFile(i.InnerText);
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
                            ProjectFile file = new ProjectFile(Program.AbsPathFromRel(dirPath, i.InnerText));
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
            try
            {
                xml = new XmlTextWriter(path, null);

                xml.Indentation = 4;
                xml.Formatting = Formatting.Indented;
                xml.WriteStartDocument();

                xml.WriteStartElement("AVRStudio");

                xml.WriteStartElement("MANAGEMENT");
                {
                    xml.WriteElementString("ProjectName", this.FileNameNoExt);
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
                                xml.WriteElementString("SOURCEFILE", file.FileRelPath(DirPath));
                            }
                            else if (file.FileExt == "h" || file.FileExt == "hpp")
                            {
                                xml.WriteElementString("HEADERFILE", file.FileRelPath(DirPath));
                            }
                            else
                            {
                                xml.WriteElementString("OTHERFILE", file.FileRelPath(DirPath));
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
                                            xml.WriteElementString("FILE", file.FileRelPath(DirPath));
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

                            string optForAll = String.Format("{0} -DF_CPU={1:0}UL {2} {3}", OtherOptions.Trim(), ClockFreq, Optimization, checkList.Trim()).Replace("  ", " ");
                            xml.WriteElementString("OPTIONSFORALL", optForAll);

                            string linkerOpts = "";
                            if (UseInitStack)
                                linkerOpts += "-Wl,--defsym=__stack=0x" + Convert.ToString(InitStackAddr, 16) + " ";
                            linkerOpts += LinkerOptions;
                            xml.WriteElementString("LINKEROPTIONS", linkerOpts.Trim());

                            xml.WriteStartElement("SEGMENTS");
                            {
                                foreach (MemorySegment seg in MemorySegList)
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


            return success;
        }

        #endregion

        public void Reset()
        {
            clkFreq = 8000000;
            device = "atmega168";
            packStructs = true;
            shortEnums = true;
            unsignedBitfields = true;
            unsignedChars = true;
            functSects = true;
            dataSects = true;
            useInitStack = false;
            initStackAddr = ushort.MaxValue;
            otherOpt = "-Wall -gdwarf-2 -std=gnu99";
            linkerOpt = "";
            outputDir = "output";
            optimization = "-Os";

            burnPart = device;
            burnProg = "avrisp";
            burnOpt = "";
            burnBaud = 0;
            burnPort = "";
            burnFuseBox = "";
            burnAutoReset = false;

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
            newObject.BurnOptions = this.BurnOptions;
            newObject.BurnPart = this.BurnPart;
            newObject.BurnProgrammer = this.BurnProgrammer;
            newObject.BurnBaud = this.BurnBaud;
            newObject.BurnPort = this.BurnPort;
            newObject.BurnFuseBox = this.BurnFuseBox;
            newObject.BurnAutoReset = this.BurnAutoReset;
            newObject.ClockFreq = this.ClockFreq;
            newObject.Device = this.Device;
            newObject.dirPath = this.DirPath;
            newObject.FilePath = this.FilePath;
            newObject.InitStackAddr = this.InitStackAddr;
            newObject.LinkerOptions = this.LinkerOptions;
            newObject.Optimization = this.Optimization;
            newObject.OtherOptions = this.OtherOptions;
            newObject.OutputDir = this.OutputDir;
            newObject.PackStructs = this.PackStructs;
            newObject.ShortEnums = this.ShortEnums;
            newObject.UnsignedBitfields = this.UnsignedBitfields;
            newObject.UnsignedChars = this.UnsignedChars;
            newObject.FunctionSections = this.FunctionSections;
            newObject.DataSections= this.DataSections;
            newObject.UseInitStack = this.UseInitStack;
            newObject.LastFile = this.LastFile;

            newObject.fileList = new Dictionary<string, ProjectFile>();
            newObject.fileList.Clear();
            foreach (KeyValuePair<string, ProjectFile> file in this.FileList)
            {
                ProjectFile newFile = (ProjectFile)file.Value.Clone();
                newObject.fileList.Add(file.Key, newFile);
            }

            newObject.includeDirList = new List<string>();
            newObject.includeDirList.Clear();
            foreach (string dir in this.IncludeDirList)
            {
                newObject.includeDirList.Add((string)dir.Clone());
            }

            newObject.libraryDirList = new List<string>();
            newObject.libraryDirList.Clear();
            foreach (string dir in this.LibraryDirList)
            {
                newObject.libraryDirList.Add((string)dir.Clone());
            }

            newObject.linkLibList = new List<string>();
            newObject.linkLibList.Clear();
            foreach (string obj in this.LinkLibList)
            {
                newObject.linkLibList.Add((string)obj.Clone());
            }

            newObject.linkObjList = new List<string>();
            newObject.linkObjList.Clear();
            foreach (string obj in this.LinkObjList)
            {
                newObject.linkObjList.Add((string)obj.Clone());
            }

            newObject.memorySegList = new List<MemorySegment>();
            newObject.memorySegList.Clear();
            foreach (MemorySegment obj in this.MemorySegList)
            {
                newObject.memorySegList.Add(new MemorySegment(obj.Type, obj.Name, obj.Addr));
            }

            newObject.APSXmlElementList = new List<XmlElement>();
            newObject.APSXmlElementList.Clear();
            foreach (XmlElement obj in this.APSXmlElementList)
            {
                newObject.APSXmlElementList.Add((XmlElement)obj.Clone());
            }

            return newObject;
        }

        #endregion
    }
}