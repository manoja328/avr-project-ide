﻿namespace AVRProjectIDE
{
    partial class ConfigWindow
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle19 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle20 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle21 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle22 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle23 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle24 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle25 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle26 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle27 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tabsMain = new System.Windows.Forms.TabControl();
            this.tabGeneral = new System.Windows.Forms.TabPage();
            this.grpBoxBurnerPanel = new System.Windows.Forms.GroupBox();
            this.numClockFreq = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.chklistOptions = new System.Windows.Forms.CheckedListBox();
            this.dropDevices = new System.Windows.Forms.ComboBox();
            this.listOptimization = new System.Windows.Forms.ComboBox();
            this.txtOtherOptions = new System.Windows.Forms.TextBox();
            this.txtOutputPath = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tabInclude = new System.Windows.Forms.TabPage();
            this.tabsIncludeDir = new System.Windows.Forms.TabControl();
            this.tabIncludeDirDir = new System.Windows.Forms.TabPage();
            this.btnIncPathMoveDown = new System.Windows.Forms.Button();
            this.btnIncPathMoveUp = new System.Windows.Forms.Button();
            this.btnIncDirAdd = new System.Windows.Forms.Button();
            this.dgvIncPaths = new System.Windows.Forms.DataGridView();
            this.dgvIncPathTxtPath = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabIncludeDirLib = new System.Windows.Forms.TabPage();
            this.btnLibPathMoveDown = new System.Windows.Forms.Button();
            this.btnLibPathMoveUp = new System.Windows.Forms.Button();
            this.btnLibDirAdd = new System.Windows.Forms.Button();
            this.dgvLibPaths = new System.Windows.Forms.DataGridView();
            this.dgvLibPathTxtPath = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabLibrary = new System.Windows.Forms.TabPage();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.txtLinkerOptions = new System.Windows.Forms.TextBox();
            this.btnLibMoveUp = new System.Windows.Forms.Button();
            this.btnLibMoveDown = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.listLinkObj = new System.Windows.Forms.ListBox();
            this.btnAddLibFile = new System.Windows.Forms.Button();
            this.btnLibRemove = new System.Windows.Forms.Button();
            this.btnAddLib = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.listAvailLibs = new System.Windows.Forms.ListBox();
            this.tabMemory = new System.Windows.Forms.TabPage();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.dgvMemory = new System.Windows.Forms.DataGridView();
            this.dgvMemListType = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.dgvMemTxtName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvMemTxtAddr = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.txtInitStackAddr = new System.Windows.Forms.TextBox();
            this.chkUseInitStack = new System.Windows.Forms.CheckBox();
            this.Output = new System.Windows.Forms.TabPage();
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
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnApplyTemplate = new System.Windows.Forms.Button();
            this.dropTemplates = new System.Windows.Forms.ComboBox();
            this.btnGotoAppdata = new System.Windows.Forms.Button();
            this.btnDiscardAndClose = new System.Windows.Forms.Button();
            this.btnSaveAndClose = new System.Windows.Forms.Button();
            this.tabsMain.SuspendLayout();
            this.tabGeneral.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numClockFreq)).BeginInit();
            this.tabInclude.SuspendLayout();
            this.tabsIncludeDir.SuspendLayout();
            this.tabIncludeDirDir.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvIncPaths)).BeginInit();
            this.tabIncludeDirLib.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLibPaths)).BeginInit();
            this.tabLibrary.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.tabMemory.SuspendLayout();
            this.groupBox6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMemory)).BeginInit();
            this.groupBox5.SuspendLayout();
            this.Output.SuspendLayout();
            this.groupBox9.SuspendLayout();
            this.groupBox8.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabsMain
            // 
            this.tabsMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tabsMain.Controls.Add(this.tabGeneral);
            this.tabsMain.Controls.Add(this.tabInclude);
            this.tabsMain.Controls.Add(this.tabLibrary);
            this.tabsMain.Controls.Add(this.tabMemory);
            this.tabsMain.Controls.Add(this.Output);
            this.tabsMain.Location = new System.Drawing.Point(0, 0);
            this.tabsMain.Name = "tabsMain";
            this.tabsMain.SelectedIndex = 0;
            this.tabsMain.Size = new System.Drawing.Size(712, 364);
            this.tabsMain.TabIndex = 0;
            // 
            // tabGeneral
            // 
            this.tabGeneral.Controls.Add(this.grpBoxBurnerPanel);
            this.tabGeneral.Controls.Add(this.numClockFreq);
            this.tabGeneral.Controls.Add(this.label7);
            this.tabGeneral.Controls.Add(this.label6);
            this.tabGeneral.Controls.Add(this.chklistOptions);
            this.tabGeneral.Controls.Add(this.dropDevices);
            this.tabGeneral.Controls.Add(this.listOptimization);
            this.tabGeneral.Controls.Add(this.txtOtherOptions);
            this.tabGeneral.Controls.Add(this.txtOutputPath);
            this.tabGeneral.Controls.Add(this.label5);
            this.tabGeneral.Controls.Add(this.label4);
            this.tabGeneral.Controls.Add(this.label3);
            this.tabGeneral.Controls.Add(this.label2);
            this.tabGeneral.Location = new System.Drawing.Point(4, 22);
            this.tabGeneral.Name = "tabGeneral";
            this.tabGeneral.Padding = new System.Windows.Forms.Padding(3);
            this.tabGeneral.Size = new System.Drawing.Size(704, 338);
            this.tabGeneral.TabIndex = 1;
            this.tabGeneral.Text = "General";
            this.tabGeneral.UseVisualStyleBackColor = true;
            // 
            // grpBoxBurnerPanel
            // 
            this.grpBoxBurnerPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.grpBoxBurnerPanel.Location = new System.Drawing.Point(8, 169);
            this.grpBoxBurnerPanel.Name = "grpBoxBurnerPanel";
            this.grpBoxBurnerPanel.Size = new System.Drawing.Size(487, 161);
            this.grpBoxBurnerPanel.TabIndex = 6;
            this.grpBoxBurnerPanel.TabStop = false;
            this.grpBoxBurnerPanel.Text = "AVRDUDE Command Builder";
            // 
            // numClockFreq
            // 
            this.numClockFreq.Increment = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.numClockFreq.Location = new System.Drawing.Point(94, 58);
            this.numClockFreq.Maximum = new decimal(new int[] {
            32000000,
            0,
            0,
            0});
            this.numClockFreq.Name = "numClockFreq";
            this.numClockFreq.Size = new System.Drawing.Size(100, 20);
            this.numClockFreq.TabIndex = 3;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(13, 117);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(75, 13);
            this.label7.TabIndex = 10;
            this.label7.Text = "Other Options:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(223, 6);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(46, 13);
            this.label6.TabIndex = 9;
            this.label6.Text = "Options:";
            // 
            // chklistOptions
            // 
            this.chklistOptions.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.chklistOptions.CheckOnClick = true;
            this.chklistOptions.FormattingEnabled = true;
            this.chklistOptions.Items.AddRange(new object[] {
            "Unsigned Chars (-funsigned-char)",
            "Unsigned Bitfields (-funsigned-bitfields)",
            "Pack Structure Members (-fpack-struct)",
            "Short Enums (-fshort-enums)",
            "Function Sections (-ffunction-sections)",
            "Data Sections (-fdata-sections)"});
            this.chklistOptions.Location = new System.Drawing.Point(275, 6);
            this.chklistOptions.Name = "chklistOptions";
            this.chklistOptions.Size = new System.Drawing.Size(220, 90);
            this.chklistOptions.TabIndex = 6;
            // 
            // dropDevices
            // 
            this.dropDevices.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.dropDevices.FormattingEnabled = true;
            this.dropDevices.Items.AddRange(new object[] {
            "at90s1200",
            "attiny11",
            "attiny12",
            "attiny15",
            "attiny28",
            "at90s2313",
            "at90s2323",
            "at90s2333",
            "at90s2343",
            "attiny22",
            "attiny26",
            "at90s4414",
            "at90s4433",
            "at90s4434",
            "at90s8515",
            "at90c8534",
            "at90s8535",
            "at86rf401",
            "ata6289",
            "attiny13",
            "attiny13a",
            "attiny2313",
            "attiny24",
            "attiny25",
            "attiny261",
            "attiny43u",
            "attiny44",
            "attiny45",
            "attiny461",
            "attiny48",
            "attiny84",
            "attiny85",
            "attiny861",
            "attiny87",
            "attiny88",
            "atmega603",
            "at43usb355",
            "atmega103",
            "at43usb320",
            "at90usb82",
            "at90usb162",
            "attiny167",
            "at76c711",
            "atmega48",
            "atmega48p",
            "atmega8",
            "atmega8515",
            "atmega8535",
            "atmega88",
            "atmega88p",
            "atmega8hva",
            "at90pwm1",
            "at90pwm2",
            "at90pwm2b",
            "at90pwm3",
            "at90pwm3b",
            "at90pwm81",
            "at90pwm216",
            "at90pwm316",
            "at90can32",
            "at90can64",
            "at90usb646",
            "at90usb647",
            "atmega16",
            "atmega161",
            "atmega162",
            "atmega163",
            "atmega164p",
            "atmega165",
            "atmega165p",
            "atmega168",
            "atmega168p",
            "atmega169",
            "atmega169p",
            "atmega16hva",
            "atmega16m1",
            "atmega16u4",
            "atmega32",
            "atmega323",
            "atmega324p",
            "atmega325",
            "atmega325p",
            "atmega3250",
            "atmega3250p",
            "atmega328p",
            "atmega329",
            "atmega329p",
            "atmega3290",
            "atmega3290p",
            "atmega32c1",
            "atmega32hvb",
            "atmega32m1",
            "atmega32u4",
            "atmega32u6",
            "atmega406",
            "atmega64",
            "atmega640",
            "atmega644",
            "atmega644p",
            "atmega645",
            "atmega6450",
            "atmega649",
            "atmega6490",
            "atmega64c1",
            "atmega64m1",
            "at94k",
            "at90scr100",
            "atmega128",
            "atmega1280",
            "atmega1281",
            "atmega1284p",
            "at90can128",
            "at90usb1286",
            "at90usb1287",
            "atmega2560",
            "atmega2561",
            "atxmega16a4",
            "atxmega16d4",
            "atxmega32d4",
            "atxmega32a4",
            "atxmega64a3",
            "atxmega64a1",
            "atxmega128a3",
            "atxmega256a3",
            "atxmega256a3b",
            "atxmega128a1"});
            this.dropDevices.Location = new System.Drawing.Point(94, 32);
            this.dropDevices.Name = "dropDevices";
            this.dropDevices.Size = new System.Drawing.Size(100, 21);
            this.dropDevices.TabIndex = 2;
            // 
            // listOptimization
            // 
            this.listOptimization.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.listOptimization.FormattingEnabled = true;
            this.listOptimization.Items.AddRange(new object[] {
            "-O0",
            "-O1",
            "-O2",
            "-O3",
            "-Os"});
            this.listOptimization.Location = new System.Drawing.Point(94, 87);
            this.listOptimization.Name = "listOptimization";
            this.listOptimization.Size = new System.Drawing.Size(100, 21);
            this.listOptimization.TabIndex = 4;
            // 
            // txtOtherOptions
            // 
            this.txtOtherOptions.Location = new System.Drawing.Point(94, 114);
            this.txtOtherOptions.Name = "txtOtherOptions";
            this.txtOtherOptions.Size = new System.Drawing.Size(401, 20);
            this.txtOtherOptions.TabIndex = 5;
            // 
            // txtOutputPath
            // 
            this.txtOutputPath.Location = new System.Drawing.Point(94, 6);
            this.txtOutputPath.Name = "txtOutputPath";
            this.txtOutputPath.Size = new System.Drawing.Size(100, 20);
            this.txtOutputPath.TabIndex = 1;
            this.txtOutputPath.Validating += new System.ComponentModel.CancelEventHandler(this.txtOutputPath_Validating);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(21, 90);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(67, 13);
            this.label5.TabIndex = 5;
            this.label5.Text = "Optimization:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(5, 64);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(83, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "Clock Freq (Hz):";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(44, 35);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(44, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Device:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(21, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Output Path:";
            // 
            // tabInclude
            // 
            this.tabInclude.Controls.Add(this.tabsIncludeDir);
            this.tabInclude.Location = new System.Drawing.Point(4, 22);
            this.tabInclude.Name = "tabInclude";
            this.tabInclude.Padding = new System.Windows.Forms.Padding(3);
            this.tabInclude.Size = new System.Drawing.Size(704, 338);
            this.tabInclude.TabIndex = 2;
            this.tabInclude.Text = "Include Dir";
            this.tabInclude.UseVisualStyleBackColor = true;
            // 
            // tabsIncludeDir
            // 
            this.tabsIncludeDir.Controls.Add(this.tabIncludeDirDir);
            this.tabsIncludeDir.Controls.Add(this.tabIncludeDirLib);
            this.tabsIncludeDir.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabsIncludeDir.Location = new System.Drawing.Point(3, 3);
            this.tabsIncludeDir.Name = "tabsIncludeDir";
            this.tabsIncludeDir.SelectedIndex = 0;
            this.tabsIncludeDir.Size = new System.Drawing.Size(698, 332);
            this.tabsIncludeDir.TabIndex = 0;
            // 
            // tabIncludeDirDir
            // 
            this.tabIncludeDirDir.Controls.Add(this.btnIncPathMoveDown);
            this.tabIncludeDirDir.Controls.Add(this.btnIncPathMoveUp);
            this.tabIncludeDirDir.Controls.Add(this.btnIncDirAdd);
            this.tabIncludeDirDir.Controls.Add(this.dgvIncPaths);
            this.tabIncludeDirDir.Location = new System.Drawing.Point(4, 22);
            this.tabIncludeDirDir.Name = "tabIncludeDirDir";
            this.tabIncludeDirDir.Padding = new System.Windows.Forms.Padding(3);
            this.tabIncludeDirDir.Size = new System.Drawing.Size(690, 306);
            this.tabIncludeDirDir.TabIndex = 0;
            this.tabIncludeDirDir.Text = "Include Paths";
            this.tabIncludeDirDir.UseVisualStyleBackColor = true;
            // 
            // btnIncPathMoveDown
            // 
            this.btnIncPathMoveDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnIncPathMoveDown.Location = new System.Drawing.Point(168, 277);
            this.btnIncPathMoveDown.Name = "btnIncPathMoveDown";
            this.btnIncPathMoveDown.Size = new System.Drawing.Size(75, 23);
            this.btnIncPathMoveDown.TabIndex = 19;
            this.btnIncPathMoveDown.Text = "Move Down";
            this.btnIncPathMoveDown.UseVisualStyleBackColor = true;
            this.btnIncPathMoveDown.Click += new System.EventHandler(this.btnIncPathMoveDown_Click);
            // 
            // btnIncPathMoveUp
            // 
            this.btnIncPathMoveUp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnIncPathMoveUp.Location = new System.Drawing.Point(87, 277);
            this.btnIncPathMoveUp.Name = "btnIncPathMoveUp";
            this.btnIncPathMoveUp.Size = new System.Drawing.Size(75, 23);
            this.btnIncPathMoveUp.TabIndex = 18;
            this.btnIncPathMoveUp.Text = "Move Up";
            this.btnIncPathMoveUp.UseVisualStyleBackColor = true;
            this.btnIncPathMoveUp.Click += new System.EventHandler(this.btnIncPathMoveUp_Click);
            // 
            // btnIncDirAdd
            // 
            this.btnIncDirAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnIncDirAdd.Location = new System.Drawing.Point(6, 277);
            this.btnIncDirAdd.Name = "btnIncDirAdd";
            this.btnIncDirAdd.Size = new System.Drawing.Size(75, 23);
            this.btnIncDirAdd.TabIndex = 17;
            this.btnIncDirAdd.Text = "Find";
            this.btnIncDirAdd.UseVisualStyleBackColor = true;
            this.btnIncDirAdd.Click += new System.EventHandler(this.btnIncDirAdd_Click);
            // 
            // dgvIncPaths
            // 
            this.dgvIncPaths.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            dataGridViewCellStyle19.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle19.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle19.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle19.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle19.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle19.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle19.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvIncPaths.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle19;
            this.dgvIncPaths.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvIncPaths.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dgvIncPathTxtPath});
            dataGridViewCellStyle20.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle20.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle20.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle20.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle20.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle20.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle20.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvIncPaths.DefaultCellStyle = dataGridViewCellStyle20;
            this.dgvIncPaths.Location = new System.Drawing.Point(3, 3);
            this.dgvIncPaths.MultiSelect = false;
            this.dgvIncPaths.Name = "dgvIncPaths";
            dataGridViewCellStyle21.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle21.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle21.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle21.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle21.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle21.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle21.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvIncPaths.RowHeadersDefaultCellStyle = dataGridViewCellStyle21;
            this.dgvIncPaths.Size = new System.Drawing.Size(684, 268);
            this.dgvIncPaths.TabIndex = 4;
            // 
            // dgvIncPathTxtPath
            // 
            this.dgvIncPathTxtPath.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dgvIncPathTxtPath.HeaderText = "Path";
            this.dgvIncPathTxtPath.Name = "dgvIncPathTxtPath";
            // 
            // tabIncludeDirLib
            // 
            this.tabIncludeDirLib.Controls.Add(this.btnLibPathMoveDown);
            this.tabIncludeDirLib.Controls.Add(this.btnLibPathMoveUp);
            this.tabIncludeDirLib.Controls.Add(this.btnLibDirAdd);
            this.tabIncludeDirLib.Controls.Add(this.dgvLibPaths);
            this.tabIncludeDirLib.Location = new System.Drawing.Point(4, 22);
            this.tabIncludeDirLib.Name = "tabIncludeDirLib";
            this.tabIncludeDirLib.Padding = new System.Windows.Forms.Padding(3);
            this.tabIncludeDirLib.Size = new System.Drawing.Size(690, 295);
            this.tabIncludeDirLib.TabIndex = 1;
            this.tabIncludeDirLib.Text = "Library Paths";
            this.tabIncludeDirLib.UseVisualStyleBackColor = true;
            // 
            // btnLibPathMoveDown
            // 
            this.btnLibPathMoveDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnLibPathMoveDown.Location = new System.Drawing.Point(168, 266);
            this.btnLibPathMoveDown.Name = "btnLibPathMoveDown";
            this.btnLibPathMoveDown.Size = new System.Drawing.Size(75, 23);
            this.btnLibPathMoveDown.TabIndex = 22;
            this.btnLibPathMoveDown.Text = "Move Down";
            this.btnLibPathMoveDown.UseVisualStyleBackColor = true;
            this.btnLibPathMoveDown.Click += new System.EventHandler(this.btnLibPathMoveDown_Click);
            // 
            // btnLibPathMoveUp
            // 
            this.btnLibPathMoveUp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnLibPathMoveUp.Location = new System.Drawing.Point(87, 266);
            this.btnLibPathMoveUp.Name = "btnLibPathMoveUp";
            this.btnLibPathMoveUp.Size = new System.Drawing.Size(75, 23);
            this.btnLibPathMoveUp.TabIndex = 21;
            this.btnLibPathMoveUp.Text = "Move Up";
            this.btnLibPathMoveUp.UseVisualStyleBackColor = true;
            this.btnLibPathMoveUp.Click += new System.EventHandler(this.btnLibPathMoveUp_Click);
            // 
            // btnLibDirAdd
            // 
            this.btnLibDirAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnLibDirAdd.Location = new System.Drawing.Point(6, 266);
            this.btnLibDirAdd.Name = "btnLibDirAdd";
            this.btnLibDirAdd.Size = new System.Drawing.Size(75, 23);
            this.btnLibDirAdd.TabIndex = 20;
            this.btnLibDirAdd.Text = "Find";
            this.btnLibDirAdd.UseVisualStyleBackColor = true;
            this.btnLibDirAdd.Click += new System.EventHandler(this.btnLibDirAdd_Click);
            // 
            // dgvLibPaths
            // 
            this.dgvLibPaths.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            dataGridViewCellStyle22.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle22.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle22.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle22.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle22.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle22.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle22.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvLibPaths.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle22;
            this.dgvLibPaths.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvLibPaths.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dgvLibPathTxtPath});
            dataGridViewCellStyle23.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle23.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle23.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle23.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle23.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle23.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle23.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvLibPaths.DefaultCellStyle = dataGridViewCellStyle23;
            this.dgvLibPaths.Location = new System.Drawing.Point(3, 3);
            this.dgvLibPaths.MultiSelect = false;
            this.dgvLibPaths.Name = "dgvLibPaths";
            dataGridViewCellStyle24.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle24.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle24.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle24.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle24.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle24.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle24.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvLibPaths.RowHeadersDefaultCellStyle = dataGridViewCellStyle24;
            this.dgvLibPaths.Size = new System.Drawing.Size(684, 257);
            this.dgvLibPaths.TabIndex = 3;
            // 
            // dgvLibPathTxtPath
            // 
            this.dgvLibPathTxtPath.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dgvLibPathTxtPath.HeaderText = "Path";
            this.dgvLibPathTxtPath.Name = "dgvLibPathTxtPath";
            // 
            // tabLibrary
            // 
            this.tabLibrary.Controls.Add(this.groupBox7);
            this.tabLibrary.Controls.Add(this.btnLibMoveUp);
            this.tabLibrary.Controls.Add(this.btnLibMoveDown);
            this.tabLibrary.Controls.Add(this.groupBox4);
            this.tabLibrary.Controls.Add(this.btnAddLibFile);
            this.tabLibrary.Controls.Add(this.btnLibRemove);
            this.tabLibrary.Controls.Add(this.btnAddLib);
            this.tabLibrary.Controls.Add(this.groupBox3);
            this.tabLibrary.Location = new System.Drawing.Point(4, 22);
            this.tabLibrary.Name = "tabLibrary";
            this.tabLibrary.Padding = new System.Windows.Forms.Padding(3);
            this.tabLibrary.Size = new System.Drawing.Size(704, 338);
            this.tabLibrary.TabIndex = 3;
            this.tabLibrary.Text = "Libraries";
            this.tabLibrary.UseVisualStyleBackColor = true;
            // 
            // groupBox7
            // 
            this.groupBox7.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox7.Controls.Add(this.txtLinkerOptions);
            this.groupBox7.Location = new System.Drawing.Point(6, 282);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(692, 50);
            this.groupBox7.TabIndex = 6;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "Linker Options:";
            // 
            // txtLinkerOptions
            // 
            this.txtLinkerOptions.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtLinkerOptions.Location = new System.Drawing.Point(6, 19);
            this.txtLinkerOptions.Name = "txtLinkerOptions";
            this.txtLinkerOptions.Size = new System.Drawing.Size(680, 20);
            this.txtLinkerOptions.TabIndex = 5;
            // 
            // btnLibMoveUp
            // 
            this.btnLibMoveUp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLibMoveUp.Location = new System.Drawing.Point(704, 22);
            this.btnLibMoveUp.Name = "btnLibMoveUp";
            this.btnLibMoveUp.Size = new System.Drawing.Size(75, 23);
            this.btnLibMoveUp.TabIndex = 5;
            this.btnLibMoveUp.Text = "Move Up";
            this.btnLibMoveUp.UseVisualStyleBackColor = true;
            this.btnLibMoveUp.Click += new System.EventHandler(this.btnLibMoveUp_Click);
            // 
            // btnLibMoveDown
            // 
            this.btnLibMoveDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLibMoveDown.Location = new System.Drawing.Point(704, 51);
            this.btnLibMoveDown.Name = "btnLibMoveDown";
            this.btnLibMoveDown.Size = new System.Drawing.Size(75, 23);
            this.btnLibMoveDown.TabIndex = 5;
            this.btnLibMoveDown.Text = "MoveDown";
            this.btnLibMoveDown.UseVisualStyleBackColor = true;
            this.btnLibMoveDown.Click += new System.EventHandler(this.btnLibMoveDown_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox4.Controls.Add(this.listLinkObj);
            this.groupBox4.Location = new System.Drawing.Point(263, 6);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(435, 270);
            this.groupBox4.TabIndex = 2;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Included";
            // 
            // listLinkObj
            // 
            this.listLinkObj.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listLinkObj.FormattingEnabled = true;
            this.listLinkObj.Location = new System.Drawing.Point(3, 16);
            this.listLinkObj.Name = "listLinkObj";
            this.listLinkObj.Size = new System.Drawing.Size(429, 251);
            this.listLinkObj.TabIndex = 4;
            // 
            // btnAddLibFile
            // 
            this.btnAddLibFile.Location = new System.Drawing.Point(182, 51);
            this.btnAddLibFile.Name = "btnAddLibFile";
            this.btnAddLibFile.Size = new System.Drawing.Size(75, 23);
            this.btnAddLibFile.TabIndex = 2;
            this.btnAddLibFile.Text = "Add File ->";
            this.btnAddLibFile.UseVisualStyleBackColor = true;
            this.btnAddLibFile.Click += new System.EventHandler(this.btnAddLibFile_Click);
            // 
            // btnLibRemove
            // 
            this.btnLibRemove.Location = new System.Drawing.Point(182, 80);
            this.btnLibRemove.Name = "btnLibRemove";
            this.btnLibRemove.Size = new System.Drawing.Size(75, 23);
            this.btnLibRemove.TabIndex = 3;
            this.btnLibRemove.Text = "Remove";
            this.btnLibRemove.UseVisualStyleBackColor = true;
            this.btnLibRemove.Click += new System.EventHandler(this.btnLibRemove_Click);
            // 
            // btnAddLib
            // 
            this.btnAddLib.Location = new System.Drawing.Point(182, 22);
            this.btnAddLib.Name = "btnAddLib";
            this.btnAddLib.Size = new System.Drawing.Size(75, 23);
            this.btnAddLib.TabIndex = 1;
            this.btnAddLib.Text = "Add ->";
            this.btnAddLib.UseVisualStyleBackColor = true;
            this.btnAddLib.Click += new System.EventHandler(this.btnAddLib_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox3.Controls.Add(this.listAvailLibs);
            this.groupBox3.Location = new System.Drawing.Point(6, 6);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(170, 270);
            this.groupBox3.TabIndex = 1;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Available Link Objects";
            // 
            // listAvailLibs
            // 
            this.listAvailLibs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listAvailLibs.FormattingEnabled = true;
            this.listAvailLibs.Items.AddRange(new object[] {
            "libc.a",
            "libm.a",
            "libobjc.a",
            "libprintf_flt.a",
            "libprintf_min.a",
            "libscanf_flt.a",
            "libscanf_min.a"});
            this.listAvailLibs.Location = new System.Drawing.Point(3, 16);
            this.listAvailLibs.Name = "listAvailLibs";
            this.listAvailLibs.Size = new System.Drawing.Size(164, 251);
            this.listAvailLibs.TabIndex = 0;
            // 
            // tabMemory
            // 
            this.tabMemory.Controls.Add(this.groupBox6);
            this.tabMemory.Controls.Add(this.groupBox5);
            this.tabMemory.Location = new System.Drawing.Point(4, 22);
            this.tabMemory.Name = "tabMemory";
            this.tabMemory.Padding = new System.Windows.Forms.Padding(3);
            this.tabMemory.Size = new System.Drawing.Size(704, 338);
            this.tabMemory.TabIndex = 4;
            this.tabMemory.Text = "Memory";
            this.tabMemory.UseVisualStyleBackColor = true;
            // 
            // groupBox6
            // 
            this.groupBox6.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox6.Controls.Add(this.dgvMemory);
            this.groupBox6.Location = new System.Drawing.Point(3, 6);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(693, 270);
            this.groupBox6.TabIndex = 1;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Memory Segments";
            // 
            // dgvMemory
            // 
            dataGridViewCellStyle25.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle25.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle25.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle25.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle25.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle25.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle25.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvMemory.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle25;
            this.dgvMemory.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMemory.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dgvMemListType,
            this.dgvMemTxtName,
            this.dgvMemTxtAddr});
            dataGridViewCellStyle26.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle26.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle26.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle26.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle26.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle26.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle26.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvMemory.DefaultCellStyle = dataGridViewCellStyle26;
            this.dgvMemory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvMemory.Location = new System.Drawing.Point(3, 16);
            this.dgvMemory.Name = "dgvMemory";
            dataGridViewCellStyle27.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle27.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle27.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle27.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle27.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle27.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle27.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvMemory.RowHeadersDefaultCellStyle = dataGridViewCellStyle27;
            this.dgvMemory.Size = new System.Drawing.Size(687, 251);
            this.dgvMemory.TabIndex = 2;
            this.dgvMemory.RowValidating += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.dgvMemory_RowValidating);
            // 
            // dgvMemListType
            // 
            this.dgvMemListType.HeaderText = "Memory Type";
            this.dgvMemListType.Items.AddRange(new object[] {
            "Flash",
            "SRAM",
            "EEPROM"});
            this.dgvMemListType.Name = "dgvMemListType";
            // 
            // dgvMemTxtName
            // 
            this.dgvMemTxtName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dgvMemTxtName.HeaderText = "Segment Name";
            this.dgvMemTxtName.Name = "dgvMemTxtName";
            // 
            // dgvMemTxtAddr
            // 
            this.dgvMemTxtAddr.HeaderText = "Address";
            this.dgvMemTxtAddr.MaxInputLength = 4;
            this.dgvMemTxtAddr.Name = "dgvMemTxtAddr";
            // 
            // groupBox5
            // 
            this.groupBox5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox5.Controls.Add(this.txtInitStackAddr);
            this.groupBox5.Controls.Add(this.chkUseInitStack);
            this.groupBox5.Location = new System.Drawing.Point(6, 282);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(690, 50);
            this.groupBox5.TabIndex = 0;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Stack Settings";
            // 
            // txtInitStackAddr
            // 
            this.txtInitStackAddr.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtInitStackAddr.Location = new System.Drawing.Point(187, 19);
            this.txtInitStackAddr.MaxLength = 4;
            this.txtInitStackAddr.Name = "txtInitStackAddr";
            this.txtInitStackAddr.Size = new System.Drawing.Size(55, 20);
            this.txtInitStackAddr.TabIndex = 1;
            this.txtInitStackAddr.Text = "0000";
            this.txtInitStackAddr.Validating += new System.ComponentModel.CancelEventHandler(this.txtInitStackAddr_Validating);
            // 
            // chkUseInitStack
            // 
            this.chkUseInitStack.AutoSize = true;
            this.chkUseInitStack.Location = new System.Drawing.Point(15, 21);
            this.chkUseInitStack.Name = "chkUseInitStack";
            this.chkUseInitStack.Size = new System.Drawing.Size(177, 17);
            this.chkUseInitStack.TabIndex = 0;
            this.chkUseInitStack.Text = "Specify Initial Stack Address: 0x";
            this.chkUseInitStack.UseVisualStyleBackColor = true;
            // 
            // Output
            // 
            this.Output.Controls.Add(this.groupBox9);
            this.Output.Controls.Add(this.groupBox8);
            this.Output.Controls.Add(this.groupBox2);
            this.Output.Controls.Add(this.btnGotoAppdata);
            this.Output.Location = new System.Drawing.Point(4, 22);
            this.Output.Name = "Output";
            this.Output.Padding = new System.Windows.Forms.Padding(3);
            this.Output.Size = new System.Drawing.Size(704, 338);
            this.Output.TabIndex = 5;
            this.Output.Text = "Project Templates";
            this.Output.UseVisualStyleBackColor = true;
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
            this.groupBox9.Location = new System.Drawing.Point(8, 139);
            this.groupBox9.Name = "groupBox9";
            this.groupBox9.Size = new System.Drawing.Size(688, 74);
            this.groupBox9.TabIndex = 2;
            this.groupBox9.TabStop = false;
            this.groupBox9.Text = "Arduino Related Folders";
            this.groupBox9.Visible = false;
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
            this.txtArduinoLibs.Size = new System.Drawing.Size(524, 20);
            this.txtArduinoLibs.TabIndex = 8;
            // 
            // btnFindArduinoLibs
            // 
            this.btnFindArduinoLibs.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFindArduinoLibs.Location = new System.Drawing.Point(607, 43);
            this.btnFindArduinoLibs.Name = "btnFindArduinoLibs";
            this.btnFindArduinoLibs.Size = new System.Drawing.Size(75, 23);
            this.btnFindArduinoLibs.TabIndex = 5;
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
            this.txtArduinoCore.Size = new System.Drawing.Size(524, 20);
            this.txtArduinoCore.TabIndex = 7;
            // 
            // btnFindArduinoCore
            // 
            this.btnFindArduinoCore.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFindArduinoCore.Location = new System.Drawing.Point(607, 17);
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
            this.groupBox8.Controls.Add(this.txtFavoriteDir);
            this.groupBox8.Controls.Add(this.btnFavoriteBrowse);
            this.groupBox8.Location = new System.Drawing.Point(8, 87);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Size = new System.Drawing.Size(688, 46);
            this.groupBox8.TabIndex = 2;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "Favorite Project Folder";
            this.groupBox8.Visible = false;
            // 
            // txtFavoriteDir
            // 
            this.txtFavoriteDir.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFavoriteDir.Location = new System.Drawing.Point(6, 19);
            this.txtFavoriteDir.Name = "txtFavoriteDir";
            this.txtFavoriteDir.ReadOnly = true;
            this.txtFavoriteDir.Size = new System.Drawing.Size(595, 20);
            this.txtFavoriteDir.TabIndex = 6;
            // 
            // btnFavoriteBrowse
            // 
            this.btnFavoriteBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFavoriteBrowse.Location = new System.Drawing.Point(607, 17);
            this.btnFavoriteBrowse.Name = "btnFavoriteBrowse";
            this.btnFavoriteBrowse.Size = new System.Drawing.Size(75, 23);
            this.btnFavoriteBrowse.TabIndex = 3;
            this.btnFavoriteBrowse.Text = "Find";
            this.btnFavoriteBrowse.UseVisualStyleBackColor = true;
            this.btnFavoriteBrowse.Click += new System.EventHandler(this.btnFavoriteBrowse_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.btnApplyTemplate);
            this.groupBox2.Controls.Add(this.dropTemplates);
            this.groupBox2.Location = new System.Drawing.Point(8, 35);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(688, 46);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Project Templates";
            // 
            // btnApplyTemplate
            // 
            this.btnApplyTemplate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnApplyTemplate.Location = new System.Drawing.Point(607, 18);
            this.btnApplyTemplate.Name = "btnApplyTemplate";
            this.btnApplyTemplate.Size = new System.Drawing.Size(75, 23);
            this.btnApplyTemplate.TabIndex = 2;
            this.btnApplyTemplate.Text = "Apply";
            this.btnApplyTemplate.UseVisualStyleBackColor = true;
            this.btnApplyTemplate.Click += new System.EventHandler(this.btnApplyTemplate_Click);
            // 
            // dropTemplates
            // 
            this.dropTemplates.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dropTemplates.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.dropTemplates.FormattingEnabled = true;
            this.dropTemplates.Location = new System.Drawing.Point(6, 19);
            this.dropTemplates.Name = "dropTemplates";
            this.dropTemplates.Size = new System.Drawing.Size(596, 21);
            this.dropTemplates.TabIndex = 1;
            // 
            // btnGotoAppdata
            // 
            this.btnGotoAppdata.Location = new System.Drawing.Point(8, 6);
            this.btnGotoAppdata.Name = "btnGotoAppdata";
            this.btnGotoAppdata.Size = new System.Drawing.Size(198, 23);
            this.btnGotoAppdata.TabIndex = 0;
            this.btnGotoAppdata.Text = "Go To Application Settings Folder";
            this.btnGotoAppdata.UseVisualStyleBackColor = true;
            this.btnGotoAppdata.Visible = false;
            this.btnGotoAppdata.Click += new System.EventHandler(this.btnGotoAppdata_Click);
            // 
            // btnDiscardAndClose
            // 
            this.btnDiscardAndClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDiscardAndClose.Location = new System.Drawing.Point(595, 370);
            this.btnDiscardAndClose.Name = "btnDiscardAndClose";
            this.btnDiscardAndClose.Size = new System.Drawing.Size(105, 23);
            this.btnDiscardAndClose.TabIndex = 2;
            this.btnDiscardAndClose.Text = "Discard Changes";
            this.btnDiscardAndClose.UseVisualStyleBackColor = true;
            this.btnDiscardAndClose.Click += new System.EventHandler(this.btnDiscardAndClose_Click);
            // 
            // btnSaveAndClose
            // 
            this.btnSaveAndClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSaveAndClose.Location = new System.Drawing.Point(484, 370);
            this.btnSaveAndClose.Name = "btnSaveAndClose";
            this.btnSaveAndClose.Size = new System.Drawing.Size(105, 23);
            this.btnSaveAndClose.TabIndex = 1;
            this.btnSaveAndClose.Text = "Save Changes";
            this.btnSaveAndClose.UseVisualStyleBackColor = true;
            this.btnSaveAndClose.Click += new System.EventHandler(this.btnSaveAndClose_Click);
            // 
            // ConfigWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(712, 405);
            this.Controls.Add(this.btnSaveAndClose);
            this.Controls.Add(this.btnDiscardAndClose);
            this.Controls.Add(this.tabsMain);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ConfigWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Project Configuration";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmProjEditor_FormClosing);
            this.tabsMain.ResumeLayout(false);
            this.tabGeneral.ResumeLayout(false);
            this.tabGeneral.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numClockFreq)).EndInit();
            this.tabInclude.ResumeLayout(false);
            this.tabsIncludeDir.ResumeLayout(false);
            this.tabIncludeDirDir.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvIncPaths)).EndInit();
            this.tabIncludeDirLib.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvLibPaths)).EndInit();
            this.tabLibrary.ResumeLayout(false);
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.tabMemory.ResumeLayout(false);
            this.groupBox6.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvMemory)).EndInit();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.Output.ResumeLayout(false);
            this.groupBox9.ResumeLayout(false);
            this.groupBox9.PerformLayout();
            this.groupBox8.ResumeLayout(false);
            this.groupBox8.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabsMain;
        private System.Windows.Forms.TabPage tabGeneral;
        private System.Windows.Forms.TabPage tabInclude;
        private System.Windows.Forms.TabPage tabLibrary;
        private System.Windows.Forms.TabPage tabMemory;
        private System.Windows.Forms.TabPage Output;
        private System.Windows.Forms.TextBox txtOutputPath;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox listOptimization;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.CheckedListBox chklistOptions;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtOtherOptions;
        private System.Windows.Forms.TabControl tabsIncludeDir;
        private System.Windows.Forms.TabPage tabIncludeDirDir;
        private System.Windows.Forms.TabPage tabIncludeDirLib;
        private System.Windows.Forms.DataGridView dgvIncPaths;
        private System.Windows.Forms.DataGridView dgvLibPaths;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btnAddLibFile;
        private System.Windows.Forms.Button btnLibRemove;
        private System.Windows.Forms.Button btnAddLib;
        private System.Windows.Forms.ListBox listAvailLibs;
        private System.Windows.Forms.Button btnLibMoveUp;
        private System.Windows.Forms.Button btnLibMoveDown;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.ListBox listLinkObj;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.TextBox txtInitStackAddr;
        private System.Windows.Forms.CheckBox chkUseInitStack;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.DataGridView dgvMemory;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.TextBox txtLinkerOptions;
        private System.Windows.Forms.NumericUpDown numClockFreq;
        private System.Windows.Forms.Button btnIncDirAdd;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvIncPathTxtPath;
        private System.Windows.Forms.Button btnLibDirAdd;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvLibPathTxtPath;
        private System.Windows.Forms.DataGridViewComboBoxColumn dgvMemListType;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvMemTxtName;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvMemTxtAddr;
        private System.Windows.Forms.Button btnIncPathMoveDown;
        private System.Windows.Forms.Button btnIncPathMoveUp;
        private System.Windows.Forms.Button btnLibPathMoveDown;
        private System.Windows.Forms.Button btnLibPathMoveUp;
        private System.Windows.Forms.ComboBox dropDevices;
        private System.Windows.Forms.GroupBox grpBoxBurnerPanel;
        private System.Windows.Forms.Button btnGotoAppdata;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ComboBox dropTemplates;
        private System.Windows.Forms.Button btnApplyTemplate;
        private System.Windows.Forms.GroupBox groupBox8;
        private System.Windows.Forms.TextBox txtFavoriteDir;
        private System.Windows.Forms.Button btnFavoriteBrowse;
        private System.Windows.Forms.GroupBox groupBox9;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtArduinoLibs;
        private System.Windows.Forms.Button btnFindArduinoLibs;
        private System.Windows.Forms.TextBox txtArduinoCore;
        private System.Windows.Forms.Button btnFindArduinoCore;
        private System.Windows.Forms.Button btnDiscardAndClose;
        private System.Windows.Forms.Button btnSaveAndClose;
    }
}
