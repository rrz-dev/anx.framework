#region Using Statements
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using ANX.ContentCompiler.GUI.Dialogues;
using ANX.ContentCompiler.GUI.Properties;
using ANX.Framework.Content.Pipeline;
using ANX.Framework.Content.Pipeline.Tasks;
using ANX.Framework.NonXNA.Development;
using ANX.ContentCompiler.GUI.Nodes;
using ANX.ContentCompiler.GUI.Helpers;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.ServiceModel;
using ANX.Framework.Content.Pipeline.Tasks.References;
#endregion

// This file is part of the EES Content Compiler 4,
// © 2008 - 2012 by Eagle Eye Studios.
// The EES Content Compiler 4 is released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.ContentCompiler.GUI
{
    [Developer("SilentWarrior/Eagle Eye Studios, Konstantin Koch")]
    [PercentageComplete(90)] //TODO: Preview, Renaming of Folders!
    [TestState(TestStateAttribute.TestState.InProgress)]
    public partial class MainWindow : Form
    {
        #region Fields

        public static String DefaultOutputPath = "bin";
        public static String SettingsFile =
            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                         "ANX Content Compiler" + Path.DirectorySeparatorChar + "settings.ees");
		
		
        private bool _firstStart = true;

        private ContentProject _contentProject;
        private ContentProjectNodeProperties _contentProjectNodeProperties;
        private Point _lastPos;
        private bool _menuMode;
        private bool _mouseDown;
        private readonly string[] _args;
        private int _showCounter = 0;

        private readonly ImporterManager _iManager;
        private readonly ProcessorManager _pManager;
        private PreviewScreen _previewScreen;
        #endregion

        #region Properties

        public static MainWindow Instance { get; private set; }

        public Uri ProjectPath { get; set; }
        public Uri ProjectFolder { get; set; }
        public RecentProjects RecentProjects { get; set; }

        public Configuration ActiveConfiguration
        {
            get 
            {
                if (_contentProject == null)
                    throw new InvalidOperationException("No ContentProject loaded.");

                return GetConfiguration(_contentProject, _contentProjectNodeProperties.ActiveConfiguration, _contentProjectNodeProperties.ActivePlatform);
            }
            set 
            {
                if (value == null)
                {
                    throw new ArgumentNullException();
                }
                else
                {
                    _contentProjectNodeProperties.ActiveConfiguration = value.Name;
                    _contentProjectNodeProperties.ActivePlatform = value.Platform;
                }
            }
        }

        private static Configuration GetConfiguration(ContentProject project, string configurationName, TargetPlatform configurationPlatform)
        {
            if (project == null)
                throw new ArgumentNullException("project");

            Configuration result;
            project.Configurations.TryGetConfiguration(configurationName, configurationPlatform, out result);

            return result;
        }

        #endregion

        #region Init

        public MainWindow(string[] args)
        {
            InitializeComponent();
            Icon = Resources.anx;
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
            _iManager = new ImporterManager();
            _pManager = new ProcessorManager();
        }

        private void MainWindowShown(object sender, EventArgs e)
        {

            if (!_firstStart && Settings.ShowFirstStartScreen)
                _firstStart = true;
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
            ContentProject newContentProject;

            using (var dlg = new NewProjectScreen())
            {
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    newContentProject = new ContentProject(dlg.textBoxName.Text)
                        {
                            Creator = "ANX Content Compiler (4.0)",
                        };

                    ProjectFolder = new Uri(PathHelper.EnsureTrailingSlash(!String.IsNullOrEmpty(dlg.textBoxLocation.Text)
                                        ? dlg.textBoxLocation.Text
                                        : Path.Combine(Settings.DefaultProjectPath, newContentProject.Name)));
                    ProjectPath = new Uri(ProjectFolder, new Uri(newContentProject.Name + ".cproj", UriKind.Relative));

                    newContentProject.Configurations.Add(new Configuration("Release", TargetPlatform.Windows)
                    {
                        OutputDirectory = Path.Combine(DefaultOutputPath, "Release"),
                    });
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
                    var config = GetConfiguration(newContentProject, "Release", TargetPlatform.Windows);

                    config.OutputDirectory = !String.IsNullOrEmpty(dlg2.textBoxLocation.Text)
                                           ? dlg2.textBoxLocation.Text
                                           : DefaultOutputPath;
                }
                else
                    return;
            }
            using (var dlg3 = new NewProjectImportersScreen())
            {
                if (dlg3.ShowDialog() == DialogResult.OK)
                {
                    //Extend with extensions for other platforms, if necessary.
                    if (!string.IsNullOrWhiteSpace(dlg3.textBoxLocation.Text))
                    {
                        try
                        {
                            var files = Directory.GetFiles(dlg3.textBoxLocation.Text, "*.*").Where((x) => Path.GetExtension(x) == ".dll" || Path.GetExtension(x) == ".exe");
                            foreach (var file in files)
                            {
                                newContentProject.References.Add(new AssemblyReference() { AssemblyPath = file, Name = Path.GetFileNameWithoutExtension(file) });
                            }
                        }
                        catch
                        {
                            MessageBox.Show(this, ShowStrings.NoFilesForImporters);
                        }
                    }
                }
                else
                    return;
            }

            using (var dlg4 = new NewProjectSummaryScreen(newContentProject.Name, ProjectFolder.LocalPath, GetConfiguration(newContentProject, "Release", TargetPlatform.Windows).OutputDirectory))
            {
                dlg4.ShowDialog();
            }

            _contentProject = newContentProject;
            _contentProjectNodeProperties = new ContentProjectNodeProperties(_contentProject);
            ActiveConfiguration = _contentProject.Configurations[0];

            Directory.CreateDirectory(ProjectFolder.LocalPath);

            ChangeEnvironmentOpenProject();

            //Select the rootNode.
            treeView.SelectedNode = treeView.Nodes[0];

            SaveProject();
        }

        #endregion

        #region OpenProject

        public void OpenProjectDialog(object sender, EventArgs e)
        {
            using (var dlg = new OpenProjectScreen())
            {
				var result = dlg.ShowDialog();
                if (result != DialogResult.OK) return;

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
            ProjectFolder = new Uri(PathHelper.EnsureTrailingSlash(Path.GetDirectoryName(path)));
            ProjectPath = new Uri(path);

            if (string.IsNullOrEmpty(_contentProject.Creator))
                _contentProject.Creator = "ANX Content Compiler (4.0)";

            _contentProjectNodeProperties = new ContentProjectNodeProperties(_contentProject);

            if (_contentProject.Configurations.Count > 0)
            {
                //TODO: Try to restore previous setting for the project.
                this.ActiveConfiguration = _contentProject.Configurations[0];
            }

            ChangeEnvironmentOpenProject();

            if (RecentProjects.Contains(ProjectPath.LocalPath))
                RecentProjects.Remove(ProjectPath.LocalPath);
            RecentProjects.Add(ProjectPath.LocalPath);
            RecentProjects.Save();
        }

        #endregion

        #region SaveProject

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData.HasFlag(Keys.Control))
            {
                if (keyData.HasFlag(Keys.S))
                {
                    SaveProject(this, null);
                    return true;
                }
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        public void SaveProject(object sender, EventArgs e)
        {
            if (_contentProject == null) 
                return;
            if (!File.Exists(ProjectPath.LocalPath))
                SaveProjectAs(sender, e);

            SaveProject();
        }

        private void SaveProject()
        {
            _contentProject.Save(ProjectPath.LocalPath);
            if (RecentProjects.Contains(ProjectPath.LocalPath))
                RecentProjects.Remove(ProjectPath.LocalPath);
            RecentProjects.Add(ProjectPath.LocalPath);
            RecentProjects.Save();
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
                    ProjectPath = new Uri(dlg.FileName, UriKind.Absolute);
                    SaveProject(sender, e);
                }
            }
        }

        #endregion

        #region CleanProject
        public void CleanProject()
        {
            if (Directory.Exists(ActiveConfiguration.OutputDirectory))
                Directory.Delete(ActiveConfiguration.OutputDirectory, true);
        }
        #endregion

        #region BuildProject

        public void BuildProject(object sender, EventArgs e)
        {
            DisableUI();

            Task task = new Task(() =>
                {
                    List<string> arguments = new List<string>();

                    arguments.Add(ProjectPath.LocalPath);
                    arguments.Add("-c:" + ActiveConfiguration.Name);
                    arguments.Add("-t:" + ActiveConfiguration.Platform);
                    arguments.Add("-cd:" + ProjectFolder.LocalPath);

                    Uri loggerUri = new Uri("net.pipe://localhost/ContentCompilerGui/" + Process.GetCurrentProcess().Id + "/");

                    var loggerServiceHost = new ServiceHost(new CCompilerBuildLogger(), loggerUri);
                    try
                    {
                        loggerServiceHost.AddServiceEndpoint(typeof(IContentBuildLogger), new NetNamedPipeBinding(), "ContentBuildLogger");

                        loggerServiceHost.Open();

                        arguments.Add("-l:" + new Uri(loggerUri, new Uri("ContentBuildLogger", UriKind.RelativeOrAbsolute)).OriginalString);

                        //Prepare the arguments to be used for process start.
                        arguments = arguments.Select((x) => "\"" + Regex.Replace(x, "(\\\\*)(\\\\$|\")", "$1$1\\$2") + "\"").ToList();

                        string ccompilerPath = Directory.GetFiles(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "ContentBuilder.*").FirstOrDefault();

                        //TODO: might be an idea to use the normal buildContentTask instead of a separate process. A lot of things could go wrong here.
                        //Most straight forward one would be a hanging process and processes that are never closed.
                        Process.Start(new ProcessStartInfo()
                        {
                            FileName = ccompilerPath,
                            WindowStyle = ProcessWindowStyle.Hidden,
                            CreateNoWindow = true,
                            ErrorDialog = true,
                            Arguments = string.Join(" ", arguments)
                        }).WaitForExit();
                    }
                    finally
                    {
                        loggerServiceHost.Close();

                        //Callback to UI thread.
                        this.BeginInvoke(new Action(() =>
                        {
                            EnableUI();
                        }));
                    }
                });

            task.Start();
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
            {
                MessageBox.Show("Sorry, but there is a problem: The file you requested was not found!",
                                "Something went wrong", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Uri newFileUri = new Uri(ProjectFolder, new Uri(Path.GetFileName(file), UriKind.Relative));
            if (file != newFileUri.LocalPath)
            {
                try
                {
                    File.Copy(file, newFileUri.LocalPath, true);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Sorry, but there is a problem: " + ex.Message, "Something went wrong", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            var item = new BuildItem
            {
                AssetName = Path.GetFileNameWithoutExtension(file),
                SourceFilename = ProjectFolder.MakeRelativeUri(newFileUri).OriginalString,
                ImporterName = _iManager.GuessImporterByFileExtension(file),
                ProcessorName = _pManager.GetProcessorForImporter(_iManager.GetInstance(_iManager.GuessImporterByFileExtension(file)))
            };

            _contentProject.BuildItems.Add(item);
        }

        /// <summary>
        /// Wrapper for adding moar files!
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
            TreeNode node = treeView.SelectedNode;
            if (node == null)
                return;

            //Whitelist
            if (node.Name != "Folder" && node.Name != "Project")
            {
                MessageBox.Show("Can not add a file to a file!");
                return;
            }

            var newFolder = new TreeNode(name) { Name = "Folder" };
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
                for (int i = _contentProject.BuildItems.Count - 1; i >= 0; i--)
                {
                    if (_contentProject.BuildItems[i].AssetName.Contains(name.Replace("Content" + Path.DirectorySeparatorChar, "")))
                        RemoveFile(_contentProject.BuildItems[i].AssetName);
                }
            }
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
            Text = _contentProject.Name + " - ANX Content Compiler 4";
            labelTitle.Text = "ANX Content Compiler 4 - " + _contentProject.Name;

            if (treeView.Nodes.Count > 0)
            {
                //The only node that could changed in this method is the one for the Project, we make sure that we will find it later on by renaming it before get the paths of alle the expanded nodes.
                treeView.Nodes[0].Text = _contentProject.Name;
            }

            List<string> expandedNodes = new List<string>();
            foreach (var node in treeView.Nodes.GetAllNodes())
            {
                if (node.IsExpanded)
                {
                    expandedNodes.Add(node.GetPath());
                }
            }

            treeView.BeginUpdate();

            treeView.Nodes.Clear();
            var rootNode = new TreeNode(_contentProject.Name);
            treeView.Nodes.Add(rootNode);
            rootNode.Name = "Project";
            rootNode.Tag = this._contentProject;
            TreeNode lastNode = rootNode;
            foreach (var buildItem in _contentProject.BuildItems)
            {
                var directories = Path.GetDirectoryName(buildItem.SourceFilename).Split(new [] { Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar }, StringSplitOptions.RemoveEmptyEntries);
                TreeNode currentNode = rootNode;
                //Build folder hierarchy in tree view.
                foreach (string directory in directories)
                {
                    int nodeIndex = currentNode.Nodes.IndexOfKey(directory);
                    if (nodeIndex == -1)
                    {
                        var newNode = new TreeNode(directory) { Name = "Folder" };

                        currentNode.Nodes.Add(newNode);
                        currentNode = newNode;
                    }
                    else
                    {
                        currentNode = currentNode.Nodes[nodeIndex];
                    }
                }

                //Create the node for the build item.
                var node = new TreeNode(buildItem.AssetName) { Name = "Item", Tag = buildItem };
                currentNode.Nodes.Add(node);
            }

            foreach (var path in expandedNodes)
            {
                TreeNode targetNode = treeView.Nodes.FindNode(path);
                if (targetNode != null)
                    targetNode.Expand();
            }

            treeView.EndUpdate();
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
            if (!Directory.Exists(ActiveConfiguration.OutputDirectory)) return;
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
            if (_previewScreen != null)
            {
                _previewScreen.SetFile(null);
            }

            if (treeView.SelectedNode.Name == "Project")
                propertyGrid.SelectedObject = _contentProjectNodeProperties;
            else if (treeView.SelectedNode.Name == "Folder")
            {
                propertyGrid.SelectedObject = new FolderNodeProperties(treeView.SelectedNode.Text);
            }
            else if (treeView.SelectedNode.Name == "Item")
            {
                var buildItem = (BuildItem)treeView.SelectedNode.Tag;

                propertyGrid.SelectedObject = new BuildItemNodeProperties(buildItem);

                if (_previewScreen != null)
                {
                    _previewScreen.SetFile(buildItem);
                }
            }
            else
                throw new NotImplementedException();
        }

        private bool ContainsTreeNode(TreeNode haystack, TreeNode needle)
        {
            return haystack.Nodes.Cast<TreeNode>().Any(node => node.Name.Equals(needle.Name));
        }

        #endregion

        #region PropertyGridEvents

        private void PropertyGridPropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            if (e.ChangedItem.Parent == null || e.ChangedItem.Parent.Parent == null)
                return;

            var parent = e.ChangedItem.Parent.Parent;

            if (parent.Value is ContentProjectNodeProperties)
            {
                ChangeEnvironmentOpenProject();
            }
            else if (parent.Value is FolderNodeProperties)
            {
                if (e.ChangedItem.Label == "Name")
                {
                    //Collect all folders up the tree until we hit the Project node, wenn that happens, we concat all folders with the project folder path and have our old path.
                    //We then change the last segment of the path to the new name and rename the folder with that info.
                    List<string> folderParts = new List<string>();

                    var currentNode = treeView.SelectedNode;
                    while (currentNode != null && currentNode.Name == "Folder")
                    {
                        folderParts.Add(currentNode.Text);

                        currentNode = currentNode.Parent;
                    }

                    folderParts.Reverse();
                    string oldRelativePath = Path.Combine(folderParts.ToArray());
                    Uri oldFolder = new Uri(ProjectFolder, new Uri(oldRelativePath, UriKind.Relative));

                    treeView.SelectedNode.Text = (string)e.ChangedItem.Value;

                    folderParts[folderParts.Count - 1] = treeView.SelectedNode.Text;
                    string newRelativePath = Path.Combine(folderParts.ToArray());
                    Uri newFolder = new Uri(ProjectFolder, new Uri(newRelativePath, UriKind.Relative));

                    Directory.Move(oldFolder.LocalPath, newFolder.LocalPath);

                    foreach (var buildItem in treeView.SelectedNode.Nodes.GetAllNodes().Where((x) => x.Name == "Item").Select((x) => (BuildItem)x.Tag))
                    {
                        //Should be everyone of them
                        if (buildItem.SourceFilename.StartsWith(oldRelativePath))
                        {
                            buildItem.SourceFilename = newRelativePath + buildItem.SourceFilename.Substring(oldRelativePath.Length);
                        }
                        else
                            throw new InvalidOperationException("Expected all child buildItems of a folder to be contained inside the folder, but apparently that is not the case.");
                    }
                }
            }
            else if (parent.Value is BuildItemNodeProperties)
            {
                if (e.ChangedItem.Label == "AssetName")
                    treeView.SelectedNode.Text = (string)e.ChangedItem.Value;
            }
        }

        #endregion

        #region ContextMenuStuff
        private void FileToolStripMenuItemClick(object sender, EventArgs e)
        {
			if (_contentProject == null)
				return;
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
			if (_contentProject == null)
				return;
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
			if (_contentProject == null)
				return;
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

        #region TourMethods
        public void StartShow()
        {
            propertyGrid.Visible = false;
            treeView.Visible = false;
            editingState.Visible = false;
            startState.Visible = false;
            ribbonButtonClean.Enabled = false;
            ribbonButtonHelp.Enabled = false;
            ribbonButtonLoad.Enabled = false;
            ribbonButtonNew.Enabled = false;
            ribbonButtonSave.Enabled = false;
            ribbonButtonWeb.Enabled = false;
            ribbonTextBox.Enabled = false;
            buttonMenu.Enabled = false;

            show_timer.Start();
        }

        public void EndShow()
        {
            show_timer.Stop();
            show_labelDesc.Visible = false;
            show_pictureBoxSmiley.Visible = false;
            
            propertyGrid.Visible = true;
            treeView.Visible = true;
            startState.Visible = true;
            ribbonButtonClean.Enabled = true;
            ribbonButtonHelp.Enabled = true;
            ribbonButtonLoad.Enabled = true;
            ribbonButtonNew.Enabled = true;
            ribbonButtonSave.Enabled = true;
            ribbonButtonWeb.Enabled = true;
            ribbonTextBox.Enabled = true;
            buttonMenu.Enabled = true;
            Settings.ShowFirstStartScreen = false;
        }

        private void ShowTimerTick(object sender, EventArgs e)
        {
            switch(_showCounter)
            {
                case 0:
                    show_timer.Interval = 8000;
                    startState.Visible = false;
                    show_pictureBoxSmiley.Visible = true;
                    show_labelDesc.Visible = true;
                    show_labelDesc.Text = ShowStrings.Start;
                    break;
                case 1:
                    show_timer.Interval = 6000;
                    show_labelDesc.Text = ShowStrings.Start2;
                    break;
                case 2:
                    show_timer.Interval = 9000;
                    show_pictureBoxMainPanel.Visible = true;
                    show_labelDesc.Text = ShowStrings.ActionPanel;
                    editingState.Enabled = false;
                    editingState.Visible = true;
                    break;
                case 3:
                    show_timer.Interval = 9000;
                    editingState.Enabled = true;
                    editingState.Visible = false;
                    show_pictureBoxMainPanel.Visible = false;
                    show_pictureBoxProjectExplorer.Visible = true;
                    show_pictureBoxSmiley.BackColor = Settings.DarkMainColor;
                    show_labelDesc.Text = ShowStrings.TreeView;
                    treeView.Visible = true;
                    treeView.Enabled = false;
                    break;
                case 4:
                    show_timer.Interval = 8000;
                    treeView.Enabled = true;
                    treeView.Visible = false;
                    show_pictureBoxSmiley.BackColor = Settings.MainColor;
                    show_pictureBoxProjectExplorer.Visible = false;
                    show_pictureBoxProperties.Visible = true;
                    show_labelDesc.Text = ShowStrings.PropertyGrid;
                    propertyGrid.Visible = true;
                    propertyGrid.Enabled = false;
                    break;
                case 5:
                    show_timer.Interval = 6000;
                    propertyGrid.Visible = false;
                    propertyGrid.Enabled = true;
                    show_pictureBoxProperties.Visible = false;
                    show_pictureBoxRibbon.Visible = true;
                    show_labelDesc.Text = ShowStrings.RibbonButtons;
                    break;
                case 6:
                    show_timer.Interval = 8000;
                    show_pictureBoxRibbon.Visible = false;
                    show_pictureBoxMenu.Visible = true;
                    show_labelDesc.Text = ShowStrings.Menu;
                    break;
                case 7:
                    show_timer.Interval = 11000;
                    show_pictureBoxMenu.Visible = false;
                    show_pictureBoxErrorLog.Visible = true;
                    show_labelDesc.Text = ShowStrings.LogBox;
                    break;
                case 8:
                    show_timer.Interval = 7000;
                    show_pictureBoxErrorLog.Visible = false;
                    show_labelDesc.Text = ShowStrings.End;
                    break;
                case 9:
                    show_timer.Interval = 2000;
                    EndShow();
                    break;
            }
            _showCounter++;
        }

        #endregion

        #region ShowPreview
        internal void ShowPreview()
        {
            BuildItem buildItem = null;
            if (treeView.SelectedNode == null)
                return;
            foreach (var item in _contentProject.BuildItems.Where(item => item.AssetName == treeView.SelectedNode.Text))
            {
                buildItem = item;
            }

            if (_previewScreen == null)
                _previewScreen = new PreviewScreen();
            
            _previewScreen.Show();
            _previewScreen.SetFile(buildItem);
        }
        #endregion
    }
}