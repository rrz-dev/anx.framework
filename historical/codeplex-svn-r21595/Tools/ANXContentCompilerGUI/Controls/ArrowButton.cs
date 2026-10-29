#region Using Statements
using System;
using System.ComponentModel;
using System.Windows.Forms;
using ANX.Framework.NonXNA.Development;
#endregion

// This file is part of the EES Content Compiler 4,
// © 2008 - 2012 by Eagle Eye Studios.
// The EES Content Compiler 4 is released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.ContentCompiler.GUI.Controls
{
    [Developer("SilentWarrior/Eagle Eye Studios")]
    [PercentageComplete(100)]
    [TestState(TestStateAttribute.TestState.Tested)] //TODO: Fix the strange flickering with mono that makes the button unusable as seen on Linux
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
        #endregion
    }
}