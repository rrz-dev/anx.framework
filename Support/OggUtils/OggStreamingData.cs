using System;
using System.IO;
using csogg;
using csvorbis;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace OggUtils
{
	public class OggStreamingData
    {
        private readonly Page page;

        public Block Block;
	    public readonly DspState DspState;
	    public readonly SyncState SyncState;
	    public readonly StreamState StreamState;
	    public readonly Packet Packet;
	    public readonly Info Info;
	    public readonly Comment Comment;
        public bool EndOfStream;
        public Stream Input { get; private set; }
        public bool EndOfPage
        {
            get { return page.eos() != 0; }
        }

		public OggStreamingData(Stream setInput)
		{
			Input = setInput;
            page = new Page();
            DspState = new DspState();
            SyncState = new SyncState();
            StreamState = new StreamState();
            Packet = new Packet();
            Info = new Info();
            Comment = new Comment();
			Block = new Block(DspState);
		}

		public bool InitSyncState()
		{
			SyncState.init();
			SyncState.buffer(4096);
			byte[] readData = SyncState.data;
			int bytes = Input.Read(readData, 0, readData.Length);
			SyncState.wrote(bytes);

			if (PageOut() == 1)
				return true;
			if (bytes < 4096)
				return false;

			throw new Exception("Input does not appear to be an Ogg bitstream.");
		}

        public int ReadSyncStateDataAt(int index)
        {
            return Input.Read(SyncState.data, index, 4096);
        }

		public void InitStreamState()
		{
			StreamState.init(page.serialno());
		}

		public int PageIn()
		{
			return StreamState.pagein(page);
		}

		public int PageOut()
		{
			return SyncState.pageout(page);
		}

		public int PacketOut()
		{
			return StreamState.packetout(Packet);
		}
	}
}
