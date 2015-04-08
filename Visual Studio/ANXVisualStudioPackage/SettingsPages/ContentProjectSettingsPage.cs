using ANX.Framework.VisualStudio.Controls;
using ANX.Framework.VisualStudio.Nodes;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Project;
using Microsoft.VisualStudio.Shell.Interop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ANX.Framework.VisualStudio
{
    [ComVisible(true)]
    [Guid("2B88FCF6-A6EC-4AF3-95CA-06724A337438")]
    public sealed class ContentProjectSettingsPage : PropertyPage, IDisposable
    {
        PropertyPageControl control;

        public ContentProjectSettingsPage()
            : base()
        {
            control = new PropertyPageControl(this);
        }

        public override Control Control
        {
            get { return control; }
        }

        public override void Apply()
        {
            this.control.Apply();
        }

        public override string Name
        {
            get { return PackageResources.GetString(PackageResources.ContentProjectSettings); }
        }

        public override void SetObjects(uint count, object[] punk)
        {
            if (punk == null || count <= 0)
                return;

            try
            {
                this.Loading = true;

                this.control.LoadSettings(punk[0] as ContentProjectNodeProperties);
            }
            finally
            {
                this.Loading = false;
            }
        }

        public void Dispose()
        {
            control.Dispose();
        }
    }
}
