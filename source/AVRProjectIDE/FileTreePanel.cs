using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace AVRProjectIDE
{
    public partial class FileTreePanel : DockContent
    {

        #region Fields and Properties

        private AVRProject project;
        private Dictionary<string, EditorPanel> editorList;

        private TreeNode rootNode;
        private TreeNode sourceNode;
        private TreeNode headerNode;
        private TreeNode otherNode;

        #endregion

        public FileTreePanel()
        {
            InitializeComponent();

            InitializeTree();
        }

        #region Events and Delegates

        public event OpenFileEvent OpenNode;
        public delegate void OpenFileEvent(TreeNode node);

        #endregion

        #region Methods

        public void RemoveNode(TreeNode node)
        {
            string fileName = node.Text;

            ProjectFile f = null;
            if (project.FileList.TryGetValue(fileName, out f))
            {
                project.FileList.Remove(fileName);
            }

            EditorPanel editor;
            if (editorList.TryGetValue(fileName, out editor))
            {
                editor.Close();
                editorList.Remove(fileName);
            }

            node.Remove();

            if (f != null)
            {
                if (f.Exists)
                {
                    if (MessageBox.Show("Delete Permanently?", "Delete?", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        try
                        {
                            File.Delete(f.FileAbsPath);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Could not delete '" + f.FileAbsPath + "', " + ex.Message);
                        }
                    }
                }
            }

            if (project.Save() == SaveResult.Failed)
            {
                MessageBox.Show("Error saving project");
            }
        }

        public bool RenameNode(TreeNode node, string newName)
        {
            newName = newName.Trim();

            if (newName.Contains(" "))
                return false;

            foreach (char c in Path.GetInvalidFileNameChars())
            {
                if (newName.Contains(c))
                    return false;
            }

            if (Path.GetExtension(newName) != Path.GetExtension(node.Text))
                return false;

            ProjectFile f;
            if (project.FileList.TryGetValue(node.Text, out f))
            {
                if (f.Exists == false)
                    return false;
            }
            else
                return false;

            if (project.FileList.TryGetValue(newName, out f) == false)
            {
                if (project.FileList.TryGetValue(node.Text, out f))
                {
                    string newPath = f.FileDir + Path.DirectorySeparatorChar + newName;
                    if (File.Exists(newPath) == false)
                    {
                        try
                        {
                            EditorPanel editor = null;
                            if (editorList.TryGetValue(node.Text, out editor))
                            {
                                editor.WatchingForChange = false;
                            }
                            File.Move(f.FileAbsPath, newPath);

                            f.FileAbsPath = newPath;

                            if (editor != null)
                            {
                                editorList.Remove(node.Text);
                                editorList.Add(newName, editor);
                                editor.File.FileAbsPath = newPath;
                                editor.Text = newName;
                                editor.TabText = newName;
                                editor.WatchingForChange = true;
                            }
                        }
                        catch { return false; }

                        node.ToolTipText = f.FileRelPath(project.DirPath);

                        project.FileList.Remove(node.Text);
                        project.FileList.Add(newName, f);

                        if (project.Save() == SaveResult.Failed)
                        {
                            MessageBox.Show("Error saving project");
                        }

                        return true;
                    }
                    else
                        return false;
                }
                else
                    return false;
            }
            else
                return false;
        }

        public void InitializeTree()
        {
            rootNode = new TreeNode("Project");
            sourceNode = new TreeNode("Source Files (c, cpp, cxx, S, pde)");
            headerNode = new TreeNode("Header Files (h, hpp)");
            otherNode = new TreeNode("Other Files");

            rootNode.ContextMenuStrip = treeRClickMenu;
            sourceNode.ContextMenuStrip = treeRClickMenu;
            headerNode.ContextMenuStrip = treeRClickMenu;
            otherNode.ContextMenuStrip = treeRClickMenu;

            rootNode.Checked = true;
            sourceNode.Checked = true;
            headerNode.Checked = false;
            otherNode.Checked = false;

            rootNode.ToolTipText = "Double Click Me To Open Project Folder";
            sourceNode.ToolTipText = "Only Source Code Files";
            headerNode.ToolTipText = "Only Header Files";
            otherNode.ToolTipText = "Other Files";

            rootNode.Nodes.Add(sourceNode);
            rootNode.Nodes.Add(headerNode);
            rootNode.Nodes.Add(otherNode);

            rootNode.ImageKey = "folder2.png";
            rootNode.SelectedImageKey = "folder2.png";
            rootNode.StateImageKey = "folder2.png";

            sourceNode.ImageKey = "folder.png";
            sourceNode.SelectedImageKey = "folder.png";
            sourceNode.StateImageKey = "folder.png";

            headerNode.ImageKey = "folder.png";
            headerNode.SelectedImageKey = "folder.png";
            headerNode.StateImageKey = "folder.png";

            otherNode.ImageKey = "folder.png";
            otherNode.SelectedImageKey = "folder.png";
            otherNode.StateImageKey = "folder.png";
        }

        public void PopulateList(AVRProject newProj, Dictionary<string, EditorPanel> newList)
        {
            project = newProj;
            editorList = newList;
            PopulateList();
        }

        private void PopulateList()
        {
            rootNode.Text = project.FileName;

            sourceNode.Nodes.Clear();
            headerNode.Nodes.Clear();
            otherNode.Nodes.Clear();

            List<TreeNode> sourceNodeList = new List<TreeNode>();
            List<TreeNode> headerNodeList = new List<TreeNode>();
            List<TreeNode> otherNodeList = new List<TreeNode>();

            foreach (ProjectFile file in project.FileList.Values)
            {
                KeywordScanner.FeedFileContent(file);

                string fn = file.FileName;

                TreeNode tn = new TreeNode(fn);

                tn.ToolTipText = file.FileRelPath(project.DirPath);

                // attach the menu
                tn.ContextMenuStrip = nodeRClickMenu;

                // set icon according to whether or not the file is missing on disk
                if (file.Exists == false)
                {
                    tn.ImageKey = "missing.ico";
                    tn.SelectedImageKey = "missing.ico";
                    tn.StateImageKey = "missing.ico";
                }
                else
                {
                    tn.ImageKey = "file.ico";
                    tn.SelectedImageKey = "file.ico";
                    tn.StateImageKey = "file.ico";
                }

                string ext = fn.ToLowerInvariant();
                if (ext.EndsWith(".s") || ext.EndsWith(".c") || ext.EndsWith(".cpp") || ext.EndsWith(".cxx") || ext.EndsWith(".pde"))
                {
                    // only source files can be compiled

                    if (file.ToCompile)
                    {
                        tn.Checked = true;
                    }

                    sourceNodeList.Add(tn);
                    //sourceNode.Nodes.Add(tn);
                }
                else if (ext.EndsWith(".h") || ext.EndsWith(".hpp"))
                {
                    tn.Checked = false;

                    //headerNode.Nodes.Add(tn);
                    headerNodeList.Add(tn);
                }
                else
                {
                    tn.Checked = false;

                    //otherNode.Nodes.Add(tn);
                    otherNodeList.Add(tn);
                }
            }

            sourceNodeList.Sort((x, y) => string.Compare(x.Text, y.Text));
            foreach (TreeNode i in sourceNodeList)
            {
                sourceNode.Nodes.Add(i);
            }

            headerNodeList.Sort((x, y) => string.Compare(x.Text, y.Text));
            foreach (TreeNode i in headerNodeList)
            {
                headerNode.Nodes.Add(i);
            }

            otherNodeList.Sort((x, y) => string.Compare(x.Text, y.Text));
            foreach (TreeNode i in otherNodeList)
            {
                otherNode.Nodes.Add(i);
            }

            treeView1.SuspendLayout();

            treeView1.Nodes.Clear();
            treeView1.Nodes.Add(rootNode);

            treeView1.ExpandAll();

            treeView1.ResumeLayout();

            KeywordScanner.DoMoreWork();
        }

        public SaveResult AddFileWiz(out ProjectFile file)
        {
            file = null;

            FileAddWizard faw = new FileAddWizard(project);
            if (faw.ShowDialog() == DialogResult.OK)
            {
                if (faw.CreatedFile != null)
                {
                    file = faw.CreatedFile;
                    PopulateList();
                    return SaveResult.Successful;
                }
                else
                {
                    return SaveResult.Failed;
                }
            }
            else
                return SaveResult.Cancelled;
        }

        public SaveResult AddFile(out ProjectFile file)
        {
            file = null;

            SaveFileDialog sfd = new SaveFileDialog();

            sfd.Title = "Find or Create a File to Add";

            sfd.InitialDirectory = project.DirPath;

            string filter = "";
            filter += "C Source Code (*.c)|*.c" + "|";
            filter += "CPP Source Code (*.cpp)|*.cpp" + "|";
            filter += "Assembly Source Code (*.S)|*.S" + "|";
            filter += "Arduino Source Code (*.pde)|*.pde" + "|";
            filter += "H Header File (*.h)|*.h" + "|";
            filter += "HPP Header File (*.hpp)|*.hpp" + "|";
            filter += "Any File (*.*)|*.*";
            sfd.Filter = filter;
            sfd.FilterIndex = SettingsManagement.LastFileTypeFilter;

            sfd.AddExtension = true;

            sfd.OverwritePrompt = false;

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                SettingsManagement.LastFileTypeFilter = sfd.FilterIndex;
                return AddFile(out file, sfd.FileName);
            }
            else
            {
                return SaveResult.Cancelled;
            }
        }

        public SaveResult AddFile(out ProjectFile file, string filePath)
        {
            string fn = Path.GetFileName(filePath);
            string ext = Path.GetExtension(fn).ToLowerInvariant();

            if (project.FileList.TryGetValue(fn, out file))
            {
                if (file.FileAbsPath != filePath && file.Exists)
                {
                    // name conflict, do not allow
                    MessageBox.Show("Error, Cannot Add File " + file.FileName + " Due To Name Conflict");
                    return SaveResult.Failed;
                }
                else
                {
                    // added file already in list, maybe it was missing, so refresh the list to update icons
                    PopulateList();
                    return SaveResult.Cancelled;
                }
            }
            else
            {
                if (ext == "c" || ext == "cpp" || ext == "cxx" || ext == "s" || ext == "h" || ext == "hpp")
                {
                    // check for space if it's a source or header file, we don't care about the other files
                    if (fn.Contains(" "))
                    {
                        MessageBox.Show("Error, File Name May Not Contain Spaces");
                        return SaveResult.Failed;
                    }
                }

                file = new ProjectFile(filePath);

                if (file.Exists == false)
                {
                    try
                    {
                        StreamWriter newFile = new StreamWriter(file.FileAbsPath);

                        if (file.FileExt == "h" || file.FileExt == "hpp")
                        {
                            newFile.WriteLine(FileTemplate.CreateFile(file.FileName, project.FileNameNoExt, "defaultheader.txt"));
                        }
                        else if (file.FileExt == "c" || file.FileExt == "cpp")
                        {
                            newFile.WriteLine(FileTemplate.CreateFile(file.FileName, project.FileNameNoExt, "defaultcode.txt"));
                        }
                        else
                            newFile.WriteLine(FileTemplate.CreateFile(file.FileName, project.FileNameNoExt, "default_" + file.FileExt + ".txt"));

                        newFile.Close();
                    }
                    catch (Exception ex)
                    {
                        ErrorReportWindow erw = new ErrorReportWindow(ex, "Error Creating New File " + file.FileName);
                        erw.ShowDialog();
                    }
                }

                project.FileList.Add(fn, file);

                if (project.Save() == SaveResult.Failed)
                {
                    MessageBox.Show("Error saving project");
                }

                PopulateList();
                return SaveResult.Successful;
            }
        }

        public SaveResult AddFile(string filePath)
        {
            ProjectFile file;
            return AddFile(out file, filePath);
        }

        #endregion

        #region Event Handlers

        private void mbtnRename_Click(object sender, EventArgs e)
        {
            TreeNode tn = treeView1.SelectedNode;

            if (tn != null)
                if (tn != rootNode && tn != sourceNode && tn != headerNode && tn != otherNode)
                    tn.BeginEdit();
        }

        private void mbtnDelete_Click(object sender, EventArgs e)
        {
            TreeNode tn = treeView1.SelectedNode;

            if (tn != null)
                if (tn != rootNode && tn != sourceNode && tn != headerNode && tn != otherNode)
                    RemoveNode(tn);
        }

        private void treeView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                TreeNode tn = treeView1.SelectedNode;

                if (tn != null)
                    if (tn != rootNode && tn != sourceNode && tn != headerNode && tn != otherNode)
                        RemoveNode(tn);
            }
            else if (e.KeyCode == Keys.F2)
            {
                TreeNode tn = treeView1.SelectedNode;

                if (tn != null)
                    if (tn != rootNode && tn != sourceNode && tn != headerNode && tn != otherNode)
                        tn.BeginEdit();
            }
        }

        private void mbtnSetOpt_Click(object sender, EventArgs e)
        {
            TreeNode tn = treeView1.SelectedNode;

            if (tn != null)
                if (tn != rootNode && tn != sourceNode && tn != headerNode && tn != otherNode && tn.Parent == sourceNode)
                {
                    ProjectFile file;
                    if (project.FileList.TryGetValue(tn.Text, out file))
                    {
                        if (file.FileExt != "pde")
                        {
                            FileOptionsDialog optForm = new FileOptionsDialog(file);
                            optForm.ShowDialog();
                        }
                    }
                }
        }

        private void mbtnAddFile_Click(object sender, EventArgs e)
        {
            if (project == null)
                return;

            if (project.IsReady == false)
                return;

            ProjectFile file;
            if (AddFile(out file) == SaveResult.Successful)
                OpenNode(new TreeNode(file.FileName)); // this is cheating, but i don't want to write another open event
        }

        private void mbtnAddFileWiz_Click(object sender, EventArgs e)
        {
            if (project == null)
                return;

            if (project.IsReady == false)
                return;

            ProjectFile file;
            if (AddFileWiz(out file) == SaveResult.Successful)
                OpenNode(new TreeNode(file.FileName)); // this is cheating, but i don't want to write another open event
        }

        private void treeView1_BeforeLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            if (e.Node == sourceNode || e.Node == headerNode || e.Node == rootNode || e.Node == otherNode)
            {
                e.CancelEdit = true;
                return;
            }
        }

        private void treeView1_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            if (e.Node == sourceNode || e.Node == headerNode || e.Node == rootNode || e.Node == otherNode)
            {
                e.CancelEdit = true;
                return;
            }

            if (RenameNode(e.Node, e.Label) == false)
            {
                e.CancelEdit = true;
                return;
            }
        }

        private void treeView1_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Node != sourceNode && e.Node != headerNode && e.Node != rootNode && e.Node != otherNode)
            {
                OpenNode(e.Node);
            }
            else
            {
                if (e.Node == rootNode)
                {
                    System.Diagnostics.Process.Start(project.DirPath + Path.DirectorySeparatorChar);
                }
                else if (e.Node != sourceNode)
                {
                    e.Node.Checked = false;
                }
            }
        }

        private void treeView1_BeforeCollapse(object sender, TreeViewCancelEventArgs e)
        {
            e.Cancel = true;
        }

        private void treeView1_BeforeCheck(object sender, TreeViewCancelEventArgs e)
        {
            if ((e.Node == sourceNode || e.Node == headerNode || e.Node == rootNode || e.Node == otherNode) || e.Node.Parent != sourceNode)
                e.Cancel = true;
        }

        private void treeView1_AfterCheck(object sender, TreeViewEventArgs e)
        {
            if (e.Node != sourceNode && e.Node != headerNode && e.Node != rootNode && e.Node != otherNode && e.Node.Parent == sourceNode)
            {
                ProjectFile f;
                if (project.FileList.TryGetValue(e.Node.Text, out f))
                {
                    f.ToCompile = e.Node.Checked;
                }
            }
        }

        /// <summary>
        /// Fixes a bug that makes you first left click on a node then right click, this function selects the node
        /// before the menu shows up, thus now you can right click and the right node will be sent to the context
        /// menu button event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            treeView1.SelectedNode = e.Node;
        }

        #endregion

        #region Drag and Drop Event Handling

        private delegate SaveResult DragInFile(string filePath);

        private void treeView1_DragDrop(object sender, DragEventArgs e)
        {
            if (project.IsReady == false)
                return;

            try
            {
                Array a = (Array)e.Data.GetData(DataFormats.FileDrop);

                if (a != null)
                {
                    foreach (string filePath in a)
                    {
                        this.BeginInvoke(new DragInFile(AddFile), new object[] { filePath, });
                    }

                    this.Activate();
                }
            }
            catch { }
        }

        private void treeView1_DragEnter(object sender, DragEventArgs e)
        {
            if (project.IsReady == false)
            {
                e.Effect = DragDropEffects.None;
                return;
            }

            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy;
            else
                e.Effect = DragDropEffects.None;
        }

        #endregion
    }
}
