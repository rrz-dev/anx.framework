using System;
using System.IO;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace OggUtils
{
	public class OggInputStream
    {
        protected readonly OggStreamingData StreamData;
        private const int DefaultConvsize = 4096 * 2;
        private readonly int[] indexCache;
        private static int convsizePerChannel = DefaultConvsize;
        protected int ConversionBufferOffset;
        protected int ConversionBufferSize;
        protected static readonly byte[] ConversionBuffer = new byte[DefaultConvsize];
        private readonly float[][][] pcmCache = new float[1][][];

        public int SampleRate
        {
            get { return StreamData.Info.rate; }
        }

        public int Channels
        {
            get { return StreamData.Info.channels; }
        }

		public OggInputStream(Stream input)
		{
			StreamData = new OggStreamingData(input);
			try
			{
                InitVorbis();
                convsizePerChannel = DefaultConvsize / StreamData.Info.channels;
                indexCache = new int[StreamData.Info.channels];
			}
			catch
			{
				StreamData.EndOfStream = true;
			}
		}

        public int Read(byte[] buffer)
        {
            if (StreamData.EndOfStream)
                return 0;

            int length = buffer.Length;
            int readOffset = 0;
            while (length > 0)
            {
                if (FillConversionBufferIfNeeded() == false)
                    break;

                int convertedBytesAvailable = ConversionBufferSize - ConversionBufferOffset;
                int bytesToCopy = Math.Min(length, convertedBytesAvailable);
                Array.Copy(ConversionBuffer, ConversionBufferOffset, buffer, readOffset, bytesToCopy);
                ConversionBufferOffset += bytesToCopy;
                length -= bytesToCopy;
                readOffset += bytesToCopy;
            }

            return readOffset;
        }

	    private void InitVorbis()
		{
			if (StreamData.InitSyncState() == false)
				return;

			StreamData.InitStreamState();
			StreamData.Info.init();

			if (StreamData.PageIn() < 0)
				throw new Exception("Error reading first page of Ogg bitstream data.");
			if (StreamData.PacketOut() != 1)
				throw new Exception("Error reading initial header packet.");
			if (StreamData.Info.synthesis_headerin(StreamData.Comment, StreamData.Packet) < 0)
				throw new Exception("This Ogg bitstream does not contain Vorbis audio data.");

            int headerIndex = 0;
            while (headerIndex < 2)
            {
                while (headerIndex < 2)
                {
                    int result = StreamData.PageOut();
                    if (result == 0)
                        break;
                    if (result != 1)
                        continue;

                    StreamData.PageIn();
                    while (headerIndex < 2)
                    {
                        result = StreamData.PacketOut();
                        if (result == 0)
                            break;
                        if (result == -1)
                            throw new Exception("Corrupt secondary header. Exiting.");

                        StreamData.Info.synthesis_headerin(StreamData.Comment, StreamData.Packet);
                        headerIndex++;
                    }
                }

                int index = StreamData.SyncState.buffer(4096);
                int bytes = Math.Max(0, StreamData.ReadSyncStateDataAt(index));
                if (bytes == 0 && headerIndex < 2)
                    throw new Exception("End of file before finding all Vorbis headers!");

                StreamData.SyncState.wrote(bytes);
            }

			StreamData.DspState.synthesis_init(StreamData.Info);
			StreamData.Block.init(StreamData.DspState);
        }

        protected bool FillConversionBufferIfNeeded()
        {
            if (ConversionBufferOffset < ConversionBufferSize)
                return true;

            ConversionBufferSize = GetNextPacket();
            ConversionBufferOffset = 0;
            if (ConversionBufferSize == -1)
            {
                StreamData.EndOfStream = true;
                return false;
            }

            ConversionBufferSize = DecodePacket();
            return true;
        }

        private int GetNextPacket()
        {
            bool fetchedPacket = false;
            while (StreamData.EndOfStream == false && fetchedPacket == false)
            {
                int result1 = StreamData.PacketOut();
                if (result1 == 0)
                {
                    int result2 = 0;
                    while (StreamData.EndOfStream == false && result2 == 0)
                    {
                        result2 = StreamData.PageOut();
                        if (result2 == 0)
                            FetchData();
                    }

                    if (result2 == 0 && StreamData.EndOfPage)
                        return -1;

                    if (result2 == 0)
                        FetchData();
                    else if (result2 == -1)
                        return -1;
                    else
                        StreamData.PageIn();
                }
                else if (result1 == -1)
                    return -1;
                else
                    fetchedPacket = true;
            }

            return 0;
        }

        private void FetchData()
        {
            if (StreamData.EndOfStream)
                return;

            int index = StreamData.SyncState.buffer(4096);
            if (index >= 0)
            {
                int bytesRead = StreamData.ReadSyncStateDataAt(index);
                StreamData.SyncState.wrote(bytesRead);
                if (bytesRead > 0)
                    return;
            }

            StreamData.EndOfStream = true;
        }

        private int DecodePacket()
        {
            if (StreamData.Block.synthesis(StreamData.Packet) == 0)
                StreamData.DspState.synthesis_blockin(StreamData.Block);

            int conversionOffset = 0;
            int samplesToProcess;
            while ((samplesToProcess = StreamData.DspState.synthesis_pcmout(pcmCache, indexCache)) > 0)
            {
                float[][] pcmDataPerChannel = pcmCache[0];
                int bout = Math.Min(samplesToProcess, convsizePerChannel);

                for (int channelIndex = 0; channelIndex < StreamData.Info.channels; channelIndex++)
                {
                    int ptr = (channelIndex << 1) + conversionOffset;
                    int mono = indexCache[channelIndex];

                    for (int sampleOffset = 0; sampleOffset < bout; sampleOffset++)
                    {
                        int pcmSample = ConvertPcmData(pcmDataPerChannel[channelIndex][mono + sampleOffset]);
                        ConversionBuffer[ptr + 0] = (byte)pcmSample;
                        ConversionBuffer[ptr + 1] = (byte)(int)((uint)pcmSample >> 8);
                        ptr += StreamData.Info.channels << 1;
                    }
                }

                conversionOffset += 2 * StreamData.Info.channels * bout;
                StreamData.DspState.synthesis_read(bout);
            }

            return conversionOffset;
        }

        private static int ConvertPcmData(float sourceSample)
        {
            sourceSample = Math.Max(-1f, Math.Min(1f, sourceSample));
            int pcmSampleResult = (int)(sourceSample * 32767);
            return pcmSampleResult | (pcmSampleResult < 0 ? 0x8000 : 0);
        }
	}
}
