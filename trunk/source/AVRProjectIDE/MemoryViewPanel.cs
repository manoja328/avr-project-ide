using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AVRProjectIDE
{
    public class MemoryViewPanel : UserControl
    {
        private Dictionary<int, byte> data = new Dictionary<int, byte>();
        private int maxAddr = 0;
        private TextBox textBox1;
        private VScrollBar vScrollBar1;
        private ComboBox dropViewMode;
        private byte rstVal = 0;

        private int viewStart = 0;

        public MemoryViewPanel()
        {
            InitializeComponent();

            this.dropViewMode.SelectedIndex = 0;
        }

        public byte ResetValue
        {
            get { return rstVal; }
            set { rstVal = value; }
        }

        public void SetValue(int addr, byte value)
        {
            for (int i = maxAddr; i < addr; i++)
            {
                data.Add(i, this.rstVal);
            }
        }

        public void Reset()
        {
            data.Clear();
        }

        private void InitializeComponent()
        {
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.vScrollBar1 = new System.Windows.Forms.VScrollBar();
            this.dropViewMode = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox1.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.Location = new System.Drawing.Point(3, 3);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.textBox1.Size = new System.Drawing.Size(356, 206);
            this.textBox1.TabIndex = 0;
            this.textBox1.WordWrap = false;
            this.textBox1.Click += new System.EventHandler(this.textBox1_Click);
            this.textBox1.KeyDown += new KeyEventHandler(textBox1_KeyDown);
            // 
            // vScrollBar1
            // 
            this.vScrollBar1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.vScrollBar1.LargeChange = 2;
            this.vScrollBar1.Location = new System.Drawing.Point(362, 3);
            this.vScrollBar1.Maximum = 4;
            this.vScrollBar1.Minimum = 1;
            this.vScrollBar1.Name = "vScrollBar1";
            this.vScrollBar1.Size = new System.Drawing.Size(17, 187);
            this.vScrollBar1.TabIndex = 1;
            this.vScrollBar1.Value = 2;
            this.vScrollBar1.Scroll += new System.Windows.Forms.ScrollEventHandler(this.vScrollBar1_Scroll);
            // 
            // dropViewMode
            // 
            this.dropViewMode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.dropViewMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.dropViewMode.FormattingEnabled = true;
            this.dropViewMode.Items.AddRange(new object[] {
            "Hexadecimal",
            "ASCII"});
            this.dropViewMode.Location = new System.Drawing.Point(3, 215);
            this.dropViewMode.Name = "dropViewMode";
            this.dropViewMode.Size = new System.Drawing.Size(121, 21);
            this.dropViewMode.TabIndex = 2;
            this.dropViewMode.SelectedIndexChanged += new System.EventHandler(this.dropViewMode_SelectedIndexChanged);
            // 
            // MemoryViewPanel
            // 
            this.Controls.Add(this.dropViewMode);
            this.Controls.Add(this.vScrollBar1);
            this.Controls.Add(this.textBox1);
            this.Name = "MemoryViewPanel";
            this.Size = new System.Drawing.Size(379, 239);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                if (textBox1.SelectionStart > 3)
                {
                    textBox1.SelectionStart -= 3;
                    Print();
                }
            }
            else if (e.KeyCode == Keys.Right)
            {
                if (textBox1.SelectionStart <= textBox1.Text.Length - 3)
                {
                    textBox1.SelectionStart += 3;
                    Print();
                }
            }
            else if (e.KeyCode == Keys.Up || e.KeyCode == Keys.Down)
            {
                Print();
            }
        }

        [System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.Demand, Name = "FullTrust")]
        protected override void WndProc(ref Message m)
        {
            try
            {
                base.WndProc(ref m);
            }
            catch (Exception ex)
            {
                ErrorReportWindow.Show(ex, "Error In Debugger Memory View");

            }
        }

        private bool lockVScrollBar = false;
        private void vScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
            if (lockVScrollBar)
                return;



            lockVScrollBar = true;
            vScrollBar1.Value = 2;
            lockVScrollBar = false;
        }

        private void Print()
        {
            int selStart = -1;

            int boxHeight = this.textBox1.Height / this.textBox1.Font.Height - 2;

                           //0x12345678: 00 01 02
            string text = "Address   :  0  1  2  3  4  5  6  7  8  9  A  B  C  D  E  F" + Environment.NewLine;

            for (int i = viewStart, j = 0; j < boxHeight; i++, j++)
            {
                text += String.Format("0x{0:X7}_: ", i);

                for (int addr = i * 0x10, k = 0; k <= 0x0F; addr++, k++)
                {
                    byte val = rstVal;

                    if (data.ContainsKey(addr))
                    {
                        val = data[addr];
                    }

                    int selectedAddr = -1;

                    if (textBox1.SelectionStart >= text.Length && textBox1.SelectionStart <= text.Length + 2)
                    {
                        selectedAddr = addr;
                    }

                    if (addr == selectedAddr)
                    {
                        selStart = text.Length;
                    }

                    if (dropViewMode.SelectedIndex == 0)
                    {
                        text += val.ToString("X2");
                    }
                    else if (dropViewMode.SelectedIndex == 1)
                    {
                        if (val == 0)
                        {
                            text += ".0";
                        }
                        else if (Convert.ToChar(val) != '?')
                        {
                            text += val.ToString("X2");
                        }
                        else
                        {
                            text += " ";
                            text += Convert.ToChar(val);
                        }
                    }

                    text += " ";
                }

                text += Environment.NewLine;
            }

            textBox1.Text = text;

            if (selStart >= 0)
            {
                textBox1.Select(selStart, 2);
                textBox1.SelectionStart = selStart;
                textBox1.SelectionLength = 2;

                textBox1.Focus();
            }
        }

        private void dropViewMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (dropViewMode.SelectedIndex < 0)
                return;

            Print();
        }

        private void textBox1_Click(object sender, EventArgs e)
        {
            Print();
        }
    }
}
