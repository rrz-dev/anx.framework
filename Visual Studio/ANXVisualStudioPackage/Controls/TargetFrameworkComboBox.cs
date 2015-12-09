using ANX.Framework.VisualStudio.Nodes;
using EnvDTE;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.OLE.Interop;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Versioning;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ANX.Framework.VisualStudio.Controls
{
    [DefaultEvent("SelectedFrameworkChanged")]
    class TargetFrameworkComboBox : ComboBox
    {
        class InstallOtherFrameworksValue
        {
            static InstallOtherFrameworksValue()
            {
                Text = PackageResources.GetString(PackageResources.InstallOtherFrameworks);
            }

            public static readonly string Text;

            public override string ToString()
            {
                return Text;
            }
        }

        private ContentProjectNode node;

        int previousCommittedValue;

        public int IndexOfLastCommittedValue
        {
            get;
            set;
        }

        public void RevertToPreviousCommittedValue()
        {
            this.SelectedIndex = previousCommittedValue;
        }

        public event EventHandler<EventArgs> SelectedFrameworkChanged;

        public TargetFrameworkComboBox()
		{
            previousCommittedValue = -1;
            this.IndexOfLastCommittedValue = -1;
            this.SelectedIndexChanged += TargetFrameworkComboBox_SelectedIndexChanged;
		}

        public void SelectFramework(FrameworkName frameworkName)
        {
            if (frameworkName == null)
            {
                this.SelectedIndex = -1;
                return;
            }

            for (int i = 0; i < Items.Count; i++)
            {
                object item = Items[i];
                if (item is TargetFrameworkMoniker)
                {
                    var moniker = (TargetFrameworkMoniker)item;

                    if (moniker.Moniker == frameworkName.FullName)
                    {
                        this.SelectedIndex = i;
                        break;
                    }
                }
            }
        }

        public FrameworkName GetSelectedFramework()
        {
            if (this.SelectedIndex == -1 || IsInstallOtherFrameworksSelected())
                return null;

            return new FrameworkName(((TargetFrameworkMoniker)this.SelectedItem).Moniker);
        }

        protected virtual void OnSelectedFrameworkChanged(EventArgs e)
        {
            if (SelectedFrameworkChanged != null)
                SelectedFrameworkChanged(this, e);
        }

        public void Initialize(ContentProjectNode node)
        {
            this.node = node;

            Items.Clear();
            previousCommittedValue = -1;
            IndexOfLastCommittedValue = -1;
            SelectedIndex = -1;
            try
            {
                Microsoft.VisualStudio.OLE.Interop.IServiceProvider serviceInterface = null;
                ErrorHandler.ThrowOnFailure(this.node.GetSite(out serviceInterface));
                ServiceProvider serviceProvider = new ServiceProvider(serviceInterface);

                IVsFrameworkMultiTargeting vsFrameworkMultiTargeting = serviceProvider.GetService(typeof(SVsFrameworkMultiTargeting).GUID) as IVsFrameworkMultiTargeting;
                if (vsFrameworkMultiTargeting != null)
                {
                    IEnumerable<TargetFrameworkMoniker> supportedTargetFrameworkMonikers = TargetFrameworkMoniker.GetSupportedTargetFrameworkMonikers(vsFrameworkMultiTargeting, (Project)this.node.GetAutomationObject());

                    foreach (var framework in supportedTargetFrameworkMonikers)
                    {
                        this.Items.Add(framework);
                    }

                    Items.Add(new InstallOtherFrameworksValue());

                    return;
                }
            }
            catch
            {
                Items.Clear();
            }

            Enabled = false;
        }

        void TargetFrameworkComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.IsInstallOtherFrameworksSelected())
            {
                this.SelectedIndex = this.IndexOfLastCommittedValue;
                this.NavigateToInstallOtherFrameworksLink();
            }
            else if (this.SelectedIndex != this.IndexOfLastCommittedValue)
            {
                previousCommittedValue = this.IndexOfLastCommittedValue;
                this.IndexOfLastCommittedValue = this.SelectedIndex;
                OnSelectedFrameworkChanged(EventArgs.Empty);
            }
        }

		private bool IsInstallOtherFrameworksSelected()
		{
            return this.SelectedIndex >= 0 && this.Items[this.SelectedIndex] is InstallOtherFrameworksValue;
		}

		private void NavigateToInstallOtherFrameworksLink()
		{
            Microsoft.VisualStudio.OLE.Interop.IServiceProvider serviceInterface;
            ErrorHandler.ThrowOnFailure(this.node.GetSite(out serviceInterface));
            Microsoft.VisualStudio.Shell.ServiceProvider serviceProvider = new ServiceProvider(serviceInterface);

            //Doesn't work
            //var dialogService = serviceProvider.GetService(typeof(SVsFrameworkRetargetingDlg)) as IVsFrameworkRetargetingDlg;
            //ErrorHandler.ThrowOnFailure(dialogService.NavigateToFrameworkDownloadUrl());

			IVsUIShellOpenDocument vsUIShellOpenDocument = serviceProvider.GetService(typeof(SVsUIShellOpenDocument).GUID) as IVsUIShellOpenDocument;
			if (vsUIShellOpenDocument == null)
			{
				return;
			}

            ErrorHandler.Failed(vsUIShellOpenDocument.OpenStandardPreviewer(0, PackageResources.GetString(PackageResources.InstallOtherFrameworksFWLink), VSPREVIEWRESOLUTION.PR_Default, 0));
		}
    }
}
