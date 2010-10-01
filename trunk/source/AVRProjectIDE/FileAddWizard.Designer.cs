namespace AVRProjectIDE
{
    partial class FileAddWizard
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
            this.txtFileName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dropFileExt = new System.Windows.Forms.ComboBox();
            this.lblFileExtensions = new System.Windows.Forms.Label();
            this.txtDirLoc = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnFind = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.dropTemplates = new System.Windows.Forms.ComboBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnCreate = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtFileName
            // 
            this.txtFileName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFileName.Location = new System.Drawing.Point(114, 12);
            this.txtFileName.Name = "txtFileName";
            this.txtFileName.Size = new System.Drawing.Size(277, 20);
            this.txtFileName.TabIndex = 0;
            this.txtFileName.TextChanged += new System.EventHandler(this.txtFileName_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(51, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "File Name:";
            // 
            // dropFileExt
            // 
            this.dropFileExt.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dropFileExt.FormattingEnabled = true;
            this.dropFileExt.Items.AddRange(new object[] {
            "c",
            "cpp",
            "S",
            "pde",
            "h",
            "hpp"});
            this.dropFileExt.Location = new System.Drawing.Point(114, 38);
            this.dropFileExt.Name = "dropFileExt";
            this.dropFileExt.Size = new System.Drawing.Size(277, 21);
            this.dropFileExt.TabIndex = 2;
            this.dropFileExt.SelectedIndexChanged += new System.EventHandler(this.dropFileExt_SelectedIndexChanged);
            // 
            // lblFileExtensions
            // 
            this.lblFileExtensions.AutoSize = true;
            this.lblFileExtensions.Location = new System.Drawing.Point(33, 41);
            this.lblFileExtensions.Name = "lblFileExtensions";
            this.lblFileExtensions.Size = new System.Drawing.Size(75, 13);
            this.lblFileExtensions.TabIndex = 1;
            this.lblFileExtensions.Text = "File Extension:";
            // 
            // txtDirLoc
            // 
            this.txtDirLoc.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDirLoc.Location = new System.Drawing.Point(114, 65);
            this.txtDirLoc.Name = "txtDirLoc";
            this.txtDirLoc.ReadOnly = true;
            this.txtDirLoc.Size = new System.Drawing.Size(185, 20);
            this.txtDirLoc.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 68);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(96, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "Directory Location:";
            // 
            // btnFind
            // 
            this.btnFind.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFind.Location = new System.Drawing.Point(305, 63);
            this.btnFind.Name = "btnFind";
            this.btnFind.Size = new System.Drawing.Size(86, 23);
            this.btnFind.TabIndex = 4;
            this.btnFind.Text = "&Find / Create";
            this.btnFind.UseVisualStyleBackColor = true;
            this.btnFind.Click += new System.EventHandler(this.btnFind_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 94);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(96, 13);
            this.label4.TabIndex = 1;
            this.label4.Text = "Creation Template:";
            // 
            // dropTemplates
            // 
            this.dropTemplates.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dropTemplates.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.dropTemplates.FormattingEnabled = true;
            this.dropTemplates.Location = new System.Drawing.Point(114, 91);
            this.dropTemplates.Name = "dropTemplates";
            this.dropTemplates.Size = new System.Drawing.Size(277, 21);
            this.dropTemplates.TabIndex = 5;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(316, 124);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 7;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnCreate
            // 
            this.btnCreate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCreate.Location = new System.Drawing.Point(235, 124);
            this.btnCreate.Name = "btnCreate";
            this.btnCreate.Size = new System.Drawing.Size(75, 23);
            this.btnCreate.TabIndex = 6;
            this.btnCreate.Text = "Cr&eate";
            this.btnCreate.UseVisualStyleBackColor = true;
            this.btnCreate.Click += new System.EventHandler(this.btnCreate_Click);
            // 
            // FileAddWizard
            // 
            this.AcceptButton = this.btnCreate;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(403, 159);
            this.Controls.Add(this.btnCreate);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnFind);
            this.Controls.Add(this.dropTemplates);
            this.Controls.Add(this.dropFileExt);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lblFileExtensions);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtDirLoc);
            this.Controls.Add(this.txtFileName);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FileAddWizard";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "File Add Wizard";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtFileName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox dropFileExt;
        private System.Windows.Forms.Label lblFileExtensions;
        private System.Windows.Forms.TextBox txtDirLoc;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnFind;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox dropTemplates;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnCreate;
    }
}