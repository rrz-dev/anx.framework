using System;
using System.Collections.Generic;
using System.Reflection;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANXStatusComparer.Data
{
	public class PropertyElement : BaseObjectElement
	{
		public PropertyInfo Handle
		{
			get;
			private set;
		}

		public MethodInfo GetMethod
		{
			get;
			private set;
		}

		public MethodInfo SetMethod
		{
			get;
			private set;
		}

		public PropertyElement(PropertyInfo method, string setKeyName)
			 : base(setKeyName)
		{
			Handle = method;
			GetMethod = Handle.GetGetMethod();
			SetMethod = Handle.GetSetMethod();
		}

		#region IsCorrect
		public override bool IsCorrect(BaseObjectElement otherElement)
		{
			PropertyElement other = otherElement as PropertyElement;
			bool isCorrect = true;

			if (otherElement.KeyName != KeyName)
			{
				isCorrect = false;
			}

			if (GetMethod != null &&
				other.GetMethod != null)
			{
				if (Handle.CanRead != other.Handle.CanRead)
				{
					isCorrect = false;
				}

				if (GetMethod.IsPublic != other.GetMethod.IsPublic)
				{
					isCorrect = false;
				}
				if (GetMethod.IsFamily != other.GetMethod.IsFamily)
				{
					isCorrect = false;
				}
			}
            else if (GetMethod == null && other.GetMethod != null)
            {
                if (other.GetMethod.IsPublic)
                {
                    isCorrect = false;
                }
                if (other.GetMethod.IsFamily)
                {
                    isCorrect = false;
                }
            }
            else if (GetMethod != null && other.GetMethod == null)
            {
                if (GetMethod.IsPublic)
                {
                    isCorrect = false;
                }
                if (GetMethod.IsFamily)
                {
                    isCorrect = false;
                }
            }

			if (SetMethod != null &&
				other.SetMethod != null)
			{
				if (Handle.CanWrite != other.Handle.CanWrite)
				{
					isCorrect = false;
				}

				if (SetMethod.IsPublic != other.SetMethod.IsPublic)
				{
					isCorrect = false;
				}
				if (SetMethod.IsFamily != other.SetMethod.IsFamily)
				{
					isCorrect = false;
				}
			}
            else if (SetMethod == null && other.SetMethod != null)
            {
                if (other.SetMethod.IsPublic)
                {
                    isCorrect = false;
                }
                if (other.SetMethod.IsFamily)
                {
                    isCorrect = false;
                }
            }
            else if (SetMethod != null && other.SetMethod == null)
            {
                if (SetMethod.IsPublic)
                {
                    isCorrect = false;
                }
                if (SetMethod.IsFamily)
                {
                    isCorrect = false;
                }
            }

			return isCorrect;
		}
		#endregion

		public override string GetDescription()
		{
			string result = "";
			if(GetMethod != null)
			{
				result += (GetMethod.IsPublic ? "[PUBLIC GET] " : "") +
					(GetMethod.IsFamily ? "[PROTECTED GET] " : "");
			}
			if (SetMethod != null)
			{
				result += (SetMethod.IsPublic ? "[PUBLIC SET] " : "") +
					(SetMethod.IsFamily ? "[PROTECTED SET] " : "");
			}

			result += Handle.ToString();
			return result;
		}
	}
}
