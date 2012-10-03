using System;
using System.IO;
using OggUtils;
using SharpDX;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.SoundSystem.Windows.XAudio
{
    internal class XAudioOggInputStream : OggInputStream
    {
        public const int BufferLength = 4096 * 8;

        public XAudioOggInputStream(Stream input)
            : base(input)
        {
        }

        public int Read(DataStream buffer)
        {
            if (StreamData.EndOfStream)
                return 0;

            int length = BufferLength;
            int readOffset = 0;
            while (length > 0)
            {
                if (FillConversionBufferIfNeeded() == false)
                    break;

                int convertedBytesAvailable = ConversionBufferSize - ConversionBufferOffset;
                int bytesToCopy = Math.Min(length, convertedBytesAvailable);
                buffer.WriteRange(ConversionBuffer, ConversionBufferOffset, bytesToCopy);
                ConversionBufferOffset += bytesToCopy;
                length -= bytesToCopy;
                readOffset += bytesToCopy;
            }

            return readOffset;
        }
    }
}
