using System;
using System.Windows.Forms;

namespace ANX.ContentCompiler.GUI.Dialogues
{
    public partial class NewProjectSummaryScreen : Form
    {
        public NewProjectSummaryScreen(String projectName, String projectDir, String outputDir, bool customImporters, String customImportersDir, int customImportersFound, int customProcessorsFound)
        {
            InitializeComponent();
            textBox.Text =
                "Summary for new project " + projectName + Environment.NewLine +
                "=========================================" + Environment.NewLine +
                "Name: " + projectName + Environment.NewLine + 
                "Media Directory: " + projectDir + Environment.NewLine + Environment.NewLine +
                "Output Directory: " + outputDir + Environment.NewLine + Environment.NewLine +
                "Custom Importers/Processors: " + customImporters + Environment.NewLine;
            if (customImporters)
            {
                textBox.Text +=
                    "Custom Importers/Processors Location: " + customImportersDir + Environment.NewLine + Environment.NewLine + 
                    "Importers/Processors found in given Location:" + Environment.NewLine + 
                    "Importers: " + customImportersFound + Environment.NewLine +
                    "Processors: " + customProcessorsFound;
            }

        }

        private void ButtonNextClick(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }
    }
}