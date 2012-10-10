using System;
using System.IO;
using System.Resources;
using ANX.Framework.NonXNA.Development;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Content
{
    [Developer("GinieDP")]
	public class ResourceContentManager : ContentManager
	{
		private ResourceManager resource;

		public ResourceContentManager(IServiceProvider servicesProvider,
			ResourceManager resource)
			: base(servicesProvider)
		{
			if (resource == null)
			{
				throw new ArgumentNullException("resource");
			}
			this.resource = resource;
		}

		protected override Stream OpenStream(string assetName)
		{
#if WINDOWSMETRO // TODO: find replacement and encapsulate in platform addins!!
			object obj = null;
#else
			object obj = resource.GetObject(assetName);
#endif

			if (obj == null)
			{
				throw new ContentLoadException("Resource not found");
			}
			if ((obj is byte[]) == false)
			{
				throw new ContentLoadException("Resource is not in binary format");
			}
			return new MemoryStream(obj as byte[]);
		}
	}
}
