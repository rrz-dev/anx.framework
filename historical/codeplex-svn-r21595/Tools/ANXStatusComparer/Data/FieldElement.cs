using System;
using System.Collections.Generic;
using System.Reflection;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANXStatusComparer.Data
{
	public class FieldElement : BaseObjectElement
	{
		public FieldInfo Handle
		{
			get;
			private set;
		}

		public FieldElement(FieldInfo method, string setKeyName)
			 : base(setKeyName)
		{
			Handle = method;
		}

		public override bool IsCorrect(BaseObjectElement otherElement)
		{
			FieldElement other = otherElement as FieldElement;
			bool isCorrect = true;

			if (otherElement.KeyName != KeyName)
			{
				isCorrect = false;
			}

			if(Handle.IsPublic != other.Handle.IsPublic)
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
