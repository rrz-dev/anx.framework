using System;
using System.Runtime.Serialization;

namespace ANX.Framework.Net
{
	public class NetworkException : Exception
	{
		public NetworkException()
		{
		}

		public NetworkException(string message)
			: base(message)
		{
		}

		public NetworkException(string message, Exception innerException)
			: base(message, innerException)
		{
		}
	}
}
