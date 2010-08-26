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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.treeDevices = new System.Windows.Forms.TreeView();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.treeEvents = new System.Windows.Forms.TreeView();
            this.btnEventsClear = new System.Windows.Forms.Button();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.treeErrors = new System.Windows.Forms.TreeView();
            this.btnClearErrors = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
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
            this.tabPage1.Controls.Add(this.btnRefresh);
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
            this.treeDevices.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.treeDevices.Location = new System.Drawing.Point(6, 6);
            this.treeDevices.Name = "treeDevices";
            this.treeDevices.Size = new System.Drawing.Size(482, 224);
            this.treeDevices.TabIndex = 1;
            // 
            // btnRefresh
            // 
            this.btnRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRefresh.Location = new System.Drawing.Point(494, 6);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(71, 224);
            this.btnRefresh.TabIndex = 0;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.treeEvents);
            this.tabPage2.Controls.Add(this.btnEventsClear);
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
            this.treeEvents.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.treeEvents.Location = new System.Drawing.Point(5, 6);
            this.treeEvents.Name = "treeEvents";
            this.treeEvents.Size = new System.Drawing.Size(492, 224);
            this.treeEvents.TabIndex = 3;
            // 
            // btnEventsClear
            // 
            this.btnEventsClear.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEventsClear.Location = new System.Drawing.Point(503, 6);
            this.btnEventsClear.Name = "btnEventsClear";
            this.btnEventsClear.Size = new System.Drawing.Size(61, 222);
            this.btnEventsClear.TabIndex = 2;
            this.btnEventsClear.Text = "Clear";
            this.btnEventsClear.UseVisualStyleBackColor = true;
            this.btnEventsClear.Click += new System.EventHandler(this.btnEventsClear_Click);
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.treeErrors);
            this.tabPage3.Controls.Add(this.btnClearErrors);
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
            this.treeErrors.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.treeErrors.Location = new System.Drawing.Point(5, 6);
            this.treeErrors.Name = "treeErrors";
            this.treeErrors.Size = new System.Drawing.Size(492, 224);
            this.treeErrors.TabIndex = 5;
            // 
            // btnClearErrors
            // 
            this.btnClearErrors.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClearErrors.Location = new System.Drawing.Point(503, 6);
            this.btnClearErrors.Name = "btnClearErrors";
            this.btnClearErrors.Size = new System.Drawing.Size(61, 222);
            this.btnClearErrors.TabIndex = 4;
            this.btnClearErrors.Text = "Clear";
            this.btnClearErrors.UseVisualStyleBackColor = true;
            this.btnClearErrors.Click += new System.EventHandler(this.btnClearErrors_Click);
            // 
            // UsbPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(576, 262);
            this.Controls.Add(this.tabControl1);
            this.DockAreas = ((WeifenLuo.WinFormsUI.Docking.DockAreas)(((((WeifenLuo.WinFormsUI.Docking.DockAreas.Float | WeifenLuo.WinFormsUI.Docking.DockAreas.DockLeft)
                        | WeifenLuo.WinFormsUI.Docking.DockAreas.DockRight)
                        | WeifenLuo.WinFormsUI.Docking.DockAreas.DockTop)
                        | WeifenLuo.WinFormsUI.Docking.DockAreas.DockBottom)));
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "UsbPanel";
            this.ShowInTaskbar = false;
            this.Text = "USB Info";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.TreeView treeDevices;
        private System.Windows.Forms.TreeView treeEvents;
        private System.Windows.Forms.Button btnEventsClear;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TreeView treeErrors;
        private System.Windows.Forms.Button btnClearErrors;
    }
}