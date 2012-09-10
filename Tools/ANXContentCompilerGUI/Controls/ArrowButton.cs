using System;
using System.ComponentModel;
using System.Windows.Forms;
using ANX.Framework.NonXNA.Development;

namespace ANX.ContentCompiler.GUI.Controls
{
    [Developer("SilentWarrior/Eagle Eye Studios")]
    [PercentageComplete(100)]
    [TestState(TestStateAttribute.TestState.Tested)]
    public partial class ArrowButton : UserControl
    {
        public ArrowButton()
        {
            InitializeComponent();
        }

        [EditorBrowsable(EditorBrowsableState.Always)]
        public String Content
        {
            get { return labelText.Text; }
            set { labelText.Text = value; }
        }

        private void ArrowButtonMouseEnter(object sender, EventArgs e)
        {
            BorderStyle = BorderStyle.FixedSingle;
        }

        private void ArrowButtonMouseLeave(object sender, EventArgs e)
        {
            BorderStyle = BorderStyle.None;
        }

        private void ArrowButtonMouseDown(object sender, MouseEventArgs e)
        {
            BackColor = Settings.AccentColor3;
        }

        private void ArrowButtonMouseUp(object sender, MouseEventArgs e)
        {
            BackColor = Settings.MainColor;
        }

        private void PictureBox1Click(object sender, EventArgs e)
        {
            OnClick(e);
        }

        private void LabelTextClick(object sender, EventArgs e)
        {
            OnClick(e);
        }

        private void ArrowButtonFontChanged(object sender, EventArgs e)
        {
            labelText.Font = Font;
        }

        private void ArrowButtonLoad(object sender, EventArgs e)
        {
            BackColor = Settings.MainColor;
            ForeColor = Settings.ForeColor;
        }
    }
}