namespace AVRProjectIDE
{
    partial class EditorPanel
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
            this.scint = new ScintillaNet.Scintilla();
            this.rClickMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mbtnSelectAll = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.mbtnCut = new System.Windows.Forms.ToolStripMenuItem();
            this.mbtnCopy = new System.Windows.Forms.ToolStripMenuItem();
            this.mbtnPaste = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.mbtnFind = new System.Windows.Forms.ToolStripMenuItem();
            this.mbtnAdvancedEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.mbtnComment = new System.Windows.Forms.ToolStripMenuItem();
            this.mbtnUncomment = new System.Windows.Forms.ToolStripMenuItem();
            this.mbtnIndent = new System.Windows.Forms.ToolStripMenuItem();
            this.mbtnUnindent = new System.Windows.Forms.ToolStripMenuItem();
            this.timerChangeMonitor = new System.Windows.Forms.Timer(this.components);
            this.fileSystemWatcher1 = new System.IO.FileSystemWatcher();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.tabMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mbtnCloseMe = new System.Windows.Forms.ToolStripMenuItem();
            this.mbtnCloseExceptMe = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblLineNum = new System.Windows.Forms.ToolStripStatusLabel();
            this.mbtnReplace = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.scint)).BeginInit();
            this.rClickMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fileSystemWatcher1)).BeginInit();
            this.tabMenuStrip.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // scint
            // 
            this.scint.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.scint.ConfigurationManager.Language = "cs";
            this.scint.ContextMenuStrip = this.rClickMenu;
            this.scint.IsBraceMatching = true;
            this.scint.Location = new System.Drawing.Point(0, 0);
            this.scint.Margins.Margin0.Width = 33;
            this.scint.Margins.Margin1.AutoToggleMarkerNumber = 0;
            this.scint.Margins.Margin1.IsClickable = true;
            this.scint.Margins.Margin2.Width = 10;
            this.scint.Name = "scint";
            this.scint.Size = new System.Drawing.Size(868, 354);
            this.scint.Styles.BraceBad.FontName = "Verdana";
            this.scint.Styles.BraceLight.FontName = "Verdana";
            this.scint.Styles.ControlChar.FontName = "Verdana";
            this.scint.Styles.Default.FontName = "Verdana";
            this.scint.Styles.IndentGuide.FontName = "Verdana";
            this.scint.Styles.LastPredefined.FontName = "Verdana";
            this.scint.Styles.LineNumber.FontName = "Verdana";
            this.scint.Styles.Max.FontName = "Verdana";
            this.scint.TabIndex = 0;
            this.scint.ZoomChanged += new System.EventHandler(this.scint_ZoomChanged);
            this.scint.MarginClick += new System.EventHandler<ScintillaNet.MarginClickEventArgs>(this.scint_MarginClick);
            this.scint.CharAdded += new System.EventHandler<ScintillaNet.CharAddedEventArgs>(this.scint_CharAdded);
            this.scint.MarkerChanged += new System.EventHandler<ScintillaNet.MarkerChangedEventArgs>(this.scint_MarkerChanged);
            this.scint.AutoCompleteAccepted += new System.EventHandler<ScintillaNet.AutoCompleteAcceptedEventArgs>(this.scint_AutoCompleteAccepted);
            // 
            // rClickMenu
            // 
            this.rClickMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mbtnSelectAll,
            this.toolStripSeparator1,
            this.mbtnCut,
            this.mbtnCopy,
            this.mbtnPaste,
            this.toolStripSeparator2,
            this.mbtnFind,
            this.mbtnReplace,
            this.mbtnAdvancedEdit});
            this.rClickMenu.Name = "rClickMenu";
            this.rClickMenu.ShowImageMargin = false;
            this.rClickMenu.ShowItemToolTips = false;
            this.rClickMenu.Size = new System.Drawing.Size(140, 192);
            // 
            // mbtnSelectAll
            // 
            this.mbtnSelectAll.Name = "mbtnSelectAll";
            this.mbtnSelectAll.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.A)));
            this.mbtnSelectAll.Size = new System.Drawing.Size(139, 22);
            this.mbtnSelectAll.Text = "Select All";
            this.mbtnSelectAll.Click += new System.EventHandler(this.mbtnSelectAll_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(136, 6);
            // 
            // mbtnCut
            // 
            this.mbtnCut.Name = "mbtnCut";
            this.mbtnCut.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.X)));
            this.mbtnCut.Size = new System.Drawing.Size(139, 22);
            this.mbtnCut.Text = "Cut";
            this.mbtnCut.Click += new System.EventHandler(this.mbtnCut_Click);
            // 
            // mbtnCopy
            // 
            this.mbtnCopy.Name = "mbtnCopy";
            this.mbtnCopy.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
            this.mbtnCopy.Size = new System.Drawing.Size(139, 22);
            this.mbtnCopy.Text = "Copy";
            this.mbtnCopy.Click += new System.EventHandler(this.mbtnCopy_Click);
            // 
            // mbtnPaste
            // 
            this.mbtnPaste.Name = "mbtnPaste";
            this.mbtnPaste.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.V)));
            this.mbtnPaste.Size = new System.Drawing.Size(139, 22);
            this.mbtnPaste.Text = "Paste";
            this.mbtnPaste.Click += new System.EventHandler(this.mbtnPaste_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(136, 6);
            // 
            // mbtnFind
            // 
            this.mbtnFind.Name = "mbtnFind";
            this.mbtnFind.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F)));
            this.mbtnFind.Size = new System.Drawing.Size(139, 22);
            this.mbtnFind.Text = "Find";
            this.mbtnFind.Click += new System.EventHandler(this.mbtnFind_Click);
            // 
            // mbtnAdvancedEdit
            // 
            this.mbtnAdvancedEdit.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.mbtnAdvancedEdit.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mbtnComment,
            this.mbtnUncomment,
            this.mbtnIndent,
            this.mbtnUnindent});
            this.mbtnAdvancedEdit.Name = "mbtnAdvancedEdit";
            this.mbtnAdvancedEdit.Size = new System.Drawing.Size(139, 22);
            this.mbtnAdvancedEdit.Text = "Advanced";
            // 
            // mbtnComment
            // 
            this.mbtnComment.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.mbtnComment.Name = "mbtnComment";
            this.mbtnComment.Size = new System.Drawing.Size(141, 22);
            this.mbtnComment.Text = "Comment";
            this.mbtnComment.Click += new System.EventHandler(this.mbtnComment_Click);
            // 
            // mbtnUncomment
            // 
            this.mbtnUncomment.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.mbtnUncomment.Name = "mbtnUncomment";
            this.mbtnUncomment.Size = new System.Drawing.Size(141, 22);
            this.mbtnUncomment.Text = "Uncomment";
            this.mbtnUncomment.Click += new System.EventHandler(this.mbtnUncomment_Click);
            // 
            // mbtnIndent
            // 
            this.mbtnIndent.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.mbtnIndent.Name = "mbtnIndent";
            this.mbtnIndent.Size = new System.Drawing.Size(141, 22);
            this.mbtnIndent.Text = "Indent";
            this.mbtnIndent.Click += new System.EventHandler(this.mbtnIndent_Click);
            // 
            // mbtnUnindent
            // 
            this.mbtnUnindent.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.mbtnUnindent.Name = "mbtnUnindent";
            this.mbtnUnindent.Size = new System.Drawing.Size(141, 22);
            this.mbtnUnindent.Text = "Unindent";
            this.mbtnUnindent.Click += new System.EventHandler(this.mbtnUnindent_Click);
            // 
            // timerChangeMonitor
            // 
            this.timerChangeMonitor.Enabled = true;
            this.timerChangeMonitor.Interval = 500;
            this.timerChangeMonitor.Tick += new System.EventHandler(this.timerChangeMonitor_Tick);
            // 
            // fileSystemWatcher1
            // 
            this.fileSystemWatcher1.EnableRaisingEvents = true;
            this.fileSystemWatcher1.SynchronizingObject = this;
            this.fileSystemWatcher1.Renamed += new System.IO.RenamedEventHandler(this.fileSystemWatcher1_Renamed);
            this.fileSystemWatcher1.Changed += new System.IO.FileSystemEventHandler(this.fileSystemWatcher1_Changed);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // tabMenuStrip
            // 
            this.tabMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mbtnCloseMe,
            this.mbtnCloseExceptMe});
            this.tabMenuStrip.Name = "tabMenuStrip";
            this.tabMenuStrip.ShowImageMargin = false;
            this.tabMenuStrip.Size = new System.Drawing.Size(137, 48);
            // 
            // mbtnCloseMe
            // 
            this.mbtnCloseMe.Name = "mbtnCloseMe";
            this.mbtnCloseMe.Size = new System.Drawing.Size(136, 22);
            this.mbtnCloseMe.Text = "Close Me";
            this.mbtnCloseMe.Click += new System.EventHandler(this.mbtnCloseMe_Click);
            // 
            // mbtnCloseExceptMe
            // 
            this.mbtnCloseExceptMe.Name = "mbtnCloseExceptMe";
            this.mbtnCloseExceptMe.Size = new System.Drawing.Size(136, 22);
            this.mbtnCloseExceptMe.Text = "Close All But Me";
            this.mbtnCloseExceptMe.Click += new System.EventHandler(this.mbtnCloseExceptMe_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.lblLineNum});
            this.statusStrip1.Location = new System.Drawing.Point(0, 355);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(868, 22);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(35, 17);
            this.toolStripStatusLabel1.Text = "Line: ";
            // 
            // lblLineNum
            // 
            this.lblLineNum.Name = "lblLineNum";
            this.lblLineNum.Size = new System.Drawing.Size(0, 17);
            // 
            // mbtnReplace
            // 
            this.mbtnReplace.Name = "mbtnReplace";
            this.mbtnReplace.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.H)));
            this.mbtnReplace.Size = new System.Drawing.Size(139, 22);
            this.mbtnReplace.Text = "Replace";
            this.mbtnReplace.Click += new System.EventHandler(this.mbtnReplace_Click);
            // 
            // EditorPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(868, 377);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.scint);
            this.DockAreas = ((WeifenLuo.WinFormsUI.Docking.DockAreas)((WeifenLuo.WinFormsUI.Docking.DockAreas.Float | WeifenLuo.WinFormsUI.Docking.DockAreas.Document)));
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MinimizeBox = false;
            this.Name = "EditorPanel";
            this.ShowIcon = false;
            this.TabPageContextMenuStrip = this.tabMenuStrip;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Shown += new System.EventHandler(this.EditorPanelContent_Shown);
            this.Activated += new System.EventHandler(this.EditorPanel_Activated);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.EditorPanelContent_FormClosed);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.EditorPanelContent_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.scint)).EndInit();
            this.rClickMenu.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.fileSystemWatcher1)).EndInit();
            this.tabMenuStrip.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ScintillaNet.Scintilla scint;
        private System.Windows.Forms.Timer timerChangeMonitor;
        private System.IO.FileSystemWatcher fileSystemWatcher1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.ContextMenuStrip rClickMenu;
        private System.Windows.Forms.ToolStripMenuItem mbtnCut;
        private System.Windows.Forms.ToolStripMenuItem mbtnCopy;
        private System.Windows.Forms.ToolStripMenuItem mbtnPaste;
        private System.Windows.Forms.ToolStripMenuItem mbtnSelectAll;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem mbtnFind;
        private System.Windows.Forms.ToolStripMenuItem mbtnAdvancedEdit;
        private System.Windows.Forms.ToolStripMenuItem mbtnComment;
        private System.Windows.Forms.ToolStripMenuItem mbtnUncomment;
        private System.Windows.Forms.ToolStripMenuItem mbtnIndent;
        private System.Windows.Forms.ToolStripMenuItem mbtnUnindent;
        private System.Windows.Forms.ContextMenuStrip tabMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem mbtnCloseMe;
        private System.Windows.Forms.ToolStripMenuItem mbtnCloseExceptMe;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel lblLineNum;
        private System.Windows.Forms.ToolStripMenuItem mbtnReplace;
    }
}