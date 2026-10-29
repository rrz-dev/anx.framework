using System;
using System.Collections.Generic;
using System.Reflection;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

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

		#region IsCorrect
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

			ParameterInfo[] parameters = Handle.GetParameters();
			ParameterInfo[] otherParameters = other.Handle.GetParameters();
			if (parameters.Length != otherParameters.Length)
			{
				isCorrect = false;
			}
			else
			{
				for (int index = 0; index < parameters.Length; index++)
				{
					ParameterInfo thisParam = parameters[index];
					ParameterInfo otherParam = otherParameters[index];
					if (thisParam.IsIn != otherParam.IsIn)
					{
						isCorrect = false;
					}
					if (thisParam.IsOptional != otherParam.IsOptional)
					{
						isCorrect = false;
					}
					if (thisParam.IsOut != otherParam.IsOut)
					{
						isCorrect = false;
					}

					#region Check custom attributes
					object[] thisAttributes = thisParam.GetCustomAttributes(false);
					object[] otherAttributes = otherParam.GetCustomAttributes(false);
					Dictionary<Type, int> thisAttribDict = new Dictionary<Type, int>();
					Dictionary<Type, int> otherAttribDict = new Dictionary<Type, int>();
					if (thisAttributes.Length != otherAttributes.Length)
					{
						isCorrect = false;
					}
					else
					{
						for (int attribIndex = 0; attribIndex < thisAttributes.Length; attribIndex++)
						{
							Type thisType = thisAttributes[attribIndex].GetType();
							if (thisAttribDict.ContainsKey(thisType) == false)
							{
								thisAttribDict.Add(thisType, 0);
							}
							thisAttribDict[thisType]++;

							Type otherType = otherAttributes[attribIndex].GetType();
							if (otherAttribDict.ContainsKey(otherType) == false)
							{
								otherAttribDict.Add(otherType, 0);
							}
							otherAttribDict[otherType]++;
						}
					}

					foreach (Type thisType in thisAttribDict.Keys)
					{
						if (otherAttribDict.ContainsKey(thisType) == false)
						{
							isCorrect = false;
							break;
						}

						if (thisAttribDict[thisType] != otherAttribDict[thisType])
						{
							isCorrect = false;
							break;
						}
					}
					#endregion
				}
			}

			if (Handle.IsFamily != other.Handle.IsFamily)
			{
				isCorrect = false;
			}

			return isCorrect;
		}
		#endregion

		public override string GetDescription()
		{
			string result = (Handle.IsPublic ? "PUBLIC " : "") +
				(Handle.IsFamily ? "PROTECTED " : "") +
				Handle.ToString();

			#region Add attributes
			ParameterInfo[] parameters = Handle.GetParameters();
			result += " (Attributes: ";
			for (int index = 0; index < parameters.Length; index++)
			{
				ParameterInfo thisParam = parameters[index];
				object[] attributes = thisParam.GetCustomAttributes(false);
				if (attributes.Length == 0)
				{
					continue;
				}

				result += thisParam.Position + ": ";

				foreach (object attrib in attributes)
				{
					result += "[" + attrib.GetType().Name + "] ";
				}

				result += ", ";
			}
			result += ")";
			if (result.EndsWith(" (Attributes: )"))
			{
				result = result.Substring(0, result.Length - 14);
			}
			#endregion

			return result;
		}
	}
}
