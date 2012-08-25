#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ANX.Framework.Content.Pipeline.Tasks;
using ANX.Framework.Content.Pipeline;

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
            BuildContent buildContentTask = new BuildContent();
            buildContentTask.BuildLogger = new ConsoleLogger();

            foreach (string arg in args)
            {
                if (!arg.StartsWith("/") && !arg.StartsWith("-"))
                {
                    BuildItem buildItem = new BuildItem();
                    buildItem.BuildRequest = new BuildRequest();
                    buildItem.BuildRequest.ImporterName = ImporterManager.GuessImporterByFileExtension(arg);
                    //TODO: set configured processor name
                    buildItem.BuildRequest.SourceFilename = arg;
                    buildItem.BuildRequest.AssetName = System.IO.Path.GetFileNameWithoutExtension(arg);
                    buildItem.OutputFilename = String.Format("{0}.xnb", buildItem.BuildRequest.AssetName);

                    itemsToBuild.Add(buildItem);
                }
                else
                {
                    // parse argument
                    string parameterChar1 = arg.Substring(1,1).ToLowerInvariant();
                    string parameterChar2 = arg.Substring(2,1).ToLowerInvariant();

                    if (parameterChar1 == "o" && parameterChar2 == "d")
                    {
                        // output dir
                        buildContentTask.OutputDirectory = arg.Substring(3);
                    }
                    else if (parameterChar1 == "t")
                    {
                        string[] argParts = arg.Split(new char[] { '=', ':' }, StringSplitOptions.RemoveEmptyEntries);
                        TargetPlatform targetPlatform = TargetPlatform.Windows;
                        if (Enum.TryParse<TargetPlatform>(argParts[1], true, out targetPlatform))
                        {
                            buildContentTask.TargetPlatform = targetPlatform;
                        } else {
                            throw new InvalidOperationException("couldn't set target platform '" + argParts[1] + "'");
                        }
                    }
                }
            }

            //
            // Build the content
            //
            buildContentTask.Execute(itemsToBuild);
        }
    }
}
