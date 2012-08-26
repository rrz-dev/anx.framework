using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using ANX.ContentCompiler.GUI.Dialogues;

namespace ANX.ContentCompiler.GUI
{
    public partial class MainWindow : Form
    {
        #region Fields
        public static String DefaultProjectPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "ANX Content Compiler\\4.0\\");
        public static String DefaultOutputPath = "bin";

        private Point lastPos;
        private bool mouseDown;
        #endregion

        #region Properties
        public static MainWindow Instance { get; private set; }

        public String ProjectName { get; set; }
        public String ProjectPath { get; set; }
        public String ProjectFolder { get; set; }
        public String ProjectOutputDir { get; set; }
        public String ProjectImportersDir { get; set; }
        #endregion

        #region Constructor
        public MainWindow()
        {
            InitializeComponent();
            Instance = this;
            treeViewItemAddFolder.MouseEnter += TreeViewItemMouseEnter;
            treeViewItemAddFolder.MouseLeave += TreeViewItemeLeave;
            treeViewItemDelete.MouseEnter += TreeViewItemMouseEnter;
            treeViewItemRename.MouseEnter += TreeViewItemMouseEnter;
            
        }
        #endregion

        #region NewProject
        private void RibbonButtonNewClick(object sender, EventArgs e)
        {
            using (var dlg = new NewProjectScreen())
            {
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    ProjectName = dlg.textBoxName.Text;
                    ProjectFolder = !String.IsNullOrEmpty(dlg.textBoxLocation.Text) ? dlg.textBoxLocation.Text : Path.Combine(DefaultProjectPath, ProjectName);
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
            ChangeEnvironmentOpenProject();
        }
        #endregion

        #region EnvironmentStates
        private void ChangeEnvironmentStartState()
        {
            editingState.Visible = false;
            startState.Visible = true;
            Text = "ANX Content Compiler 4";
            labelTitle.Text = "ANX Content Compiler 4";
        }

        private void ChangeEnvironmentOpenProject()
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
        #endregion

        #region WindowMoveMethods
        private void LabelTitleMouseMove(object sender, MouseEventArgs e)
        {
            if (!mouseDown) return;
            var xoffset = MousePosition.X - lastPos.X;
            var yoffset = MousePosition.Y - lastPos.Y;
            Left += xoffset;
            Top += yoffset;
            lastPos = MousePosition;
        }

        private void LabelTitleMouseDown(object sender, MouseEventArgs e)
        {
            mouseDown = true;
            lastPos = MousePosition;
        }

        private void LabelTitleMouseUp(object sender, MouseEventArgs e)
        {
            mouseDown = false;
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
    }
}