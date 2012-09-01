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
            textBoxLocation.Text = Settings.DefaultProjectPath;
            SetUpColors();
        }

        private void SetUpColors()
        {
            BackColor = Settings.MainColor;
            ForeColor = Settings.ForeColor;
            button3.FlatAppearance.MouseOverBackColor = Settings.LightMainColor;
            buttonBrowse.FlatAppearance.MouseOverBackColor = Settings.LightMainColor;
            buttonCancel.FlatAppearance.MouseOverBackColor = Settings.LightMainColor;
            buttonNext.FlatAppearance.MouseOverBackColor = Settings.LightMainColor;
            button3.FlatAppearance.MouseDownBackColor = Settings.AccentColor3;
            buttonBrowse.FlatAppearance.MouseDownBackColor = Settings.AccentColor3;
            buttonCancel.FlatAppearance.MouseDownBackColor = Settings.AccentColor3;
            buttonNext.FlatAppearance.MouseDownBackColor = Settings.AccentColor3;
            buttonNext.FlatAppearance.BorderColor = Settings.LightMainColor;
            button3.FlatAppearance.BorderColor = Settings.LightMainColor;
            buttonBrowse.FlatAppearance.BorderColor = Settings.LightMainColor;
            buttonCancel.FlatAppearance.BorderColor = Settings.LightMainColor;
            textBoxName.BackColor = Settings.DarkMainColor;
            textBoxLocation.BackColor = Settings.DarkMainColor;
            textBoxName.ForeColor = Settings.ForeColor;
            textBoxLocation.ForeColor = Settings.ForeColor;
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
            textBoxLocation.Text = Path.Combine(Settings.DefaultProjectPath, textBoxName.Text);
        }

        private void ButtonNextClick(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(textBoxName.Text))
                MessageBox.Show("Give your child a name!", "Missing value", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else if (Directory.Exists(textBoxLocation.Text))
                MessageBox.Show("A project with this name already exists in that path!", "Will not overwrite existing stuff", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
                DialogResult = DialogResult.OK;
        }
    }
}