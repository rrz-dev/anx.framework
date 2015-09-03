#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using ANX.ContentCompiler.GUI.Dialogues;
using ANX.Framework.NonXNA.Development;
using ANX.Framework;
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
    public partial class RibbonTextBox : UserControl
    {
        #region Constructor
        public RibbonTextBox()
        {
            InitializeComponent();
        }
        #endregion

        #region Public
        public void AddMessage(string msg)
        {
            textBox.AppendText(msg + Environment.NewLine);
        }

        public void SetTextColor(System.Drawing.Color color)
        {
            textBox.SelectionStart = textBox.TextLength;
            textBox.SelectionLength = 0;
            textBox.SelectionColor = color;
        }

        public void ResetTextColor()
        {
            textBox.SelectionColor = textBox.ForeColor;
        }
        #endregion

        #region Private
        private void ButtonDownClick(object sender, EventArgs e)
        {
            using (var dlg = new ErrorLogScreen(textBox.Rtf))
            {
                dlg.ShowDialog();
            }
        }
        #endregion
    }
}