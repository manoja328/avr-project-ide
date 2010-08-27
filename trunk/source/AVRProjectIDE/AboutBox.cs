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
                "{2}" + Environment.NewLine +
                "Company: {3}" + Environment.NewLine +
                "Description: {4}" + Environment.NewLine +
                "----------" + Environment.NewLine,
                AssemblyProduct,
                AssemblyVersion,
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

        public string AssemblyTitle
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

        public string AssemblyVersion
        {
            get
            {
                return SettingsManagement.BuildID;
            }
        }

        public string AssemblyDescription
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

        public string AssemblyProduct
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

        public string AssemblyCopyright
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

        public string AssemblyCompany
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
    }
}
