namespace AVRProjectIDE
{
    partial class FuseCalculator
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtSuggestedFusebox = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtYourFusebox = new System.Windows.Forms.TextBox();
            this.tabAVRDUDE = new System.Windows.Forms.TabPage();
            this.tabControl1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabAVRDUDE);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(965, 437);
            this.tabControl1.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.txtSuggestedFusebox);
            this.groupBox1.Location = new System.Drawing.Point(12, 455);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(965, 47);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Suggested Fusebox";
            // 
            // txtSuggestedFusebox
            // 
            this.txtSuggestedFusebox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSuggestedFusebox.Location = new System.Drawing.Point(6, 19);
            this.txtSuggestedFusebox.Name = "txtSuggestedFusebox";
            this.txtSuggestedFusebox.ReadOnly = true;
            this.txtSuggestedFusebox.Size = new System.Drawing.Size(953, 20);
            this.txtSuggestedFusebox.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.txtYourFusebox);
            this.groupBox2.Location = new System.Drawing.Point(12, 508);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(965, 47);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Your Fusebox";
            // 
            // txtYourFusebox
            // 
            this.txtYourFusebox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtYourFusebox.Location = new System.Drawing.Point(6, 19);
            this.txtYourFusebox.Name = "txtYourFusebox";
            this.txtYourFusebox.Size = new System.Drawing.Size(953, 20);
            this.txtYourFusebox.TabIndex = 0;
            // 
            // tabAVRDUDE
            // 
            this.tabAVRDUDE.Location = new System.Drawing.Point(4, 22);
            this.tabAVRDUDE.Name = "tabAVRDUDE";
            this.tabAVRDUDE.Padding = new System.Windows.Forms.Padding(3);
            this.tabAVRDUDE.Size = new System.Drawing.Size(957, 411);
            this.tabAVRDUDE.TabIndex = 0;
            this.tabAVRDUDE.Text = "AVRDUDE Control";
            this.tabAVRDUDE.UseVisualStyleBackColor = true;
            // 
            // FuseCalculator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(989, 567);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.tabControl1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FuseCalculator";
            this.Text = "Fuse Calculator";
            this.Load += new System.EventHandler(this.FuseCalculator_Load);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FuseCalculator_FormClosed);
            this.tabControl1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtYourFusebox;
        public System.Windows.Forms.TextBox txtSuggestedFusebox;
        private System.Windows.Forms.TabPage tabAVRDUDE;

    }
}