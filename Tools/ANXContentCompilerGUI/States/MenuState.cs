using System;
using System.Reflection;
using System.Windows.Forms;
using ANX.ContentCompiler.GUI.Dialogues;
using ANX.Framework.NonXNA.Development;

namespace ANX.ContentCompiler.GUI.States
{
    [Developer("SilentWarrior/Eagle Eye Studios")]
    [PercentageComplete(90)] //TODO: Actually implement Import dialogue, for now just using the usual one; Fix weird width issue on Mono/Linux
    [TestState(TestStateAttribute.TestState.Tested)]
    public partial class MenuState : UserControl
    {
        public MenuState()
        {
            InitializeComponent();
        }

        #region HelperMethods

        private void SetUpColors()
        {
            labelVersion.Text = "ANX Content Compiler v" + Assembly.GetExecutingAssembly().GetName().Version;
            BackColor = Settings.MainColor;
            ForeColor = Settings.ForeColor;
            panel1.BackColor = Settings.AccentColor3;
            buttonClose.BackColor = Settings.AccentColor3;
            buttonExit.BackColor = Settings.AccentColor3;
            buttonNew.BackColor = Settings.AccentColor3;
            buttonOpen.BackColor = Settings.AccentColor3;
            buttonSave.BackColor = Settings.AccentColor3;
            buttonSaveAs.BackColor = Settings.AccentColor3;
            buttonSettings.BackColor = Settings.AccentColor3;
            buttonClose.FlatAppearance.BorderColor = Settings.AccentColor2;
            buttonExit.FlatAppearance.BorderColor = Settings.AccentColor2;
            buttonNew.FlatAppearance.BorderColor = Settings.AccentColor2;
            buttonOpen.FlatAppearance.BorderColor = Settings.AccentColor2;
            buttonSave.FlatAppearance.BorderColor = Settings.AccentColor2;
            buttonSaveAs.FlatAppearance.BorderColor = Settings.AccentColor2;
            buttonSettings.FlatAppearance.BorderColor = Settings.AccentColor2;
            buttonClose.FlatAppearance.MouseOverBackColor = Settings.AccentColor2;
            buttonExit.FlatAppearance.MouseOverBackColor = Settings.AccentColor2;
            buttonNew.FlatAppearance.MouseOverBackColor = Settings.AccentColor2;
            buttonOpen.FlatAppearance.MouseOverBackColor = Settings.AccentColor2;
            buttonSave.FlatAppearance.MouseOverBackColor = Settings.AccentColor2;
            buttonSaveAs.FlatAppearance.MouseOverBackColor = Settings.AccentColor2;
            buttonSettings.FlatAppearance.MouseDownBackColor = Settings.AccentColor2;
            checkBoxShowWelcomeScreen.Checked = Settings.ShowFirstStartScreen;
        }

        private void ResetMenuState()
        {
            buttonNew.BackColor = Settings.AccentColor3;
            buttonOpen.BackColor = Settings.AccentColor3;
            buttonSave.BackColor = Settings.AccentColor3;
            buttonSaveAs.BackColor = Settings.AccentColor3;
            buttonClose.BackColor = Settings.AccentColor3;
            buttonSettings.BackColor = Settings.AccentColor3;
            buttonExit.BackColor = Settings.AccentColor3;
            panelNew.Visible = false;
            panelOpen.Visible = false;
            panelSaveAs.Visible = false;
            panelSettings.Visible = false;
        }

        #endregion

        #region MenuButtons

        private void ButtonNewClick(object sender, EventArgs e)
        {
            ResetMenuState();
            buttonNew.BackColor = Settings.AccentColor;
            panelNew.Visible = true;
        }

        private void ButtonOpenClick(object sender, EventArgs e)
        {
            ResetMenuState();
            buttonOpen.BackColor = Settings.AccentColor;
            panelOpen.Visible = true;
        }

        private void ButtonSaveClick(object sender, EventArgs e)
        {
            MainWindow.Instance.SaveProject(sender, e);
        }

        private void ButtonSaveAsClick(object sender, EventArgs e)
        {
            ResetMenuState();
            buttonSaveAs.BackColor = Settings.AccentColor;
            panelSaveAs.Visible = true;
        }

        private void ButtonCloseClick(object sender, EventArgs e)
        {
            ResetMenuState();
            Visible = false;
            MainWindow.Instance.ToggleMenuMode();
            MainWindow.Instance.ChangeEnvironmentStartState();
        }

        private void ButtonSettingsClick(object sender, EventArgs e)
        {
            ResetMenuState();
            buttonSettings.BackColor = Settings.AccentColor;
            panelSettings.Visible = true;
        }

        private void ButtonExitClick(object sender, EventArgs e)
        {
            ResetMenuState();
            Application.Exit();
        }

        #endregion

        #region MenuNew

        private void ArrowButtonFileClick(object sender, EventArgs e)
        {
            using (var dlg = new OpenFileDialog())
            {
                dlg.Multiselect = true;
                dlg.Title = "Add files";
                if (dlg.ShowDialog() == DialogResult.OK)
                    MainWindow.Instance.AddFiles(dlg.FileNames);
            }
            MainWindow.Instance.ToggleMenuMode();
        }

        private void ArrowButtonFolderClick(object sender, EventArgs e)
        {
            using (var dlg = new NewFolderScreen())
            {
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    MainWindow.Instance.AddFolder(dlg.textBoxName.Text);
                }
            }
            MainWindow.Instance.ToggleMenuMode();
        }

        private void ArrowButtonNewProjectClick(object sender, EventArgs e)
        {
            MainWindow.Instance.NewProject(sender, e);
        }

        #endregion

        #region MenuOpen

        private void ArrowButtonOpenClick(object sender, EventArgs e)
        {
            MainWindow.Instance.OpenProjectDialog(sender, e);
            MainWindow.Instance.ToggleMenuMode();
        }

        private void ArrowButtonImportClick(object sender, EventArgs e)
        {
            MainWindow.Instance.OpenProjectDialog(sender, e);
            MainWindow.Instance.ToggleMenuMode();
        }

        #endregion

        #region MenuSave

        private void ArrowButtonSaveAsCprojClick(object sender, EventArgs e)
        {
            MainWindow.Instance.SaveProjectAs(sender, e);
            MainWindow.Instance.ToggleMenuMode();
        }

        private void ArrowButtonSaveAsCcProjClick(object sender, EventArgs e)
        {
            MainWindow.Instance.SaveProjectAs(sender, e);
            MainWindow.Instance.ToggleMenuMode();
        }

        #endregion

        #region Load
        private void MenuState_Load(object sender, EventArgs e)
        {
            SetUpColors();
            ResetMenuState();
            buttonNew.BackColor = Settings.AccentColor;
            panelNew.Visible = true;
        }
        #endregion

        #region Options Stuff
        private void CheckBoxShowWelcomeScreenCheckedChanged(object sender, EventArgs e)
        {
            Settings.ShowFirstStartScreen = ((CheckBox)sender).Checked;
        }

        #endregion
    }
}