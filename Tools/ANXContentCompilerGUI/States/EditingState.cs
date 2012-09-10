using System;
using System.Windows.Forms;
using ANX.ContentCompiler.GUI.Dialogues;
using ANX.Framework.NonXNA.Development;

namespace ANX.ContentCompiler.GUI.States
{
    [Developer("SilentWarrior/Eagle Eye Studios")]
    [PercentageComplete(90)] //TODO: Add click event to open preview window!
    [TestState(TestStateAttribute.TestState.Tested)]
    public partial class EditingState : UserControl
    {
        public EditingState()
        {
            InitializeComponent();
        }

        private void EditingStateLoad(object sender, EventArgs e)
        {
            ForeColor = Settings.ForeColor;
            BackColor = Settings.MainColor;
        }

        private void ArrowButtonAddFilesClick(object sender, EventArgs e)
        {
            using (var dlg = new OpenFileDialog())
            {
                dlg.Multiselect = true;
                dlg.Title = "Add files";
                if (dlg.ShowDialog() == DialogResult.OK)
                    MainWindow.Instance.AddFiles(dlg.FileNames);
            }
        }

        private void ArrowButtonCreateFolderClick(object sender, EventArgs e)
        {
            using (var dlg = new NewFolderScreen())
            {
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    MainWindow.Instance.AddFolder(dlg.textBoxName.Text);
                }
            }
        }

        private void ArrowButtonBuildClick(object sender, EventArgs e)
        {
            MainWindow.Instance.BuildProject(sender, e);
        }
    }
}