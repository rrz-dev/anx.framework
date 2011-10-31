using System;
using System.Collections.Generic;
using System.Reflection;

namespace ANXStatusComparer.Data
{
	public class ConstructorElement : BaseObjectElement
	{
		public ConstructorInfo Handle
		{
			get;
			private set;
		}

		public ConstructorElement(ConstructorInfo method, string setKeyName)
			 : base(setKeyName)
		{
			Handle = method;
		}

		public override bool IsCorrect(BaseObjectElement otherElement)
		{
			ConstructorElement other = otherElement as ConstructorElement;
			bool isCorrect = true;

			if (otherElement.KeyName != KeyName)
			{
				isCorrect = false;
			}

			if (Handle.IsPublic != other.Handle.IsPublic)
			{
				isCorrect = false;
			}

			if (Handle.IsFamily != other.Handle.IsFamily)
			{
				isCorrect = false;
			}

			return isCorrect;
		}

		public override string GetDescription()
		{
			return (Handle.IsPublic ? "PUBLIC " : "") +
				(Handle.IsFamily ? "PROTECTED " : "") +
				Handle.ToString();
		}
	}
}
