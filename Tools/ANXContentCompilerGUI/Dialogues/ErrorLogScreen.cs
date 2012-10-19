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
    public partial class ErrorLogScreen : Form
    {
        #region Constructor
        public ErrorLogScreen(string[] errorLog)
        {
            InitializeComponent();
            SetUpColors();
            textBox.Lines = errorLog;
        }
        #endregion

        #region Private Methods
        private void SetUpColors()
        {
            BackColor = Settings.MainColor;
            ForeColor = Settings.ForeColor;
            button3.FlatAppearance.MouseOverBackColor = Settings.LightMainColor;
            buttonCancel.FlatAppearance.MouseOverBackColor = Settings.LightMainColor;
            buttonNext.FlatAppearance.MouseOverBackColor = Settings.LightMainColor;
            button3.FlatAppearance.MouseDownBackColor = Settings.AccentColor3;
            buttonCancel.FlatAppearance.MouseDownBackColor = Settings.AccentColor3;
            buttonNext.FlatAppearance.MouseDownBackColor = Settings.AccentColor3;
            buttonNext.FlatAppearance.BorderColor = Settings.LightMainColor;
            button3.FlatAppearance.BorderColor = Settings.LightMainColor;
            buttonCancel.FlatAppearance.BorderColor = Settings.LightMainColor;
        }

        #region Events
        private void ButtonNextClick(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private void ButtonCancelClick(object sender, EventArgs e)
        {
            Clipboard.SetText(textBox.Text);
            buttonCancel.Text = "Copied.";
        }
        #endregion
        #endregion
    }
}