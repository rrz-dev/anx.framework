/* ****************************************************************************
 *
 * Copyright (c) Microsoft Corporation. 
 *
 * This source code is subject to terms and conditions of the Apache License, Version 2.0. A 
 * copy of the license can be found in the License.html file at the root of this distribution. If 
 * you cannot locate the Apache License, Version 2.0, please send an email to 
 * vspython@microsoft.com. By using this source code in any fashion, you are agreeing to be bound 
 * by the terms of the Apache License, Version 2.0.
 *
 * You must not remove this notice, or any other, from this software.
 *
 * ***************************************************************************/

using Microsoft.VisualStudio;
using System;

namespace Microsoft.VisualStudio.Project {
    /// <summary>
    /// Enables the Any CPU Platform form name for Dynamic Projects.
    /// Hooks language specific project config.
    /// </summary>
    public class CommonConfigProvider : ConfigProvider 
    {
        private CommonProjectNode _project;

        public CommonConfigProvider(CommonProjectNode project)
            : base(project) {
            _project = project;
        }

        #region overridden methods

        /// <summary>
        /// Returns one or more platform names. 
        /// </summary>
        /// <param name="celt">Specifies the requested number of platform names. If this number is unknown, celt can be zero.</param>
        /// <param name="names">On input, an allocated array to hold the number of platform names specified by celt. This parameter can also be a null reference if the celt parameter is zero. On output, names contains platform names.</param>
        /// <param name="actual">The actual number of platform names returned.</param>
        /// <returns>If the method succeeds, it returns S_OK. If it fails, it returns an error code.</returns>
        public override int GetPlatformNames(uint celt, string[] names, uint[] actual)
        {
            string[] platforms = this.GetPlatformsFromProject();
            return GetPlatforms(celt, names, actual, platforms);
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
            string[] platforms = this.GetSupportedPlatformsFromProject();
            return GetPlatforms(celt, names, actual, platforms);
        }

        protected override Config CreateProjectConfiguration(string configName, string platformName) {
            return _project.MakeConfiguration(configName, platformName);
        }

        #endregion

        /// <summary>
        /// Gets all the platforms defined in the project
        /// </summary>
        /// <returns>An array of platform names.</returns>
        private string[] GetPlatformsFromProject()
        {
            string[] platforms = GetPropertiesConditionedOn(ProjectFileConstants.Platform);

            if (platforms == null || platforms.Length == 0)
            {
                return new string[] { x86Platform, AnyCPUPlatform };
            }

            for (int i = 0; i < platforms.Length; i++)
            {
                platforms[i] = ConvertPlatformToVsProject(platforms[i]);
            }

            return platforms;
        }

        /// <summary>
        /// Return the supported platform names.
        /// </summary>
        /// <returns>An array of supported platform names.</returns>
        private string[] GetSupportedPlatformsFromProject()
        {
            string platforms = this.ProjectMgr.BuildProject.GetPropertyValue(ProjectFileConstants.AvailablePlatforms);

            if (platforms == null)
            {
                return new string[] { };
            }

            if (platforms.Contains(","))
            {
                return platforms.Split(',');
            }

            return new string[] { platforms };
        }

        /// <summary>
        /// Helper function to convert AnyCPU to Any CPU.
        /// </summary>
        /// <param name="oldName">The oldname.</param>
        /// <returns>The new name.</returns>
        private static string ConvertPlatformToVsProject(string oldPlatformName)
        {
            if (String.Compare(oldPlatformName, ProjectFileValues.AnyCPU, StringComparison.OrdinalIgnoreCase) == 0)
            {
                return AnyCPUPlatform;
            }

            return oldPlatformName;
        }
    }
}
