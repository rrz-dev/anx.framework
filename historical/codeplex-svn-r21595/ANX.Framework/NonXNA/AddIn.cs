using System;
using System.Reflection;
using ANX.Framework.NonXNA.Reflection;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.NonXNA
{
	public class AddIn : IComparable<AddIn>
	{
		#region Private
		private Type creatorType;
		private ICreator instance;
		private ISupportedPlatforms supportedPlatforms;
		#endregion

		#region Public
		public bool IsSupported
		{
			get
			{
				if (supportedPlatforms != null)
				{
					PlatformName platformName = OSInformation.GetName();
					foreach (var platform in supportedPlatforms.Names)
						if (platformName == platform)
							return true;
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
				try
				{
					if (Instance != null)
						return Instance.Name;
				}
				catch
				{
				}

				return "*** no instance of AddIn *** (" + creatorType.FullName + ")";
			}
		}

		public int Priority
		{
			get
			{
                if (instance != null)
                    return instance.Priority;
                else
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
		public AddIn(Type creatorType, Type supportedPlatformsType)
		{
			this.creatorType = creatorType;
			Type = AddInSystemFactory.GetAddInType(creatorType);
			this.supportedPlatforms = TypeHelper.Create<ISupportedPlatforms>(supportedPlatformsType);

			var assembly = TypeHelper.GetAssemblyFrom(creatorType);
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
					instance = TypeHelper.Create<ICreator>(creatorType);
                }
                catch (Exception ex)
				{
					HandleCreateException(ex);
                }

                if (instance != null)
                    AddInSystemFactory.Instance.AddCreator(instance);
			}
		}
		#endregion

		#region HandleCreateException
		private void HandleCreateException(Exception ex)
		{
			if (ex.InnerException == null)
			{
				Logger.Error("couldn't create instance of creator '" + creatorType.FullName + "'.", ex);
				return;
			}

			string innerMessage = ex.InnerException.Message;
			if (innerMessage.Contains("openal32.dll"))
			{
				Logger.Error("Couldn't create instance of creator '" + creatorType.FullName +
					"' cause OpenAL is not installed and the dll's couldn't be found in the output path, too! " +
					"Make sure the OpenAL32.dll and the wrap_oal.dll files are in the output folder. You can " +
					"find them in the lib folder of the ANX.Framework or download and run the installer from " +
					"http://connect.creativelabs.com/openal/Downloads/oalinst.zip");
			}
			else
				Logger.Error("couldn't create instance of creator '" + creatorType.FullName + "'.", ex.InnerException);
		}
		#endregion
	}
}
