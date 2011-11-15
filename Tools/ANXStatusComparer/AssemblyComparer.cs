using System;
using System.Collections.Generic;
using ANXStatusComparer.Data;

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

namespace ANXStatusComparer
{
	/// <summary>
	/// The assembly comparer is the main logic component of the tool.
	/// </summary>
	public static class AssemblyComparer
	{
		#region Private
		/// <summary>
		/// Private caches of assemblies, so we don't need to pass them over
		/// as parameters all the time.
		/// </summary>
		private static AssembliesData xnaAssemblies;
		private static AssembliesData anxAssemblies;
		private static ResultData result;
		#endregion

		#region Compare
		/// <summary>
		/// Compare the assemblies of xna and anx and create a result data instance.
		/// </summary>
		/// <param name="xnaAssemblies">XNA assemblies.</param>
		/// <param name="anxAssemblies">ANX assemblies.</param>
		/// <param name="checkType">The type of check to do.</param>
		/// <returns>Generated result data.</returns>
		public static ResultData Compare(AssembliesData setXnaAssemblies,
			AssembliesData setAnxAssemblies, CheckType checkType)
		{
			xnaAssemblies = setXnaAssemblies;
			anxAssemblies = setAnxAssemblies;

			result = new ResultData();

			if (checkType == CheckType.All ||
				(checkType | CheckType.Namespaces) == checkType)
			{
				CheckNamespaces();
			}

			if (checkType == CheckType.All ||
				(checkType | CheckType.Structs) == checkType)
			{
				CheckStructs();
			}

			if (checkType == CheckType.All ||
				(checkType | CheckType.Interfaces) == checkType)
			{
				CheckInterfaces();
			}

			if (checkType == CheckType.All ||
				(checkType | CheckType.Classes) == checkType)
			{
				CheckClasses();
			}

			if (checkType == CheckType.All ||
				(checkType | CheckType.Enumerations) == checkType)
			{
				CheckEnums();
			}

			return result;
		}
		#endregion

		#region CheckNamespaces
		/// <summary>
		/// Check all the namespaces.
		/// </summary>
		private static void CheckNamespaces()
		{
			foreach (string name in xnaAssemblies.Namespaces.Keys)
			{
				if (xnaAssemblies.Namespaces[name].IsPublic == false)
				{
					continue;
				}

				string compareName = TranslateNamespaceName(name);

				if (anxAssemblies.Namespaces.ContainsKey(compareName) == false)
				{
					if (result.MissingNamespaces.Contains(name) == false)
					{
						result.MissingNamespaces.Add(name);
					}
				}
				else
				{
					if (result.ImplementedNamespaces.Contains(name) == false)
					{
						result.ImplementedNamespaces.Add(name);
					}
				}
			}
		}
		#endregion

		#region CheckInterfaces
		private static void CheckInterfaces()
		{
			foreach (string key in xnaAssemblies.Namespaces.Keys)
			{
				NamespaceData namespaceData = xnaAssemblies.Namespaces[key];
				string compareName = TranslateNamespaceName(key);

				foreach (string classKey in namespaceData.Interfaces.Keys)
				{
					CheckInterface(compareName, namespaceData.Interfaces[classKey]);
				}
			}
		}

		private static void CheckInterface(string namespaceKey,
			BaseObject xnaInterface)
		{
			// If the namespace is already missing, we can abort directly.
			if (anxAssemblies.Namespaces.ContainsKey(namespaceKey) == false)
			{
				result.MissingInterfaces.Add(xnaInterface);
				return;
			}

			NamespaceData anxNamespace = anxAssemblies.Namespaces[namespaceKey];
			// Now check if we got this enum in the anx namespace.
			if (anxNamespace.Interfaces.ContainsKey(xnaInterface.Handle.Name) == false)
			{
				result.MissingInterfaces.Add(xnaInterface);
				return;
			}

			BaseObject anxInterface = anxNamespace.Interfaces[xnaInterface.Handle.Name];

			ResultData.WrongObjectPair pair = new ResultData.WrongObjectPair()
			{
				XnaObject = xnaInterface,
				AnxObject = anxInterface,
			};
			// Everything is present, so we do the in-depth checks.
			if (xnaInterface.IsCorrect(anxInterface, pair) == false)
			{
				result.WrongInterfaces.Add(pair);
			}
			else
			{
				result.ImplementedInterfaces.Add(anxInterface);
			}
		}
		#endregion

		#region CheckClasses
		private static void CheckClasses()
		{
			foreach (string key in xnaAssemblies.Namespaces.Keys)
			{
				NamespaceData namespaceData = xnaAssemblies.Namespaces[key];
				string compareName = TranslateNamespaceName(key);

				foreach (string classKey in namespaceData.Classes.Keys)
				{
					CheckClass(compareName, namespaceData.Classes[classKey]);
				}
			}
		}

		private static void CheckClass(string namespaceKey, BaseObject xnaClass)
		{
			// If the namespace is already missing, we can abort directly.
			if (anxAssemblies.Namespaces.ContainsKey(namespaceKey) == false)
			{
				result.MissingClasses.Add(xnaClass);
				return;
			}

			NamespaceData anxNamespace = anxAssemblies.Namespaces[namespaceKey];
			// Now check if we got this enum in the anx namespace.
			if (anxNamespace.Classes.ContainsKey(xnaClass.Handle.Name) == false)
			{
				result.MissingClasses.Add(xnaClass);
				return;
			}

			BaseObject anxClass = anxNamespace.Classes[xnaClass.Handle.Name];
			
			ResultData.WrongObjectPair pair = new ResultData.WrongObjectPair()
			{
				XnaObject = xnaClass,
				AnxObject = anxClass,
			};
			// Everything is present, so we do the in-depth checks.
			if (xnaClass.IsCorrect(anxClass, pair) == false)
			{
				result.WrongClasses.Add(pair);
			}
			else
			{
				result.ImplementedClasses.Add(anxClass);
			}
		}
		#endregion

		#region CheckStructs
		private static void CheckStructs()
		{
			foreach (string key in xnaAssemblies.Namespaces.Keys)
			{
				NamespaceData namespaceData = xnaAssemblies.Namespaces[key];
				string compareName = TranslateNamespaceName(key);

				foreach (string classKey in namespaceData.Structs.Keys)
				{
					CheckStruct(compareName, namespaceData.Structs[classKey]);
				}
			}
		}

		private static void CheckStruct(string namespaceKey,
			BaseObject xnaStruct)
		{
			// If the namespace is already missing, we can abort directly.
			if (anxAssemblies.Namespaces.ContainsKey(namespaceKey) == false)
			{
				result.MissingStructs.Add(xnaStruct);
				return;
			}

			NamespaceData anxNamespace = anxAssemblies.Namespaces[namespaceKey];
			// Now check if we got this enum in the anx namespace.
			if (anxNamespace.Structs.ContainsKey(xnaStruct.Handle.Name) == false)
			{
				result.MissingStructs.Add(xnaStruct);
				return;
			}

			BaseObject anxStruct = anxNamespace.Structs[xnaStruct.Handle.Name];

			ResultData.WrongObjectPair pair = new ResultData.WrongObjectPair()
			{
				XnaObject = xnaStruct,
				AnxObject = anxStruct,
			};
			// Everything is present, so we do the in-depth checks.
			if (xnaStruct.IsCorrect(anxStruct, pair) == false)
			{
				result.WrongStructs.Add(pair);
			}
			else
			{
				result.ImplementedStructs.Add(anxStruct);
			}
		}
		#endregion

		#region CheckEnums
		/// <summary>
		/// Compare all enumerations.
		/// </summary>
		private static void CheckEnums()
		{
			foreach (string key in xnaAssemblies.Namespaces.Keys)
			{
				NamespaceData namespaceData = xnaAssemblies.Namespaces[key];
				string compareName = TranslateNamespaceName(key);

				foreach(string enumKey in namespaceData.Enums.Keys)
				{
					CheckEnum(compareName, namespaceData.Enums[enumKey]);
				}
			}
		}

		/// <summary>
		/// Compare a specific xna enumeration.
		/// </summary>
		/// <param name="namespaceKey">The name of the namespace this enum is in.
		/// </param>
		/// <param name="xnaEnum">XNA enumeration to compare.</param>
		private static void CheckEnum(string namespaceKey, EnumData xnaEnum)
		{
			// If the namespace is already missing, we can abort directly.
			if (anxAssemblies.Namespaces.ContainsKey(namespaceKey) == false)
			{
				result.MissingEnums.Add(xnaEnum);
				return;
			}

			NamespaceData anxNamespace = anxAssemblies.Namespaces[namespaceKey];
			// Now check if we got this enum in the anx namespace.
			if (anxNamespace.Enums.ContainsKey(xnaEnum.Handle.Name) == false)
			{
				result.MissingEnums.Add(xnaEnum);
				return;
			}

			EnumData anxEnum = anxNamespace.Enums[xnaEnum.Handle.Name];

			// Everything is present, so we do the in-depth checks for names and
			// values contained in the enumeration.
			bool isWrong = false;
			for (int index = 0; index < xnaEnum.Names.Count; index++)
			{
				int indexOfAnxValue = anxEnum.Names.IndexOf(xnaEnum.Names[index]);
				if (indexOfAnxValue == -1)
				{
					isWrong = true;
					break;
				}

				object value1 = anxEnum.Values.GetValue(indexOfAnxValue);
				object value2 = xnaEnum.Values.GetValue(index);
				if (value1.Equals(value2) == false)
				{
					isWrong = true;
					break;
				}
			}

			if (isWrong)
			{
				result.WrongEnums.Add(new KeyValuePair<EnumData, EnumData>(
					xnaEnum, anxEnum));
			}
			else
			{
				result.ImplementedEnums.Add(anxEnum);
			}
		}
		#endregion

		#region TranslateNamespaceName
		/// <summary>
		/// Translate a namespace name if needed.
		/// Used to get the ANX equivalent to the XNA namespace names.
		/// </summary>
		/// <param name="name">XNA namespace name.</param>
		/// <returns>ANX valid namespace name.</returns>
		public static string TranslateNamespaceName(string name)
		{
			return name.Replace("Microsoft.Xna.", "ANX.");
		}
		#endregion
	}
}
