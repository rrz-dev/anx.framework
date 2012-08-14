using System;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.RenderSystem.Windows.Metro
{
	public abstract class BaseStateObject
	{
		#region Private
		protected bool isDirty;
		protected bool bound;
		#endregion

		#region Public
		public bool IsBound
		{
			get
			{
				return bound;
			}
		}
		#endregion

		#region Constructor
		protected BaseStateObject()
		{
			isDirty = true;
		}
		#endregion

		#region Release
		public void Release()
		{
			bound = false;
		}
		#endregion

		#region SetValueIfDifferentAndMarkDirty
		protected void SetValueIfDifferentAndMarkDirty<T>(
			ref T oldValue, ref T newValue)
		{
			if (oldValue.Equals(newValue) == false)
			{
				isDirty = true;
				oldValue = newValue;
			}
		}
		#endregion
	}
}
