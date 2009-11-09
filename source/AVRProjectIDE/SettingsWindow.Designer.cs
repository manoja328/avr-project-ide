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
            this.lnkLibFiles = new System.Windows.Forms.LinkLabel();
            this.lnkCoreFiles = new System.Windows.Forms.LinkLabel();
            this.txtArduinoLibs = new System.Windows.Forms.TextBox();
            this.btnFindArduinoLibs = new System.Windows.Forms.Button();
            this.txtArduinoCore = new System.Windows.Forms.TextBox();
            this.btnFindArduinoCore = new System.Windows.Forms.Button();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.lnkFolder = new System.Windows.Forms.LinkLabel();
            this.txtFavoriteDir = new System.Windows.Forms.TextBox();
            this.btnFavoriteBrowse = new System.Windows.Forms.Button();
            this.btnOpenAppData = new System.Windows.Forms.Button();
            this.chkAutocomplete = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dropSmartIndent = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.numIndentWidth = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.numBackupInterval = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.numTabWidth = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.chkShowWS = new System.Windows.Forms.CheckBox();
            this.chkBackspaceUnindents = new System.Windows.Forms.CheckBox();
            this.chkTabIndents = new System.Windows.Forms.CheckBox();
            this.chkUseTabs = new System.Windows.Forms.CheckBox();
            this.chkWordWrap = new System.Windows.Forms.CheckBox();
            this.chkShowLineNum = new System.Windows.Forms.CheckBox();
            this.chkIndentGuide = new System.Windows.Forms.CheckBox();
            this.btnOpenInstallationFolder = new System.Windows.Forms.Button();
            this.chkCheckUpdates = new System.Windows.Forms.CheckBox();
            this.btnSaveAndClose = new System.Windows.Forms.Button();
            this.btnDiscardAndClose = new System.Windows.Forms.Button();
            this.radMsgOnTop = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.radMsgOnBottom = new System.Windows.Forms.RadioButton();
            this.chkReopenProject = new System.Windows.Forms.CheckBox();
            this.chkHighlightCurLine = new System.Windows.Forms.CheckBox();
            this.groupBox9.SuspendLayout();
            this.groupBox8.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numIndentWidth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numBackupInterval)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numTabWidth)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox9
            // 
            this.groupBox9.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox9.Controls.Add(this.lnkLibFiles);
            this.groupBox9.Controls.Add(this.lnkCoreFiles);
            this.groupBox9.Controls.Add(this.txtArduinoLibs);
            this.groupBox9.Controls.Add(this.btnFindArduinoLibs);
            this.groupBox9.Controls.Add(this.txtArduinoCore);
            this.groupBox9.Controls.Add(this.btnFindArduinoCore);
            this.groupBox9.Location = new System.Drawing.Point(12, 64);
            this.groupBox9.Name = "groupBox9";
            this.groupBox9.Size = new System.Drawing.Size(725, 74);
            this.groupBox9.TabIndex = 4;
            this.groupBox9.TabStop = false;
            this.groupBox9.Text = "Arduino Related Folders";
            // 
            // lnkLibFiles
            // 
            this.lnkLibFiles.AutoSize = true;
            this.lnkLibFiles.Location = new System.Drawing.Point(6, 48);
            this.lnkLibFiles.Name = "lnkLibFiles";
            this.lnkLibFiles.Size = new System.Drawing.Size(65, 13);
            this.lnkLibFiles.TabIndex = 5;
            this.lnkLibFiles.TabStop = true;
            this.lnkLibFiles.Text = "Library Files:";
            this.lnkLibFiles.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkLibFiles_LinkClicked);
            // 
            // lnkCoreFiles
            // 
            this.lnkCoreFiles.AutoSize = true;
            this.lnkCoreFiles.Location = new System.Drawing.Point(15, 22);
            this.lnkCoreFiles.Name = "lnkCoreFiles";
            this.lnkCoreFiles.Size = new System.Drawing.Size(56, 13);
            this.lnkCoreFiles.TabIndex = 3;
            this.lnkCoreFiles.TabStop = true;
            this.lnkCoreFiles.Text = "Core Files:";
            this.lnkCoreFiles.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkCoreFiles_LinkClicked);
            // 
            // txtArduinoLibs
            // 
            this.txtArduinoLibs.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtArduinoLibs.Location = new System.Drawing.Point(77, 45);
            this.txtArduinoLibs.Name = "txtArduinoLibs";
            this.txtArduinoLibs.ReadOnly = true;
            this.txtArduinoLibs.Size = new System.Drawing.Size(561, 20);
            this.txtArduinoLibs.TabIndex = 22;
            // 
            // btnFindArduinoLibs
            // 
            this.btnFindArduinoLibs.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFindArduinoLibs.Location = new System.Drawing.Point(644, 43);
            this.btnFindArduinoLibs.Name = "btnFindArduinoLibs";
            this.btnFindArduinoLibs.Size = new System.Drawing.Size(75, 23);
            this.btnFindArduinoLibs.TabIndex = 6;
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
            this.txtArduinoCore.Size = new System.Drawing.Size(561, 20);
            this.txtArduinoCore.TabIndex = 21;
            // 
            // btnFindArduinoCore
            // 
            this.btnFindArduinoCore.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFindArduinoCore.Location = new System.Drawing.Point(644, 17);
            this.btnFindArduinoCore.Name = "btnFindArduinoCore";
            this.btnFindArduinoCore.Size = new System.Drawing.Size(75, 23);
            this.btnFindArduinoCore.TabIndex = 4;
            this.btnFindArduinoCore.Text = "Find";
            this.btnFindArduinoCore.UseVisualStyleBackColor = true;
            this.btnFindArduinoCore.Click += new System.EventHandler(this.btnFindArduinoCore_Click);
            // 
            // groupBox8
            // 
            this.groupBox8.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox8.Controls.Add(this.lnkFolder);
            this.groupBox8.Controls.Add(this.txtFavoriteDir);
            this.groupBox8.Controls.Add(this.btnFavoriteBrowse);
            this.groupBox8.Location = new System.Drawing.Point(12, 12);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Size = new System.Drawing.Size(725, 46);
            this.groupBox8.TabIndex = 3;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "Favorite Project Folder";
            // 
            // lnkFolder
            // 
            this.lnkFolder.AutoSize = true;
            this.lnkFolder.Location = new System.Drawing.Point(32, 22);
            this.lnkFolder.Name = "lnkFolder";
            this.lnkFolder.Size = new System.Drawing.Size(39, 13);
            this.lnkFolder.TabIndex = 1;
            this.lnkFolder.TabStop = true;
            this.lnkFolder.Text = "Folder:";
            this.lnkFolder.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkFolder_LinkClicked);
            // 
            // txtFavoriteDir
            // 
            this.txtFavoriteDir.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFavoriteDir.Location = new System.Drawing.Point(77, 19);
            this.txtFavoriteDir.Name = "txtFavoriteDir";
            this.txtFavoriteDir.ReadOnly = true;
            this.txtFavoriteDir.Size = new System.Drawing.Size(561, 20);
            this.txtFavoriteDir.TabIndex = 20;
            // 
            // btnFavoriteBrowse
            // 
            this.btnFavoriteBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFavoriteBrowse.Location = new System.Drawing.Point(644, 17);
            this.btnFavoriteBrowse.Name = "btnFavoriteBrowse";
            this.btnFavoriteBrowse.Size = new System.Drawing.Size(75, 23);
            this.btnFavoriteBrowse.TabIndex = 2;
            this.btnFavoriteBrowse.Text = "Find";
            this.btnFavoriteBrowse.UseVisualStyleBackColor = true;
            this.btnFavoriteBrowse.Click += new System.EventHandler(this.btnFavoriteBrowse_Click);
            // 
            // btnOpenAppData
            // 
            this.btnOpenAppData.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnOpenAppData.Location = new System.Drawing.Point(12, 371);
            this.btnOpenAppData.Name = "btnOpenAppData";
            this.btnOpenAppData.Size = new System.Drawing.Size(130, 23);
            this.btnOpenAppData.TabIndex = 17;
            this.btnOpenAppData.Text = "Open App Data Folder";
            this.btnOpenAppData.UseVisualStyleBackColor = true;
            this.btnOpenAppData.Click += new System.EventHandler(this.btnGotoAppdata_Click);
            // 
            // chkAutocomplete
            // 
            this.chkAutocomplete.AutoSize = true;
            this.chkAutocomplete.Checked = true;
            this.chkAutocomplete.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAutocomplete.Location = new System.Drawing.Point(6, 19);
            this.chkAutocomplete.Name = "chkAutocomplete";
            this.chkAutocomplete.Size = new System.Drawing.Size(127, 17);
            this.chkAutocomplete.TabIndex = 7;
            this.chkAutocomplete.Text = "Enable Autocomplete";
            this.chkAutocomplete.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.chkHighlightCurLine);
            this.groupBox1.Controls.Add(this.dropSmartIndent);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.numIndentWidth);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.numBackupInterval);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.numTabWidth);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.chkShowWS);
            this.groupBox1.Controls.Add(this.chkBackspaceUnindents);
            this.groupBox1.Controls.Add(this.chkTabIndents);
            this.groupBox1.Controls.Add(this.chkUseTabs);
            this.groupBox1.Controls.Add(this.chkWordWrap);
            this.groupBox1.Controls.Add(this.chkShowLineNum);
            this.groupBox1.Controls.Add(this.chkIndentGuide);
            this.groupBox1.Controls.Add(this.chkAutocomplete);
            this.groupBox1.Location = new System.Drawing.Point(12, 144);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(725, 128);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Editor Settings (may require restarting the editor to make changes take effect)";
            // 
            // dropSmartIndent
            // 
            this.dropSmartIndent.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.dropSmartIndent.FormattingEnabled = true;
            this.dropSmartIndent.Items.AddRange(new object[] {
            "Disabled",
            "Simple",
            "Smart Type 1",
            "Smart Type 2"});
            this.dropSmartIndent.Location = new System.Drawing.Point(416, 71);
            this.dropSmartIndent.Name = "dropSmartIndent";
            this.dropSmartIndent.Size = new System.Drawing.Size(108, 21);
            this.dropSmartIndent.TabIndex = 17;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(340, 74);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(70, 13);
            this.label3.TabIndex = 14;
            this.label3.Text = "Smart Indent:";
            // 
            // numIndentWidth
            // 
            this.numIndentWidth.Location = new System.Drawing.Point(416, 44);
            this.numIndentWidth.Maximum = new decimal(new int[] {
            8,
            0,
            0,
            0});
            this.numIndentWidth.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numIndentWidth.Name = "numIndentWidth";
            this.numIndentWidth.Size = new System.Drawing.Size(57, 20);
            this.numIndentWidth.TabIndex = 16;
            this.numIndentWidth.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(339, 46);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 13);
            this.label2.TabIndex = 12;
            this.label2.Text = "Indent Width:";
            // 
            // numBackupInterval
            // 
            this.numBackupInterval.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numBackupInterval.Location = new System.Drawing.Point(416, 98);
            this.numBackupInterval.Maximum = new decimal(new int[] {
            60000,
            0,
            0,
            0});
            this.numBackupInterval.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numBackupInterval.Name = "numBackupInterval";
            this.numBackupInterval.Size = new System.Drawing.Size(57, 20);
            this.numBackupInterval.TabIndex = 18;
            this.numBackupInterval.Value = new decimal(new int[] {
            30,
            0,
            0,
            0});
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(325, 100);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(85, 13);
            this.label4.TabIndex = 12;
            this.label4.Text = "Backup Interval:";
            // 
            // numTabWidth
            // 
            this.numTabWidth.Location = new System.Drawing.Point(416, 18);
            this.numTabWidth.Maximum = new decimal(new int[] {
            8,
            0,
            0,
            0});
            this.numTabWidth.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numTabWidth.Name = "numTabWidth";
            this.numTabWidth.Size = new System.Drawing.Size(57, 20);
            this.numTabWidth.TabIndex = 15;
            this.numTabWidth.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(350, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 13);
            this.label1.TabIndex = 12;
            this.label1.Text = "Tab Width:";
            // 
            // chkShowWS
            // 
            this.chkShowWS.AutoSize = true;
            this.chkShowWS.Location = new System.Drawing.Point(188, 88);
            this.chkShowWS.Name = "chkShowWS";
            this.chkShowWS.Size = new System.Drawing.Size(113, 17);
            this.chkShowWS.TabIndex = 14;
            this.chkShowWS.Text = "Show Whitespace";
            this.chkShowWS.UseVisualStyleBackColor = true;
            // 
            // chkBackspaceUnindents
            // 
            this.chkBackspaceUnindents.AutoSize = true;
            this.chkBackspaceUnindents.Location = new System.Drawing.Point(188, 65);
            this.chkBackspaceUnindents.Name = "chkBackspaceUnindents";
            this.chkBackspaceUnindents.Size = new System.Drawing.Size(131, 17);
            this.chkBackspaceUnindents.TabIndex = 13;
            this.chkBackspaceUnindents.Text = "Backspace Unindents";
            this.chkBackspaceUnindents.UseVisualStyleBackColor = true;
            // 
            // chkTabIndents
            // 
            this.chkTabIndents.AutoSize = true;
            this.chkTabIndents.Location = new System.Drawing.Point(188, 42);
            this.chkTabIndents.Name = "chkTabIndents";
            this.chkTabIndents.Size = new System.Drawing.Size(83, 17);
            this.chkTabIndents.TabIndex = 12;
            this.chkTabIndents.Text = "Tab Indents";
            this.chkTabIndents.UseVisualStyleBackColor = true;
            // 
            // chkUseTabs
            // 
            this.chkUseTabs.AutoSize = true;
            this.chkUseTabs.Location = new System.Drawing.Point(188, 19);
            this.chkUseTabs.Name = "chkUseTabs";
            this.chkUseTabs.Size = new System.Drawing.Size(72, 17);
            this.chkUseTabs.TabIndex = 11;
            this.chkUseTabs.Text = "Use Tabs";
            this.chkUseTabs.UseVisualStyleBackColor = true;
            // 
            // chkWordWrap
            // 
            this.chkWordWrap.AutoSize = true;
            this.chkWordWrap.Checked = true;
            this.chkWordWrap.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkWordWrap.Location = new System.Drawing.Point(6, 42);
            this.chkWordWrap.Name = "chkWordWrap";
            this.chkWordWrap.Size = new System.Drawing.Size(137, 17);
            this.chkWordWrap.TabIndex = 8;
            this.chkWordWrap.Text = "Enable Word Wrapping";
            this.chkWordWrap.UseVisualStyleBackColor = true;
            // 
            // chkShowLineNum
            // 
            this.chkShowLineNum.AutoSize = true;
            this.chkShowLineNum.Checked = true;
            this.chkShowLineNum.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkShowLineNum.Location = new System.Drawing.Point(6, 88);
            this.chkShowLineNum.Name = "chkShowLineNum";
            this.chkShowLineNum.Size = new System.Drawing.Size(121, 17);
            this.chkShowLineNum.TabIndex = 10;
            this.chkShowLineNum.Text = "Show Line Numbers";
            this.chkShowLineNum.UseVisualStyleBackColor = true;
            // 
            // chkIndentGuide
            // 
            this.chkIndentGuide.AutoSize = true;
            this.chkIndentGuide.Checked = true;
            this.chkIndentGuide.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkIndentGuide.Location = new System.Drawing.Point(6, 65);
            this.chkIndentGuide.Name = "chkIndentGuide";
            this.chkIndentGuide.Size = new System.Drawing.Size(168, 17);
            this.chkIndentGuide.TabIndex = 9;
            this.chkIndentGuide.Text = "Show Indentation Guide Lines";
            this.chkIndentGuide.UseVisualStyleBackColor = true;
            // 
            // btnOpenInstallationFolder
            // 
            this.btnOpenInstallationFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnOpenInstallationFolder.Location = new System.Drawing.Point(148, 371);
            this.btnOpenInstallationFolder.Name = "btnOpenInstallationFolder";
            this.btnOpenInstallationFolder.Size = new System.Drawing.Size(132, 23);
            this.btnOpenInstallationFolder.TabIndex = 18;
            this.btnOpenInstallationFolder.Text = "Open Installation Folder";
            this.btnOpenInstallationFolder.UseVisualStyleBackColor = true;
            this.btnOpenInstallationFolder.Click += new System.EventHandler(this.btnOpenInstallationFolder_Click);
            // 
            // chkCheckUpdates
            // 
            this.chkCheckUpdates.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chkCheckUpdates.AutoSize = true;
            this.chkCheckUpdates.Location = new System.Drawing.Point(294, 375);
            this.chkCheckUpdates.Name = "chkCheckUpdates";
            this.chkCheckUpdates.Size = new System.Drawing.Size(115, 17);
            this.chkCheckUpdates.TabIndex = 19;
            this.chkCheckUpdates.Text = "Check for Updates";
            this.chkCheckUpdates.UseVisualStyleBackColor = true;
            // 
            // btnSaveAndClose
            // 
            this.btnSaveAndClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSaveAndClose.Location = new System.Drawing.Point(519, 371);
            this.btnSaveAndClose.Name = "btnSaveAndClose";
            this.btnSaveAndClose.Size = new System.Drawing.Size(105, 23);
            this.btnSaveAndClose.TabIndex = 20;
            this.btnSaveAndClose.Text = "Save Changes";
            this.btnSaveAndClose.UseVisualStyleBackColor = true;
            this.btnSaveAndClose.Click += new System.EventHandler(this.btnSaveAndClose_Click);
            // 
            // btnDiscardAndClose
            // 
            this.btnDiscardAndClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDiscardAndClose.Location = new System.Drawing.Point(630, 371);
            this.btnDiscardAndClose.Name = "btnDiscardAndClose";
            this.btnDiscardAndClose.Size = new System.Drawing.Size(105, 23);
            this.btnDiscardAndClose.TabIndex = 21;
            this.btnDiscardAndClose.Text = "Discard Changes";
            this.btnDiscardAndClose.UseVisualStyleBackColor = true;
            this.btnDiscardAndClose.Click += new System.EventHandler(this.btnDiscardAndClose_Click);
            // 
            // radMsgOnTop
            // 
            this.radMsgOnTop.AutoSize = true;
            this.radMsgOnTop.Location = new System.Drawing.Point(6, 19);
            this.radMsgOnTop.Name = "radMsgOnTop";
            this.radMsgOnTop.Size = new System.Drawing.Size(148, 17);
            this.radMsgOnTop.TabIndex = 22;
            this.radMsgOnTop.TabStop = true;
            this.radMsgOnTop.Text = "Recent Messages on Top";
            this.radMsgOnTop.UseVisualStyleBackColor = true;
            this.radMsgOnTop.CheckedChanged += new System.EventHandler(this.radMsgOn_CheckedChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.radMsgOnBottom);
            this.groupBox2.Controls.Add(this.radMsgOnTop);
            this.groupBox2.Location = new System.Drawing.Point(12, 278);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(200, 70);
            this.groupBox2.TabIndex = 8;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Build Output Behaviour";
            // 
            // radMsgOnBottom
            // 
            this.radMsgOnBottom.AutoSize = true;
            this.radMsgOnBottom.Location = new System.Drawing.Point(6, 42);
            this.radMsgOnBottom.Name = "radMsgOnBottom";
            this.radMsgOnBottom.Size = new System.Drawing.Size(162, 17);
            this.radMsgOnBottom.TabIndex = 22;
            this.radMsgOnBottom.TabStop = true;
            this.radMsgOnBottom.Text = "Recent Messages on Bottom";
            this.radMsgOnBottom.UseVisualStyleBackColor = true;
            this.radMsgOnBottom.CheckedChanged += new System.EventHandler(this.radMsgOn_CheckedChanged);
            // 
            // chkReopenProject
            // 
            this.chkReopenProject.AutoSize = true;
            this.chkReopenProject.Location = new System.Drawing.Point(218, 278);
            this.chkReopenProject.Name = "chkReopenProject";
            this.chkReopenProject.Size = new System.Drawing.Size(194, 17);
            this.chkReopenProject.TabIndex = 9;
            this.chkReopenProject.Text = "Reopen Last Open Project On Start";
            this.chkReopenProject.UseVisualStyleBackColor = true;
            // 
            // chkHighlightCurLine
            // 
            this.chkHighlightCurLine.AutoSize = true;
            this.chkHighlightCurLine.Location = new System.Drawing.Point(535, 19);
            this.chkHighlightCurLine.Name = "chkHighlightCurLine";
            this.chkHighlightCurLine.Size = new System.Drawing.Size(127, 17);
            this.chkHighlightCurLine.TabIndex = 19;
            this.chkHighlightCurLine.Text = "Highlight Current Line";
            this.chkHighlightCurLine.UseVisualStyleBackColor = true;
            // 
            // SettingsWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(747, 406);
            this.Controls.Add(this.chkReopenProject);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.btnSaveAndClose);
            this.Controls.Add(this.btnDiscardAndClose);
            this.Controls.Add(this.chkCheckUpdates);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnOpenInstallationFolder);
            this.Controls.Add(this.btnOpenAppData);
            this.Controls.Add(this.groupBox9);
            this.Controls.Add(this.groupBox8);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SettingsWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Settings";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SettingsWindow_FormClosing);
            this.groupBox9.ResumeLayout(false);
            this.groupBox9.PerformLayout();
            this.groupBox8.ResumeLayout(false);
            this.groupBox8.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numIndentWidth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numBackupInterval)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numTabWidth)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox9;
        private System.Windows.Forms.TextBox txtArduinoLibs;
        private System.Windows.Forms.Button btnFindArduinoLibs;
        private System.Windows.Forms.TextBox txtArduinoCore;
        private System.Windows.Forms.Button btnFindArduinoCore;
        private System.Windows.Forms.GroupBox groupBox8;
        private System.Windows.Forms.TextBox txtFavoriteDir;
        private System.Windows.Forms.Button btnFavoriteBrowse;
        private System.Windows.Forms.Button btnOpenAppData;
        private System.Windows.Forms.CheckBox chkAutocomplete;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox chkWordWrap;
        private System.Windows.Forms.CheckBox chkIndentGuide;
        private System.Windows.Forms.LinkLabel lnkFolder;
        private System.Windows.Forms.Button btnOpenInstallationFolder;
        private System.Windows.Forms.LinkLabel lnkLibFiles;
        private System.Windows.Forms.LinkLabel lnkCoreFiles;
        private System.Windows.Forms.CheckBox chkBackspaceUnindents;
        private System.Windows.Forms.CheckBox chkTabIndents;
        private System.Windows.Forms.CheckBox chkUseTabs;
        private System.Windows.Forms.ComboBox dropSmartIndent;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown numIndentWidth;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown numTabWidth;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown numBackupInterval;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox chkCheckUpdates;
        private System.Windows.Forms.CheckBox chkShowLineNum;
        private System.Windows.Forms.CheckBox chkShowWS;
        private System.Windows.Forms.Button btnSaveAndClose;
        private System.Windows.Forms.Button btnDiscardAndClose;
        private System.Windows.Forms.RadioButton radMsgOnTop;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton radMsgOnBottom;
        private System.Windows.Forms.CheckBox chkReopenProject;
        private System.Windows.Forms.CheckBox chkHighlightCurLine;
    }
}