namespace AVRProjectIDE
{
    partial class SearchPanel
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
            this.chkEscape = new System.Windows.Forms.CheckBox();
            this.chkMatchCase = new System.Windows.Forms.CheckBox();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.chkWordStart = new System.Windows.Forms.CheckBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.chkWholeWord = new System.Windows.Forms.CheckBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.listSearchResults = new System.Windows.Forms.ListView();
            this.listColResPreview = new System.Windows.Forms.ColumnHeader();
            this.listColResFileName = new System.Windows.Forms.ColumnHeader();
            this.listColResLineNum = new System.Windows.Forms.ColumnHeader();
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
            this.tabPage1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.tabPage1.Controls.Add(this.chkEscape);
            this.tabPage1.Controls.Add(this.chkMatchCase);
            this.tabPage1.Controls.Add(this.txtSearch);
            this.tabPage1.Controls.Add(this.chkWordStart);
            this.tabPage1.Controls.Add(this.btnSearch);
            this.tabPage1.Controls.Add(this.chkWholeWord);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(806, 292);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Search";
            // 
            // chkEscape
            // 
            this.chkEscape.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkEscape.AutoSize = true;
            this.chkEscape.Location = new System.Drawing.Point(552, 33);
            this.chkEscape.Name = "chkEscape";
            this.chkEscape.Size = new System.Drawing.Size(246, 17);
            this.chkEscape.TabIndex = 6;
            this.chkEscape.Text = "Use Escape Sequence ( \\\\ \\t \\r \\n Supported)";
            this.chkEscape.UseVisualStyleBackColor = true;
            // 
            // chkMatchCase
            // 
            this.chkMatchCase.AutoSize = true;
            this.chkMatchCase.Location = new System.Drawing.Point(114, 32);
            this.chkMatchCase.Name = "chkMatchCase";
            this.chkMatchCase.Size = new System.Drawing.Size(83, 17);
            this.chkMatchCase.TabIndex = 4;
            this.chkMatchCase.Text = "Match Case";
            this.chkMatchCase.UseVisualStyleBackColor = true;
            // 
            // txtSearch
            // 
            this.txtSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSearch.Location = new System.Drawing.Point(8, 6);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(709, 20);
            this.txtSearch.TabIndex = 1;
            this.txtSearch.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtSearch_KeyPress);
            // 
            // chkWordStart
            // 
            this.chkWordStart.AutoSize = true;
            this.chkWordStart.Location = new System.Drawing.Point(230, 33);
            this.chkWordStart.Name = "chkWordStart";
            this.chkWordStart.Size = new System.Drawing.Size(77, 17);
            this.chkWordStart.TabIndex = 5;
            this.chkWordStart.Text = "Word Start";
            this.chkWordStart.UseVisualStyleBackColor = true;
            // 
            // btnSearch
            // 
            this.btnSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSearch.Location = new System.Drawing.Point(723, 4);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 23);
            this.btnSearch.TabIndex = 2;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // chkWholeWord
            // 
            this.chkWholeWord.AutoSize = true;
            this.chkWholeWord.Location = new System.Drawing.Point(8, 32);
            this.chkWholeWord.Name = "chkWholeWord";
            this.chkWholeWord.Size = new System.Drawing.Size(86, 17);
            this.chkWholeWord.TabIndex = 3;
            this.chkWholeWord.Text = "Whole Word";
            this.chkWholeWord.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.listSearchResults);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(806, 292);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Results";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // listSearchResults
            // 
            this.listSearchResults.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.listColResPreview,
            this.listColResFileName,
            this.listColResLineNum});
            this.listSearchResults.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listSearchResults.FullRowSelect = true;
            this.listSearchResults.GridLines = true;
            this.listSearchResults.HideSelection = false;
            this.listSearchResults.Location = new System.Drawing.Point(3, 3);
            this.listSearchResults.MultiSelect = false;
            this.listSearchResults.Name = "listSearchResults";
            this.listSearchResults.ShowItemToolTips = true;
            this.listSearchResults.Size = new System.Drawing.Size(800, 286);
            this.listSearchResults.TabIndex = 7;
            this.listSearchResults.UseCompatibleStateImageBehavior = false;
            this.listSearchResults.View = System.Windows.Forms.View.Details;
            this.listSearchResults.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listSearchResults_MouseDoubleClick);
            // 
            // listColResPreview
            // 
            this.listColResPreview.Text = "Preview";
            this.listColResPreview.Width = 596;
            // 
            // listColResFileName
            // 
            this.listColResFileName.Text = "File";
            // 
            // listColResLineNum
            // 
            this.listColResLineNum.Text = "Line";
            // 
            // SearchPanel
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
            this.Name = "SearchPanel";
            this.ShowInTaskbar = false;
            this.TabText = "Search in Open Files";
            this.Text = "Search In Open Files";
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
        private System.Windows.Forms.ListView listSearchResults;
        private System.Windows.Forms.ColumnHeader listColResPreview;
        private System.Windows.Forms.ColumnHeader listColResFileName;
        private System.Windows.Forms.ColumnHeader listColResLineNum;
        private System.Windows.Forms.CheckBox chkEscape;
        private System.Windows.Forms.CheckBox chkMatchCase;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.CheckBox chkWordStart;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.CheckBox chkWholeWord;
    }
}