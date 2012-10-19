#region Using Statements
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
    public partial class FirstStartScreen : Form
    {
        #region Constructor
        public FirstStartScreen()
        {
            InitializeComponent();
            BackColor = Settings.MainColor;
            ForeColor = Settings.ForeColor;
            button1.FlatAppearance.MouseOverBackColor = Settings.LightMainColor;
            button2.FlatAppearance.MouseOverBackColor = Settings.LightMainColor;
            button3.FlatAppearance.MouseOverBackColor = Settings.LightMainColor;
            button1.FlatAppearance.MouseDownBackColor = Settings.AccentColor3;
            button2.FlatAppearance.MouseDownBackColor = Settings.AccentColor3;
            button3.FlatAppearance.MouseDownBackColor = Settings.AccentColor3;
        }
        #endregion

        #region Private Methods
        private void Button2Click(object sender, System.EventArgs e)
        {
            MainWindow.Instance.StartShow();
        }
        #endregion
    }
}