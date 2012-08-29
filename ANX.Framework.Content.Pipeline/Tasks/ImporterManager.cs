#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using ANX.Framework.NonXNA.Reflection;

#endregion

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Content.Pipeline.Tasks
{
    public class ImporterManager
    {
        private Dictionary<String, Type> importerTypes = new Dictionary<string,Type>();

        public ImporterManager()
        {
            foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
            {

				foreach (Type type in TypeHelper.SafelyExtractTypesFrom(assembly))
                {
                    ContentImporterAttribute[] value = (ContentImporterAttribute[])type.GetCustomAttributes(typeof(ContentImporterAttribute), true);
                    if (value.Length > 0)
                    {
                        importerTypes[type.Name] = type;
                    }
                }
            }
        }

        public IContentImporter GetInstance(string importerName)
        {
            Type type;
            if (!this.importerTypes.TryGetValue(importerName, out type))
            {
                throw new Exception(String.Format("can't find importer {0}", importerName));
            }
            return (IContentImporter)Activator.CreateInstance(type);
        }

        public static String GuessImporterByFileExtension(string filename)
        {
            String extension = System.IO.Path.GetExtension(filename);

            foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                foreach (Type type in assembly.GetTypes())
                {
                    ContentImporterAttribute[] value = (ContentImporterAttribute[])type.GetCustomAttributes(typeof(ContentImporterAttribute), true);
                    foreach (ContentImporterAttribute cia in value)
                    {
                        foreach (string fe in cia.FileExtensions)
                        {
                            if (string.Equals(fe, extension, StringComparison.InvariantCultureIgnoreCase))
                            {
                                return type.Name;
                            }
                        }
                    }
                }
            }

            return String.Empty;
        }
    }
}
