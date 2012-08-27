using System;
using System.Windows.Forms;

namespace ANX.ContentCompiler.GUI.States
{
    public partial class EditingState : UserControl
    {
        public EditingState()
        {
            InitializeComponent();
        }

        private void EditingStateLoad(object sender, EventArgs e)
        {
            ForeColor = Settings.ForeColor;
            BackColor = Settings.MainColor;
        }

    }
}
