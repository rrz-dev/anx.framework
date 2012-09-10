using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using ANX.ContentCompiler.GUI.Dialogues;
using ANX.Framework.NonXNA.Development;

namespace ANX.ContentCompiler.GUI.Controls
{
    [Developer("SilentWarrior/Eagle Eye Studios")]
    [PercentageComplete(100)]
    [TestState(TestStateAttribute.TestState.Tested)]
    public partial class RibbonTextBox : UserControl
    {
        public RibbonTextBox()
        {
            InitializeComponent();
        }

        public void AddMessage(string msg)
        {
            List<string> contents = textBox.Lines.ToList();
            contents.Add(msg);
            textBox.Lines = contents.ToArray();
        }

        private void ButtonDownClick(object sender, EventArgs e)
        {
            using (var dlg = new ErrorLogScreen(textBox.Lines))
            {
                dlg.ShowDialog();
            }
        }
    }
}