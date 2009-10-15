namespace AVRProjectIDE
{
    partial class SettingsWindow
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
            this.groupBox9 = new System.Windows.Forms.GroupBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.txtArduinoLibs = new System.Windows.Forms.TextBox();
            this.btnFindArduinoLibs = new System.Windows.Forms.Button();
            this.txtArduinoCore = new System.Windows.Forms.TextBox();
            this.btnFindArduinoCore = new System.Windows.Forms.Button();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.txtFavoriteDir = new System.Windows.Forms.TextBox();
            this.btnFavoriteBrowse = new System.Windows.Forms.Button();
            this.btnOpenAppData = new System.Windows.Forms.Button();
            this.chkAutocomplete = new System.Windows.Forms.CheckBox();
            this.groupBox9.SuspendLayout();
            this.groupBox8.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox9
            // 
            this.groupBox9.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox9.Controls.Add(this.label13);
            this.groupBox9.Controls.Add(this.label12);
            this.groupBox9.Controls.Add(this.txtArduinoLibs);
            this.groupBox9.Controls.Add(this.btnFindArduinoLibs);
            this.groupBox9.Controls.Add(this.txtArduinoCore);
            this.groupBox9.Controls.Add(this.btnFindArduinoCore);
            this.groupBox9.Location = new System.Drawing.Point(12, 64);
            this.groupBox9.Name = "groupBox9";
            this.groupBox9.Size = new System.Drawing.Size(627, 74);
            this.groupBox9.TabIndex = 4;
            this.groupBox9.TabStop = false;
            this.groupBox9.Text = "Arduino Related Folders";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(6, 48);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(65, 13);
            this.label13.TabIndex = 3;
            this.label13.Text = "Library Files:";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(15, 22);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(56, 13);
            this.label12.TabIndex = 3;
            this.label12.Text = "Core Files:";
            // 
            // txtArduinoLibs
            // 
            this.txtArduinoLibs.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtArduinoLibs.Location = new System.Drawing.Point(77, 45);
            this.txtArduinoLibs.Name = "txtArduinoLibs";
            this.txtArduinoLibs.ReadOnly = true;
            this.txtArduinoLibs.Size = new System.Drawing.Size(463, 20);
            this.txtArduinoLibs.TabIndex = 1;
            // 
            // btnFindArduinoLibs
            // 
            this.btnFindArduinoLibs.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFindArduinoLibs.Location = new System.Drawing.Point(546, 43);
            this.btnFindArduinoLibs.Name = "btnFindArduinoLibs";
            this.btnFindArduinoLibs.Size = new System.Drawing.Size(75, 23);
            this.btnFindArduinoLibs.TabIndex = 0;
            this.btnFindArduinoLibs.Text = "Find";
            this.btnFindArduinoLibs.UseVisualStyleBackColor = true;
            this.btnFindArduinoLibs.Click += new System.EventHandler(this.btnFindArduinoLibs_Click);
            // 
            // txtArduinoCore
            // 
            this.txtArduinoCore.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtArduinoCore.Location = new System.Drawing.Point(77, 19);
            this.txtArduinoCore.Name = "txtArduinoCore";
            this.txtArduinoCore.ReadOnly = true;
            this.txtArduinoCore.Size = new System.Drawing.Size(463, 20);
            this.txtArduinoCore.TabIndex = 1;
            // 
            // btnFindArduinoCore
            // 
            this.btnFindArduinoCore.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFindArduinoCore.Location = new System.Drawing.Point(546, 17);
            this.btnFindArduinoCore.Name = "btnFindArduinoCore";
            this.btnFindArduinoCore.Size = new System.Drawing.Size(75, 23);
            this.btnFindArduinoCore.TabIndex = 0;
            this.btnFindArduinoCore.Text = "Find";
            this.btnFindArduinoCore.UseVisualStyleBackColor = true;
            this.btnFindArduinoCore.Click += new System.EventHandler(this.btnFindArduinoCore_Click);
            // 
            // groupBox8
            // 
            this.groupBox8.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox8.Controls.Add(this.txtFavoriteDir);
            this.groupBox8.Controls.Add(this.btnFavoriteBrowse);
            this.groupBox8.Location = new System.Drawing.Point(12, 12);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Size = new System.Drawing.Size(627, 46);
            this.groupBox8.TabIndex = 3;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "Favorite Project Folder";
            // 
            // txtFavoriteDir
            // 
            this.txtFavoriteDir.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFavoriteDir.Location = new System.Drawing.Point(6, 19);
            this.txtFavoriteDir.Name = "txtFavoriteDir";
            this.txtFavoriteDir.ReadOnly = true;
            this.txtFavoriteDir.Size = new System.Drawing.Size(534, 20);
            this.txtFavoriteDir.TabIndex = 1;
            // 
            // btnFavoriteBrowse
            // 
            this.btnFavoriteBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFavoriteBrowse.Location = new System.Drawing.Point(546, 17);
            this.btnFavoriteBrowse.Name = "btnFavoriteBrowse";
            this.btnFavoriteBrowse.Size = new System.Drawing.Size(75, 23);
            this.btnFavoriteBrowse.TabIndex = 0;
            this.btnFavoriteBrowse.Text = "Find";
            this.btnFavoriteBrowse.UseVisualStyleBackColor = true;
            this.btnFavoriteBrowse.Click += new System.EventHandler(this.btnFavoriteBrowse_Click);
            // 
            // btnOpenAppData
            // 
            this.btnOpenAppData.Location = new System.Drawing.Point(12, 144);
            this.btnOpenAppData.Name = "btnOpenAppData";
            this.btnOpenAppData.Size = new System.Drawing.Size(130, 23);
            this.btnOpenAppData.TabIndex = 5;
            this.btnOpenAppData.Text = "Visit AppData Folder";
            this.btnOpenAppData.UseVisualStyleBackColor = true;
            this.btnOpenAppData.Click += new System.EventHandler(this.btnGotoAppdata_Click);
            // 
            // chkAutocomplete
            // 
            this.chkAutocomplete.AutoSize = true;
            this.chkAutocomplete.Location = new System.Drawing.Point(197, 148);
            this.chkAutocomplete.Name = "chkAutocomplete";
            this.chkAutocomplete.Size = new System.Drawing.Size(328, 17);
            this.chkAutocomplete.TabIndex = 6;
            this.chkAutocomplete.Text = "Enable Autocomplete (change takes affect on re-opening editor)";
            this.chkAutocomplete.UseVisualStyleBackColor = true;
            this.chkAutocomplete.CheckedChanged += new System.EventHandler(this.chkAutocomplete_CheckedChanged);
            // 
            // SettingsWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(649, 176);
            this.Controls.Add(this.chkAutocomplete);
            this.Controls.Add(this.btnOpenAppData);
            this.Controls.Add(this.groupBox9);
            this.Controls.Add(this.groupBox8);
            this.Name = "SettingsWindow";
            this.Text = "Editor Settings";
            this.groupBox9.ResumeLayout(false);
            this.groupBox9.PerformLayout();
            this.groupBox8.ResumeLayout(false);
            this.groupBox8.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox9;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtArduinoLibs;
        private System.Windows.Forms.Button btnFindArduinoLibs;
        private System.Windows.Forms.TextBox txtArduinoCore;
        private System.Windows.Forms.Button btnFindArduinoCore;
        private System.Windows.Forms.GroupBox groupBox8;
        private System.Windows.Forms.TextBox txtFavoriteDir;
        private System.Windows.Forms.Button btnFavoriteBrowse;
        private System.Windows.Forms.Button btnOpenAppData;
        private System.Windows.Forms.CheckBox chkAutocomplete;
    }
}