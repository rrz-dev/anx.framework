#region Using Statements
using System;

#endregion // Using Statements

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.GamerServices
{
	public class AvatarDescription
	{
		public bool IsValid
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public byte[] Description
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public float Height
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public AvatarBodyType BodyType
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public event EventHandler<EventArgs> Changed;

		public AvatarDescription(byte[] data)
		{
			throw new NotImplementedException();
		}

		public static IAsyncResult BeginGetFromGamer(Gamer gamer, AsyncCallback callback,
			object state)
		{
			throw new NotImplementedException();
		}

		public static AvatarDescription EndGetFromGamer(IAsyncResult result)
		{
			throw new NotImplementedException();
		}

		public static AvatarDescription CreateRandom()
		{
			throw new NotImplementedException();
		}

		public static AvatarDescription CreateRandom(AvatarBodyType bodyType)
		{
			throw new NotImplementedException();
		}
	}
}
