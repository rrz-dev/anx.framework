using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ANX.Tools.XNBInspector
{
    public partial class InspectForm : Form
    {
        StringBuilder result = new StringBuilder();

        public InspectForm()
        {
            InitializeComponent();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                InspectFile(openFileDialog.FileName);
            }
        }

        private void InspectFile(string filePath)
        {
            result.Clear();

            using (Stream input = File.OpenRead(filePath))
            {
                richTextBox1.Text = InspectReader.TryInspectXNB(input);
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dropArea_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void dropArea_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] filePaths = (string[])(e.Data.GetData(DataFormats.FileDrop));
                foreach (string fileLoc in filePaths)
                {
                    if (System.IO.File.Exists(fileLoc))
                    {
                        InspectFile(fileLoc);
                        return;
                    }
                }
            }
        }
       
    }
}
