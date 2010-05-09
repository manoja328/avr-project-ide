using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace GetInterruptVectorsFromHTML
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            foreach (FileInfo f in new DirectoryInfo(@"C:\Users\Frank\Documents\Projects\Visual Studio\AVRProjectIDE\ChipExplorerTest\xml_files").GetFiles())
            {
                File.Move(f.FullName, @"C:\Users\Frank\Documents\Projects\Visual Studio\AVRProjectIDE\ChipExplorerTest\xml_files\" + Path.GetFileName(f.FullName).ToLowerInvariant());
            }
            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new Form1());
        }
    }

    struct IntVect
    {
        private string newName;
        public string NewName
        {
            get { return newName.Trim(); }
            set { newName = value.Trim(); }
        }

        private string oldName;
        public string OldName
        {
            get { return oldName.Trim(); }
            set { oldName = value.Trim(); }
        }

        private string description;
        public string Description
        {
            get { return description.Trim(); }
            set { description = value.Trim(); }
        }

        public IntVect(string newName, string oldName, string description)
        {
            this.newName = newName;
            this.oldName = oldName;
            this.description = description;
        }
    }
}
