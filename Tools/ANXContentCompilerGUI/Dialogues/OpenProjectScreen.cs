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
    [TestState(TestStateAttribute.TestState.Tested)]
    public partial class OpenProjectScreen : Form
    {
        #region Constructor
        public OpenProjectScreen()
        {
            InitializeComponent();
            SetUpColors();
            listBoxRecentProjects.Items.Clear();
            foreach (string project in MainWindow.Instance.RecentProjects)
            {
                listBoxRecentProjects.Items.Add(project);
            }
        }
        #endregion

        #region Private Methods
        private void SetUpColors()
        {
            BackColor = Settings.MainColor;
            ForeColor = Settings.ForeColor;
            button3.FlatAppearance.MouseOverBackColor = Settings.LightMainColor;
            buttonBrowse.FlatAppearance.MouseOverBackColor = Settings.LightMainColor;
            buttonCancel.FlatAppearance.MouseOverBackColor = Settings.LightMainColor;
            buttonNext.FlatAppearance.MouseOverBackColor = Settings.LightMainColor;
            button3.FlatAppearance.MouseDownBackColor = Settings.AccentColor3;
            buttonBrowse.FlatAppearance.MouseDownBackColor = Settings.AccentColor3;
            buttonCancel.FlatAppearance.MouseDownBackColor = Settings.AccentColor3;
            buttonNext.FlatAppearance.MouseDownBackColor = Settings.AccentColor3;
            buttonNext.FlatAppearance.BorderColor = Settings.LightMainColor;
            button3.FlatAppearance.BorderColor = Settings.LightMainColor;
            buttonBrowse.FlatAppearance.BorderColor = Settings.LightMainColor;
            buttonCancel.FlatAppearance.BorderColor = Settings.LightMainColor;
            listBoxRecentProjects.BackColor = Settings.DarkMainColor;
            textBoxLocation.BackColor = Settings.DarkMainColor;
            listBoxRecentProjects.ForeColor = Settings.LightMainColor;
            textBoxLocation.ForeColor = Settings.ForeColor;
        }

        private void ButtonBrowseClick(object sender, EventArgs e)
        {
            using (var dlg = new OpenFileDialog())
            {
                dlg.Filter = "ANX Content Project (*.cproj)|*.cproj|Compressed Content Projekt (*.ccproj)|*.ccproj";
                dlg.Multiselect = false;
                dlg.Title = "Select project to open";
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    textBoxLocation.Text = dlg.FileName;
                }
            }
            DialogResult = DialogResult.None;
        }

        private void ButtonNextClick(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(textBoxLocation.Text) && listBoxRecentProjects.SelectedItem == null)
                MessageBox.Show("You need to select a project!", "Missing value", MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
            else
                DialogResult = DialogResult.OK;
        }
        #endregion
    }
}