using System.Windows.Forms;

namespace ANX.ContentCompiler.GUI.Dialogues
{
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
    }
}