using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Net;
using System.Text.RegularExpressions;

namespace AVRProjectIDE
{
    public class UpdateMech
    {
        private static bool updateAvail = false;
        private static string oldBuildID = "";
        private static string newBuildID = "";
        private static Thread checkerThread;

        public static void CheckForUpdates()
        {
            oldBuildID = SettingsManagement.BuildID;

            if (string.IsNullOrEmpty(oldBuildID))
                oldBuildID = Guid.NewGuid().ToString();

            checkerThread = new Thread(new ThreadStart(GetBuildID));
            checkerThread.Start();
        }

        private static void GetBuildID()
        {
            try
            {
                WebRequest wReq = WebRequest.Create("http://code.google.com/p/avr-project-ide/wiki/BuildID");
                wReq.Credentials = CredentialCache.DefaultCredentials;
                HttpWebResponse wResp = (HttpWebResponse)wReq.GetResponse();
                Stream wStream = wResp.GetResponseStream();
                StreamReader reader = new StreamReader(wStream);
                string content = reader.ReadToEnd().Replace("&quot;", "\"");

                Regex r = new Regex("(BUILDID:)(\".*?\")");
                Match m = r.Match(content);
                if (m.Success)
                {
                    newBuildID = m.Groups[2].Value.Trim('"');
                    if (newBuildID != oldBuildID)
                        updateAvail = true;
                }

                reader.Close();
                wStream.Close();
                wResp.Close();
            }
            catch
            {
                updateAvail = false;
            }
        }

        public static bool HasFinishedChecking
        {
            get { return checkerThread.ThreadState != ThreadState.Running; }
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
    }
}
