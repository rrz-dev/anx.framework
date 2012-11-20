using System;
using System.Collections.Generic;
using ANXStatusComparer.Data;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

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
            if (xnaAssemblies.Namespaces == null)
            {
                return;
            }

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
            if (xnaAssemblies.Namespaces == null)
            {
                return;
            }

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
            if (xnaAssemblies.Namespaces == null)
            {
                return;
            }

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
            if (xnaAssemblies.Namespaces == null)
            {
                return;
            }

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
            if (xnaAssemblies.Namespaces == null)
            {
                return;
            }

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
			name = name.Replace("Microsoft.Xna.", "ANX.");
			return name;
		}
		#endregion
	}
}
