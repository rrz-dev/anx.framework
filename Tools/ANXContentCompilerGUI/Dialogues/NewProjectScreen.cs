using System;
using System.IO;
using System.Windows.Forms;

namespace ANX.ContentCompiler.GUI.Dialogues
{
    public partial class NewProjectScreen : Form
    {
        public NewProjectScreen()
        {
            InitializeComponent();
            textBoxLocation.Text = MainWindow.DefaultProjectPath;
        }

        private void ButtonBrowseClick(object sender, EventArgs e)
        {
            using (var dlg = new FolderBrowserDialog())
            {
                dlg.ShowNewFolderButton = true;
                dlg.Description = "Select Directory to save the uncompiled files in:";
                dlg.RootFolder = Environment.SpecialFolder.MyComputer;
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    textBoxLocation.Text = dlg.SelectedPath;
                }
            }
            DialogResult = DialogResult.None;
        }

        private void TextBoxNameTextChanged(object sender, EventArgs e)
        {
            textBoxLocation.Text = Path.Combine(MainWindow.DefaultProjectPath, textBoxName.Text);
        }

        private void ButtonNextClick(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(textBoxName.Text))
                MessageBox.Show("Give your child a name!", "Missing value", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
                DialogResult = DialogResult.OK;
        }
    }
}