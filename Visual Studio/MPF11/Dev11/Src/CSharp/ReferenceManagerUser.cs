using Microsoft.VisualStudio.Shell.Interop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.VisualStudio.Project
{
    //Written by KorsarNek for ANX.Framework
#if DEV11_OR_LATER
    [ComVisible(true)]
    [Guid("DC188DAC-F250-45CB-AA77-B9E6FB1679C3")]
    public class ReferenceManagerUser : IVsReferenceManagerUser
    {
        IVsReferenceProviderContext[] contexts;
        IReferenceContainer referenceContainer;

        public ReferenceManagerUser(IVsReferenceProviderContext[] contexts, IReferenceContainer referenceContainer)
        {
            if (contexts == null)
                throw new ArgumentNullException("contexts");

            if (referenceContainer == null)
                throw new ArgumentNullException("referenceContainer");

            this.contexts = contexts;
            this.referenceContainer = referenceContainer;
        }
        
        public void ChangeReferences(uint operation, IVsReferenceProviderContext changedContext)
        {
            __VSREFERENCECHANGEOPERATION mod = (__VSREFERENCECHANGEOPERATION)operation;
            if (mod == __VSREFERENCECHANGEOPERATION.VSREFERENCECHANGEOPERATION_ADD)
            {
                foreach (IVsReference reference in changedContext.References)
                {
                    //Checks internally if duplicates would be created.
                    referenceContainer.AddReference(reference);
                }
            }
            else if (mod == __VSREFERENCECHANGEOPERATION.VSREFERENCECHANGEOPERATION_REMOVE)
            {
                foreach (IVsReference reference in changedContext.References)
                {
                    //Checks internally if duplicates would be created.
                    referenceContainer.RemoveReference(reference);
                }
            }
        }

        public Array GetProviderContexts()
        {
            return contexts;
        }
    }
#endif
}
