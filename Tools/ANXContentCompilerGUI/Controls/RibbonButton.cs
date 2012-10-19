#region Using Statements
using System;
using System.ComponentModel;
using System.Drawing;
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
    [TestState(TestStateAttribute.TestState.Tested)]
    public partial class RibbonButton : UserControl
    {
        #region Constructor
        public RibbonButton()
        {
            InitializeComponent();
        }
        #endregion

        #region Properties
        [Category("Content"), Description("Text that will be displayed on the button.")]
        public String Content
        {
            get { return labelText.Text; }
            set { labelText.Text = value; }
        }

        [Category("Design"), Description("Picture that will be the icon. (60x60)")]
        public Image Image
        {
            get { return pictureBox.Image; }
            set { pictureBox.Image = value; }
        }

        #endregion

        #region Private
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
        #endregion
    }
}