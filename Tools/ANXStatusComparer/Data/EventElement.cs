using System;
using System.Collections.Generic;
using System.Reflection;

namespace ANXStatusComparer.Data
{
	public class EventElement : BaseObjectElement
	{
		public EventInfo Handle
		{
			get;
			private set;
		}

		public MethodInfo AddMethod
		{
			get;
			private set;
		}

		public MethodInfo RemoveMethod
		{
			get;
			private set;
		}

		public EventElement(EventInfo method, string setKeyName)
			 : base(setKeyName)
		{
			Handle = method;
			AddMethod = Handle.GetAddMethod();
			RemoveMethod = Handle.GetRemoveMethod();
		}

		public override bool IsCorrect(BaseObjectElement otherElement)
		{
			EventElement other = otherElement as EventElement;
			bool isCorrect = true;

			if (otherElement.KeyName != KeyName)
			{
				isCorrect = false;
			}

			if (AddMethod != null &&
				other.AddMethod != null)
			{
				if (AddMethod.IsPublic != other.AddMethod.IsPublic)
				{
					isCorrect = false;
				}
				if (AddMethod.IsFamily != other.AddMethod.IsFamily)
				{
					isCorrect = false;
				}
			}
			if (RemoveMethod != null &&
				other.RemoveMethod != null)
			{
				if (RemoveMethod.IsPublic != other.RemoveMethod.IsPublic)
				{
					isCorrect = false;
				}
				if (RemoveMethod.IsFamily != other.RemoveMethod.IsFamily)
				{
					isCorrect = false;
				}
			}

			return isCorrect;
		}

		public override string GetDescription()
		{
			string result = "";
			if (AddMethod != null)
			{
				result += (AddMethod.IsPublic ? "[PUBLIC ADD] " : "") +
					(AddMethod.IsFamily ? "[PROTECTED ADD] " : "");
			}
			if (RemoveMethod != null)
			{
				result += (RemoveMethod.IsPublic ? "[PUBLIC REMOVE] " : "") +
					(RemoveMethod.IsFamily ? "[PROTECTED REMOVE] " : "");
			}

			result += Handle.ToString();
			return result;
		}
	}
}
