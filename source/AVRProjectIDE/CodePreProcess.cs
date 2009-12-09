using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace AVRProjectIDE
{
    public class CodePreProcess
    {
        public static string StripComments(string content)
        {
            bool inStreamComment = false;
            bool inLineComment = false;
            bool inString = false;
            bool inChar = false;

            content = Environment.NewLine + content;
            int originalLength = content.Length;
            content += Environment.NewLine;

            string result = "";

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
                    result += content[i];
                }
            }

            return result.Trim();
        }

        public static string SingleWhiteSpace(string content)
        {
            content = content.Replace('\t', ' ').Replace('\n', ' ').Replace('\r', ' ');
            while (content.Contains("  "))
                content.Replace("  ", " ");
            return content.Trim();
        }

        public static string CollapseFunctions(string content)
        {
            content = StripComments(content);

            int braceNest = 0;
            bool inString = false;
            bool inChar = false;

            content = Environment.NewLine + content;
            int originalLength = content.Length;
            content += Environment.NewLine;

            string result = "";

            for (int i = 1; i < originalLength; i++)
            {
                bool printFirstBrace = false;

                if ((inString || inChar) && content[i] == '\\' && content[i + 1] == '\\')
                {
                    content = content.Remove(i, 2);
                    content = content.Insert(i, "  ");
                }
                else if (content[i] == '"' && content[i - 1] != '\\' && inChar == false)
                {
                    inString = !inString;
                }
                else if (content[i] == '\'' && content[i - 1] != '\\' && inString == false)
                {
                    inChar = !inChar;
                }
                else if (content[i] == '{' && inString == false && inChar == false)
                {
                    braceNest++;
                    printFirstBrace = true;
                }
                else if (content[i] == '}' && inString == false && inChar == false)
                {
                    braceNest--;
                }

                if ((braceNest == 1 && printFirstBrace) || braceNest == 0)
                {
                    result += content[i];
                }
            }

            return result.Trim();
        }

        public static List<string> GetAllPrototypes(string content, List<string> protoList)
        {
            if (protoList == null) protoList = new List<string>();

            content = CollapseFunctions(content);

            Regex r = new Regex("([\\w\\[\\]\\*]+\\s+[\\[\\]\\*\\w\\s]+\\([,\\[\\]\\*\\w\\s]*\\))(?=\\s*\\{)", RegexOptions.IgnoreCase | RegexOptions.Multiline);
            Match m = r.Match(content);
            while (m.Success)
            {
                protoList.Add(m.Groups[1].Value.Trim() + ";");
                m = m.NextMatch();
            }

            return protoList;
        }

        public static List<string> GetAllLibraries(string content, List<string> libList)
        {
            if (libList == null) libList = new List<string>();

            content = StripComments(content);

            Regex r = new Regex("^\\s*#include\\s+((\"\\S+\")|(<\\S+>))", RegexOptions.IgnoreCase | RegexOptions.Multiline);
            Match m = r.Match(content);
            while (m.Success)
            {
                string lib = m.Groups[1].Value.Trim(new char[] { ' ', '\t', '\r', '\n', '"', '<', '>', });

                if (lib.Contains('.'))
                {
                    lib = lib.Substring(0, lib.LastIndexOf('.'));
                }

                if (libList.Contains(lib) == false)
                    libList.Add(lib);

                m = m.NextMatch();
            }

            return libList;
        }
    }
}
