using Microsoft.VisualStudio.Project;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ANX.Framework.VisualStudio.Nodes
{
    [ClassInterface(ClassInterfaceType.AutoDual)]
    [ComVisible(true)]
    public class ContentProjectNodeProperties : CommonProjectNodeProperties
    {
        private ContentProjectNode projectNode;

        public ContentProjectNodeProperties(ContentProjectNode node)
            : base(node)
        {
            this.projectNode = node;
        }

        [Browsable(false)]
        [DispId(10076)]
        public string PreBuildEvent
        {
            get { return null; }
            set { }
        }

        [Browsable(false)]
        [DispId(10077)]
        public string PostBuildEvent
        {
            get { return null; }
            set { }
        }

        [Browsable(false)]
        [DispId(10078)]
        public string RunPostBuildEvent
        {
            get { return null; }
            set { }
        }

        [Browsable(false)]
        public object CustomTool
        {
            get { return null; }
            set { }
        }

        [Browsable(false)]
        public uint OutputTypeEx
        {
            get { return 0; }
            set { }
        }
    }
}
