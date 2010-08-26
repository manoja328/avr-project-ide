namespace AVRProjectIDE
{
    partial class DisassemblyViewer
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
            this.scintilla1 = new ScintillaNet.Scintilla();
            this.btnLoad = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtOptions = new System.Windows.Forms.TextBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mbtnSelectAll = new System.Windows.Forms.ToolStripMenuItem();
            this.mbtnCopy = new System.Windows.Forms.ToolStripMenuItem();
            this.mbtnFind = new System.Windows.Forms.ToolStripMenuItem();
            this.mbtnInvisible = new System.Windows.Forms.ToolStripMenuItem();
            this.mbtnReplace = new System.Windows.Forms.ToolStripMenuItem();
            this.btnHelp = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.scintilla1)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // scintilla1
            // 
            this.scintilla1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.scintilla1.ContextMenuStrip = this.contextMenuStrip1;
            this.scintilla1.IsReadOnly = true;
            this.scintilla1.Location = new System.Drawing.Point(1, 25);
            this.scintilla1.Name = "scintilla1";
            this.scintilla1.Scrolling.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.scintilla1.Size = new System.Drawing.Size(441, 238);
            this.scintilla1.Styles.BraceBad.FontName = "Verdana";
            this.scintilla1.Styles.BraceLight.FontName = "Verdana";
            this.scintilla1.Styles.ControlChar.FontName = "Verdana";
            this.scintilla1.Styles.Default.FontName = "Verdana";
            this.scintilla1.Styles.IndentGuide.FontName = "Verdana";
            this.scintilla1.Styles.LastPredefined.FontName = "Verdana";
            this.scintilla1.Styles.LineNumber.FontName = "Verdana";
            this.scintilla1.Styles.Max.FontName = "Verdana";
            this.scintilla1.TabIndex = 2;
            // 
            // btnLoad
            // 
            this.btnLoad.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLoad.Location = new System.Drawing.Point(387, 1);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(55, 23);
            this.btnLoad.TabIndex = 1;
            this.btnLoad.Text = "Load";
            this.btnLoad.UseVisualStyleBackColor = true;
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(1, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(105, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "avr-objdump options:";
            // 
            // txtOptions
            // 
            this.txtOptions.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtOptions.Location = new System.Drawing.Point(112, 2);
            this.txtOptions.Name = "txtOptions";
            this.txtOptions.Size = new System.Drawing.Size(235, 20);
            this.txtOptions.TabIndex = 0;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mbtnSelectAll,
            this.mbtnCopy,
            this.mbtnFind,
            this.mbtnInvisible});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.ShowImageMargin = false;
            this.contextMenuStrip1.Size = new System.Drawing.Size(140, 92);
            // 
            // mbtnSelectAll
            // 
            this.mbtnSelectAll.Name = "mbtnSelectAll";
            this.mbtnSelectAll.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.A)));
            this.mbtnSelectAll.Size = new System.Drawing.Size(139, 22);
            this.mbtnSelectAll.Text = "Select &All";
            this.mbtnSelectAll.Click += new System.EventHandler(this.mbtnSelectAll_Click);
            // 
            // mbtnCopy
            // 
            this.mbtnCopy.Name = "mbtnCopy";
            this.mbtnCopy.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
            this.mbtnCopy.Size = new System.Drawing.Size(139, 22);
            this.mbtnCopy.Text = "&Copy";
            this.mbtnCopy.Click += new System.EventHandler(this.mbtnCopy_Click);
            // 
            // mbtnFind
            // 
            this.mbtnFind.Name = "mbtnFind";
            this.mbtnFind.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F)));
            this.mbtnFind.Size = new System.Drawing.Size(139, 22);
            this.mbtnFind.Text = "&Find";
            this.mbtnFind.Click += new System.EventHandler(this.mbtnFind_Click);
            // 
            // mbtnInvisible
            // 
            this.mbtnInvisible.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mbtnReplace});
            this.mbtnInvisible.Name = "mbtnInvisible";
            this.mbtnInvisible.Size = new System.Drawing.Size(139, 22);
            this.mbtnInvisible.Text = "Invisible";
            // 
            // mbtnReplace
            // 
            this.mbtnReplace.Name = "mbtnReplace";
            this.mbtnReplace.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.H)));
            this.mbtnReplace.Size = new System.Drawing.Size(158, 22);
            this.mbtnReplace.Text = "Replace";
            this.mbtnReplace.Click += new System.EventHandler(this.mbtnReplace_Click);
            // 
            // btnHelp
            // 
            this.btnHelp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnHelp.Location = new System.Drawing.Point(353, 2);
            this.btnHelp.Name = "btnHelp";
            this.btnHelp.Size = new System.Drawing.Size(28, 22);
            this.btnHelp.TabIndex = 4;
            this.btnHelp.Text = "?";
            this.btnHelp.UseVisualStyleBackColor = true;
            this.btnHelp.Click += new System.EventHandler(this.btnHelp_Click);
            // 
            // DisassemblyViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(444, 262);
            this.CloseButton = false;
            this.CloseButtonVisible = false;
            this.Controls.Add(this.txtOptions);
            this.Controls.Add(this.btnHelp);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnLoad);
            this.Controls.Add(this.scintilla1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "DisassemblyViewer";
            this.ShowInTaskbar = false;
            this.Text = "Disassembly";
            ((System.ComponentModel.ISupportInitialize)(this.scintilla1)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ScintillaNet.Scintilla scintilla1;
        private System.Windows.Forms.Button btnLoad;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtOptions;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem mbtnSelectAll;
        private System.Windows.Forms.ToolStripMenuItem mbtnCopy;
        private System.Windows.Forms.ToolStripMenuItem mbtnFind;
        private System.Windows.Forms.ToolStripMenuItem mbtnInvisible;
        private System.Windows.Forms.ToolStripMenuItem mbtnReplace;
        private System.Windows.Forms.Button btnHelp;
    }
}