#region Using Statements
using System;
using System.Windows.Forms;
using ANX.Framework.NonXNA.Development;
#endregion

// This file is part of the EES Content Compiler 4,
// © 2008 - 2012 by Eagle Eye Studios.
// The EES Content Compiler 4 is released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.ContentCompiler.GUI.Dialogues
{
    [Developer("SilentWarrior/Eagle Eye Studios")]
    [PercentageComplete(100)]
    [TestState(TestStateAttribute.TestState.InProgress)]
    public partial class NewProjectImportersScreen : Form
    {
        #region Constructor
        public NewProjectImportersScreen()
        {
            InitializeComponent();
            SetUpColors();
        }
        #endregion

        #region Private Methods
        private void SetUpColors()
        {
            BackColor = Settings.MainColor;
            ForeColor = Settings.ForeColor;
            buttonClose.FlatAppearance.MouseOverBackColor = Settings.LightMainColor;
            buttonBrowse.FlatAppearance.MouseOverBackColor = Settings.LightMainColor;
            buttonCancel.FlatAppearance.MouseOverBackColor = Settings.LightMainColor;
            buttonNext.FlatAppearance.MouseOverBackColor = Settings.LightMainColor;
            buttonClose.FlatAppearance.MouseDownBackColor = Settings.AccentColor3;
            buttonBrowse.FlatAppearance.MouseDownBackColor = Settings.AccentColor3;
            buttonCancel.FlatAppearance.MouseDownBackColor = Settings.AccentColor3;
            buttonNext.FlatAppearance.MouseDownBackColor = Settings.AccentColor3;
            buttonNext.FlatAppearance.BorderColor = Settings.LightMainColor;
            buttonClose.FlatAppearance.BorderColor = Settings.LightMainColor;
            buttonBrowse.FlatAppearance.BorderColor = Settings.LightMainColor;
            buttonCancel.FlatAppearance.BorderColor = Settings.LightMainColor;
            textBoxLocation.BackColor = Settings.DarkMainColor;
            textBoxLocation.ForeColor = Settings.ForeColor;
        }

        private void ArrowButtonYesClick(object sender, EventArgs e)
        {
            labelLocation.Visible = true;
            textBoxLocation.Visible = true;
            buttonBrowse.Visible = true;
            buttonNext.Enabled = true;
        }

        private void ArrowButtonNoClick(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void ButtonBrowseClick(object sender, EventArgs e)
        {
            using (var dlg = new FolderBrowserDialog())
            {
                dlg.ShowNewFolderButton = true;
                dlg.RootFolder = Environment.SpecialFolder.MyComputer;
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    textBoxLocation.Text = dlg.SelectedPath;
                }
            }
            DialogResult = DialogResult.None;
        }

        private void ButtonNextClick(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(textBoxLocation.Text))
                MessageBox.Show("You need to specify the directory where your Importers/Processors are located in!",
                                "Missing value", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
                DialogResult = DialogResult.OK;
        }
        #endregion
    }
}