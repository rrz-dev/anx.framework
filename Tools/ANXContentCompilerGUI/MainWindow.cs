using System;
using System.Drawing;
using System.IO;
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

        #region EnvironmentStates
        public void ChangeEnvironmentStartState()
        {
            editingState.Visible = false;
            startState.Visible = true;
            Text = "ANX Content Compiler 4";
            labelTitle.Text = "ANX Content Compiler 4";
        }

        public void ChangeEnvironmentOpenProject()
        {
            startState.Visible = false;
            editingState.Visible = true;
            Text = ProjectName + " - ANX Content Compiler 4";
            labelTitle.Text = "ANX Content Compiler 4 - " + ProjectName;
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
    }
}