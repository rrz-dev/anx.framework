using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace ANX.ContentCompiler.GUI.Controls
{
    public partial class RibbonButton : UserControl
    {
        public RibbonButton()
        {
            InitializeComponent();
        }

        [Category("Content"), Description("Text der auf dem Button gezeigt werden soll.")]
        public String Content
        {
            get { return labelText.Text; }
            set { labelText.Text = value; }
        }

        [Category("Design"), Description("Das Bild, das als Icon dienen soll. (60x60)")]
        public Image Image
        {
            get { return pictureBox.Image; }
            set { pictureBox.Image = value; }
        }

        private void RibbonButton_MouseEnter(object sender, EventArgs e)
        {
            BackColor = Color.LightGray;
        }

        private void RibbonButton_MouseDown(object sender, MouseEventArgs e)
        {
            BackColor = Color.LimeGreen;
            OnClick(new EventArgs());
        }

        private void RibbonButton_MouseUp(object sender, MouseEventArgs e)
        {
            BackColor = Color.LightGray;
        }

        private void RibbonButton_MouseLeave(object sender, EventArgs e)
        {
            BackColor = Color.FromArgb(0, 64, 64, 64);
        }

        private void RibbonButton_MouseHover(object sender, EventArgs e)
        {
            //BackColor = Color.LightGray;
        }
    }
}