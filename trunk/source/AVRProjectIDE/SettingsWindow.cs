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
        public SettingsWindow()
        {
            InitializeComponent();

            chkAutocomplete.Checked = SettingsManagement.AutocompleteEnable;
            txtFavoriteDir.Text = SettingsManagement.FavFolder;
            txtArduinoCore.Text = SettingsManagement.ArduinoCorePath;
            txtArduinoLibs.Text = SettingsManagement.ArduinoLibPath;
        }

        private void btnFavoriteBrowse_Click(object sender, EventArgs e)
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

        private void chkAutocomplete_CheckedChanged(object sender, EventArgs e)
        {
            SettingsManagement.AutocompleteEnable = chkAutocomplete.Checked;
        }
    }
}
