using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace ANX.Framework.Content.Pipeline
{
    class AssimpImporterAttribute : ContentImporterAttribute
    {
        public AssimpImporterAttribute()
        {
            try
            {
                //AssimpDeploy.DeployLibraries();
                
                Assimp.AssimpContext context = new Assimp.AssimpContext();

                this.FileExtensions = context.GetSupportedImportFormats();
            }
            catch (Exception exc)
            {
                Trace.TraceError(exc.Message + Environment.NewLine + exc.StackTrace);
            }
        }
    }
}
