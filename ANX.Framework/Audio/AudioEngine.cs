using System;
using System.Collections.ObjectModel;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Audio
{
	public class AudioEngine : IDisposable
	{
		#region Constants
		public const int ContentVersion = 0x27;
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
			get
			{
				throw new NotImplementedException();
			}
		}
		#endregion

		#region Constructor
		public AudioEngine(string settingsFile)
		{
			throw new NotImplementedException();
		}

		public AudioEngine(string settingsFile, TimeSpan lookAheadTime, string rendererId)
		{
			throw new NotImplementedException();
		}

		~AudioEngine()
		{
			Dispose();
		}
		#endregion

		#region GetCategory
		public AudioCategory GetCategory(string name)
		{
			throw new NotImplementedException();
		}
		#endregion

		#region GetGlobalVariable
		public float GetGlobalVariable(string name)
		{
			throw new NotImplementedException();
		}
		#endregion

		#region SetGlobalVariable
		public void SetGlobalVariable(string name, float value)
		{
			throw new NotImplementedException();
		}
		#endregion

		#region Update
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
				IsDisposed = true;
				throw new NotImplementedException();
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
