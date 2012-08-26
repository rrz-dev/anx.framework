using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace ANX.ContentCompiler.GUI.Controls
{
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

        private void ArrowButton_MouseEnter(object sender, EventArgs e)
        {
            BorderStyle = BorderStyle.FixedSingle;
        }

        private void ArrowButton_MouseLeave(object sender, EventArgs e)
        {
            BorderStyle = BorderStyle.None;
        }

        private void ArrowButton_MouseDown(object sender, MouseEventArgs e)
        {
            BackColor = Color.Green;
        }

        private void ArrowButton_MouseUp(object sender, MouseEventArgs e)
        {
            BackColor = Color.Transparent;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            OnClick(e);
        }

        private void labelText_Click(object sender, EventArgs e)
        {
            OnClick(e);
        }
    }
}