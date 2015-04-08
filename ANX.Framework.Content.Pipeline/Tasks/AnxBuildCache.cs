using ANX.Framework.Content.Pipeline.Serialization.Compiler;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using ANX.Framework.Content.Pipeline.Serialization;
using System.Diagnostics;
using System.Globalization;

namespace ANX.Framework.Content.Pipeline.Tasks
{
    public class AnxBuildCache : IBuildCache
    {
        Dictionary<Uri, CacheData> cacheData = new Dictionary<Uri, CacheData>();
        ImporterManager importerManager;
        ProcessorManager processorManager;
        ContentCompiler contentCompiler;

        public AnxBuildCache(ImporterManager importerManager, ProcessorManager processorManager, ContentCompiler contentCompiler)
        {
            if (importerManager == null)
                throw new ArgumentNullException("importerManager");

            if (processorManager == null)
                throw new ArgumentNullException("processorManager");

            if (contentCompiler == null)
                throw new ArgumentNullException("contentCompiler");

            this.importerManager = importerManager;
            this.processorManager = processorManager;
            this.contentCompiler = contentCompiler;
        }

        public void LoadCache(Uri cacheFilePath)
        {
            if (cacheFilePath == null)
                throw new ArgumentNullException("cacheFilePath");

            if (!cacheFilePath.IsAbsoluteUri)
                throw new ArgumentException("cacheFilePath must be absolute.");

            if (!File.Exists(cacheFilePath.LocalPath))
                throw new FileNotFoundException("cache file doesn't exist.", cacheFilePath.LocalPath);

            using (XmlReader reader = XmlReader.Create(cacheFilePath.LocalPath))
            {
                XmlDocument document = new XmlDocument();
                document.Load(reader);

                //skip document root.
                foreach (var assetNode in document.FirstChild.NextSibling.ChildNodes.GetAsEnumerable())
                {
                    var data = new CacheData();

                    var uri = new Uri(assetNode.Attributes["Uri"].Value, UriKind.RelativeOrAbsolute);

                    foreach (var assetContentNode in assetNode.ChildNodes.GetAsEnumerable())
                    {
                        string name = assetContentNode.Name;

                        switch (name)
                        {
                            case "SourceWriteTime":
                                data.sourceWriteTime = new DateTime(long.Parse(assetContentNode.InnerText, CultureInfo.InvariantCulture), DateTimeKind.Utc);
                                break;
                            case "OutputWriteTime":
                                data.outputWriteTime = new DateTime(long.Parse(assetContentNode.InnerText, CultureInfo.InvariantCulture), DateTimeKind.Utc);
                                break;
                            case "ImporterType":
                                data.importerType = Type.GetType(assetContentNode.InnerText, false);
                                break;
                            case "ImporterAssemblyWriteTime":
                                data.importerAssemblyWriteTime = new DateTime(long.Parse(assetContentNode.InnerText, CultureInfo.InvariantCulture), DateTimeKind.Utc);
                                break;
                            case "ProcessorType":
                                data.processorType = Type.GetType(assetContentNode.InnerText, false);
                                break;
                            case "ProcessorAssemblyWriteTime":
                                data.processorAssemblyWriteTime = new DateTime(long.Parse(assetContentNode.InnerText, CultureInfo.InvariantCulture), DateTimeKind.Utc);
                                break;
                            case "CompilerType":
                                data.compilerType = Type.GetType(assetContentNode.InnerText, false);
                                break;
                            case "CompilerAssemblyWriteTime":
                                data.compilerAssemblyWriteTime = new DateTime(long.Parse(assetContentNode.InnerText, CultureInfo.InvariantCulture), DateTimeKind.Utc);
                                break;
                            case "ProcessorParameters":
                                foreach (var paramNode in assetContentNode.ChildNodes.GetAsEnumerable())
                                {
                                    string paramName = paramNode.Attributes["Key"].Value;
                                    object paramValue = paramNode.InnerText;

                                    data.processorParameters.Add(paramName, paramValue);
                                }
                                break;
                        }
                    }

                    this.cacheData[uri] = data;
                }
            }
        }

        public void SaveCache(Uri cacheFilePath)
        {
            if (cacheFilePath == null)
                throw new ArgumentNullException("cacheFilePath");
            
            if (!cacheFilePath.IsAbsoluteUri)
                throw new ArgumentException("cacheFilePath must be absolute.");

            using (XmlWriter writer = XmlWriter.Create(cacheFilePath.LocalPath, new XmlWriterSettings() { Indent = true }))
            {
                writer.WriteStartDocument();

                writer.WriteStartElement("CacheData");

                foreach (var pair in this.cacheData)
                {
                    var data = pair.Value;

                    writer.WriteStartElement("Asset");
                    writer.WriteAttributeString("Uri", pair.Key.OriginalString);

                    writer.WriteElementString("SourceWriteTime", data.sourceWriteTime.Ticks.ToString());
                    writer.WriteElementString("OutputWriteTime", data.outputWriteTime.Ticks.ToString());
                    if (data.importerType != null)
                    {
                        writer.WriteElementString("ImporterType", data.importerType.AssemblyQualifiedName);
                        //DateTimes are structs, so ne need to check for null.
                        writer.WriteElementString("ImporterAssemblyWriteTime", data.importerAssemblyWriteTime.Ticks.ToString(CultureInfo.InvariantCulture));
                    }

                    if (data.processorType != null)
                    {
                        writer.WriteElementString("ProcessorType", data.processorType.AssemblyQualifiedName);
                        writer.WriteElementString("ProcessorAssemblyWriteTime", data.processorAssemblyWriteTime.Ticks.ToString(CultureInfo.InvariantCulture));
                    }

                    if (data.compilerType != null)
                    {
                        writer.WriteElementString("CompilerType", data.compilerType.AssemblyQualifiedName);
                        writer.WriteElementString("CompilerAssemblyWriteTime", data.compilerAssemblyWriteTime.Ticks.ToString(CultureInfo.InvariantCulture));
                    }

                    writer.WriteStartElement("ProcessorParameters");

                    foreach (var parameterPair in data.processorParameters)
                    {
                        writer.WriteStartElement("Param");
                        writer.WriteAttributeString("Key", parameterPair.Key);

                        try
                        {
                            writer.WriteValue(parameterPair.Value);
                        }
                        catch (Exception exc)
                        {
                            Trace.WriteLine(string.Format("Error while writing build.cache at processor parameter \"{0}\" for asset \"{1}\": {2}", parameterPair.Key, pair.Key.OriginalString, exc.Message));
                        }

                        writer.WriteEndElement();
                    }

                    writer.WriteEndElement();

                    writer.WriteEndElement();
                }

                writer.WriteEndElement();

                writer.Flush();
            }
        }

        public bool IsValid(BuildItem buildItem, Uri outputFilePath)
        {
            if (buildItem == null)
                throw new ArgumentNullException("buildItem");

            if (outputFilePath == null)
                throw new ArgumentNullException("outputFilePath");

            if (!File.Exists(GetAbsoluteFileName(buildItem.SourceFilename)) || !File.Exists(GetAbsoluteFileName(outputFilePath).LocalPath))
                return false;

            Uri filePath = new Uri(buildItem.SourceFilename, UriKind.RelativeOrAbsolute);

            CacheData data;
            if (cacheData.TryGetValue(filePath, out data))
            {
                if (File.GetLastWriteTimeUtc(filePath.OriginalString) != data.sourceWriteTime)
                    return false;

                if (File.GetLastWriteTimeUtc(outputFilePath.OriginalString) != data.outputWriteTime)
                    return false;
                
                Type importerType = importerManager.AvailableImporters.FirstOrDefault((x) => x.Key == buildItem.ImporterName).Value;
                //The visual studio extension tries to set the importer types whenever it can, so the case when the importer is null should be rather rare and 
                //the extension should immediatly try to save the importer, same for the processor.
                //For additional meta data, we check the write time of the assemblies that contain the used importer and processor. When a dependant assembly changes,
                //the referencing assembly will have to relink and therefore the write time should change.
                if (importerType == null || data.importerType != importerType)
                    return false;
                else
                {
                    if (!string.IsNullOrEmpty(importerType.Assembly.Location))
                        if (File.GetLastWriteTimeUtc(importerType.Assembly.Location) != data.importerAssemblyWriteTime)
                            return false;
                }

                var processor = processorManager.AvailableProcessors.FirstOrDefault((x) => x.Key == buildItem.ProcessorName).Value;
                if (processor == null)
                    return false;
                else
                {
                    if (data.processorType != processor.GetType())
                        return false;

                    if (!string.IsNullOrEmpty(processor.GetType().Assembly.Location))
                        if (File.GetLastWriteTimeUtc(processor.GetType().Assembly.Location) != data.processorAssemblyWriteTime)
                            return false;

                    var compiler = this.contentCompiler.GetTypeWriter(processor.OutputType);
                    if (compiler == null)
                        return false;

                    if (compiler.GetType() != data.compilerType)
                        return false;

                    if (!string.IsNullOrEmpty(compiler.GetType().Assembly.Location))
                        if (File.GetLastWriteTimeUtc(compiler.GetType().Assembly.Location) != data.compilerAssemblyWriteTime)
                            return false;
                }

                if (buildItem.ProcessorParameters.Count != data.processorParameters.Count)
                    return false;

                string[] keys = buildItem.ProcessorParameters.Keys.ToArray();
                for (int i = 0; i < keys.Length; i++)
                {
                    string key = keys[i];
                    if (!data.processorParameters[key].Equals(buildItem.ProcessorParameters[key]))
                        return false;
                }

                return true;
            }
            return false;
        }

        public void Refresh(BuildItem buildItem, Uri outputFilePath)
        {
            if (buildItem == null)
                throw new ArgumentNullException("buildItem");

            if (!File.Exists(GetAbsoluteFileName(buildItem.SourceFilename)))
                throw new ArgumentException(string.Format("The file \"{0}\" does not exist.", buildItem.SourceFilename));

            CacheData data = new CacheData();

            if (File.Exists(GetAbsoluteFileName(outputFilePath.OriginalString)))
                data.outputWriteTime = File.GetLastWriteTimeUtc(GetAbsoluteFileName(outputFilePath.OriginalString));

            data.sourceWriteTime = File.GetLastWriteTimeUtc(buildItem.SourceFilename);

            var importerType = importerManager.AvailableImporters.FirstOrDefault((x) => x.Key == buildItem.ImporterName).Value;
            data.importerType = importerType;
            if (importerType != null && File.Exists(importerType.Assembly.Location))
                data.importerAssemblyWriteTime = File.GetLastWriteTimeUtc(importerType.Assembly.Location);

            var processor = processorManager.AvailableProcessors.FirstOrDefault((x) => x.Key == buildItem.ProcessorName).Value;
            if (processor == null)
                data.processorType = null;
            else
            {
                data.processorType = processor.GetType();
                if (File.Exists(processor.GetType().Assembly.Location))
                    data.processorAssemblyWriteTime = File.GetLastWriteTimeUtc(processor.GetType().Assembly.Location);

                var typeWriter = contentCompiler.GetTypeWriter(processor.OutputType);
                if (typeWriter == null)
                    data.compilerType = null;
                else
                {
                    data.compilerType = typeWriter.GetType();

                    if (File.Exists(typeWriter.GetType().Assembly.Location))
                        data.compilerAssemblyWriteTime = File.GetLastWriteTimeUtc(typeWriter.GetType().Assembly.Location);
                }
            }

            data.processorParameters.AddRange(buildItem.ProcessorParameters);

            this.cacheData[new Uri(buildItem.SourceFilename, UriKind.RelativeOrAbsolute)] = data;
        }

        public void Clear()
        {
            cacheData.Clear();
        }

        public void Remove(Uri filePath)
        {
            cacheData.Remove(filePath);
        }

        public Uri ProjectHome
        {
            get;
            set;
        }

        private string GetAbsoluteFileName(string fileName)
        {
            if (Path.IsPathRooted(fileName))
                return fileName;

            var result = new Uri(GetProjectHome(), new Uri(fileName, UriKind.Relative)).LocalPath;
            return result;
        }

        private Uri GetAbsoluteFileName(Uri fileName)
        {
            if (fileName == null)
                throw new ArgumentNullException("fileName");

            if (fileName.IsAbsoluteUri)
                return fileName;

            var result = new Uri(GetProjectHome(), fileName);
            return result;
        }

        private Uri GetProjectHome()
        {
            if (ProjectHome != null)
                return ProjectHome;

            return new Uri(Environment.CurrentDirectory + "\\", UriKind.Absolute);
        }

        class CacheData
        {
            public DateTime outputWriteTime;
            public DateTime sourceWriteTime;
            public Type importerType;
            public DateTime importerAssemblyWriteTime;
            public Type processorType;
            public DateTime processorAssemblyWriteTime;
            public Type compilerType;
            public DateTime compilerAssemblyWriteTime;
            public OpaqueDataDictionary processorParameters = new OpaqueDataDictionary();
        }
    }
}
