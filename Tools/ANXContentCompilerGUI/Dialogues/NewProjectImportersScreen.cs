using System;
using System.Windows.Forms;

namespace ANX.ContentCompiler.GUI.Dialogues
{
    public partial class NewProjectImportersScreen : Form
    {
        public NewProjectImportersScreen()
        {
            InitializeComponent();
        }

        private void ArrowButtonYesClick(object sender, EventArgs e)
        {
            labelLocation.Visible = true;
            textBoxLocation.Visible = true;
            buttonBrowse.Visible = true;
            buttonNext.Enabled = true;
        }

        private void ArrowButtonNoClick(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void ButtonBrowseClick(object sender, EventArgs e)
        {
            using (var dlg = new FolderBrowserDialog())
            {
                dlg.ShowNewFolderButton = true;
                dlg.RootFolder = Environment.SpecialFolder.MyComputer;
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    textBoxLocation.Text = dlg.SelectedPath;
                }
            }
            DialogResult = DialogResult.None;
        }

        private void ButtonNextClick(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(textBoxLocation.Text))
                MessageBox.Show("You need to specify the directory where your Importers/Processors are located in!",
                                "Missing value", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
                DialogResult = DialogResult.OK;
        }
    }
}