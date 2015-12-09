// Guids.cs
// MUST match guids.h
using System;

namespace ANX.Framework.VisualStudio
{
    static class GuidList
    {
        public const string guidANXVisualStudio2012PackagePkgString = "b1437ba8-a8c6-48c4-af58-47ba72b2a7c8";
        public const string guidANXVisualStudio2012PackageCmdSetString = "9db42e81-d7ba-4245-8f80-1103239437d5";
        public const string guidANXVisualStudio2012ContentProjectFactoryString = "75EFAE60-726E-430F-8661-4CF9ABD1306C";
        public const string guidANXVisualStudio2012ContentProjectString = "E5C9DDBD-6EE5-444D-A611-B998C4BE421D";

        public static readonly Guid guidANXVisualStudio2012PackageCmdSet = new Guid(guidANXVisualStudio2012PackageCmdSetString);
        public static readonly Guid guidANXVisualStudio2012ContentProject = new Guid(guidANXVisualStudio2012ContentProjectString);
        public static readonly Guid guidANXVisualStudio2012ContentProjectFactory = new Guid(guidANXVisualStudio2012ContentProjectFactoryString);
        
        public static class VsGuids
        {
            //source: http://msdn.microsoft.com/en-us/library/bb166496(v=vs.110).aspx
            public static readonly Guid guidBuildOutputWindowPane = new Guid("1BD8A850-02D1-11D1-BEE7-00A0C913D1F8");
            public static readonly Guid guidDebugOutputWindowPane = new Guid("FC076020-078A-11D1-A7DF-00A0C9110051");
        }
    };
}