#region Using Statements
using System;
using System.Windows.Forms;
using System.IO;
using ProjectConverter.Platforms;
using System.Collections.Generic;

#endregion

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ProjectConverter
{
	static class Program
	{
        static Converter[] converters = new Converter[] { new LinuxConverter(), new MetroConverter(), new PsVitaConverter() };

		[STAThread]
		static void Main(string[] args)
		{
            //
            // To restore "old" behaviour use:
            //   ProjectConverter /linux /psvita /windowsmetro ../../ANX.Framework.sln
			//
			// For testing only
			//args = new string[] { "/linux", "/psvita", "/windowsmetro", "../../ANX.Framework.sln" };

            List<string> switches = new List<string>();
            Dictionary<string, string> keyValueParameters = new Dictionary<string,string>();
            List<string> files = new List<string>();

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
                else
                {
                    if (File.Exists(arg))
                    {
                        files.Add(arg);
                    }
                }
            }

            foreach (string file in files)
            {
                string fileExt = Path.GetExtension(file).ToLowerInvariant();
                foreach (Converter converter in converters)
                {
                    if (switches.Contains(converter.Postfix.ToLowerInvariant()))
                    {
                        switch (fileExt)
                        {
                            case ".sln":
                                converter.ConvertAllProjects(file);
                                break;
                            case ".csproj":
                                converter.ConvertProject(file);
                                break;
                            default:
                                throw new NotImplementedException("unsupported file type '" + fileExt + "'");
                        }
                    }
                }
            }
		}
	}
}
