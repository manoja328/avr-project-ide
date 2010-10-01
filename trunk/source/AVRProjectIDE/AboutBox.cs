using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace AVRProjectIDE
{
    partial class AboutBox : Form
    {
        public AboutBox()
        {
            InitializeComponent();

            this.Icon = GraphicsResx.mainicon;

            this.Text = String.Format("About {0}", AssemblyTitle);
            this.textBox1.Text = string.Format(
                "{0}" + Environment.NewLine +
                "Version {1}" + Environment.NewLine +
                "Version Date: {2}" + Environment.NewLine +
                "{3}" + Environment.NewLine +
                "Company: {4}" + Environment.NewLine +
                "Description: {5}" + Environment.NewLine +
                "----------" + Environment.NewLine,
                AssemblyProduct,
                AssemblyVersion,
                AssemblyDate <= DateTime.Now ? AssemblyDate.ToString("MMMM d yyyy") : "Error",
                AssemblyCopyright,
                AssemblyCompany,
                AssemblyDescription);

            this.textBox1.Text += "Default Arduino Core Path: " + SettingsManagement.ArduinoCorePath + Environment.NewLine;
            this.textBox1.Text += "Arduino Library Path: " + SettingsManagement.ArduinoLibPath + Environment.NewLine;
            this.textBox1.Text += "AppData Path: " + SettingsManagement.AppDataPath + Environment.NewLine;
            this.textBox1.Text += "AppInstall Path: " + SettingsManagement.AppInstallPath + Environment.NewLine;
            this.textBox1.Text += "----------" + Environment.NewLine;
            var assems = AppDomain.CurrentDomain.GetAssemblies();
            foreach (var a in assems)
            {
                if (a.Location.ToLowerInvariant().StartsWith(SettingsManagement.AppInstallPath.ToLowerInvariant()))
                {
                    this.textBox1.Text += string.Format(
                        "Assembly: {0}" + Environment.NewLine +
                        "File: {1}" + Environment.NewLine +
                        "-----------" + Environment.NewLine,
                        a.FullName,
                        a.Location
                        );
                }
            }
            foreach (var a in assems)
            {
                if (false == a.Location.ToLowerInvariant().StartsWith(SettingsManagement.AppInstallPath.ToLowerInvariant()))
                {
                    this.textBox1.Text += string.Format(
                        "Assembly: {0}" + Environment.NewLine +
                        "File: {1}" + Environment.NewLine +
                        "-----------" + Environment.NewLine,
                        a.FullName,
                        a.Location
                        );
                }
            }
        }

        #region Assembly Attribute Accessors

        public static string AssemblyTitle
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

        public static string AssemblyVersion
        {
            get
            {
                return SettingsManagement.Version;
            }
        }

        public static string AssemblyDescription
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyDescriptionAttribute)attributes[0]).Description;
            }
        }

        public static string AssemblyProduct
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyProductAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyProductAttribute)attributes[0]).Product;
            }
        }

        public static string AssemblyCopyright
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyCopyrightAttribute)attributes[0]).Copyright;
            }
        }

        public static string AssemblyCompany
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCompanyAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyCompanyAttribute)attributes[0]).Company;
            }
        }

        public static DateTime AssemblyDate
        {
            get
            {
                try
                {
                    var version = Assembly.GetEntryAssembly().GetName().Version;
                    return new DateTime(2000, 1, 1).Add(new TimeSpan(
                    TimeSpan.TicksPerDay * version.Build + // days since 1 January 2000
                    TimeSpan.TicksPerSecond * 2 * version.Revision)); // seconds since midnight, (multiply by 2 to get original)
                }
                catch
                {
                }

                try
                {
                    string filePath = System.Reflection.Assembly.GetCallingAssembly().Location;
                    const int c_PeHeaderOffset = 60;
                    const int c_LinkerTimestampOffset = 8;
                    byte[] b = new byte[2048];
                    System.IO.Stream s = null;

                    try
                    {
                        s = new System.IO.FileStream(filePath, System.IO.FileMode.Open, System.IO.FileAccess.Read);
                        s.Read(b, 0, 2048);
                    }
                    finally
                    {
                        if (s != null)
                        {
                            s.Close();
                        }
                    }

                    int i = System.BitConverter.ToInt32(b, c_PeHeaderOffset);
                    int secondsSince1970 = System.BitConverter.ToInt32(b, i + c_LinkerTimestampOffset);
                    DateTime dt = new DateTime(1970, 1, 1, 0, 0, 0);
                    dt = dt.AddSeconds(secondsSince1970);
                    dt = dt.AddHours(TimeZone.CurrentTimeZone.GetUtcOffset(dt).Hours);
                    return dt;
                }
                catch
                {
                    return DateTime.Now.AddDays(2).Date;
                }
            }
        }

        #endregion

        private void lnkButton_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(Properties.Resources.WebsiteURL);
        }

        [System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.Demand, Name = "FullTrust")]
        protected override void WndProc(ref Message m)
        {
            try
            {
                base.WndProc(ref m);
            }
            catch (Exception ex)
            {
                ErrorReportWindow.Show(ex, "Error In About Box");
                
            }
        }

        private void btnDonate_Click(object sender, EventArgs e)
        {
            Program.LaunchDonate();
        }

        private void picAdBox_Click(object sender, EventArgs e)
        {
            Program.GotoUSnooBie();
        }
    }
}
