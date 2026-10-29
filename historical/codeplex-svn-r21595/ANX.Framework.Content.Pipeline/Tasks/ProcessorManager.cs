#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

#endregion

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Content.Pipeline.Tasks
{
    public class ProcessorManager
    {
        private Dictionary<string, IContentProcessor> processors = new Dictionary<string, IContentProcessor>();

        public ProcessorManager()
        {
            foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                foreach (Type type in assembly.GetTypes())
                {
                    ContentProcessorAttribute[] value = (ContentProcessorAttribute[]) type.GetCustomAttributes(typeof(ContentProcessorAttribute), true);
                    if (value.Length > 0 && !processors.ContainsKey(type.Name))
                    {
                        processors[type.Name] = (IContentProcessor)Activator.CreateInstance(type);
                    }
                }
            }
        }

        public IContentProcessor GetInstance(string processorName)
        {
            IContentProcessor processor;
            if (!this.processors.TryGetValue(processorName, out processor))
            {
                throw new Exception(String.Format("can't find processor {0}", processorName));
            }

            return processor;
        }

        public String GetProcessorForType(Type type)
        {
            foreach (KeyValuePair<string, IContentProcessor> processorDescription in processors)
            {
                if (Type.Equals(processorDescription.Value.InputType, type))
                {
                    return processorDescription.Key;
                }
            }

            return string.Empty;
        }

        public string GetProcessorForImporter(IContentImporter contentImporter)
        {
            return GetProcessorForType(contentImporter.OutputType);
        }

        public IEnumerable<KeyValuePair<string, IContentProcessor>> AvailableProcessors
        {
            get
            {
                foreach (KeyValuePair<string, IContentProcessor> kvp in processors)
                {
                    yield return kvp;
                }
            }
        }
    }
}
