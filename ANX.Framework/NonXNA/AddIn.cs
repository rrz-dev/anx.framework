using System;
using System.Collections;
using System.IO;
using System.Reflection;
using System.Resources;
using ANX.Framework.NonXNA.InputSystem;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.NonXNA
{
	public class AddIn : IComparable<AddIn>
	{
		#region Constants
		private const string PlatformsResourceEntryKey = "SupportedPlatforms";
		#endregion

		#region Private
		private string assemblyFilepath;
		private string[] platforms;
		private Assembly assembly;
		private Type creatorType;
		private ICreator instance;
		#endregion

		#region Public
		public bool IsValid
		{
			get
			{
				return assembly != null && creatorType != null;
			}
		}

		public bool IsSupported
		{
			get
			{
				if (IsValid && platforms.Length > 0)
				{
					string platformName = OSInformation.GetName().ToString();
					foreach (string platform in platforms)
					{
						if (String.Equals(platformName, platform, StringComparison.OrdinalIgnoreCase))
						{
							return true;
						}
					}
				}

				return false;
			}
		}

		public Version Version
		{
			get
			{
				if (assembly != null)
				{
					return assembly.GetName().Version;
				}

				return null;
			}
		}

		public string Name
		{
			get
			{
				if (assembly != null)
				{
					return Instance.Name;
				}

				return String.Empty;
			}
		}

		public int Priority
		{
			get
			{
				if (assembly != null)
				{
					return Instance.Priority;
				}

				return int.MaxValue;
			}
		}

		public AddInType Type
		{
			get;
			private set;
		}

		/// <summary>
		/// Returns the concrete instance of the creator of this AddIn.
		/// The instance is cached and only created on first access.
		/// </summary>
		/// <remarks>
		/// If this AddIn is not supported on this platform the instance will be null.
		/// </remarks>
		public ICreator Instance
		{
			get
			{
				CreateInstanceIfPossible();
				return instance;
			}
		}
		#endregion

		#region Constructor
		public AddIn(string assemblyFilepath)
		{
			ValidateAndSetFilepath(assemblyFilepath);

			try
			{
#if WINDOWSMETRO
                // TODO: make sure this works with the extracted data from the typelist, move to an extra class with all stuff for assembly loading
                assembly = Assembly.Load(new AssemblyName(assemblyFilepath));
#else
				assembly = Assembly.LoadFrom(assemblyFilepath);
#endif
			}
			catch
			{
				return;
			}

			SearchForCreatorsAndInputDevices();

			if (creatorType != null)
			{
				GetSupportedPlatformsFromResources(assembly);
			}
			else
			{
				platforms = new string[0];
			}
		}
		#endregion

		#region CompareTo
		public int CompareTo(AddIn other)
		{
			return this.Priority.CompareTo(other.Priority);
		}
		#endregion

		#region GetSupportedPlatformsFromResources
		private void GetSupportedPlatformsFromResources(Assembly assembly)
		{
			string[] allResourceNames = assembly.GetManifestResourceNames();
			foreach (string resource in allResourceNames)
			{
				try
				{
					Stream manifestResourceStream = assembly.GetManifestResourceStream(resource);
					CheckIfResourceIsPlatformsEntry(manifestResourceStream);
				}
				catch
				{
				}
			}
		}
		#endregion

		#region CheckIfResourceIsPlatformsEntry
		private void CheckIfResourceIsPlatformsEntry(Stream manifestResourceStream)
		{
			using (var resourceReader = new ResourceReader(manifestResourceStream))
			{
				IDictionaryEnumerator currentResourceEntry = resourceReader.GetEnumerator();
				while (currentResourceEntry.MoveNext())
				{
					if (IsResourceEntryPlatformsString(currentResourceEntry))
					{
						SetPlatformsByResourceEntry(currentResourceEntry.Value.ToString());
						break;
					}
				}
			}
		}
		#endregion

		#region IsResourceEntryPlatformsString
		private bool IsResourceEntryPlatformsString(IDictionaryEnumerator entry)
		{
			string key = entry.Key.ToString();
			return String.Equals(key, PlatformsResourceEntryKey,
				StringComparison.OrdinalIgnoreCase) &&
				entry.Value is String;
		}
		#endregion

		#region SetPlatformsByResourceEntry
		private void SetPlatformsByResourceEntry(string entry)
		{
			platforms = entry.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
		}
		#endregion

		#region ValidateAndSetFilepath
		private void ValidateAndSetFilepath(string setFilepath)
		{
			if (String.IsNullOrEmpty(setFilepath))
			{
				throw new ArgumentNullException("fileName");
			}

#if !WINDOWSMETRO
			if (File.Exists(setFilepath) == false)
			{
				throw new InvalidOperationException(
					String.Format("The AddIn '{0}' does not exist.", setFilepath));
			}
#endif

			assemblyFilepath = setFilepath;
		}
		#endregion

		#region SearchForCreatorsAndInputDevices
		private void SearchForCreatorsAndInputDevices()
		{
			Type[] allTypes = TypeHelper.SafelyExtractTypesFrom(assembly);

			bool foundCreator = false;
			foreach (Type type in allTypes)
			{
				if (foundCreator == false)
				{
					bool isTypeValidCreator = TypeHelper.IsAnyTypeAssignableFrom(
						AddInSystemFactory.ValidAddInCreators, type);
					if (isTypeValidCreator)
					{
						creatorType = type;
						Type = AddInSystemFactory.GetAddInType(type);
						foundCreator = true;
						break;
					}
				}

				bool isTypeValidInputDevice = TypeHelper.IsAnyTypeAssignableFrom(
					InputDeviceFactory.ValidInputDeviceCreators, type);
				if (isTypeValidInputDevice)
				{
					var inputCreator = Activator.CreateInstance(type) as IInputDeviceCreator;
					InputDeviceFactory.Instance.AddCreator(type, inputCreator);
				}
			}
		}
		#endregion

		#region CreateInstanceIfPossible
		private void CreateInstanceIfPossible()
		{
			if (instance == null && IsSupported)
			{
                instance = Activator.CreateInstance(creatorType) as ICreator;
				if (instance != null)
				{
					instance.RegisterCreator(AddInSystemFactory.Instance);
				}
			}
		}
		#endregion
	}
}
