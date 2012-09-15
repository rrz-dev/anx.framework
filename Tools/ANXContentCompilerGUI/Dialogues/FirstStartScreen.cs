using System.Windows.Forms;
using ANX.Framework.NonXNA.Development;

namespace ANX.ContentCompiler.GUI.Dialogues
{
    [Developer("SilentWarrior/Eagle Eye Studios")]
    [PercentageComplete(100)] //TODO: Implement tour in MainWindow and launch it from here!
    [TestState(TestStateAttribute.TestState.Tested)]
    public partial class FirstStartScreen : Form
    {
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

        private void Button2Click(object sender, System.EventArgs e)
        {
            MainWindow.Instance.StartShow();
        }
    }
}