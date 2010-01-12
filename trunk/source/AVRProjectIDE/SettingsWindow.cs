using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AVRProjectIDE
{
    public partial class SettingsWindow : Form
    {
        [System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.Demand, Name = "FullTrust")]
        protected override void WndProc(ref Message m)
        {
            try
            {
                base.WndProc(ref m);
            }
            catch (Exception ex)
            {
                ErrorReportWindow erw = new ErrorReportWindow(ex, "Error In Settings Window");
                erw.ShowDialog();
            }
        }

        bool backspaceUnindents = true;
        bool tabIndents = true;
        bool useTabs = true;
        int tabWidth = 4;
        int indentWidth = 4;
        int smartIndent = 3;

        bool indentGuide = true;
        bool lineWrap = true;

        public SettingsWindow()
        {
            InitializeComponent();

            txtFavoriteDir.Text = SettingsManagement.FavFolder;
            txtArduinoCore.Text = SettingsManagement.ArduinoCorePath;
            txtArduinoLibs.Text = SettingsManagement.ArduinoLibPath;
            chkCheckUpdates.Checked = SettingsManagement.CheckForUpdates;

            string tmpStr = SettingsManagement.SettingsFile.Read("Editor", "IndentWidth");
            int i = 0;
            if (string.IsNullOrEmpty(tmpStr))
            {
                SettingsManagement.SettingsFile.Write("Editor", "IndentWidth", indentWidth.ToString("0"));
            }
            else if (int.TryParse(tmpStr, out i))
            {
                indentWidth = i;
            }
            else
            {
                SettingsManagement.SettingsFile.Write("Editor", "IndentWidth", indentWidth.ToString("0"));
            }

            tmpStr = SettingsManagement.SettingsFile.Read("Editor", "IndentTabWidth");
            if (string.IsNullOrEmpty(tmpStr))
            {
                SettingsManagement.SettingsFile.Write("Editor", "IndentTabWidth", tabWidth.ToString("0"));
            }
            else if (int.TryParse(tmpStr, out i))
            {
                tabWidth = i;
            }
            else
            {
                SettingsManagement.SettingsFile.Write("Editor", "IndentTabWidth", tabWidth.ToString("0"));
            }

            tmpStr = SettingsManagement.SettingsFile.Read("Editor", "IndentUseTab");
            if (string.IsNullOrEmpty(tmpStr))
            {
                SettingsManagement.SettingsFile.Write("Editor", "IndentUseTab", useTabs.ToString().ToLowerInvariant().Trim());
            }
            else
            {
                useTabs = Program.StringToBool(tmpStr);
            }

            tmpStr = SettingsManagement.SettingsFile.Read("Editor", "IndentBackspaceUnindents");
            if (string.IsNullOrEmpty(tmpStr))
            {
                SettingsManagement.SettingsFile.Write("Editor", "IndentBackspaceUnindents", backspaceUnindents.ToString().ToLowerInvariant().Trim());
            }
            else
            {
                backspaceUnindents = Program.StringToBool(tmpStr);
            }

            tmpStr = SettingsManagement.SettingsFile.Read("Editor", "IndentTabIndents");
            if (string.IsNullOrEmpty(tmpStr))
            {
                SettingsManagement.SettingsFile.Write("Editor", "IndentTabIndents", tabIndents.ToString().ToLowerInvariant().Trim());
            }
            else
            {
                tabIndents = Program.StringToBool(tmpStr);
            }

            tmpStr = SettingsManagement.SettingsFile.Read("Editor", "IndentGuide");
            if (string.IsNullOrEmpty(tmpStr))
            {
                SettingsManagement.SettingsFile.Write("Editor", "IndentGuide", indentGuide.ToString().ToLowerInvariant().Trim());
            }
            else
            {
                indentGuide = Program.StringToBool(tmpStr);
            }

            tmpStr = SettingsManagement.SettingsFile.Read("Editor", "IndentSmartness");
            if (string.IsNullOrEmpty(tmpStr))
            {
                SettingsManagement.SettingsFile.Write("Editor", "IndentSmartness", smartIndent.ToString("0"));
            }
            else if (int.TryParse(tmpStr, out i))
            {
                smartIndent = i;
            }
            else
            {
                SettingsManagement.SettingsFile.Write("Editor", "IndentSmartness", smartIndent.ToString("0"));
            }

            tmpStr = SettingsManagement.SettingsFile.Read("Editor", "LineWrap");
            if (string.IsNullOrEmpty(tmpStr))
            {
                SettingsManagement.SettingsFile.Write("Editor", "LineWrap", lineWrap.ToString().ToLowerInvariant().Trim());
            }
            else
            {
                lineWrap = Program.StringToBool(tmpStr);
            }

            tmpStr = SettingsManagement.SettingsFile.Read("Editor", "AutocompleteEnable");
            if (string.IsNullOrEmpty(tmpStr))
            {
                SettingsManagement.SettingsFile.Write("Editor", "AutocompleteEnable", chkAutocomplete.Checked.ToString().ToLowerInvariant().Trim());
            }
            else
            {
                chkAutocomplete.Checked = Program.StringToBool(tmpStr);
            }

            tmpStr = SettingsManagement.SettingsFile.Read("Editor", "ShowLineNumbers");
            if (string.IsNullOrEmpty(tmpStr))
            {
                SettingsManagement.SettingsFile.Write("Editor", "ShowLineNumbers", chkShowLineNum.Checked.ToString().ToLowerInvariant().Trim());
            }
            else
            {
                chkShowLineNum.Checked = Program.StringToBool(tmpStr);
            }

            tmpStr = SettingsManagement.SettingsFile.Read("Editor", "ShowWhiteSpace");
            if (string.IsNullOrEmpty(tmpStr))
            {
                SettingsManagement.SettingsFile.Write("Editor", "ShowWhiteSpace", chkShowWS.Checked.ToString().ToLowerInvariant().Trim());
            }
            else
            {
                chkShowWS.Checked = Program.StringToBool(tmpStr);
            }

            tmpStr = SettingsManagement.SettingsFile.Read("Editor", "OpenLastProject");
            if (string.IsNullOrEmpty(tmpStr))
            {
                SettingsManagement.SettingsFile.Write("Editor", "OpenLastProject", chkReopenProject.Checked.ToString().ToLowerInvariant().Trim());
            }
            else
            {
                chkReopenProject.Checked = Program.StringToBool(tmpStr);
            }

            tmpStr = SettingsManagement.SettingsFile.Read("Editor", "HighlightCurrentLine");
            if (string.IsNullOrEmpty(tmpStr))
            {
                SettingsManagement.SettingsFile.Write("Editor", "HighlightCurrentLine", chkHighlightCurLine.Checked.ToString().ToLowerInvariant().Trim());
            }
            else
            {
                chkHighlightCurLine.Checked = Program.StringToBool(tmpStr);
            }

            blockCheckChanged = true;
            tmpStr = SettingsManagement.SettingsFile.Read("Editor", "BuildMessageBehaviour");
            if (string.IsNullOrEmpty(tmpStr))
            {
                SettingsManagement.SettingsFile.Write("Editor", "BuildMessageBehaviour", "NewMsgOnTop");
                radMsgOnTop.Checked = true;
            }
            else
            {
                radMsgOnTop.Checked = !tmpStr.Trim().ToLowerInvariant().Contains("bottom");
            }

            radMsgOnBottom.Checked = !radMsgOnTop.Checked;
            blockCheckChanged = false;


            chkUseTabs.Checked = useTabs;
            chkWordWrap.Checked = lineWrap;
            chkTabIndents.Checked = tabIndents;
            chkIndentGuide.Checked = indentGuide;
            chkBackspaceUnindents.Checked = backspaceUnindents;

            numIndentWidth.Value = Math.Max(numIndentWidth.Minimum, Math.Min(indentWidth, numIndentWidth.Maximum));
            numTabWidth.Value = Math.Max(numTabWidth.Minimum, Math.Min(tabWidth, numTabWidth.Maximum));

            numBackupInterval.Value = Math.Max(numBackupInterval.Minimum, Math.Min(SettingsManagement.BackupInterval, numBackupInterval.Maximum));

            dropSmartIndent.SelectedIndex = Math.Max(0, Math.Min(smartIndent, 3));
        }

        private void btnArduinoCoreOverrideBrowse_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();

            if (SettingsManagement.FavFolder != "")
                fbd.SelectedPath = SettingsManagement.FavFolder;

            if (fbd.ShowDialog() == DialogResult.OK)
            {
                SettingsManagement.FavFolder = fbd.SelectedPath;
                txtFavoriteDir.Text = SettingsManagement.FavFolder;
            }
        }

        private void btnFindArduinoCore_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();

            if (SettingsManagement.ArduinoCorePath != "")
                fbd.SelectedPath = SettingsManagement.ArduinoCorePath;

            if (fbd.ShowDialog() == DialogResult.OK)
            {
                SettingsManagement.ArduinoCorePath = fbd.SelectedPath;
                txtArduinoCore.Text = SettingsManagement.ArduinoCorePath;
            }
        }

        private void btnFindArduinoLibs_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();

            if (SettingsManagement.ArduinoLibPath != "")
                fbd.SelectedPath = SettingsManagement.ArduinoLibPath;

            if (fbd.ShowDialog() == DialogResult.OK)
            {
                SettingsManagement.ArduinoLibPath = fbd.SelectedPath;
                txtArduinoLibs.Text = SettingsManagement.ArduinoLibPath;
            }
        }

        private void btnGotoAppdata_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(SettingsManagement.AppDataPath);
        }

        private void btnOpenInstallationFolder_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(SettingsManagement.AppInstallPath);
        }

        private void lnkFolder_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(txtFavoriteDir.Text);
        }

        private void lnkCoreFiles_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(txtArduinoCore.Text);
        }

        private void lnkLibFiles_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(txtArduinoLibs.Text);
        }

        private void SettingsWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            
        }

        private void btnSaveAndClose_Click(object sender, EventArgs e)
        {
            SettingsManagement.AutocompleteEnable = chkAutocomplete.Checked;
            SettingsManagement.SettingsFile.Write("Editor", "AutocompleteEnable", chkAutocomplete.Checked.ToString().ToLowerInvariant().Trim());
            SettingsManagement.SettingsFile.Write("Editor", "IndentWidth", numIndentWidth.Value.ToString("0"));
            SettingsManagement.SettingsFile.Write("Editor", "IndentTabWidth", numTabWidth.Value.ToString("0"));
            SettingsManagement.SettingsFile.Write("Editor", "IndentUseTab", chkUseTabs.Checked.ToString().Trim().ToLowerInvariant());
            SettingsManagement.SettingsFile.Write("Editor", "IndentBackspaceUnindents", chkBackspaceUnindents.Checked.ToString().Trim().ToLowerInvariant());
            SettingsManagement.SettingsFile.Write("Editor", "IndentTabIndents", chkTabIndents.Checked.ToString().Trim().ToLowerInvariant());
            SettingsManagement.SettingsFile.Write("Editor", "IndentGuide", chkIndentGuide.Checked.ToString().Trim().ToLowerInvariant());
            SettingsManagement.SettingsFile.Write("Editor", "IndentSmartness", dropSmartIndent.SelectedIndex.ToString("0"));
            SettingsManagement.SettingsFile.Write("Editor", "LineWrap", chkWordWrap.Checked.ToString().Trim().ToLowerInvariant());
            SettingsManagement.SettingsFile.Write("Editor", "ShowLineNumbers", chkShowLineNum.Checked.ToString().Trim().ToLowerInvariant());
            SettingsManagement.SettingsFile.Write("Editor", "ShowWhiteSpace", chkShowWS.Checked.ToString().Trim().ToLowerInvariant());
            //SettingsManagement.SettingsFile.Write("Editor", "TrimOnSave", chkTrimSpaceOnSave.Checked.ToString().Trim().ToLowerInvariant());
            SettingsManagement.SettingsFile.Write("Editor", "BackupInterval", numBackupInterval.Value.ToString("0"));
            SettingsManagement.SettingsFile.Write("Editor", "BuildMessageBehaviour", radMsgOnTop.Checked ? "NewMsgOnTop" : "NewMsgOnBottom");
            SettingsManagement.SettingsFile.Write("Editor", "OpenLastProject", chkReopenProject.Checked.ToString().Trim().ToLowerInvariant());
            SettingsManagement.SettingsFile.Write("Editor", "HighlightCurrentLine", chkHighlightCurLine.Checked.ToString().Trim().ToLowerInvariant());
            
            SettingsManagement.CheckForUpdates = chkCheckUpdates.Checked;
            SettingsManagement.LoadScintSettings();

            ProjectBuilder.ReverseOutput = radMsgOnBottom.Checked;

            this.Close();
        }

        private void btnDiscardAndClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private bool blockCheckChanged = false;

        private void radMsgOn_CheckedChanged(object sender, EventArgs e)
        {
            if (blockCheckChanged == false)
            {
                blockCheckChanged = true;
                radMsgOnBottom.Checked = !radMsgOnTop.Checked;
                radMsgOnTop.Checked = !radMsgOnBottom.Checked;
                blockCheckChanged = false;
            }
        }
    }
}
