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

        public static string TryInspectXNB(Stream input, IInspectLogger result)
        {
            try
            {
                InspectXNB(input, result);
            }
            catch (Exception e)
            {
                result.AppendLine();
                result.AppendLine(Severity.Error, e.Message);
                result.AppendLine(Severity.Error, e.StackTrace);
            }
            return result.ToString();
        }

        public static void InspectXNB(Stream input, IInspectLogger result)
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
            byte[] magicBytes = new byte[] { magicX, magicN, magicB };
            byte[] magicWants = new byte[] { 88, 78, 66 };

            for (int i = 0; i < magicBytes.Length; i++)
            {
                result.Append(Severity.None, "Format identifier: ");
                if (magicBytes[i] != magicWants[i])
                {
                    result.AppendFormat(Severity.Error, "{0} (should be {1}[{2}])", magicBytes[i], magicWants[i], (char)magicWants[i]);
                }
                else
                {
                    result.AppendFormat(Severity.Success, "{0}[{1}]", magicWants[i], (char)magicWants[i]);
                }
                result.AppendLine();
            }
            
            byte targetPlattform = reader.ReadByte();
            // w = Microsoft Windows
            // m = Windows Phone 7
            // x = Xbox 360
            // ANX-EXTENSIONS:
            // a = Android
            // i = iOS
            // l = Linux
            // o = MacOs
            // p = PS Vita / Mobile
            // 8 = Windows 8 Metro
            result.Append(Severity.None, "Target platform  : ");
            switch ((char)targetPlattform)
            {
                case 'w':
                    result.AppendFormat(Severity.Success, "{0}[w] (Microsoft Windows)", targetPlattform);
                    break;
                case 'm':
                    result.AppendFormat(Severity.Success, "{0}[m] (Windows Phone 7)", targetPlattform);
                    break;
                case 'x':
                    result.AppendFormat(Severity.Success, "{0}[x] (Xbox 360)", targetPlattform);
                    break;
                case 'a':
                    result.AppendFormat(Severity.Success, "{0}[a] (Android) | ANX extension", targetPlattform);
                    break;
                case 'i':
                    result.AppendFormat(Severity.Success, "{0}[i] (iOS) | ANX extension", targetPlattform);
                    break;
                case 'l':
                    result.AppendFormat(Severity.Success, "{0}[l] (Linux) | ANX extension", targetPlattform);
                    break;
                case 'o':
                    result.AppendFormat(Severity.Success, "{0}[o] (MacOS) | ANX extension", targetPlattform);
                    break;
                case 'p':
                    result.AppendFormat(Severity.Success, "{0}[p] (PS Vita / mobile) | ANX extension", targetPlattform);
                    break;
                case '8':
                    result.AppendFormat(Severity.Success, "{0}[8] (Windows 8 metro) | ANX extension", targetPlattform);
                    break;
                default:
                    result.AppendFormat(Severity.Error, "{0} (Unknown or non XNA/ANX platform)", targetPlattform);
                    break;
            }
            result.AppendLine();

            byte formatVersion = reader.ReadByte();
            // 5 = XNA Game Studio 4.0
            result.Append(Severity.None, "Format version   : ");
            switch (formatVersion)
            {
                case 1:
                    result.Append(Severity.Success, "1 (XNA Game Studio 1.0)");
                    break;
                case 2:
                    result.Append(Severity.Success, "2 (XNA Game Studio 2.0)");
                    break;
                case 3:
                    result.Append(Severity.Success, "3 (XNA Game Studio 3.0)");
                    break;
                case 4:
                    result.Append(Severity.Success, "4 (XNA Game Studio 3.1)");
                    break;
                case 5:
                    result.Append(Severity.Success, "5 (XNA Game Studio 4.0)");
                    break;
                default:
                    result.AppendFormat(Severity.Warning, "{0} (Unknown or non XNA content)", formatVersion);
                    break;
            }
            result.AppendLine();

            byte flags = reader.ReadByte();
            result.AppendFormat(Severity.None, "Flags            : 0x{0:X4}\n", flags);
            if ((flags & 0x01) == 0x01)
            {
                // HiDef Profile
                result.AppendLine(Severity.None, " - HiDef Profile");
            }
            else
            {
                // Reach Profile
                result.AppendLine(Severity.None, " - Reach Profile");
            }

            bool isCompressed = (flags & 0x80) != 0;
            result.AppendFormat(Severity.None, " - Compressed {0}", isCompressed);
            result.AppendLine();

            int sizeOnDisk = reader.ReadInt32();
            result.Append(Severity.None, "Size on disk     : ");
            if (sizeOnDisk != input.Length)
            {
                result.AppendFormat(Severity.Error, "{0} bytes [{1}]", sizeOnDisk, ToHumanSize(sizeOnDisk));
                result.AppendFormat(Severity.Error, " (Should be {0} bytes [{1}])", input.Length, ToHumanSize(input.Length));
            }
            else
            {
                result.AppendFormat(Severity.Success, "{0} bytes [{1}]", sizeOnDisk, ToHumanSize(sizeOnDisk));
            }
            result.AppendLine();

            if (isCompressed)
            {
                long position = reader.BaseStream.Position;
                int sizeOfdata = reader.ReadInt32();
                reader.BaseStream.Seek(position, SeekOrigin.Begin);

                result.AppendFormat(Severity.None, "Uncompressed     : {0} bytes [{1}]", sizeOfdata, ToHumanSize(sizeOfdata));
                result.AppendLine();

                input = ANX.Framework.Content.Decompressor.DecompressStream(reader, input, sizeOnDisk);
                reader = new InspectReader(input);
            }

            int numTypes = reader.Read7BitEncodedInt();
            result.AppendFormat(Severity.None, "Type readers     : {0}", numTypes);
            result.AppendLine();

            for (int i = 0; i < numTypes; i++)
            {
                string readerTypeName = reader.ReadString();
                int readerVersionNumber = reader.ReadInt32();
                result.AppendFormat(Severity.None, " - Version: {1}    Type: {2}", i, readerVersionNumber, readerTypeName);
                result.AppendLine();
            }

            int numSharedResources = reader.Read7BitEncodedInt();
            result.AppendFormat(Severity.None, "Shared resources : {0}", numSharedResources);
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
