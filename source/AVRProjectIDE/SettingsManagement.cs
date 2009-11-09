using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;
using System.Windows.Forms;
using System.Reflection;
using Ini;
using ScintillaNet;
using System.Text.RegularExpressions;

namespace AVRProjectIDE
{
    public class SettingsManagement
    {
        public static string BuildID
        {
            get
            {
                return Properties.Resources.BuildID;
            }
        }

        public static string FuseCalcLink
        {
            get
            {
                string lnk = iniFile.Read("Links", "FuseCalculator");

                if (lnk != null)
                    lnk.Trim().ToLowerInvariant();

                if (string.IsNullOrEmpty(lnk))
                {
                    FuseCalcLink = "http://www.engbedded.com/cgi-bin/fcx.cgi";
                    lnk = FuseCalcLink;
                }
                return lnk;
            }

            set
            {
                iniFile.Write("Links", "FuseCalculator", value.Trim());
            }
        }

        public static bool WelcomeWindowAtStart
        {
            get
            {
                bool res = true;
                string str = iniFile.Read("Editor", "WelcomeWindowAtStart");

                if (str != null)
                    str.Trim().ToLowerInvariant();

                if (string.IsNullOrEmpty(str))
                {
                    WelcomeWindowAtStart = res;
                    res = WelcomeWindowAtStart;
                    return true;
                }

                res = str == true.ToString().Trim().ToLowerInvariant();

                return res;
            }

            set
            {
                iniFile.Write("Editor", "WelcomeWindowAtStart", value.ToString().Trim().ToLowerInvariant());
            }
        }

        #region Last Used Preferences

        public static string LastChipChoosen
        {
            get
            {
                string str = iniFile.Read("Project", "LastChipChoosen");
                if (str != null)
                    str.Trim().ToLowerInvariant();
                if (string.IsNullOrEmpty(str))
                {
                    LastChipChoosen = "atmega168";
                    str = LastChipChoosen;
                }
                return str;
            }

            set
            {
                iniFile.Write("Project", "LastChipChoosen", value.Trim().ToLowerInvariant());
            }
        }

        public static string LastProgChoosen
        {
            get
            {
                string str = iniFile.Read("Project", "LastProgChoosen");

                if (str != null)
                    str.Trim();

                if (string.IsNullOrEmpty(str))
                {
                    LastProgChoosen = "usbasp";
                    str = LastProgChoosen;
                }
                return str;
            }

            set
            {
                iniFile.Write("Project", "LastProgChoosen", value.Trim());
            }
        }

        public static int LastProgBaud
        {
            get
            {
                int res = 0;
                string str = iniFile.Read("Project", "LastProgBaud");

                if (str != null)
                    str.Trim();

                if (string.IsNullOrEmpty(str))
                {
                    LastProgBaud = res;
                    res = LastProgBaud;
                }
                else if (int.TryParse(str, out res) == false)
                {
                    res = 0;
                    LastProgBaud = res;
                    res = LastProgBaud;
                }
                return res;
            }

            set
            {
                iniFile.Write("Project", "LastProgBaud", value.ToString("0"));
            }
        }

        public static string LastProgPortChoosen
        {
            get
            {
                string str = iniFile.Read("Project", "LastProgPortChoosen");

                if (str != null)
                    str.Trim().ToLowerInvariant();

                if (string.IsNullOrEmpty(str))
                {
                    LastProgPortChoosen = "NoOverride";
                    str = LastProgPortChoosen;
                }
                return str;
            }

            set
            {
                string v = value.Trim();
                if (string.IsNullOrEmpty(v))
                    v = "NoOverride";

                iniFile.Write("Project", "LastProgPortChoosen", v);
            }
        }

        public static bool LastProgAutoReset
        {
            get
            {
                bool res = false;
                string str = iniFile.Read("Project", "LastProgAutoReset");

                if (str != null)
                    str.Trim().ToLowerInvariant();

                if (string.IsNullOrEmpty(str))
                {
                    LastProgAutoReset = res;
                    res = LastProgAutoReset;
                    return false;
                }

                res = str == true.ToString().Trim().ToLowerInvariant();

                return res;
            }

            set
            {
                iniFile.Write("Project", "LastProgAutoReset", value.ToString().Trim().ToLowerInvariant());
            }
        }

        public static decimal LastClockChoosen
        {
            get
            {
                decimal res = 8000000;
                string str = iniFile.Read("Project", "LastClockChoosen");

                if (str != null)
                    str.Trim().ToLowerInvariant();

                if (string.IsNullOrEmpty(str))
                {
                    LastClockChoosen = res;
                    res = LastClockChoosen;
                }
                else if (decimal.TryParse(str, out res) == false)
                {
                    res = 8000000;
                    LastClockChoosen = res;
                    res = LastClockChoosen;
                }
                return res;
            }

            set
            {
                iniFile.Write("Project", "LastClockChoosen", value.ToString("0.00"));
            }
        }

        public static int LastFileTypeFilter
        {
            get
            {
                int res = 0;
                string str = iniFile.Read("Wizard", "LastFileTypeFilter");

                if (str != null)
                    str.Trim();

                if (string.IsNullOrEmpty(str))
                {
                    LastFileTypeFilter = res;
                    res = LastFileTypeFilter;
                }
                else if (int.TryParse(str, out res) == false)
                {
                    res = 0;
                    LastFileTypeFilter = res;
                    res = LastFileTypeFilter;
                }
                return res;
            }

            set
            {
                iniFile.Write("Wizard", "LastFileTypeFilter", value.ToString("0"));
            }
        }

        #endregion

        static public string AssemblyTitle
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
                if (attributes.Length > 0)
                {
                    AssemblyTitleAttribute titleAttribute = (AssemblyTitleAttribute)attributes[0];
                    if (titleAttribute.Title != "")
                    {
                        return titleAttribute.Title;
                    }
                }
                return System.IO.Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().CodeBase);
            }
        }

        #region Paths

        static public string AppDataPath
        {
            get { return Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + Path.DirectorySeparatorChar + AssemblyTitle + Path.DirectorySeparatorChar; }
        }

        static public string CurDirPath
        {
            get { return Directory.GetCurrentDirectory().Trim(Path.DirectorySeparatorChar) + Path.DirectorySeparatorChar; }
        }

        static public string AppInstallPath
        {
            get { return Application.ExecutablePath.Substring(0, Application.ExecutablePath.LastIndexOf(Path.DirectorySeparatorChar)) + Path.DirectorySeparatorChar; }
        }

        #endregion

        static private string settingsFilePath;

        static private IniFile iniFile;

        public static IniFile SettingsFile
        {
            get { return iniFile; }
        }

        /// <summary>
        /// Load all settings into static memory
        /// Create files if not already existing
        /// </summary>
        static public void Load()
        {
            Program.MakeSurePathExists(SettingsManagement.AppDataPath);

            recentFilePath = Program.CleanFilePath(AppDataPath + "recent.txt");
            settingsFilePath = Program.CleanFilePath(AppDataPath + "settings.ini");
            scintSettingFilePath = Program.CleanFilePath(AppDataPath + "scintconfig.xml");

            iniFile = new IniFile(settingsFilePath);

            if (File.Exists(recentFilePath) == false)
            {
                WriteBlankFile(recentFilePath);
            }
            if (LoadRecentList() == false)
            {
                MessageBox.Show("Error Loading Recent Projects");
                WriteBlankFile(recentFilePath);
            }

            scint = new Scintilla();

            if (File.Exists(scintSettingFilePath) == false)
            {
                SaveDefaultScintSettings();
            }
            if (LoadScintSettings() == false)
            {
                MessageBox.Show("Error Loading Editor Settings");
            }
            LoadAutocompleteOnce();

            LoadFavFolder();

            GetArduinoPaths();

            string buildOutput = iniFile.Read("Editor", "BuildMessageBehaviour");

            if (string.IsNullOrEmpty(buildOutput))
                buildOutput = "top";

            buildOutput = buildOutput.ToLowerInvariant().Trim();

            if (string.IsNullOrEmpty(buildOutput))
                buildOutput = "top";

            ProjectBuilder.ReverseOutput = buildOutput.Contains("bottom");
        }

        /// <summary>
        /// writes a blank file just to make sure it's there later
        /// </summary>
        /// <param name="path">path to the file</param>
        static public void WriteBlankFile(string path)
        {
            try
            {
                StreamWriter writer = new StreamWriter(path);
                writer.WriteLine();
                writer.Close();
            }
            catch { }
        }

        #region Recent File List Related

        static private string recentFilePath;

        static private List<string> recentFileList = new List<string>();
        static public List<string> RecentFileList
        {
            get { return recentFileList; }
            set { recentFileList = value; }
        }

        static private Dictionary<int, string> recentFileLookupList = new Dictionary<int, string>();

        /// <summary>
        /// gets the actual file path from what the user clicked on in the list box
        /// since the path displayed is shortened
        /// </summary>
        /// <param name="index">index of clicked item</param>
        /// <returns>the real full file path</returns>
        static public string FilePathFromListBoxIndex(int index)
        {
            string result = "";
            if (recentFileLookupList.TryGetValue(index, out result))
            {
                return result;
            }
            else
                return "";
        }

        /// <summary>
        /// add a file path to the most recent list
        /// </summary>
        /// <param name="filePath">the file path to add</param>
        static public void AddFileAsMostRecent(string filePath)
        {
            filePath = Program.CleanFilePath(filePath);
            while (recentFileList.Contains(filePath))
            {
                recentFileList.Remove(filePath);
            }
            recentFileList.Insert(0, filePath);
        }

        /// <summary>
        /// fill a list box with all the recent items
        /// </summary>
        /// <param name="toFill"></param>
        static public void FillListBox(ListBox toFill)
        {
            recentFileLookupList.Clear();
            toFill.Items.Clear();
            foreach(string filePath in recentFileList)
            {
                string[] folders = filePath.Split(Path.DirectorySeparatorChar);
                string displayName = "";
                if (folders.Length - 1 >= 0)
                {
                    displayName = Path.DirectorySeparatorChar + folders[folders.Length - 1];
                }
                if (folders.Length - 2 >= 0)
                {
                    displayName = Path.DirectorySeparatorChar + folders[folders.Length - 2] + displayName;
                }
                else
                {
                    displayName = folders[0] + displayName;
                }
                if (folders.Length - 3 >= 1)
                {
                    displayName = folders[0] + @"\~~~\" + folders[folders.Length - 3] + displayName;
                }
                else
                {
                    displayName = folders[0] + displayName;
                }
                int index = toFill.Items.Add(displayName);
                recentFileLookupList.Add(index, filePath);
            }
        }

        /// <summary>
        /// save the list of recent item into a text file
        /// </summary>
        /// <returns>true if successful</returns>
        static public bool SaveRecentList()
        {
            StreamWriter writer = null;
            bool success = true;
            try
            {
                int cnt = 0;
                writer = new StreamWriter(recentFilePath);
                foreach (string file in recentFileList)
                {
                    string f = Program.CleanFilePath(file);
                    if (File.Exists(f))
                    {
                        writer.WriteLine(f);
                        cnt++;
                    }

                    if (cnt > 15)
                        break;
                }
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
        /// gets the list of recent items from a text file
        /// </summary>
        /// <returns>true if successful</returns>
        static public bool LoadRecentList()
        {
            bool success = true;
            string rFP = Program.CleanFilePath(recentFilePath);
            recentFileList.Clear();
            if (File.Exists(rFP))
            {
                StreamReader reader = null;
                try
                {
                    reader = new StreamReader(rFP);

                    int cnt = 0;
                    string line = reader.ReadLine();
                    while (line != null)
                    {
                        line = Program.CleanFilePath(line);
                        if (File.Exists(line) && line.EndsWith(".avrproj"))
                        {
                            recentFileList.Add(line);
                            cnt++;
                        }
                        line = reader.ReadLine();
                        if (cnt > 10)
                            break;
                    }
                }
                catch
                {
                    success = false;
                }
                try
                {
                    reader.Close();
                }
                catch
                {
                    success = false;
                }
            }
            return success;
        }

        public static string LastProjectPath
        {
            get
            {
                try
                {
                    return recentFileList[0];
                }
                catch
                {
                    return "";
                }
            }
        }

        public static bool OpenLastProject
        {
            get
            {
                bool res = false;
                string str = iniFile.Read("Editor", "OpenLastProject");

                if (str != null)
                    str.Trim().ToLowerInvariant();

                if (string.IsNullOrEmpty(str))
                {
                    OpenLastProject = res;
                    res = OpenLastProject;
                    return false;
                }

                res = str == true.ToString().Trim().ToLowerInvariant();

                return res;
            }

            set
            {
                iniFile.Write("Editor", "OpenLastProject", value.ToString().Trim().ToLowerInvariant());
            }
        }

        #endregion

        #region Scintilla Settings Related

        static string scintSettingFilePath;

        static Scintilla scint;

        static Dictionary<int, Color> styleForeColour = new Dictionary<int, Color>();
        static Dictionary<int, Color> styleBackColour = new Dictionary<int, Color>();
        static Dictionary<int, string> styleFont = new Dictionary<int, string>();
        static Dictionary<int, int> styleFontSize = new Dictionary<int, int>();
        static Dictionary<int, bool> styleBold = new Dictionary<int, bool>();
        static Dictionary<int, bool> styleItalic = new Dictionary<int, bool>();
        static Dictionary<int, bool> styleUnderline = new Dictionary<int, bool>();
        static Dictionary<int, string> styleKeywords = new Dictionary<int, string>();

        static Color selForeColour;
        static Color selBackColour;
        static Color editorBackColour;

        static bool backspaceUnindents = true;
        static bool tabIndents = true;
        static bool useTabs = true;
        static int tabWidth = 4;
        static int indentWidth = 4;
        static int smartIndent = 3;

        static bool showLineNumber = true;
        static bool showWS = false;
        static bool highlightCurLine = false;

        static int zoomLevel = int.MinValue;
        public static int ZoomLevel
        {
            get { return zoomLevel; }
            set { zoomLevel = value; }
        }

        static bool indentGuide = true;
        static bool lineWrap = true;

        static int backupInterval = 30;
        public static int BackupInterval
        {
            get
            {
                string str = iniFile.Read("Editor", "BackupInterval");
                if (int.TryParse(str, out backupInterval) == false)
                {
                    BackupInterval = 30;
                }

                return backupInterval;
            }
            set
            {
                backupInterval = value;
                iniFile.Write("Editor", "BackupInterval", backupInterval.ToString("0"));
            }
        }

        static bool autocompleteEnable = true;
        public static bool AutocompleteEnable
        {
            get { return autocompleteEnable; }
            set
            {
                iniFile.Write("Editor", "AutocompleteEnable", value.ToString().ToLowerInvariant().Trim());
            }
        }

        public static bool TrimOnSave
        {
            get
            {
                bool res = false;
                string str = iniFile.Read("Editor", "TrimOnSave");

                if (str != null)
                    str.Trim().ToLowerInvariant();

                if (string.IsNullOrEmpty(str))
                {
                    TrimOnSave = res;
                    res = TrimOnSave;
                    return false;
                }

                res = str == true.ToString().Trim().ToLowerInvariant();

                return res;
            }

            set
            {
                iniFile.Write("Editor", "TrimOnSave", value.ToString().Trim().ToLowerInvariant());
            }
        }

        /// <summary>
        /// load editor settings from configuration file
        /// </summary>
        /// <returns>true if successful</returns>
        static public bool LoadScintSettings()
        {
            // first load a built in configuration
            scint.ConfigurationManager.Language = "cs";

            XmlDocument xDoc = new XmlDocument();
            try
            {
                xDoc.Load(scintSettingFilePath);
            }
            catch
            {
                // if this fails, then use default settings
                return false;
            }

            try
            {
                XmlElement xDocEle = xDoc.DocumentElement;
                XmlNodeList xStyles = xDocEle.GetElementsByTagName("Style");

                selForeColour = scint.Selection.ForeColor;
                selBackColour = scint.Selection.BackColor;
                editorBackColour = scint.BackColor;

                // match styles with the style names used by the scintilla

                styleForeColour.Clear();
                styleBackColour.Clear();
                styleFont.Clear();
                styleFontSize.Clear();
                styleBold.Clear();
                styleItalic.Clear();
                styleUnderline.Clear();
                styleKeywords.Clear();

                foreach (XmlElement xStyle in xStyles)
                {
                    string name = xStyle.GetAttribute("Name");
                    int styleIndex;
                    if (scint.Lexing.StyleNameMap.TryGetValue(name, out styleIndex))
                    {
                        foreach (XmlAttribute xAttrib in xStyle.Attributes)
                        {
                            if (xAttrib.Name == "ForeColor")
                            {
                                styleForeColour.Add(styleIndex, Color.FromName(xAttrib.Value));
                            }
                            else if (xAttrib.Name == "BackColor")
                            {
                                styleBackColour.Add(styleIndex, Color.FromName(xAttrib.Value));
                            }
                            else if (xAttrib.Name == "Font")
                            {
                                styleFont.Add(styleIndex, xAttrib.Value);
                            }
                            else if (xAttrib.Name == "Size")
                            {
                                int size = 10;
                                if (int.TryParse(xAttrib.Value, out size))
                                    styleFontSize.Add(styleIndex, size);
                            }
                            else if (xAttrib.Name == "Bold")
                            {
                                styleBold.Add(styleIndex, xAttrib.Value.ToLowerInvariant().Trim() == true.ToString().Trim().ToLowerInvariant());
                            }
                            else if (xAttrib.Name == "Italic")
                            {
                                styleItalic.Add(styleIndex, xAttrib.Value.ToLowerInvariant().Trim() == true.ToString().Trim().ToLowerInvariant());
                            }
                            else if (xAttrib.Name == "Underline")
                            {
                                styleUnderline.Add(styleIndex, xAttrib.Value.ToLowerInvariant().Trim() == true.ToString().Trim().ToLowerInvariant());
                            }
                        }
                    }

                    if (name == "SelectionHighlighting")
                    {
                        foreach (XmlAttribute xAttrib in xStyle.Attributes)
                        {
                            if (xAttrib.Name == "ForeColor")
                            {
                                selForeColour = Color.FromName(xAttrib.Value);
                            }
                            else if (xAttrib.Name == "BackColor")
                            {
                                selBackColour = Color.FromName(xAttrib.Value);
                            }
                        }
                    }
                    else if (name == "EditorBackground")
                    {
                        editorBackColour = Color.FromName(xStyle.InnerText);
                    }
                }

                // construct lists of keywords
                XmlNodeList xKeywordsList = xDocEle.GetElementsByTagName("Keywords");
                foreach (XmlElement xKeywords in xKeywordsList)
                {
                    int listNum = 2;
                    if (int.TryParse(xKeywords.GetAttribute("List"), out listNum))
                    {
                        string oldKeywords = "";
                        if (styleKeywords.TryGetValue(listNum, out oldKeywords))
                        {
                            // append to this list if this list already contains keywords
                            styleKeywords[listNum] = (oldKeywords + " " + xKeywords.InnerText).Replace('\t', ' ').Replace('\r', ' ').Replace('\n', ' ').Replace('\0', ' ');
                        }
                        else
                        {
                            styleKeywords.Add(listNum, xKeywords.InnerText.Replace('\t', ' ').Replace('\r', ' ').Replace('\n', ' ').Replace('\0', ' '));
                        }
                    }
                }

                string tmpStr = iniFile.Read("Editor", "IndentWidth");
                int i = 0;
                if (string.IsNullOrEmpty(tmpStr))
                {
                    iniFile.Write("Editor", "IndentWidth", indentWidth.ToString("0"));
                }
                else if (int.TryParse(tmpStr, out i))
                {
                    indentWidth = i;
                }
                else
                {
                    iniFile.Write("Editor", "IndentWidth", indentWidth.ToString("0"));
                }

                tmpStr = iniFile.Read("Editor", "IndentTabWidth");
                if (string.IsNullOrEmpty(tmpStr))
                {
                    iniFile.Write("Editor", "IndentTabWidth", tabWidth.ToString("0"));
                }
                else if (int.TryParse(tmpStr, out i))
                {
                    tabWidth = i;
                }
                else
                {
                    iniFile.Write("Editor", "IndentTabWidth", tabWidth.ToString("0"));
                }

                tmpStr = iniFile.Read("Editor", "IndentUseTab");
                if (string.IsNullOrEmpty(tmpStr))
                {
                    iniFile.Write("Editor", "IndentUseTab", useTabs.ToString().ToLowerInvariant().Trim());
                }
                else
                {
                    useTabs = tmpStr == true.ToString().Trim().ToLowerInvariant();
                }

                tmpStr = iniFile.Read("Editor", "IndentBackspaceUnindents");
                if (string.IsNullOrEmpty(tmpStr))
                {
                    iniFile.Write("Editor", "IndentBackspaceUnindents", backspaceUnindents.ToString().ToLowerInvariant().Trim());
                }
                else
                {
                    backspaceUnindents = tmpStr == true.ToString().Trim().ToLowerInvariant();
                }

                tmpStr = iniFile.Read("Editor", "IndentTabIndents");
                if (string.IsNullOrEmpty(tmpStr))
                {
                    iniFile.Write("Editor", "IndentTabIndents", tabIndents.ToString().ToLowerInvariant().Trim());
                }
                else
                {
                    tabIndents = tmpStr == true.ToString().Trim().ToLowerInvariant();
                }

                tmpStr = iniFile.Read("Editor", "IndentGuide");
                if (string.IsNullOrEmpty(tmpStr))
                {
                    iniFile.Write("Editor", "IndentGuide", indentGuide.ToString().ToLowerInvariant().Trim());
                }
                else
                {
                    indentGuide = tmpStr == true.ToString().Trim().ToLowerInvariant();
                }

                tmpStr = iniFile.Read("Editor", "IndentSmartness");
                if (string.IsNullOrEmpty(tmpStr))
                {
                    iniFile.Write("Editor", "IndentSmartness", smartIndent.ToString("0"));
                }
                else if (int.TryParse(tmpStr, out i))
                {
                    smartIndent = i;
                }
                else
                {
                    iniFile.Write("Editor", "IndentSmartness", smartIndent.ToString("0"));
                }

                tmpStr = iniFile.Read("Editor", "LineWrap");
                if (string.IsNullOrEmpty(tmpStr))
                {
                    iniFile.Write("Editor", "LineWrap", lineWrap.ToString().ToLowerInvariant().Trim());
                }
                else
                {
                    lineWrap = tmpStr == true.ToString().Trim().ToLowerInvariant();
                }

                zoomLevel = scint.Zoom;

                tmpStr = iniFile.Read("Editor", "ZoomLevel");
                if (string.IsNullOrEmpty(tmpStr))
                {
                    iniFile.Write("Editor", "ZoomLevel", zoomLevel.ToString("0"));
                }
                else if (int.TryParse(tmpStr, out i))
                {
                    zoomLevel = Math.Min(3, Math.Max(i, 0));
                }
                else
                {
                    iniFile.Write("Editor", "ZoomLevel", zoomLevel.ToString("0"));
                }

                tmpStr = iniFile.Read("Editor", "ShowLineNumbers");
                if (string.IsNullOrEmpty(tmpStr))
                {
                    iniFile.Write("Editor", "ShowLineNumbers", showLineNumber.ToString().ToLowerInvariant().Trim());
                }
                else
                {
                    showLineNumber = tmpStr == true.ToString().Trim().ToLowerInvariant();
                }

                tmpStr = iniFile.Read("Editor", "ShowWhiteSpace");
                if (string.IsNullOrEmpty(tmpStr))
                {
                    iniFile.Write("Editor", "ShowWhiteSpace", showWS.ToString().ToLowerInvariant().Trim());
                }
                else
                {
                    showWS = tmpStr == true.ToString().Trim().ToLowerInvariant();
                }

                tmpStr = iniFile.Read("Editor", "HighlightCurrentLine");
                if (string.IsNullOrEmpty(tmpStr))
                {
                    iniFile.Write("Editor", "HighlightCurrentLine", highlightCurLine.ToString().ToLowerInvariant().Trim());
                }
                else
                {
                    highlightCurLine = tmpStr == true.ToString().Trim().ToLowerInvariant();
                }                

                scint.Dispose(); // dispose of the scint, it causes an exception if it is not disposed manually
            }
            catch { return false; }

            return true;
        }

        public static void SaveZoomLevel()
        {
            iniFile.Write("Editor", "ZoomLevel", zoomLevel.ToString("0"));
        }

        /// <summary>
        /// applys the settings stored to the scint
        /// </summary>
        /// <param name="scinti">reference to the scint</param>
        /// <returns>the same reference that was passed in</returns>
        static public Scintilla SetScintSettings(Scintilla scinti)
        {
            foreach (KeyValuePair<int, string> i in styleKeywords)
            {
                scinti.Lexing.SetKeywords(i.Key, i.Value);
            }

            foreach (KeyValuePair<int, Color> i in styleForeColour)
            {
                scinti.Styles[i.Key].ForeColor = i.Value;
            }

            foreach (KeyValuePair<int, Color> i in styleBackColour)
            {
                scinti.Styles[i.Key].BackColor = i.Value;
            }

            foreach (KeyValuePair<int, int> i in styleFontSize)
            {
                scinti.Styles[i.Key].Size = i.Value;
            }

            foreach (KeyValuePair<int, string> i in styleFont)
            {
                scinti.Styles[i.Key].FontName = i.Value;
            }

            foreach (KeyValuePair<int, bool> i in styleBold)
            {
                scinti.Styles[i.Key].Bold = i.Value;
            }

            foreach (KeyValuePair<int, bool> i in styleItalic)
            {
                scinti.Styles[i.Key].Italic = i.Value;
            }

            foreach (KeyValuePair<int, bool> i in styleUnderline)
            {
                scinti.Styles[i.Key].Underline = i.Value;
            }

            scinti.Selection.BackColor = selBackColour;
            scinti.Selection.ForeColor = selForeColour;
            scinti.BackColor = editorBackColour;

            if (showWS)
                scinti.Whitespace.Mode = WhitespaceMode.VisibleAlways;
            else
                scinti.Whitespace.Mode = WhitespaceMode.Invisible;

            scinti.Margins[0].Width = showLineNumber ? 35 : 0;

            scinti.Indentation.BackspaceUnindents = backspaceUnindents;
            scinti.Indentation.ShowGuides = indentGuide;
            scinti.Indentation.TabIndents = tabIndents;
            scinti.Indentation.UseTabs = useTabs;
            scinti.Indentation.IndentWidth = indentWidth;
            scinti.Indentation.TabWidth = tabWidth;

            if (smartIndent == 0)
                scinti.Indentation.SmartIndentType = SmartIndent.None;
            else if (smartIndent == 1)
                scinti.Indentation.SmartIndentType = SmartIndent.Simple;
            else if (smartIndent == 2)
                scinti.Indentation.SmartIndentType = SmartIndent.CPP;
            else if (smartIndent == 3)
                scinti.Indentation.SmartIndentType = SmartIndent.CPP2;

            if (lineWrap)
            {
                scinti.LineWrap.Mode = WrapMode.Word;
                scinti.Scrolling.ScrollBars = ScrollBars.Vertical;
            }
            else
            {
                scinti.LineWrap.Mode = WrapMode.None;
                scinti.Scrolling.ScrollBars = ScrollBars.Both;
            }

            scinti.Zoom = zoomLevel;

            scinti.AutoComplete.AutomaticLengthEntered = true;
            scinti.AutoComplete.FillUpCharacters = "";
            scinti.AutoComplete.AutoHide = true;

            scinti.Caret.HighlightCurrentLine = highlightCurLine;

            return scinti;
        }

        /// <summary>
        /// creates a default config file from project resource
        /// </summary>
        static public void SaveDefaultScintSettings()
        {
            try
            {
                StreamWriter writer = new StreamWriter(scintSettingFilePath);
                writer.Write(Properties.Resources.scintconfig);
                writer.Close();
            }
            catch { }
        }

        private static void LoadAutocompleteOnce()
        {
            string acStr = iniFile.Read("Editor", "AutocompleteEnable");
            if (string.IsNullOrEmpty(acStr))
            {
                autocompleteEnable = true;
            }
            else
            {
                autocompleteEnable = acStr.Trim().ToLowerInvariant() == true.ToString().Trim().ToLowerInvariant();
            }
        }

        #endregion

        #region Serial Port Preferences Related

        public static string PortName
        {
            get
            {
                string str = iniFile.Read("SerialPort", "PortName");
                if (str != null)
                    str.Trim().ToLowerInvariant();
                if (string.IsNullOrEmpty(str))
                {
                    PortName = "COM1";
                    str = LastChipChoosen;
                }
                return str;
            }

            set
            {
                if (value.ToUpperInvariant().Trim().StartsWith("COM"))
                    iniFile.Write("SerialPort", "PortName", value.Trim().ToUpperInvariant());
            }
        }

        static public int BaudRate
        {
            get
            {
                int res = 9600;
                string str = iniFile.Read("SerialPort", "BaudRate");

                if (str != null)
                    str.Trim();

                if (string.IsNullOrEmpty(str))
                {
                    BaudRate = res;
                    res = BaudRate;
                }
                else if (int.TryParse(str, out res) == false)
                {
                    res = 9600;
                    BaudRate = res;
                    res = BaudRate;
                }
                return res;
            }

            set
            {
                iniFile.Write("SerialPort", "BaudRate", value.ToString("0"));
            }
        }

        /// <summary>
        /// save preferences to ini file
        /// </summary>
        /// <param name="port"></param>
        /// <param name="baud"></param>
        /// <returns></returns>
        static public bool SaveSerialPortPrefs(string port, int baud)
        {
            try
            {
                PortName = port;
                BaudRate = baud;
                return true;
            }
            catch { return false; }
        }

        #endregion

        #region Arduino Related

        private static string arduinoCorePath;
        private static string arduinoLibPath;

        public static string ArduinoCorePath
        {
            get { return Program.CleanFilePath(arduinoCorePath); }
            set
            {
                arduinoCorePath = Program.CleanFilePath(value);
                iniFile.Write("Arduino", "CorePath", arduinoCorePath);
            }
        }

        public static string ArduinoLibPath
        {
            get { return Program.CleanFilePath(arduinoLibPath); }
            set
            {
                arduinoLibPath = Program.CleanFilePath(value);
                iniFile.Write("Arduino", "LibPath", arduinoLibPath);
            }
        }

        /// <summary>
        /// Gets the core path and the library path by first checking the ini file, and then checking to see if they are included with the IDE installation
        /// </summary>
        public static void GetArduinoPaths()
        {
            string path = iniFile.Read("Arduino", "CorePath");
            if (string.IsNullOrEmpty(path))
            {
                arduinoCorePath = SettingsManagement.AppInstallPath + "arduino\\core";
            }
            else
            {
                arduinoCorePath = path;
            }
            if (Directory.Exists(arduinoCorePath))
            {
                iniFile.Write("Arduino", "CorePath", arduinoCorePath);
            }

            path = iniFile.Read("Arduino", "LibPath");
            if (string.IsNullOrEmpty(path))
            {
                arduinoLibPath = SettingsManagement.AppInstallPath + "arduino\\libraries";
            }
            else
            {
                arduinoLibPath = path;
            }
            if (Directory.Exists(arduinoLibPath))
            {
                iniFile.Write("Arduino", "LibPath", arduinoLibPath);
            }
        }

        #endregion

        #region Favorite Folder Related

        static private string favFolderPath = "";
        static public string FavFolder
        {
            get { return Program.CleanFilePath(favFolderPath); }
            set
            {
                //favFolderPath = Program.CleanFilePath(value);
                SaveFavFolder(value);
            }
        }

        static public string LoadFavFolder()
        {
            favFolderPath = iniFile.Read("Other", "FavFolder");
            if (string.IsNullOrEmpty(favFolderPath))
                favFolderPath = Program.CleanFilePath(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)) + Path.DirectorySeparatorChar + "Projects";
            return FavFolder;
        }

        static public void SaveFavFolder(string folder)
        {
            favFolderPath = Program.CleanFilePath(folder);
            iniFile.Write("Other", "FavFolder", favFolderPath);
        }

        #endregion

        #region Wizard Preferences

        static public string LastTemplate
        {
            get
            {
                return iniFile.Read("Wizard", "LastTemplate");
            }
            set
            {
                iniFile.Write("Wizard", "LastTemplate", value);
            }
        }

        static public string LastInitialFileType
        {
            get
            {
                return iniFile.Read("Wizard", "LastInitialFileType");
            }
            set
            {
                iniFile.Write("Wizard", "LastInitialFileType", value);
            }
        }

        #endregion

        #region Window State

        public static void LoadWindowState(Form form)
        {
            try
            {
                string winMax = iniFile.Read("Editor", "WindowMax");
                string winWidth = iniFile.Read("Editor", "WindowWidth");
                string winHeight = iniFile.Read("Editor", "WindowHeight");
                if (string.IsNullOrEmpty(winMax) || string.IsNullOrEmpty(winWidth) || string.IsNullOrEmpty(winHeight))
                {
                    form.WindowState = FormWindowState.Maximized;
                }
                else
                {
                    if (winMax.ToLowerInvariant().Trim() == true.ToString().Trim().ToLowerInvariant())
                    {
                        form.WindowState = FormWindowState.Maximized;
                    }
                    else
                    {
                        int w;
                        int h;
                        if (int.TryParse(winWidth, out w) == false || int.TryParse(winHeight, out h) == false)
                        {
                            form.WindowState = FormWindowState.Maximized;
                        }
                        else
                        {
                            form.WindowState = FormWindowState.Normal;
                            form.Width = w;
                            form.Height = h;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorReportWindow erw = new ErrorReportWindow(ex, "Error while loading window state");
                erw.ShowDialog();
            }
        }

        public static void SaveWindowState(Form form)
        {
            try
            {
                iniFile.Write("Editor", "WindowMax", (form.WindowState == FormWindowState.Maximized).ToString().ToLowerInvariant());
                iniFile.Write("Editor", "WindowWidth", form.Width.ToString("0"));
                iniFile.Write("Editor", "WindowHeight", form.Height.ToString("0"));
            }
            catch (Exception ex)
            {
                ErrorReportWindow erw = new ErrorReportWindow(ex, "Error while saving window state");
                erw.ShowDialog();
            }
        }

        #endregion

        #region Updater

        public static bool CheckForUpdates
        {
            get
            {
                bool res = false;
                string str = iniFile.Read("Stuff", "CheckForUpdates");

                if (str != null)
                    str.Trim().ToLowerInvariant();

                if (string.IsNullOrEmpty(str))
                {
                    CheckForUpdates = res;
                    res = CheckForUpdates;
                    return false;
                }

                res = str == true.ToString().Trim().ToLowerInvariant();

                return res;
            }

            set
            {
                iniFile.Write("Stuff", "CheckForUpdates", value.ToString().Trim().ToLowerInvariant());
            }
        }

        #endregion
    }
}
