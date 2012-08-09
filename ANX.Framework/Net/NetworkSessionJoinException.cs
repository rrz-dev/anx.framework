using System;
using System.Runtime.Serialization;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Net
{
	public class NetworkSessionJoinException : NetworkException
	{
		public NetworkSessionJoinError JoinError
		{
			get;
			set;
		}

		public NetworkSessionJoinException()
		{
		}

		public NetworkSessionJoinException(string message)
			: base(message)
		{
		}

		public NetworkSessionJoinException(string message,
			NetworkSessionJoinError setJoinError)
			: base(message)
		{
			JoinError = setJoinError;
		}

		public NetworkSessionJoinException(string message,
			Exception innerException)
			: base(message, innerException)
		{
		}

#if !WINDOWSMETRO      //TODO: search replacement for Win8
		protected NetworkSessionJoinException(SerializationInfo info,
			StreamingContext context)
			: base(info, context)
		{
			JoinError = (NetworkSessionJoinError)info.GetInt32("joinError");
		}

		public override void GetObjectData(SerializationInfo info,
			StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("joinError", (int)JoinError);
		}
#endif
    }
}
