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
    public partial class NewProjectSummaryScreen : Form
    {
        #region Contructor
        public NewProjectSummaryScreen(String projectName, String projectDir, String outputDir, bool customImporters,
                                       String customImportersDir, int customImportersFound, int customProcessorsFound)
        {
            InitializeComponent();
            SetUpColors();
            textBox.Text =
                "Summary for new project " + projectName + Environment.NewLine +
                "=========================================" + Environment.NewLine +
                "Name: " + projectName + Environment.NewLine +
                "Media Directory: " + projectDir + Environment.NewLine + Environment.NewLine +
                "Output Directory: " + outputDir + Environment.NewLine + Environment.NewLine +
                "Custom Importers/Processors: " + customImporters + Environment.NewLine;
            if (customImporters)
            {
                textBox.Text +=
                    "Custom Importers/Processors Location: " + customImportersDir + Environment.NewLine +
                    Environment.NewLine +
                    "Importers/Processors found in given Location:" + Environment.NewLine +
                    "Importers: " + customImportersFound + Environment.NewLine +
                    "Processors: " + customProcessorsFound;
            }
        }
        #endregion

        #region Private Methods
        private void SetUpColors()
        {
            BackColor = Settings.MainColor;
            ForeColor = Settings.ForeColor;
            buttonClose.FlatAppearance.MouseOverBackColor = Settings.LightMainColor;
            buttonNext.FlatAppearance.MouseOverBackColor = Settings.LightMainColor;
            buttonClose.FlatAppearance.MouseDownBackColor = Settings.AccentColor3;
            buttonNext.FlatAppearance.MouseDownBackColor = Settings.AccentColor3;
            buttonNext.FlatAppearance.BorderColor = Settings.LightMainColor;
            buttonClose.FlatAppearance.BorderColor = Settings.LightMainColor;
            textBox.BackColor = Settings.DarkMainColor;
            textBox.ForeColor = Settings.ForeColor;
        }

        private void ButtonNextClick(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }
        #endregion
    }
}