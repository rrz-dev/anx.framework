#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ANX.Framework.Content.Pipeline.Tasks;
using ANX.Framework.Content.Pipeline;
using System.IO;
using System.Reflection;

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

            buildContentTask.BuildLogger.LogMessage(String.Format("ANX.Framework {0} v{1}", Assembly.GetExecutingAssembly().GetName().Name, Assembly.GetExecutingAssembly().GetName().Version));

            foreach (string arg in args)
            {
                if (!arg.StartsWith("/") && !arg.StartsWith("-"))
                {
                    if (File.Exists(arg))
                    {
                        if (Path.GetExtension(arg) == ".cproj")
                        {
                            var contentProject = ContentProject.Load(arg);

                            buildContentTask.OutputDirectory = contentProject.OutputDirectory;
                            buildContentTask.TargetPlatform = contentProject.Platform;
                            buildContentTask.TargetProfile = contentProject.Profile;
                            buildContentTask.CompressContent = false; //TODO: make dynamic

                            foreach (var dir in contentProject.BuildItems.Select(buildItem => Path.GetDirectoryName(buildItem.OutputFilename)).Where(dir => !Directory.Exists(dir)))
                            {
                                Directory.CreateDirectory(dir);
                            }

                            itemsToBuild.AddRange(contentProject.BuildItems);
                        }
                        else
                        {
                            BuildItem buildItem = new BuildItem();
                            buildItem.ImporterName = ImporterManager.GuessImporterByFileExtension(arg);
                            //TODO: set configured processor name
                            buildItem.SourceFilename = arg;
                            buildItem.AssetName = System.IO.Path.GetFileNameWithoutExtension(arg);
                            buildItem.OutputFilename = String.Format("{0}.xnb", buildItem.AssetName);

                            itemsToBuild.Add(buildItem);
                        }
                    }
                    else
                    {
                        buildContentTask.BuildLogger.LogMessage("could not find file '{0}' to import. skipping.", arg);
                    }
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
