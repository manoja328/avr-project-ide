namespace AVRProjectIDE
{
    partial class HardwareExplorer
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
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.txtChipInfo = new System.Windows.Forms.TextBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.treeIOModules = new System.Windows.Forms.TreeView();
            this.txtIOModuleInfo = new System.Windows.Forms.TextBox();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.splitContainer4 = new System.Windows.Forms.SplitContainer();
            this.listInterrupts = new System.Windows.Forms.ListBox();
            this.txtInterruptInfo = new System.Windows.Forms.TextBox();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.treePins = new System.Windows.Forms.TreeView();
            this.txtPinsInfo = new System.Windows.Forms.TextBox();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.treeXML = new System.Windows.Forms.TreeView();
            this.txtXMLInfo = new System.Windows.Forms.TextBox();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.splitContainer4.Panel1.SuspendLayout();
            this.splitContainer4.Panel2.SuspendLayout();
            this.splitContainer4.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            this.tabPage5.SuspendLayout();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Controls.Add(this.tabPage5);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(303, 264);
            this.tabControl1.TabIndex = 1;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.txtChipInfo);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(295, 238);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Chip Data";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // txtChipInfo
            // 
            this.txtChipInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtChipInfo.Location = new System.Drawing.Point(3, 3);
            this.txtChipInfo.Multiline = true;
            this.txtChipInfo.Name = "txtChipInfo";
            this.txtChipInfo.ReadOnly = true;
            this.txtChipInfo.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtChipInfo.Size = new System.Drawing.Size(289, 232);
            this.txtChipInfo.TabIndex = 0;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.splitContainer1);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(295, 238);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "IO Modules";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(3, 3);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.treeIOModules);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.txtIOModuleInfo);
            this.splitContainer1.Size = new System.Drawing.Size(289, 232);
            this.splitContainer1.SplitterDistance = 167;
            this.splitContainer1.TabIndex = 1;
            // 
            // treeIOModules
            // 
            this.treeIOModules.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeIOModules.FullRowSelect = true;
            this.treeIOModules.HideSelection = false;
            this.treeIOModules.LabelEdit = true;
            this.treeIOModules.Location = new System.Drawing.Point(0, 0);
            this.treeIOModules.Name = "treeIOModules";
            this.treeIOModules.ShowNodeToolTips = true;
            this.treeIOModules.Size = new System.Drawing.Size(289, 167);
            this.treeIOModules.TabIndex = 0;
            this.treeIOModules.AfterLabelEdit += new System.Windows.Forms.NodeLabelEditEventHandler(this.tree_AfterLabelEditCancel);
            this.treeIOModules.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeIOModules_AfterSelect);
            // 
            // txtIOModuleInfo
            // 
            this.txtIOModuleInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtIOModuleInfo.Location = new System.Drawing.Point(0, 0);
            this.txtIOModuleInfo.Multiline = true;
            this.txtIOModuleInfo.Name = "txtIOModuleInfo";
            this.txtIOModuleInfo.ReadOnly = true;
            this.txtIOModuleInfo.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtIOModuleInfo.Size = new System.Drawing.Size(289, 61);
            this.txtIOModuleInfo.TabIndex = 1;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.splitContainer4);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(295, 238);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Interrupts";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // splitContainer4
            // 
            this.splitContainer4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer4.Location = new System.Drawing.Point(3, 3);
            this.splitContainer4.Name = "splitContainer4";
            this.splitContainer4.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer4.Panel1
            // 
            this.splitContainer4.Panel1.Controls.Add(this.listInterrupts);
            // 
            // splitContainer4.Panel2
            // 
            this.splitContainer4.Panel2.Controls.Add(this.txtInterruptInfo);
            this.splitContainer4.Size = new System.Drawing.Size(289, 232);
            this.splitContainer4.SplitterDistance = 167;
            this.splitContainer4.TabIndex = 2;
            // 
            // listInterrupts
            // 
            this.listInterrupts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listInterrupts.FormattingEnabled = true;
            this.listInterrupts.Location = new System.Drawing.Point(0, 0);
            this.listInterrupts.Name = "listInterrupts";
            this.listInterrupts.Size = new System.Drawing.Size(289, 160);
            this.listInterrupts.TabIndex = 0;
            this.listInterrupts.SelectedIndexChanged += new System.EventHandler(this.listInterrupts_SelectedIndexChanged);
            // 
            // txtInterruptInfo
            // 
            this.txtInterruptInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtInterruptInfo.Location = new System.Drawing.Point(0, 0);
            this.txtInterruptInfo.Multiline = true;
            this.txtInterruptInfo.Name = "txtInterruptInfo";
            this.txtInterruptInfo.ReadOnly = true;
            this.txtInterruptInfo.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtInterruptInfo.Size = new System.Drawing.Size(289, 61);
            this.txtInterruptInfo.TabIndex = 1;
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.splitContainer3);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(295, 238);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "Pins";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // splitContainer3
            // 
            this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer3.Location = new System.Drawing.Point(3, 3);
            this.splitContainer3.Name = "splitContainer3";
            this.splitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this.treePins);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this.txtPinsInfo);
            this.splitContainer3.Size = new System.Drawing.Size(289, 232);
            this.splitContainer3.SplitterDistance = 167;
            this.splitContainer3.TabIndex = 2;
            // 
            // treePins
            // 
            this.treePins.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treePins.FullRowSelect = true;
            this.treePins.HideSelection = false;
            this.treePins.HotTracking = true;
            this.treePins.LabelEdit = true;
            this.treePins.Location = new System.Drawing.Point(0, 0);
            this.treePins.Name = "treePins";
            this.treePins.ShowNodeToolTips = true;
            this.treePins.Size = new System.Drawing.Size(289, 167);
            this.treePins.TabIndex = 0;
            this.treePins.AfterLabelEdit += new System.Windows.Forms.NodeLabelEditEventHandler(this.tree_AfterLabelEditCancel);
            this.treePins.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treePins_AfterSelect);
            // 
            // txtPinsInfo
            // 
            this.txtPinsInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtPinsInfo.Location = new System.Drawing.Point(0, 0);
            this.txtPinsInfo.Multiline = true;
            this.txtPinsInfo.Name = "txtPinsInfo";
            this.txtPinsInfo.ReadOnly = true;
            this.txtPinsInfo.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtPinsInfo.Size = new System.Drawing.Size(289, 61);
            this.txtPinsInfo.TabIndex = 1;
            // 
            // tabPage5
            // 
            this.tabPage5.Controls.Add(this.splitContainer2);
            this.tabPage5.Location = new System.Drawing.Point(4, 22);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage5.Size = new System.Drawing.Size(295, 238);
            this.tabPage5.TabIndex = 4;
            this.tabPage5.Text = "XML View";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(3, 3);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.treeXML);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.txtXMLInfo);
            this.splitContainer2.Size = new System.Drawing.Size(289, 232);
            this.splitContainer2.SplitterDistance = 167;
            this.splitContainer2.TabIndex = 2;
            // 
            // treeXML
            // 
            this.treeXML.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeXML.FullRowSelect = true;
            this.treeXML.HideSelection = false;
            this.treeXML.LabelEdit = true;
            this.treeXML.Location = new System.Drawing.Point(0, 0);
            this.treeXML.Name = "treeXML";
            this.treeXML.ShowNodeToolTips = true;
            this.treeXML.Size = new System.Drawing.Size(289, 167);
            this.treeXML.TabIndex = 0;
            this.treeXML.AfterLabelEdit += new System.Windows.Forms.NodeLabelEditEventHandler(this.tree_AfterLabelEditCancel);
            this.treeXML.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeXML_AfterSelect);
            // 
            // txtXMLInfo
            // 
            this.txtXMLInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtXMLInfo.Location = new System.Drawing.Point(0, 0);
            this.txtXMLInfo.Multiline = true;
            this.txtXMLInfo.Name = "txtXMLInfo";
            this.txtXMLInfo.ReadOnly = true;
            this.txtXMLInfo.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtXMLInfo.Size = new System.Drawing.Size(289, 61);
            this.txtXMLInfo.TabIndex = 1;
            // 
            // HardwareExplorer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(303, 264);
            this.CloseButton = false;
            this.CloseButtonVisible = false;
            this.Controls.Add(this.tabControl1);
            this.DockAreas = ((WeifenLuo.WinFormsUI.Docking.DockAreas)(((((WeifenLuo.WinFormsUI.Docking.DockAreas.Float | WeifenLuo.WinFormsUI.Docking.DockAreas.DockLeft)
                        | WeifenLuo.WinFormsUI.Docking.DockAreas.DockRight)
                        | WeifenLuo.WinFormsUI.Docking.DockAreas.DockTop)
                        | WeifenLuo.WinFormsUI.Docking.DockAreas.DockBottom)));
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "HardwareExplorer";
            this.TabText = "Hardware Explorer";
            this.Text = "Hardware Explorer";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            this.splitContainer1.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.splitContainer4.Panel1.ResumeLayout(false);
            this.splitContainer4.Panel2.ResumeLayout(false);
            this.splitContainer4.Panel2.PerformLayout();
            this.splitContainer4.ResumeLayout(false);
            this.tabPage4.ResumeLayout(false);
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel2.ResumeLayout(false);
            this.splitContainer3.Panel2.PerformLayout();
            this.splitContainer3.ResumeLayout(false);
            this.tabPage5.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.Panel2.PerformLayout();
            this.splitContainer2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.TabPage tabPage5;
        private System.Windows.Forms.TextBox txtChipInfo;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TreeView treeIOModules;
        private System.Windows.Forms.TextBox txtIOModuleInfo;
        private System.Windows.Forms.SplitContainer splitContainer4;
        private System.Windows.Forms.ListBox listInterrupts;
        private System.Windows.Forms.TextBox txtInterruptInfo;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private System.Windows.Forms.TreeView treePins;
        private System.Windows.Forms.TextBox txtPinsInfo;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.TreeView treeXML;
        private System.Windows.Forms.TextBox txtXMLInfo;
    }
}