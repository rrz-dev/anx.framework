#region Using Statements
using System.IO;
using ANX.Framework.NonXNA.Development;
#endregion // Using Statements

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

#region License MonoXna
/*
MIT License
Copyright © 2011 The MonoXNA Team

All rights reserved.

Authors:
 * Lars Magnusson <lavima@gmail.com>

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
*/
#endregion License

namespace ANX.Framework.Content
{
    [Developer("GinieDP")]
    internal static class Decompressor
    {
        public static Stream DecompressStream(BinaryReader reader, Stream input, int num)
        {
            // ... let's decompress it!
            // get the decompressed size (num is our compressed size)
            int decompressedSize = reader.ReadInt32();
            // create a memory stream of that size
            MemoryStream output = new MemoryStream(decompressedSize);

            // save our initial position
            long pos = input.Position;
            // default window size for XNB encoded files is 64Kb (need 16 bits to represent it)
            LzxDecoder decoder = new LzxDecoder(16);
            // start our decode process
            while (pos < num)
            {
                // the compressed stream is seperated into blocks that will decompress
                // into 32Kb or some other size if specified.
                // normal, 32Kb output blocks will have a short indicating the size
                // of the block before the block starts
                // blocks that have a defined output will be preceded by a byte of value
                // 0xFF (255), then a short indicating the output size and another
                // for the block size
                // all shorts for these cases are encoded in big endian order
                int hi, lo, block_size, frame_size;
                // let's get the first byte
                hi = reader.ReadByte();
                // does this block define a frame size?
                if (hi == 0xFF)
                {
                    // get our bytes
                    hi = reader.ReadByte();
                    lo = reader.ReadByte();
                    // make a beautiful short baby together
                    frame_size = (hi << 8) | lo;
                    // let's read the block size
                    hi = reader.ReadByte();
                    lo = reader.ReadByte();
                    block_size = (hi << 8) | lo;
                    // add the read in bytes to the position
                    pos += 5;
                }
                else
                {
                    // just block size, so let's read the rest
                    lo = reader.ReadByte();
                    block_size = (hi << 8) | lo;
                    // frame size is 32Kb by default
                    frame_size = 32768;
                    // add the read in bytes to the position
                    pos += 2;
                }

                // either says there is nothing to decode
                if (block_size == 0 || frame_size == 0)
                    break;

                // let's decompress the sucker
                decoder.Decompress(input, block_size, output, frame_size);

                // let's increment the input position by the block size
                pos += block_size;
                // reset the position of the input just incase the bit buffer
                // read in some unused bytes
                input.Seek(pos, SeekOrigin.Begin);
            }

            // finished decoding everything, let's set the decompressed buffer
            // to the beginning and return that
            output.Seek(0, SeekOrigin.Begin);
            return output;
        }
    }
}
