using ANX.Framework.VisualStudio.Controls;
using ANX.Framework.VisualStudio.Nodes;
using Microsoft.VisualStudio.Project;
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
    [Guid("7C835B4E-7813-4CFF-98FB-9FB72DC634AB")]
    public sealed class ConfigurableContentProjectSettingsPage : PropertyPage, IDisposable
    {
        ConfigurablePropertyPageControl control;

        public ConfigurableContentProjectSettingsPage()
            : base()
        {
            control = new ConfigurablePropertyPageControl(this);
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
            get { return PackageResources.GetString(PackageResources.ConfigurableContentProjectSettings); }
        }

        public override void SetObjects(uint count, object[] punk)
        {
            if (punk == null || count <= 0)
                return;

            try
            {
                this.Loading = true;

                this.control.LoadSettings(punk.Cast<ContentConfig>().ToArray());
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
