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
    public enum Severity
    {
        None,
        Success,
        Warning,
        Error
    }

    public interface IInspectLogger
    {
        void AppendFormat(Severity severity, string message, params object[] arg);
        void Append(Severity severity, string message);
        void AppendLine(Severity severity, string message);
        void AppendLine();
    }

    public partial class InspectForm : Form, IInspectLogger
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
            textBox.Text = String.Empty;

            using (Stream input = File.OpenRead(filePath))
            {
                InspectReader.TryInspectXNB(input, this);
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

        private void InspectForm_Load(object sender, EventArgs e)
        {
            this.Text += " v" + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
        }

        void AppendText(RichTextBox box, Color color, string text)
        {
            int start = box.TextLength;
            box.AppendText(text);
            int end = box.TextLength;

            // Textbox may transform chars, so (end-start) != text.Length
            box.Select(start, end - start);
            {
                box.SelectionColor = color;
                // could set box.SelectionBackColor, box.SelectionFont too.
            }
            box.SelectionLength = 0; // clear
        }

        public void AppendFormat(Severity severity, string message, params object[] arg)
        {
            Append(severity, String.Format(message, arg));
        }

        public void Append(Severity severity, string message)
        {
            Color color = Color.Black;
            switch (severity)
            {
                case Severity.Success:
                    color = Color.Green;
                    break;
                case Severity.Warning:
                    color = Color.Orange;
                    break;
                case Severity.Error:
                    color = Color.Red;
                    break;
                case Severity.None:
                default:
                    break;
            }

            int start = textBox.TextLength;
            textBox.AppendText(message);
            int end = textBox.TextLength;

            textBox.Select(start, end - start);
            {
                textBox.SelectionColor = color;
            }
            textBox.SelectionLength = 0;
        }

        public void AppendLine(Severity severity, string message)
        {
            Append(severity, message + "\n");
        }

        public void AppendLine()
        {
            Append(Severity.None, "\n");
        }
    }
}
