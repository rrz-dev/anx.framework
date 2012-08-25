using System;
using System.Reflection;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.NonXNA
{
	public class AddIn : IComparable<AddIn>
	{
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
			get;
			private set;
		}

		public string Name
		{
			get
			{
				return Instance.Name;
			}
		}

		public int Priority
		{
			get
			{
				return Instance.Priority;
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
		public AddIn(Type creatorType, Type supportedPlatformsType)
		{
			this.assembly = creatorType.Assembly;
			this.creatorType = creatorType;
			Type = AddInSystemFactory.GetAddInType(creatorType);
			this.supportedPlatforms = (ISupportedPlatforms)Activator.CreateInstance(supportedPlatformsType);
			Version = assembly.GetName().Version;
		}
		#endregion

		#region CompareTo
		public int CompareTo(AddIn other)
		{
			return this.Priority.CompareTo(other.Priority);
		}
		#endregion

		#region CreateInstanceIfPossible
		private void CreateInstanceIfPossible()
		{
			if (instance == null && IsSupported)
			{
				try
				{
					instance = Activator.CreateInstance(creatorType) as ICreator;
				}
				catch
				{
				}

				if (instance != null)
					AddInSystemFactory.Instance.AddCreator(instance);
			}
		}
		#endregion
	}
}
