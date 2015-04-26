#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using ANX.Framework.NonXNA.Reflection;
using ANX.Framework.Content.Pipeline.Helpers;
using ANX.Framework.NonXNA.Development;

#endregion

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Content.Pipeline.Tasks
{
    
    public class ImporterManager
    {
        private Dictionary<String, Type> importerTypes = new Dictionary<string,Type>();
        private Dictionary<String, String> defaultProcessor = new Dictionary<string, string>();

        
        public ImporterManager()
        {
            foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                if (!AssemblyHelper.IsValidForPipeline(assembly.GetName()))
                    continue;
#if LINUX
                Console.WriteLine("ImporterManager: Checking " + assembly.FullName);
#endif
                foreach (Type type in ANX.Framework.NonXNA.Reflection.TypeHelper.SafelyExtractTypesFrom(assembly))
                {
                    if (type == null)
                        continue;
                    ContentImporterAttribute[] value = (ContentImporterAttribute[])type.GetCustomAttributes(typeof(ContentImporterAttribute), true);
                    if (value.Length > 0)
                    {
                        importerTypes[type.Name] = type;

                        foreach (ContentImporterAttribute cia in value)
                        {
                            if (!String.IsNullOrEmpty(cia.DefaultProcessor))
                            {
                                defaultProcessor.Add(type.Name, cia.DefaultProcessor);
                            }
                        }
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


        public string GetImporterDisplayName(string importerName)
        {
            var value = this.AvailableImporters.FirstOrDefault((x) => x.Key == importerName);
            if (value.Value != null)
            {
                var attribute = value.Value.GetCustomAttributes(typeof(ContentImporterAttribute), true).Cast<ContentImporterAttribute>().FirstOrDefault();
                if (attribute != null && !string.IsNullOrEmpty(attribute.DisplayName))
                {
                    return attribute.DisplayName;
                }
            }

            return importerName;
        }


        public string GetImporterName(string displayName)
        {
            foreach (var value in this.AvailableImporters)
            {
                var attribute = value.Value.GetCustomAttributes(typeof(ContentImporterAttribute), true).Cast<ContentImporterAttribute>().FirstOrDefault();
                if (attribute != null)
                {
                    if (attribute.DisplayName == displayName)
                    {
                        return value.Key;
                    }
                }
            }

            return displayName;
        }

        
        public String GetDefaultProcessor(string importerName)
        {
            if (defaultProcessor.ContainsKey(importerName))
            {
                return defaultProcessor[importerName];
            }

            return String.Empty;
        }

        
        public String GuessImporterByFileExtension(string filename)
        {
            String extension = System.IO.Path.GetExtension(filename);

            foreach (var type in this.importerTypes.Values)
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

            return String.Empty;
        }

        
        public IEnumerable<KeyValuePair<string, Type>> AvailableImporters
        {
            get 
            {
                return importerTypes;
            }
        }
    }
}
