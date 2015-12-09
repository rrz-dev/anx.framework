using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ANX.Framework.Build;
using ANX.Framework.VisualStudio.Nodes;
using Microsoft.VisualStudio.Project;
using ANX.Framework.Graphics;

namespace ANX.Framework.VisualStudio.Controls
{
    public partial class ConfigurablePropertyPageControl : UserControl
    {
        ConfigurableContentProjectSettingsPage page;
        ContentProjectNode node;
        ContentConfig[] configs;

        public ConfigurablePropertyPageControl(ConfigurableContentProjectSettingsPage page)
        {
            InitializeComponent();

            this.page = page;
        }

        public void Apply()
        {
            if (node == null)
                throw new InvalidOperationException("Call LoadSettings first before calling Apply.");

            if (page.IsDirty)
            {
                foreach (var conf in configs)
                {
                    conf.Configuration.CompressContent = this.checkBox_CompressContent.Checked;
                    conf.Configuration.OutputDirectory = this.textBox_OutputDirectory.Text;
                    conf.Configuration.Profile = (GraphicsProfile)Enum.Parse(typeof(GraphicsProfile), (string)this.comboBox_GraphicsProfile.SelectedItem);
                }

                node.SetProjectFileDirty(true);
                page.IsDirty = false;
            }
        }

        public void LoadSettings(ContentConfig[] configurations)
        {
            this.node = (ContentProjectNode)configurations[0].ProjectMgr;

            comboBox_GraphicsProfile.Items.Clear();
            try
            {
                using (var domain = node.BuildAppDomain.Aquire())
                {
                    this.comboBox_GraphicsProfile.Items.AddRange(domain.Proxy.GetGraphicsProfilesNames());
                }
            }
            catch { }

            this.comboBox_GraphicsProfile.Enabled = this.comboBox_GraphicsProfile.Items.Count > 0;

            this.configs = configurations;

            browseFolderDialog1.Site = new VisualStudioSite(node);

            //Determine the common values of the configs.
            bool compressContent = configs[0].Configuration.CompressContent;
            string outputDirectory = configs[0].Configuration.OutputDirectory;
            var graphicsProfile = configs[0].Configuration.Profile;

            foreach (var conf in configurations)
            {
                if (compressContent != conf.Configuration.CompressContent)
                    compressContent = false;

                if (outputDirectory != conf.Configuration.OutputDirectory)
                    outputDirectory = "";

                if (graphicsProfile != conf.Configuration.Profile)
                    graphicsProfile = Graphics.GraphicsProfile.HiDef;
            }

            this.checkBox_CompressContent.Checked = compressContent;
            this.textBox_OutputDirectory.Text = outputDirectory;

            if (this.comboBox_GraphicsProfile.Items.Count == 0)
            {
                this.comboBox_GraphicsProfile.SelectedIndex = -1;
            }
            else
            {
                int index = this.comboBox_GraphicsProfile.Items.IndexOf(graphicsProfile.ToString());

                this.comboBox_GraphicsProfile.SelectedIndex = index;
            }
        }

        private void button_OutputDirectory_Click(object sender, EventArgs e)
        {
            browseFolderDialog1.Title = PackageResources.GetString(PackageResources.BrowseOutputDirectory);
            browseFolderDialog1.RootFolder = node.ProjectHome;
            browseFolderDialog1.SelectedPath = textBox_OutputDirectory.Text;
            if (browseFolderDialog1.ShowDialog() == DialogResult.OK)
            {
                textBox_OutputDirectory.Text = browseFolderDialog1.SelectedPath;
            }
        }

        private void textBox_OutputDirectory_TextChanged(object sender, EventArgs e)
        {
            page.IsDirty = true;
        }

        private void comboBox_GraphicsProfile_SelectedIndexChanged(object sender, EventArgs e)
        {
            page.IsDirty = true;
        }

        private void checkBox_CompressContent_CheckedChanged(object sender, EventArgs e)
        {
            page.IsDirty = true;
        }
    }
}
