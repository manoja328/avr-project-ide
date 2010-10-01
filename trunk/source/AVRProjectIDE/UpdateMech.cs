using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Net;
using System.Web;
using System.Text.RegularExpressions;

namespace AVRProjectIDE
{
    public class UpdateMech
    {
        private static bool updateAvail = false;
        private static string oldBuildID = "";
        private static string newBuildID = "";
        private static string downloadURL = "";
        private static string newFileName = "";
        private static bool autoExe = true;
        private static Thread checkerThread;

        public static void CheckForUpdates()
        {
            UpdateCheckError = true;

            if (SettingsManagement.CheckForUpdates == false)
            {
                return;
            }

            oldBuildID = SettingsManagement.Version;

            checkerThread = new Thread(new ThreadStart(GetBuildID));
            checkerThread.IsBackground = true;
            checkerThread.Priority = ThreadPriority.Lowest;
            checkerThread.Start();
        }

        private static void GetBuildID()
        {
            try
            {
                WebRequest wReq = WebRequest.Create(Properties.Resources.WebsiteURL);
                wReq.Credentials = CredentialCache.DefaultCredentials;
                HttpWebResponse wResp = (HttpWebResponse)wReq.GetResponse();
                Stream wStream = wResp.GetResponseStream();
                StreamReader reader = new StreamReader(wStream);
                string content = HttpUtility.HtmlDecode(reader.ReadToEnd());
                reader.Close();
                wStream.Close();
                wResp.Close();

                string re1="(AVRProjectIDE_Installer)";	// Variable Name 1
                string re2="(\\.)";	// Any Single Character 1
                string re3="(\\d+)";	// Integer Number 1
                string re4="(\\.)";	// Any Single Character 2
                string re5="(\\d+)";	// Integer Number 2
                string re6="(\\.)";	// Any Single Character 3
                string re7="(\\d+)";	// Integer Number 3
                string re8="(\\.)";	// Any Single Character 4
                string re9="(\\d+)";	// Integer Number 4
                string re10="(\\.)";	// Any Single Character 5
                string re11="(exe)";	// Word 1

                Regex r = new Regex(re1+re2+re3+re4+re5+re6+re7+re8+re9+re10+re11,RegexOptions.IgnoreCase|RegexOptions.Singleline);
                Match m = r.Match(content);
                if (m.Success)
                {
                    newBuildID = string.Format("{0}.{1}.{2}.{3}",
                        m.Groups[3].Value,
                        m.Groups[5].Value,
                        m.Groups[7].Value,
                        m.Groups[9].Value);

                    if (newBuildID.ToLowerInvariant() != oldBuildID.ToLowerInvariant())
                        updateAvail = true;

                    UpdateCheckError = false;
                }
            }
            catch
            {
                updateAvail = false;
                UpdateCheckError = true;
            }
        }

        public static bool HasFinishedChecking
        {
            get
            {
                if (checkerThread == null)
                    return true;

                return checkerThread.ThreadState != ThreadState.Running;
            }
        }

        public static bool UpdateAvailable
        {
            get
            {
                if (HasFinishedChecking)
                    return updateAvail;
                else
                    return false;
            }
        }

        public static bool UpdateCheckError
        {
            get;
            set;
        }

        public static string NewBuildID
        {
            get { return newBuildID; }
        }

        public static string DownloadURL
        {
            get { return downloadURL; }
        }

        public static string NewFileName
        {
            get { return newFileName; }
        }

        public bool AutoExe
        {
            get { return autoExe; }
        }
    }
}
