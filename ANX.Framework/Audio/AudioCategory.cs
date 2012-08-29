using System;
using System.IO;
using ANX.Framework.NonXNA.Development;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Audio
{
	[PercentageComplete(30)]
	public struct AudioCategory : IEquatable<AudioCategory>
	{
		#region Internal helper enums
		internal enum InstanceBehaviour
		{
			// behaviour flags (reside in upper 5 bits of the byte)
			FailToPlay = 0x00,
			Queue = 0x01,
			ReplaceLowestPriority = 0x04,
			ReplaceOldest = 0x02,
			ReplaceQuietest = 0x03,
		}

		internal enum InstanceCrossFading
		{
			// cross fade type flags (reside in lower 3 bits of byte)
			XFadeLinear = 0x00,
			XFadeLog = 0x01,
			XFadeEqlPow = 0x02,
		}

		internal enum Visibility
		{
			BgMusic = 0x01,
			Public = 0x02,
			Private = 0x00,
		}
		#endregion

		#region Public
		/// <summary>
		/// Maximum number of instances (if 0xFF, then there is no limit)
		/// </summary>
		internal byte MaxNumberOfInstances
		{
			get;
			private set;
		}

		internal float FadeInSeconds
		{
			get;
			private set;
		}

		internal float FadeOutSeconds
		{
			get;
			private set;
		}

		internal InstanceBehaviour Behaviour
		{
			get;
			private set;
		}

		internal InstanceCrossFading CrossFading
		{
			get;
			private set;
		}

		internal byte Volume
		{
			get;
			private set;
		}

		internal Visibility CategoryVisibility
		{
			get;
			private set;
		}

		public string Name
		{
			get;
			internal set;
		}
		#endregion

		#region Constructor
		internal AudioCategory(BinaryReader reader)
			: this()
		{
			MaxNumberOfInstances = reader.ReadByte();

			// fixed point (1000 = 1.000) of fade (in seconds)
			FadeInSeconds = reader.ReadUInt16() / 1000f;
			FadeOutSeconds = reader.ReadUInt16() / 1000f;

			byte behaviourFlags = reader.ReadByte();
			Behaviour = (InstanceBehaviour)(behaviourFlags >> 3);
			CrossFading = (InstanceCrossFading)(behaviourFlags & 7);

			// unknown, seems to be 0xFFFF for Default and 0x0000 for everyone else
			reader.ReadUInt16();

			Volume = reader.ReadByte();
			CategoryVisibility = (Visibility)reader.ReadByte();
		}

		internal AudioCategory(string setName)
			: this()
		{
			Name = setName;
		}
		#endregion

		#region Pause (TODO)
		public void Pause()
		{
			throw new NotImplementedException();
		}
		#endregion

		#region Resume (TODO)
		public void Resume()
		{
			throw new NotImplementedException();
		}
		#endregion

		#region Stop (TODO)
		public void Stop(AudioStopOptions options)
		{
			throw new NotImplementedException();
		}
		#endregion

		#region SetVolume (TODO)
		public void SetVolume(float volume)
		{
			throw new NotImplementedException();
		}
		#endregion

		#region GetHashCode (TODO)
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

		#region Equality (TODO)
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
