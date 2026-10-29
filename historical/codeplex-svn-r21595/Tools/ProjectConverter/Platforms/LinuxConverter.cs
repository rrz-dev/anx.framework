using System;
using System.IO;
using System.Linq;
using System.Xml.Linq;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ProjectConverter.Platforms
{
	public class LinuxConverter : Converter
	{
		public override string Postfix
		{
			get { return "Linux"; }
		}

        public override string Name
        {
            get { return "linux"; }
        }

		#region ConvertImport
		protected override void ConvertImport(XElement element, XAttribute projectAttribute)
		{
			if (projectAttribute != null &&
				(projectAttribute.Value.EndsWith(XnaGameStudioTarget) ||
				projectAttribute.Value.EndsWith(XnaPipelineExtensionTarget)))
			{
				element.Remove();
			}
		}
		#endregion

        private string[] referencesToRemove = new string[] { "ANX.RenderSystem.Windows.DX10",
                                                             "ANX.RenderSystem.Windows.DX11",
                                                             "XInput",
                                                             "XAudio",
                                                             "SharpDX",
                                                             "ANX.PlatformSystem.Windows",
                                                           };

        #region ConvertReference
        protected override void ConvertReference(XElement element)
        {
            XAttribute includeAttribute = element.Attribute("Include");
            if (includeAttribute != null)
            {
                string value = includeAttribute.Value;
                foreach (var reference in referencesToRemove)
                {
                    if (value.ToLowerInvariant().Contains(reference.ToLowerInvariant()))
                    {
                        element.Remove();
                        break;
                    }
                }
            }
        }
        #endregion

        #region ConvertReference
        protected override void ConvertProjectReference(XElement element)
        {
            XAttribute includeAttribute = element.Attribute("Include");
            if (includeAttribute != null)
            {
                string value = includeAttribute.Value;
                foreach (var reference in referencesToRemove)
                {
                    if (value.ToLowerInvariant().Contains(reference.ToLowerInvariant()))
                    {
                        element.Remove();
                        break;
                    }
                }
            }
        }
        #endregion

        #region ConvertMainPropertyGroup
        protected override void ConvertMainPropertyGroup(XElement element)
		{
			DeleteNodeIfExists(element, "ProjectTypeGuids");
			DeleteNodeIfExists(element, "TargetFrameworkProfile");

			XElement outputTypeNode = GetOrCreateNode(element, "OutputType");
			if (outputTypeNode.Value == "WinExe" ||
				outputTypeNode.Value == "appcontainerexe")
			{
				outputTypeNode.Value = "Exe";
			}
			else if (String.IsNullOrEmpty(outputTypeNode.Value))
			{
				outputTypeNode.Value = "Library";
			}
		}
		#endregion
	}
}
