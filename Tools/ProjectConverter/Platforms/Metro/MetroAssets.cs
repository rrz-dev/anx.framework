using System;
using System.IO;
using System.Xml.Linq;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ProjectConverter.Platforms.Metro
{
	public class MetroAssets
	{
		#region Private
		private ProjectPath project;
		#endregion

		#region Constructor
		public MetroAssets(ProjectPath setProject)
		{
			project = setProject;
		}
		#endregion

		#region AddAssetsToProject
		public void AddAssetsToProject(XElement itemGroup)
		{
			string assetsPath = Path.Combine(project.FullSourceDirectoryPath, "Assets");
			if (Directory.Exists(assetsPath) == false)
			{
				Directory.CreateDirectory(assetsPath);
			}
			
			XName contentNodeName = XName.Get("Content", itemGroup.Name.NamespaceName);
			const string anxAssetsPath = "../../media/MetroDefaultAssets";
			foreach (string assetFilepath in Directory.GetFiles(anxAssetsPath))
			{
				string filename = Path.GetFileName(assetFilepath);
				string targetAssetFilepath = Path.Combine(assetsPath, filename);
				File.Copy(assetFilepath, targetAssetFilepath, true);

				XElement newContentNode = new XElement(contentNodeName);
				newContentNode.Add(new XAttribute("Include", "Assets\\" + filename));
				itemGroup.Add(newContentNode);
			}
		}
		#endregion
	}
}
