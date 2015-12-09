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
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using System.Runtime.Versioning;
using System.Runtime.InteropServices;
using Microsoft.VisualStudio.Project;
using ANX.Framework.VisualStudio.Controls;
using ANX.Framework.VisualStudio.Nodes;

namespace ANX.Framework.VisualStudio.Controls
{
    public partial class PropertyPageControl : UserControl
    {
        ContentProjectSettingsPage page;
        ContentProjectNode node;

        public PropertyPageControl(ContentProjectSettingsPage page)
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
                node.SetProjectFileDirty(true);
                page.IsDirty = false;
            }
        }

        public void LoadSettings(ContentProjectNodeProperties properties)
        {
            this.node = (ContentProjectNode)properties.Node;

            //targetFrameworkComboBox1.Initialize(node);
            //targetFrameworkComboBox1.SelectFramework(node.ContentProject.DotNetFramework);

            sdkComboBox1.Initialize(node);
            //this.targetFrameworkComboBox1.SelectFramework(node.TargetFrameworkMoniker);
        }

        /*private void targetFrameworkComboBox1_SelectedFrameworkChanged(object sender, EventArgs e)
        {
            if (!page.Loading)
            {
                FrameworkName newFramework = this.targetFrameworkComboBox1.GetSelectedFramework();
                FrameworkName oldFramework = this.node.TargetFrameworkMoniker;

                if (oldFramework != newFramework)
                {
                    if (MessageBox.Show(SR.GetString(SR.ReloadPromptOnTargetFxChanged), SR.GetString(SR.ReloadPromptOnTargetFxChangedCaption), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        if (!node.PerformTargetFrameworkCheck(newFramework.FullName))
                            return;

                        page.IsDirty = true;

                        Apply();

                        IVsPropertyPageFrame propertyPageFrame = (IVsPropertyPageFrame)this.node.Site.GetService((typeof(SVsPropertyPageFrame)));

                        //Hide the unloaded project, if we would keep using it, we would get an AccessViolationException.
                        propertyPageFrame.HideFrame();
                        node.OnTargetFrameworkMonikerChanged(oldFramework, newFramework);
                    }
                    else
                    {
                        this.targetFrameworkComboBox1.RevertToPreviousCommittedValue();
                    }
                }
            }
        }*/

        private void sdkComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            /*var box = (ComboBox)sender;
            if (box.SelectedIndex == 0)
            {
                targetFrameworkComboBox1.Enabled = true;
            }
            else
            {
                targetFrameworkComboBox1.Enabled = false;
            }*/
        }
    }
}
