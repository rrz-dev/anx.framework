using System;
using System.Windows.Forms;

namespace ANX.ContentCompiler.GUI.States
{
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

    }
}
