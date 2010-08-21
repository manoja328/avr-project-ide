using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

//#define DEBUGGERWINTEST

namespace AVRProjectIDE
{
    public partial class IDEWindow : Form
    {
        #region Fields and Properties

        private static AVRProject curProj;
        public static AVRProject CurrentProject
        {
            get
            {
                if (curProj == null)
                    return null;

                if (curProj.IsReady == false)
                    return null;

                return curProj;
            }
        }

        private static IDEWindow curOpenWind;
        public static IDEWindow CurrentIDEWindow
        {
            get { return curOpenWind; }
        }

        private AVRProject project;

        private SerialPortPanel serialWin;
        private SearchPanel searchWin;
        private FileTreePanel fileTreeWin;
        private MessagePanel messageWin;
        private HardwareExplorer hardwareExplorerWin;
        private DisassemblyViewer disassemViewer;
        private UsbPanel usbWin;
        
#if DEBUGGERWINTEST
        private DebuggerPanel debuggerWin;
#endif

        private EditorPanel lastEditor;
        
        private ProjectBuilder projBuilder;
        private ProjectBurner projBurner;

        private Dictionary<string, EditorPanel> editorList = new Dictionary<string, EditorPanel>();

        public Dictionary<string, EditorPanel> EditorList
        {
            get { return editorList; }
        }

        public ProjectFile CurrentFile
        {
            get
            {
                if (this.project != null)
                {
                    if (this.project.IsReady)
                    {
                        ProjectFile file = null;

                        if (dockPanel1.ActiveContent != null && dockPanel1.ActiveContent.GetType() == typeof(EditorPanel))
                        {
                            ((EditorPanel)dockPanel1.ActiveContent).Save();
                            file = ((EditorPanel)dockPanel1.ActiveContent).File;
                        }
                        else if (lastEditor != null)
                        {
                            lastEditor.Save();
                            file = lastEditor.File;
                        }

                        return file;
                    }
                }

                return null;
            }
        }

        public AVRProject CurrentProj
        {
            get
            {
                return this.project;
            }
        }

        #endregion

        public IDEWindow(AVRProject project)
        {
            // assign references
            this.project = project;

            InitializeComponent();

            this.Icon = GraphicsResx.mainicon;

            DisableButtons();

            // create panel windows, attach events, etc etc

            fileTreeWin = new FileTreePanel();
            fileTreeWin.OpenNode += new FileTreePanel.OpenFileEvent(fileTreeWin_OpenNode);

            hardwareExplorerWin = new HardwareExplorer();

            searchWin = new SearchPanel(editorList);
            searchWin.GotoResult += new SearchPanel.SearchResultDoubleClickedEvent(searchWin_GotoResult);

            serialWin = new SerialPortPanel(SettingsManagement.PortName, SettingsManagement.BaudRate);
            serialWin.SerialPortException += new SerialPortPanel.SerialPortErrorHandler(serialWin_SerialPortException);

            usbWin = new UsbPanel();

            messageWin = new MessagePanel();
            messageWin.GotoError += new MessagePanel.OnClickError(messageWin_GotoError);

            disassemViewer = new DisassemblyViewer();

            #if DEBUGGERWINTEST
            debuggerWin = new DebuggerPanel(this);
            #endif

            if (project.IsReady)
            {
                EnableButtons();

                projBuilder = new ProjectBuilder(project, messageWin.MyTextBox, messageWin.MyListView, messageWin.MyErrorOnlyListView);
                projBuilder.DoneWork += new ProjectBuilder.EventHandler(projBuilder_DoneWork);
                projBurner = new ProjectBurner(project);
            }

            // fill help menu with a list of bookmarks to websites
            try
            {
                helpToolStripMenuItem.DropDownItems.Add(MenuWebLink.GetMenuLinkRoot("Resources"));
            }
            catch (Exception ex)
            {
                messageWin.MessageBoxModify(TextBoxChangeMode.AppendNewLine, "Error Loading Web Links:, " + ex.Message);
            }

            messageWin.MessageBoxModify(TextBoxChangeMode.AppendNewLine, "Default Arduino Core Path: " + SettingsManagement.ArduinoCorePath);
            messageWin.MessageBoxModify(TextBoxChangeMode.AppendNewLine, "Arduino Library Path: " + SettingsManagement.ArduinoLibPath);
            messageWin.MessageBoxModify(TextBoxChangeMode.AppendNewLine, "AppData Path: " + SettingsManagement.AppDataPath);
            messageWin.MessageBoxModify(TextBoxChangeMode.AppendNewLine, "AppInstall Path: " + SettingsManagement.AppInstallPath);
            messageWin.MessageBoxModify(TextBoxChangeMode.AppendNewLine, "Build Version: " + Properties.Resources.BuildID);
            messageWin.MessageBoxModify(TextBoxChangeMode.AppendNewLine, "Environment Version: " + Environment.Version);
            messageWin.MessageBoxModify(TextBoxChangeMode.AppendNewLine, "OS: " + Environment.OSVersion);

            hardwareExplorerWin.ClockFreq = project.ClockFreq;

            FillRecentProjects();

            timerBackup.Interval = SettingsManagement.BackupInterval * 1000;
            timerBackup.Enabled = true;

            try
            {
                if (System.Net.Dns.GetHostName().ToUpperInvariant().Contains("FRANK"))
                    progressBar1.Visible = true;
            }
            catch { }
        }

        private IDockContent GetPanelFromPersistString(string persistString)
        {
            if (persistString == typeof(SerialPortPanel).ToString())
                return serialWin;
            else if (persistString == typeof(UsbPanel).ToString())
                return usbWin;
            else if (persistString == typeof(SearchPanel).ToString())
                return searchWin;
            else if (persistString == typeof(MessagePanel).ToString())
                return messageWin;
            else if (persistString == typeof(FileTreePanel).ToString())
                return fileTreeWin;
            else if (persistString == typeof(HardwareExplorer).ToString())
                return hardwareExplorerWin;
            else if (persistString == typeof(DisassemblyViewer).ToString())
                return disassemViewer;
            #if DEBUGGERWINTEST
            else if (persistString == typeof(DebuggerPanel).ToString())
                return debuggerWin;
            #endif
            else
            {
                return null;
            }
        }

        #region Events from Other Windows

        void messageWin_GotoError(string fileName, int line)
        {
            GotoEditor(fileName, line);
        }

        private void searchWin_GotoResult(string fileName, int selectionStart, int selectionEnd)
        {
            GotoEditor(fileName, selectionStart, selectionEnd);
        }

        void fileTreeWin_OpenNode(TreeNode node)
        {
            GotoEditor(node);
        }

        private void serialWin_SerialPortException(Exception ex)
        {
            messageWin.MessageBoxModify(TextBoxChangeMode.PrependNewLine, "Serial Port Error:, " + ex.Message);
        }

        private void projBuilder_DoneWork(bool success)
        {
            if (success)
            {
                messageWin.MessageBoxModify(TextBoxChangeMode.PrependNewLine, "Build Succeeded");
                messageWin.SwitchToMessageBox();
            }
            else
            {
                messageWin.MessageBoxModify(TextBoxChangeMode.PrependNewLine, "Build Failed with " + projBuilder.HasError.ToString("0") + " errors");

                if (projBuilder.HasError > 0)
                    messageWin.SwitchToListView();
                else
                    messageWin.SwitchToMessageBox();
            }

            messageWin.BringToFront();
            messageWin.Activate();
        }

        private void dockPanel1_ActiveContentChanged(object sender, EventArgs e)
        {
            if (dockPanel1.ActiveContent != null)
            {
                if (dockPanel1.ActiveContent != null && dockPanel1.ActiveContent.GetType() == typeof(EditorPanel))
                {
                    lastEditor = (EditorPanel)dockPanel1.ActiveContent;
                    project.LastFile = lastEditor.FileName;
                }
            }
        }

        private void dockPanel1_ActiveDocumentChanged(object sender, EventArgs e)
        {
            if (dockPanel1.ActiveDocument != null)
            {
                if (dockPanel1.ActiveDocument.GetType() == typeof(EditorPanel))
                {
                    lastEditor = (EditorPanel)dockPanel1.ActiveDocument;
                    project.LastFile = lastEditor.FileName;
                }
            }
        }

        #endregion

        #region Editor Window Management

        public void ReloadLastOpened()
        {
            if (project.IsReady == false)
                return;

            string lastFileName = project.LastFile;
            foreach (KeyValuePair<string, ProjectFile> i in project.FileList)
            {
                if (i.Value.IsOpen)
                {
                    GotoEditor(i.Value.FileName);
                }
            }

            GotoEditor(lastFileName);
            GotoEditor(lastFileName);
        }

        public bool SaveAll()
        {
            if (project.IsReady == false)
                return true;

            bool success = true;

            // save each window
            foreach (KeyValuePair<string, EditorPanel> editor in editorList)
            {
                success &= editor.Value.Save() == SaveResult.Successful;
            }
            if (success == false)
                MessageBox.Show("Error While Saving One or More Files");

            return success; // both must succeed
        }

        public bool SaveProj()
        {
            if (project.IsReady == false)
                return true;

            if (lastEditor != null)
            {
                project.LastFile = lastEditor.FileName;
            }

            SaveResult res = project.Save();
            if (res == SaveResult.Failed)
            {
                MessageBox.Show("Error While Saving Project Configuration");
                return false;
            }
            else
                return true;
        }

        public bool HasChanged
        {
            get
            {
                // if any window has changed, then return true
                foreach (EditorPanel editor in editorList.Values)
                {
                    if (editor.HasChanged)
                        return true;
                }
                return false;
            }
        }

        public void CloseAll()
        {
            foreach (KeyValuePair<string, EditorPanel> editor in editorList)
            {
                editor.Value.Close();
                // note that calling close on the editor will trigger an event that removes it from the list
                // that's why it's not removed here
            }
        }

        public void GotoEditor(TreeNode node)
        {
            GotoEditor(node.Text);
        }

        private void GotoEditor(string fileName, int line)
        {
            EditorPanel editor = null;
            if (editorList.TryGetValue(fileName.ToLowerInvariant(), out editor) == false)
            {
                // not open, open it first, OpenEditor will add it to the dictionary
                editor = OpenEditor(fileName);
            }

            if (editor != null)
            {
                editor.BringToFront();
                editor.Activate();

                // note that line index and number are off by 1

                editor.Scint.Focus();
                editor.Scint.Caret.Position = editor.Scint.Lines[line - 1].StartPosition;
                editor.Scint.GoTo.Line(line - 1);
                editor.Scint.Selection.Start = editor.Scint.Lines[line - 1].StartPosition;
                editor.Scint.Selection.End = editor.Scint.Lines[line - 1].StartPosition;

                lastEditor = editor;
            }
        }

        private void GotoEditor(string fileName, int selectionStart, int selectionEnd)
        {
            EditorPanel editor = null;
            if (editorList.TryGetValue(fileName.ToLowerInvariant(), out editor) == false)
            {
                editor = OpenEditor(fileName);
            }
            if (editor != null)
            {
                editor.BringToFront();
                editor.Activate();

                editor.Scint.Focus();

                editor.Scint.Caret.Position = selectionStart;
                editor.Scint.GoTo.Position(selectionStart);
                editor.Scint.Selection.Start = selectionStart;
                editor.Scint.Selection.End = selectionEnd;

                lastEditor = editor;
            }
        }

        public void GotoEditor(string fileName)
        {
            EditorPanel editor = null;
            if (editorList.TryGetValue(fileName.ToLowerInvariant(), out editor) == false)
            {
                editor = OpenEditor(fileName);
            }
            if (editor != null)
            {
                editor.BringToFront();
                editor.Activate();
                editor.Scint.Focus();

                lastEditor = editor;
            }
        }

        public EditorPanel OpenEditor(string fileName)
        {
            ProjectFile file;
            if (project.FileList.TryGetValue(fileName.ToLowerInvariant(), out file))
            {
                // file is in project, so open the editor and attach events
                EditorPanel editor = new EditorPanel(file, project, this);
                editor.OnRename += new RenamedEventHandler(editor_OnRename);
                editor.EditorClosed += new EditorPanel.EditorClosedEvent(editor_EditorClosed);
                editor.CloseAllExceptMe += new EditorPanel.CloseAllButMe(editor_CloseAllExceptMe);

                file.Node.ImageKey = "file.ico";
                file.Node.SelectedImageKey = "file.ico";
                file.Node.StateImageKey = "file.ico";

                // add editor to list and show it
                editorList.Add(fileName.ToLowerInvariant(), editor);
                editor.Show(dockPanel1, DockState.Document);

                lastEditor = editor;

                // return reference to work with
                return editor;
            }
            return null;
        }

        void editor_CloseAllExceptMe(string fileName)
        {
            List<EditorPanel> toBeClosed = new List<EditorPanel>(editorList.Values);

            foreach (EditorPanel i in toBeClosed)
            {
                if (i.FileName != fileName)
                {
                    editorList.Remove(i.FileName.ToLowerInvariant());
                    i.Close();
                }
            }
        }

        void editor_EditorClosed(string fileName, object sender, FormClosedEventArgs e)
        {
            EditorPanel editor;
            if (editorList.TryGetValue(fileName, out editor))
            {
                editorList.Remove(fileName);
            }

            KeywordScanner.DoMoreWork();
        }

        void editor_OnRename(object sender, RenamedEventArgs e)
        {
            // this event happens when an external rename occurs

            EditorPanel editor = (EditorPanel)sender;

            // re-key the dictionary
            editor.File.FileAbsPath = e.FullPath;
            ProjectFile f;
            if (project.FileList.TryGetValue(e.OldName.ToLowerInvariant(), out f))
            {
                project.FileList.Remove(e.OldName.ToLowerInvariant());

                if (project.FileList.TryGetValue(editor.FileName.ToLowerInvariant(), out f) == false)
                {
                    project.FileList.Add(editor.FileName.ToLowerInvariant(), editor.File);
                }
                else
                {
                    // note that this should never occur, the function that triggers this event should have already checked
                    MessageBox.Show("Error, File Name Conflict: " + editor.FileName);
                }
            }

            // make sure the file tree displays right
            fileTreeWin.PopulateList(project, editorList);
        }

        #endregion

        #region Misc Form Window Related

        private void frmProjIDE_FormClosing(object sender, FormClosingEventArgs e)
        {
            SettingsManagement.SaveWindowState(this);
        }

        /// <summary>
        /// This intercepts the form closing signal, so that the parent window receives the close signal before the childs (aka the editor tabs)
        /// </summary>
        /// <param name="m"></param>
        //[System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.Demand, Name = "FullTrust")]
        protected override void WndProc(ref Message m)
        {
            try
            {
                // closing signal
                if (m.Msg == 0x0010)
                {
                    SaveProj();

                    if (HasChanged)
                    {
                        // if changes have occured, ask to save everything

                        DialogResult res = MessageBox.Show("You have unsaved changes. Do you want to save?", "Unsaved Project", MessageBoxButtons.YesNoCancel);
                        if (res == DialogResult.Yes)
                        {
                            SaveAll();
                        }
                        else if (res == DialogResult.Cancel)
                        {
                            // cancelled, returning here won't call base.WndProc
                            return;
                        }
                    }

                    // try to close all editor panels
                    // new list because collections shouldn't be modified in foreach
                    List<EditorPanel> toClose = new List<EditorPanel>(editorList.Values);
                    foreach (EditorPanel i in toClose)
                    {
                        i.Close(false);
                    }

                    // this thread, if not killed, may cause the IDE to hang
                    serialWin.Disconnect();
                    serialWin.KillThread();
                }

                base.WndProc(ref m);
            }
            catch (Exception ex)
            {
                ErrorReportWindow.Show(ex, "Error In Main IDE");
                
            }
        }

        private void frmProjIDE_FormClosed(object sender, FormClosedEventArgs e)
        {
            try { LibUsbDotNet.UsbDevice.Exit(); }
            catch { }
            SettingsManagement.SaveZoomLevel();
            dockPanel1.SaveAsXml(SettingsManagement.AppDataPath + "workspace_version_" + Properties.Resources.PanelWorkspaceVersion + ".xml");
        }

        private void mbtnAbout_Click(object sender, EventArgs e)
        {
            AboutBox ab = new AboutBox();
            ab.ShowDialog();
        }

        private void mbtnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tbtnNewOrig_Click(object sender, EventArgs e)
        {
            if (project.IsReady == false)
            {
                mbtnOpenProject_Click(sender, e);
            }
            else
            {
                ProjectFile file;
                if (fileTreeWin.AddFileWiz(out file) == SaveResult.Successful)
                {
                    GotoEditor(file.FileName);
                }
            }
        }

        private void tbtnOpenOrig_Click(object sender, EventArgs e)
        {
            mbtnOpenProject_Click(sender, e);
        }        

        #endregion

        #region Advanced Editing Functions

        private void mbtnComment_Click(object sender, EventArgs e)
        {
            if (dockPanel1.ActiveContent != null && dockPanel1.ActiveContent.GetType() == typeof(EditorPanel))
                ((EditorPanel)dockPanel1.ActiveContent).BlockComment();
            else if (lastEditor != null)
            {
                lastEditor.Activate();
                lastEditor.BlockComment();
            }
        }

        private void tbtnBlockComment_Click(object sender, EventArgs e)
        {
            if (dockPanel1.ActiveContent != null && dockPanel1.ActiveContent.GetType() == typeof(EditorPanel))
                ((EditorPanel)dockPanel1.ActiveContent).BlockComment();
            else if (lastEditor != null)
            {
                lastEditor.Activate();
                lastEditor.BlockComment();
            }
        }

        private void mbtnUncomment_Click(object sender, EventArgs e)
        {
            if (dockPanel1.ActiveContent != null && dockPanel1.ActiveContent.GetType() == typeof(EditorPanel))
                ((EditorPanel)dockPanel1.ActiveContent).BlockUncomment();
            else if (lastEditor != null)
            {
                lastEditor.Activate();
                lastEditor.BlockUncomment();
            }
        }

        private void tbtnBlockUncomment_Click(object sender, EventArgs e)
        {
            if (dockPanel1.ActiveContent != null && dockPanel1.ActiveContent.GetType() == typeof(EditorPanel))
                ((EditorPanel)dockPanel1.ActiveContent).BlockUncomment();
            else if (lastEditor != null)
            {
                lastEditor.Activate();
                lastEditor.BlockUncomment();
            }
        }

        private void tbtnBlockTab_Click(object sender, EventArgs e)
        {
            if (dockPanel1.ActiveContent != null && dockPanel1.ActiveContent.GetType() == typeof(EditorPanel))
                ((EditorPanel)dockPanel1.ActiveContent).BlockIndent();
            else if (lastEditor != null)
            {
                lastEditor.Activate();
                lastEditor.BlockIndent();
            }
        }

        private void mbtnIndent_Click(object sender, EventArgs e)
        {
            if (dockPanel1.ActiveContent != null && dockPanel1.ActiveContent.GetType() == typeof(EditorPanel))
                ((EditorPanel)dockPanel1.ActiveContent).BlockIndent();
            else if (lastEditor != null)
            {
                lastEditor.Activate();
                lastEditor.BlockIndent();
            }
        }

        private void mbtnUnindent_Click(object sender, EventArgs e)
        {
            if (dockPanel1.ActiveContent != null && dockPanel1.ActiveContent.GetType() == typeof(EditorPanel))
                ((EditorPanel)dockPanel1.ActiveContent).BlockUnindent();
            else if (lastEditor != null)
            {
                lastEditor.Activate();
                lastEditor.BlockUnindent();
            }
        }

        private void tbtnBlockUntab_Click(object sender, EventArgs e)
        {
            if (dockPanel1.ActiveContent != null && dockPanel1.ActiveContent.GetType() == typeof(EditorPanel))
                ((EditorPanel)dockPanel1.ActiveContent).BlockUnindent();
            else if (lastEditor != null)
            {
                lastEditor.Activate();
                lastEditor.BlockUnindent();
            }
        }

        private void mbtnClearHighlights_Click(object sender, EventArgs e)
        {
            if (dockPanel1.ActiveContent != null && dockPanel1.ActiveContent.GetType() == typeof(EditorPanel))
            {
                ((EditorPanel)dockPanel1.ActiveContent).ClearHighlights();
            }
            else if (lastEditor != null)
            {
                lastEditor.Activate();
                lastEditor.ClearHighlights();
            }
        }

        #endregion

        #region Save / Open Button Functions

        private void mbtnSaveAllFiles_Click(object sender, EventArgs e)
        {
            SaveProj();
            SaveAll();
        }

        private void mbtnSaveFileAs_Click(object sender, EventArgs e)
        {
            if (dockPanel1.ActiveContent != null && dockPanel1.ActiveContent.GetType() == typeof(EditorPanel))
                ((EditorPanel)dockPanel1.ActiveContent).SaveAs();
            else if (lastEditor != null)
            {
                lastEditor.Activate();
                lastEditor.SaveAs();
            }
        }

        private void mbtnSaveConfigAs_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "AVR Project (*.avrproj)|*.avrproj";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                if (project.Save(sfd.FileName) == false)
                {
                    MessageBox.Show("Error While Saving Project Configuration File");
                }
            }
        }

        private void mbtnOpenProject_Click(object sender, EventArgs e)
        {
            if (SaveProj() == false)
            {
                return;
            }

            if (HasChanged)
            {
                DialogResult res = MessageBox.Show("You have unsaved changes. Do you want to save?", "Unsaved Project", MessageBoxButtons.YesNoCancel);
                if (res == DialogResult.Yes)
                {
                    if (SaveAll() == false)
                    {
                        return;
                    }
                }
                else if (res == DialogResult.Cancel)
                {
                    return;
                }
            }

            AVRProject newProj = new AVRProject();

            WelcomeWindow newWelcome = new WelcomeWindow(newProj);
            newWelcome.ShowDialog();

            if (newProj.IsReady) // IsReady == false means that the user closed the welcome window without opening a project
            {
                HandleNewOpenProj(newProj);
            }
        }

        private void HandleNewOpenProj(AVRProject newProj)
        {
            //LoadWaitWindow lww = new LoadWaitWindow();
            //lww.Show();

            EnableButtons();

            // close all editors
            List<EditorPanel> toClose = new List<EditorPanel>(editorList.Values);
            foreach (EditorPanel i in toClose)
            {
                i.Close(false);
            }


            this.project = newProj; // reassign project
            curProj = this.project;

            projBuilder = new ProjectBuilder(project, messageWin.MyTextBox, messageWin.MyListView, messageWin.MyErrorOnlyListView);
            projBuilder.DoneWork += new ProjectBuilder.EventHandler(projBuilder_DoneWork);
            projBurner = new ProjectBurner(project);

            // set title
            this.Text = project.FileNameNoExt + " - AVR Project IDE";

            editorList.Clear();

            searchWin.Clear();

            FillRecentProjects();

            //lww.Close();

            if (project.HasBeenConfigged == false)
            {
                ConfigWindow wnd = new ConfigWindow(project);
                wnd.ShowDialog();
            }

            hardwareExplorerWin.LoadDataForChip(project.Device);
            hardwareExplorerWin.ClockFreq = project.ClockFreq;

            fileTreeWin.PopulateList(newProj, editorList);

            ReloadLastOpened();

            KeywordScanner.LaunchScan(project, editorList);
        }

        private void FillRecentProjects()
        {
            mbtnRecentProjects.DropDownItems.Clear();

            List<ToolStripMenuItem> items = new List<ToolStripMenuItem>();

            // make a new menuItem for every filepath in the list, and attach the event
            foreach (string i in SettingsManagement.RecentFileList)
            {
                ToolStripMenuItem recentMenuItem = new ToolStripMenuItem(i.Substring(i.LastIndexOf('\\') + 1));
                recentMenuItem.Click += new EventHandler(recentMenuItem_Click);
                recentMenuItem.ToolTipText = i;
                items.Add(recentMenuItem);
            }

            // honestly, I forgot why I did this with 2 foreach loops
            foreach (ToolStripMenuItem i in items)
            {
                mbtnRecentProjects.DropDownItems.Add(i);
            }
        }

        private void recentMenuItem_Click(object sender, EventArgs e)
        {
            if (SaveProj() == false)
            {
                return;
            }

            ToolStripMenuItem item = (ToolStripMenuItem)sender;

            // like all good editors, ask to save first
            if (HasChanged)
            {
                DialogResult res = MessageBox.Show("You have unsaved changes. Do you want to save?", "Unsaved Project", MessageBoxButtons.YesNoCancel);
                if (res == DialogResult.Yes)
                {
                    if (SaveAll() == false)
                    {
                        return;
                    }
                }
                else if (res == DialogResult.Cancel)
                {
                    return;
                }
            }

            AVRProject newProj = new AVRProject();

            // find the actual file path by matching the menuItem's text with the list of file paths
            string recentPath = "";
            foreach (string i in SettingsManagement.RecentFileList)
            {
                if (i.EndsWith(item.Text))
                {
                    recentPath = i; // found it
                    break;
                }
            }

            if (newProj.Open(recentPath) == false)
            {
                MessageBox.Show("Error Opening Recent Project");
            }
            else if (newProj.IsReady)
            {
                HandleNewOpenProj(newProj);
            }
        }

        private void tbtnSaveOrig_Click(object sender, EventArgs e)
        {
            if (dockPanel1.ActiveContent != null && dockPanel1.ActiveContent.GetType() == typeof(EditorPanel))
                ((EditorPanel)dockPanel1.ActiveContent).Save();
            else if (lastEditor != null)
            {
                lastEditor.Activate();
                lastEditor.Save();
            }
        }

        private void mbtnSaveCurFile_Click(object sender, EventArgs e)
        {
            if (dockPanel1.ActiveContent != null && dockPanel1.ActiveContent.GetType() == typeof(EditorPanel))
                ((EditorPanel)dockPanel1.ActiveContent).Save();
            else if (lastEditor != null)
            {
                lastEditor.Activate();
                lastEditor.Save();
            }
        }

        private void tbtnSaveAll_Click(object sender, EventArgs e)
        {
            SaveProj();
            SaveAll();
        }

        #endregion        

        #region Basic Editing Button Functions

        private void mbtnUndo_Click(object sender, EventArgs e)
        {
            if (dockPanel1.ActiveContent != null && dockPanel1.ActiveContent.GetType() == typeof(EditorPanel))
                ((EditorPanel)dockPanel1.ActiveContent).Undo();
            else if (lastEditor != null)
            {
                lastEditor.Activate();
                lastEditor.Undo();
            }
        }

        private void mbtnRedo_Click(object sender, EventArgs e)
        {
            if (dockPanel1.ActiveContent != null && dockPanel1.ActiveContent.GetType() == typeof(EditorPanel))
                ((EditorPanel)dockPanel1.ActiveContent).Redo();
            else if (lastEditor != null)
            {
                lastEditor.Activate();
                lastEditor.Redo();
            }
        }

        private void tbtnUndo_Click(object sender, EventArgs e)
        {
            if (dockPanel1.ActiveContent != null && dockPanel1.ActiveContent.GetType() == typeof(EditorPanel))
                ((EditorPanel)dockPanel1.ActiveContent).Undo();
            else if (lastEditor != null)
            {
                lastEditor.Activate();
                lastEditor.Undo();
            }
        }

        private void tbtnRedo_Click(object sender, EventArgs e)
        {
            if (dockPanel1.ActiveContent != null && dockPanel1.ActiveContent.GetType() == typeof(EditorPanel))
                ((EditorPanel)dockPanel1.ActiveContent).Redo();
            else if (lastEditor != null)
            {
                lastEditor.Activate();
                lastEditor.Redo();
            }
        }

        private void mbtnCut_Click(object sender, EventArgs e)
        {
            if (dockPanel1.ActiveContent != null && dockPanel1.ActiveContent.GetType() == typeof(EditorPanel))
                ((EditorPanel)dockPanel1.ActiveContent).Cut();
            else if (lastEditor != null)
            {
                lastEditor.Activate();
                lastEditor.Cut();
            }
        }

        private void tbtnCutOrig_Click(object sender, EventArgs e)
        {
            if (dockPanel1.ActiveContent != null && dockPanel1.ActiveContent.GetType() == typeof(EditorPanel))
                ((EditorPanel)dockPanel1.ActiveContent).Cut();
            else if (lastEditor != null)
            {
                lastEditor.Activate();
                lastEditor.Cut();
            }
        }

        private void tbtnCopyOrig_Click(object sender, EventArgs e)
        {
            if (dockPanel1.ActiveContent != null && dockPanel1.ActiveContent.GetType() == typeof(EditorPanel))
                ((EditorPanel)dockPanel1.ActiveContent).Copy();
            else if (lastEditor != null)
            {
                lastEditor.Activate();
                lastEditor.Copy();
            }
        }

        private void mbtnCopy_Click(object sender, EventArgs e)
        {
            if (dockPanel1.ActiveContent != null && dockPanel1.ActiveContent.GetType() == typeof(EditorPanel))
                ((EditorPanel)dockPanel1.ActiveContent).Copy();
            else if (lastEditor != null)
            {
                lastEditor.Activate();
                lastEditor.Copy();
            }
        }

        private void mbtnPaste_Click(object sender, EventArgs e)
        {
            if (dockPanel1.ActiveContent != null && dockPanel1.ActiveContent.GetType() == typeof(EditorPanel))
                ((EditorPanel)dockPanel1.ActiveContent).Paste();
            else if (lastEditor != null)
            {
                lastEditor.Activate();
                lastEditor.Paste();
            }
        }
        
        private void tbtnPasteOrig_Click(object sender, EventArgs e)
        {
            if (dockPanel1.ActiveContent != null && dockPanel1.ActiveContent.GetType() == typeof(EditorPanel))
                ((EditorPanel)dockPanel1.ActiveContent).Paste();
            else if (lastEditor != null)
            {
                lastEditor.Activate();
                lastEditor.Paste();
            }
        }

        private void mbtnSelectAll_Click(object sender, EventArgs e)
        {
            if (dockPanel1.ActiveContent != null && dockPanel1.ActiveContent.GetType() == typeof(EditorPanel))
                ((EditorPanel)dockPanel1.ActiveContent).SelectAll();
            else if (lastEditor != null)
            {
                lastEditor.Activate();
                lastEditor.SelectAll();
            }
        }

        #endregion

        #region Find and Replace Buttons

        private void mbtnFindReplace_Click(object sender, EventArgs e)
        {
            if (dockPanel1.ActiveContent != null && dockPanel1.ActiveContent.GetType() == typeof(DisassemblyViewer))
                ((DisassemblyViewer)dockPanel1.ActiveContent).FindWindow();
            else if (dockPanel1.ActiveContent != null && dockPanel1.ActiveContent.GetType() == typeof(EditorPanel))
                ((EditorPanel)dockPanel1.ActiveContent).FindWindow();
            else if (lastEditor != null)
            {
                lastEditor.Activate();
                lastEditor.FindWindow();
            }
        }
        
        private void tbtnFind_Click(object sender, EventArgs e)
        {
            if (dockPanel1.ActiveContent != null && dockPanel1.ActiveContent.GetType() == typeof(DisassemblyViewer))
                ((DisassemblyViewer)dockPanel1.ActiveContent).FindWindow();
            if (dockPanel1.ActiveContent != null && dockPanel1.ActiveContent.GetType() == typeof(EditorPanel))
                ((EditorPanel)dockPanel1.ActiveContent).FindWindow();
            else if (lastEditor != null)
            {
                lastEditor.Activate();
                lastEditor.FindWindow();
            }
        }

        private void mbtnFindNext_Click(object sender, EventArgs e)
        {
            if (dockPanel1.ActiveContent != null && dockPanel1.ActiveContent.GetType() == typeof(DisassemblyViewer))
                ((DisassemblyViewer)dockPanel1.ActiveContent).FindNext();
            if (dockPanel1.ActiveContent != null && dockPanel1.ActiveContent.GetType() == typeof(EditorPanel))
                ((EditorPanel)dockPanel1.ActiveContent).FindNext();
            else if (lastEditor != null)
            {
                lastEditor.Activate();
                lastEditor.FindNext();
            }
        }

        private void tbtnFindNext_Click(object sender, EventArgs e)
        {
            if (dockPanel1.ActiveContent != null && dockPanel1.ActiveContent.GetType() == typeof(DisassemblyViewer))
                ((DisassemblyViewer)dockPanel1.ActiveContent).FindNext();
            if (dockPanel1.ActiveContent != null && dockPanel1.ActiveContent.GetType() == typeof(EditorPanel))
                ((EditorPanel)dockPanel1.ActiveContent).FindNext();
            else if (lastEditor != null)
            {
                lastEditor.Activate();
                lastEditor.FindNext();
            }
        }

        private void mbtnSearchAll_Click(object sender, EventArgs e)
        {
            searchWin.Activate();
            searchWin.ShowSearch();
        }

        private void tbtnSearch_Click(object sender, EventArgs e)
        {
            searchWin.Activate();
            searchWin.ShowSearch();
        }

        #endregion

        #region Actions of Config, Compile, and Burn Buttons

        private void mbtnConfig_Click(object sender, EventArgs e)
        {
            if (project.IsReady == false)
                return;

            ConfigWindow wnd = new ConfigWindow(project);
            wnd.ShowDialog();

            if (project.ShouldReloadDevice)
                hardwareExplorerWin.LoadDataForChip(project.Device);

            if (project.ShouldReloadFiles)
                fileTreeWin.PopulateList(project, editorList);

            if (project.ShouldReloadClock)
                hardwareExplorerWin.ClockFreq = project.ClockFreq;

            project.ShouldReloadFiles = false;
            project.ShouldReloadDevice = false;
            project.ShouldReloadClock = false;
        }

        private void tbtnConfig_Click(object sender, EventArgs e)
        {
            mbtnConfig_Click(sender, e);
        }

        private void mbtnCompile_Click(object sender, EventArgs e)
        {
            if (project.IsReady == false)
                return;

            if (ProjectBuilder.CheckForWinAVR())
            {
                messageWin.BringToFront();
                messageWin.Activate();
                messageWin.SwitchToMessageBox();
                SaveAll();
                projBuilder.StartBuild();
            }
        }

        private void tbtnCompile_Click(object sender, EventArgs e)
        {
            if (project.IsReady == false)
                return;

            if (ProjectBuilder.CheckForWinAVR())
            {
                messageWin.BringToFront();
                messageWin.Activate();
                messageWin.SwitchToMessageBox();
                SaveAll();
                projBuilder.StartBuild();
            }
        }

        private void tbtnBurn_Click(object sender, EventArgs e)
        {
            if (project.IsReady == false)
                return;

            if (ProjectBuilder.CheckForWinAVR() == false)
                return;

            if (serialWin.IsConnected && serialWin.CurrentPort == project.BurnPort)
                serialWin.Disconnect();

            projBurner.BurnCMD(false, false, messageWin);

            if (serialWin.CurrentPort == project.BurnPort)
            {
                serialWin.Activate();
                serialWin.BringToFront();
            }
        }

        private void mbtnBurn_Click(object sender, EventArgs e)
        {
            if (project.IsReady == false)
                return;

            if (ProjectBuilder.CheckForWinAVR() == false)
                return;

            if (serialWin.IsConnected && serialWin.CurrentPort == project.BurnPort)
                serialWin.Disconnect();

            projBurner.BurnCMD(false, false, messageWin);

            if (serialWin.CurrentPort == project.BurnPort)
            {
                serialWin.Activate();
                serialWin.BringToFront();
            }
        }

        private void mbtnExportMakefile_Click(object sender, EventArgs e)
        {
            if (project.IsReady == false)
                return;

            if (Makefile.Generate(project))
            {
                MessageBox.Show("Makefile Generated in Your Project Directory");
            }
            else
            {
                MessageBox.Show("Error, failed to generate a makefile");
            }
        }

        private void mbtnExportAPS_Click(object sender, EventArgs e)
        {
            if (project.IsReady == false)
                return;

            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "AVRStudio Project (*.aps)|*.aps";
            sfd.InitialDirectory = Path.GetDirectoryName(project.FilePath);
            sfd.FileName = Path.GetDirectoryName(project.FilePath) + Path.DirectorySeparatorChar + project.FileNameNoExt + ".aps";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                if (project.ExportAPS(sfd.FileName) == false)
                {
                    MessageBox.Show("Error During Export");
                }
            }
        }

        private void mbtnRunMake_Click(object sender, EventArgs e)
        {
            if (project.IsReady)
            {
                if (ProjectBuilder.CheckForWinAVR())
                {
                    projBuilder.StartMake();
                }
            }
        }

        private void mbtnOpenCmdLine_Click(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process cmd = new System.Diagnostics.Process();
                cmd.StartInfo.FileName = "cmd";
                if (project.IsReady)
                    cmd.StartInfo.WorkingDirectory = project.DirPath;
                cmd.Start();
            }
            catch (Exception ex)
            {
                ErrorReportWindow.Show(ex, "Error while opening command line window.");
            }
        }

        private void mbtnCompileCurrent_Click(object sender, EventArgs e)
        {
            if (project.IsReady == false)
                return;

            ProjectFile file = null;

            if (dockPanel1.ActiveContent != null && dockPanel1.ActiveContent.GetType() == typeof(EditorPanel))
            {
                ((EditorPanel)dockPanel1.ActiveContent).Save();
                file = ((EditorPanel)dockPanel1.ActiveContent).File;
            }
            else if (lastEditor != null)
            {
                lastEditor.Save();
                file = lastEditor.File;
            }

            if (file != null)
            {
                if (file.FileExt == "c" || file.FileExt == "cpp" || file.FileExt == "s")
                {
                    projBuilder.HasError = 0;

                    messageWin.MessageBoxModify(TextBoxChangeMode.Set, "");
                    messageWin.ClearErrors();

                    if (projBuilder.Compile(file))
                    {
                        messageWin.MessageBoxModify(TextBoxChangeMode.PrependNewLine, "Compilation of " + file.FileName + " Successful");
                        messageWin.AddErrorWarning(file.FileName, -1, "", "", "Compilation Successful");
                    }
                    else
                    {
                        messageWin.MessageBoxModify(TextBoxChangeMode.PrependNewLine, "Compilation of " + file.FileName + " Failed");
                        messageWin.AddErrorWarning(file.FileName, -1, "", "", "Compilation Failed");

                        if (projBuilder.HasError > 0)
                            messageWin.SwitchToListView();
                        else
                            messageWin.SwitchToMessageBox();
                    }

                    messageWin.MyTextBox.SelectionStart = 0;
                    messageWin.MyTextBox.SelectionLength = 0;
                    messageWin.MyTextBox.ScrollToCaret();

                    messageWin.BringToFront();
                    messageWin.Activate();
                }
                else
                    MessageBox.Show("You can only compile C or C++ or S files");
            }
        }

        #endregion

        private void mbtnHelpTopics_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(Properties.Resources.HelpURL);
        }

        /// <summary>
        /// ignore me, the progress bar is only there to do some testing
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void progressBar1_Click(object sender, EventArgs e)
        {
            
        }

        private void timerScanner_Tick(object sender, EventArgs e)
        {
            try
            {
                KeywordScanner.DoMoreWork();
            }
            catch { }
        }

        private void IDEWindow_Load(object sender, EventArgs e)
        {
            curOpenWind = this;

            // fill external tools
            try
            {
                this.toolsToolStripMenuItem.DropDownItems.Add(ExternalTool.GetExternalToolsRoot(this));
            }
            catch (Exception ex)
            {
                messageWin.MessageBoxModify(TextBoxChangeMode.AppendNewLine, "Error Loading External Tools:, " + ex.Message);
            }

            SettingsManagement.LoadWindowState(this);

            Program.SplashScreen.BringToFront();
            Program.SplashScreen.Activate();

            bool useSavedDockSettings = false;
            // if workspace setting exists, load it
            string workspaceName = "workspace_version_" + Properties.Resources.PanelWorkspaceVersion + ".xml";
            if (File.Exists(SettingsManagement.AppDataPath + workspaceName))
            {
                DeserializeDockContent deserDockCont = new DeserializeDockContent(GetPanelFromPersistString);
                try
                {
                    dockPanel1.LoadFromXml(SettingsManagement.AppDataPath + workspaceName, deserDockCont);
                    useSavedDockSettings = true;
                }
                catch { }
            }

            if (useSavedDockSettings)
            {
                fileTreeWin.Show(dockPanel1);
                messageWin.Show(dockPanel1);
                searchWin.Show(dockPanel1);
                serialWin.Show(dockPanel1);
                usbWin.Show(dockPanel1);
                disassemViewer.Show(dockPanel1);
                hardwareExplorerWin.Show(dockPanel1);
                #if DEBUGGERWINTEST
                debuggerWin.Show(dockPanel1);
                #endif
            }
            else
            {
                // if an xml was not loaded, do the default config
                fileTreeWin.Show(dockPanel1, DockState.DockLeft);
                messageWin.Show(dockPanel1, DockState.DockBottom);
                searchWin.Show(dockPanel1, DockState.DockBottom);
                serialWin.Show(dockPanel1, DockState.DockBottom);
                usbWin.Show(dockPanel1, DockState.DockBottom);
                disassemViewer.Show(dockPanel1, DockState.DockBottom);
                hardwareExplorerWin.Show(dockPanel1, DockState.DockRightAutoHide);
                #if DEBUGGERWINTEST
                debuggerWin.Show(dockPanel1, DockState.DockRightAutoHide);
                #endif
            }

            Program.SplashScreen.Close();

            searchWin.Activate();

            if (project.IsReady == false && SettingsManagement.WelcomeWindowAtStart)
            {
                WelcomeWindow ww = new WelcomeWindow(project);
                ww.ShowDialog();
            }

            if (project.IsReady)
            {
                HandleNewOpenProj(project);
            }
        }

        private void mbtnEditorSettings_Click(object sender, EventArgs e)
        {
            SettingsWindow w = new SettingsWindow();
            w.ShowDialog();
            timerBackup.Interval = SettingsManagement.BackupInterval * 1000;
            List<EditorPanel> tmpList = new List<EditorPanel>(editorList.Values);
            foreach (EditorPanel i in tmpList)
            {
                ProjectFile file = i.File;
                if (file.FileExt == "c" || file.FileExt == "cpp" || file.FileExt == "h" || file.FileExt == "hpp" || file.FileExt == "pde")
                    SettingsManagement.SetScintSettings(i.Scint, false, false);
                else if (file.FileExt == "s" || file.FileExt == "asm")
                    SettingsManagement.SetScintSettings(i.Scint, true, true);
                else
                    SettingsManagement.SetScintSettings(i.Scint, true, true);
            }
        }

        private void timerBackup_Tick(object sender, EventArgs e)
        {
            try
            {
                List<EditorPanel> tmpList = new List<EditorPanel>(editorList.Values);
                foreach (EditorPanel i in tmpList)
                {
                    i.SaveBackup();
                }
            }
            catch { }
        }

        private void mbtnFuseTool_Click(object sender, EventArgs e)
        {
            if (project.IsReady)
                new FuseCalculator(project).Show();
        }

        private void mbtnAVRDUDE_Click(object sender, EventArgs e)
        {
            if (project.IsReady)
            {
                AvrDudeWindow adw = new AvrDudeWindow(project);
                adw.Show();
            }
        }

        private void mbtnGoto_Click(object sender, EventArgs e)
        {
            if (project.IsReady == false)
                return;

            if (lastEditor == null)
                return;

            GotoDialog gd = new GotoDialog(lastEditor.Scint.Lines.Current.Number + 1, lastEditor.Scint.Lines.Count);
            if (gd.ShowDialog() == DialogResult.OK)
                GotoEditor(lastEditor.FileName, gd.SelectedLineNum);
        }

        private void DisableButtons()
        {
            editToolStripMenuItem.Enabled = false;

            mbtnSaveAllFiles.Enabled = false;
            mbtnSaveConfigAs.Enabled = false;
            mbtnSaveFileAs.Enabled = false;
            mbtnSaveCurFile.Enabled = false;
            mbtnCloneProject.Enabled = false;

            foreach (ToolStripMenuItem i in toolsToolStripMenuItem.DropDown.Items)
            {
                i.Enabled = false;
            }

            mbtnEditorSettings.Enabled = true;

            foreach (ToolStripItem i in toolStripMain.Items)
            {
                i.Enabled = false;
            }

            tbtnNewOrig.Enabled = true;
            tbtnOpenOrig.Enabled = true;
        }

        private void EnableButtons()
        {
            editToolStripMenuItem.Enabled = true;

            mbtnSaveAllFiles.Enabled = true;
            mbtnSaveConfigAs.Enabled = true;
            mbtnSaveFileAs.Enabled = true;
            mbtnSaveCurFile.Enabled = true;
            mbtnCloneProject.Enabled = true;

            foreach (ToolStripMenuItem i in toolsToolStripMenuItem.DropDown.Items)
            {
                i.Enabled = true;
            }

            foreach (ToolStripItem i in toolStripMain.Items)
            {
                i.Enabled = true;
            }

            #if DEBUGGERWINTEST
            debuggerWin.EnableButtons();
            #endif
        }

        #region Bookmark Button Events

        private void tbtnBookmarkToggle_Click(object sender, EventArgs e)
        {
            if (dockPanel1.ActiveContent != null && dockPanel1.ActiveContent.GetType() == typeof(EditorPanel))
            {
                ((EditorPanel)dockPanel1.ActiveContent).ToggleBookmark();
            }
            else if (lastEditor != null)
            {
                lastEditor.Activate();
                lastEditor.ToggleBookmark();
            }
        }

        private void tbtnBookmarkDelete_Click(object sender, EventArgs e)
        {
            if (dockPanel1.ActiveContent != null && dockPanel1.ActiveContent.GetType() == typeof(EditorPanel))
            {
                ((EditorPanel)dockPanel1.ActiveContent).ClearBookmarks();
            }
            else if (lastEditor != null)
            {
                lastEditor.Activate();
                lastEditor.ClearBookmarks();
            }
        }

        private void tbtnBookmarkPrev_Click(object sender, EventArgs e)
        {
            ScintillaNet.Line line = null;
            if (dockPanel1.ActiveContent != null && dockPanel1.ActiveContent.GetType() == typeof(EditorPanel))
            {
                line = ((EditorPanel)dockPanel1.ActiveContent).Scint.Lines.Current.FindPreviousMarker(1);
            }
            else if (lastEditor != null)
            {
                lastEditor.Activate();
                line = lastEditor.Scint.Lines.Current.FindPreviousMarker(1);
            }

            if (line != null)
                line.Goto();
        }

        private void tbtnBookmarkNext_Click(object sender, EventArgs e)
        {
            ScintillaNet.Line line = null;
            if (dockPanel1.ActiveContent != null && dockPanel1.ActiveContent.GetType() == typeof(EditorPanel))
            {
                line = ((EditorPanel)dockPanel1.ActiveContent).Scint.Lines.Current.FindNextMarker(1);
            }
            else if (lastEditor != null)
            {
                lastEditor.Activate();
                line = lastEditor.Scint.Lines.Current.FindNextMarker(1);
            }

            if (line != null)
                line.Goto();
        }

        private void mbtnBookmarkToggle_Click(object sender, EventArgs e)
        {
            tbtnBookmarkToggle_Click(sender, e);
        }

        private void mbtnBookmarkDelete_Click(object sender, EventArgs e)
        {
            tbtnBookmarkDelete_Click(sender, e);
        }

        private void mbtnBookmarkPrev_Click(object sender, EventArgs e)
        {
            tbtnBookmarkPrev_Click(sender, e);
        }

        private void mbtnBookmarkNext_Click(object sender, EventArgs e)
        {
            tbtnBookmarkNext_Click(sender, e);
        }

        #endregion


        #region Debug Related Functions

        public void ReadyForDebug()
        {
            foreach (EditorPanel i in editorList.Values)
            {
                i.ReadOnly = true;
            }
        }

        public void EndDebug()
        {
            foreach (EditorPanel i in editorList.Values)
            {
                i.ReadOnly = false;
            }
        }

        #endregion


        private void mbtnDonate_Click(object sender, EventArgs e)
        {
            Program.LaunchDonate();
        }

        internal void BreakpointChanged(bool isAdding, string fileName, int lineNum)
        {
            #if DEBUGGERWINTEST
            debuggerWin.BreakpointChanged(isAdding, fileName, lineNum);
            #endif
        }

        private void mbtnCloneProject_Click(object sender, EventArgs e)
        {
            if (project.IsReady)
            {
                if (SaveAll())
                {
                    CopyCatWizard.Show(project);
                }
                else
                {
                    MessageBox.Show("'Save All' was not successful, could not clone project");
                }
            }
        }
    }
}
