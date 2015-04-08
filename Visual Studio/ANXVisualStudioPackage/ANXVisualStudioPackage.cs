using System;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Linq;
using System.ComponentModel.Design;
using Microsoft.Win32;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VisualStudio.OLE.Interop;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Project;
using System.Reflection;
using System.Collections.Generic;

namespace ANX.Framework.VisualStudio
{
    /// <summary>
    /// This is the class that implements the package exposed by this assembly.
    ///
    /// The minimum requirement for a class to be considered a valid package for Visual Studio
    /// is to implement the IVsPackage interface and register itself with the shell.
    /// This package uses the helper classes defined inside the Managed Package Framework (MPF)
    /// to do it: it derives from the Package class that provides the implementation of the 
    /// IVsPackage interface and uses the registration attributes defined in the framework to 
    /// register itself and its components with the shell.
    /// </summary>
    // This attribute tells the PkgDef creation utility (CreatePkgDef.exe) that this class is
    // a package.
    [PackageRegistration(UseManagedResourcesOnly = true)]
    // This attribute is used to register the information needed to show this package
    // in the Help/About dialog of Visual Studio.
    [InstalledProductRegistration("#110", "#112", "1.0", IconResourceID = 400)]
    [Guid(GuidList.guidANXVisualStudio2012PackagePkgString)]
    [ProvideProjectFactory(typeof(ContentProjectFactory), "ANX Project Extension", "ANX Content Project Files (*.cproj);*.cproj", "cproj", "cproj", ".\\Null", 
        LanguageVsTemplate = "CSharp", TemplateGroupIDsVsTemplate="ANX.Framework-Content", TemplateIDsVsTemplate="Microsoft.CSharp.Bitmap",
        ShowOnlySpecifiedTemplatesVsTemplate=true)]
    //[ProvideObject(typeof(ContentProjectSettingsPage))]
    [ProvideObject(typeof(ConfigurableContentProjectSettingsPage))]
    [ProvideMenuResource("AnxCommands.ctmenu", 1)]
    public sealed class ANXVisualStudioPackage : CommonProjectPackage
    {
        /// <summary>
        /// Default constructor of the package.
        /// Inside this method you can place any initialization code that does not require 
        /// any Visual Studio service because at this point the package object is created but 
        /// not sited yet inside Visual Studio environment. The place to do all the other 
        /// initialization is the Initialize method.
        /// </summary>
        public ANXVisualStudioPackage()
        {
            Debug.WriteLine(string.Format(CultureInfo.CurrentCulture, "Entering constructor for: {0}", this.ToString()));
        }



        /////////////////////////////////////////////////////////////////////////////
        // Overridden Package Implementation
        #region Package Members

        /// <summary>
        /// Initialization of the package; this method is called right after the package is sited, so this is the place
        /// where you can put all the initialization code that rely on services provided by VisualStudio.
        /// </summary>
        protected override void Initialize()
        {
            Debug.WriteLine (string.Format(CultureInfo.CurrentCulture, "Entering Initialize() of: {0}", this.ToString()));
            base.Initialize();
        }
        #endregion

        public override ProjectFactory CreateProjectFactory()
        {
            return new ContentProjectFactory(this);
        }

        public override CommonEditorFactory CreateEditorFactory()
        {
            return null;
        }

        public override uint GetIconIdForAboutBox()
        {
            throw new NotImplementedException();
        }

        public override uint GetIconIdForSplashScreen()
        {
            throw new NotImplementedException();
        }

        public override string GetProductName()
        {
            var attr = GetAssemblyAttribute<AssemblyProductAttribute>();
            if (attr != null)
                return attr.Product;
            return null;
        }

        public override string GetProductDescription()
        {
            var attr = GetAssemblyAttribute<AssemblyDescriptionAttribute>();
            if (attr != null)
                return attr.Description;
            return null;
        }

        public override string GetProductVersion()
        {
            var attr = GetAssemblyAttribute<AssemblyVersionAttribute>();
            if (attr != null)
                return attr.Version;
            return null;
        }

        private T GetAssemblyAttribute<T>()
        {
            return this.GetType().Assembly
                .GetCustomAttributes(typeof(T), false)
                .OfType<T>()
                .FirstOrDefault();
        }
    }
}
