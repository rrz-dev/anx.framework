using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using ANX.ContentCompiler.GUI.Dialogues;
using ANX.Framework.Content.Pipeline;
using ANX.Framework.Content.Pipeline.Tasks;

namespace ANX.ContentCompiler.GUI
{
    public partial class MainWindow : Form
    {
        #region Fields
        public static String DefaultOutputPath = "bin";
        public static String SettingsFile = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "ANX Content Compiler" + Path.DirectorySeparatorChar + "settings.ees");

        private Point _lastPos;
        private bool _mouseDown;
        private bool _menuMode;
        private readonly bool _firstStart = true;

        private ContentProject _contentProject;
        #endregion

        #region Properties
        public static MainWindow Instance { get; private set; }

        public String ProjectName { get; set; }
        public String ProjectPath { get; set; }
        public String ProjectFolder { get; set; }
        public String ProjectOutputDir { get; set; }
        public String ProjectImportersDir { get; set; }
        #endregion

        #region Init
        public MainWindow()
        {
            InitializeComponent();
            Instance = this;
            _firstStart = !File.Exists(SettingsFile);
            if (_firstStart)
            {
                Settings.Defaults();
                Settings.Save(SettingsFile);
            }
            else
                Settings.Load(SettingsFile);
            treeViewItemAddFolder.MouseEnter += TreeViewItemMouseEnter;
            treeViewItemAddFolder.MouseLeave += TreeViewItemeLeave;
            treeViewItemDelete.MouseEnter += TreeViewItemMouseEnter;
            treeViewItemRename.MouseEnter += TreeViewItemMouseEnter;
            SetUpColors();
        }

        private void MainWindowShown(object sender, EventArgs e)
        {
            if (_firstStart)
                ShowFirstStartStuff();
            ChangeEnvironmentStartState();
        }
        #endregion

        #region NewProject
        public void NewProject(object sender, EventArgs e)
        {
            using (var dlg = new NewProjectScreen())
            {
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    ProjectName = dlg.textBoxName.Text;
                    ProjectFolder = !String.IsNullOrEmpty(dlg.textBoxLocation.Text) ? dlg.textBoxLocation.Text : Path.Combine(Settings.DefaultProjectPath, ProjectName);
                    ProjectPath = Path.Combine(ProjectFolder, ProjectName + ".cproj");
                }
                else
                {
                    return;
                }
            }
            using (var dlg2 = new NewProjectOutputScreen())
            {
                if (dlg2.ShowDialog() == DialogResult.OK)
                {
                    ProjectOutputDir = !String.IsNullOrEmpty(dlg2.textBoxLocation.Text) ? dlg2.textBoxLocation.Text : Path.Combine(ProjectFolder, DefaultOutputPath);
                }
                else
                    return;
            }
            using (var dlg3 = new NewProjectImportersScreen())
            {
                if (dlg3.ShowDialog() == DialogResult.OK)
                    ProjectImportersDir = dlg3.textBoxLocation.Text;
                else
                    return;
            }
            var importersEnabled = !String.IsNullOrEmpty(ProjectImportersDir);
            var importers = 0;
            var processors = 0;

            using (var dlg4 = new NewProjectSummaryScreen(ProjectName, ProjectFolder, ProjectOutputDir, importersEnabled, ProjectImportersDir, importers, processors))
            {
                dlg4.ShowDialog();
            }
            _contentProject = new ContentProject(ProjectName)
                                  {
                                      OutputDirectory = ProjectOutputDir,
                                      InputDirectory = ProjectFolder,
                                      Configuration = "Release",
                                      Creator = "ANX Content Compiler (4.0)",
                                      ContentRoot = "Content",
                                      Platform = TargetPlatform.Windows
                                  };
            ChangeEnvironmentOpenProject();
        }
        #endregion

        #region OpenProject
        public void OpenProjectDialog(object sender, EventArgs e)
        {
            using (var dlg = new OpenProjectScreen())
            {
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    OpenProject(dlg.textBoxLocation.Text);
                }
            }
        }
        public void OpenProject(string path)
        {
            if (!File.Exists(path))
                throw new FileNotFoundException("No file found at the given location:", path);
            _contentProject = ContentProject.Load(path);
            ProjectName = _contentProject.Name;
            ProjectOutputDir = _contentProject.OutputDirectory;
            ProjectFolder = _contentProject.InputDirectory;
            ProjectImportersDir = _contentProject.ReferenceIncludeDirectory;
            ProjectPath = path;
            if (string.IsNullOrEmpty(_contentProject.Creator))
                _contentProject.Creator = "ANX Content Compiler (4.0)";
            ChangeEnvironmentOpenProject();
        }
        #endregion

        #region SaveProject
        public void SaveProject(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(ProjectPath))
                SaveProjectAs(sender, e);

            _contentProject.Save(ProjectPath);
        }
        #endregion

        #region SaveProjectAs
        public void SaveProjectAs(object sender, EventArgs e)
        {
            using (var dlg = new SaveFileDialog())
            {
                dlg.Title = "Save project as";
                dlg.AddExtension = true;
                dlg.Filter = "ANX Content Project (*.cproj)|*.cproj|Compressed Content Project (*.ccproj)|*.ccproj";
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    ProjectPath = dlg.FileName;
                    SaveProject(sender, e);
                }
            }
        }
        #endregion

        #region FileMethods
        private void AddFile(string file)
        {
            if (!File.Exists(file))
                throw new FileNotFoundException();

            var folder = _contentProject.ContentRoot;
            var node = treeView.SelectedNode;
            if (node != null)
                folder = node.Name;
            else
                node = treeView.Nodes[0];
            var absPath = ProjectFolder + Path.DirectorySeparatorChar + folder + Path.DirectorySeparatorChar + Path.GetFileName(file);
            if (!Directory.Exists(Path.Combine(ProjectFolder, folder)))
                Directory.CreateDirectory(Path.Combine(ProjectFolder, folder));
            File.Copy(file, absPath);
            var item = new BuildItem
                           {
                               AssetName = String.IsNullOrEmpty(folder) ? folder.Replace(_contentProject.ContentRoot, "") + Path.DirectorySeparatorChar + Path.GetFileNameWithoutExtension(file) : Path.GetFileNameWithoutExtension(file),
                               SourceFilename = absPath,
                               OutputFilename = ProjectOutputDir + Path.DirectorySeparatorChar + folder + Path.DirectorySeparatorChar + Path.GetFileName(file),
                               ImporterName = ImporterManager.GuessImporterByFileExtension(file)
                           };
            _contentProject.BuildItems.Add(item);
        }

        public void AddFiles(string[] files)
        {
            foreach (var file in files)
            {
                AddFile(file);
            }
            ChangeEnvironmentOpenProject();
        }
        #endregion

        #region EnvironmentStates
        public void ChangeEnvironmentStartState()
        {
            editingState.Visible = false;
            startState.Visible = true;
            Text = "ANX Content Compiler 4";
            labelTitle.Text = "ANX Content Compiler 4";
            treeView.Nodes.Clear();
            propertyGrid.SelectedObject = null;
        }

        public void ChangeEnvironmentOpenProject()
        {
            startState.Visible = false;
            editingState.Visible = true;
            Text = ProjectName + " - ANX Content Compiler 4";
            labelTitle.Text = "ANX Content Compiler 4 - " + ProjectName;

            ProjectFolder = _contentProject.InputDirectory;
            treeView.Nodes.Clear();
            var rootNode = new TreeNode(ProjectName + "(" + _contentProject.ContentRoot + ")") {Name = _contentProject.ContentRoot};
            treeView.Nodes.Add(rootNode);
            var lastNode = rootNode;
            foreach (var parts in _contentProject.BuildItems.Select(buildItem => buildItem.AssetName.Split('/')).Where(parts => parts.Length >= 2))
            {
                for (int i=0; i < parts.Length - 1; i++)
                {
                    var node = new TreeNode(parts[i]) {Name = lastNode.Name + "/" + parts[i] + "/"};
                    if (!lastNode.Nodes.Contains(node))
                    {
                        lastNode.Nodes.Add(node);
                        lastNode = node;
                    }
                    else
                    {
                        lastNode = lastNode.Nodes[parts[i]];
                    }
                }
                lastNode = rootNode;
            }
            if (_contentProject.BuildItems.Count > 0)
            {
                foreach (var buildItem in _contentProject.BuildItems)
                {
                    String[] parts = null;
                    if (buildItem.AssetName.Contains("\\"))
                        parts = buildItem.AssetName.Split('\\');
                    else if (buildItem.AssetName.Contains("/"))
                        parts = buildItem.AssetName.Split('/');
                    /*if (parts.Length >= 2)
                    {
                        for (int i = 0; i < parts.Length - 1; i++)
                        {
                            lastNode = lastNode.Nodes[parts[i]];
                        }
                    }*/
                    string path = "";
                    if (parts != null)
                    {
                        for (int i = 0; i < parts.Length - 1; i++)
                        {
                            path = parts[i];
                        }
                    }
                    if (!String.IsNullOrEmpty(path))
                    {
                        var node = treeView.RecursiveSearch(path);
                        if (node == null) throw new ArgumentNullException("Node not found!");
                        var item = new TreeNode(parts[parts.Length - 1]) {Name = buildItem.AssetName};
                        node.Nodes.Add(item);
                    }
                    else
                    {
                        var item = new TreeNode(buildItem.AssetName) {Name = buildItem.AssetName};
                        treeView.Nodes[0].Nodes.Add(item);
                    }
                }
            }

        }
        #endregion

        #region ButtonHandlers
        private void ButtonQuitClick(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void ButtonMenuClick(object sender, EventArgs e)
        {
            ToggleMenuMode();
        }
        #endregion

        #region WindowMoveMethods
        private void LabelTitleMouseMove(object sender, MouseEventArgs e)
        {
            if (!_mouseDown) return;
            var xoffset = MousePosition.X - _lastPos.X;
            var yoffset = MousePosition.Y - _lastPos.Y;
            Left += xoffset;
            Top += yoffset;
            _lastPos = MousePosition;
        }

        private void LabelTitleMouseDown(object sender, MouseEventArgs e)
        {
            _mouseDown = true;
            _lastPos = MousePosition;
        }

        private void LabelTitleMouseUp(object sender, MouseEventArgs e)
        {
            _mouseDown = false;
        }
        #endregion

        #region TreeVieItemDesignMethods
        void TreeViewItemeLeave(object sender, EventArgs e)
        {
            ((ToolStripItem)sender).BackColor = Color.FromArgb(0, 64, 64, 64);
        }

        void TreeViewItemMouseEnter(object sender, EventArgs e)
        {
            ((ToolStripItem)sender).BackColor = Color.Green;
        }
        #endregion

        #region MenuMethods
        public void ToggleMenuMode()
        {
            _menuMode = !_menuMode;
            if (_menuMode)
            {
                buttonMenu.BackColor = Settings.AccentColor3;
                menuState.Visible = true;
            }
            else
            {
                menuState.Visible = false;
                buttonMenu.BackColor = Settings.AccentColor;
            }
        }

        #endregion

        #region ShowFirstStartStuff
        private void ShowFirstStartStuff()
        {
            using (var dlg = new FirstStartScreen())
            {
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    
                }
            }
        }
        #endregion

        #region SetUpColors
        private void SetUpColors()
        {
            BackColor = Settings.MainColor;
            ForeColor = Settings.ForeColor;
            buttonQuit.FlatAppearance.MouseOverBackColor = Settings.LightMainColor;
            buttonQuit.FlatAppearance.MouseDownBackColor = Settings.AccentColor;
            buttonMenu.BackColor = Settings.AccentColor;
            buttonMenu.FlatAppearance.MouseOverBackColor = Settings.AccentColor2;
            buttonMenu.FlatAppearance.MouseDownBackColor = Settings.AccentColor3;
            labelTitle.ForeColor = Settings.ForeColor;
            labelProperties.ForeColor = Settings.ForeColor;
            labelFileTree.ForeColor = Settings.ForeColor;
            treeView.BackColor = Settings.DarkMainColor;
            propertyGrid.BackColor = Settings.DarkMainColor;
            propertyGrid.ForeColor = Settings.ForeColor;
            propertyGrid.HelpBackColor = Settings.MainColor;
            propertyGrid.LineColor = Settings.MainColor;
            propertyGrid.ViewBackColor = Settings.DarkMainColor;
            propertyGrid.ViewForeColor = Settings.ForeColor;
        }
        #endregion

        #region Exit
        private void MainWindowFormClosed(object sender, FormClosedEventArgs e)
        {
            Settings.Save(SettingsFile);
        }

        private void MainWindowFormClosing(object sender, FormClosingEventArgs e)
        {

        }
        #endregion

        #region TreeViewEvents
        private void TreeViewAfterSelect(object sender, TreeViewEventArgs e)
        {
            if (treeView.SelectedNode == treeView.TopNode)
                propertyGrid.SelectedObject = _contentProject;
            else
            {
                foreach (var buildItem in _contentProject.BuildItems.Where(buildItem => buildItem.AssetName.Equals(treeView.SelectedNode.Name)))
                {
                    propertyGrid.SelectedObject = buildItem;
                }
            }
        }
        #endregion

        #region PropertyGridEvents
        private void PropertyGridPropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            ProjectName = _contentProject.Name;
            ProjectImportersDir = _contentProject.ReferenceIncludeDirectory;
            ProjectFolder = _contentProject.InputDirectory;
            ProjectOutputDir = _contentProject.OutputDirectory;
            if (e.ChangedItem.Label.Equals("ContentRoot"))
            {
                foreach (BuildItem buildItem in _contentProject.BuildItems)
                {
                    buildItem.AssetName = buildItem.AssetName.Replace((string)e.OldValue, _contentProject.ContentRoot);
                }
                treeView.Nodes[0].RecursivelyReplacePartOfName((string)e.OldValue, _contentProject.ContentRoot);
            }
            ChangeEnvironmentOpenProject();
        }
        #endregion
    }
}