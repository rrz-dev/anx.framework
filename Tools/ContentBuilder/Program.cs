#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ANX.Framework.Content.Pipeline.Tasks;

#endregion

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ContentBuilder
{
    class Program
    {
        static void Main(string[] args)
        {
            //
            // Generate a list of items to build and set build parameters
            //
            List<BuildItem> itemsToBuild = new List<BuildItem>();

            foreach (string arg in args)
            {
                if (!arg.StartsWith("/") && !arg.StartsWith("-"))
                {
                    BuildItem buildItem = new BuildItem();
                    buildItem.BuildRequest = new BuildRequest();
                    buildItem.BuildRequest.ImporterName = ImporterManager.GuessImporterByFileExtension(arg);
                    buildItem.BuildRequest.SourceFilename = arg;
                    buildItem.BuildRequest.AssetName = System.IO.Path.GetFileNameWithoutExtension(arg);

                    itemsToBuild.Add(buildItem);
                }
            }

            //
            // Build the content
            //
            BuildContent buildContentTask = new BuildContent();
            buildContentTask.Execute(itemsToBuild);
        }
    }
}
