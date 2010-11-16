namespace AVRProjectIDE
{
    partial class AboutBox
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
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
            this.okButton = new System.Windows.Forms.Button();
            this.lnkButton = new System.Windows.Forms.Button();
            this.btnDonate = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.picAdBox = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.picAdBox)).BeginInit();
            this.SuspendLayout();
            // 
            // okButton
            // 
            this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.okButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.okButton.Location = new System.Drawing.Point(12, 358);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 20);
            this.okButton.TabIndex = 24;
            this.okButton.Text = "&OK";
            // 
            // lnkButton
            // 
            this.lnkButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lnkButton.Location = new System.Drawing.Point(93, 358);
            this.lnkButton.Name = "lnkButton";
            this.lnkButton.Size = new System.Drawing.Size(75, 20);
            this.lnkButton.TabIndex = 24;
            this.lnkButton.Text = "&Website";
            this.lnkButton.Click += new System.EventHandler(this.lnkButton_Click);
            // 
            // btnDonate
            // 
            this.btnDonate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnDonate.Location = new System.Drawing.Point(174, 358);
            this.btnDonate.Name = "btnDonate";
            this.btnDonate.Size = new System.Drawing.Size(75, 20);
            this.btnDonate.TabIndex = 24;
            this.btnDonate.Text = "&Donate";
            this.btnDonate.Click += new System.EventHandler(this.btnDonate_Click);
            // 
            // textBox1
            // 
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox1.Location = new System.Drawing.Point(12, 12);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox1.Size = new System.Drawing.Size(620, 209);
            this.textBox1.TabIndex = 25;
            // 
            // picAdBox
            // 
            this.picAdBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.picAdBox.BackColor = System.Drawing.Color.White;
            this.picAdBox.BackgroundImage = global::AVRProjectIDE.GraphicsResx.usnoobie_ad;
            this.picAdBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.picAdBox.Location = new System.Drawing.Point(12, 227);
            this.picAdBox.Name = "picAdBox";
            this.picAdBox.Size = new System.Drawing.Size(620, 125);
            this.picAdBox.TabIndex = 26;
            this.picAdBox.TabStop = false;
            this.picAdBox.Click += new System.EventHandler(this.picAdBox_Click);
            // 
            // AboutBox
            // 
            this.AcceptButton = this.okButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.okButton;
            this.ClientSize = new System.Drawing.Size(644, 390);
            this.Controls.Add(this.picAdBox);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.btnDonate);
            this.Controls.Add(this.lnkButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AboutBox";
            this.Padding = new System.Windows.Forms.Padding(9);
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "About AVR Project IDE";
            ((System.ComponentModel.ISupportInitialize)(this.picAdBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Button lnkButton;
        private System.Windows.Forms.Button btnDonate;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.PictureBox picAdBox;
    }
}
