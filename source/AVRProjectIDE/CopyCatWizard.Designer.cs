namespace AVRProjectIDE
{
    partial class CopyCatWizard
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
            this.btnClone = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtPath = new System.Windows.Forms.TextBox();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.radCopyAll = new System.Windows.Forms.RadioButton();
            this.radOnlyLocal = new System.Windows.Forms.RadioButton();
            this.chkOpenAfterClone = new System.Windows.Forms.CheckBox();
            this.chkCopyUnmanaged = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnClone
            // 
            this.btnClone.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClone.Location = new System.Drawing.Point(308, 167);
            this.btnClone.Name = "btnClone";
            this.btnClone.Size = new System.Drawing.Size(75, 23);
            this.btnClone.TabIndex = 3;
            this.btnClone.Text = "&Clone!";
            this.btnClone.UseVisualStyleBackColor = true;
            this.btnClone.Click += new System.EventHandler(this.btnClone_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Location = new System.Drawing.Point(389, 167);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "C&ancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.txtPath);
            this.groupBox1.Controls.Add(this.btnBrowse);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(452, 47);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Target Path";
            // 
            // txtPath
            // 
            this.txtPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPath.Location = new System.Drawing.Point(6, 21);
            this.txtPath.Name = "txtPath";
            this.txtPath.ReadOnly = true;
            this.txtPath.Size = new System.Drawing.Size(359, 20);
            this.txtPath.TabIndex = 0;
            // 
            // btnBrowse
            // 
            this.btnBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBrowse.Location = new System.Drawing.Point(371, 19);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(75, 23);
            this.btnBrowse.TabIndex = 1;
            this.btnBrowse.Text = "Browse";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.chkCopyUnmanaged);
            this.groupBox2.Controls.Add(this.radCopyAll);
            this.groupBox2.Controls.Add(this.radOnlyLocal);
            this.groupBox2.Location = new System.Drawing.Point(12, 65);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(452, 96);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Files to Copy";
            // 
            // radCopyAll
            // 
            this.radCopyAll.AutoSize = true;
            this.radCopyAll.Location = new System.Drawing.Point(6, 42);
            this.radCopyAll.Name = "radCopyAll";
            this.radCopyAll.Size = new System.Drawing.Size(328, 17);
            this.radCopyAll.TabIndex = 1;
            this.radCopyAll.Text = "Copy all files, files outside the project folder will be copied as well";
            this.radCopyAll.UseVisualStyleBackColor = true;
            // 
            // radOnlyLocal
            // 
            this.radOnlyLocal.AutoSize = true;
            this.radOnlyLocal.Checked = true;
            this.radOnlyLocal.Location = new System.Drawing.Point(6, 19);
            this.radOnlyLocal.Name = "radOnlyLocal";
            this.radOnlyLocal.Size = new System.Drawing.Size(214, 17);
            this.radOnlyLocal.TabIndex = 0;
            this.radOnlyLocal.TabStop = true;
            this.radOnlyLocal.Text = "Copy only managed files in project folder";
            this.radOnlyLocal.UseVisualStyleBackColor = true;
            // 
            // chkOpenAfterClone
            // 
            this.chkOpenAfterClone.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chkOpenAfterClone.AutoSize = true;
            this.chkOpenAfterClone.Location = new System.Drawing.Point(12, 173);
            this.chkOpenAfterClone.Name = "chkOpenAfterClone";
            this.chkOpenAfterClone.Size = new System.Drawing.Size(153, 17);
            this.chkOpenAfterClone.TabIndex = 2;
            this.chkOpenAfterClone.Text = "Open Folder After Cloning?";
            this.chkOpenAfterClone.UseVisualStyleBackColor = true;
            // 
            // chkCopyUnmanaged
            // 
            this.chkCopyUnmanaged.AutoSize = true;
            this.chkCopyUnmanaged.Location = new System.Drawing.Point(6, 65);
            this.chkCopyUnmanaged.Name = "chkCopyUnmanaged";
            this.chkCopyUnmanaged.Size = new System.Drawing.Size(136, 17);
            this.chkCopyUnmanaged.TabIndex = 2;
            this.chkCopyUnmanaged.Text = "Copy unmanaged files?";
            this.chkCopyUnmanaged.UseVisualStyleBackColor = true;
            // 
            // CopyCatWizard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(476, 202);
            this.Controls.Add(this.chkOpenAfterClone);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnClone);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "CopyCatWizard";
            this.Text = "Clone Project";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnClone;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.TextBox txtPath;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton radCopyAll;
        private System.Windows.Forms.RadioButton radOnlyLocal;
        private System.Windows.Forms.CheckBox chkOpenAfterClone;
        private System.Windows.Forms.CheckBox chkCopyUnmanaged;
    }
}