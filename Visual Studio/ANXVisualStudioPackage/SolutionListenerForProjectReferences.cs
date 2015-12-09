using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VisualStudio.Project;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ANX.Framework.VisualStudio
{
    class SolutionListenerForProjectReferences : SolutionListener
    {
        IReferenceContainer referenceContainer;

        public SolutionListenerForProjectReferences(IServiceProvider serviceProvider, IReferenceContainer referenceContainer)
            : base(serviceProvider)
        {
            this.referenceContainer = referenceContainer;
        }

        public override int OnAfterOpenProject(IVsHierarchy hierarchy, int added)
        {
            Guid projectGuid;
            if (hierarchy.GetGuidProperty(VSConstants.VSITEMID_ROOT, (int)__VSHPROPID.VSHPROPID_ProjectIDGuid, out projectGuid) == VSConstants.S_OK)
            {
                foreach (var reference in referenceContainer.EnumReferences())
                {
                    if (reference is ProjectReferenceNode)
                    {
                        var projectReference = (ProjectReferenceNode)reference;
                        if (projectReference.ReferencedProjectGuid == projectGuid)
                        {
                            projectReference.RefreshReference();
                        }
                    }
                }
            }
            return VSConstants.S_OK;
        }
    }
}
