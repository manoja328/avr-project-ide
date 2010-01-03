using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ScintillaNet;
using WeifenLuo.WinFormsUI.Docking;

namespace AVRProjectIDE
{
    public partial class SearchPanel : DockContent
    {
        [System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.Demand, Name = "FullTrust")]
        protected override void WndProc(ref Message m)
        {
            try
            {
                base.WndProc(ref m);
            }
            catch (Exception ex)
            {
                ErrorReportWindow erw = new ErrorReportWindow(ex, "Error In Search Panel");
                erw.ShowDialog();
            }
        }

        private Dictionary<string, EditorPanel> editorList;

        public SearchPanel(Dictionary<string, EditorPanel> listOfEditors)
        {
            InitializeComponent();

            this.editorList = listOfEditors;

            this.Icon = GraphicsResx.serial;
        }

        private void txtSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r' || e.KeyChar == '\n')
                Search();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            Search();
        }

        private void Search()
        {
            string searchString = txtSearch.Text;

            if (string.IsNullOrEmpty(searchString))
                return;

            bool matchCase = chkMatchCase.Checked;
            bool wholeWord = chkWholeWord.Checked;
            bool wordStart = chkWordStart.Checked;
            bool escape = chkEscape.Checked;

            SearchFlags flags = SearchFlags.Empty;

            if (matchCase)
                flags |= SearchFlags.MatchCase;

            if (wholeWord)
                flags |= SearchFlags.WholeWord;

            if (wordStart)
                flags |= SearchFlags.WordStart;

            if (escape)
            {
                searchString = searchString.Replace("\\\\", "\\");
                searchString = searchString.Replace("\\t", "\t");
                searchString = searchString.Replace("\\r", "\r");
                searchString = searchString.Replace("\\n", "\n");
            }

            listSearchResults.Items.Clear();

            foreach (KeyValuePair<string, EditorPanel> i in editorList)
            {
                EditorPanel editor = i.Value;

                string fileName = editor.FileName;

                Scintilla scint = editor.Scint;

                List<Range> results = editor.FindAll(searchString, flags);

                foreach (Range r in results)
                {
                    int lineNum = scint.Lines.FromPosition(r.Start).Number;
                    string resLine = scint.Lines[lineNum].Text.TrimEnd();

                    string hoverTxt = "";
                    if (lineNum >= 2)
                        hoverTxt += scint.Lines[lineNum - 2].Text.TrimEnd() + Environment.NewLine;
                    if (lineNum >= 1)
                        hoverTxt += scint.Lines[lineNum - 1].Text.TrimEnd() + Environment.NewLine;

                    hoverTxt += scint.Lines[lineNum].Text.TrimEnd() + Environment.NewLine;

                    if (lineNum < scint.Lines.Count - 1)
                        hoverTxt += scint.Lines[lineNum + 1].Text.TrimEnd() + Environment.NewLine;
                    if (lineNum < scint.Lines.Count - 2)
                        hoverTxt += scint.Lines[lineNum + 2].Text.TrimEnd() + Environment.NewLine;

                    hoverTxt = hoverTxt.TrimEnd();

                    ListViewItem lvi = new ListViewItem(new string[] { resLine, fileName, (lineNum + 1).ToString(), r.Start.ToString(), r.End.ToString(), });
                    lvi.ToolTipText = hoverTxt;

                    listSearchResults.Items.Add(lvi);
                }
            }

            ShowResults();
        }

        public void ShowSearch()
        {
            this.tabControl1.SelectedIndex = 0;
        }

        public void ShowResults()
        {
            this.tabControl1.SelectedIndex = 1;
        }

        public event SearchResultDoubleClickedEvent GotoResult;
        public delegate void SearchResultDoubleClickedEvent(string fileName, int selectionStart, int selectionEnd);

        private void listSearchResults_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (listSearchResults.SelectedItems[0] == null)
                return;
            if (string.IsNullOrEmpty(listSearchResults.SelectedItems[0].SubItems[1].Text))
                return;
            if (string.IsNullOrEmpty(listSearchResults.SelectedItems[0].SubItems[2].Text))
                return;
            if (string.IsNullOrEmpty(listSearchResults.SelectedItems[0].SubItems[3].Text))
                return;
            if (string.IsNullOrEmpty(listSearchResults.SelectedItems[0].SubItems[4].Text))
                return;
            string fname = listSearchResults.SelectedItems[0].SubItems[1].Text;
            int line = int.Parse(listSearchResults.SelectedItems[0].SubItems[2].Text);
            int start = int.Parse(listSearchResults.SelectedItems[0].SubItems[3].Text);
            int end = int.Parse(listSearchResults.SelectedItems[0].SubItems[4].Text);

            GotoResult(fname, start, end);
        }

        public void Clear()
        {
            listSearchResults.Items.Clear();
        }
    }
}
