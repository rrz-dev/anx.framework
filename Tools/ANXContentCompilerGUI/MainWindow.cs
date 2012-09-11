using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using ANX.ContentCompiler.GUI.Dialogues;
using ANX.Framework.Content.Pipeline;
using ANX.Framework.Content.Pipeline.Tasks;
using ANX.Framework.NonXNA.Development;

namespace ANX.ContentCompiler.GUI
{
    [Developer("SilentWarrior/Eagle Eye Studios")]
    [PercentageComplete(80)] //TODO: Implement Tour, Preview, Renaming of Folders!
    [TestState(TestStateAttribute.TestState.InProgress)]
    public partial class MainWindow : Form
    {
        #region Fields

        public static String DefaultOutputPath = "bin";

        public static String SettingsFile =
            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                         "ANX Content Compiler" + Path.DirectorySeparatorChar + "settings.ees");

        private readonly bool _firstStart = true;

        private ContentProject _contentProject;
        private Point _lastPos;
        private bool _menuMode;
        private bool _mouseDown;
        private readonly string[] _args;

        #endregion

        #region Properties

        public static MainWindow Instance { get; private set; }

        public String ProjectName { get; set; }
        public String ProjectPath { get; set; }
        public String ProjectFolder { get; set; }
        public String ProjectOutputDir { get; set; }
        public String ProjectImportersDir { get; set; }
        public RecentProjects RecentProjects { get; set; }

        #endregion

        #region Init

        public MainWindow(string[] args)
        {
            InitializeComponent();
            Instance = this;
            _args = args;
            _firstStart = !File.Exists(SettingsFile);
            if (_firstStart)
            {
                Settings.Defaults();
                Settings.Save(SettingsFile);
                RecentProjects = new RecentProjects();
                RecentProjects.Save();
            }
            else
            {
                Settings.Load(SettingsFile);
                RecentProjects = RecentProjects.Load();
            }
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
            if (_args.Length > 0)
            {
                if (File.Exists(_args[0]))
                    OpenProject(_args[0]);
            }
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
                    ProjectFolder = !String.IsNullOrEmpty(dlg.textBoxLocation.Text)
                                        ? dlg.textBoxLocation.Text
                                        : Path.Combine(Settings.DefaultProjectPath, ProjectName);
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
                    ProjectOutputDir = !String.IsNullOrEmpty(dlg2.textBoxLocation.Text)
                                           ? dlg2.textBoxLocation.Text
                                           : Path.Combine(ProjectFolder, DefaultOutputPath);
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
            bool importersEnabled = !String.IsNullOrEmpty(ProjectImportersDir);
            int importers = 0;
            int processors = 0;

            using (
                var dlg4 = new NewProjectSummaryScreen(ProjectName, ProjectFolder, ProjectOutputDir, importersEnabled,
                                                       ProjectImportersDir, importers, processors))
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
                if (dlg.ShowDialog() != DialogResult.OK) return;

                if (dlg.listBoxRecentProjects.SelectedItem == null)
                    OpenProject(dlg.textBoxLocation.Text);
                else
                    OpenProject((string) dlg.listBoxRecentProjects.SelectedItem);
            }
        }

        public void OpenProject(string path)
        {
            if (!File.Exists(path))
            {
                MessageBox.Show("No file found at this location: " + path, "Project not found");
                return;
            }
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
            if (_contentProject == null) return;
            if (String.IsNullOrEmpty(ProjectPath))
                SaveProjectAs(sender, e);

            _contentProject.Save(ProjectPath);
            if (RecentProjects.Contains(ProjectPath))
                RecentProjects.Remove(ProjectPath);
            RecentProjects.Add(ProjectPath);
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

        #region CleanProject
        public void CleanProject()
        {
            if (Directory.Exists(ProjectOutputDir))
                Directory.Delete(ProjectOutputDir, true);
        }
        #endregion

        #region BuildProject

        public void BuildProject(object sender, EventArgs e)
        {
            DisableUI();
            var builderTask = new BuildContent
                                  {
                                      BuildLogger = new CCompilerBuildLogger(),
                                      OutputDirectory = _contentProject.OutputDirectory,
                                      TargetPlatform = _contentProject.Platform,
                                      TargetProfile = _contentProject.Profile,
                                      CompressContent = false
                                  };
            try
            {
                foreach (var dir in _contentProject.BuildItems.Select(buildItem => Path.GetDirectoryName(buildItem.OutputFilename)).Where(dir => !Directory.Exists(dir)))
                {
                    Directory.CreateDirectory(dir);
                }
                builderTask.Execute(_contentProject.BuildItems);
                ribbonTextBox.AddMessage("[Info] Build process successfully finished.");
            }
            catch (Exception ex)
            {
                ribbonTextBox.AddMessage("[ERROR] " + ex + "\n Stack: " + ex.StackTrace);
                EnableUI();
            }
            EnableUI();
        }

        private void DisableUI()
        {
            buttonMenu.Enabled = false;
            buttonQuit.Enabled = false;
            editingState.Enabled = false;
            treeView.Enabled = false;
            propertyGrid.Enabled = false;
            ribbonButtonNew.Enabled = false;
            ribbonButtonSave.Enabled = false;
            ribbonButtonLoad.Enabled = false;
            ribbonButtonClean.Enabled = false;
        }

        private void EnableUI()
        {
            buttonMenu.Enabled = true;
            buttonQuit.Enabled = true;
            editingState.Enabled = true;
            treeView.Enabled = true;
            propertyGrid.Enabled = true;
            ribbonButtonNew.Enabled = true;
            ribbonButtonSave.Enabled = true;
            ribbonButtonLoad.Enabled = true;
            ribbonButtonClean.Enabled = true;
        }

        #endregion

        #region FileMethods
        /// <summary>
        /// Adds a file to the project
        /// </summary>
        /// <param name="file">the file to add</param>
        private void AddFile(string file)
        {
            if (!File.Exists(file))
                throw new FileNotFoundException();

            string folder = _contentProject.ContentRoot;
            TreeNode node = treeView.SelectedNode;
            if (node != null)
                folder = node.Name;
            string absPath = ProjectFolder + Path.DirectorySeparatorChar + folder + Path.DirectorySeparatorChar +
                             Path.GetFileName(file);
            if (!Directory.Exists(Path.Combine(ProjectFolder, folder)))
                Directory.CreateDirectory(Path.Combine(ProjectFolder, folder));
            File.Copy(file, absPath, true);
            var item = new BuildItem
                           {
                               AssetName =
                                   folder.Equals(_contentProject.ContentRoot)
                                       ? Path.GetFileNameWithoutExtension(file)
                                       : folder.Replace(_contentProject.ContentRoot + Path.DirectorySeparatorChar, "") +
                                         Path.DirectorySeparatorChar + Path.GetFileNameWithoutExtension(file),
                               SourceFilename = absPath,
                               OutputFilename =
                                   ProjectOutputDir + Path.DirectorySeparatorChar + folder + Path.DirectorySeparatorChar +
                                   Path.GetFileNameWithoutExtension(file) + ".xnb",   //<- Change this if you want some other extension (i.e. to annoy modders or whatever)
                               ImporterName = ImporterManager.GuessImporterByFileExtension(file)//,
                               //ProcessorName = ProcessorManager.GuessImporterByFileExtension(file) //<- This still needs to be implemented, Pipeline devs! *poke*
                           };
            _contentProject.BuildItems.Add(item);
        }

        /// <summary>
        /// Wrapper for adding moar files! (Just a foreach loop, nothing special)
        /// </summary>
        /// <param name="files">files to add</param>
        public void AddFiles(string[] files)
        {
            foreach (string file in files)
            {
                AddFile(file);
            }
            ChangeEnvironmentOpenProject();
        }

        public void AddFolder(string name)
        {
            string folder = _contentProject.ContentRoot;
            TreeNode node = treeView.SelectedNode;
            if (node != null)
                folder = node.Name;
            else
                node = treeView.Nodes[0];

            var newFolder = new TreeNode(name) {Name = folder + Path.DirectorySeparatorChar + name};
            node.Nodes.Add(newFolder);
        }

        public void RemoveFile(string name)
        {
            for (var i = _contentProject.BuildItems.Count - 1; i >= 0; i--)
            {
                if (_contentProject.BuildItems[i].AssetName == name)
                    _contentProject.BuildItems.RemoveAt(i);
            }
            ChangeEnvironmentOpenProject();
        }

        public void RemoveFiles(string[] files)
        {
            foreach (var file in files)
            {
                RemoveFile(file);
            }
        }

        public void RemoveFolder(string name)
        {
            if (treeView.RecursiveSearch(name).Nodes.Count > 0)
            {
                foreach (var buildItem in _contentProject.BuildItems.Where(buildItem => buildItem.AssetName.Contains(name)))
                {
                    RemoveFile(buildItem.AssetName);
                }
            }
            treeView.Nodes.Remove(treeView.RecursiveSearch(name));
        }

        #endregion

        #region EnvironmentStates

        /// <summary>
        /// Changes the current editor state to the "No project open" state
        /// </summary>
        public void ChangeEnvironmentStartState()
        {
            editingState.Visible = false;
            startState.Visible = true;
            Text = "ANX Content Compiler 4";
            labelTitle.Text = "ANX Content Compiler 4";
            treeView.Nodes.Clear();
            propertyGrid.SelectedObject = null;
        }

        /// <summary>
        /// Changes the current editor state to edit mode
        /// </summary>
        public void ChangeEnvironmentOpenProject()
        {
            startState.Visible = false;
            editingState.Visible = true;
            Text = ProjectName + " - ANX Content Compiler 4";
            labelTitle.Text = "ANX Content Compiler 4 - " + ProjectName;

            ProjectFolder = _contentProject.InputDirectory;
            treeView.Nodes.Clear();
            var rootNode = new TreeNode(ProjectName + "(" + _contentProject.ContentRoot + ")")
                               {Name = _contentProject.ContentRoot};
            treeView.Nodes.Add(rootNode);
            TreeNode lastNode = rootNode;
            //aaaand here comes the nasty part. Watch out, it bites...um bugs!
            foreach (
                var parts in
                    _contentProject.BuildItems.Select(
                        buildItem => buildItem.AssetName.Split(Path.DirectorySeparatorChar)).Where(
                            parts => parts.Length >= 2)) //all BuildItems which names contain more than two elements (ContentRoot + FileName), Split by SeperatorChar (=> platform independent coding :))
            {
                string folder = "";
                string parent = _contentProject.ContentRoot;
                for (int i = 0; i < parts.Length - 1; i++) //Examine everything between ContentRoot and fileName. If we find something, add a folder!
                {
                    if (parts[i] == null) continue; 
                    if (i > 0) //if there is already a path we need to add the new part with a SeperatorChar!
                        folder += Path.DirectorySeparatorChar + parts[i];
                    else
                        folder = parts[0]; //Yay! We are first! Let's make ourselves comfortable here!


                    if (parts.Length > 2 && i < parts.Length - 2) // if we have more than two parts we have another parent than the content root! 
                        parent += Path.DirectorySeparatorChar + parts[i];
                    //else if (parts.Length == 2)
                    //  parent += Path.DirectorySeparatorChar + parts[0];
                }
                lastNode = treeView.RecursiveSearch(parent); //Search for parent node! Often an Exception Candidate! Check the 'parent' var then.
                var node = new TreeNode(parts[parts.Length - 2])
                               {Name = _contentProject.ContentRoot + Path.DirectorySeparatorChar + folder}; //Finally glue a new folder node together
                if (!ContainsTreeNode(lastNode, node))
                {
                    lastNode.Nodes.Add(node); // If the folder is new, add it - else it's just wasted memory :)
                }
                lastNode = rootNode;
            }
            if (_contentProject.BuildItems.Count > 0) //Only do this when there are items, it'll get nasty soon if there isn't one!
            {
                foreach (BuildItem buildItem in _contentProject.BuildItems)
                {
                    String[] parts = null; //Split by seperator char
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
                    //Add the actual files to the tree in their apropriate subdirs
                    string path = _contentProject.ContentRoot;
                    if (parts != null) 
                    {
                        for (int i = 0; i < parts.Length - 1; i++)
                        {
                            path += String.IsNullOrEmpty(path) ? parts[i] : Path.DirectorySeparatorChar + parts[i];
                        }
                    }
                    if (parts != null)
                    {
                        TreeNode node = treeView.RecursiveSearch(path);
                        if (node == null) throw new ArgumentNullException("Node not found!");
                        var item = new TreeNode(parts[parts.Length - 1]) {Name = buildItem.AssetName};
                        node.Nodes.Add(item);
                    }
                    else //if the node is "forever alone", put him into the rootNode to make some friends!
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

        private void RibbonButtonCleanClick(object sender, EventArgs e)
        {
            if (!Directory.Exists(ProjectOutputDir)) return;
            if (MessageBox.Show(
                "You are about to delete stuff you previously built! That already built content will be lost forever (which is a very long time!). Still want to continue?",
                "Delete Output?", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                CleanProject();
                MessageBox.Show("Your build directory has been emptied. Goodbye Files!", "Success", MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
            }
        }

        private void RibbonButtonWebClick(object sender, EventArgs e)
        {
            Process.Start("http://anxframework.codeplex.com/");
        }

        private void RibbonButtonHelpClick(object sender, EventArgs e)
        {
            Process.Start("http://anxframework.codeplex.com/wikipage?title=Content%20Compiler");
        }
        #endregion

        #region WindowMoveMethods

        private void LabelTitleMouseMove(object sender, MouseEventArgs e)
        {
            if (!_mouseDown) return;
            int xoffset = MousePosition.X - _lastPos.X;
            int yoffset = MousePosition.Y - _lastPos.Y;
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

        private void TreeViewItemeLeave(object sender, EventArgs e)
        {
            ((ToolStripItem) sender).BackColor = Color.FromArgb(0, 64, 64, 64);
        }

        private void TreeViewItemMouseEnter(object sender, EventArgs e)
        {
            ((ToolStripItem) sender).BackColor = Color.Green;
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
            RecentProjects.Save();
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
                foreach (
                    BuildItem buildItem in
                        _contentProject.BuildItems.Where(
                            buildItem => buildItem.AssetName.Equals(treeView.SelectedNode.Name)))
                {
                    propertyGrid.SelectedObject = buildItem;
                }
            }
        }

        private bool ContainsTreeNode(TreeNode haystack, TreeNode needle)
        {
            return haystack.Nodes.Cast<TreeNode>().Any(node => node.Name.Equals(needle.Name));
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
                    buildItem.AssetName = buildItem.AssetName.Replace((string) e.OldValue, _contentProject.ContentRoot);
                }
                treeView.Nodes[0].RecursivelyReplacePartOfName((string) e.OldValue, _contentProject.ContentRoot);
            }
            ChangeEnvironmentOpenProject();
        }

        #endregion

        #region ContextMenuStuff
        private void FileToolStripMenuItemClick(object sender, EventArgs e)
        {
            using (var dlg = new OpenFileDialog())
            {
                dlg.Multiselect = true;
                dlg.Title = "Add files";
                if (dlg.ShowDialog() == DialogResult.OK)
                    AddFiles(dlg.FileNames);
            }
        }

        private void FolderToolStripMenuItemClick(object sender, EventArgs e)
        {
            using (var dlg = new NewFolderScreen())
            {
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    AddFolder(dlg.textBoxName.Text);
                }
            }
        }

        private void TreeViewItemDeleteClick(object sender, EventArgs e)
        {
            if (treeView.SelectedNode == null) return;
            if (treeView.SelectedNode == treeView.Nodes[0]) return;
            foreach (var buildItem in _contentProject.BuildItems.Where(buildItem => buildItem.AssetName == treeView.SelectedNode.Name))
            {
                RemoveFile(buildItem.AssetName);
                return;
            }
            RemoveFolder(treeView.SelectedNode.Name);
        }
        #endregion
    }
}