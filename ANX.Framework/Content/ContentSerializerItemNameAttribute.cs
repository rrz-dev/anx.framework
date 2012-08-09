using System;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Content
{
	[AttributeUsage(AttributeTargets.Class)]
	public sealed class ContentSerializerCollectionItemNameAttribute : Attribute
	{
		private string collectionItemName;

		public string CollectionItemName
		{
			get
			{
				return this.collectionItemName;
			}
		}

		public ContentSerializerCollectionItemNameAttribute(string collectionItemName)
		{
			if (string.IsNullOrEmpty(collectionItemName))
			{
				throw new ArgumentNullException("collectionItemName");
			}
			this.collectionItemName = collectionItemName;
		}
	}
}
