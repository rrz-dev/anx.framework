#region Using Statements
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

#endregion // Using Statements

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.GamerServices
{
	public class AvatarRenderer : IDisposable
	{
		public const int BoneCount = 71;

		public bool IsDisposed
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public Vector3 AmbientLightColor
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

		public Vector3 LightColor
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

		public Vector3 LightDirection
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

		public AvatarRendererState State
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public Matrix World
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

		public Matrix View
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

		public Matrix Projection
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

		public ReadOnlyCollection<int> ParentBones
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public ReadOnlyCollection<Matrix> BindPose
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public AvatarRenderer(AvatarDescription avatarDescription)
		{
			throw new NotImplementedException();
		}

		public AvatarRenderer(AvatarDescription avatarDescription, bool useLoadingEffect)
		{
			throw new NotImplementedException();
		}

		~AvatarRenderer()
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

		public void Draw(IAvatarAnimation animation)
		{
			throw new NotImplementedException();
		}

		public void Draw(IList<Matrix> bones, AvatarExpression expression)
		{
			throw new NotImplementedException();
		}
	}
}
