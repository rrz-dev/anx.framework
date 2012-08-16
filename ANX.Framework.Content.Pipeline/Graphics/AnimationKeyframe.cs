using System;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Content.Pipeline.Graphics
{
	public sealed class AnimationKeyframe : IComparable<AnimationKeyframe>
	{
		#region Private
		[ContentSerializer(ElementName = "Time")]
		private TimeSpan time;
		[ContentSerializer(ElementName = "Transform")]
		private Matrix transform;
		#endregion

		#region Public
		[ContentSerializerIgnore]
		public TimeSpan Time
		{
			get
			{
				return time;
			}
		}

		[ContentSerializerIgnore]
		public Matrix Transform
		{
			get
			{
				return transform;
			}
			set
			{
				transform = value;
			}
		}
		#endregion

		#region Constructor
		/// <summary>
		/// The default constructor is hidden.
		/// </summary>
		private AnimationKeyframe()
		{
		}

		public AnimationKeyframe(TimeSpan setTime, Matrix setTransform)
		{
			time = setTime;
			transform = setTransform;
		}
		#endregion

		#region CompareTo
		public int CompareTo(AnimationKeyframe other)
		{
			return time.CompareTo(other.time);
		}
		#endregion
	}
}
