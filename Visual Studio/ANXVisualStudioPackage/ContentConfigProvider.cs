using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VisualStudio.Project;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ANX.Framework.VisualStudio.Nodes;
using System.Runtime.InteropServices;
using System.Globalization;
using ANX.Framework.Build;
using System.IO;
using ANX.Framework.Graphics;
using ANX.Framework.Content.Pipeline.Tasks;
using ANX.Framework.Content.Pipeline;

namespace ANX.Framework.VisualStudio
{
    public class ContentConfigProvider : ConfigProvider
    {
        ContentProjectNode contentProjectNode;
        List<ContentConfig> configurationsList = new List<ContentConfig>();

        public ContentConfigProvider(ContentProjectNode manager)
            : base(manager)
        {
            this.contentProjectNode = manager;
            configurationsList.AddRange(manager.ContentProject.Configurations.Select((x) => new ContentConfig(manager, x)));
        }

        private void AddConfiguration(ContentConfig config)
        {
            this.configurationsList.Add(config);
            this.contentProjectNode.ContentProject.Configurations.Add(config.Configuration);
        }

        /// <summary>
        /// Returns one or more platform names. 
        /// </summary>
        /// <param name="celt">Specifies the requested number of platform names. If this number is unknown, celt can be zero.</param>
        /// <param name="names">On input, an allocated array to hold the number of platform names specified by celt. This parameter can also be a null reference if the celt parameter is zero. On output, names contains platform names.</param>
        /// <param name="actual">The actual number of platform names returned.</param>
        /// <returns>If the method succeeds, it returns S_OK. If it fails, it returns an error code.</returns>
        public override int GetPlatformNames(uint celt, string[] names, uint[] actual)
        {
            try
            {
                return GetPlatforms(celt, names, actual, GetPlatformNames());
            }
            catch
            {
                return VSConstants.S_FALSE;
            }
        }

        private string[] GetPlatformNames()
        {
            using (var domain = this.contentProjectNode.BuildAppDomain.Aquire())
            {
                return domain.Proxy.GetPlatformDisplayNames();
            }
        }

        /// <summary>
        /// Returns the set of platforms that are installed on the user's machine. 
        /// </summary>
        /// <param name="celt">Specifies the requested number of supported platform names. If this number is unknown, celt can be zero.</param>
        /// <param name="names">On input, an allocated array to hold the number of names specified by celt. This parameter can also be a null reference (Nothing in Visual Basic)if the celt parameter is zero. On output, names contains the names of supported platforms</param>
        /// <param name="actual">The actual number of platform names returned.</param>
        /// <returns>If the method succeeds, it returns S_OK. If it fails, it returns an error code.</returns>
        public override int GetSupportedPlatformNames(uint celt, string[] names, uint[] actual)
        {
            return GetPlatforms(celt, names, actual, contentProjectNode.ContentProject.Configurations.GetUniquePlatforms().Select((x) => x.ToString()).ToArray());
        }

        protected override Config GetProjectConfiguration(string configName, string platformName)
        {
            // if we already created it, return the cached one
            var configuration = configurationsList.FirstOrDefault((x) =>  x.ConfigName == configName && x.PlatformName == platformName);
            if (configuration == null)
            {
                configuration = (ContentConfig)this.CreateProjectConfiguration(configName, platformName);
                this.AddConfiguration(configuration);
            }

            return configuration;
        }

        protected override Config CreateProjectConfiguration(string configName, string platformName)
        {
            var config = new ContentConfig(contentProjectNode, configName, platformName);

            return config;
        }

        /// <summary>
        /// Copies an existing configuration name or creates a new one. 
        /// </summary>
        /// <param name="name">The name of the new configuration.</param>
        /// <param name="cloneName">the name of the configuration to copy, or a null reference, indicating that AddCfgsOfCfgName should create a new configuration.</param>
        /// <param name="fPrivate">Flag indicating whether or not the new configuration is private. If fPrivate is set to true, the configuration is private. If set to false, the configuration is public. This flag can be ignored.</param>
        /// <returns>If the method succeeds, it returns S_OK. If it fails, it returns an error code. </returns>
        public override int AddCfgsOfCfgName(string name, string cloneName, int fPrivate)
        {
            string outputBasePath = this.ProjectMgr.OutputBaseRelativePath;
            ConfigurationCollection configCollection = this.contentProjectNode.ContentProject.Configurations;

            if (cloneName == null)
            {
                var conf = new Configuration(name, TargetPlatform.Windows) { OutputDirectory = CommonUtils.NormalizeDirectoryPath(Path.Combine(outputBasePath, name)) };
                this.AddConfiguration(new ContentConfig(this.contentProjectNode, conf));
            }
            else
            {
                var originalConfig = configCollection.First((x) => x.Name == cloneName);
                var conf = new Configuration(name, originalConfig.Platform, originalConfig);
                this.AddConfiguration(new ContentConfig(this.contentProjectNode, conf));
            }

            this.contentProjectNode.SetProjectFileDirty(true);

            NotifyOnCfgNameAdded(name);
            return VSConstants.S_OK;
        }

        /// <summary>
        /// Deletes a specified configuration name. 
        /// </summary>
        /// <param name="name">The name of the configuration to be deleted.</param>
        /// <returns>If the method succeeds, it returns S_OK. If it fails, it returns an error code. </returns>
        public override int DeleteCfgsOfCfgName(string name)
        {
            if (this.contentProjectNode.ContentProject.Configurations.Count < 1)
                throw new InvalidOperationException("Can't delete the last configuration.");

            bool deleted = false;
            var configs = this.contentProjectNode.ContentProject.Configurations;
            for (int i = 0; i < configs.Count; i++)
            {
                if (configs[i].Name == name)
                {
                    configs.RemoveAt(i);
                    configurationsList.RemoveAt(i);
                    i--;
                    deleted = true;
                }
            }

            if (deleted)
            {
                this.contentProjectNode.SetProjectFileDirty(true);
                NotifyOnCfgNameDeleted(name);
            }

            return VSConstants.S_OK;
        }

        /// <summary>
        /// Returns the existing configurations stored in the project file.
        /// </summary>
        /// <param name="celt">Specifies the requested number of property names. If this number is unknown, celt can be zero.</param>
        /// <param name="names">On input, an allocated array to hold the number of configuration property names specified by celt. This parameter can also be a null reference if the celt parameter is zero. 
        /// On output, names contains configuration property names.</param>
        /// <param name="actual">The actual number of property names returned.</param>
        /// <returns>If the method succeeds, it returns S_OK. If it fails, it returns an error code.</returns>
        public override int GetCfgNames(uint celt, string[] names, uint[] actual)
        {
            // get's called twice, once for allocation, then for retrieval            
            int i = 0;

            string[] configList = this.contentProjectNode.ContentProject.Configurations.GetUniqueNames();

            if (names != null)
            {
                foreach (string config in configList)
                {
                    names[i++] = config;
                    if (i == celt)
                        break;
                }
            }
            else
                i = configList.Length;

            if (actual != null)
            {
                actual[0] = (uint)i;
            }

            return VSConstants.S_OK;
        }

        /// <summary>
        /// Returns the per-configuration objects for this object. 
        /// </summary>
        /// <param name="celt">Number of configuration objects to be returned or zero, indicating a request for an unknown number of objects.</param>
        /// <param name="a">On input, pointer to an interface array or a null reference. On output, this parameter points to an array of IVsCfg interfaces belonging to the requested configuration objects.</param>
        /// <param name="actual">The number of configuration objects actually returned or a null reference, if this information is not necessary.</param>
        /// <param name="flags">Flags that specify settings for project configurations, or a null reference (Nothing in Visual Basic) if no additional flag settings are required. For valid prgrFlags values, see __VSCFGFLAGS.</param>
        /// <returns>If the method succeeds, it returns S_OK. If it fails, it returns an error code.</returns>
        public override int GetCfgs(uint celt, IVsCfg[] a, uint[] actual, uint[] flags)
        {
            if (flags != null)
                flags[0] = 0;
            
            int i = 0;
            if (a != null)
            {
                foreach (var config in this.contentProjectNode.ContentProject.Configurations)
                {
                    a[i] = this.GetProjectConfiguration(config.Name, Utilities.GetDisplayName(config.Platform));

                    i++;
                    if (i == celt)
                        break;
                }
            }
            else
                i = this.contentProjectNode.ContentProject.Configurations.Count;

            if (actual != null)
                actual[0] = (uint)i;

            return VSConstants.S_OK;
        }

        /// <summary>
        /// Assigns a new name to a configuration. 
        /// </summary>
        /// <param name="old">The old name of the target configuration.</param>
        /// <param name="newname">The new name of the target configuration.</param>
        /// <returns>If the method succeeds, it returns S_OK. If it fails, it returns an error code.</returns>
        public override int RenameCfgsOfCfgName(string old, string newname)
        {
            bool renamed = false;
            var configurations = this.contentProjectNode.ContentProject.Configurations;
            for (int i = 0; i < configurations.Count; i++)
            {
                var config = configurations[i];
                if (config.Name == old)
                {
                    configurations.RemoveAt(i);
                    configurationsList.RemoveAt(i);

                    var conf = new Configuration(newname, config.Platform, config);

                    configurations.Insert(i, conf);
                    configurationsList.Insert(i, new ContentConfig(this.contentProjectNode, conf));

                    renamed = true;
                }
            }

            if (renamed)
            {
                this.contentProjectNode.SetProjectFileDirty(true);
                NotifyOnCfgNameRenamed(old, newname);
            }

            return VSConstants.S_OK;
        }

        public override IEnumerator<Config> GetEnumerator()
        {
            return this.configurationsList.GetEnumerator();
        }
    }
}
