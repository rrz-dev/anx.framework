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

        private void StartState_Load(object sender, System.EventArgs e)
        {
            SetUpColor();
        }
    }
}