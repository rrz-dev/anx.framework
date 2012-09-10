using System;
using System.Windows.Forms;
using ANX.Framework.NonXNA.Development;

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