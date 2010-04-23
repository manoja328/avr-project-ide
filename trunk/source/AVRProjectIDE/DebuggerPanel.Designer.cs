namespace AVRProjectIDE
{
    partial class DebuggerPanel
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tbtnStartDebug = new System.Windows.Forms.ToolStripButton();
            this.tbtnStopDebug = new System.Windows.Forms.ToolStripButton();
            this.tbtnLoadELF = new System.Windows.Forms.ToolStripButton();
            this.tbtnReset = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tbtnBreak = new System.Windows.Forms.ToolStripButton();
            this.tbtnStepInto = new System.Windows.Forms.ToolStripButton();
            this.tbtnStepOver = new System.Windows.Forms.ToolStripButton();
            this.tbtnStepOut = new System.Windows.Forms.ToolStripButton();
            this.tbtnBreakpointsToggle = new System.Windows.Forms.ToolStripButton();
            this.tbtnGoto = new System.Windows.Forms.ToolStripButton();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.numServerPort = new System.Windows.Forms.NumericUpDown();
            this.txtGDBInput = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.dropAVRChips = new System.Windows.Forms.ComboBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.dropHardwareSelection = new System.Windows.Forms.ComboBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.numBitRate = new System.Windows.Forms.NumericUpDown();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.txtJTAGPort = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtAVaRICEOtherOpts = new System.Windows.Forms.TextBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.listRegisters = new System.Windows.Forms.ListView();
            this.colRegName = new System.Windows.Forms.ColumnHeader();
            this.colRegValue = new System.Windows.Forms.ColumnHeader();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.memoryViewPanel1 = new AVRProjectIDE.MemoryViewPanel();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.tabPage6 = new System.Windows.Forms.TabPage();
            this.tabPage7 = new System.Windows.Forms.TabPage();
            this.txtOutput = new System.Windows.Forms.TextBox();
            this.tmrReader = new System.Windows.Forms.Timer(this.components);
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numServerPort)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numBitRate)).BeginInit();
            this.groupBox4.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.toolStrip1);
            this.splitContainer1.Panel1.Controls.Add(this.tabControl1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.txtOutput);
            this.splitContainer1.Size = new System.Drawing.Size(454, 513);
            this.splitContainer1.SplitterDistance = 422;
            this.splitContainer1.TabIndex = 0;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tbtnStartDebug,
            this.tbtnStopDebug,
            this.tbtnLoadELF,
            this.tbtnReset,
            this.toolStripSeparator1,
            this.tbtnBreak,
            this.tbtnStepInto,
            this.tbtnStepOver,
            this.tbtnStepOut,
            this.tbtnBreakpointsToggle,
            this.tbtnGoto});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(454, 25);
            this.toolStrip1.TabIndex = 4;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tbtnStartDebug
            // 
            this.tbtnStartDebug.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tbtnStartDebug.Image = global::AVRProjectIDE.GraphicsResx.debug_play;
            this.tbtnStartDebug.ImageTransparentColor = System.Drawing.Color.Transparent;
            this.tbtnStartDebug.Name = "tbtnStartDebug";
            this.tbtnStartDebug.Size = new System.Drawing.Size(23, 22);
            this.tbtnStartDebug.Text = "Start Debug";
            // 
            // tbtnStopDebug
            // 
            this.tbtnStopDebug.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tbtnStopDebug.Image = global::AVRProjectIDE.GraphicsResx.debug_stop;
            this.tbtnStopDebug.ImageTransparentColor = System.Drawing.Color.Transparent;
            this.tbtnStopDebug.Name = "tbtnStopDebug";
            this.tbtnStopDebug.Size = new System.Drawing.Size(23, 22);
            this.tbtnStopDebug.Text = "Stop Debug";
            // 
            // tbtnLoadELF
            // 
            this.tbtnLoadELF.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tbtnLoadELF.Image = global::AVRProjectIDE.GraphicsResx.burn_img;
            this.tbtnLoadELF.ImageTransparentColor = System.Drawing.Color.Transparent;
            this.tbtnLoadELF.Name = "tbtnLoadELF";
            this.tbtnLoadELF.Size = new System.Drawing.Size(23, 22);
            this.tbtnLoadELF.Text = "Load ELF";
            // 
            // tbtnReset
            // 
            this.tbtnReset.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tbtnReset.Image = global::AVRProjectIDE.GraphicsResx.debug_reset;
            this.tbtnReset.ImageTransparentColor = System.Drawing.Color.Transparent;
            this.tbtnReset.Name = "tbtnReset";
            this.tbtnReset.Size = new System.Drawing.Size(23, 22);
            this.tbtnReset.Text = "Reset";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // tbtnBreak
            // 
            this.tbtnBreak.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tbtnBreak.Image = global::AVRProjectIDE.GraphicsResx.debug_pause;
            this.tbtnBreak.ImageTransparentColor = System.Drawing.Color.Transparent;
            this.tbtnBreak.Name = "tbtnBreak";
            this.tbtnBreak.Size = new System.Drawing.Size(23, 22);
            this.tbtnBreak.Text = "Break/Continue Execution";
            // 
            // tbtnStepInto
            // 
            this.tbtnStepInto.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tbtnStepInto.Image = global::AVRProjectIDE.GraphicsResx.debug_stepinto;
            this.tbtnStepInto.ImageTransparentColor = System.Drawing.Color.Transparent;
            this.tbtnStepInto.Name = "tbtnStepInto";
            this.tbtnStepInto.Size = new System.Drawing.Size(23, 22);
            this.tbtnStepInto.Text = "Step Into";
            // 
            // tbtnStepOver
            // 
            this.tbtnStepOver.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tbtnStepOver.Image = global::AVRProjectIDE.GraphicsResx.debug_stepover;
            this.tbtnStepOver.ImageTransparentColor = System.Drawing.Color.Transparent;
            this.tbtnStepOver.Name = "tbtnStepOver";
            this.tbtnStepOver.Size = new System.Drawing.Size(23, 22);
            this.tbtnStepOver.Text = "Step Over";
            // 
            // tbtnStepOut
            // 
            this.tbtnStepOut.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tbtnStepOut.Image = global::AVRProjectIDE.GraphicsResx.debug_stepout;
            this.tbtnStepOut.ImageTransparentColor = System.Drawing.Color.Transparent;
            this.tbtnStepOut.Name = "tbtnStepOut";
            this.tbtnStepOut.Size = new System.Drawing.Size(23, 22);
            this.tbtnStepOut.Text = "Step Out";
            // 
            // tbtnBreakpointsToggle
            // 
            this.tbtnBreakpointsToggle.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tbtnBreakpointsToggle.Image = global::AVRProjectIDE.GraphicsResx.debug_breakptstog;
            this.tbtnBreakpointsToggle.ImageTransparentColor = System.Drawing.Color.Transparent;
            this.tbtnBreakpointsToggle.Name = "tbtnBreakpointsToggle";
            this.tbtnBreakpointsToggle.Size = new System.Drawing.Size(23, 22);
            this.tbtnBreakpointsToggle.Text = "Enable/Disable Bookmarks as Breakpoints";
            // 
            // tbtnGoto
            // 
            this.tbtnGoto.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tbtnGoto.Image = global::AVRProjectIDE.GraphicsResx.debug_goto;
            this.tbtnGoto.ImageTransparentColor = System.Drawing.Color.Transparent;
            this.tbtnGoto.Name = "tbtnGoto";
            this.tbtnGoto.Size = new System.Drawing.Size(23, 22);
            this.tbtnGoto.Text = "Goto Location";
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Controls.Add(this.tabPage5);
            this.tabControl1.Controls.Add(this.tabPage6);
            this.tabControl1.Controls.Add(this.tabPage7);
            this.tabControl1.Location = new System.Drawing.Point(0, 28);
            this.tabControl1.Multiline = true;
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(454, 391);
            this.tabControl1.TabIndex = 3;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.groupBox6);
            this.tabPage1.Controls.Add(this.txtGDBInput);
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(446, 365);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Main";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 323);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(161, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Manually Enter GDB Commands:";
            // 
            // groupBox6
            // 
            this.groupBox6.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox6.Controls.Add(this.numServerPort);
            this.groupBox6.Location = new System.Drawing.Point(219, 6);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(221, 60);
            this.groupBox6.TabIndex = 1;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "GDB Server Port";
            // 
            // numServerPort
            // 
            this.numServerPort.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.numServerPort.Location = new System.Drawing.Point(4, 34);
            this.numServerPort.Name = "numServerPort";
            this.numServerPort.Size = new System.Drawing.Size(215, 20);
            this.numServerPort.TabIndex = 0;
            // 
            // txtGDBInput
            // 
            this.txtGDBInput.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtGDBInput.BackColor = System.Drawing.Color.Black;
            this.txtGDBInput.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtGDBInput.ForeColor = System.Drawing.Color.White;
            this.txtGDBInput.Location = new System.Drawing.Point(6, 339);
            this.txtGDBInput.Name = "txtGDBInput";
            this.txtGDBInput.Size = new System.Drawing.Size(434, 20);
            this.txtGDBInput.TabIndex = 4;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.groupBox7);
            this.groupBox1.Controls.Add(this.groupBox3);
            this.groupBox1.Controls.Add(this.groupBox5);
            this.groupBox1.Controls.Add(this.groupBox4);
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Location = new System.Drawing.Point(8, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(205, 261);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "AVaRICE";
            // 
            // groupBox7
            // 
            this.groupBox7.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox7.Controls.Add(this.dropAVRChips);
            this.groupBox7.Location = new System.Drawing.Point(6, 209);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(193, 43);
            this.groupBox7.TabIndex = 4;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "AVR Chip Selection";
            // 
            // dropAVRChips
            // 
            this.dropAVRChips.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dropAVRChips.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.dropAVRChips.FormattingEnabled = true;
            this.dropAVRChips.Location = new System.Drawing.Point(3, 16);
            this.dropAVRChips.Name = "dropAVRChips";
            this.dropAVRChips.Size = new System.Drawing.Size(187, 21);
            this.dropAVRChips.TabIndex = 0;
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.dropHardwareSelection);
            this.groupBox3.Location = new System.Drawing.Point(6, 66);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(193, 43);
            this.groupBox3.TabIndex = 1;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Debugger Selection";
            // 
            // dropHardwareSelection
            // 
            this.dropHardwareSelection.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dropHardwareSelection.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.dropHardwareSelection.FormattingEnabled = true;
            this.dropHardwareSelection.Items.AddRange(new object[] {
            "JTAG ICE mkI",
            "JTAG ICE mkII",
            "JTAG ICE mkII + debugWIRE",
            "AVR Dragon",
            "AVR Dragon + debugWIRE"});
            this.dropHardwareSelection.Location = new System.Drawing.Point(3, 16);
            this.dropHardwareSelection.Name = "dropHardwareSelection";
            this.dropHardwareSelection.Size = new System.Drawing.Size(187, 21);
            this.dropHardwareSelection.TabIndex = 0;
            // 
            // groupBox5
            // 
            this.groupBox5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox5.Controls.Add(this.numBitRate);
            this.groupBox5.Location = new System.Drawing.Point(6, 162);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(193, 41);
            this.groupBox5.TabIndex = 3;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "--jtag-bitrate <bitrate>";
            // 
            // numBitRate
            // 
            this.numBitRate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.numBitRate.Location = new System.Drawing.Point(3, 16);
            this.numBitRate.Maximum = new decimal(new int[] {
            6400,
            0,
            0,
            0});
            this.numBitRate.Minimum = new decimal(new int[] {
            22,
            0,
            0,
            0});
            this.numBitRate.Name = "numBitRate";
            this.numBitRate.Size = new System.Drawing.Size(187, 20);
            this.numBitRate.TabIndex = 0;
            this.numBitRate.Value = new decimal(new int[] {
            250,
            0,
            0,
            0});
            // 
            // groupBox4
            // 
            this.groupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox4.Controls.Add(this.txtJTAGPort);
            this.groupBox4.Location = new System.Drawing.Point(6, 115);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(193, 41);
            this.groupBox4.TabIndex = 2;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "--jtag <port>";
            // 
            // txtJTAGPort
            // 
            this.txtJTAGPort.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtJTAGPort.Location = new System.Drawing.Point(3, 16);
            this.txtJTAGPort.Name = "txtJTAGPort";
            this.txtJTAGPort.Size = new System.Drawing.Size(187, 20);
            this.txtJTAGPort.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.txtAVaRICEOtherOpts);
            this.groupBox2.Location = new System.Drawing.Point(6, 19);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(193, 41);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Other Options";
            // 
            // txtAVaRICEOtherOpts
            // 
            this.txtAVaRICEOtherOpts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtAVaRICEOtherOpts.Location = new System.Drawing.Point(3, 16);
            this.txtAVaRICEOtherOpts.Name = "txtAVaRICEOtherOpts";
            this.txtAVaRICEOtherOpts.Size = new System.Drawing.Size(187, 20);
            this.txtAVaRICEOtherOpts.TabIndex = 0;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.listRegisters);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(446, 365);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Registers";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // listRegisters
            // 
            this.listRegisters.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colRegName,
            this.colRegValue});
            this.listRegisters.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listRegisters.GridLines = true;
            this.listRegisters.Location = new System.Drawing.Point(3, 3);
            this.listRegisters.MultiSelect = false;
            this.listRegisters.Name = "listRegisters";
            this.listRegisters.Size = new System.Drawing.Size(440, 359);
            this.listRegisters.TabIndex = 0;
            this.listRegisters.UseCompatibleStateImageBehavior = false;
            this.listRegisters.View = System.Windows.Forms.View.Details;
            // 
            // colRegName
            // 
            this.colRegName.Text = "Register Name";
            this.colRegName.Width = 109;
            // 
            // colRegValue
            // 
            this.colRegValue.Text = "Value";
            this.colRegValue.Width = 140;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.memoryViewPanel1);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(446, 365);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "RAM";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // memoryViewPanel1
            // 
            this.memoryViewPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.memoryViewPanel1.Location = new System.Drawing.Point(3, 3);
            this.memoryViewPanel1.Name = "memoryViewPanel1";
            this.memoryViewPanel1.ResetValue = ((byte)(0));
            this.memoryViewPanel1.Size = new System.Drawing.Size(440, 359);
            this.memoryViewPanel1.TabIndex = 0;
            // 
            // tabPage4
            // 
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(446, 365);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "EEPROM";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // tabPage5
            // 
            this.tabPage5.Location = new System.Drawing.Point(4, 22);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage5.Size = new System.Drawing.Size(446, 365);
            this.tabPage5.TabIndex = 4;
            this.tabPage5.Text = "Flash Memory";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // tabPage6
            // 
            this.tabPage6.Location = new System.Drawing.Point(4, 22);
            this.tabPage6.Name = "tabPage6";
            this.tabPage6.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage6.Size = new System.Drawing.Size(446, 365);
            this.tabPage6.TabIndex = 5;
            this.tabPage6.Text = "Stack";
            this.tabPage6.UseVisualStyleBackColor = true;
            // 
            // tabPage7
            // 
            this.tabPage7.Location = new System.Drawing.Point(4, 22);
            this.tabPage7.Name = "tabPage7";
            this.tabPage7.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage7.Size = new System.Drawing.Size(446, 365);
            this.tabPage7.TabIndex = 6;
            this.tabPage7.Text = "Watch";
            this.tabPage7.UseVisualStyleBackColor = true;
            // 
            // txtOutput
            // 
            this.txtOutput.BackColor = System.Drawing.Color.Black;
            this.txtOutput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtOutput.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtOutput.ForeColor = System.Drawing.Color.White;
            this.txtOutput.Location = new System.Drawing.Point(0, 0);
            this.txtOutput.Multiline = true;
            this.txtOutput.Name = "txtOutput";
            this.txtOutput.ReadOnly = true;
            this.txtOutput.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtOutput.Size = new System.Drawing.Size(454, 87);
            this.txtOutput.TabIndex = 0;
            // 
            // tmrReader
            // 
            this.tmrReader.Enabled = true;
            this.tmrReader.Interval = 250;
            this.tmrReader.Tick += new System.EventHandler(this.tmrReader_Tick);
            // 
            // DebuggerPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(454, 513);
            this.CloseButton = false;
            this.CloseButtonVisible = false;
            this.Controls.Add(this.splitContainer1);
            this.DockAreas = ((WeifenLuo.WinFormsUI.Docking.DockAreas)(((((WeifenLuo.WinFormsUI.Docking.DockAreas.Float | WeifenLuo.WinFormsUI.Docking.DockAreas.DockLeft)
                        | WeifenLuo.WinFormsUI.Docking.DockAreas.DockRight)
                        | WeifenLuo.WinFormsUI.Docking.DockAreas.DockTop)
                        | WeifenLuo.WinFormsUI.Docking.DockAreas.DockBottom)));
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DebuggerPanel";
            this.ShowInTaskbar = false;
            this.TabText = "Debugger (DOES NOT WORK YET)";
            this.Text = "Debugger (DOES NOT WORK YET)";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.DebuggerPanel_FormClosing);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            this.splitContainer1.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numServerPort)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox7.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numBitRate)).EndInit();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TextBox txtOutput;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtAVaRICEOtherOpts;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ComboBox dropHardwareSelection;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.TextBox txtJTAGPort;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.NumericUpDown numBitRate;
        private System.Windows.Forms.TextBox txtGDBInput;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.NumericUpDown numServerPort;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.ComboBox dropAVRChips;
        private System.Windows.Forms.ToolStripButton tbtnStartDebug;
        private System.Windows.Forms.ToolStripButton tbtnStopDebug;
        private System.Windows.Forms.ToolStripButton tbtnLoadELF;
        private System.Windows.Forms.ToolStripButton tbtnReset;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton tbtnBreak;
        private System.Windows.Forms.ToolStripButton tbtnStepInto;
        private System.Windows.Forms.ToolStripButton tbtnStepOver;
        private System.Windows.Forms.ToolStripButton tbtnStepOut;
        private System.Windows.Forms.ToolStripButton tbtnBreakpointsToggle;
        private System.Windows.Forms.ToolStripButton tbtnGoto;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.TabPage tabPage5;
        private System.Windows.Forms.TabPage tabPage6;
        private System.Windows.Forms.ListView listRegisters;
        private System.Windows.Forms.ColumnHeader colRegName;
        private System.Windows.Forms.ColumnHeader colRegValue;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabPage tabPage7;
        private MemoryViewPanel memoryViewPanel1;
        private System.Windows.Forms.Timer tmrReader;
    }
}