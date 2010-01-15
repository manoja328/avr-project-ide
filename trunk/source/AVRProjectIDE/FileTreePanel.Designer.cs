namespace AVRProjectIDE
{
    partial class FileTreePanel
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FileTreePanel));
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.treeRClickMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mbtnAddFile = new System.Windows.Forms.ToolStripMenuItem();
            this.mbtnAddFileWiz = new System.Windows.Forms.ToolStripMenuItem();
            this.imgListFileTree = new System.Windows.Forms.ImageList(this.components);
            this.nodeRClickMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mbtnRename = new System.Windows.Forms.ToolStripMenuItem();
            this.mbtnRemove = new System.Windows.Forms.ToolStripMenuItem();
            this.mbtnSetOpt = new System.Windows.Forms.ToolStripMenuItem();
            this.treeRClickMenu.SuspendLayout();
            this.nodeRClickMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // treeView1
            // 
            this.treeView1.AllowDrop = true;
            this.treeView1.CheckBoxes = true;
            this.treeView1.ContextMenuStrip = this.treeRClickMenu;
            this.treeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView1.HideSelection = false;
            this.treeView1.ImageIndex = 0;
            this.treeView1.ImageList = this.imgListFileTree;
            this.treeView1.Indent = 12;
            this.treeView1.LabelEdit = true;
            this.treeView1.Location = new System.Drawing.Point(0, 0);
            this.treeView1.Name = "treeView1";
            this.treeView1.SelectedImageIndex = 0;
            this.treeView1.ShowNodeToolTips = true;
            this.treeView1.Size = new System.Drawing.Size(221, 295);
            this.treeView1.TabIndex = 0;
            this.treeView1.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeView1_NodeMouseDoubleClick);
            this.treeView1.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterCheck);
            this.treeView1.AfterLabelEdit += new System.Windows.Forms.NodeLabelEditEventHandler(this.treeView1_AfterLabelEdit);
            this.treeView1.BeforeCollapse += new System.Windows.Forms.TreeViewCancelEventHandler(this.treeView1_BeforeCollapse);
            this.treeView1.DragDrop += new System.Windows.Forms.DragEventHandler(this.treeView1_DragDrop);
            this.treeView1.DragEnter += new System.Windows.Forms.DragEventHandler(this.treeView1_DragEnter);
            this.treeView1.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeView1_NodeMouseClick);
            this.treeView1.BeforeCheck += new System.Windows.Forms.TreeViewCancelEventHandler(this.treeView1_BeforeCheck);
            this.treeView1.BeforeLabelEdit += new System.Windows.Forms.NodeLabelEditEventHandler(this.treeView1_BeforeLabelEdit);
            this.treeView1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.treeView1_KeyDown);
            // 
            // treeRClickMenu
            // 
            this.treeRClickMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mbtnAddFile,
            this.mbtnAddFileWiz});
            this.treeRClickMenu.Name = "treeRClickMenu";
            this.treeRClickMenu.ShowImageMargin = false;
            this.treeRClickMenu.Size = new System.Drawing.Size(155, 70);
            // 
            // mbtnAddFile
            // 
            this.mbtnAddFile.Name = "mbtnAddFile";
            this.mbtnAddFile.Size = new System.Drawing.Size(154, 22);
            this.mbtnAddFile.Text = "Add / Find File";
            this.mbtnAddFile.Click += new System.EventHandler(this.mbtnAddFile_Click);
            // 
            // mbtnAddFileWiz
            // 
            this.mbtnAddFileWiz.Name = "mbtnAddFileWiz";
            this.mbtnAddFileWiz.Size = new System.Drawing.Size(154, 22);
            this.mbtnAddFileWiz.Text = "Add File using Wizard";
            this.mbtnAddFileWiz.Click += new System.EventHandler(this.mbtnAddFileWiz_Click);
            // 
            // imgListFileTree
            // 
            this.imgListFileTree.TransparentColor = System.Drawing.Color.Transparent;
            this.imgListFileTree.Images.Add("file.ico", global::AVRProjectIDE.GraphicsResx.file);
            this.imgListFileTree.Images.Add("file2.ico", global::AVRProjectIDE.GraphicsResx.file2);
            this.imgListFileTree.Images.Add("nocompile.ico", global::AVRProjectIDE.GraphicsResx.nocompile);
            this.imgListFileTree.Images.Add("missing.ico", global::AVRProjectIDE.GraphicsResx.missing);
            this.imgListFileTree.Images.Add("treeroot.ico", global::AVRProjectIDE.GraphicsResx.treeroot);
            this.imgListFileTree.Images.Add("unknown2.ico", global::AVRProjectIDE.GraphicsResx.unknown2);
            this.imgListFileTree.Images.Add("folder.png", global::AVRProjectIDE.GraphicsResx.folder);
            this.imgListFileTree.Images.Add("folder2.png", global::AVRProjectIDE.GraphicsResx.folder2);
            // 
            // nodeRClickMenu
            // 
            this.nodeRClickMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mbtnRename,
            this.mbtnRemove,
            this.mbtnSetOpt});
            this.nodeRClickMenu.Name = "rClickMenu";
            this.nodeRClickMenu.ShowImageMargin = false;
            this.nodeRClickMenu.Size = new System.Drawing.Size(148, 70);
            // 
            // mbtnRename
            // 
            this.mbtnRename.Name = "mbtnRename";
            this.mbtnRename.Size = new System.Drawing.Size(147, 22);
            this.mbtnRename.Text = "Rename";
            this.mbtnRename.Click += new System.EventHandler(this.mbtnRename_Click);
            // 
            // mbtnRemove
            // 
            this.mbtnRemove.Name = "mbtnRemove";
            this.mbtnRemove.Size = new System.Drawing.Size(147, 22);
            this.mbtnRemove.Text = "Remove";
            this.mbtnRemove.Click += new System.EventHandler(this.mbtnDelete_Click);
            // 
            // mbtnSetOpt
            // 
            this.mbtnSetOpt.Name = "mbtnSetOpt";
            this.mbtnSetOpt.Size = new System.Drawing.Size(147, 22);
            this.mbtnSetOpt.Text = "Set Compile Options";
            this.mbtnSetOpt.Click += new System.EventHandler(this.mbtnSetOpt_Click);
            // 
            // FileTreePanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(221, 295);
            this.CloseButton = false;
            this.CloseButtonVisible = false;
            this.ControlBox = false;
            this.Controls.Add(this.treeView1);
            this.DockAreas = ((WeifenLuo.WinFormsUI.Docking.DockAreas)(((WeifenLuo.WinFormsUI.Docking.DockAreas.Float | WeifenLuo.WinFormsUI.Docking.DockAreas.DockLeft)
                        | WeifenLuo.WinFormsUI.Docking.DockAreas.DockRight)));
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.HideOnClose = true;
            this.Name = "FileTreePanel";
            this.TabText = "Files";
            this.Text = "Files";
            this.treeRClickMenu.ResumeLayout(false);
            this.nodeRClickMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.ImageList imgListFileTree;
        private System.Windows.Forms.ContextMenuStrip nodeRClickMenu;
        private System.Windows.Forms.ToolStripMenuItem mbtnRename;
        private System.Windows.Forms.ToolStripMenuItem mbtnRemove;
        private System.Windows.Forms.ToolStripMenuItem mbtnSetOpt;
        private System.Windows.Forms.ContextMenuStrip treeRClickMenu;
        private System.Windows.Forms.ToolStripMenuItem mbtnAddFile;
        private System.Windows.Forms.ToolStripMenuItem mbtnAddFileWiz;
    }
}
