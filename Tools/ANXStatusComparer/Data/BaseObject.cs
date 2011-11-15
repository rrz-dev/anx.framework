#region Private Members
using System;
using System.Collections.Generic;
using System.Reflection;

#endregion // Private Members

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
	public class BaseObject
	{
		#region Public
		/// <summary>
		/// The type of the enumeration.
		/// </summary>
		public Type Handle
		{
			get;
			private set;
		}

		public Dictionary<string, BaseObjectElement> Methods
		{
			get;
			private set;
		}

		public Dictionary<string, BaseObjectElement> Fields
		{
			get;
			private set;
		}

		public Dictionary<string, BaseObjectElement> Properties
		{
			get;
			private set;
		}

		public Dictionary<string, BaseObjectElement> Events
		{
			get;
			private set;
		}

		public List<string> ParentNames
		{
			get;
			private set;
		}
		#endregion

		#region Constructor
		/// <summary>
		/// Create a new BaseObject data holder.
		/// </summary>
		/// <param name="setType">Type of the object.</param>
		public BaseObject(Type setType)
		{
			Handle = setType;
			Methods = new Dictionary<string, BaseObjectElement>();
			Fields = new Dictionary<string, BaseObjectElement>();
			Properties = new Dictionary<string, BaseObjectElement>();
			Events = new Dictionary<string, BaseObjectElement>();

			ParentNames = new List<string>();
			if (Handle.BaseType != null)
			{
				ParentNames.Add(Handle.BaseType.Name);
			}

			foreach (Type interfaceName in Handle.GetInterfaces())
			{
				ParentNames.Add(interfaceName.Name);
			}

			BindingFlags bindingFlags = BindingFlags.Public |
				BindingFlags.Static | BindingFlags.Instance |
				BindingFlags.DeclaredOnly | BindingFlags.NonPublic;

			#region Find Events
			foreach (EventInfo instanceEvent in Handle.GetEvents(bindingFlags))
			{
				MethodInfo addMethod = instanceEvent.GetAddMethod();
				MethodInfo removeMethod = instanceEvent.GetRemoveMethod();
				if ((addMethod != null &&
						(addMethod.IsPublic || addMethod.IsFamily)) ||
					(removeMethod != null &&
						(removeMethod.IsPublic || removeMethod.IsFamily)))
				{
					string keyName = AssemblyComparer.TranslateNamespaceName(
						instanceEvent.ToString());
					Events.Add(keyName, new EventElement(instanceEvent, keyName));
				}
			}
			#endregion

			#region Find Fields
			foreach (FieldInfo field in Handle.GetFields(bindingFlags))
			{
				if (field.IsPublic == false &&
					field.IsFamily == false)
				{
					continue;
				}
				string keyName = AssemblyComparer.TranslateNamespaceName(
					field.ToString());
				Fields.Add(keyName, new FieldElement(field, keyName));
			}
			#endregion

			#region Find Properties
			foreach (PropertyInfo property in Handle.GetProperties(bindingFlags))
			{
				MethodInfo getMethod = property.GetGetMethod();
				MethodInfo setMethod = property.GetSetMethod();
				if ((getMethod != null &&
						(getMethod.IsPublic || getMethod.IsFamily)) ||
					(setMethod != null &&
						(setMethod.IsPublic || setMethod.IsFamily)))
				{
					string keyName = AssemblyComparer.TranslateNamespaceName(
						property.ToString());
					Properties.Add(keyName, new PropertyElement(property, keyName));
				}
			}
			#endregion

			#region Find Methods
			foreach (MethodInfo method in Handle.GetMethods(bindingFlags))
			{
				if(method.IsSpecialName &&
					(method.Name.StartsWith("get_") ||
					method.Name.StartsWith("set_") ||
					method.Name.StartsWith("add_") ||
					method.Name.StartsWith("remove_")))
				{
					continue;
				}
				if(method.IsPublic == false &&
					method.IsFamily == false)
				{
					continue;
				}
				string keyName = AssemblyComparer.TranslateNamespaceName(
					method.ToString());
				Methods.Add(keyName, new MethodElement(method, keyName));
			}
			foreach (ConstructorInfo method in Handle.GetConstructors(bindingFlags))
			{
				if (method.IsPublic == false &&
					method.IsFamily == false)
				{
					continue;
				}
				string keyName = AssemblyComparer.TranslateNamespaceName(
					method.ToString());
				Methods.Add(keyName, new ConstructorElement(method, keyName));
			}
			#endregion
		}
		#endregion

		#region IsCorrect
		public bool IsCorrect(BaseObject otherObject,
			ResultData.WrongObjectPair wrongPair)
		{
			bool isCorrect = true;
			if (CompareLists(Methods, otherObject.Methods, wrongPair) == false)
			{
				isCorrect = false;
			}
			if (CompareLists(Events, otherObject.Events, wrongPair) == false)
			{
				isCorrect = false;
			}
			if (CompareLists(Fields, otherObject.Fields, wrongPair) == false)
			{
				isCorrect = false;
			}
			if (CompareLists(Properties, otherObject.Properties, wrongPair) == false)
			{
				isCorrect = false;
			}

			foreach(string parent in ParentNames)
			{
				if(otherObject.ParentNames.Contains(parent) == false)
				{
					wrongPair.MissingParents.Add(parent);
					isCorrect = false;
				}
			}

			if (Handle.IsPublic != otherObject.Handle.IsPublic)
			{
				wrongPair.WrongAccesses.Add("[IsPublic(XNA:" + Handle.IsPublic +
					"|ANX:" + otherObject.Handle.IsPublic + ")] ");
				isCorrect = false;
			}

			if (Handle.IsSealed != otherObject.Handle.IsSealed)
			{
				wrongPair.WrongAccesses.Add("[IsSealed(XNA:" + Handle.IsSealed +
					"|ANX:" + otherObject.Handle.IsSealed + ")] ");
				isCorrect = false;
			}

			if (Handle.IsAbstract != otherObject.Handle.IsAbstract)
			{
				wrongPair.WrongAccesses.Add("[IsAbstract(XNA:" + Handle.IsAbstract +
					"|ANX:" + otherObject.Handle.IsAbstract + ")] ");
				isCorrect = false;
			}

			if (Handle.IsGenericType != otherObject.Handle.IsGenericType)
			{
				wrongPair.WrongAccesses.Add("[IsGenericType(XNA:" +
					Handle.IsGenericType +
					"|ANX:" + otherObject.Handle.IsGenericType + ")] ");
				isCorrect = false;
			}

			if (Handle.IsVisible != otherObject.Handle.IsVisible)
			{
				wrongPair.WrongAccesses.Add("[IsVisible(XNA:" + Handle.IsVisible +
					"|ANX:" + otherObject.Handle.IsVisible + ")] ");
				isCorrect = false;
			}

			return isCorrect;
		}
		#endregion

		#region CompareLists
		private bool CompareLists(Dictionary<string, BaseObjectElement> dictXna,
			Dictionary<string, BaseObjectElement> dictAnx,
			ResultData.WrongObjectPair wrongPair)
		{
			bool isCorrect = true;
			foreach (string methodKey in dictXna.Keys)
			{
				if (dictAnx.ContainsKey(methodKey) == false)
				{
					isCorrect = false;
					wrongPair.XnaElements.Add(dictXna[methodKey]);
					wrongPair.AnxElements.Add(null);
					continue;
				}

				if (dictXna[methodKey].IsCorrect(dictAnx[methodKey]) == false)
				{
					isCorrect = false;
					wrongPair.XnaElements.Add(dictXna[methodKey]);
					wrongPair.AnxElements.Add(dictAnx[methodKey]);
				}
			}

			return isCorrect;
		}
		#endregion

		#region Tests
		private class Tests
		{
			private class TestClass
			{
				public static int StaticMethod1(string value)
				{
					return 1;
				}

				public void NormalMethod() { }

				protected void Protected() { }

				private void Private() { }

				internal void Internal() { }
			}

			public static void TestMethodGet()
			{
				Type classType = typeof (TestClass);
				
				BindingFlags bindingFlags = BindingFlags.Public |
					BindingFlags.Static | BindingFlags.Instance |
					BindingFlags.DeclaredOnly | BindingFlags.NonPublic;

				Console.WriteLine("Members");
				Console.WriteLine("-----------------");
				foreach (MemberInfo info in classType.GetMembers(bindingFlags))
				{
					Console.WriteLine(info.ToString());
				}
			}
		}
		#endregion
	}
}
