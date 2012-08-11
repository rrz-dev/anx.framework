using System;
using System.Reflection;
using ANX.Framework.NonXNA.InputSystem;
using ANX.Framework.NonXNA.Reflection;

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
		private Assembly assembly;
		private Type creatorType;
		private ICreator instance;
		private ISupportedPlatforms supportedPlatforms;
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
				if (IsValid && supportedPlatforms != null)
				{
					PlatformName platformName = OSInformation.GetName();
					foreach (var platform in supportedPlatforms.Names)
					{
						if (platformName == platform)
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
		public AddIn(Assembly assembly)
		{
			this.assembly = assembly;
			SearchForValidAddInTypes();
		}
		#endregion

		#region CompareTo
		public int CompareTo(AddIn other)
		{
			return this.Priority.CompareTo(other.Priority);
		}
		#endregion
		
		#region SearchForValidAddInTypes
		private void SearchForValidAddInTypes()
		{
			Type[] allTypes = TypeHelper.SafelyExtractTypesFrom(assembly);

			bool foundCreator = false;
			foreach (Type type in allTypes)
			{
				bool isSupportedPlatformsImpl =
					TypeHelper.IsTypeAssignableFrom(typeof(ISupportedPlatforms), type);
				if (isSupportedPlatformsImpl && TypeHelper.IsAbstract(type) == false)
				{
					try
					{
						supportedPlatforms = (ISupportedPlatforms)Activator.CreateInstance(type);
					}
					catch
					{
					}
				}

				if (foundCreator == false)
				{
					bool isTypeValidCreator = TypeHelper.IsAnyTypeAssignableFrom(
						AddInSystemFactory.ValidAddInCreators, type);
					if (isTypeValidCreator)
					{
						creatorType = type;
						Type = AddInSystemFactory.GetAddInType(type);
						foundCreator = true;
						continue;
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
