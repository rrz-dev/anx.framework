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


using System.Runtime.InteropServices;

using Microsoft.VisualStudio.Shell.Interop;

namespace Microsoft.VisualStudio.Project {

    [ComVisible(true)]
    public class CommonConfig : Config {
        private readonly CommonProjectNode/*!*/ _project;

        public CommonConfig(CommonProjectNode/*!*/ project, string configuration, string platform)
            : base(project, configuration, platform) {
            _project = project;
        }

        public override int DebugLaunch(uint flags) 
        {
            IProjectLauncher starter = _project.GetLauncher();

            return starter.LaunchProject(flags | (uint)VSConstants.VSStd2KCmdID.Debug, this, this._project.Package.GetOutputPane(VSConstants.SID_SVsGeneralOutputWindowPane, null));
        }
    }
}
