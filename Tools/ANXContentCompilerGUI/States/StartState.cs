using System.Windows.Forms;

namespace ANX.ContentCompiler.GUI.States
{
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

        private void StartStateLoad(object sender, System.EventArgs e)
        {
            SetUpColor();
        }

        private void ArrowButtonNewClick(object sender, System.EventArgs e)
        {
            MainWindow.Instance.NewProject(sender, e);
        }

        private void ArrowButtonLoadClick(object sender, System.EventArgs e)
        {
            MainWindow.Instance.OpenProject(sender, e);
        }
    }
}