using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace AVRProjectIDE
{
    public partial class DummyPanel : DockContent
    {
        public DummyPanel()
        {
            InitializeComponent();
            this.Close();
        }
    }
}
