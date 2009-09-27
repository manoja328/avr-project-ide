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

        static public string AppDataPath
        {
            get { return Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + Path.DirectorySeparatorChar + AssemblyTitle + Path.DirectorySeparatorChar; }
        }

        static private string settingsFilePath;

        static private IniFile iniFile;

        /// <summary>
        /// Load all settings into static memory
        /// Create files if not already existing
        /// </summary>
        static public void Load()
        {
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

            if (iniFile.Exists == false)
            {
                SaveSerialPortPrefs();
            }
            if (LoadSerialPortPrefs() == false)
            {
                MessageBox.Show("Error Loading Serial Port Prefs");
            }

            LoadFavFolder();

            GetArduinoPaths();
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
                writer = new StreamWriter(recentFilePath);
                foreach (string file in recentFileList)
                {
                    string f = Program.CleanFilePath(file);
                    if (File.Exists(f))
                    {
                        writer.WriteLine(f);
                    }
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

        static bool backspaceUnindents = true;
        static bool tabIndents = true;
        static bool useTabs = true;
        static int tabWidth = 4;
        static int indentWidth = 4;
        static int smartIndent = 3;

        static bool indentGuide = true;
        static bool lineWrap = true;

        static int backupInterval = 30;
        public static int BackupInterval
        {
            get { return backupInterval; }
        }

        /// <summary>
        /// load editor settings from configuration file
        /// </summary>
        /// <returns>true if successful</returns>
        static private bool LoadScintSettings()
        {
            try
            {
                // get non-scintilla related settings
                string bkIntStr = iniFile.Read("Editor", "BackupInterval");
                if (string.IsNullOrEmpty(bkIntStr) == false)
                {
                    if (int.TryParse(bkIntStr, out backupInterval) == false)
                    {
                        backupInterval = 30;
                        iniFile.Write("Editor", "BackupInterval", backupInterval.ToString("0"));
                    }
                }
            }
            catch { }

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

                // match styles with the style names used by the scintilla
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
                                styleBold.Add(styleIndex, xAttrib.Value.ToLowerInvariant().Trim() == "true");
                            }
                            else if (xAttrib.Name == "Italic")
                            {
                                styleItalic.Add(styleIndex, xAttrib.Value.ToLowerInvariant().Trim() == "true");
                            }
                            else if (xAttrib.Name == "Underline")
                            {
                                styleUnderline.Add(styleIndex, xAttrib.Value.ToLowerInvariant().Trim() == "true");
                            }
                        }
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

                // find indentation settings
                XmlNodeList xIndentSettings = xDocEle.GetElementsByTagName("Indentation");
                foreach (XmlElement x in xIndentSettings)
                {
                    foreach (XmlAttribute i in x.Attributes)
                    {
                        if (i.Name.ToLowerInvariant() == "backspaceunindents")
                        {
                            backspaceUnindents = i.Value.ToLowerInvariant().Trim() == "true";
                        }
                        else if (i.Name.ToLowerInvariant() == "usetabs")
                        {
                            useTabs = i.Value.ToLowerInvariant().Trim() == "true";
                        }
                        else if (i.Name.ToLowerInvariant() == "tabindents")
                        {
                            tabIndents = i.Value.ToLowerInvariant().Trim() == "true";
                        }
                        else if (i.Name.ToLowerInvariant() == "indentguide")
                        {
                            indentGuide = i.Value.ToLowerInvariant().Trim() == "true";
                        }
                        else if (i.Name.ToLowerInvariant() == "tabwidth")
                        {
                            int width = 4;
                            if (int.TryParse(i.Value, out width))
                                tabWidth = width;
                        }
                        else if (i.Name.ToLowerInvariant() == "indentwidth")
                        {
                            int width = 4;
                            if (int.TryParse(i.Value, out width))
                                indentWidth = width;
                        }
                        else if (i.Name.ToLowerInvariant() == "smartindentlevel")
                        {
                            int width = 3;
                            if (int.TryParse(i.Value, out width))
                                smartIndent = width;
                        }
                    }
                }

                // other settings, duh
                XmlNodeList xOtherSettings = xDocEle.GetElementsByTagName("OtherSettings");
                foreach (XmlElement x in xOtherSettings)
                {
                    foreach (XmlAttribute i in x.Attributes)
                    {
                        if (i.Name.ToLowerInvariant() == "linewrap")
                        {
                            lineWrap = i.Value.ToLowerInvariant().Trim() == "true";
                        }
                    }
                }

                scint.Dispose(); // dispose of the scint, it causes an exception if it is not disposed manually
            }
            catch { return false; }

            return true;
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

        #endregion

        #region Serial Port Preferences Related

        static private string portName = "COM1";
        static public string PortName
        {
            get { return portName.Trim(); }
            set { portName = value.Trim(); }
        }

        static private string baudRateStr = "9600";
        static public int BaudRate
        {
            get
            {
                int res;
                if (int.TryParse(baudRateStr, out res))
                {
                    return res;
                }
                else
                {
                    return 9600;
                }
            }

            set
            {
                baudRateStr = value.ToString();
            }
        }

        /// <summary>
        /// load preferences from ini file
        /// </summary>
        /// <returns>true if successful</returns>
        static public bool LoadSerialPortPrefs()
        {
            try
            {
                PortName = iniFile.Read("SerialPort", "PortName").Trim();
                baudRateStr = iniFile.Read("SerialPort", "BaudRate").Trim();
                return true;
            }
            catch { return false; }
        }

        /// <summary>
        /// save preferences to ini file
        /// </summary>
        /// <returns>true if successful</returns>
        static public bool SaveSerialPortPrefs()
        {
            try
            {
                iniFile.Write("SerialPort", "PortName", PortName);
                iniFile.Write("SerialPort", "BaudRate", baudRateStr);
                return true;
            }
            catch { return false; }
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
                baudRateStr = baud.ToString();
                iniFile.Write("SerialPort", "PortName", PortName);
                iniFile.Write("SerialPort", "BaudRate", baudRateStr);
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
                arduinoCorePath = Directory.GetCurrentDirectory().TrimEnd(Path.DirectorySeparatorChar) + Path.DirectorySeparatorChar + "arduino\\core";
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
                arduinoLibPath = Directory.GetCurrentDirectory().TrimEnd(Path.DirectorySeparatorChar) + Path.DirectorySeparatorChar + "arduino\\libraries";
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
                favFolderPath = "";
            return FavFolder;
        }

        static public void SaveFavFolder(string folder)
        {
            favFolderPath = Program.CleanFilePath(folder);
            iniFile.Write("Other", "FavFolder", favFolderPath);
        }

        #endregion
    }
}
