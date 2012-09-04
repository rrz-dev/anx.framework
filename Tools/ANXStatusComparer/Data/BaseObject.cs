using System;
using System.Collections.Generic;
using System.Reflection;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

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

		public int PercentageComplete
		{
			get;
			private set;
		}

		public bool HasPercentageAttribute
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

			HasPercentageAttribute = false;
			foreach (Attribute attribute in Handle.GetCustomAttributes(false))
			{
				if (attribute.GetType().FullName ==
					"ANX.Framework.NonXNA.Development.PercentageCompleteAttribute")
				{
					var property = attribute.GetType().GetProperty("Percentage");
					PercentageComplete = (int)property.GetValue(attribute, null);
					HasPercentageAttribute = true;
				}
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

		public override string ToString()
		{
			return (HasPercentageAttribute ? PercentageComplete.ToString("000") : "-?-") + "% - " + Handle.FullName;
		}

		#region IsCorrect
		public bool IsCorrect(BaseObject otherObject,
			ResultData.WrongObjectPair wrongPair)
		{
			bool isCorrect = true;
			if (CompareLists(Methods, otherObject.Methods, wrongPair) == false)
				isCorrect = false;
			if (CompareLists(Events, otherObject.Events, wrongPair) == false)
				isCorrect = false;
			if (CompareLists(Fields, otherObject.Fields, wrongPair) == false)
				isCorrect = false;
			if (CompareLists(Properties, otherObject.Properties, wrongPair) == false)
				isCorrect = false;

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
				wrongPair.WrongAccesses.Add("[IsPublic(XNA:" + Handle.IsPublic + "|ANX:" + otherObject.Handle.IsPublic + ")] ");
				isCorrect = false;
			}

			if (Handle.IsSealed != otherObject.Handle.IsSealed)
			{
				wrongPair.WrongAccesses.Add("[IsSealed(XNA:" + Handle.IsSealed + "|ANX:" + otherObject.Handle.IsSealed + ")] ");
				isCorrect = false;
			}

			if (Handle.IsAbstract != otherObject.Handle.IsAbstract)
			{
				wrongPair.WrongAccesses.Add("[IsAbstract(XNA:" + Handle.IsAbstract + "|ANX:" + otherObject.Handle.IsAbstract + ")] ");
				isCorrect = false;
			}

			if (Handle.IsGenericType != otherObject.Handle.IsGenericType)
			{
				wrongPair.WrongAccesses.Add("[IsGenericType(XNA:" + Handle.IsGenericType + "|ANX:" +
					otherObject.Handle.IsGenericType + ")] ");
				isCorrect = false;
			}

			if (Handle.IsVisible != otherObject.Handle.IsVisible)
			{
				wrongPair.WrongAccesses.Add("[IsVisible(XNA:" + Handle.IsVisible + "|ANX:" + otherObject.Handle.IsVisible +
					")] ");
				isCorrect = false;
			}

			return isCorrect;
		}
		#endregion

		#region CompareLists
		private bool CompareLists(Dictionary<string, BaseObjectElement> dictXna, Dictionary<string, BaseObjectElement> dictAnx,
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
