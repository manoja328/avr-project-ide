namespace AVRProjectIDE
{
    partial class IDEWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                try
                {
                    components.Dispose();
                }
                catch { }
            }
            try
            {
                base.Dispose(disposing);
            }
            catch { }
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(IDEWindow));
            WeifenLuo.WinFormsUI.Docking.DockPanelSkin dockPanelSkin1 = new WeifenLuo.WinFormsUI.Docking.DockPanelSkin();
            WeifenLuo.WinFormsUI.Docking.AutoHideStripSkin autoHideStripSkin1 = new WeifenLuo.WinFormsUI.Docking.AutoHideStripSkin();
            WeifenLuo.WinFormsUI.Docking.DockPanelGradient dockPanelGradient1 = new WeifenLuo.WinFormsUI.Docking.DockPanelGradient();
            WeifenLuo.WinFormsUI.Docking.TabGradient tabGradient1 = new WeifenLuo.WinFormsUI.Docking.TabGradient();
            WeifenLuo.WinFormsUI.Docking.DockPaneStripSkin dockPaneStripSkin1 = new WeifenLuo.WinFormsUI.Docking.DockPaneStripSkin();
            WeifenLuo.WinFormsUI.Docking.DockPaneStripGradient dockPaneStripGradient1 = new WeifenLuo.WinFormsUI.Docking.DockPaneStripGradient();
            WeifenLuo.WinFormsUI.Docking.TabGradient tabGradient2 = new WeifenLuo.WinFormsUI.Docking.TabGradient();
            WeifenLuo.WinFormsUI.Docking.DockPanelGradient dockPanelGradient2 = new WeifenLuo.WinFormsUI.Docking.DockPanelGradient();
            WeifenLuo.WinFormsUI.Docking.TabGradient tabGradient3 = new WeifenLuo.WinFormsUI.Docking.TabGradient();
            WeifenLuo.WinFormsUI.Docking.DockPaneStripToolWindowGradient dockPaneStripToolWindowGradient1 = new WeifenLuo.WinFormsUI.Docking.DockPaneStripToolWindowGradient();
            WeifenLuo.WinFormsUI.Docking.TabGradient tabGradient4 = new WeifenLuo.WinFormsUI.Docking.TabGradient();
            WeifenLuo.WinFormsUI.Docking.TabGradient tabGradient5 = new WeifenLuo.WinFormsUI.Docking.TabGradient();
            WeifenLuo.WinFormsUI.Docking.DockPanelGradient dockPanelGradient3 = new WeifenLuo.WinFormsUI.Docking.DockPanelGradient();
            WeifenLuo.WinFormsUI.Docking.TabGradient tabGradient6 = new WeifenLuo.WinFormsUI.Docking.TabGradient();
            WeifenLuo.WinFormsUI.Docking.TabGradient tabGradient7 = new WeifenLuo.WinFormsUI.Docking.TabGradient();
            this.menuStripMain = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mbtnOpenProject = new System.Windows.Forms.ToolStripMenuItem();
            this.mbtnSaveCurFile = new System.Windows.Forms.ToolStripMenuItem();
            this.mbtnSaveFileAs = new System.Windows.Forms.ToolStripMenuItem();
            this.mbtnSaveAllFiles = new System.Windows.Forms.ToolStripMenuItem();
            this.mbtnSaveConfigAs = new System.Windows.Forms.ToolStripMenuItem();
            this.mbtnRecentProjects = new System.Windows.Forms.ToolStripMenuItem();
            this.mbtnExit = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mbtnUndo = new System.Windows.Forms.ToolStripMenuItem();
            this.mbtnRedo = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator16 = new System.Windows.Forms.ToolStripSeparator();
            this.mbtnSelectAll = new System.Windows.Forms.ToolStripMenuItem();
            this.mbtnCut = new System.Windows.Forms.ToolStripMenuItem();
            this.mbtnCopy = new System.Windows.Forms.ToolStripMenuItem();
            this.mbtnPaste = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator17 = new System.Windows.Forms.ToolStripSeparator();
            this.mbtnFindReplace = new System.Windows.Forms.ToolStripMenuItem();
            this.mbtnFindNext = new System.Windows.Forms.ToolStripMenuItem();
            this.mbtnSearchAll = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator15 = new System.Windows.Forms.ToolStripSeparator();
            this.mbtnBlockEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.mbtnIndent = new System.Windows.Forms.ToolStripMenuItem();
            this.mbtnUnindent = new System.Windows.Forms.ToolStripMenuItem();
            this.mbtnComment = new System.Windows.Forms.ToolStripMenuItem();
            this.mbtnUncomment = new System.Windows.Forms.ToolStripMenuItem();
            this.mbtnClearBookmarks = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mbtnConfig = new System.Windows.Forms.ToolStripMenuItem();
            this.mbtnCompile = new System.Windows.Forms.ToolStripMenuItem();
            this.mbtnCompileCurrent = new System.Windows.Forms.ToolStripMenuItem();
            this.mbtnBurn = new System.Windows.Forms.ToolStripMenuItem();
            this.mbtnAVRDUDE = new System.Windows.Forms.ToolStripMenuItem();
            this.mbtnFuseTool = new System.Windows.Forms.ToolStripMenuItem();
            this.mbtnExportMakefile = new System.Windows.Forms.ToolStripMenuItem();
            this.mbtnExportAPS = new System.Windows.Forms.ToolStripMenuItem();
            this.mbtnRunMake = new System.Windows.Forms.ToolStripMenuItem();
            this.mbtnEditorSettings = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mbtnHelpTopics = new System.Windows.Forms.ToolStripMenuItem();
            this.mbtnAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMain = new System.Windows.Forms.ToolStrip();
            this.tbtnNewOrig = new System.Windows.Forms.ToolStripButton();
            this.tbtnOpenOrig = new System.Windows.Forms.ToolStripButton();
            this.tbtnSaveOrig = new System.Windows.Forms.ToolStripButton();
            this.tbtnSaveAll = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator10 = new System.Windows.Forms.ToolStripSeparator();
            this.tbtnUndo = new System.Windows.Forms.ToolStripButton();
            this.tbtnRedo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator11 = new System.Windows.Forms.ToolStripSeparator();
            this.tbtnCutOrig = new System.Windows.Forms.ToolStripButton();
            this.tbtnCopyOrig = new System.Windows.Forms.ToolStripButton();
            this.tbtnPasteOrig = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator12 = new System.Windows.Forms.ToolStripSeparator();
            this.tbtnFind = new System.Windows.Forms.ToolStripButton();
            this.tbtnFindNext = new System.Windows.Forms.ToolStripButton();
            this.tbtnSearch = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator13 = new System.Windows.Forms.ToolStripSeparator();
            this.tbtnBlockTab = new System.Windows.Forms.ToolStripButton();
            this.tbtnBlockUntab = new System.Windows.Forms.ToolStripButton();
            this.tbtnBlockComment = new System.Windows.Forms.ToolStripButton();
            this.tbtnBlockUncomment = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator14 = new System.Windows.Forms.ToolStripSeparator();
            this.tbtnConfig = new System.Windows.Forms.ToolStripButton();
            this.tbtnCompile = new System.Windows.Forms.ToolStripButton();
            this.tbtnBurn = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator9 = new System.Windows.Forms.ToolStripSeparator();
            this.imgListFileTree = new System.Windows.Forms.ImageList(this.components);
            this.imgListTabs = new System.Windows.Forms.ImageList(this.components);
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparatorReuse = new System.Windows.Forms.ToolStripSeparator();
            this.dockPanel1 = new WeifenLuo.WinFormsUI.Docking.DockPanel();
            this.BottomToolStripPanel = new System.Windows.Forms.ToolStripPanel();
            this.TopToolStripPanel = new System.Windows.Forms.ToolStripPanel();
            this.RightToolStripPanel = new System.Windows.Forms.ToolStripPanel();
            this.LeftToolStripPanel = new System.Windows.Forms.ToolStripPanel();
            this.ContentPanel = new System.Windows.Forms.ToolStripContentPanel();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.timerScanner = new System.Windows.Forms.Timer(this.components);
            this.timerBackup = new System.Windows.Forms.Timer(this.components);
            this.mbtnGoto = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStripMain.SuspendLayout();
            this.toolStripMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStripMain
            // 
            this.menuStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.editToolStripMenuItem,
            this.toolsToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStripMain.Location = new System.Drawing.Point(0, 0);
            this.menuStripMain.Name = "menuStripMain";
            this.menuStripMain.Size = new System.Drawing.Size(994, 24);
            this.menuStripMain.TabIndex = 0;
            this.menuStripMain.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mbtnOpenProject,
            this.mbtnSaveCurFile,
            this.mbtnSaveFileAs,
            this.mbtnSaveAllFiles,
            this.mbtnSaveConfigAs,
            this.mbtnRecentProjects,
            this.mbtnExit});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // mbtnOpenProject
            // 
            this.mbtnOpenProject.Name = "mbtnOpenProject";
            this.mbtnOpenProject.Size = new System.Drawing.Size(181, 22);
            this.mbtnOpenProject.Text = "&Open / New Project";
            this.mbtnOpenProject.Click += new System.EventHandler(this.mbtnOpenProject_Click);
            // 
            // mbtnSaveCurFile
            // 
            this.mbtnSaveCurFile.Name = "mbtnSaveCurFile";
            this.mbtnSaveCurFile.Size = new System.Drawing.Size(181, 22);
            this.mbtnSaveCurFile.Text = "&Save File";
            this.mbtnSaveCurFile.Click += new System.EventHandler(this.mbtnSaveCurFile_Click);
            // 
            // mbtnSaveFileAs
            // 
            this.mbtnSaveFileAs.Name = "mbtnSaveFileAs";
            this.mbtnSaveFileAs.Size = new System.Drawing.Size(181, 22);
            this.mbtnSaveFileAs.Text = "Save File &As";
            this.mbtnSaveFileAs.Click += new System.EventHandler(this.mbtnSaveFileAs_Click);
            // 
            // mbtnSaveAllFiles
            // 
            this.mbtnSaveAllFiles.Name = "mbtnSaveAllFiles";
            this.mbtnSaveAllFiles.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.mbtnSaveAllFiles.Size = new System.Drawing.Size(181, 22);
            this.mbtnSaveAllFiles.Text = "Save All &Files";
            this.mbtnSaveAllFiles.Click += new System.EventHandler(this.mbtnSaveAllFiles_Click);
            // 
            // mbtnSaveConfigAs
            // 
            this.mbtnSaveConfigAs.Name = "mbtnSaveConfigAs";
            this.mbtnSaveConfigAs.Size = new System.Drawing.Size(181, 22);
            this.mbtnSaveConfigAs.Text = "Save &Config As";
            this.mbtnSaveConfigAs.Click += new System.EventHandler(this.mbtnSaveConfigAs_Click);
            // 
            // mbtnRecentProjects
            // 
            this.mbtnRecentProjects.Name = "mbtnRecentProjects";
            this.mbtnRecentProjects.Size = new System.Drawing.Size(181, 22);
            this.mbtnRecentProjects.Text = "&Recent Projects";
            // 
            // mbtnExit
            // 
            this.mbtnExit.Image = global::AVRProjectIDE.Properties.Resources.exit;
            this.mbtnExit.Name = "mbtnExit";
            this.mbtnExit.Size = new System.Drawing.Size(181, 22);
            this.mbtnExit.Text = "E&xit";
            this.mbtnExit.Click += new System.EventHandler(this.mbtnExit_Click);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mbtnUndo,
            this.mbtnRedo,
            this.toolStripSeparator16,
            this.mbtnSelectAll,
            this.mbtnCut,
            this.mbtnCopy,
            this.mbtnPaste,
            this.toolStripSeparator17,
            this.mbtnFindReplace,
            this.mbtnFindNext,
            this.mbtnSearchAll,
            this.mbtnGoto,
            this.toolStripSeparator15,
            this.mbtnBlockEdit,
            this.mbtnClearBookmarks});
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
            this.editToolStripMenuItem.Text = "&Edit";
            // 
            // mbtnUndo
            // 
            this.mbtnUndo.Image = ((System.Drawing.Image)(resources.GetObject("mbtnUndo.Image")));
            this.mbtnUndo.Name = "mbtnUndo";
            this.mbtnUndo.ShortcutKeyDisplayString = "Ctrl-Z";
            this.mbtnUndo.Size = new System.Drawing.Size(204, 22);
            this.mbtnUndo.Text = "&Undo";
            this.mbtnUndo.Click += new System.EventHandler(this.mbtnUndo_Click);
            // 
            // mbtnRedo
            // 
            this.mbtnRedo.Image = ((System.Drawing.Image)(resources.GetObject("mbtnRedo.Image")));
            this.mbtnRedo.Name = "mbtnRedo";
            this.mbtnRedo.ShortcutKeyDisplayString = "Ctrl-Y";
            this.mbtnRedo.Size = new System.Drawing.Size(204, 22);
            this.mbtnRedo.Text = "&Redo";
            this.mbtnRedo.Click += new System.EventHandler(this.mbtnRedo_Click);
            // 
            // toolStripSeparator16
            // 
            this.toolStripSeparator16.Name = "toolStripSeparator16";
            this.toolStripSeparator16.Size = new System.Drawing.Size(201, 6);
            // 
            // mbtnSelectAll
            // 
            this.mbtnSelectAll.Name = "mbtnSelectAll";
            this.mbtnSelectAll.ShortcutKeyDisplayString = "Ctrl-A";
            this.mbtnSelectAll.Size = new System.Drawing.Size(204, 22);
            this.mbtnSelectAll.Text = "Select &All";
            this.mbtnSelectAll.Click += new System.EventHandler(this.mbtnSelectAll_Click);
            // 
            // mbtnCut
            // 
            this.mbtnCut.Image = global::AVRProjectIDE.Properties.Resources.cut;
            this.mbtnCut.Name = "mbtnCut";
            this.mbtnCut.ShortcutKeyDisplayString = "Ctrl-X";
            this.mbtnCut.Size = new System.Drawing.Size(204, 22);
            this.mbtnCut.Text = "Cu&t";
            this.mbtnCut.Click += new System.EventHandler(this.mbtnCut_Click);
            // 
            // mbtnCopy
            // 
            this.mbtnCopy.Image = global::AVRProjectIDE.Properties.Resources.copy;
            this.mbtnCopy.Name = "mbtnCopy";
            this.mbtnCopy.ShortcutKeyDisplayString = "Ctrl-C";
            this.mbtnCopy.Size = new System.Drawing.Size(204, 22);
            this.mbtnCopy.Text = "&Copy";
            this.mbtnCopy.Click += new System.EventHandler(this.mbtnCopy_Click);
            // 
            // mbtnPaste
            // 
            this.mbtnPaste.Image = global::AVRProjectIDE.Properties.Resources.paste;
            this.mbtnPaste.Name = "mbtnPaste";
            this.mbtnPaste.ShortcutKeyDisplayString = "Ctrl-C";
            this.mbtnPaste.Size = new System.Drawing.Size(204, 22);
            this.mbtnPaste.Text = "&Paste";
            this.mbtnPaste.Click += new System.EventHandler(this.mbtnPaste_Click);
            // 
            // toolStripSeparator17
            // 
            this.toolStripSeparator17.Name = "toolStripSeparator17";
            this.toolStripSeparator17.Size = new System.Drawing.Size(201, 6);
            // 
            // mbtnFindReplace
            // 
            this.mbtnFindReplace.Image = global::AVRProjectIDE.Properties.Resources.find;
            this.mbtnFindReplace.Name = "mbtnFindReplace";
            this.mbtnFindReplace.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F)));
            this.mbtnFindReplace.Size = new System.Drawing.Size(204, 22);
            this.mbtnFindReplace.Text = "&Find and Replace";
            this.mbtnFindReplace.Click += new System.EventHandler(this.mbtnFindReplace_Click);
            // 
            // mbtnFindNext
            // 
            this.mbtnFindNext.Image = global::AVRProjectIDE.Properties.Resources.findnext;
            this.mbtnFindNext.Name = "mbtnFindNext";
            this.mbtnFindNext.ShortcutKeys = System.Windows.Forms.Keys.F3;
            this.mbtnFindNext.Size = new System.Drawing.Size(204, 22);
            this.mbtnFindNext.Text = "Find &Next";
            this.mbtnFindNext.Click += new System.EventHandler(this.mbtnFindNext_Click);
            // 
            // mbtnSearchAll
            // 
            this.mbtnSearchAll.Image = global::AVRProjectIDE.Properties.Resources.searchall;
            this.mbtnSearchAll.Name = "mbtnSearchAll";
            this.mbtnSearchAll.Size = new System.Drawing.Size(204, 22);
            this.mbtnSearchAll.Text = "&Search in Project";
            this.mbtnSearchAll.Click += new System.EventHandler(this.mbtnSearchAll_Click);
            // 
            // toolStripSeparator15
            // 
            this.toolStripSeparator15.Name = "toolStripSeparator15";
            this.toolStripSeparator15.Size = new System.Drawing.Size(201, 6);
            // 
            // mbtnBlockEdit
            // 
            this.mbtnBlockEdit.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mbtnIndent,
            this.mbtnUnindent,
            this.mbtnComment,
            this.mbtnUncomment});
            this.mbtnBlockEdit.Name = "mbtnBlockEdit";
            this.mbtnBlockEdit.Size = new System.Drawing.Size(204, 22);
            this.mbtnBlockEdit.Text = "&Block Edit";
            // 
            // mbtnIndent
            // 
            this.mbtnIndent.Image = global::AVRProjectIDE.Properties.Resources.indent;
            this.mbtnIndent.Name = "mbtnIndent";
            this.mbtnIndent.Size = new System.Drawing.Size(141, 22);
            this.mbtnIndent.Text = "&Indent";
            this.mbtnIndent.Click += new System.EventHandler(this.mbtnIndent_Click);
            // 
            // mbtnUnindent
            // 
            this.mbtnUnindent.Image = global::AVRProjectIDE.Properties.Resources.unindent;
            this.mbtnUnindent.Name = "mbtnUnindent";
            this.mbtnUnindent.Size = new System.Drawing.Size(141, 22);
            this.mbtnUnindent.Text = "U&nindent";
            this.mbtnUnindent.Click += new System.EventHandler(this.mbtnUnindent_Click);
            // 
            // mbtnComment
            // 
            this.mbtnComment.Image = global::AVRProjectIDE.Properties.Resources.comment;
            this.mbtnComment.Name = "mbtnComment";
            this.mbtnComment.Size = new System.Drawing.Size(141, 22);
            this.mbtnComment.Text = "&Comment";
            this.mbtnComment.Click += new System.EventHandler(this.mbtnComment_Click);
            // 
            // mbtnUncomment
            // 
            this.mbtnUncomment.Image = global::AVRProjectIDE.Properties.Resources.uncomment;
            this.mbtnUncomment.Name = "mbtnUncomment";
            this.mbtnUncomment.Size = new System.Drawing.Size(141, 22);
            this.mbtnUncomment.Text = "&Uncomment";
            this.mbtnUncomment.Click += new System.EventHandler(this.mbtnUncomment_Click);
            // 
            // mbtnClearBookmarks
            // 
            this.mbtnClearBookmarks.Name = "mbtnClearBookmarks";
            this.mbtnClearBookmarks.Size = new System.Drawing.Size(204, 22);
            this.mbtnClearBookmarks.Text = "Clear Bookmarks";
            this.mbtnClearBookmarks.Click += new System.EventHandler(this.mbtnClearBookmarks_Click);
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mbtnConfig,
            this.mbtnCompile,
            this.mbtnCompileCurrent,
            this.mbtnBurn,
            this.mbtnAVRDUDE,
            this.mbtnFuseTool,
            this.mbtnExportMakefile,
            this.mbtnExportAPS,
            this.mbtnRunMake,
            this.mbtnEditorSettings});
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.toolsToolStripMenuItem.Text = "&Tools";
            // 
            // mbtnConfig
            // 
            this.mbtnConfig.Image = global::AVRProjectIDE.Properties.Resources.configure;
            this.mbtnConfig.Name = "mbtnConfig";
            this.mbtnConfig.Size = new System.Drawing.Size(190, 22);
            this.mbtnConfig.Text = "Con&figure Project";
            this.mbtnConfig.Click += new System.EventHandler(this.mbtnConfig_Click);
            // 
            // mbtnCompile
            // 
            this.mbtnCompile.Image = global::AVRProjectIDE.Properties.Resources.build;
            this.mbtnCompile.Name = "mbtnCompile";
            this.mbtnCompile.ShortcutKeys = System.Windows.Forms.Keys.F7;
            this.mbtnCompile.Size = new System.Drawing.Size(190, 22);
            this.mbtnCompile.Text = "&Compile All";
            this.mbtnCompile.Click += new System.EventHandler(this.mbtnCompile_Click);
            // 
            // mbtnCompileCurrent
            // 
            this.mbtnCompileCurrent.Name = "mbtnCompileCurrent";
            this.mbtnCompileCurrent.Size = new System.Drawing.Size(190, 22);
            this.mbtnCompileCurrent.Text = "Compile Current File";
            this.mbtnCompileCurrent.Click += new System.EventHandler(this.mbtnCompileCurrent_Click);
            // 
            // mbtnBurn
            // 
            this.mbtnBurn.Image = global::AVRProjectIDE.Properties.Resources.burn;
            this.mbtnBurn.Name = "mbtnBurn";
            this.mbtnBurn.ShortcutKeys = System.Windows.Forms.Keys.F8;
            this.mbtnBurn.Size = new System.Drawing.Size(190, 22);
            this.mbtnBurn.Text = "&Program Chip";
            this.mbtnBurn.Click += new System.EventHandler(this.mbtnBurn_Click);
            // 
            // mbtnAVRDUDE
            // 
            this.mbtnAVRDUDE.Name = "mbtnAVRDUDE";
            this.mbtnAVRDUDE.Size = new System.Drawing.Size(190, 22);
            this.mbtnAVRDUDE.Text = "AVRDUDE Tool";
            this.mbtnAVRDUDE.Click += new System.EventHandler(this.mbtnAVRDUDE_Click);
            // 
            // mbtnFuseTool
            // 
            this.mbtnFuseTool.Name = "mbtnFuseTool";
            this.mbtnFuseTool.Size = new System.Drawing.Size(190, 22);
            this.mbtnFuseTool.Text = "Fuse Tool";
            this.mbtnFuseTool.Click += new System.EventHandler(this.mbtnFuseTool_Click);
            // 
            // mbtnExportMakefile
            // 
            this.mbtnExportMakefile.Name = "mbtnExportMakefile";
            this.mbtnExportMakefile.Size = new System.Drawing.Size(190, 22);
            this.mbtnExportMakefile.Text = "Export Makefile";
            this.mbtnExportMakefile.Click += new System.EventHandler(this.mbtnExportMakefile_Click);
            // 
            // mbtnExportAPS
            // 
            this.mbtnExportAPS.Name = "mbtnExportAPS";
            this.mbtnExportAPS.Size = new System.Drawing.Size(190, 22);
            this.mbtnExportAPS.Text = "Export AVRStudio .aps";
            this.mbtnExportAPS.Click += new System.EventHandler(this.mbtnExportAPS_Click);
            // 
            // mbtnRunMake
            // 
            this.mbtnRunMake.Name = "mbtnRunMake";
            this.mbtnRunMake.Size = new System.Drawing.Size(190, 22);
            this.mbtnRunMake.Text = "Run \"Make\"";
            this.mbtnRunMake.Click += new System.EventHandler(this.mbtnRunMake_Click);
            // 
            // mbtnEditorSettings
            // 
            this.mbtnEditorSettings.Name = "mbtnEditorSettings";
            this.mbtnEditorSettings.Size = new System.Drawing.Size(190, 22);
            this.mbtnEditorSettings.Text = "Editor Settings";
            this.mbtnEditorSettings.Click += new System.EventHandler(this.mbtnEditorSettings_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mbtnHelpTopics,
            this.mbtnAbout});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "&Help";
            // 
            // mbtnHelpTopics
            // 
            this.mbtnHelpTopics.Image = global::AVRProjectIDE.Properties.Resources.help;
            this.mbtnHelpTopics.Name = "mbtnHelpTopics";
            this.mbtnHelpTopics.Size = new System.Drawing.Size(137, 22);
            this.mbtnHelpTopics.Text = "&Help Topics";
            this.mbtnHelpTopics.Click += new System.EventHandler(this.mbtnHelpTopics_Click);
            // 
            // mbtnAbout
            // 
            this.mbtnAbout.Name = "mbtnAbout";
            this.mbtnAbout.Size = new System.Drawing.Size(137, 22);
            this.mbtnAbout.Text = "&About";
            this.mbtnAbout.Click += new System.EventHandler(this.mbtnAbout_Click);
            // 
            // toolStripMain
            // 
            this.toolStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tbtnNewOrig,
            this.tbtnOpenOrig,
            this.tbtnSaveOrig,
            this.tbtnSaveAll,
            this.toolStripSeparator10,
            this.tbtnUndo,
            this.tbtnRedo,
            this.toolStripSeparator11,
            this.tbtnCutOrig,
            this.tbtnCopyOrig,
            this.tbtnPasteOrig,
            this.toolStripSeparator12,
            this.tbtnFind,
            this.tbtnFindNext,
            this.tbtnSearch,
            this.toolStripSeparator13,
            this.tbtnBlockTab,
            this.tbtnBlockUntab,
            this.tbtnBlockComment,
            this.tbtnBlockUncomment,
            this.toolStripSeparator14,
            this.tbtnConfig,
            this.tbtnCompile,
            this.tbtnBurn,
            this.toolStripSeparator9});
            this.toolStripMain.Location = new System.Drawing.Point(0, 24);
            this.toolStripMain.Name = "toolStripMain";
            this.toolStripMain.Size = new System.Drawing.Size(994, 25);
            this.toolStripMain.Stretch = true;
            this.toolStripMain.TabIndex = 0;
            this.toolStripMain.Text = "toolStrip1";
            // 
            // tbtnNewOrig
            // 
            this.tbtnNewOrig.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tbtnNewOrig.Image = ((System.Drawing.Image)(resources.GetObject("tbtnNewOrig.Image")));
            this.tbtnNewOrig.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbtnNewOrig.Name = "tbtnNewOrig";
            this.tbtnNewOrig.Size = new System.Drawing.Size(23, 22);
            this.tbtnNewOrig.Text = "&New";
            this.tbtnNewOrig.ToolTipText = "Add New File";
            this.tbtnNewOrig.Click += new System.EventHandler(this.tbtnNewOrig_Click);
            // 
            // tbtnOpenOrig
            // 
            this.tbtnOpenOrig.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tbtnOpenOrig.Image = ((System.Drawing.Image)(resources.GetObject("tbtnOpenOrig.Image")));
            this.tbtnOpenOrig.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbtnOpenOrig.Name = "tbtnOpenOrig";
            this.tbtnOpenOrig.Size = new System.Drawing.Size(23, 22);
            this.tbtnOpenOrig.Text = "&Open";
            this.tbtnOpenOrig.ToolTipText = "Open Project";
            this.tbtnOpenOrig.Click += new System.EventHandler(this.tbtnOpenOrig_Click);
            // 
            // tbtnSaveOrig
            // 
            this.tbtnSaveOrig.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tbtnSaveOrig.Image = global::AVRProjectIDE.Properties.Resources.save;
            this.tbtnSaveOrig.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbtnSaveOrig.Name = "tbtnSaveOrig";
            this.tbtnSaveOrig.Size = new System.Drawing.Size(23, 22);
            this.tbtnSaveOrig.Text = "&Save";
            this.tbtnSaveOrig.Click += new System.EventHandler(this.tbtnSaveOrig_Click);
            // 
            // tbtnSaveAll
            // 
            this.tbtnSaveAll.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tbtnSaveAll.Image = global::AVRProjectIDE.Properties.Resources.saveall;
            this.tbtnSaveAll.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbtnSaveAll.Name = "tbtnSaveAll";
            this.tbtnSaveAll.Size = new System.Drawing.Size(23, 22);
            this.tbtnSaveAll.Text = "Save All";
            this.tbtnSaveAll.Click += new System.EventHandler(this.tbtnSaveAll_Click);
            // 
            // toolStripSeparator10
            // 
            this.toolStripSeparator10.Name = "toolStripSeparator10";
            this.toolStripSeparator10.Size = new System.Drawing.Size(6, 25);
            // 
            // tbtnUndo
            // 
            this.tbtnUndo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tbtnUndo.Image = ((System.Drawing.Image)(resources.GetObject("tbtnUndo.Image")));
            this.tbtnUndo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbtnUndo.Name = "tbtnUndo";
            this.tbtnUndo.Size = new System.Drawing.Size(23, 22);
            this.tbtnUndo.Text = "Undo";
            this.tbtnUndo.Click += new System.EventHandler(this.tbtnUndo_Click);
            // 
            // tbtnRedo
            // 
            this.tbtnRedo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tbtnRedo.Image = ((System.Drawing.Image)(resources.GetObject("tbtnRedo.Image")));
            this.tbtnRedo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbtnRedo.Name = "tbtnRedo";
            this.tbtnRedo.Size = new System.Drawing.Size(23, 22);
            this.tbtnRedo.Text = "Redo";
            this.tbtnRedo.Click += new System.EventHandler(this.tbtnRedo_Click);
            // 
            // toolStripSeparator11
            // 
            this.toolStripSeparator11.Name = "toolStripSeparator11";
            this.toolStripSeparator11.Size = new System.Drawing.Size(6, 25);
            // 
            // tbtnCutOrig
            // 
            this.tbtnCutOrig.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tbtnCutOrig.Image = global::AVRProjectIDE.Properties.Resources.cut;
            this.tbtnCutOrig.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbtnCutOrig.Name = "tbtnCutOrig";
            this.tbtnCutOrig.Size = new System.Drawing.Size(23, 22);
            this.tbtnCutOrig.Text = "C&ut";
            this.tbtnCutOrig.Click += new System.EventHandler(this.tbtnCutOrig_Click);
            // 
            // tbtnCopyOrig
            // 
            this.tbtnCopyOrig.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tbtnCopyOrig.Image = global::AVRProjectIDE.Properties.Resources.copy;
            this.tbtnCopyOrig.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbtnCopyOrig.Name = "tbtnCopyOrig";
            this.tbtnCopyOrig.Size = new System.Drawing.Size(23, 22);
            this.tbtnCopyOrig.Text = "&Copy";
            this.tbtnCopyOrig.Click += new System.EventHandler(this.tbtnCopyOrig_Click);
            // 
            // tbtnPasteOrig
            // 
            this.tbtnPasteOrig.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tbtnPasteOrig.Image = global::AVRProjectIDE.Properties.Resources.paste;
            this.tbtnPasteOrig.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbtnPasteOrig.Name = "tbtnPasteOrig";
            this.tbtnPasteOrig.Size = new System.Drawing.Size(23, 22);
            this.tbtnPasteOrig.Text = "&Paste";
            this.tbtnPasteOrig.Click += new System.EventHandler(this.tbtnPasteOrig_Click);
            // 
            // toolStripSeparator12
            // 
            this.toolStripSeparator12.Name = "toolStripSeparator12";
            this.toolStripSeparator12.Size = new System.Drawing.Size(6, 25);
            // 
            // tbtnFind
            // 
            this.tbtnFind.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tbtnFind.Image = ((System.Drawing.Image)(resources.GetObject("tbtnFind.Image")));
            this.tbtnFind.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbtnFind.Name = "tbtnFind";
            this.tbtnFind.Size = new System.Drawing.Size(23, 22);
            this.tbtnFind.Text = "Find and Replace";
            this.tbtnFind.Click += new System.EventHandler(this.tbtnFind_Click);
            // 
            // tbtnFindNext
            // 
            this.tbtnFindNext.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tbtnFindNext.Image = ((System.Drawing.Image)(resources.GetObject("tbtnFindNext.Image")));
            this.tbtnFindNext.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbtnFindNext.Name = "tbtnFindNext";
            this.tbtnFindNext.Size = new System.Drawing.Size(23, 22);
            this.tbtnFindNext.Text = "Find Next";
            this.tbtnFindNext.Click += new System.EventHandler(this.tbtnFindNext_Click);
            // 
            // tbtnSearch
            // 
            this.tbtnSearch.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tbtnSearch.Image = ((System.Drawing.Image)(resources.GetObject("tbtnSearch.Image")));
            this.tbtnSearch.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbtnSearch.Name = "tbtnSearch";
            this.tbtnSearch.Size = new System.Drawing.Size(23, 22);
            this.tbtnSearch.Text = "Search in All Files";
            this.tbtnSearch.Click += new System.EventHandler(this.tbtnSearch_Click);
            // 
            // toolStripSeparator13
            // 
            this.toolStripSeparator13.Name = "toolStripSeparator13";
            this.toolStripSeparator13.Size = new System.Drawing.Size(6, 25);
            // 
            // tbtnBlockTab
            // 
            this.tbtnBlockTab.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tbtnBlockTab.Image = ((System.Drawing.Image)(resources.GetObject("tbtnBlockTab.Image")));
            this.tbtnBlockTab.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbtnBlockTab.Name = "tbtnBlockTab";
            this.tbtnBlockTab.Size = new System.Drawing.Size(23, 22);
            this.tbtnBlockTab.Text = "Indent";
            this.tbtnBlockTab.ToolTipText = "Indent";
            this.tbtnBlockTab.Click += new System.EventHandler(this.tbtnBlockTab_Click);
            // 
            // tbtnBlockUntab
            // 
            this.tbtnBlockUntab.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tbtnBlockUntab.Image = ((System.Drawing.Image)(resources.GetObject("tbtnBlockUntab.Image")));
            this.tbtnBlockUntab.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbtnBlockUntab.Name = "tbtnBlockUntab";
            this.tbtnBlockUntab.Size = new System.Drawing.Size(23, 22);
            this.tbtnBlockUntab.Text = "Unindent";
            this.tbtnBlockUntab.ToolTipText = "Unindent";
            this.tbtnBlockUntab.Click += new System.EventHandler(this.tbtnBlockUntab_Click);
            // 
            // tbtnBlockComment
            // 
            this.tbtnBlockComment.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tbtnBlockComment.Image = ((System.Drawing.Image)(resources.GetObject("tbtnBlockComment.Image")));
            this.tbtnBlockComment.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbtnBlockComment.Name = "tbtnBlockComment";
            this.tbtnBlockComment.Size = new System.Drawing.Size(23, 22);
            this.tbtnBlockComment.Text = "Comment";
            this.tbtnBlockComment.ToolTipText = "Comment";
            this.tbtnBlockComment.Click += new System.EventHandler(this.tbtnBlockComment_Click);
            // 
            // tbtnBlockUncomment
            // 
            this.tbtnBlockUncomment.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tbtnBlockUncomment.Image = ((System.Drawing.Image)(resources.GetObject("tbtnBlockUncomment.Image")));
            this.tbtnBlockUncomment.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbtnBlockUncomment.Name = "tbtnBlockUncomment";
            this.tbtnBlockUncomment.Size = new System.Drawing.Size(23, 22);
            this.tbtnBlockUncomment.Text = "Uncomment";
            this.tbtnBlockUncomment.ToolTipText = "Uncomment";
            this.tbtnBlockUncomment.Click += new System.EventHandler(this.tbtnBlockUncomment_Click);
            // 
            // toolStripSeparator14
            // 
            this.toolStripSeparator14.Name = "toolStripSeparator14";
            this.toolStripSeparator14.Size = new System.Drawing.Size(6, 25);
            // 
            // tbtnConfig
            // 
            this.tbtnConfig.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tbtnConfig.Image = global::AVRProjectIDE.Properties.Resources.configure;
            this.tbtnConfig.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbtnConfig.Name = "tbtnConfig";
            this.tbtnConfig.Size = new System.Drawing.Size(23, 22);
            this.tbtnConfig.Text = "Configure Project";
            this.tbtnConfig.ToolTipText = "Configure Project";
            this.tbtnConfig.Click += new System.EventHandler(this.tbtnConfig_Click);
            // 
            // tbtnCompile
            // 
            this.tbtnCompile.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tbtnCompile.Image = global::AVRProjectIDE.Properties.Resources.build;
            this.tbtnCompile.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbtnCompile.Name = "tbtnCompile";
            this.tbtnCompile.Size = new System.Drawing.Size(23, 22);
            this.tbtnCompile.Text = "Compile";
            this.tbtnCompile.Click += new System.EventHandler(this.tbtnCompile_Click);
            // 
            // tbtnBurn
            // 
            this.tbtnBurn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tbtnBurn.Image = global::AVRProjectIDE.Properties.Resources.burn;
            this.tbtnBurn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbtnBurn.Name = "tbtnBurn";
            this.tbtnBurn.Size = new System.Drawing.Size(23, 22);
            this.tbtnBurn.Text = "Program";
            this.tbtnBurn.Click += new System.EventHandler(this.tbtnBurn_Click);
            // 
            // toolStripSeparator9
            // 
            this.toolStripSeparator9.Name = "toolStripSeparator9";
            this.toolStripSeparator9.Size = new System.Drawing.Size(6, 25);
            // 
            // imgListFileTree
            // 
            this.imgListFileTree.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgListFileTree.ImageStream")));
            this.imgListFileTree.TransparentColor = System.Drawing.Color.Transparent;
            this.imgListFileTree.Images.SetKeyName(0, "file.ico");
            this.imgListFileTree.Images.SetKeyName(1, "file2.ico");
            this.imgListFileTree.Images.SetKeyName(2, "nocompile.ico");
            this.imgListFileTree.Images.SetKeyName(3, "missing.ico");
            this.imgListFileTree.Images.SetKeyName(4, "treeroot.ico");
            this.imgListFileTree.Images.SetKeyName(5, "unknown2.ico");
            this.imgListFileTree.Images.SetKeyName(6, "folder.png");
            this.imgListFileTree.Images.SetKeyName(7, "folder2.png");
            // 
            // imgListTabs
            // 
            this.imgListTabs.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgListTabs.ImageStream")));
            this.imgListTabs.TransparentColor = System.Drawing.Color.Transparent;
            this.imgListTabs.Images.SetKeyName(0, "serial.ico");
            this.imgListTabs.Images.SetKeyName(1, "searchall.png");
            this.imgListTabs.Images.SetKeyName(2, "warning.ico");
            this.imgListTabs.Images.SetKeyName(3, "message.png");
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(201, 6);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(201, 6);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(201, 6);
            // 
            // toolStripSeparator
            // 
            this.toolStripSeparator.Name = "toolStripSeparator";
            this.toolStripSeparator.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripSeparator8
            // 
            this.toolStripSeparator8.Name = "toolStripSeparator8";
            this.toolStripSeparator8.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripSeparatorReuse
            // 
            this.toolStripSeparatorReuse.Name = "toolStripSeparatorReuse";
            this.toolStripSeparatorReuse.Size = new System.Drawing.Size(6, 25);
            // 
            // dockPanel1
            // 
            this.dockPanel1.ActiveAutoHideContent = null;
            this.dockPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dockPanel1.BackColor = System.Drawing.SystemColors.ControlDark;
            this.dockPanel1.DockBackColor = System.Drawing.SystemColors.ControlDark;
            this.dockPanel1.Location = new System.Drawing.Point(2, 52);
            this.dockPanel1.Name = "dockPanel1";
            this.dockPanel1.Size = new System.Drawing.Size(992, 458);
            dockPanelGradient1.EndColor = System.Drawing.SystemColors.ControlLight;
            dockPanelGradient1.StartColor = System.Drawing.SystemColors.ControlLight;
            autoHideStripSkin1.DockStripGradient = dockPanelGradient1;
            tabGradient1.EndColor = System.Drawing.SystemColors.Control;
            tabGradient1.StartColor = System.Drawing.SystemColors.Control;
            tabGradient1.TextColor = System.Drawing.SystemColors.ControlDarkDark;
            autoHideStripSkin1.TabGradient = tabGradient1;
            dockPanelSkin1.AutoHideStripSkin = autoHideStripSkin1;
            tabGradient2.EndColor = System.Drawing.SystemColors.ControlLightLight;
            tabGradient2.StartColor = System.Drawing.SystemColors.ControlLightLight;
            tabGradient2.TextColor = System.Drawing.SystemColors.ControlText;
            dockPaneStripGradient1.ActiveTabGradient = tabGradient2;
            dockPanelGradient2.EndColor = System.Drawing.SystemColors.Control;
            dockPanelGradient2.StartColor = System.Drawing.SystemColors.Control;
            dockPaneStripGradient1.DockStripGradient = dockPanelGradient2;
            tabGradient3.EndColor = System.Drawing.SystemColors.ControlLight;
            tabGradient3.StartColor = System.Drawing.SystemColors.ControlLight;
            tabGradient3.TextColor = System.Drawing.SystemColors.ControlText;
            dockPaneStripGradient1.InactiveTabGradient = tabGradient3;
            dockPaneStripSkin1.DocumentGradient = dockPaneStripGradient1;
            tabGradient4.EndColor = System.Drawing.SystemColors.ActiveCaption;
            tabGradient4.LinearGradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
            tabGradient4.StartColor = System.Drawing.SystemColors.GradientActiveCaption;
            tabGradient4.TextColor = System.Drawing.SystemColors.ActiveCaptionText;
            dockPaneStripToolWindowGradient1.ActiveCaptionGradient = tabGradient4;
            tabGradient5.EndColor = System.Drawing.SystemColors.Control;
            tabGradient5.StartColor = System.Drawing.SystemColors.Control;
            tabGradient5.TextColor = System.Drawing.SystemColors.ControlText;
            dockPaneStripToolWindowGradient1.ActiveTabGradient = tabGradient5;
            dockPanelGradient3.EndColor = System.Drawing.SystemColors.ControlLight;
            dockPanelGradient3.StartColor = System.Drawing.SystemColors.ControlLight;
            dockPaneStripToolWindowGradient1.DockStripGradient = dockPanelGradient3;
            tabGradient6.EndColor = System.Drawing.SystemColors.GradientInactiveCaption;
            tabGradient6.LinearGradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
            tabGradient6.StartColor = System.Drawing.SystemColors.GradientInactiveCaption;
            tabGradient6.TextColor = System.Drawing.SystemColors.ControlText;
            dockPaneStripToolWindowGradient1.InactiveCaptionGradient = tabGradient6;
            tabGradient7.EndColor = System.Drawing.Color.Transparent;
            tabGradient7.StartColor = System.Drawing.Color.Transparent;
            tabGradient7.TextColor = System.Drawing.SystemColors.ControlDarkDark;
            dockPaneStripToolWindowGradient1.InactiveTabGradient = tabGradient7;
            dockPaneStripSkin1.ToolWindowGradient = dockPaneStripToolWindowGradient1;
            dockPanelSkin1.DockPaneStripSkin = dockPaneStripSkin1;
            this.dockPanel1.Skin = dockPanelSkin1;
            this.dockPanel1.TabIndex = 1;
            this.dockPanel1.ActiveContentChanged += new System.EventHandler(this.dockPanel1_ActiveContentChanged);
            this.dockPanel1.ActiveDocumentChanged += new System.EventHandler(this.dockPanel1_ActiveDocumentChanged);
            // 
            // BottomToolStripPanel
            // 
            this.BottomToolStripPanel.Location = new System.Drawing.Point(0, 0);
            this.BottomToolStripPanel.Name = "BottomToolStripPanel";
            this.BottomToolStripPanel.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.BottomToolStripPanel.RowMargin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.BottomToolStripPanel.Size = new System.Drawing.Size(0, 0);
            // 
            // TopToolStripPanel
            // 
            this.TopToolStripPanel.Location = new System.Drawing.Point(0, 0);
            this.TopToolStripPanel.Name = "TopToolStripPanel";
            this.TopToolStripPanel.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.TopToolStripPanel.RowMargin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.TopToolStripPanel.Size = new System.Drawing.Size(0, 0);
            // 
            // RightToolStripPanel
            // 
            this.RightToolStripPanel.Location = new System.Drawing.Point(0, 0);
            this.RightToolStripPanel.Name = "RightToolStripPanel";
            this.RightToolStripPanel.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.RightToolStripPanel.RowMargin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.RightToolStripPanel.Size = new System.Drawing.Size(0, 0);
            // 
            // LeftToolStripPanel
            // 
            this.LeftToolStripPanel.Location = new System.Drawing.Point(0, 0);
            this.LeftToolStripPanel.Name = "LeftToolStripPanel";
            this.LeftToolStripPanel.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.LeftToolStripPanel.RowMargin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.LeftToolStripPanel.Size = new System.Drawing.Size(0, 0);
            // 
            // ContentPanel
            // 
            this.ContentPanel.Size = new System.Drawing.Size(467, 175);
            // 
            // progressBar1
            // 
            this.progressBar1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar1.Location = new System.Drawing.Point(882, 27);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(100, 18);
            this.progressBar1.TabIndex = 4;
            this.progressBar1.Visible = false;
            this.progressBar1.Click += new System.EventHandler(this.progressBar1_Click);
            // 
            // timerScanner
            // 
            this.timerScanner.Enabled = true;
            this.timerScanner.Interval = 30000;
            this.timerScanner.Tick += new System.EventHandler(this.timerScanner_Tick);
            // 
            // timerBackup
            // 
            this.timerBackup.Tick += new System.EventHandler(this.timerBackup_Tick);
            // 
            // mbtnGoto
            // 
            this.mbtnGoto.Name = "mbtnGoto";
            this.mbtnGoto.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.G)));
            this.mbtnGoto.Size = new System.Drawing.Size(204, 22);
            this.mbtnGoto.Text = "Goto Line";
            this.mbtnGoto.Click += new System.EventHandler(this.mbtnGoto_Click);
            // 
            // IDEWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(994, 510);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.toolStripMain);
            this.Controls.Add(this.menuStripMain);
            this.Controls.Add(this.dockPanel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStripMain;
            this.Name = "IDEWindow";
            this.Text = "AVR Project IDE";
            this.Load += new System.EventHandler(this.IDEWindow_Load);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmProjIDE_FormClosed);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmProjIDE_FormClosing);
            this.menuStripMain.ResumeLayout(false);
            this.menuStripMain.PerformLayout();
            this.toolStripMain.ResumeLayout(false);
            this.toolStripMain.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStripMain;
        private System.Windows.Forms.MenuStrip menuStripMain;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton tbtnNewOrig;
        private System.Windows.Forms.ToolStripButton tbtnOpenOrig;
        private System.Windows.Forms.ToolStripButton tbtnSaveOrig;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripButton tbtnCutOrig;
        private System.Windows.Forms.ToolStripButton tbtnCopyOrig;
        private System.Windows.Forms.ToolStripButton tbtnPasteOrig;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ImageList imgListFileTree;
        private System.Windows.Forms.ImageList imgListTabs;
        private System.Windows.Forms.ToolStripMenuItem mbtnSaveAllFiles;
        private System.Windows.Forms.ToolStripMenuItem mbtnSaveConfigAs;
        private System.Windows.Forms.ToolStripButton tbtnConfig;
        private System.Windows.Forms.ToolStripButton tbtnCompile;
        private System.Windows.Forms.ToolStripButton tbtnBurn;
        private System.Windows.Forms.ToolStripMenuItem mbtnOpenProject;
        private System.Windows.Forms.ToolStripMenuItem mbtnUndo;
        private System.Windows.Forms.ToolStripMenuItem mbtnRedo;
        private System.Windows.Forms.ToolStripMenuItem mbtnSelectAll;
        private System.Windows.Forms.ToolStripMenuItem mbtnCut;
        private System.Windows.Forms.ToolStripMenuItem mbtnCopy;
        private System.Windows.Forms.ToolStripMenuItem mbtnPaste;
        private System.Windows.Forms.ToolStripMenuItem mbtnFindReplace;
        private System.Windows.Forms.ToolStripMenuItem mbtnConfig;
        private System.Windows.Forms.ToolStripMenuItem mbtnCompile;
        private System.Windows.Forms.ToolStripMenuItem mbtnBurn;
        private System.Windows.Forms.ToolStripMenuItem mbtnHelpTopics;
        private System.Windows.Forms.ToolStripMenuItem mbtnAbout;
        private System.Windows.Forms.ToolStripMenuItem mbtnRecentProjects;
        private System.Windows.Forms.ToolStripMenuItem mbtnExit;
        private System.Windows.Forms.ToolStripMenuItem mbtnSaveFileAs;
        private System.Windows.Forms.ToolStripMenuItem mbtnSearchAll;
        private System.Windows.Forms.ToolStripMenuItem mbtnBlockEdit;
        private System.Windows.Forms.ToolStripMenuItem mbtnComment;
        private System.Windows.Forms.ToolStripMenuItem mbtnUncomment;
        private System.Windows.Forms.ToolStripMenuItem mbtnIndent;
        private System.Windows.Forms.ToolStripMenuItem mbtnUnindent;
        private System.Windows.Forms.ToolStripMenuItem mbtnFindNext;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripButton tbtnUndo;
        private System.Windows.Forms.ToolStripButton tbtnRedo;
        private System.Windows.Forms.ToolStripButton tbtnSearch;
        private System.Windows.Forms.ToolStripButton tbtnBlockUncomment;
        private System.Windows.Forms.ToolStripButton tbtnSaveAll;
        private System.Windows.Forms.ToolStripButton tbtnFind;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripButton tbtnBlockTab;
        private System.Windows.Forms.ToolStripButton tbtnBlockUntab;
        private System.Windows.Forms.ToolStripButton tbtnBlockComment;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
        private System.Windows.Forms.ToolStripButton tbtnFindNext;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripMenuItem mbtnSaveCurFile;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparatorReuse;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator10;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator11;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator12;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator13;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator14;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator9;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator16;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator17;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator15;
        private System.Windows.Forms.ToolStripMenuItem mbtnClearBookmarks;
        private System.Windows.Forms.ToolStripMenuItem mbtnExportMakefile;
        private WeifenLuo.WinFormsUI.Docking.DockPanel dockPanel1;
        private System.Windows.Forms.ToolStripPanel BottomToolStripPanel;
        private System.Windows.Forms.ToolStripPanel TopToolStripPanel;
        private System.Windows.Forms.ToolStripPanel RightToolStripPanel;
        private System.Windows.Forms.ToolStripPanel LeftToolStripPanel;
        private System.Windows.Forms.ToolStripContentPanel ContentPanel;
        private System.Windows.Forms.ToolStripMenuItem mbtnExportAPS;
        private System.Windows.Forms.ToolStripMenuItem mbtnRunMake;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Timer timerScanner;
        private System.Windows.Forms.ToolStripMenuItem mbtnEditorSettings;
        private System.Windows.Forms.Timer timerBackup;
        private System.Windows.Forms.ToolStripMenuItem mbtnCompileCurrent;
        private System.Windows.Forms.ToolStripMenuItem mbtnFuseTool;
        private System.Windows.Forms.ToolStripMenuItem mbtnAVRDUDE;
        private System.Windows.Forms.ToolStripMenuItem mbtnGoto;


    }
}

