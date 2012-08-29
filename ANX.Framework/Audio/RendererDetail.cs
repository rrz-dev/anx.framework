using System;
using ANX.Framework.NonXNA.Development;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Audio
{
	[PercentageComplete(100)]
	public struct RendererDetail
	{
		#region Private
		private string friendlyName;
		private string rendererId;
		#endregion

		#region Public
		public string FriendlyName
		{
			get
			{
				return friendlyName;
			}
		}

		public string RendererId
		{
			get
			{
				return rendererId;
			}
		}
		#endregion
		
		#region Constructor
		internal RendererDetail(string setFriendlyName, string setRendererId)
		{
			friendlyName = setFriendlyName;
			rendererId = setRendererId;
		}
		#endregion

		#region GetHashCode
		public override int GetHashCode()
		{
			int hash1 = String.IsNullOrEmpty(friendlyName) ?
				0 :
				friendlyName.GetHashCode();

			int hash2 = String.IsNullOrEmpty(rendererId) ?
				0 :
				rendererId.GetHashCode();

			return hash1 ^ hash2;
		}
		#endregion

		#region ToString
		public override string ToString()
		{
			return base.ToString();
		}
		#endregion
		
		#region Equality
		public override bool Equals(object obj)
		{
			if (obj != null && obj is RendererDetail)
			{
				return this == (RendererDetail)obj;
			}

			return false;
		}

		public static bool operator ==(RendererDetail lhs, RendererDetail rhs)
		{
			return lhs.friendlyName.Equals(rhs.friendlyName) &&
				lhs.rendererId.Equals(rhs.rendererId);
		}

		public static bool operator !=(RendererDetail lhs, RendererDetail rhs)
		{
			return lhs.friendlyName.Equals(rhs.friendlyName) == false ||
				lhs.rendererId.Equals(rhs.rendererId) == false;
		}
		#endregion
	}
}
