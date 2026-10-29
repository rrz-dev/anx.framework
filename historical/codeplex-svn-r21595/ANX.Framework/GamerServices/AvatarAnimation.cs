#region Using Statements
using System;
using ANX.Framework.NonXNA.Development;

#endregion // Using Statements

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.GamerServices
{
    [PercentageComplete(10)]
    [Developer("Glatzemann")]
    [TestState(TestStateAttribute.TestState.Untested)]
    public class AvatarAnimation : IAvatarAnimation, IDisposable
	{
		public AvatarAnimation(AvatarAnimationPreset preset)
		{
			throw new NotImplementedException();
		}

		~AvatarAnimation()
		{
			Dispose();
		}

		public void Dispose()
		{
			throw new NotImplementedException();
		}

		protected void Dispose(bool disposing)
		{
			throw new NotImplementedException();
		}

		public System.Collections.ObjectModel.ReadOnlyCollection<Matrix> BoneTransforms
		{
			get { throw new NotImplementedException(); }
		}

		public TimeSpan CurrentPosition
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		public AvatarExpression Expression
		{
			get { throw new NotImplementedException(); }
		}

		public bool IsDisposed
		{
			get { throw new NotImplementedException(); }
		}

		public TimeSpan Length
		{
			get { throw new NotImplementedException(); }
		}

		public void Update(TimeSpan elapsedAnimationTime, bool loop)
		{
			throw new NotImplementedException();
		}
	}
}
