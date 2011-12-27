using System;
using System.Collections.Generic;
using System.Reflection;

#region License

//
// This file is part of the ANX.Framework created by the "ANX.Framework developer group".
//
// This file is released under the Ms-PL license.
//
//
//
// Microsoft Public License (Ms-PL)
//
// This license governs use of the accompanying software. If you use the software, you accept this license. 
// If you do not accept the license, do not use the software.
//
// 1.Definitions
//   The terms "reproduce," "reproduction," "derivative works," and "distribution" have the same meaning 
//   here as under U.S. copyright law.
//   A "contribution" is the original software, or any additions or changes to the software.
//   A "contributor" is any person that distributes its contribution under this license.
//   "Licensed patents" are a contributor's patent claims that read directly on its contribution.
//
// 2.Grant of Rights
//   (A) Copyright Grant- Subject to the terms of this license, including the license conditions and limitations 
//       in section 3, each contributor grants you a non-exclusive, worldwide, royalty-free copyright license to 
//       reproduce its contribution, prepare derivative works of its contribution, and distribute its contribution
//       or any derivative works that you create.
//   (B) Patent Grant- Subject to the terms of this license, including the license conditions and limitations in 
//       section 3, each contributor grants you a non-exclusive, worldwide, royalty-free license under its licensed
//       patents to make, have made, use, sell, offer for sale, import, and/or otherwise dispose of its contribution 
//       in the software or derivative works of the contribution in the software.
//
// 3.Conditions and Limitations
//   (A) No Trademark License- This license does not grant you rights to use any contributors' name, logo, or trademarks.
//   (B) If you bring a patent claim against any contributor over patents that you claim are infringed by the software, your 
//       patent license from such contributor to the software ends automatically.
//   (C) If you distribute any portion of the software, you must retain all copyright, patent, trademark, and attribution 
//       notices that are present in the software.
//   (D) If you distribute any portion of the software in source code form, you may do so only under this license by including
//       a complete copy of this license with your distribution. If you distribute any portion of the software in compiled or 
//       object code form, you may only do so under a license that complies with this license.
//   (E) The software is licensed "as-is." You bear the risk of using it. The contributors give no express warranties, guarantees,
//       or conditions. You may have additional consumer rights under your local laws which this license cannot change. To the
//       extent permitted under your local laws, the contributors exclude the implied warranties of merchantability, fitness for a 
//       particular purpose and non-infringement.

#endregion // License

namespace ANXStatusComparer.Data
{
	public class MethodElement : BaseObjectElement
	{
		public MethodInfo Handle
		{
			get;
			private set;
		}

		public MethodElement(MethodInfo method, string setKeyName)
			 : base(setKeyName)
		{
			Handle = method;
		}

		#region IsCorrect
		public override bool IsCorrect(BaseObjectElement otherElement)
		{
			MethodElement other = otherElement as MethodElement;
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
