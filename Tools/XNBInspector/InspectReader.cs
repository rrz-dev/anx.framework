using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ANX.Tools.XNBInspector
{
    public class InspectReader : BinaryReader
    {
        private InspectReader(Stream input)
            : base(input)
        {
        }

        public static string TryInspectXNB(Stream input)
        {
            StringBuilder result = new StringBuilder();
            try
            {
                InspectXNB(input, result);
            }
            catch (Exception e)
            {
                result.AppendLine();
                result.AppendLine(e.Message);
                result.AppendLine(e.StackTrace);
            }
            return result.ToString();
        }

        public static void InspectXNB(Stream input, StringBuilder result)
        {
            // read the XNB file information
            //
            // | Type   |   Description        | example/value
            // |--------|----------------------|--------------------------------
            // | Byte   | Format identifier    | X (88)
            // |--------|----------------------|--------------------------------
            // | Byte   | Format identifier    | N (78)
            // |--------|----------------------|--------------------------------
            // | Byte   | Format identifier    | B (66)
            // |--------|----------------------|--------------------------------
            // | Byte   | Target platform	   | w = Microsoft Windows
            // |        |                      | m = Windows Phone 7
            // |        |                      | x = Xbox 360
            // |--------|----------------------|--------------------------------
            // | Byte   | XNB format version   | 5 = XNA Game Studio 4.0
            // |--------|----------------------|--------------------------------
            // | Byte   | Flag bits            | Bit 0x01 = content is for HiDef profile (otherwise Reach)
            // |        |                      | Bit 0x80 = asset data is compressed
            // |--------|----------------------|--------------------------------
            // | UInt32 | Compressed file size | Total size of the (optionally compressed) 
            // |        |                      | .xnb file as stored on disk (including this header block)

            InspectReader reader = new InspectReader(input);

            byte magicX = reader.ReadByte(); // X
            byte magicN = reader.ReadByte(); // N
            byte magicB = reader.ReadByte(); // B

            result.AppendFormat("Format identifier: {0}\n", (char)magicX);
            result.AppendFormat("Format identifier: {0}\n", (char)magicN);
            result.AppendFormat("Format identifier: {0}\n", (char)magicB);

            byte targetPlattform = reader.ReadByte();
            // w = Microsoft Windows
            // m = Windows Phone 7
            // x = Xbox 360
            result.AppendFormat("Target platform  : {0} ", (char)targetPlattform);
            switch ((char)targetPlattform)
            {
                case 'w':
                    result.Append("(Microsoft Windows)");
                    break;
                case 'm':
                    result.Append("(Windows Phone 7)");
                    break;
                case 'x':
                    result.Append("(Xbox 360)");
                    break;
                default:
                    result.Append("(Unknown)");
                    break;
            }
            result.AppendLine();

            byte formatVersion = reader.ReadByte();
            // 5 = XNA Game Studio 4.0
            result.AppendFormat("Format version   : {0} ", formatVersion);
            switch (formatVersion)
            {
                case 5:
                    result.Append("(XNA Game Studio 4.0)");
                    break;
                default:
                    result.Append("(Unknown)");
                    break;
            }
            result.AppendLine();

            byte flags = reader.ReadByte();
            result.AppendFormat("Flags            : 0x{0:X4}\n", flags);
            if ((flags & 0x01) == 0x01)
            {
                // HiDef Profile
                result.AppendLine(" - HiDef Profile");
            }
            else
            {
                // Reach Profile
                result.AppendLine(" - Reach Profile");
            }

            bool isCompressed = (flags & 0x80) != 0;
            result.AppendFormat(" - Compressed {0}", isCompressed);
            result.AppendLine();

            int sizeOnDisk = reader.ReadInt32();
            result.AppendFormat("Size on disk     : {0,10} ({1,10} bytes)", ToHumanSize(sizeOnDisk), sizeOnDisk);
            result.AppendLine();

            long position = reader.BaseStream.Position;
            int sizeOfdata = reader.ReadInt32();
            reader.BaseStream.Seek(position, SeekOrigin.Begin);

            result.AppendFormat("Uncompressed     : {0,10} ({1,10} bytes)", ToHumanSize(sizeOfdata), sizeOfdata);
            result.AppendLine();

            if (isCompressed)
            {
                input = ANX.Framework.Content.Decompressor.DecompressStream(reader, input, sizeOnDisk);
                reader = new InspectReader(input);
            }

            int numTypes = reader.Read7BitEncodedInt();
            result.AppendFormat("Type readers     : {0}", numTypes);
            result.AppendLine();

            for (int i = 0; i < numTypes; i++)
            {
                string readerTypeName = reader.ReadString();
                int readerVersionNumber = reader.ReadInt32();
                result.AppendFormat(" - Version: {1}    Type: {2}", i, readerVersionNumber, readerTypeName);
                result.AppendLine();
            }

            int numSharedResources = reader.Read7BitEncodedInt();
            result.AppendFormat("Shared resources : {0}", numSharedResources);
            result.AppendLine();
        }

        private static string ToHumanSize(long bytes)
        {
            double s = bytes;
            string[] format = new string[]
                  {
                      "{0:0.00} bytes", 
                      "{0:0.00} KB",  
                      "{0:0.00} MB", 
                      "{0:0.00} GB", 
                      "{0:0.00} TB", 
                      "{0:0.00} PB", 
                      "{0:0.00} EB"
                  };

            int i = 0;

            while (i < format.Length && s >= 1024)
            {
                s = (long)(100 * s / 1024.0) / 100.0;
                i++;
            }
            return string.Format(format[i], s);
        }
    }
}
