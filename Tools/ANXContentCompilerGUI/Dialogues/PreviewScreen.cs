using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ANX.ContentCompiler.GUI.Dialogues
{
    public partial class PreviewScreen : Form
    {
        #region Fields
        private Point _lastPos;
        private bool _mouseDown;
        #endregion

        public PreviewScreen()
        {
            InitializeComponent();
        }

        #region WindowMoveMethods

        private void LabelTitleMouseMove(object sender, MouseEventArgs e)
        {
            if (!_mouseDown) return;
            int xoffset = MousePosition.X - _lastPos.X;
            int yoffset = MousePosition.Y - _lastPos.Y;
            Left += xoffset;
            Top += yoffset;
            _lastPos = MousePosition;
        }

        private void LabelTitleMouseDown(object sender, MouseEventArgs e)
        {
            _mouseDown = true;
            _lastPos = MousePosition;
        }

        private void LabelTitleMouseUp(object sender, MouseEventArgs e)
        {
            _mouseDown = false;
        }

        #endregion

        private void ButtonQuitClick(object sender, EventArgs e)
        {
            Close();
            DialogResult = DialogResult.Cancel;
        }
    }
}
