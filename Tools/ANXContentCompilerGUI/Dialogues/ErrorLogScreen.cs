using System;
using System.Windows.Forms;
using ANX.Framework.NonXNA.Development;

namespace ANX.ContentCompiler.GUI.Dialogues
{
    [Developer("SilentWarrior/Eagle Eye Studios")]
    [PercentageComplete(100)]
    [TestState(TestStateAttribute.TestState.Tested)]
    public partial class ErrorLogScreen : Form
    {
        public ErrorLogScreen(string[] errorLog)
        {
            InitializeComponent();
            SetUpColors();
            textBox.Lines = errorLog;
        }

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

        private void ButtonNextClick(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private void ButtonCancelClick(object sender, EventArgs e)
        {
            Clipboard.SetText(textBox.Text);
            buttonCancel.Text = "Copied.";
        }
    }
}