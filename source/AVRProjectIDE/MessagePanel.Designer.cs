﻿namespace AVRProjectIDE
{
    partial class MessagePanel
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MessagePanel));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.txtMessages = new System.Windows.Forms.TextBox();
            this.tabListViews = new System.Windows.Forms.TabPage();
            this.listErrorsWarnings = new System.Windows.Forms.ListView();
            this.listErrorsOnly = new System.Windows.Forms.ListView();
            this.colFile = new System.Windows.Forms.ColumnHeader();
            this.colLine = new System.Windows.Forms.ColumnHeader();
            this.colLocation = new System.Windows.Forms.ColumnHeader();
            this.colType = new System.Windows.Forms.ColumnHeader();
            this.colMessage = new System.Windows.Forms.ColumnHeader();
            this.colFile2 = new System.Windows.Forms.ColumnHeader();
            this.colLine2 = new System.Windows.Forms.ColumnHeader();
            this.colLocation2 = new System.Windows.Forms.ColumnHeader();
            this.colType2 = new System.Windows.Forms.ColumnHeader();
            this.colMessage2 = new System.Windows.Forms.ColumnHeader();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabListViews.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabListViews);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.ImageList = this.imageList1;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(814, 318);
            this.tabControl1.TabIndex = 0;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.Transparent;
            this.tabPage1.Controls.Add(this.txtMessages);
            this.tabPage1.ImageKey = "message.png";
            this.tabPage1.Location = new System.Drawing.Point(4, 23);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(806, 291);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Messages";
            // 
            // txtMessages
            // 
            this.txtMessages.BackColor = System.Drawing.Color.Black;
            this.txtMessages.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtMessages.ForeColor = System.Drawing.Color.White;
            this.txtMessages.Location = new System.Drawing.Point(3, 3);
            this.txtMessages.Multiline = true;
            this.txtMessages.Name = "txtMessages";
            this.txtMessages.ReadOnly = true;
            this.txtMessages.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtMessages.Size = new System.Drawing.Size(800, 285);
            this.txtMessages.TabIndex = 0;
            // 
            // tabPage2
            // 
            this.tabListViews.Controls.Add(this.listErrorsWarnings);
            this.tabListViews.Controls.Add(this.listErrorsOnly);
            this.tabListViews.ImageKey = "warning.ico";
            this.tabListViews.Location = new System.Drawing.Point(4, 23);
            this.tabListViews.Name = "tabPage2";
            this.tabListViews.Padding = new System.Windows.Forms.Padding(3);
            this.tabListViews.Size = new System.Drawing.Size(806, 291);
            this.tabListViews.TabIndex = 1;
            this.tabListViews.Text = "Errors & Warnings";
            this.tabListViews.UseVisualStyleBackColor = true;
            // 
            // listErrorsWarnings
            // 
            this.listErrorsWarnings.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colFile,
            this.colLine,
            this.colLocation,
            this.colType,
            this.colMessage});
            this.listErrorsWarnings.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listErrorsWarnings.FullRowSelect = true;
            this.listErrorsWarnings.GridLines = true;
            this.listErrorsWarnings.HideSelection = false;
            this.listErrorsWarnings.Location = new System.Drawing.Point(3, 3);
            this.listErrorsWarnings.MultiSelect = false;
            this.listErrorsWarnings.Name = "listErrorsWarnings";
            this.listErrorsWarnings.Size = new System.Drawing.Size(800, 285);
            this.listErrorsWarnings.TabIndex = 0;
            this.listErrorsWarnings.UseCompatibleStateImageBehavior = false;
            this.listErrorsWarnings.View = System.Windows.Forms.View.Details;
            this.listErrorsWarnings.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listErrorsWarnings_MouseDoubleClick);
            // 
            // listErrorsOnly
            // 
            this.listErrorsOnly.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colFile2,
            this.colLine2,
            this.colLocation2,
            this.colType2,
            this.colMessage2});
            this.listErrorsOnly.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listErrorsOnly.FullRowSelect = true;
            this.listErrorsOnly.GridLines = true;
            this.listErrorsOnly.HideSelection = false;
            this.listErrorsOnly.Location = new System.Drawing.Point(3, 3);
            this.listErrorsOnly.MultiSelect = false;
            this.listErrorsOnly.Name = "listErrorsOnly";
            this.listErrorsOnly.Size = new System.Drawing.Size(800, 285);
            this.listErrorsOnly.TabIndex = 0;
            this.listErrorsOnly.UseCompatibleStateImageBehavior = false;
            this.listErrorsOnly.View = System.Windows.Forms.View.Details;
            this.listErrorsOnly.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listErrorsOnly_MouseDoubleClick);
            // 
            // colFile
            // 
            this.colFile.Text = "File";
            // 
            // colLine
            // 
            this.colLine.Text = "Line";
            // 
            // colLocation
            // 
            this.colLocation.Text = "Where";
            // 
            // colType
            // 
            this.colType.Text = "Type";
            // 
            // colMessage
            // 
            this.colMessage.Text = "Message";
            this.colMessage.Width = 400;
            // 
            // colFile2
            // 
            this.colFile2.Text = "File";
            // 
            // colLine2
            // 
            this.colLine2.Text = "Line";
            // 
            // colLocation2
            // 
            this.colLocation2.Text = "Where";
            // 
            // colType2
            // 
            this.colType2.Text = "Type";
            // 
            // colMessage2
            // 
            this.colMessage2.Text = "Message";
            this.colMessage2.Width = 400;
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "message.png");
            this.imageList1.Images.SetKeyName(1, "warning.ico");
            // 
            // tabPage3
            // 
            this.tabPage3.Location = new System.Drawing.Point(4, 23);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(806, 291);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Toggle Warnings";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // MessagePanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(814, 318);
            this.CloseButton = false;
            this.CloseButtonVisible = false;
            this.Controls.Add(this.tabControl1);
            this.DockAreas = ((WeifenLuo.WinFormsUI.Docking.DockAreas)(((WeifenLuo.WinFormsUI.Docking.DockAreas.Float | WeifenLuo.WinFormsUI.Docking.DockAreas.DockTop)
                        | WeifenLuo.WinFormsUI.Docking.DockAreas.DockBottom)));
            this.Font = new System.Drawing.Font(System.Drawing.FontFamily.GenericSansSerif, 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "MessagePanel";
            this.ShowInTaskbar = false;
            this.TabText = "Output";
            this.Text = "Output";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabListViews.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabListViews;
        private System.Windows.Forms.TextBox txtMessages;
        private System.Windows.Forms.ListView listErrorsWarnings;
        private System.Windows.Forms.ListView listErrorsOnly;
        private System.Windows.Forms.ColumnHeader colFile;
        private System.Windows.Forms.ColumnHeader colLine;
        private System.Windows.Forms.ColumnHeader colLocation;
        private System.Windows.Forms.ColumnHeader colType;
        private System.Windows.Forms.ColumnHeader colMessage;
        private System.Windows.Forms.ColumnHeader colFile2;
        private System.Windows.Forms.ColumnHeader colLine2;
        private System.Windows.Forms.ColumnHeader colLocation2;
        private System.Windows.Forms.ColumnHeader colType2;
        private System.Windows.Forms.ColumnHeader colMessage2;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.TabPage tabPage3;
    }
}