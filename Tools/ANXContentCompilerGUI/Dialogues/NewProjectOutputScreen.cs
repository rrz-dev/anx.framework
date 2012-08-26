using System;
using System.Windows.Forms;

namespace ANX.ContentCompiler.GUI.Dialogues
{
    public partial class NewProjectOutputScreen : Form
    {
        public NewProjectOutputScreen()
        {
            InitializeComponent();
        }

        private void ArrowButtonYesClick(object sender, EventArgs e)
        {
            labelLocation.Visible = true;
            textBoxLocation.Visible = true;
            buttonBrowse.Visible = true;
            buttonNext.Enabled = true;
            arrowButtonNo.Enabled = false;
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
    }
}