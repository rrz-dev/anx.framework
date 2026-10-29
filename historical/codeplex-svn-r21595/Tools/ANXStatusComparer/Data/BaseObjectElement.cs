using System;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANXStatusComparer.Data
{
	public abstract class BaseObjectElement
	{
		public string KeyName
		{
			get;
			private set;
		}

		protected BaseObjectElement(string setKeyName)
		{
			KeyName = setKeyName;
		}

		public abstract bool IsCorrect(BaseObjectElement otherElement);

		public abstract string GetDescription();
	}
}
