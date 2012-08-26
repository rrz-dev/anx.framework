using System;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Audio
{
	public struct AudioCategory : IEquatable<AudioCategory>
	{
		#region Public
		public string Name
		{
			get
			{
				throw new NotImplementedException();
			}
		}
		#endregion

		#region Pause
		public void Pause()
		{
			throw new NotImplementedException();
		}
		#endregion

		#region Resume
		public void Resume()
		{
			throw new NotImplementedException();
		}
		#endregion

		#region SetVolume
		public void SetVolume(float volume)
		{
			throw new NotImplementedException();
		}
		#endregion

		#region Stop
		public void Stop(AudioStopOptions options)
		{
			throw new NotImplementedException();
		}
		#endregion

		#region GetHashCode
		public override int GetHashCode()
		{
			throw new NotImplementedException();
		}
		#endregion

		#region ToString
		public override string ToString()
		{
			return Name;
		}
		#endregion

		#region Equals
		public override bool Equals(object obj)
		{
			if (obj != null &&
				obj is AudioCategory)
			{
				return this == (AudioCategory)obj;
			}

			return false;
		}

		public bool Equals(AudioCategory other)
		{
			return this == other;
		}
		#endregion

		#region Equality
		public static bool operator ==(AudioCategory lhs, AudioCategory rhs)
		{
			throw new NotImplementedException();
		}

		public static bool operator !=(AudioCategory lhs, AudioCategory rhs)
		{
			throw new NotImplementedException();
		}
		#endregion
	}
}
