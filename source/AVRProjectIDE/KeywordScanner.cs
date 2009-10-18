using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using System.Threading;
using System.Drawing;

namespace AVRProjectIDE
{
    public static class KeywordScanner
    {
        private static AVRProject project;
        private static Dictionary<string, EditorPanel> editorList;

        private static Dictionary<ProjectFile, string> fileContents = new Dictionary<ProjectFile,string>();
        private static Dictionary<ProjectFile, List<string>> fileLibs = new Dictionary<ProjectFile,List<string>>();
        private static Dictionary<ProjectFile, Dictionary<string, CodeKeyword>> fileKeywords = new Dictionary<ProjectFile,Dictionary<string,CodeKeyword>>();
        private static Dictionary<ProjectFile, List<string>> allResults = new Dictionary<ProjectFile,List<string>>();

        private static Dictionary<string, CodeKeyword> CKeywords = new Dictionary<string,CodeKeyword>();
        private static Dictionary<string, CodeKeyword> CPPKeywords = new Dictionary<string,CodeKeyword>();
        private static Dictionary<string, CodeKeyword> ArduinoKeywords = new Dictionary<string,CodeKeyword>();
        private static Dictionary<string, Dictionary<string, CodeKeyword>> AVRLibcKeywords = new Dictionary<string,Dictionary<string,CodeKeyword>>();

        private static List<string> preprocKeywords = new List<string>();

        #region Thread

        private static Thread bgScanner;
        private static bool quitScan;
        private static ManualResetEvent bgHasQuit = new ManualResetEvent(false);
        private static ManualResetEvent moreWorkRequired = new ManualResetEvent(false);
        private static ManualResetEvent allResReady = new ManualResetEvent(false);
        private static ManualResetEvent readyForFeed = new ManualResetEvent(false);

        private static void BackgroundScan()
        {
            if (SettingsManagement.AutocompleteEnable == false)
                return;

            readyForFeed.Reset();
            fileContents.Clear();

            quitScan = false;
            do
            {
                try
                {
                    fileLibs.Clear();
                    fileKeywords.Clear();

                    readyForFeed.Reset();

                    LoadAllLibs();
                    LoadFileKeywords();

                    readyForFeed.Set();

                    lock (allResults)
                    {
                        allResReady.Reset();
                        allResults.Clear();

                        foreach (KeyValuePair<ProjectFile, Dictionary<string, CodeKeyword>> i in fileKeywords)
                        {
                            List<string> j = new List<string>();
                            foreach (KeyValuePair<string, CodeKeyword> k in i.Value)
                            {
                                j.Add(k.Value.ListEntry);
                            }
                            allResults.Add(i.Key, j);
                        }

                        foreach (List<string> l in allResults.Values)
                        {
                            l.Sort((x, y) => string.Compare(x, y));
                        }

                        allResReady.Set();
                    }

                    if (quitScan)
                        break;

                    Thread.Sleep(2000);

                    moreWorkRequired.WaitOne();
                    moreWorkRequired.Reset();
                }
                catch { }

            } while (quitScan == false);

            bgHasQuit.Set();
        }

        #endregion

        #region Public Methods

        public static void Initialize()
        {
            if (SettingsManagement.AutocompleteEnable == false)
                return;

            LoadDefaultKeywords();
        }

        public static List<string> GetListForFile(ProjectFile file)
        {
            if (allResReady.WaitOne(500))
            {
                if (allResults.ContainsKey(file))
                    return allResults[file];
                else
                    return new List<string>();
            }
            else
                return new List<string>();
        }

        public static void DoMoreWork()
        {
            if (SettingsManagement.AutocompleteEnable == false)
                return;

            moreWorkRequired.Set();
        }

        public static List<string> GetPreprocKeywords()
        {
            return preprocKeywords;
        }

        public static void LaunchScan(AVRProject project_, Dictionary<string, EditorPanel> editorList_)
        {
            if (SettingsManagement.AutocompleteEnable == false)
                return;

            if (bgScanner == null)
                bgScanner = new Thread(new ThreadStart(BackgroundScan));

            bgHasQuit.Reset();

            moreWorkRequired.Reset();

            if (bgScanner.IsAlive)
            {
                quitScan = true;
                moreWorkRequired.Set();
                bgHasQuit.WaitOne();
            }

            project = project_;
            editorList = editorList_;

            bgScanner = new Thread(new ThreadStart(BackgroundScan));
            bgScanner.IsBackground = true;
            bgScanner.Priority = ThreadPriority.Lowest;
            bgScanner.Start();
        }

        public static void FeedFileContent(ProjectFile file)
        {
            if (SettingsManagement.AutocompleteEnable == false)
                return;

            try
            {
                FeedFileContent(file, File.ReadAllText(file.FileAbsPath));
            }
            catch { }
        }

        public static void FeedFileContent(ProjectFile file, string content)
        {
            if (SettingsManagement.AutocompleteEnable == false)
                return;

            if (bgScanner.IsAlive)
                readyForFeed.WaitOne();

            content = CodePreProcess.StripComments(content);

            if (fileContents.ContainsKey(file))
                fileContents[file] = content;
            else
                fileContents.Add(file, content);

            DoMoreWork();
        }

        public static List<string> GetKeywordsUpTo(ProjectFile file, string content)
        {
            if (allResReady.WaitOne(1000) == false)
                return new List<string>();

            List<string> finalRes = new List<string>();

            if (allResults.ContainsKey(file))
                finalRes.AddRange(allResults[file]);

            int contentLen = content.Length;
            content = "\r\n" + content + "\r\n";

            string noStringContent = "";
            bool inStreamComment = false;
            bool inLineComment = false;
            bool inString = false;
            bool inChar = false;

            content = "\r\n" + content;
            int originalLength = content.Length;
            content += "\r\n";

            for (int i = 1; i < originalLength; i++)
            {
                char c = content[i];

                if ((inString || inChar) && c == '\\' && content[i + 1] == '\\')
                {
                    content = content.Remove(i, 2);
                    content = content.Insert(i, "  ");
                }
                else if (c == '"' && content[i - 1] != '\\' && inStreamComment == false && inLineComment == false && inChar == false)
                {
                    inString = !inString;
                }
                else if (c == '\'' && content[i - 1] != '\\' && inStreamComment == false && inLineComment == false && inString == false)
                {
                    inChar = !inChar;
                }
                else if (c == '/' && content[i + 1] == '/' && inString == false && inChar == false)
                {
                    inLineComment = true;
                    i += 2;
                }
                else if (c == '/' && content[i + 1] == '*' && inLineComment == false && inString == false && inChar == false)
                {
                    inStreamComment = true;
                }
                else if (c == '*' && content[i + 1] == '/' && inString == false && inChar == false)
                {
                    inStreamComment = false;
                    if (inLineComment == false)
                    {
                        i += 2;
                    }
                }
                else if (c == '\n')
                {
                    inLineComment = false;
                    inString = false;
                    inChar = false;
                }

                if (inStreamComment == false && inLineComment == false)
                {
                    noStringContent += c;
                }
            }

            contentLen = noStringContent.Length;
            content = "\r\n" + noStringContent + "\r\n";

            string res = "";
            int braceNest = 0;
            int maxNest = 0;
            int bracketNest = 0;

            for (int i = contentLen - 1; i >= 0; i--)
            {
                char c = content[i];

                if (c == '}')
                    braceNest++;
                else if (c == '{')
                    braceNest--;
                else if (c == ')')
                    bracketNest++;
                else if (c == '(')
                    bracketNest--;

                maxNest = Convert.ToInt32(Math.Min(maxNest, braceNest));

                if (braceNest <= maxNest)
                    res = c + res;
            }

            Regex r = new Regex("([^a-zA-Z0-9_])([a-zA-Z_][a-zA-Z0-9_]*)(\\s*)([^a-zA-Z0-9_])", RegexOptions.Multiline);
            Match m = r.Match("`" + res + "`");

            while (m.Success)
            {
                string text = m.Value;

                KeywordType type = KeywordType.Other;
                if (text.EndsWith("("))
                    type = KeywordType.Function;

                while (char.IsLetterOrDigit(text, 0) == false && text[0] != '_')
                    text = text.Substring(1);

                while (char.IsLetterOrDigit(text, text.Length - 1) == false && text[text.Length - 1] != '_')
                    text = text.Substring(0, text.Length - 1);

                CodeKeyword kw = new CodeKeyword(text, KeywordSource.User, type);
                CodeKeyword ppt1 = new CodeKeyword(text, KeywordSource.C, KeywordType.Preprocessor);
                CodeKeyword ppt2 = new CodeKeyword(text, KeywordSource.CPP, KeywordType.Preprocessor);

                if (preprocKeywords.Contains(ppt1.ListEntry) == false && preprocKeywords.Contains(ppt2.ListEntry) == false)
                    if (finalRes.Contains(kw.ListEntry) == false)
                        finalRes.Add(kw.ListEntry);

                m = m.NextMatch();
            }

            finalRes.Sort((x, y) => string.Compare(x, y));

            return finalRes;
        }

        #endregion

        #region Private Methods

        private static void LoadFileKeywords()
        {
            List<ProjectFile> tmpList = new List<ProjectFile>(project.FileList.Values);
            foreach (ProjectFile file in tmpList)
            {
                if (fileKeywords.ContainsKey(file) == false)
                {
                    GetAllKeywords(file);
                }
            }
        }

        private static void LoadAllLibs()
        {
            try
            {
                List<ProjectFile> tmpList = new List<ProjectFile>(project.FileList.Values);
                foreach (ProjectFile file in tmpList)
                {
                    if (fileLibs.ContainsKey(file) == false)
                    {
                        fileLibs.Add(file, new List<string>());
                        GetAllLibraries(file, fileLibs[file]);
                    }
                }
            }
            catch { }
        }

        private static List<string> GetAllLibraries(ProjectFile file, List<string> libList)
        {
            if (libList == null)
                libList = new List<string>();

            string content = "";

            if (fileContents.ContainsKey(file))
            {
                content = fileContents[file];
            }
            else
                return libList;

            Regex r = new Regex("^\\s*#include\\s+((\\\"\\S+\\\")|(<\\S+>))", RegexOptions.IgnoreCase | RegexOptions.Multiline);
            Match m = r.Match(content);
            while (m.Success)
            {
                string lib = m.Groups[1].Value.Trim(new char[] { ' ', '\t', '\r', '\n', '"', '<', '>', });

                if (libList.Contains(lib) == false && lib != file.FileName)
                {
                    libList.Add(lib);

                    ProjectFile nextfile;
                    if (project.FileList.TryGetValue(lib, out nextfile))
                    {
                        if (fileContents.ContainsKey(nextfile))
                        {
                            if (fileLibs.ContainsKey(nextfile) == false)
                            {
                                fileLibs.Add(nextfile, new List<string>());
                                GetAllLibraries(nextfile, fileLibs[nextfile]);
                            }

                            string[] tmpList = fileLibs[nextfile].ToArray();
                            foreach (string s in tmpList)
                            {
                                if (libList.Contains(s) == false && s != file.FileName)
                                    libList.Add(s);
                            }
                        }
                    }
                }

                m = m.NextMatch();
            }

            return libList;
        }

        private static void GetAllKeywords(ProjectFile file)
        {
            if (fileKeywords.ContainsKey(file) == false)
                fileKeywords.Add(file, new Dictionary<string, CodeKeyword>());

            foreach (CodeKeyword kw in CKeywords.Values)
            {
                if (fileKeywords[file].ContainsKey(kw.Text) == false)
                    fileKeywords[file].Add(kw.Text, kw);
            }

            if (file.FileExt == "cpp" || file.FileExt == "pde")
            {
                foreach (CodeKeyword kw in CPPKeywords.Values)
                {
                    if (fileKeywords[file].ContainsKey(kw.Text) == false)
                        fileKeywords[file].Add(kw.Text, kw);
                }
            }

            if (file.FileExt == "pde")
            {
                foreach (CodeKeyword kw in ArduinoKeywords.Values)
                {
                    if (fileKeywords[file].ContainsKey(kw.Text) == false)
                        fileKeywords[file].Add(kw.Text, kw);
                }
            }

            List<string> libList = null;
            if (fileLibs.ContainsKey(file))
            {
                libList = fileLibs[file];
            }
            else
            {
                GetAllLibraries(file, libList);
            }

            foreach (string libName in libList)
            {
                if (AVRLibcKeywords.ContainsKey(libName))
                {
                    foreach (CodeKeyword kw in AVRLibcKeywords[libName].Values)
                    {
                        if (fileKeywords[file].ContainsKey(kw.Text) == false)
                            fileKeywords[file].Add(kw.Text, kw);
                    }
                }
                else if (project.FileList.ContainsKey(libName))
                {
                    if (fileKeywords.ContainsKey(project.FileList[libName]) == false)
                    {
                        GetAllKeywords(project.FileList[libName]);
                    }

                    foreach (CodeKeyword kw in fileKeywords[project.FileList[libName]].Values)
                    {
                        if (fileKeywords[file].ContainsKey(kw.Text) == false)
                            fileKeywords[file].Add(kw.Text, kw);
                    }
                }
            }

            //return;

            string content = "";

            if (fileContents.ContainsKey(file))
            {
                content = fileContents[file];
            }
            else
                return;

            int contentLen = content.Length;
            content = "\r\n" + content + "\r\n";

            bool inString = false;
            bool inChar = false;

            int nest = 0;

            string res = "";

            for (int i = 2; i < contentLen + 2; i++)
            {
                if ((inString || inChar) && content[i] == '\\' && content[i + 1] == '\\')
                {
                    content = content.Remove(i, 2);
                    content = content.Insert(i, "  ");
                }
                else if (content[i] == '"' && content[i - 1] != '\\' && inChar == false)
                    inString = !inString;
                else if (content[i] == '\'' && content[i - 1] != '\\' && inString == false)
                    inChar = !inChar;
                else if ((content[i] == '(' || content[i] == '{') && inString == false && inChar == false)
                    nest++;
                else if ((content[i] == ')' || content[i] == '}') && inString == false && inChar == false)
                    nest--;

                if (inChar == false && inString == false && nest == 0)
                    res += content[i];
            }

            Regex r = new Regex("([^a-zA-Z0-9_])([a-zA-Z_][a-zA-Z0-9_]*)(\\s*)([^a-zA-Z0-9_])", RegexOptions.Multiline);
            Match m = r.Match("`" + res + "`");

            while (m.Success)
            {
                string text = m.Value;

                KeywordType type = KeywordType.Other;
                if (text.EndsWith("("))
                    type = KeywordType.Function;

                while (char.IsLetterOrDigit(text, 0) == false && text[0] != '_')
                    text = text.Substring(1);

                while (char.IsLetterOrDigit(text, text.Length - 1) == false && text[text.Length - 1] != '_')
                    text = text.Substring(0, text.Length - 1);

                if (fileKeywords[file].ContainsKey(text) == false)
                    fileKeywords[file].Add(text, new CodeKeyword(text, KeywordSource.User, type));

                m = m.NextMatch();
            }
        }

        private static void LoadDefaultKeywords()
        {
            XmlDocument xDoc = new XmlDocument();
            string xmlPath = SettingsManagement.AppDataPath + "autocomplete.xml";
            if (File.Exists(xmlPath) == false)
            {
                try
                {
                    File.WriteAllText(xmlPath, Properties.Resources.autocomplete);
                    xDoc.Load(xmlPath);
                }
                catch
                {
                    xDoc.LoadXml(Properties.Resources.autocomplete);
                }
            }
            else
            {
                xDoc.Load(xmlPath);
            }

            XmlElement docEle = xDoc.DocumentElement;

            LoadStandardKeywords(docEle);
        }

        private static void LoadStandardKeywords(XmlElement container)
        {
            CKeywords.Clear();
            CPPKeywords.Clear();
            ArduinoKeywords.Clear();
            preprocKeywords.Clear();

            foreach (XmlElement lib in container.GetElementsByTagName("C"))
            {
                foreach (XmlElement group in lib.GetElementsByTagName("Group"))
                {
                    string[] wordList = group.InnerText.Split(new char[] { ' ', '\t', '\r', '\n', });
                    foreach (string word in wordList)
                    {
                        if (string.IsNullOrEmpty(word) == false)
                        {
                            CodeKeyword kw = new CodeKeyword(word, KeywordSource.C, KeywordTypeFromString(group.GetAttribute("type")));
                            if (kw.Type == KeywordType.Preprocessor)
                            {
                                if (preprocKeywords.Contains(kw.ListEntry) == false)
                                    preprocKeywords.Add(kw.ListEntry);
                            }
                            else if (CKeywords.ContainsKey(word) == false)
                                CKeywords.Add(word, kw);
                        }
                    }
                }

                break;
            }

            foreach (XmlElement lib in container.GetElementsByTagName("CPP"))
            {
                foreach (XmlElement group in lib.GetElementsByTagName("Group"))
                {
                    string[] wordList = group.InnerText.Split(new char[] { ' ', '\t', '\r', '\n', });
                    foreach (string word in wordList)
                    {
                        if (string.IsNullOrEmpty(word) == false)
                        {
                            CodeKeyword kw = new CodeKeyword(word, KeywordSource.CPP, KeywordTypeFromString(group.GetAttribute("type")));

                            if (kw.Type == KeywordType.Preprocessor)
                            {
                                if (preprocKeywords.Contains(kw.ListEntry) == false)
                                    preprocKeywords.Add(kw.ListEntry);
                            }
                            else if (CPPKeywords.ContainsKey(word) == false)
                                CPPKeywords.Add(word, kw);
                        }
                    }
                }

                break;
            }

            foreach (XmlElement lib in container.GetElementsByTagName("Arduino"))
            {
                foreach (XmlElement group in lib.GetElementsByTagName("Group"))
                {
                    string[] wordList = group.InnerText.Split(new char[] { ' ', '\t', '\r', '\n', });
                    foreach (string word in wordList)
                    {
                        if (string.IsNullOrEmpty(word) == false)
                        {
                            CodeKeyword kw = new CodeKeyword(word, KeywordSource.Arduino, KeywordTypeFromString(group.GetAttribute("type")));
                            if (ArduinoKeywords.ContainsKey(word) == false)
                                ArduinoKeywords.Add(word, kw);
                        }
                    }
                }

                break;
            }

            LoadAVRLibcKeywords(container);

            preprocKeywords.Sort((x, y) => string.Compare(x, y));
        }

        private static void LoadAVRLibcKeywords(XmlElement container)
        {
            foreach (XmlElement lib in container.GetElementsByTagName("File"))
            {
                string fileName = lib.GetAttribute("name").Trim();
                if (string.IsNullOrEmpty(fileName) == false)
                {
                    XmlElement lib_ = (XmlElement)lib.CloneNode(true);
                    List<string> alreadyIncluded = new List<string>();
                    alreadyIncluded.Add(fileName);

                    bool foundNew;

                    do
                    {
                        foundNew = false;
                        string includedInnerXML = "";
                        foreach (XmlElement inc in lib_.GetElementsByTagName("Include"))
                        {
                            if (alreadyIncluded.Contains(inc.InnerText.Trim()) == false)
                            {
                                foreach (XmlElement lib__ in container.GetElementsByTagName("Lib"))
                                {
                                    string fileName_ = lib__.GetAttribute("file").Trim();
                                    if (string.IsNullOrEmpty(fileName_) == false)
                                    {
                                        if (alreadyIncluded.Contains(fileName_) == false && fileName_ == inc.InnerText.Trim())
                                        {
                                            alreadyIncluded.Add(fileName_);
                                            includedInnerXML += lib__.InnerXml;
                                            foundNew = true;
                                        }
                                    }
                                }
                            }
                        }
                        lib_.InnerXml += includedInnerXML;
                    } while (foundNew);

                    string libName = lib_.GetAttribute("name");
                    if (string.IsNullOrEmpty(libName) == false)
                    {
                        Dictionary<string, CodeKeyword> keywordList = new Dictionary<string, CodeKeyword>();
                        foreach (XmlElement group in lib_.GetElementsByTagName("Group"))
                        {
                            string[] wordList = group.InnerText.Split(new char[] { ' ', '\t', '\r', '\n', });
                            foreach (string word in wordList)
                            {
                                if (string.IsNullOrEmpty(word) == false && keywordList.ContainsKey(word) == false)
                                {
                                    keywordList.Add(word, new CodeKeyword(word, KeywordSource.AVRLibc, KeywordTypeFromString(group.GetAttribute("type"))));
                                }
                            }
                        }

                        AVRLibcKeywords.Add(libName, keywordList);
                    }
                }
            }
        }

        private static KeywordType KeywordTypeFromString(string attrib)
        {
            attrib = attrib.ToLowerInvariant().Trim();
            if (attrib.Contains("statement"))
                return KeywordType.Statement;
            else if (attrib.Contains("type"))
                return KeywordType.Type;
            else if (attrib.Contains("funct"))
                return KeywordType.Function;
            else if (attrib.Contains("const"))
                return KeywordType.Constant;
            else if (attrib.Contains("modifier"))
                return KeywordType.Modifier;
            else if (attrib.Contains("preproc"))
                return KeywordType.Preprocessor;
            else if (attrib.Contains("block"))
                return KeywordType.Block;
            else
                return KeywordType.Other;
        }

        #endregion
    }

    public static class KeywordImageGen
    {
        private static Dictionary<int, Bitmap> imgList = new Dictionary<int, Bitmap>();
        private static Dictionary<string, int> keywordIndex = new Dictionary<string, int>();

        public static int LookUpImgIndexForKeyword(KeywordSource source, KeywordType type)
        {
            string str = Enum.GetName(typeof(KeywordSource), source) + Enum.GetName(typeof(KeywordType), type);
            if (keywordIndex.ContainsKey(str))
                return keywordIndex[str];
            else
                return -1;
        }

        public static void GenerateKeywordImages()
        {
            int cnt = 0;

            foreach(KeywordSource source in Enum.GetValues(typeof(KeywordSource)))
            {
                foreach(KeywordType type in Enum.GetValues(typeof(KeywordType)))
                {
                    string imgName = Enum.GetName(typeof(KeywordSource), source) + Enum.GetName(typeof(KeywordType), type);
                    keywordIndex.Add(imgName, cnt);

                    Color color = new Color();

                    switch (source)
                    {
                        case KeywordSource.C:
                            color = Color.Green;
                            break;
                        case KeywordSource.CPP:
                            color = Color.Green;
                            break;
                        case KeywordSource.AVRLibc:
                            color = Color.Orange;
                            break;
                        case KeywordSource.Arduino:
                            color = Color.Blue;
                            break;
                        case KeywordSource.User:
                            color = Color.Purple;
                            break;
                        default:
                            color = Color.Black;
                            break;
                    }

                    KeywordShape shape = new KeywordShape();

                    switch (type)
                    {
                        case KeywordType.Block:
                            shape = KeywordShape.DiamondFrame;
                            break;
                        case KeywordType.Constant:
                            shape = KeywordShape.Ring;
                            break;
                        case KeywordType.Other:
                            shape = KeywordShape.Diamond;
                            break;
                        case KeywordType.Function:
                            shape = KeywordShape.Circle;
                            break;
                        case KeywordType.Modifier:
                            shape = KeywordShape.TriangleFrame;
                            break;
                        case KeywordType.Statement:
                            shape = KeywordShape.Square;
                            break;
                        case KeywordType.Type:
                            shape = KeywordShape.Triangle;
                            break;
                        case KeywordType.Variable:
                            shape = KeywordShape.Box;
                            break;
                        case KeywordType.Preprocessor:
                            shape = KeywordShape.Hash;
                            break;
                        default:
                            shape = KeywordShape.None;
                            break;
                    }

                    imgList.Add(cnt, GenerateImage(color, shape));

                    cnt++;
                }
            }
        }

        private static Bitmap GenerateImage(Color color, KeywordShape shape)
        {
            Bitmap bm = new Bitmap(12, 12);
            Graphics g = Graphics.FromImage(bm);
            Pen pen = new Pen(color, 2);
            
            g.FillRectangle(new Pen(Color.White).Brush, 0, 0, 12, 12);

            switch (shape)
            {
                case KeywordShape.Box:
                    g.DrawRectangle(pen, 3, 3, 6, 6);
                    break;
                case KeywordShape.Circle:
                    g.FillEllipse(pen.Brush, 2, 2, 8, 8);
                    break;
                case KeywordShape.Diamond:
                    g.FillPolygon(pen.Brush, new Point[] { new Point(2, 6), new Point(6, 10), new Point(10, 6), new Point(6, 2), });
                    break;
                case KeywordShape.DiamondFrame:
                    g.DrawPolygon(pen, new Point[] { new Point(2, 6), new Point(6, 10), new Point(10, 6), new Point(6, 2), });
                    break;
                case KeywordShape.InvertedTriangle:
                    g.FillPolygon(pen.Brush, new Point[] { new Point(6, 2), new Point(3, 10), new Point(9, 10), });
                    break;
                case KeywordShape.InvertedTriangleFrame:
                    g.DrawPolygon(pen, new Point[] { new Point(6, 2), new Point(3, 10), new Point(9, 10), });
                    break;
                case KeywordShape.Triangle:
                    g.FillPolygon(pen.Brush, new Point[] { new Point(6, 10), new Point(2, 2), new Point(10, 2), });
                    break;
                case KeywordShape.TriangleFrame:
                    g.DrawPolygon(pen, new Point[] { new Point(6, 9), new Point(2, 2), new Point(9, 2), });
                    break;
                case KeywordShape.Ring:
                    g.DrawEllipse(pen, 3, 3, 6, 6);
                    break;
                case KeywordShape.Square:
                    g.FillRectangle(pen.Brush, 2, 2, 8, 8);
                    break;
                case KeywordShape.Hash:
                    g.FillRectangle(pen.Brush, 1, 3, 10, 2);
                    g.FillRectangle(pen.Brush, 1, 7, 10, 2);
                    g.FillRectangle(pen.Brush, 3, 1, 2, 10);
                    g.FillRectangle(pen.Brush, 7, 1, 2, 10);
                    break;
                case KeywordShape.None:
                default:
                    break;
            }

            return bm;
        }

        public static ScintillaNet.Scintilla ApplyImageList(ScintillaNet.Scintilla scinti)
        {
            Bitmap[] bmList = new Bitmap[imgList.Count];
            foreach (KeyValuePair<int, Bitmap> i in imgList)
            {
                bmList[i.Key] = i.Value;
            }
            scinti.AutoComplete.RegisterImages(bmList.ToList<Bitmap>());
            return scinti;
        }
    }
}
