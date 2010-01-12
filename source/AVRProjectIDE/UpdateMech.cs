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
            if (SettingsManagement.CheckForUpdates == false)
            {

                return;
            }

            oldBuildID = SettingsManagement.BuildID;

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

                Regex r = new Regex("(BUILDID:)(\".*?\")");
                Match m = r.Match(content);
                if (m.Success)
                {
                    newBuildID = m.Groups[2].Value.Trim('"');
                    if (newBuildID != oldBuildID)
                        updateAvail = true;
                }
            }
            catch
            {
                updateAvail = false;
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
