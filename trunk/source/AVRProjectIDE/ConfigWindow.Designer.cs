namespace AVRProjectIDE
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tabsMain = new System.Windows.Forms.TabControl();
            this.tabGeneral = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lnkFuse = new System.Windows.Forms.LinkLabel();
            this.chkAutoReset = new System.Windows.Forms.CheckBox();
            this.btnBurnFuseBox = new System.Windows.Forms.Button();
            this.btnBurnOnlyOpt = new System.Windows.Forms.Button();
            this.txtBurnFuseBox = new System.Windows.Forms.TextBox();
            this.txtBurnOpt = new System.Windows.Forms.TextBox();
            this.dropBaud = new System.Windows.Forms.ComboBox();
            this.dropProg = new System.Windows.Forms.ComboBox();
            this.dropPort = new System.Windows.Forms.ComboBox();
            this.dropPart = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
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
            this.tabsMain.SuspendLayout();
            this.tabGeneral.SuspendLayout();
            this.groupBox1.SuspendLayout();
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
            this.tabsMain.Controls.Add(this.tabGeneral);
            this.tabsMain.Controls.Add(this.tabInclude);
            this.tabsMain.Controls.Add(this.tabLibrary);
            this.tabsMain.Controls.Add(this.tabMemory);
            this.tabsMain.Controls.Add(this.Output);
            this.tabsMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabsMain.Location = new System.Drawing.Point(0, 0);
            this.tabsMain.Name = "tabsMain";
            this.tabsMain.SelectedIndex = 0;
            this.tabsMain.Size = new System.Drawing.Size(712, 353);
            this.tabsMain.TabIndex = 11;
            // 
            // tabGeneral
            // 
            this.tabGeneral.Controls.Add(this.groupBox1);
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
            this.tabGeneral.Size = new System.Drawing.Size(704, 327);
            this.tabGeneral.TabIndex = 1;
            this.tabGeneral.Text = "General";
            this.tabGeneral.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox1.Controls.Add(this.lnkFuse);
            this.groupBox1.Controls.Add(this.chkAutoReset);
            this.groupBox1.Controls.Add(this.btnBurnFuseBox);
            this.groupBox1.Controls.Add(this.btnBurnOnlyOpt);
            this.groupBox1.Controls.Add(this.txtBurnFuseBox);
            this.groupBox1.Controls.Add(this.txtBurnOpt);
            this.groupBox1.Controls.Add(this.dropBaud);
            this.groupBox1.Controls.Add(this.dropProg);
            this.groupBox1.Controls.Add(this.dropPort);
            this.groupBox1.Controls.Add(this.dropPart);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(8, 184);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(487, 135);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "AVRDUDE Command Builder";
            // 
            // lnkFuse
            // 
            this.lnkFuse.AutoSize = true;
            this.lnkFuse.Location = new System.Drawing.Point(308, 76);
            this.lnkFuse.Name = "lnkFuse";
            this.lnkFuse.Size = new System.Drawing.Size(54, 13);
            this.lnkFuse.TabIndex = 12;
            this.lnkFuse.TabStop = true;
            this.lnkFuse.Text = "Fuse Box:";
            this.lnkFuse.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkFuse_LinkClicked);
            // 
            // chkAutoReset
            // 
            this.chkAutoReset.AutoSize = true;
            this.chkAutoReset.Location = new System.Drawing.Point(218, 103);
            this.chkAutoReset.Name = "chkAutoReset";
            this.chkAutoReset.Size = new System.Drawing.Size(118, 17);
            this.chkAutoReset.TabIndex = 11;
            this.chkAutoReset.Text = "Arduino Auto Reset";
            this.chkAutoReset.UseVisualStyleBackColor = true;
            // 
            // btnBurnFuseBox
            // 
            this.btnBurnFuseBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBurnFuseBox.Location = new System.Drawing.Point(368, 99);
            this.btnBurnFuseBox.Name = "btnBurnFuseBox";
            this.btnBurnFuseBox.Size = new System.Drawing.Size(107, 23);
            this.btnBurnFuseBox.TabIndex = 10;
            this.btnBurnFuseBox.Text = "Burn Fuses";
            this.btnBurnFuseBox.UseVisualStyleBackColor = true;
            this.btnBurnFuseBox.Click += new System.EventHandler(this.btnBurnFuseBox_Click);
            // 
            // btnBurnOnlyOpt
            // 
            this.btnBurnOnlyOpt.Location = new System.Drawing.Point(79, 99);
            this.btnBurnOnlyOpt.Name = "btnBurnOnlyOpt";
            this.btnBurnOnlyOpt.Size = new System.Drawing.Size(107, 23);
            this.btnBurnOnlyOpt.TabIndex = 10;
            this.btnBurnOnlyOpt.Text = "Run Only Options";
            this.btnBurnOnlyOpt.UseVisualStyleBackColor = true;
            this.btnBurnOnlyOpt.Click += new System.EventHandler(this.btnBurnOnlyOpt_Click);
            // 
            // txtBurnFuseBox
            // 
            this.txtBurnFuseBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtBurnFuseBox.Location = new System.Drawing.Point(368, 73);
            this.txtBurnFuseBox.Name = "txtBurnFuseBox";
            this.txtBurnFuseBox.Size = new System.Drawing.Size(107, 20);
            this.txtBurnFuseBox.TabIndex = 9;
            // 
            // txtBurnOpt
            // 
            this.txtBurnOpt.Location = new System.Drawing.Point(79, 73);
            this.txtBurnOpt.Name = "txtBurnOpt";
            this.txtBurnOpt.Size = new System.Drawing.Size(107, 20);
            this.txtBurnOpt.TabIndex = 9;
            // 
            // dropBaud
            // 
            this.dropBaud.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.dropBaud.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.dropBaud.FormattingEnabled = true;
            this.dropBaud.Items.AddRange(new object[] {
            "No Override",
            "300",
            "600",
            "1200",
            "2400",
            "4800",
            "9600",
            "14400",
            "19200",
            "28800",
            "38400",
            "57600",
            "76800",
            "115200",
            "230400"});
            this.dropBaud.Location = new System.Drawing.Point(368, 46);
            this.dropBaud.Name = "dropBaud";
            this.dropBaud.Size = new System.Drawing.Size(107, 21);
            this.dropBaud.TabIndex = 8;
            // 
            // dropProg
            // 
            this.dropProg.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.dropProg.FormattingEnabled = true;
            this.dropProg.Items.AddRange(new object[] {
            "c2n232i  = serial ",
            "dasa3    = serial ",
            "dasa     = serial ",
            "siprog   = Lancos ",
            "ponyser  = design ",
            "89isp    = Atmel a",
            "frank-stk200 = Fra",
            "blaster  = Altera ",
            "ere-isp-avr = ERE ",
            "atisp    = AT-ISP ",
            "dapa     = Direct ",
            "xil      = Xilinx ",
            "futurlec = Futurle",
            "abcmini  = ABCmini",
            "picoweb  = Picoweb",
            "sp12     = Steve B",
            "alf      = Nightsh",
            "bascom   = Bascom ",
            "dt006    = Dontron",
            "pony-stk200 = Pony",
            "stk200   = STK200 ",
            "bsd      = Brian D",
            "pavr     = Jason K",
            "dragon_dw = Atmel ",
            "dragon_hvsp = Atme",
            "dragon_pp = Atmel ",
            "dragon_isp = Atmel",
            "dragon_jtag = Atme",
            "jtag2dw  = Atmel J",
            "jtag2isp = Atmel J",
            "jtag2    = Atmel J",
            "jtag2fast = Atmel ",
            "jtag2slow = Atmel ",
            "jtagmkII = Atmel J",
            "jtag1slow = Atmel ",
            "jtag1    = Atmel J",
            "jtagmkI  = Atmel J",
            "avr911   = Atmel A",
            "avr109   = Atmel A",
            "butterfly = Atmel ",
            "usbtiny  = USBtiny",
            "usbasp   = USBasp,",
            "avr910   = Atmel L",
            "stk600hvsp = Atmel",
            "stk600pp = Atmel S",
            "stk600   = Atmel S",
            "stk500hvsp = Atmel",
            "stk500pp = Atmel S",
            "stk500v2 = Atmel S",
            "mib510   = Crossbo",
            "stk500v1 = Atmel S",
            "stk500   = Atmel S",
            "avrisp2  = Atmel A",
            "avrispmkII = Atmel",
            "avrispv2 = Atmel A",
            "avrisp   = Atmel A",
            "arduino  = Arduino"});
            this.dropProg.Location = new System.Drawing.Point(79, 46);
            this.dropProg.Name = "dropProg";
            this.dropProg.Size = new System.Drawing.Size(107, 21);
            this.dropProg.TabIndex = 8;
            // 
            // dropPort
            // 
            this.dropPort.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.dropPort.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.dropPort.FormattingEnabled = true;
            this.dropPort.Location = new System.Drawing.Point(368, 19);
            this.dropPort.Name = "dropPort";
            this.dropPort.Size = new System.Drawing.Size(107, 21);
            this.dropPort.TabIndex = 7;
            // 
            // dropPart
            // 
            this.dropPart.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.dropPart.FormattingEnabled = true;
            this.dropPart.Items.AddRange(new object[] {
            " x256a3 = ATXMEGA256A3    [C:\\",
            " x128a1 = ATXMEGA128A1    [C:\\",
            " x128a1d = ATXMEGA128A1REVD [C",
            " m6450 = ATMEGA6450      [C:\\W",
            " m3250 = ATMEGA3250      [C:\\W",
            " m645 = ATMEGA645       [C:\\Wi",
            " m325 = ATMEGA325       [C:\\Wi",
            " usb82 = AT90USB82       [C:\\W",
            " usb162 = AT90USB162      [C:\\",
            " usb1287 = AT90USB1287     [C:",
            " usb1286 = AT90USB1286     [C:",
            " usb647 = AT90USB647      [C:\\",
            " usb646 = AT90USB646      [C:\\",
            " t84  = ATtiny84        [C:\\Wi",
            " t44  = ATtiny44        [C:\\Wi",
            " t24  = ATtiny24        [C:\\Wi",
            " m128rfa1 = ATMEGA128RFA1   [C",
            " m2561 = ATMEGA2561      [C:\\W",
            " m2560 = ATMEGA2560      [C:\\W",
            " m1281 = ATMEGA1281      [C:\\W",
            " m1280 = ATMEGA1280      [C:\\W",
            " m640 = ATMEGA640       [C:\\Wi",
            " t85  = ATtiny85        [C:\\Wi",
            " t45  = ATtiny45        [C:\\Wi",
            " t25  = ATtiny25        [C:\\Wi",
            " pwm3b = AT90PWM3B       [C:\\W",
            " pwm2b = AT90PWM2B       [C:\\W",
            " pwm3 = AT90PWM3        [C:\\Wi",
            " pwm2 = AT90PWM2        [C:\\Wi",
            " t2313 = ATtiny2313      [C:\\W",
            " m328p = ATMEGA328P      [C:\\W",
            " t88  = attiny88        [C:\\Wi",
            " m168 = ATMEGA168       [C:\\Wi",
            " m88  = ATMEGA88        [C:\\Wi",
            " m48  = ATMEGA48        [C:\\Wi",
            " t861 = ATTINY861       [C:\\Wi",
            " t461 = ATTINY461       [C:\\Wi",
            " t261 = ATTINY261       [C:\\Wi",
            " t26  = ATTINY26        [C:\\Wi",
            " m8535 = ATMEGA8535      [C:\\W",
            " m8515 = ATMEGA8515      [C:\\W",
            " m8   = ATMEGA8         [C:\\Wi",
            " m161 = ATMEGA161       [C:\\Wi",
            " m32  = ATMEGA32        [C:\\Wi",
            " m6490 = ATMEGA6490      [C:\\W",
            " m649 = ATMEGA649       [C:\\Wi",
            " m3290p = ATMEGA3290P     [C:\\",
            " m3290 = ATMEGA3290      [C:\\W",
            " m329p = ATMEGA329P      [C:\\W",
            " m329 = ATMEGA329       [C:\\Wi",
            " m169 = ATMEGA169       [C:\\Wi",
            " m163 = ATMEGA163       [C:\\Wi",
            " m162 = ATMEGA162       [C:\\Wi",
            " m1284p = ATMEGA1284P     [C:\\",
            " m644p = ATMEGA644P      [C:\\W",
            " m644 = ATMEGA644       [C:\\Wi",
            " m324p = ATMEGA324P      [C:\\W",
            " m164p = ATMEGA164P      [C:\\W",
            " m16  = ATMEGA16        [C:\\Wi",
            " c32  = AT90CAN32       [C:\\Wi",
            " c64  = AT90CAN64       [C:\\Wi",
            " c128 = AT90CAN128      [C:\\Wi",
            " m128 = ATMEGA128       [C:\\Wi",
            " m64  = ATMEGA64        [C:\\Wi",
            " m103 = ATMEGA103       [C:\\Wi",
            " 8535 = AT90S8535       [C:\\Wi",
            " 8515 = AT90S8515       [C:\\Wi",
            " 4434 = AT90S4434       [C:\\Wi",
            " 4433 = AT90S4433       [C:\\Wi",
            " 2343 = AT90S2343       [C:\\Wi",
            " 2333 = AT90S2333       [C:\\Wi",
            " 2313 = AT90S2313       [C:\\Wi",
            " 4414 = AT90S4414       [C:\\Wi",
            " 1200 = AT90S1200       [C:\\Wi",
            " t15  = ATtiny15        [C:\\Wi",
            " t13  = ATtiny13        [C:\\Wi",
            " t12  = ATtiny12        [C:\\Wi",
            " t11  = ATtiny11        [C:\\Wi"});
            this.dropPart.Location = new System.Drawing.Point(79, 19);
            this.dropPart.Name = "dropPart";
            this.dropPart.Size = new System.Drawing.Size(107, 21);
            this.dropPart.TabIndex = 7;
            // 
            // label11
            // 
            this.label11.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(261, 49);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(101, 13);
            this.label11.TabIndex = 1;
            this.label11.Text = "Baud Rate Overide:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(27, 76);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(46, 13);
            this.label9.TabIndex = 2;
            this.label9.Text = "Options:";
            // 
            // label10
            // 
            this.label10.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(293, 22);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(69, 13);
            this.label10.TabIndex = 0;
            this.label10.Text = "Port Overide:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(7, 49);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(66, 13);
            this.label8.TabIndex = 1;
            this.label8.Text = "Programmer:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(44, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Part:";
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
            this.numClockFreq.TabIndex = 2;
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
            this.chklistOptions.TabIndex = 5;
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
            this.dropDevices.TabIndex = 1;
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
            this.listOptimization.TabIndex = 3;
            // 
            // txtOtherOptions
            // 
            this.txtOtherOptions.Location = new System.Drawing.Point(94, 114);
            this.txtOtherOptions.Name = "txtOtherOptions";
            this.txtOtherOptions.Size = new System.Drawing.Size(401, 20);
            this.txtOtherOptions.TabIndex = 4;
            // 
            // txtOutputPath
            // 
            this.txtOutputPath.Location = new System.Drawing.Point(94, 6);
            this.txtOutputPath.Name = "txtOutputPath";
            this.txtOutputPath.Size = new System.Drawing.Size(100, 20);
            this.txtOutputPath.TabIndex = 0;
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
            this.tabInclude.Size = new System.Drawing.Size(704, 327);
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
            this.tabsIncludeDir.Size = new System.Drawing.Size(698, 321);
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
            this.tabIncludeDirDir.Size = new System.Drawing.Size(690, 295);
            this.tabIncludeDirDir.TabIndex = 0;
            this.tabIncludeDirDir.Text = "Include Paths";
            this.tabIncludeDirDir.UseVisualStyleBackColor = true;
            // 
            // btnIncPathMoveDown
            // 
            this.btnIncPathMoveDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnIncPathMoveDown.Location = new System.Drawing.Point(168, 266);
            this.btnIncPathMoveDown.Name = "btnIncPathMoveDown";
            this.btnIncPathMoveDown.Size = new System.Drawing.Size(75, 23);
            this.btnIncPathMoveDown.TabIndex = 3;
            this.btnIncPathMoveDown.Text = "Move Down";
            this.btnIncPathMoveDown.UseVisualStyleBackColor = true;
            this.btnIncPathMoveDown.Click += new System.EventHandler(this.btnIncPathMoveDown_Click);
            // 
            // btnIncPathMoveUp
            // 
            this.btnIncPathMoveUp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnIncPathMoveUp.Location = new System.Drawing.Point(87, 266);
            this.btnIncPathMoveUp.Name = "btnIncPathMoveUp";
            this.btnIncPathMoveUp.Size = new System.Drawing.Size(75, 23);
            this.btnIncPathMoveUp.TabIndex = 2;
            this.btnIncPathMoveUp.Text = "Move Up";
            this.btnIncPathMoveUp.UseVisualStyleBackColor = true;
            this.btnIncPathMoveUp.Click += new System.EventHandler(this.btnIncPathMoveUp_Click);
            // 
            // btnIncDirAdd
            // 
            this.btnIncDirAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnIncDirAdd.Location = new System.Drawing.Point(6, 266);
            this.btnIncDirAdd.Name = "btnIncDirAdd";
            this.btnIncDirAdd.Size = new System.Drawing.Size(75, 23);
            this.btnIncDirAdd.TabIndex = 0;
            this.btnIncDirAdd.Text = "Find";
            this.btnIncDirAdd.UseVisualStyleBackColor = true;
            this.btnIncDirAdd.Click += new System.EventHandler(this.btnIncDirAdd_Click);
            // 
            // dgvIncPaths
            // 
            this.dgvIncPaths.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvIncPaths.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvIncPaths.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvIncPaths.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dgvIncPathTxtPath});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvIncPaths.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvIncPaths.Location = new System.Drawing.Point(3, 3);
            this.dgvIncPaths.MultiSelect = false;
            this.dgvIncPaths.Name = "dgvIncPaths";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvIncPaths.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvIncPaths.Size = new System.Drawing.Size(684, 257);
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
            this.btnLibPathMoveDown.TabIndex = 2;
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
            this.btnLibPathMoveUp.TabIndex = 1;
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
            this.btnLibDirAdd.TabIndex = 0;
            this.btnLibDirAdd.Text = "Find";
            this.btnLibDirAdd.UseVisualStyleBackColor = true;
            this.btnLibDirAdd.Click += new System.EventHandler(this.btnLibDirAdd_Click);
            // 
            // dgvLibPaths
            // 
            this.dgvLibPaths.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvLibPaths.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvLibPaths.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvLibPaths.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dgvLibPathTxtPath});
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvLibPaths.DefaultCellStyle = dataGridViewCellStyle5;
            this.dgvLibPaths.Location = new System.Drawing.Point(3, 3);
            this.dgvLibPaths.MultiSelect = false;
            this.dgvLibPaths.Name = "dgvLibPaths";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvLibPaths.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
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
            this.tabLibrary.Size = new System.Drawing.Size(704, 327);
            this.tabLibrary.TabIndex = 3;
            this.tabLibrary.Text = "Libraries";
            this.tabLibrary.UseVisualStyleBackColor = true;
            // 
            // groupBox7
            // 
            this.groupBox7.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox7.Controls.Add(this.txtLinkerOptions);
            this.groupBox7.Location = new System.Drawing.Point(6, 271);
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
            this.groupBox4.Size = new System.Drawing.Size(435, 259);
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
            this.listLinkObj.Size = new System.Drawing.Size(429, 238);
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
            this.groupBox3.Size = new System.Drawing.Size(170, 259);
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
            this.listAvailLibs.Size = new System.Drawing.Size(164, 238);
            this.listAvailLibs.TabIndex = 0;
            // 
            // tabMemory
            // 
            this.tabMemory.Controls.Add(this.groupBox6);
            this.tabMemory.Controls.Add(this.groupBox5);
            this.tabMemory.Location = new System.Drawing.Point(4, 22);
            this.tabMemory.Name = "tabMemory";
            this.tabMemory.Padding = new System.Windows.Forms.Padding(3);
            this.tabMemory.Size = new System.Drawing.Size(704, 327);
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
            this.groupBox6.Size = new System.Drawing.Size(693, 259);
            this.groupBox6.TabIndex = 1;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Memory Segments";
            // 
            // dgvMemory
            // 
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvMemory.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle7;
            this.dgvMemory.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMemory.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dgvMemListType,
            this.dgvMemTxtName,
            this.dgvMemTxtAddr});
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvMemory.DefaultCellStyle = dataGridViewCellStyle8;
            this.dgvMemory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvMemory.Location = new System.Drawing.Point(3, 16);
            this.dgvMemory.Name = "dgvMemory";
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle9.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle9.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvMemory.RowHeadersDefaultCellStyle = dataGridViewCellStyle9;
            this.dgvMemory.Size = new System.Drawing.Size(687, 240);
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
            this.groupBox5.Location = new System.Drawing.Point(6, 271);
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
            this.Output.Size = new System.Drawing.Size(704, 327);
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
            this.txtArduinoLibs.TabIndex = 1;
            // 
            // btnFindArduinoLibs
            // 
            this.btnFindArduinoLibs.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFindArduinoLibs.Location = new System.Drawing.Point(607, 43);
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
            this.txtArduinoCore.Size = new System.Drawing.Size(524, 20);
            this.txtArduinoCore.TabIndex = 1;
            // 
            // btnFindArduinoCore
            // 
            this.btnFindArduinoCore.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFindArduinoCore.Location = new System.Drawing.Point(607, 17);
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
            this.txtFavoriteDir.TabIndex = 1;
            // 
            // btnFavoriteBrowse
            // 
            this.btnFavoriteBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFavoriteBrowse.Location = new System.Drawing.Point(607, 17);
            this.btnFavoriteBrowse.Name = "btnFavoriteBrowse";
            this.btnFavoriteBrowse.Size = new System.Drawing.Size(75, 23);
            this.btnFavoriteBrowse.TabIndex = 0;
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
            this.btnApplyTemplate.TabIndex = 1;
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
            this.dropTemplates.TabIndex = 0;
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
            // ConfigWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(712, 353);
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
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
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
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnBurnOnlyOpt;
        private System.Windows.Forms.TextBox txtBurnOpt;
        private System.Windows.Forms.ComboBox dropProg;
        private System.Windows.Forms.ComboBox dropPart;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnGotoAppdata;
        private System.Windows.Forms.ComboBox dropBaud;
        private System.Windows.Forms.ComboBox dropPort;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
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
        private System.Windows.Forms.CheckBox chkAutoReset;
        private System.Windows.Forms.Button btnBurnFuseBox;
        private System.Windows.Forms.TextBox txtBurnFuseBox;
        private System.Windows.Forms.LinkLabel lnkFuse;
    }
}

