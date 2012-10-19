#region Using Statements
using System;
using System.Windows.Forms;
using ANX.Framework.NonXNA.Development;
#endregion

// This file is part of the EES Content Compiler 4,
// © 2008 - 2012 by Eagle Eye Studios.
// The EES Content Compiler 4 is released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.ContentCompiler.GUI.States
{
    [Developer("SilentWarrior/Eagle Eye Studios")]
    [PercentageComplete(100)]
    [TestState(TestStateAttribute.TestState.Tested)]
    public partial class StartState : UserControl
    {
        public StartState()
        {
            InitializeComponent();
        }

        private void SetUpColor()
        {
            BackColor = Settings.MainColor;
            ForeColor = Settings.ForeColor;
        }

        private void StartStateLoad(object sender, EventArgs e)
        {
            SetUpColor();
        }

        private void ArrowButtonNewClick(object sender, EventArgs e)
        {
            MainWindow.Instance.NewProject(sender, e);
        }

        private void ArrowButtonLoadClick(object sender, EventArgs e)
        {
            MainWindow.Instance.OpenProjectDialog(sender, e);
        }
    }
}