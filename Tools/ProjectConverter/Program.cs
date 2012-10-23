#region Using Statements
using System;
using System.Collections.Generic;
using System.IO;
using ProjectConverter.Platforms;

#endregion

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ProjectConverter
{
	static class Program
	{
		private static readonly Converter[] Converters = new Converter[]
		{
			new LinuxConverter(),
			new MetroConverter(),
			new PsVitaConverter(),
            new AnxConverter(),
            new XnaConverter(),
		};

        private static readonly List<string> switches = new List<string>();
        private static readonly Dictionary<string, string> keyValueParameters = new Dictionary<string, string>();
        private static readonly List<string> files = new List<string>();

		[STAThread]
		static void Main(string[] args)
		{
            //
            // To restore "old" behaviour use:
            //   ProjectConverter /linux /psvita /windowsmetro ../../ANX.Framework.sln
			//

            Directory.SetCurrentDirectory(Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location));

			foreach (string arg in args)
			{
				if (arg.StartsWith("/") || arg.StartsWith("-"))
				{
                    if (arg.Contains("="))
                    {
                        string[] parts = arg.Split('=');
                        keyValueParameters[parts[0].Trim().ToLowerInvariant()] = parts[1].Trim().ToLowerInvariant();
                    }
                    else
                    {
                        switches.Add(arg.Substring(1).Trim().ToLowerInvariant());
                    }
				}
                else if (File.Exists(arg))
                {
                    files.Add(arg);
                }
			}

            foreach (string file in files)
            {
                string fileExt = Path.GetExtension(file).ToLowerInvariant();
                foreach (Converter converter in Converters)
                {
                    if (switches.Contains(converter.Name.ToLowerInvariant()))
                    {
                        switch (fileExt)
                        {
                            case ".sln":
                                converter.ConvertAllProjects(file, TryGetDestinationPath());
                                break;
                            case ".csproj":
                                converter.ConvertProject(file, TryGetDestinationPath());
                                break;
                            default:
                                throw new NotImplementedException("unsupported file type '" + fileExt + "'");
                        }
                    }
                }
            }
		}

        private static string TryGetDestinationPath()
        {
            foreach (KeyValuePair<string, string> kvp in keyValueParameters)
            {
                if (string.Equals(kvp.Key, "/O", StringComparison.InvariantCultureIgnoreCase))
                {
                    return kvp.Value;
                }
            }

            return string.Empty;
        }
	}
}
