using System;
using System.IO;
using System.IO.Ports;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Diagnostics;
using System.Threading;

namespace AVRProjectIDE
{
    public partial class ConfigWindow : Form
    {
        #region Fields and Properties

        private AVRProject project;
        private bool doNotAllowClose;

        #endregion

        public ConfigWindow(AVRProject project)
        {
            InitializeComponent();

            List<string> orderedDevices = new List<string>();
            foreach (string s in dropDevices.Items)
                orderedDevices.Add(s);

            orderedDevices.Sort((x, y) => string.Compare(x, y));

            dropDevices.Items.Clear();
            foreach (string s in orderedDevices)
                dropDevices.Items.Add(s);

            this.project = project;

            project.HasBeenConfigged = true;

            projBurner = new ProjectBurner(project);

            dropProg.Items.Clear();
            dropProg.Items.AddRange(ProjectBurner.GetAvailProgrammers(true));

            dropPart.Items.Clear();
            if (dropProg.Items.Count > 0)
                dropPart.Items.AddRange(ProjectBurner.GetAvailParts((string)dropProg.Items[0], true));

            dropPort.Items.Clear();
            dropPort.Items.Add("No Override");
            dropPort.Items.Add("LPT1");
            dropPort.Items.Add("LPT2");
            dropPort.Items.Add("LPT3");
            string[] portList = SerialPort.GetPortNames();
            foreach (string port in portList)
            {
                dropPort.Items.Add(port);
            }
            dropPort.SelectedIndex = 0;
            dropBaud.SelectedIndex = 0;

            txtFavoriteDir.Text = SettingsManagement.FavFolder;
            txtArduinoCore.Text = SettingsManagement.ArduinoCorePath;
            txtArduinoLibs.Text = SettingsManagement.ArduinoLibPath;

            string[] templateList = ProjTemplate.GetTemplateNames();
            foreach (string tempName in templateList)
            {
                dropTemplates.Items.Add(tempName);
            }
            if (dropTemplates.Items.Count == 0)
            {
                dropTemplates.Items.Add("No Templates Available");
            }
            dropTemplates.SelectedIndex = 0;

            doNotAllowClose = false;

            PopulateForm();
        }

        #region Saving and Loading

        private void frmProjEditor_FormClosing(object sender, FormClosingEventArgs e)
        {
            FormToProj();
            e.Cancel = doNotAllowClose;
        }

        private SaveResult SaveProj()
        {
            if (string.IsNullOrEmpty(project.FilePath))
            {
                return SaveProjAs();
            }
            else
            {
                if (project.Save(project.FilePath))
                {
                    return SaveResult.Successful;
                }
                else
                {
                    MessageBox.Show("Save Failed");
                    return SaveResult.Failed;
                }
            }
        }

        private SaveResult SaveProjAs()
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.OverwritePrompt = true;
            sfd.Filter = "AVR Project (*.avrproj)|*.avrproj"; 
            if (string.IsNullOrEmpty(project.FilePath) == false)
                sfd.InitialDirectory = project.FilePath.Substring(0, project.FilePath.LastIndexOf('\\'));
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                FormToProj();
                project.FilePath = sfd.FileName;

                if (project.Save(sfd.FileName))
                {
                    return SaveResult.Successful;
                }
                else
                {
                    MessageBox.Show("Save Failed");
                    return SaveResult.Failed;
                }
            }
            else
            {
                return SaveResult.Cancelled;
            }
        }

        private void FormToProj()
        {
            project.OutputDir = txtOutputPath.Text;

            string newDev = (string)dropDevices.Items[dropDevices.SelectedIndex];

            if (project.Device != newDev)
                project.ShouldReloadDevice = true;

            project.Device = newDev;
            project.ClockFreq = numClockFreq.Value;
            project.LinkerOptions = txtLinkerOptions.Text;
            project.OtherOptions = txtOtherOptions.Text;

            project.PackStructs = chklistOptions.GetItemChecked(2);
            project.ShortEnums = chklistOptions.GetItemChecked(3);
            project.UnsignedBitfields = chklistOptions.GetItemChecked(1);
            project.UnsignedChars = chklistOptions.GetItemChecked(0);
            project.FunctionSections = chklistOptions.GetItemChecked(4);
            project.DataSections = chklistOptions.GetItemChecked(5);

            project.UseInitStack = chkUseInitStack.Checked;
            project.InitStackAddr = Convert.ToUInt32("0x" + txtInitStackAddr.Text, 16);

            project.BurnOptions = txtBurnOpt.Text;
            project.BurnFuseBox = txtBurnFuseBox.Text;
            project.BurnPart = (string)dropPart.Items[dropPart.SelectedIndex];
            project.BurnProgrammer = (string)dropProg.Items[dropProg.SelectedIndex];
            project.BurnAutoReset = chkAutoReset.Checked;

            int baud = 0;
            string selectedText = (string)dropBaud.Items[dropBaud.SelectedIndex];
            if (int.TryParse(selectedText, out baud))
            {
                project.BurnBaud = int.Parse(selectedText);
            }
            else
            {
                project.BurnBaud = 0;
            }

            selectedText = (string)dropPort.Items[dropPort.SelectedIndex];
            if (selectedText == "No Override")
            {
                project.BurnPort = "";
            }
            else
            {
                project.BurnPort = selectedText;
            }

            project.IncludeDirList.Clear();
            foreach (DataGridViewRow i in dgvIncPaths.Rows)
            {
                if (string.IsNullOrEmpty((string)i.Cells[0].Value) == false)
                    project.IncludeDirList.Add(Program.CleanFilePath(((string)i.Cells[0].Value).Trim('"').Trim()));
            }

            project.LibraryDirList.Clear();
            foreach (DataGridViewRow i in dgvLibPaths.Rows)
            {
                if (string.IsNullOrEmpty((string)i.Cells[0].Value) == false)
                    project.LibraryDirList.Add(Program.CleanFilePath(((string)i.Cells[0].Value).Trim('"').Trim()));
            }

            project.LinkObjList.Clear();
            project.LinkLibList.Clear();
            foreach (object i in listLinkObj.Items)
            {
                string s = (string)i;
                if (string.IsNullOrEmpty(s) == false)
                {
                    if(s.ToLowerInvariant().Trim().EndsWith(".o"))
                    {
                        project.LinkObjList.Add(Program.CleanFilePath(s).Trim('"').Trim());
                    }
                    else if (s.ToLowerInvariant().Trim().EndsWith(".a"))
                    {
                        project.LinkLibList.Add(Program.CleanFilePath(s).Trim('"').Trim());
                    }
                }
            }

            project.MemorySegList.Clear();
            foreach (DataGridViewRow i in dgvMemory.Rows)
            {
                if (string.IsNullOrEmpty((string)i.Cells[1].Value) == false)
                {
                    project.MemorySegList.Add(new MemorySegment((string)i.Cells[0].Value, (string)i.Cells[1].Value, Convert.ToUInt32("0x" + (string)i.Cells[2].Value, 16)));
                }
            }
        }

        private void PopulateForm()
        {
            txtOutputPath.Text = project.OutputDir;

            if (dropDevices.Items.Count > 0)
            {
                dropDevices.SelectedIndex = 0;
                if (dropDevices.Items.Contains(project.Device))
                    dropDevices.SelectedIndex = dropDevices.Items.IndexOf(project.Device);
                else
                    dropDevices.SelectedIndex = dropDevices.Items.Add(project.Device);
            }
            else
                dropDevices.SelectedIndex = dropDevices.Items.Add(project.Device);

            if (dropPart.Items.Count > 0)
            {
                dropPart.SelectedIndex = 0;
                if (dropPart.Items.Contains(project.BurnPart))
                    dropPart.SelectedIndex = dropPart.Items.IndexOf(project.BurnPart);
                else
                    dropPart.SelectedIndex = dropPart.Items.Add(project.BurnPart);
            }
            else
                dropPart.SelectedIndex = dropPart.Items.Add(project.BurnPart);

            if (dropProg.Items.Count > 0)
            {
                dropProg.SelectedIndex = 0;
                if (dropProg.Items.Contains(project.BurnProgrammer))
                    dropProg.SelectedIndex = dropProg.Items.IndexOf(project.BurnProgrammer);
                else
                    dropProg.SelectedIndex = dropProg.Items.Add(project.BurnProgrammer);
            }
            else
                dropProg.SelectedIndex = dropProg.Items.Add(project.BurnProgrammer);

            if (dropProg.Items.Count > 0)
            {
                dropPort.SelectedIndex = 0;
                if (dropPort.Items.Contains(project.BurnPort))
                {
                    dropPort.SelectedIndex = dropPort.Items.IndexOf(project.BurnPort);
                }
                else
                {
                    if (string.IsNullOrEmpty(project.BurnPort) == false)
                    {
                        int i = dropPort.Items.Add(project.BurnPort);
                        dropPort.SelectedIndex = i;
                    }
                }
            }

            dropBaud.SelectedIndex = 0;
            if (dropBaud.Items.Contains(project.BurnBaud.ToString("0")))
            {
                dropBaud.SelectedIndex = dropBaud.Items.IndexOf(project.BurnBaud.ToString("0"));
            }
            else
            {
                if (project.BurnBaud != 0)
                {
                    int i = dropBaud.Items.Add(project.BurnBaud.ToString("0"));
                    dropBaud.SelectedIndex = i;
                }
            }

            txtOtherOptions.Text = project.OtherOptions;
            txtLinkerOptions.Text = project.LinkerOptions;
            txtInitStackAddr.Text = Convert.ToString(project.InitStackAddr, 16).ToUpper();
            numClockFreq.Value = project.ClockFreq;
            chkUseInitStack.Checked = project.UseInitStack;
            listOptimization.SelectedIndex = listOptimization.Items.IndexOf(project.Optimization);

            txtBurnOpt.Text = project.BurnOptions;
            txtBurnFuseBox.Text = project.BurnFuseBox;
            chkAutoReset.Checked = project.BurnAutoReset;

            chklistOptions.SetItemChecked(2, project.PackStructs);
            chklistOptions.SetItemChecked(3, project.ShortEnums);
            chklistOptions.SetItemChecked(1, project.UnsignedBitfields);
            chklistOptions.SetItemChecked(0, project.UnsignedChars);
            chklistOptions.SetItemChecked(4, project.FunctionSections);
            chklistOptions.SetItemChecked(5, project.DataSections);

            listLinkObj.Items.Clear();
            foreach (string i in project.LinkLibList)
            {
                listLinkObj.Items.Add(i);
            }
            foreach (string i in project.LinkObjList)
            {
                listLinkObj.Items.Add(i);
            }

            dgvIncPaths.Rows.Clear();
            foreach (string s in project.IncludeDirList)
            {
                int i = dgvIncPaths.Rows.Add(new DataGridViewRow());
                dgvIncPaths.Rows[i].Cells[0].Value = s;
            }

            dgvLibPaths.Rows.Clear();
            foreach (string s in project.LibraryDirList)
            {
                int i = dgvLibPaths.Rows.Add(new DataGridViewRow());
                dgvLibPaths.Rows[i].Cells[0].Value = s;
            }

            dgvMemory.Rows.Clear();
            foreach (MemorySegment m in project.MemorySegList)
            {
                DataGridViewRow dgvr = new DataGridViewRow();
                string[] memStr = new string[3];
                string s = "Flash";
                if (m.Type.ToLowerInvariant().Contains("flash"))
                    s = "Flash";
                else if (m.Type.ToLowerInvariant().Contains("eeprom"))
                    s = "EEPROM";
                else if (m.Type.ToLowerInvariant().Contains("sram"))
                    s = "SRAM";
                memStr[0] = s;
                memStr[1] = m.Name;
                memStr[2] = Convert.ToString(m.Addr, 16).ToUpper();
                dgvr.CreateCells(dgvMemory, memStr);
                int i = dgvMemory.Rows.Add(dgvr);
            }
        }

        #endregion

        #region Validation

        private void txtInitStackAddr_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                Convert.ToUInt32(txtInitStackAddr.Text, 16);
                doNotAllowClose = false;
            }
            catch
            {
                MessageBox.Show("Invalid Stack Address");
                e.Cancel = true;
                doNotAllowClose = true;
            }
        }

        private void dgvMemory_RowValidating(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (e.RowIndex == dgvMemory.Rows.Count)
                return;

            try
            {
                if (Convert.ToUInt32((string)dgvMemory.Rows[e.RowIndex].Cells[2].Value, 16) == 0)
                {
                    if (string.IsNullOrEmpty((string)dgvMemory.Rows[e.RowIndex].Cells[2].Value))
                    {
                        dgvMemory.Rows[e.RowIndex].Cells[2].Value = "0";
                    }
                }

                doNotAllowClose = false;
            }
            catch
            {
                MessageBox.Show("Invalid Memory Address");
                e.Cancel = true;
                doNotAllowClose = true;
            }

            if (string.IsNullOrEmpty((string)dgvMemory.Rows[e.RowIndex].Cells[1].Value))
            {
                MessageBox.Show("Please Specify a Memory Name");
                e.Cancel = true;
                doNotAllowClose = true;
            }
            else if (((string)dgvMemory.Rows[e.RowIndex].Cells[1].Value).Contains(' '))
            {
                MessageBox.Show("No Spaces are Allowed in the Memory Name");
                e.Cancel = true;
                doNotAllowClose = true;
            }
            else
            {
                doNotAllowClose = doNotAllowClose | false;
            }

            if (string.IsNullOrEmpty((string)dgvMemory.Rows[e.RowIndex].Cells[0].Value))
            {
                dgvMemory.Rows[e.RowIndex].Cells[0].Value = "Flash";
            }
        }

        #endregion

        #region Library and Link Object Page

        private void btnAddLib_Click(object sender, EventArgs e)
        {
            if (listAvailLibs.SelectedIndex >= 0)
            {
                if (listLinkObj.Items.Contains(listAvailLibs.SelectedItem) == false)
                {
                    int i = listLinkObj.Items.Count;
                    if (listLinkObj.SelectedIndex >= 0)
                    {
                        i = listLinkObj.SelectedIndex + 1;
                    }
                    listLinkObj.Items.Insert(i, listAvailLibs.SelectedItem);
                }
            }
        }

        private void btnLibRemove_Click(object sender, EventArgs e)
        {
            if (listLinkObj.SelectedIndex >= 0)
            {
                listLinkObj.Items.RemoveAt(listLinkObj.SelectedIndex);
            }
        }

        private void btnAddLibFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Link Object (*.o)|*.o|Library (*.a)|*.a";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                int i = listLinkObj.Items.Count;
                if (listLinkObj.SelectedIndex >= 0)
                {
                    i = listLinkObj.SelectedIndex + 1;
                }
                listLinkObj.Items.Insert(i, ofd.FileName);
            }
        }

        private void btnLibMoveUp_Click(object sender, EventArgs e)
        {
            if (listLinkObj.SelectedIndex >= 1)
            {
                int i = listLinkObj.SelectedIndex;
                string s = (string)listLinkObj.Items[i];
                listLinkObj.Items.RemoveAt(i);
                listLinkObj.Items.Insert(i - 1, s);
                listLinkObj.SelectedIndex = i - 1;
            }
        }

        private void btnLibMoveDown_Click(object sender, EventArgs e)
        {
            if (listLinkObj.SelectedIndex >= 0 && listLinkObj.SelectedIndex < listLinkObj.Items.Count - 1)
            {
                int i = listLinkObj.SelectedIndex;
                string s = (string)listLinkObj.Items[i];
                listLinkObj.Items.RemoveAt(i);
                listLinkObj.Items.Insert(i + 1, s);
                listLinkObj.SelectedIndex = i + 1;
            }
        }

        #endregion

        #region Included Directory Pages

        private void btnIncDirAdd_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.ShowNewFolderButton = false;
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                int i = dgvIncPaths.Rows.Add(new DataGridViewRow());
                dgvIncPaths.Rows[i].Cells[0].Value = fbd.SelectedPath;
            }
        }

        private void btnLibDirAdd_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.ShowNewFolderButton = false;
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                int i = dgvLibPaths.Rows.Add(new DataGridViewRow());
                dgvLibPaths.Rows[i].Cells[0].Value = fbd.SelectedPath;
            }
        }

        private void txtOutputPath_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtOutputPath.Text))
            {
                doNotAllowClose = true;
                e.Cancel = true;
            }
            else
            {
                txtOutputPath.Text = Program.CleanFilePath(txtOutputPath.Text).Replace(' ', '_');
                doNotAllowClose = false;
                e.Cancel = false;
            }
        }

        private void btnIncPathMoveUp_Click(object sender, EventArgs e)
        {
            int i = -1;
            if (dgvIncPaths.SelectedRows.Count == 1 && dgvIncPaths.Rows.Count > 2)
            {
                i = dgvIncPaths.SelectedRows[0].Index;
            }
            else if (dgvIncPaths.SelectedCells.Count == 1 && dgvIncPaths.Rows.Count > 2)
            {
                i = dgvIncPaths.SelectedCells[0].RowIndex;
            }
            if (i != -1)
            {
                if (i > 0)
                {
                    if (dgvIncPaths.SelectedRows.Count == 1)
                    {
                        dgvIncPaths.SelectedRows[0].Selected = false;
                    }
                    else if (dgvIncPaths.SelectedCells.Count == 1)
                    {
                        dgvIncPaths.SelectedCells[0].Selected = false;
                    }
                    string s1 = (string)dgvIncPaths.Rows[i].Cells[0].Value;
                    string s2 = (string)dgvIncPaths.Rows[i - 1].Cells[0].Value;
                    dgvIncPaths.Rows[i - 1].Cells[0].Value = s1;
                    dgvIncPaths.Rows[i].Cells[0].Value = s2;
                    dgvIncPaths.Rows[i - 1].Selected = true;
                }
            }
        }

        private void btnIncPathMoveDown_Click(object sender, EventArgs e)
        {
            int i = -1;
            if (dgvIncPaths.SelectedRows.Count == 1 && dgvIncPaths.Rows.Count > 2)
            {
                i = dgvIncPaths.SelectedRows[0].Index;
            }
            else if (dgvIncPaths.SelectedCells.Count == 1 && dgvIncPaths.Rows.Count > 2)
            {
                i = dgvIncPaths.SelectedCells[0].RowIndex;
            }
            if (i != -1)
            {
                if (i < dgvIncPaths.Rows.Count - 2)
                {
                    if (dgvIncPaths.SelectedRows.Count == 1)
                    {
                        dgvIncPaths.SelectedRows[0].Selected = false;
                    }
                    else if (dgvIncPaths.SelectedCells.Count == 1)
                    {
                        dgvIncPaths.SelectedCells[0].Selected = false;
                    }
                    string s1 = (string)dgvIncPaths.Rows[i].Cells[0].Value;
                    string s2 = (string)dgvIncPaths.Rows[i + 1].Cells[0].Value;
                    dgvIncPaths.Rows[i + 1].Cells[0].Value = s1;
                    dgvIncPaths.Rows[i].Cells[0].Value = s2;
                    dgvIncPaths.Rows[i + 1].Selected = true;
                }
            }
        }

        private void btnLibPathMoveUp_Click(object sender, EventArgs e)
        {
            int i = -1;
            if (dgvLibPaths.SelectedRows.Count == 1 && dgvLibPaths.Rows.Count > 2)
            {
                i = dgvLibPaths.SelectedRows[0].Index;
            }
            else if (dgvLibPaths.SelectedCells.Count == 1 && dgvLibPaths.Rows.Count > 2)
            {
                i = dgvLibPaths.SelectedCells[0].RowIndex;
            }
            if (i != -1)
            {
                if (i > 0)
                {
                    if (dgvLibPaths.SelectedRows.Count == 1)
                    {
                        dgvLibPaths.SelectedRows[0].Selected = false;
                    }
                    else if (dgvLibPaths.SelectedCells.Count == 1)
                    {
                        dgvLibPaths.SelectedCells[0].Selected = false;
                    }
                    string s1 = (string)dgvLibPaths.Rows[i].Cells[0].Value;
                    string s2 = (string)dgvLibPaths.Rows[i - 1].Cells[0].Value;
                    dgvLibPaths.Rows[i - 1].Cells[0].Value = s1;
                    dgvLibPaths.Rows[i].Cells[0].Value = s2;
                    dgvLibPaths.Rows[i - 1].Selected = true;
                }
            }
        }

        private void btnLibPathMoveDown_Click(object sender, EventArgs e)
        {
            int i = -1;
            if (dgvLibPaths.SelectedRows.Count == 1 && dgvLibPaths.Rows.Count > 2)
            {
                i = dgvLibPaths.SelectedRows[0].Index;
            }
            else if (dgvLibPaths.SelectedCells.Count == 1 && dgvLibPaths.Rows.Count > 2)
            {
                i = dgvLibPaths.SelectedCells[0].RowIndex;
            }
            if (i != -1)
            {
                if (i < dgvLibPaths.Rows.Count - 2)
                {
                    if (dgvLibPaths.SelectedRows.Count == 1)
                    {
                        dgvLibPaths.SelectedRows[0].Selected = false;
                    }
                    else if (dgvLibPaths.SelectedCells.Count == 1)
                    {
                        dgvLibPaths.SelectedCells[0].Selected = false;
                    }
                    string s1 = (string)dgvLibPaths.Rows[i].Cells[0].Value;
                    string s2 = (string)dgvLibPaths.Rows[i + 1].Cells[0].Value;
                    dgvLibPaths.Rows[i + 1].Cells[0].Value = s1;
                    dgvLibPaths.Rows[i].Cells[0].Value = s2;
                    dgvLibPaths.Rows[i + 1].Selected = true;
                }
            }
        }

        #endregion

        #region Burning

        private ProjectBurner projBurner;

        private void btnBurnOnlyOpt_Click(object sender, EventArgs e)
        {
            FormToProj();
            projBurner.BurnCMD(true, false, this);
        }

        #endregion

        #region Last Tab On The Right

        private void btnGotoAppdata_Click(object sender, EventArgs e)
        {
            Process.Start(SettingsManagement.AppDataPath);
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

        private void btnApplyTemplate_Click(object sender, EventArgs e)
        {
            if (ProjTemplate.ApplyTemplate((string)dropTemplates.Items[dropTemplates.SelectedIndex], project) == false)
            {
                MessageBox.Show("Template was not applied successfully");
            }
            else
            {
                MessageBox.Show("Template \"" + (string)dropTemplates.Items[dropTemplates.SelectedIndex] + "\" was applied");
            }

            PopulateForm();
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

        #endregion

        private void lnkFuse_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(SettingsManagement.FuseCalcLink);
        }

        private void btnBurnFuseBox_Click(object sender, EventArgs e)
        {
            FormToProj();
            projBurner.BurnCMD(false, true, this);
        }
    }
}
