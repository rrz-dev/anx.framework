#region Using Statements
using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
#endregion

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ProjectConverter.GUI
{
    public partial class MainWindow : Form
    {
        private Point _lastPos;
        private bool _mouseDown;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void ButtonConvertClick(object sender, EventArgs e)
        {
            string fileExt = Path.GetExtension(textBoxSource.Text).ToLowerInvariant();
            try
            {
                switch (comboBoxType.SelectedIndex)
                {
                    case 0: //Application
                        switch (comboBoxTarget.SelectedIndex)
                        {
                            case 0:
                                foreach (
                                    var converter in
                                        Program.Converters.Where(converter => converter.Name.Equals("xna2anx")))
                                {
                                    if (!fileExt.Equals(".sln"))
                                        converter.ConvertProject(textBoxSource.Text, textBoxDestination.Text);
                                    else
                                        converter.ConvertAllProjects(textBoxSource.Text, textBoxDestination.Text);
                                }
                                break;
                            case 1:
                                foreach (
                                    var converter in
                                        Program.Converters.Where(converter => converter.Name.Equals("anx2xna")))
                                {
                                    if (!fileExt.Equals(".sln"))
                                        converter.ConvertProject(textBoxSource.Text, textBoxDestination.Text);
                                    else
                                        converter.ConvertAllProjects(textBoxSource.Text, textBoxDestination.Text);
                                }
                                break;
                        }
                        break;
                    case 1: //Content project
                        switch (comboBoxTarget.SelectedIndex)
                        {
                            case 0:
                                foreach (
                                    var converter in
                                        Program.Converters.Where(converter => converter.Name.Equals("content2anx")))
                                {
                                    if (!fileExt.Equals(".sln"))
                                        converter.ConvertProject(textBoxSource.Text, textBoxDestination.Text);
                                    else
                                        converter.ConvertAllProjects(textBoxSource.Text, textBoxDestination.Text);
                                }
                                break;
                            case 1:
                                foreach (
                                    var converter in
                                        Program.Converters.Where(converter => converter.Name.Equals("content2xna")))
                                {
                                    if (!fileExt.Equals(".sln"))
                                        converter.ConvertAnxContentProject(textBoxSource.Text, textBoxDestination.Text);
                                    else
                                        converter.ConvertAllProjects(textBoxSource.Text, textBoxDestination.Text);
                                }
                                break;
                        }
                        break;
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message + "\n" + ex.StackTrace, "Conversion error", MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }
            MessageBox.Show("Conversion successful!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void ButtonSearchClick(object sender, EventArgs e)
        {
            using (var openFileDialog = new OpenFileDialog())
            {
                openFileDialog.CheckFileExists = true;
                openFileDialog.Filter =
                    "All supported files (*.sln, *.csproj, *.contentproj, *.cproj) | *sln; *.csproj; *.contentproj; *.cproj";
                openFileDialog.Title = "Select project to convert";
                openFileDialog.Multiselect = false;
                if (openFileDialog.ShowDialog() != DialogResult.OK) return;
                textBoxSource.Text = openFileDialog.FileName;
                string fileExt = Path.GetExtension(openFileDialog.FileName).ToLowerInvariant();
                switch (fileExt)
                {
                    case ".sln":
                    case ".csproj":
                        comboBoxType.SelectedIndex = 0;
                        comboBoxTarget.SelectedIndex = 0;
                        break;
                    case ".contentproject":
                        comboBoxType.SelectedIndex = 1;
                        comboBoxTarget.SelectedIndex = 0;
                        break;
                    case ".cproj":
                        comboBoxType.SelectedIndex = 1;
                        comboBoxTarget.SelectedIndex = 1;
                        break;
                }
                textBoxDestination.Text = Path.GetDirectoryName(openFileDialog.FileName);
            }
        }

        private void Button1Click(object sender, EventArgs e)
        {
            using (var folderDialog = new FolderBrowserDialog())
            {
                folderDialog.ShowNewFolderButton = true;
                folderDialog.Description = "Select the output folder for the converted project";
                folderDialog.SelectedPath = textBoxDestination.Text;
                if (folderDialog.ShowDialog() == DialogResult.OK)
                    textBoxDestination.Text = folderDialog.SelectedPath;
            }
        }

        private void ButtonHelpClick(object sender, EventArgs e)
        {
            Process.Start("http://anxframework.codeplex.com/wikipage?title=HowTo%2fConvertXna");
        }

        private void ButtonCloseClick(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void LabelMouseEnter(object sender, EventArgs e)
        {
            ((Label)sender).BackColor = Color.Green;
        }

        private void LabelMouseLeave(object sender, EventArgs e)
        {
            ((Label)sender).BackColor = Color.FromArgb(64, 64, 64);
        }

        #region WindowMovement
        private void Label3MouseDown(object sender, MouseEventArgs e)
        {
            _mouseDown = true;
            _lastPos = MousePosition;
        }

        private void Label3MouseUp(object sender, MouseEventArgs e)
        {
            _mouseDown = false;
        }

        private void Label3MouseMove(object sender, MouseEventArgs e)
        {
            if (!_mouseDown) return;
            int xoffset = MousePosition.X - _lastPos.X;
            int yoffset = MousePosition.Y - _lastPos.Y;
            Left += xoffset;
            Top += yoffset;
            _lastPos = MousePosition;
        }
        #endregion


    }
}
