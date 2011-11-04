﻿#region Using Statements
using System;
using System.IO;
#endregion // Using Statements

#region License AnxFramework
//
// This file is part of the ANX.Framework created by the "ANX.Framework developer group".
//
// This file is released under the Ms-PL license.
//
//
//
// Microsoft Public License (Ms-PL)
//
// This license governs use of the accompanying software. If you use the software, you accept this license. 
// If you do not accept the license, do not use the software.
//
// 1.Definitions
//   The terms "reproduce," "reproduction," "derivative works," and "distribution" have the same meaning 
//   here as under U.S. copyright law.
//   A "contribution" is the original software, or any additions or changes to the software.
//   A "contributor" is any person that distributes its contribution under this license.
//   "Licensed patents" are a contributor's patent claims that read directly on its contribution.
//
// 2.Grant of Rights
//   (A) Copyright Grant- Subject to the terms of this license, including the license conditions and limitations 
//       in section 3, each contributor grants you a non-exclusive, worldwide, royalty-free copyright license to 
//       reproduce its contribution, prepare derivative works of its contribution, and distribute its contribution
//       or any derivative works that you create.
//   (B) Patent Grant- Subject to the terms of this license, including the license conditions and limitations in 
//       section 3, each contributor grants you a non-exclusive, worldwide, royalty-free license under its licensed
//       patents to make, have made, use, sell, offer for sale, import, and/or otherwise dispose of its contribution 
//       in the software or derivative works of the contribution in the software.
//
// 3.Conditions and Limitations
//   (A) No Trademark License- This license does not grant you rights to use any contributors' name, logo, or trademarks.
//   (B) If you bring a patent claim against any contributor over patents that you claim are infringed by the software, your 
//       patent license from such contributor to the software ends automatically.
//   (C) If you distribute any portion of the software, you must retain all copyright, patent, trademark, and attribution 
//       notices that are present in the software.
//   (D) If you distribute any portion of the software in source code form, you may do so only under this license by including
//       a complete copy of this license with your distribution. If you distribute any portion of the software in compiled or 
//       object code form, you may only do so under a license that complies with this license.
//   (E) The software is licensed "as-is." You bear the risk of using it. The contributors give no express warranties, guarantees,
//       or conditions. You may have additional consumer rights under your local laws which this license cannot change. To the
//       extent permitted under your local laws, the contributors exclude the implied warranties of merchantability, fitness for a 
//       particular purpose and non-infringement.

#endregion // License

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
