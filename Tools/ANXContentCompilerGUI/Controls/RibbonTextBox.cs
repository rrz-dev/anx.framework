using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ANX.ContentCompiler.GUI.Dialogues;

namespace ANX.ContentCompiler.GUI.Controls
{
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
