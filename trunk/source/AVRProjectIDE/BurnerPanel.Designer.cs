namespace AVRProjectIDE
{
    partial class BurnerPanel
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.chkAutoReset = new System.Windows.Forms.CheckBox();
            this.btnBurnOnlyOpt = new System.Windows.Forms.Button();
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
            this.SuspendLayout();
            // 
            // chkAutoReset
            // 
            this.chkAutoReset.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkAutoReset.AutoSize = true;
            this.chkAutoReset.Location = new System.Drawing.Point(315, 69);
            this.chkAutoReset.Name = "chkAutoReset";
            this.chkAutoReset.Size = new System.Drawing.Size(118, 17);
            this.chkAutoReset.TabIndex = 27;
            this.chkAutoReset.Text = "Arduino Auto Reset";
            this.chkAutoReset.UseVisualStyleBackColor = true;
            // 
            // btnBurnOnlyOpt
            // 
            this.btnBurnOnlyOpt.Location = new System.Drawing.Point(84, 93);
            this.btnBurnOnlyOpt.Name = "btnBurnOnlyOpt";
            this.btnBurnOnlyOpt.Size = new System.Drawing.Size(107, 23);
            this.btnBurnOnlyOpt.TabIndex = 26;
            this.btnBurnOnlyOpt.Text = "Run Only Options";
            this.btnBurnOnlyOpt.UseVisualStyleBackColor = true;
            this.btnBurnOnlyOpt.Click += new System.EventHandler(this.btnBurnOnlyOpt_Click);
            // 
            // txtBurnOpt
            // 
            this.txtBurnOpt.Location = new System.Drawing.Point(84, 67);
            this.txtBurnOpt.Name = "txtBurnOpt";
            this.txtBurnOpt.Size = new System.Drawing.Size(107, 20);
            this.txtBurnOpt.TabIndex = 25;
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
            this.dropBaud.Location = new System.Drawing.Point(326, 40);
            this.dropBaud.Name = "dropBaud";
            this.dropBaud.Size = new System.Drawing.Size(107, 21);
            this.dropBaud.TabIndex = 24;
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
            this.dropProg.Location = new System.Drawing.Point(84, 40);
            this.dropProg.Name = "dropProg";
            this.dropProg.Size = new System.Drawing.Size(107, 21);
            this.dropProg.TabIndex = 23;
            // 
            // dropPort
            // 
            this.dropPort.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.dropPort.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.dropPort.FormattingEnabled = true;
            this.dropPort.Location = new System.Drawing.Point(326, 13);
            this.dropPort.Name = "dropPort";
            this.dropPort.Size = new System.Drawing.Size(107, 21);
            this.dropPort.TabIndex = 22;
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
            this.dropPart.Location = new System.Drawing.Point(84, 13);
            this.dropPart.Name = "dropPart";
            this.dropPart.Size = new System.Drawing.Size(107, 21);
            this.dropPart.TabIndex = 21;
            // 
            // label11
            // 
            this.label11.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(219, 43);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(101, 13);
            this.label11.TabIndex = 18;
            this.label11.Text = "Baud Rate Overide:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(32, 70);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(46, 13);
            this.label9.TabIndex = 20;
            this.label9.Text = "Options:";
            // 
            // label10
            // 
            this.label10.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(251, 16);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(69, 13);
            this.label10.TabIndex = 16;
            this.label10.Text = "Port Overide:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(12, 43);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(66, 13);
            this.label8.TabIndex = 19;
            this.label8.Text = "Programmer:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(49, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 13);
            this.label1.TabIndex = 17;
            this.label1.Text = "Part:";
            // 
            // BurnerPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDark;
            this.Controls.Add(this.chkAutoReset);
            this.Controls.Add(this.btnBurnOnlyOpt);
            this.Controls.Add(this.txtBurnOpt);
            this.Controls.Add(this.dropBaud);
            this.Controls.Add(this.dropProg);
            this.Controls.Add(this.dropPort);
            this.Controls.Add(this.dropPart);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label1);
            this.Name = "BurnerPanel";
            this.Size = new System.Drawing.Size(446, 130);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox chkAutoReset;
        private System.Windows.Forms.Button btnBurnOnlyOpt;
        private System.Windows.Forms.TextBox txtBurnOpt;
        private System.Windows.Forms.ComboBox dropBaud;
        private System.Windows.Forms.ComboBox dropProg;
        private System.Windows.Forms.ComboBox dropPort;
        private System.Windows.Forms.ComboBox dropPart;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label1;
    }
}
