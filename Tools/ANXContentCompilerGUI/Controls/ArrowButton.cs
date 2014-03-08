#region Using Statements
using System;
using System.ComponentModel;
using System.Windows.Forms;
using ANX.Framework.NonXNA.Development;
using System.Drawing;
#endregion

// This file is part of the EES Content Compiler 4,
// © 2008 - 2012 by Eagle Eye Studios.
// The EES Content Compiler 4 is released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.ContentCompiler.GUI.Controls
{
    //Reclaimer: on systems other than windows, mono 3.x is needed to make this class not crash on creation.
    //For ubuntu, see this: http://stackoverflow.com/questions/13365158/installing-mono-3-x-3-0-x-and-or-3-2-x
    [Developer("SilentWarrior/Eagle Eye Studios")]
    [PercentageComplete(100)]
    [TestState(TestStateAttribute.TestState.Tested)]
    public partial class ArrowButton : UserControl
    {
        public ArrowButton()
        {
            InitializeComponent();
        }

        #region Properties
        [EditorBrowsable(EditorBrowsableState.Always)]
        public String Content
        {
            get { return labelText.Text; }
            set { labelText.Text = value; }
        }
        #endregion

        #region Private
        private void ArrowButtonMouseEnter(object sender, EventArgs e)
        {
            BorderStyle = BorderStyle.FixedSingle;
        }

        private void ArrowButtonMouseLeave(object sender, EventArgs e)
        {
            //Because we trigger this on multiple contained controls and we want to act as they would be one, we have to make sure that we left our main control and
            //not just the child controls. Otherwise, moving from label to pictureBox would cause the border to disappear.
            //With this solution, we also don't get any flicker on ubuntu systems, it may cause some ghosting, but otherwise it works fine.
            if (!this.ClientRectangle.Contains(this.PointToClient(Control.MousePosition)))
            {
                BorderStyle = BorderStyle.None;
            }
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
        #endregion
    }
}