#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using ANX.Framework;
using ANX.Framework.Graphics;

#endregion // Using Statements

#region License

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

namespace ANX.Framework.Content
{
    public sealed class ContentReader : BinaryReader
    {
        private int graphicsProfile;

        private List<Action<object>>[] sharedResourceFixups;

        private ContentTypeReader[] typeReaders;

        public ContentManager ContentManager { get; private set; }

        public string AssetName { get; private set; }
        private string assetDirectory;

        private ContentReader(
            ContentManager contentManager, Stream input, string assetName, int graphicsProfile)
            : base(input)
        {
            this.ContentManager = contentManager;
            this.AssetName = assetName;
            this.graphicsProfile = graphicsProfile;

            this.assetDirectory = "";
            int separatorIndex = assetName.LastIndexOfAny(new char[] { '\\', '/' });
            if (separatorIndex >= 0)
            {
                this.assetDirectory = assetName.Substring(0, separatorIndex);
            }
        }

        public static ContentReader Create(
            ContentManager contentManager, Stream input, string assetName)
        {
            int num;
            input = ContentReader.ReadXnbHeader(input, assetName, out num);
            return new ContentReader(contentManager, input, assetName, num);
        }

        public static T ReadAsset<T>(
            ContentManager contentManager, Stream input, string assetName)
        {
            int num;
            input = ContentReader.ReadXnbHeader(input, assetName, out num);
            return new ContentReader(contentManager, input, assetName, num).ReadAsset<T>();
        }

        private static Stream ReadXnbHeader(Stream input, string assetName, out int graphicsProfile)
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

            BinaryReader reader = new BinaryReader(input);

            byte magicX = reader.ReadByte();
            byte magicN = reader.ReadByte();
            byte magicB = reader.ReadByte();

            // The first three bytes must be the characters XNB 
            if (magicX != 'X' || magicN != 'N' || magicB != 'B')
            {
                throw new ContentLoadException("Not an XNB file.");
            }

            byte targetPlattform = reader.ReadByte();

            switch ((char)targetPlattform)
            {
                case 'w':
                    // windows plattform OK
                    break;
                case 'm':
                case 'x':
                default:
                    throw new ContentLoadException("Invalid or unknown target plattform.");
            }

            byte formatVersion = reader.ReadByte();

            if (formatVersion != 5)
            {
                throw new ContentLoadException("Not an XNA Game Studio version 4.0 XNB file.");
            }

            byte flags = reader.ReadByte();

            if ((flags & 0x01) == 0x01)
            {
                // HiDef Profile
                graphicsProfile = 1;
            }
            else
            {
                // Reach Profile
                graphicsProfile = 0;
            }

            bool isCompressed = (flags & 0x80) != 0;

            uint sizeOnDisk = reader.ReadUInt32();
            // TODO: check stream length

            if (isCompressed)
            {
                int decompressedSize = reader.ReadInt32();
                return Decompressor.DecompressStream(reader, input, decompressedSize);
            }
            else
            {
                return input;
            }
        }

        private int ReadXnbResourceManifest()
        {
            int numTypes = base.Read7BitEncodedInt();

            this.typeReaders = ContentTypeReaderManager.ReadXnbTypeManifest(numTypes, this);

            int numSharedResources = base.Read7BitEncodedInt();

            if (numSharedResources > 0)
            {
                this.sharedResourceFixups = new List<Action<object>>[numSharedResources];
                for (int i = 0; i < numSharedResources; i++)
                {
                    this.sharedResourceFixups[i] = new List<Action<object>>();
                }
            }
            return numSharedResources;
        }

        internal T ReadAsset<T>()
        {
            T asset;
            try
            {
                int numSharedResources = this.ReadXnbResourceManifest();
                asset = this.ReadObject<T>();

                if (numSharedResources > 0)
                {
                    object[] sharedResources = new object[numSharedResources];
                    for (int i = 0; i < numSharedResources; i++)
                    {
                        sharedResources[i] = this.ReadObject<object>();
                    }
                    for (int j = 0; j < numSharedResources; j++)
                    {
                        foreach (Action<object> action in this.sharedResourceFixups[j])
                        {
                            action.Invoke(sharedResources[j]);
                        }
                    }
                }
            }
            catch (IOException e)
            {
                throw new ContentLoadException("Bad Xnb", e);
            }

            return asset;
        }

        public T ReadObject<T>()
        {
            int index = base.Read7BitEncodedInt();
            if (index == 0)
            {
                return default(T);
            }

            if (index > this.typeReaders.Length)
            {
                throw new ContentLoadException("Bad Xnb");
            }
            ContentTypeReader reader = this.typeReaders[index-1];
            return this.ReadWithTypeReader<T>(reader, null);
        }

        public T ReadObject<T>(T existingInstance)
        {
            int index = base.Read7BitEncodedInt();
            if (index == 0)
            {
                return default(T);
            }

            if (index > this.typeReaders.Length)
            {
                throw new ContentLoadException("Bad Xnb");
            }
            ContentTypeReader reader = this.typeReaders[index-1];
            return this.ReadWithTypeReader<T>(reader, existingInstance);
        }

        public T ReadObject<T>(ContentTypeReader typeReader)
        {
            if (typeReader == null)
            {
                throw new ArgumentNullException("typeReader");
            }
            if (typeReader.TargetType.IsValueType)
            {
                return this.ReadWithTypeReader<T>(typeReader, null);
            }
            return this.ReadObject<T>(null);
        }

        public T ReadObject<T>(ContentTypeReader typeReader, T existingInstance)
        {
            if (typeReader == null)
            {
                throw new ArgumentNullException("typeReader");
            }
            if (typeReader.TargetType.IsValueType)
            {
                return this.ReadWithTypeReader<T>(typeReader, existingInstance);
            }
            return this.ReadObject<T>(existingInstance);
        }

        public T ReadRawObject<T>()
        {
            ContentTypeReader typeReader = ContentTypeReaderManager.GetTypeReader(typeof(T), this);
            return this.ReadWithTypeReader<T>(typeReader, null);
        }

        public T ReadRawObject<T>(T existingInstance)
        {
            ContentTypeReader typeReader = ContentTypeReaderManager.GetTypeReader(typeof(T), this);
            return this.ReadWithTypeReader<T>(typeReader, existingInstance);
        }

        public T ReadRawObject<T>(ContentTypeReader typeReader)
        {
            if (typeReader == null)
            {
                throw new ArgumentNullException("typeReader");
            }
            return this.ReadWithTypeReader<T>(typeReader, null);
        }

        public T ReadRawObject<T>(ContentTypeReader typeReader, T existingInstance)
        {
            if (typeReader == null)
            {
                throw new ArgumentNullException("typeReader");
            }
            return this.ReadWithTypeReader<T>(typeReader, existingInstance);
        }

        private T ReadWithTypeReader<T>(ContentTypeReader reader, object existingInstance)
        {
            ContentTypeReader<T> contentTypeReader = reader as ContentTypeReader<T>;
            T asset;
            if (contentTypeReader != null)
            {
                existingInstance = existingInstance ?? default(T);
                asset = contentTypeReader.Read(this, (T)existingInstance);
            }
            else
            {
                object obj = reader.Read(this, existingInstance) ?? default(T);

                if (obj != null && !(obj is T))
                {
                    throw new ContentLoadException("Bad xnb, wrong type");
                }

                asset = (T)obj;
            }
            if (existingInstance != null && 
                !existingInstance.GetType().IsValueType && 
                !object.ReferenceEquals(existingInstance, asset))
            {
                throw new ContentLoadException("Reader constructed new instance");
            }

            return asset;
        }

        public void ReadSharedResource<T>(Action<T> fixup)
        {
            if (fixup == null)
            {
                throw new ArgumentNullException("fixup");
            }
            int resourceNumber = base.Read7BitEncodedInt();
            if (resourceNumber == 0)
            {
                return;
            }
            int resourceIndex = resourceNumber - 1;

            if (resourceIndex >= this.sharedResourceFixups.Length)
            {
                throw new ContentLoadException("Bad XNB");
            }

            this.sharedResourceFixups[resourceIndex].Add((value) => {
                if (value is T)
                {
                    fixup((T)value);
                }
                else
                {
                    throw new ContentLoadException("Bad XNB");
                }
            });
        }

        public T ReadExternalReference<T>()
        {
            string assetReference = base.ReadString();
            if (String.IsNullOrEmpty(assetReference))
            {
                return default(T);
            }
            string assetLocation = Path.Combine(assetDirectory, assetReference);
            return this.ContentManager.Load<T>(assetLocation);
        }

        public override float ReadSingle()
        {
            // TODO: this is handeled with unsafe code in the original implementation, dont know for what reason
            return base.ReadSingle();
        }

        public override double ReadDouble()
        {
            // TODO: this is handeled with unsafe code in the original implementation, dont know for what reason
            return base.ReadDouble();
        }

        public Color ReadColor()
        {
            var result = new Color();
            result.PackedValue = base.ReadUInt32();
            return result;
        }

        public Matrix ReadMatrix()
        {
            var result = new Matrix();
            result.M11 = this.ReadSingle();
            result.M12 = this.ReadSingle();
            result.M13 = this.ReadSingle();
            result.M14 = this.ReadSingle();
            result.M21 = this.ReadSingle();
            result.M22 = this.ReadSingle();
            result.M23 = this.ReadSingle();
            result.M24 = this.ReadSingle();
            result.M31 = this.ReadSingle();
            result.M32 = this.ReadSingle();
            result.M33 = this.ReadSingle();
            result.M34 = this.ReadSingle();
            result.M41 = this.ReadSingle();
            result.M42 = this.ReadSingle();
            result.M43 = this.ReadSingle();
            result.M44 = this.ReadSingle();
            return result;
        }

        public Vector2 ReadVector2()
        {
            var result = new Vector2();
            result.X = this.ReadSingle();
            result.Y = this.ReadSingle();
            return result;
        }

        public Vector3 ReadVector3()
        {
            var result = new Vector3();
            result.X = this.ReadSingle();
            result.Y = this.ReadSingle();
            result.Z = this.ReadSingle();
            return result;
        }

        public Vector4 ReadVector4()
        {
            var result = new Vector4();
            result.X = this.ReadSingle();
            result.Y = this.ReadSingle();
            result.Z = this.ReadSingle();
            result.W = this.ReadSingle();
            return result;
        }

        public Quaternion ReadQuaternion()
        {
            var result = new Quaternion();
            result.X = this.ReadSingle();
            result.Y = this.ReadSingle();
            result.Z = this.ReadSingle();
            result.W = this.ReadSingle();
            return result;
        }

        internal Graphics.GraphicsDevice ResolveGraphicsDevice()
        {
            var service = this.ContentManager.ServiceProvider.GetService(typeof(IGraphicsDeviceService)) as IGraphicsDeviceService;
            if (service == null)
            {
                throw new ContentLoadException("Service not found: IGraphicsDeviceService");
            }
            var device = service.GraphicsDevice;
            if (device == null)
            {
                throw new ContentLoadException("Graphics device missing");
            }
            return device;
        }
    }
}
