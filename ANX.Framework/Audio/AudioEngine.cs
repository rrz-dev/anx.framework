using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using ANX.Framework.Audio.XactParser;
using ANX.Framework.NonXNA;
using ANX.Framework.NonXNA.Development;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Audio
{
	[PercentageComplete(50)]
	public class AudioEngine : IDisposable
	{
		#region Constants
		public const int ContentVersion = 0x27;
		#endregion

		#region Private
		private XactGeneralSettings generalSettings;
		#endregion

		#region Events
		public event EventHandler<EventArgs> Disposing;
		#endregion
		
		#region Public
		public bool IsDisposed
		{
			get;
			private set;
		}

		public ReadOnlyCollection<RendererDetail> RendererDetails
		{
			get;
			private set;
		}
		#endregion

		#region Constructor (TODO)
		public AudioEngine(string settingsFile)
		{
			// TODO: get renderer details
			RendererDetails = new ReadOnlyCollection<RendererDetail>(new List<RendererDetail>());

			Stream loadingStream = AddInSystemFactory.DefaultPlatformCreator.OpenReadFilestream(settingsFile);
			generalSettings = new XactGeneralSettings(loadingStream);
			loadingStream.Dispose();
		}

		public AudioEngine(string settingsFile, TimeSpan lookAheadTime, string rendererId)
		{
			// TODO: get renderer details
			RendererDetails = new ReadOnlyCollection<RendererDetail>(new List<RendererDetail>());

			// TODO: lookAheadTime and rendererId

			Stream loadingStream = AddInSystemFactory.DefaultPlatformCreator.OpenReadFilestream(settingsFile);
			generalSettings = new XactGeneralSettings(loadingStream);
			loadingStream.Dispose();
		}

		~AudioEngine()
		{
			Dispose();
		}
		#endregion

		#region GetCategory
		public AudioCategory GetCategory(string name)
		{
			for (int index = 0; index < generalSettings.Categories.Length; index++)
				if (generalSettings.Categories[index].Name == name)
					return generalSettings.Categories[index];

			return new AudioCategory(name);
		}
		#endregion

		#region GetGlobalVariable
		public float GetGlobalVariable(string name)
		{
			foreach (var variable in generalSettings.Variables)
				if (variable.Name == name)
					return variable.StartingValue;

			return 0f;
		}
		#endregion

		#region SetGlobalVariable
		public void SetGlobalVariable(string name, float value)
		{
			foreach (var variable in generalSettings.Variables)
				if (variable.Name == name)
					variable.StartingValue = MathHelper.Clamp(value, variable.MinValue, variable.MaxValue);
		}
		#endregion

		#region Update (TODO)
		public void Update()
		{
			throw new NotImplementedException();
		}
		#endregion

		#region Dispose
		protected virtual void Dispose(bool disposing)
		{
			if (IsDisposed == false)
			{
				if (Disposing != null)
					Disposing(this, EventArgs.Empty);

				IsDisposed = true;
				generalSettings = null;
			}
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}
		#endregion
	}
}
