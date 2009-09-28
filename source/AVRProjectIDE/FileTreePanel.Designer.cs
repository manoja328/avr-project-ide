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
            this.mbtnAddFile});
            this.treeRClickMenu.Name = "treeRClickMenu";
            this.treeRClickMenu.ShowImageMargin = false;
            this.treeRClickMenu.Size = new System.Drawing.Size(93, 26);
            // 
            // mbtnAddFile
            // 
            this.mbtnAddFile.Name = "mbtnAddFile";
            this.mbtnAddFile.Size = new System.Drawing.Size(92, 22);
            this.mbtnAddFile.Text = "Add File";
            this.mbtnAddFile.Click += new System.EventHandler(this.mbtnAddFile_Click);
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
            // nodeRClickMenu
            // 
            this.nodeRClickMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mbtnRename,
            this.mbtnRemove,
            this.mbtnSetOpt});
            this.nodeRClickMenu.Name = "rClickMenu";
            this.nodeRClickMenu.ShowImageMargin = false;
            this.nodeRClickMenu.Size = new System.Drawing.Size(159, 70);
            // 
            // mbtnRename
            // 
            this.mbtnRename.Name = "mbtnRename";
            this.mbtnRename.Size = new System.Drawing.Size(158, 22);
            this.mbtnRename.Text = "Rename";
            this.mbtnRename.Click += new System.EventHandler(this.mbtnRename_Click);
            // 
            // mbtnRemove
            // 
            this.mbtnRemove.Name = "mbtnRemove";
            this.mbtnRemove.Size = new System.Drawing.Size(158, 22);
            this.mbtnRemove.Text = "Remove";
            this.mbtnRemove.Click += new System.EventHandler(this.mbtnDelete_Click);
            // 
            // mbtnSetOpt
            // 
            this.mbtnSetOpt.Name = "mbtnSetOpt";
            this.mbtnSetOpt.Size = new System.Drawing.Size(158, 22);
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
    }
}
