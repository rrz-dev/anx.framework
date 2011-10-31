using System;

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
