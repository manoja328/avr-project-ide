namespace AVRProjectIDE
{
    partial class AvrDudeWindow
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
            this.grpboxBurnerPanel = new System.Windows.Forms.GroupBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnVerify = new System.Windows.Forms.Button();
            this.btnRead = new System.Windows.Forms.Button();
            this.btnWriteNoVerify = new System.Windows.Forms.Button();
            this.btnWrite = new System.Windows.Forms.Button();
            this.dropDetectionType = new System.Windows.Forms.ComboBox();
            this.dropMemoryType = new System.Windows.Forms.ComboBox();
            this.btnFuseTool = new System.Windows.Forms.Button();
            this.btnInteractive = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpboxBurnerPanel
            // 
            this.grpboxBurnerPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.grpboxBurnerPanel.Location = new System.Drawing.Point(12, 12);
            this.grpboxBurnerPanel.Name = "grpboxBurnerPanel";
            this.grpboxBurnerPanel.Size = new System.Drawing.Size(697, 123);
            this.grpboxBurnerPanel.TabIndex = 0;
            this.grpboxBurnerPanel.TabStop = false;
            this.grpboxBurnerPanel.Text = "Options and Settings";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.btnVerify);
            this.groupBox1.Controls.Add(this.btnRead);
            this.groupBox1.Controls.Add(this.btnWriteNoVerify);
            this.groupBox1.Controls.Add(this.btnWrite);
            this.groupBox1.Controls.Add(this.dropDetectionType);
            this.groupBox1.Controls.Add(this.dropMemoryType);
            this.groupBox1.Location = new System.Drawing.Point(12, 141);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(549, 79);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Operations";
            // 
            // btnVerify
            // 
            this.btnVerify.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnVerify.Location = new System.Drawing.Point(480, 19);
            this.btnVerify.Name = "btnVerify";
            this.btnVerify.Size = new System.Drawing.Size(63, 48);
            this.btnVerify.TabIndex = 5;
            this.btnVerify.Text = "Verify With File";
            this.btnVerify.UseVisualStyleBackColor = true;
            this.btnVerify.Click += new System.EventHandler(this.btnVerify_Click);
            // 
            // btnRead
            // 
            this.btnRead.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRead.Location = new System.Drawing.Point(413, 19);
            this.btnRead.Name = "btnRead";
            this.btnRead.Size = new System.Drawing.Size(61, 48);
            this.btnRead.TabIndex = 4;
            this.btnRead.Text = "Dump File";
            this.btnRead.UseVisualStyleBackColor = true;
            this.btnRead.Click += new System.EventHandler(this.btnRead_Click);
            // 
            // btnWriteNoVerify
            // 
            this.btnWriteNoVerify.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnWriteNoVerify.Location = new System.Drawing.Point(282, 46);
            this.btnWriteNoVerify.Name = "btnWriteNoVerify";
            this.btnWriteNoVerify.Size = new System.Drawing.Size(125, 21);
            this.btnWriteNoVerify.TabIndex = 3;
            this.btnWriteNoVerify.Text = "Write File No Verify";
            this.btnWriteNoVerify.UseVisualStyleBackColor = true;
            this.btnWriteNoVerify.Click += new System.EventHandler(this.btnWriteNoVerify_Click);
            // 
            // btnWrite
            // 
            this.btnWrite.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnWrite.Location = new System.Drawing.Point(282, 19);
            this.btnWrite.Name = "btnWrite";
            this.btnWrite.Size = new System.Drawing.Size(125, 21);
            this.btnWrite.TabIndex = 2;
            this.btnWrite.Text = "Write File + Verify";
            this.btnWrite.UseVisualStyleBackColor = true;
            this.btnWrite.Click += new System.EventHandler(this.btnWrite_Click);
            // 
            // dropDetectionType
            // 
            this.dropDetectionType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dropDetectionType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.dropDetectionType.FormattingEnabled = true;
            this.dropDetectionType.Items.AddRange(new object[] {
            "Autodetect Format",
            "Intel Hex Format",
            "Motorola S-Record",
            "Raw Binary Format"});
            this.dropDetectionType.Location = new System.Drawing.Point(6, 46);
            this.dropDetectionType.Name = "dropDetectionType";
            this.dropDetectionType.Size = new System.Drawing.Size(270, 21);
            this.dropDetectionType.TabIndex = 1;
            // 
            // dropMemoryType
            // 
            this.dropMemoryType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dropMemoryType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.dropMemoryType.FormattingEnabled = true;
            this.dropMemoryType.Items.AddRange(new object[] {
            "Flash Memory",
            "EEPROM Memory"});
            this.dropMemoryType.Location = new System.Drawing.Point(6, 19);
            this.dropMemoryType.Name = "dropMemoryType";
            this.dropMemoryType.Size = new System.Drawing.Size(270, 21);
            this.dropMemoryType.TabIndex = 0;
            // 
            // btnFuseTool
            // 
            this.btnFuseTool.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFuseTool.Location = new System.Drawing.Point(567, 160);
            this.btnFuseTool.Name = "btnFuseTool";
            this.btnFuseTool.Size = new System.Drawing.Size(60, 48);
            this.btnFuseTool.TabIndex = 2;
            this.btnFuseTool.Text = "Use Fuse Tool";
            this.btnFuseTool.UseVisualStyleBackColor = true;
            this.btnFuseTool.Click += new System.EventHandler(this.btnFuseTool_Click);
            // 
            // btnInteractive
            // 
            this.btnInteractive.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnInteractive.Location = new System.Drawing.Point(633, 160);
            this.btnInteractive.Name = "btnInteractive";
            this.btnInteractive.Size = new System.Drawing.Size(76, 48);
            this.btnInteractive.TabIndex = 3;
            this.btnInteractive.Text = "Interactive Mode";
            this.btnInteractive.UseVisualStyleBackColor = true;
            this.btnInteractive.Click += new System.EventHandler(this.btnInteractive_Click);
            // 
            // AvrDudeWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(721, 232);
            this.Controls.Add(this.btnInteractive);
            this.Controls.Add(this.btnFuseTool);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.grpboxBurnerPanel);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AvrDudeWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "AVRDUDE Tool";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.AvrDudeWindow_FormClosed);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpboxBurnerPanel;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox dropDetectionType;
        private System.Windows.Forms.ComboBox dropMemoryType;
        private System.Windows.Forms.Button btnVerify;
        private System.Windows.Forms.Button btnRead;
        private System.Windows.Forms.Button btnWrite;
        private System.Windows.Forms.Button btnFuseTool;
        private System.Windows.Forms.Button btnWriteNoVerify;
        private System.Windows.Forms.Button btnInteractive;


    }
}