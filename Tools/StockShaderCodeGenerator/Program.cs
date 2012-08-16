#region Private Members
using System;
using System.Reflection;

#endregion // Private Members

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace StockShaderCodeGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("ANX.Framework StockShaderCodeGenerator (sscg) Version " + Assembly.GetExecutingAssembly().GetName().Version);

            string buildFile;

            if (args.Length < 1)
            {
                Console.WriteLine("No command line arguments provided. Trying to load build.xml from current directory.");

								buildFile = "build.xml";
            }
            else
            {
                buildFile = args[0];
            }

            Console.WriteLine("Creating configuration using '{0}' configuration file.", buildFile);

            Configuration.LoadConfiguration(buildFile);

            if (Configuration.ConfigurationValid)
            {
                if (Compiler.GenerateShaders())
                {
                    CodeGenerator.Generate();
                }
                else
                {
                    Console.WriteLine("error while compiling shaders. Code generation skipped...");
                }
            }

//#if DEBUG
//            Console.WriteLine("Press enter to exit.");
//            Console.ReadLine();
//#endif
        }
    }
}
