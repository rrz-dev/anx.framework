using System;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Content.Pipeline.Graphics
{
	public struct BoneWeight
	{
		#region Private
		private string boneName;
		private float weight;
		#endregion

		#region Public
		public string BoneName
		{
			get
			{
				return boneName;
			}
		}

		public float Weight
		{
			get
			{
				return weight;
			}
		}
		#endregion

		#region Constructor
		public BoneWeight(string boneName, float weight)
		{
			if (string.IsNullOrEmpty(boneName))
			{
				throw new ArgumentNullException("boneName");
			}

			if (weight < 0f || weight > 1f)
			{
				throw new ArgumentOutOfRangeException("weight");
			}

			this.boneName = boneName;
			this.weight = weight;
		}
		#endregion
	}
}
