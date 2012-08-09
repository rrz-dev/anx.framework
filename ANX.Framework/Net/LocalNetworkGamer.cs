using System;
using ANX.Framework.GamerServices;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Net
{
	public sealed class LocalNetworkGamer : NetworkGamer
	{
		public SignedInGamer SignedInGamer
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public bool IsDataAvailable
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public void EnableSendVoice(NetworkGamer remoteGamer, bool enable)
		{
			throw new NotImplementedException();
		}

		public void SendData(byte[] data, SendDataOptions options)
		{
			throw new NotImplementedException();
		}

		public void SendData(byte[] data, SendDataOptions options, NetworkGamer recipient)
		{
			throw new NotImplementedException();
		}

		public void SendData(byte[] data, int offset, int count, SendDataOptions options)
		{
			throw new NotImplementedException();
		}

		public void SendData(byte[] data, int offset, int count, SendDataOptions options,
			NetworkGamer recipient)
		{
			throw new NotImplementedException();
		}

		public void SendData(PacketWriter data, SendDataOptions options)
		{
			throw new NotImplementedException();
		}

		public void SendData(PacketWriter data, SendDataOptions options, NetworkGamer recipient)
		{
			throw new NotImplementedException();
		}

		public int ReceiveData(byte[] data, out NetworkGamer sender)
		{
			throw new NotImplementedException();
		}

		public int ReceiveData(byte[] data, int offset, out NetworkGamer sender)
		{
			throw new NotImplementedException();
		}

		public int ReceiveData(PacketReader data, out NetworkGamer sender)
		{
			throw new NotImplementedException();
		}

		public void SendPartyInvites()
		{
			throw new NotImplementedException();
		}
	}
}
