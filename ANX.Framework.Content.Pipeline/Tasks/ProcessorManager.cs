#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using ANX.Framework.Content.Pipeline.Helpers;
using ANX.Framework.NonXNA.Development;
using System.ComponentModel;

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
                if (!AssemblyHelper.IsValidForPipeline(assembly.GetName()))
                    continue;
 #if LINUX
                Console.WriteLine("ProcessorManager: Checking " + assembly.FullName);
#endif
                try
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
                catch
                {
                    Console.WriteLine(assembly.FullName);
                }
            }
        }

        
        public IContentProcessor GetInstance(string processorName)
        {
            IContentProcessor processor;
            if (!this.processors.TryGetValue(processorName, out processor))
            {
                throw new ArgumentException(String.Format("Can't find processor {0}", processorName));
            }

            return processor;
        }


        public string GetProcessorName(string displayName)
        {
            foreach (var value in this.AvailableProcessors)
            {
                var attribute = value.Value.GetType().GetCustomAttributes(typeof(ContentProcessorAttribute), true).Cast<ContentProcessorAttribute>().FirstOrDefault();
                if (attribute.DisplayName == displayName)
                {
                    return value.Key;
                }
            }

            return displayName;
        }

        public string GetProcessorDisplayName(string proccessorName)
        {
            var attribute = this.GetInstance(proccessorName).GetType().GetCustomAttributes(typeof(ContentProcessorAttribute), true).Cast<ContentProcessorAttribute>().FirstOrDefault();
            if (attribute != null && !string.IsNullOrEmpty(attribute.DisplayName))
                return attribute.DisplayName;
            else
                return proccessorName;
        }
        
        public String GetProcessorForType(Type inputType)
        {
            foreach (KeyValuePair<string, IContentProcessor> processorDescription in processors)
            {
                if (Type.Equals(processorDescription.Value.InputType, inputType))
                {
                    return processorDescription.Key;
                }
            }

            return string.Empty;
        }

        
        public String GetProcessorForType(Type inputType, Type outputType)
        {
            foreach (KeyValuePair<string, IContentProcessor> processorDescription in processors)
            {
                if (Type.Equals(processorDescription.Value.InputType, inputType) && Type.Equals(processorDescription.Value.OutputType, outputType))
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

        
        public void SetProcessorParameters(IContentProcessor processor, OpaqueDataDictionary parameters)
        {
            if (processor == null)
                throw new ArgumentNullException("processor");

            if (parameters == null)
                throw new ArgumentNullException("parameters");

            if (parameters.Count == 0)
            {
                return;
            }

            Type processorType = processor.GetType();

            foreach (var keyPair in parameters)
            {
                var property = processorType.GetProperty(keyPair.Key);
                if (property != null)
                {
                    property.SetValue(processor, keyPair.Value, null);
                }
            }
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

        
        public ProcessorParameterCollection GetProcessorParameters(string processor)
        {
            IContentProcessor processorInstance = this.GetInstance(processor);

            List<ProcessorParameter> list = new List<ProcessorParameter>();

            IEnumerable<PropertyInfo> properties = processorInstance.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.FlattenHierarchy);
            foreach (var propertyInfo in properties)
            {
                if (propertyInfo.GetSetMethod(false) != null && propertyInfo.GetIndexParameters().Length == 0 && ProcessorManager.IsBrowsable(propertyInfo))
                {
                    list.Add(new ProcessorParameter(propertyInfo.Name, propertyInfo.PropertyType)
                    {
                        DefaultValue = ProcessorManager.GetDefaultValue(propertyInfo),
                        Description = ProcessorManager.GetDescription(propertyInfo),
                        DisplayName = ProcessorManager.GetDisplayName(propertyInfo)
                    });
                }
            }
            return new ProcessorParameterCollection(list);
        }

        private static bool IsBrowsable(PropertyInfo property)
        {
            BrowsableAttribute attribute = (BrowsableAttribute)property.GetCustomAttributes(typeof(BrowsableAttribute), true).FirstOrDefault();
            return attribute == null || attribute.Browsable;
        }

        private static string GetDisplayName(PropertyInfo property)
        {
            DisplayNameAttribute attribute = (DisplayNameAttribute)property.GetCustomAttributes(typeof(DisplayNameAttribute), true).FirstOrDefault();
            if (attribute != null)
            {
                return attribute.DisplayName;
            }
            return null;
        }

        private static string GetDescription(PropertyInfo property)
        {
            DescriptionAttribute attribute = (DescriptionAttribute)property.GetCustomAttributes(typeof(DescriptionAttribute), true).FirstOrDefault();
            if (attribute != null)
            {
                return attribute.Description;
            }
            return null;
        }

        private static object GetDefaultValue(PropertyInfo property)
        {
            DefaultValueAttribute attribute = (DefaultValueAttribute)property.GetCustomAttributes(typeof(DefaultValueAttribute), true).FirstOrDefault();
            object obj = null;
            if (attribute != null)
            {
                obj = attribute.Value;
            }
            return obj;
        }

        
        public void ValidateProcessorTypes(string processorName, Type inputType, Type outputType)
        {
            if (inputType == null)
                throw new ArgumentNullException("inputType");

            if (outputType == null)
                throw new ArgumentNullException("outputType");

            var processor = this.GetInstance(processorName);
            if (!processor.InputType.IsAssignableFrom(inputType))
                throw new InvalidOperationException(string.Format("The type {0} is not compatible to the input type of the processor {1}", inputType, processor.GetType().Name));

            if (!outputType.IsAssignableFrom(processor.OutputType))
                throw new InvalidOperationException(string.Format("The type {0} is not compatible to the output type of the processor {1}", outputType, processor.GetType().Name));
        }
    }
}
