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
            this.tabControl2 = new System.Windows.Forms.TabControl();
            this.tabPage6 = new System.Windows.Forms.TabPage();
            this.splitContainer4 = new System.Windows.Forms.SplitContainer();
            this.listInterruptsAVRLibc = new System.Windows.Forms.ListBox();
            this.txtInterruptInfoAVRLibc = new System.Windows.Forms.TextBox();
            this.tabPage7 = new System.Windows.Forms.TabPage();
            this.splitContainer5 = new System.Windows.Forms.SplitContainer();
            this.listInterruptsAtmelXML = new System.Windows.Forms.ListBox();
            this.txtInterruptInfoAtmelXML = new System.Windows.Forms.TextBox();
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
            this.tabControl2.SuspendLayout();
            this.tabPage6.SuspendLayout();
            this.splitContainer4.Panel1.SuspendLayout();
            this.splitContainer4.Panel2.SuspendLayout();
            this.splitContainer4.SuspendLayout();
            this.tabPage7.SuspendLayout();
            this.splitContainer5.Panel1.SuspendLayout();
            this.splitContainer5.Panel2.SuspendLayout();
            this.splitContainer5.SuspendLayout();
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
            this.tabControl1.Size = new System.Drawing.Size(336, 472);
            this.tabControl1.TabIndex = 1;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.txtChipInfo);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(328, 446);
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
            this.txtChipInfo.Size = new System.Drawing.Size(322, 440);
            this.txtChipInfo.TabIndex = 0;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.splitContainer1);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(328, 446);
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
            this.splitContainer1.Size = new System.Drawing.Size(322, 440);
            this.splitContainer1.SplitterDistance = 316;
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
            this.treeIOModules.Size = new System.Drawing.Size(322, 316);
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
            this.txtIOModuleInfo.Size = new System.Drawing.Size(322, 120);
            this.txtIOModuleInfo.TabIndex = 1;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.tabControl2);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(328, 446);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Interrupts";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // tabControl2
            // 
            this.tabControl2.Controls.Add(this.tabPage6);
            this.tabControl2.Controls.Add(this.tabPage7);
            this.tabControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl2.Location = new System.Drawing.Point(3, 3);
            this.tabControl2.Name = "tabControl2";
            this.tabControl2.SelectedIndex = 0;
            this.tabControl2.Size = new System.Drawing.Size(322, 440);
            this.tabControl2.TabIndex = 3;
            // 
            // tabPage6
            // 
            this.tabPage6.Controls.Add(this.splitContainer4);
            this.tabPage6.Location = new System.Drawing.Point(4, 22);
            this.tabPage6.Name = "tabPage6";
            this.tabPage6.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage6.Size = new System.Drawing.Size(314, 414);
            this.tabPage6.TabIndex = 0;
            this.tabPage6.Text = "Data Source: AVR Libc";
            this.tabPage6.UseVisualStyleBackColor = true;
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
            this.splitContainer4.Panel1.Controls.Add(this.listInterruptsAVRLibc);
            // 
            // splitContainer4.Panel2
            // 
            this.splitContainer4.Panel2.Controls.Add(this.txtInterruptInfoAVRLibc);
            this.splitContainer4.Size = new System.Drawing.Size(308, 408);
            this.splitContainer4.SplitterDistance = 293;
            this.splitContainer4.TabIndex = 2;
            // 
            // listInterruptsAVRLibc
            // 
            this.listInterruptsAVRLibc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listInterruptsAVRLibc.FormattingEnabled = true;
            this.listInterruptsAVRLibc.Location = new System.Drawing.Point(0, 0);
            this.listInterruptsAVRLibc.Name = "listInterruptsAVRLibc";
            this.listInterruptsAVRLibc.Size = new System.Drawing.Size(308, 290);
            this.listInterruptsAVRLibc.TabIndex = 0;
            this.listInterruptsAVRLibc.SelectedIndexChanged += new System.EventHandler(this.listInterruptsAVRLibc_SelectedIndexChanged);
            // 
            // txtInterruptInfoAVRLibc
            // 
            this.txtInterruptInfoAVRLibc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtInterruptInfoAVRLibc.Location = new System.Drawing.Point(0, 0);
            this.txtInterruptInfoAVRLibc.Multiline = true;
            this.txtInterruptInfoAVRLibc.Name = "txtInterruptInfoAVRLibc";
            this.txtInterruptInfoAVRLibc.ReadOnly = true;
            this.txtInterruptInfoAVRLibc.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtInterruptInfoAVRLibc.Size = new System.Drawing.Size(308, 111);
            this.txtInterruptInfoAVRLibc.TabIndex = 1;
            // 
            // tabPage7
            // 
            this.tabPage7.Controls.Add(this.splitContainer5);
            this.tabPage7.Location = new System.Drawing.Point(4, 22);
            this.tabPage7.Name = "tabPage7";
            this.tabPage7.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage7.Size = new System.Drawing.Size(314, 414);
            this.tabPage7.TabIndex = 1;
            this.tabPage7.Text = "Data Source: Atmel XML";
            this.tabPage7.UseVisualStyleBackColor = true;
            // 
            // splitContainer5
            // 
            this.splitContainer5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer5.Location = new System.Drawing.Point(3, 3);
            this.splitContainer5.Name = "splitContainer5";
            this.splitContainer5.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer5.Panel1
            // 
            this.splitContainer5.Panel1.Controls.Add(this.listInterruptsAtmelXML);
            // 
            // splitContainer5.Panel2
            // 
            this.splitContainer5.Panel2.Controls.Add(this.txtInterruptInfoAtmelXML);
            this.splitContainer5.Size = new System.Drawing.Size(308, 408);
            this.splitContainer5.SplitterDistance = 293;
            this.splitContainer5.TabIndex = 3;
            // 
            // listInterruptsAtmelXML
            // 
            this.listInterruptsAtmelXML.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listInterruptsAtmelXML.FormattingEnabled = true;
            this.listInterruptsAtmelXML.Location = new System.Drawing.Point(0, 0);
            this.listInterruptsAtmelXML.Name = "listInterruptsAtmelXML";
            this.listInterruptsAtmelXML.Size = new System.Drawing.Size(308, 290);
            this.listInterruptsAtmelXML.TabIndex = 0;
            this.listInterruptsAtmelXML.SelectedIndexChanged += new System.EventHandler(this.listInterruptsAtmelXML_SelectedIndexChanged);
            // 
            // txtInterruptInfoAtmelXML
            // 
            this.txtInterruptInfoAtmelXML.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtInterruptInfoAtmelXML.Location = new System.Drawing.Point(0, 0);
            this.txtInterruptInfoAtmelXML.Multiline = true;
            this.txtInterruptInfoAtmelXML.Name = "txtInterruptInfoAtmelXML";
            this.txtInterruptInfoAtmelXML.ReadOnly = true;
            this.txtInterruptInfoAtmelXML.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtInterruptInfoAtmelXML.Size = new System.Drawing.Size(308, 111);
            this.txtInterruptInfoAtmelXML.TabIndex = 1;
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.splitContainer3);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(328, 446);
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
            this.splitContainer3.Size = new System.Drawing.Size(322, 440);
            this.splitContainer3.SplitterDistance = 316;
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
            this.treePins.Size = new System.Drawing.Size(322, 316);
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
            this.txtPinsInfo.Size = new System.Drawing.Size(322, 120);
            this.txtPinsInfo.TabIndex = 1;
            // 
            // tabPage5
            // 
            this.tabPage5.Controls.Add(this.splitContainer2);
            this.tabPage5.Location = new System.Drawing.Point(4, 22);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage5.Size = new System.Drawing.Size(328, 446);
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
            this.splitContainer2.Size = new System.Drawing.Size(322, 440);
            this.splitContainer2.SplitterDistance = 316;
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
            this.treeXML.Size = new System.Drawing.Size(322, 316);
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
            this.txtXMLInfo.Size = new System.Drawing.Size(322, 120);
            this.txtXMLInfo.TabIndex = 1;
            // 
            // HardwareExplorer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(336, 472);
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
            this.tabControl2.ResumeLayout(false);
            this.tabPage6.ResumeLayout(false);
            this.splitContainer4.Panel1.ResumeLayout(false);
            this.splitContainer4.Panel2.ResumeLayout(false);
            this.splitContainer4.Panel2.PerformLayout();
            this.splitContainer4.ResumeLayout(false);
            this.tabPage7.ResumeLayout(false);
            this.splitContainer5.Panel1.ResumeLayout(false);
            this.splitContainer5.Panel2.ResumeLayout(false);
            this.splitContainer5.Panel2.PerformLayout();
            this.splitContainer5.ResumeLayout(false);
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
        private System.Windows.Forms.ListBox listInterruptsAVRLibc;
        private System.Windows.Forms.TextBox txtInterruptInfoAVRLibc;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private System.Windows.Forms.TreeView treePins;
        private System.Windows.Forms.TextBox txtPinsInfo;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.TreeView treeXML;
        private System.Windows.Forms.TextBox txtXMLInfo;
        private System.Windows.Forms.TabControl tabControl2;
        private System.Windows.Forms.TabPage tabPage6;
        private System.Windows.Forms.TabPage tabPage7;
        private System.Windows.Forms.SplitContainer splitContainer5;
        private System.Windows.Forms.ListBox listInterruptsAtmelXML;
        private System.Windows.Forms.TextBox txtInterruptInfoAtmelXML;
    }
}