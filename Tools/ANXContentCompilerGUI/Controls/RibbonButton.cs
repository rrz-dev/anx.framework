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

        private void RibbonButtonMouseEnter(object sender, EventArgs e)
        {
            BackColor = Settings.LightMainColor;
        }

        private void RibbonButtonMouseDown(object sender, MouseEventArgs e)
        {
            BackColor = Settings.AccentColor;
            OnClick(new EventArgs());
        }

        private void RibbonButtonMouseUp(object sender, MouseEventArgs e)
        {
            BackColor = Settings.LightMainColor;
        }

        private void RibbonButtonMouseLeave(object sender, EventArgs e)
        {
            BackColor = Settings.MainColor;
        }

        private void RibbonButtonMouseHover(object sender, EventArgs e)
        {
            BackColor = Settings.LightMainColor;
        }

        private void RibbonButtonLoad(object sender, EventArgs e)
        {
            BackColor = Settings.MainColor;
            ForeColor = Settings.ForeColor;
            Refresh();
        }
    }
}