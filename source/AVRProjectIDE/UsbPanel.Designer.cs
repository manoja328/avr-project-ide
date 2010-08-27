namespace AVRProjectIDE
{
    partial class UsbPanel
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
            this.components = new System.ComponentModel.Container();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.treeDevices = new System.Windows.Forms.TreeView();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.treeEvents = new System.Windows.Forms.TreeView();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.treeErrors = new System.Windows.Forms.TreeView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mbtnRefreshDevices = new System.Windows.Forms.ToolStripMenuItem();
            this.mbtnClearEventsErrors = new System.Windows.Forms.ToolStripMenuItem();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(576, 262);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.treeDevices);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(568, 236);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "USB Device Info";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // treeDevices
            // 
            this.treeDevices.ContextMenuStrip = this.contextMenuStrip1;
            this.treeDevices.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeDevices.LabelEdit = true;
            this.treeDevices.Location = new System.Drawing.Point(3, 3);
            this.treeDevices.Name = "treeDevices";
            this.treeDevices.Size = new System.Drawing.Size(562, 230);
            this.treeDevices.TabIndex = 1;
            this.treeDevices.AfterLabelEdit += new System.Windows.Forms.NodeLabelEditEventHandler(this.treeDevices_AfterLabelEdit);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.treeEvents);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(568, 236);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "USB Events";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // treeEvents
            // 
            this.treeEvents.ContextMenuStrip = this.contextMenuStrip1;
            this.treeEvents.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeEvents.Location = new System.Drawing.Point(3, 3);
            this.treeEvents.Name = "treeEvents";
            this.treeEvents.Size = new System.Drawing.Size(562, 230);
            this.treeEvents.TabIndex = 3;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.treeErrors);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(568, 236);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "USB Errors";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // treeErrors
            // 
            this.treeErrors.ContextMenuStrip = this.contextMenuStrip1;
            this.treeErrors.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeErrors.Location = new System.Drawing.Point(3, 3);
            this.treeErrors.Name = "treeErrors";
            this.treeErrors.Size = new System.Drawing.Size(562, 230);
            this.treeErrors.TabIndex = 5;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mbtnRefreshDevices,
            this.mbtnClearEventsErrors});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.ShowImageMargin = false;
            this.contextMenuStrip1.Size = new System.Drawing.Size(170, 48);
            // 
            // mbtnRefreshDevices
            // 
            this.mbtnRefreshDevices.Name = "mbtnRefreshDevices";
            this.mbtnRefreshDevices.Size = new System.Drawing.Size(169, 22);
            this.mbtnRefreshDevices.Text = "Refresh Devices";
            this.mbtnRefreshDevices.Click += new System.EventHandler(this.mbtnRefreshDevices_Click);
            // 
            // mbtnClearEventsErrors
            // 
            this.mbtnClearEventsErrors.Name = "mbtnClearEventsErrors";
            this.mbtnClearEventsErrors.Size = new System.Drawing.Size(169, 22);
            this.mbtnClearEventsErrors.Text = "Clear Events and Errors";
            this.mbtnClearEventsErrors.Click += new System.EventHandler(this.mbtnClearEventsErrors_Click);
            // 
            // UsbPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(576, 262);
            this.CloseButton = false;
            this.CloseButtonVisible = false;
            this.Controls.Add(this.tabControl1);
            this.DockAreas = ((WeifenLuo.WinFormsUI.Docking.DockAreas)(((((WeifenLuo.WinFormsUI.Docking.DockAreas.Float | WeifenLuo.WinFormsUI.Docking.DockAreas.DockLeft)
                        | WeifenLuo.WinFormsUI.Docking.DockAreas.DockRight)
                        | WeifenLuo.WinFormsUI.Docking.DockAreas.DockTop)
                        | WeifenLuo.WinFormsUI.Docking.DockAreas.DockBottom)));
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "UsbPanel";
            this.ShowInTaskbar = false;
            this.Text = "USB Info";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TreeView treeDevices;
        private System.Windows.Forms.TreeView treeEvents;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TreeView treeErrors;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem mbtnRefreshDevices;
        private System.Windows.Forms.ToolStripMenuItem mbtnClearEventsErrors;
    }
}