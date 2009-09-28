namespace AVRProjectIDE
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.txtMessages = new System.Windows.Forms.TextBox();
            this.listErrorsWarnings = new System.Windows.Forms.ListView();
            this.colFile = new System.Windows.Forms.ColumnHeader();
            this.colLine = new System.Windows.Forms.ColumnHeader();
            this.colLocation = new System.Windows.Forms.ColumnHeader();
            this.colType = new System.Windows.Forms.ColumnHeader();
            this.colMessage = new System.Windows.Forms.ColumnHeader();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(814, 318);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.Transparent;
            this.tabPage1.Controls.Add(this.txtMessages);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(806, 292);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Messages";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.listErrorsWarnings);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(806, 292);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Errors & Warnings";
            this.tabPage2.UseVisualStyleBackColor = true;
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
            this.txtMessages.Size = new System.Drawing.Size(800, 286);
            this.txtMessages.TabIndex = 0;
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
            this.listErrorsWarnings.Size = new System.Drawing.Size(800, 286);
            this.listErrorsWarnings.TabIndex = 0;
            this.listErrorsWarnings.UseCompatibleStateImageBehavior = false;
            this.listErrorsWarnings.View = System.Windows.Forms.View.Details;
            this.listErrorsWarnings.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listErrorsWarnings_MouseDoubleClick);
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
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "MessagePanel";
            this.ShowInTaskbar = false;
            this.TabText = "Output";
            this.Text = "Output";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TextBox txtMessages;
        private System.Windows.Forms.ListView listErrorsWarnings;
        private System.Windows.Forms.ColumnHeader colFile;
        private System.Windows.Forms.ColumnHeader colLine;
        private System.Windows.Forms.ColumnHeader colLocation;
        private System.Windows.Forms.ColumnHeader colType;
        private System.Windows.Forms.ColumnHeader colMessage;
    }
}