#region Using Statements
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using ANX.Framework.Graphics;
using ANX.Framework.NonXNA.Reflection;
using ANX.Framework.NonXNA.Development;

#endregion // Using Statements

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Content
{
    [Developer("GinieDP")]
    public sealed class ContentReader : BinaryReader
    {
        private GraphicsProfile graphicsProfile;

        private List<Action<object>>[] sharedResourceFixups;

        private ContentTypeReader[] typeReaders;

        public ContentManager ContentManager { get; private set; }

        public bool AnxExtensions { get; private set; }
        public string AssetName { get; private set; }
        private string assetDirectory;

        private ContentReader(ContentManager contentManager, Stream input, string assetName, GraphicsProfile graphicsProfile, bool anxExtensions)
            : base(input)
        {
            this.AnxExtensions = anxExtensions;
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
            GraphicsProfile profile;
            bool anxExtensions;

            input = ContentReader.ReadXnbHeader(input, assetName, out profile, out anxExtensions);
            return new ContentReader(contentManager, input, assetName, profile, anxExtensions);
        }

        public static T ReadAsset<T>(ContentManager contentManager, Stream input, string assetName)
        {
            GraphicsProfile profile;
            bool anxExtensions;

            input = ContentReader.ReadXnbHeader(input, assetName, out profile, out anxExtensions);
            return new ContentReader(contentManager, input, assetName, profile, anxExtensions).ReadAsset<T>();
        }

        private static Stream ReadXnbHeader(Stream input, string assetName, out GraphicsProfile graphicsProfile, out bool anxExtensions)
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
            // |        |                      | 6 = future XNA version 
            // |        |                      |     OR anx version if the next three bytes are ANX
            // |--------|----------------------|--------------------------------
            // | Byte   | Flag bits            | Bit 0x01 = content is for HiDef profile (otherwise Reach)
            // |        |                      | Bit 0x80 = asset data is compressed
            // |--------|----------------------|--------------------------------
            // | UInt32 | Compressed file size | Total size of the (optionally compressed) 
            // |        |                      | .xnb file as stored on disk (including this header block)

            BinaryReader reader = new BinaryReader(input);
            anxExtensions = false;

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

            if (formatVersion == 6)
            {
                byte magicVA = reader.ReadByte(); // A
                byte magicVN = reader.ReadByte(); // N
                byte magicVX = reader.ReadByte(); // X

                if (magicVA != 'A' || magicVN != 'N' || magicVX != 'X')
                {
                    throw new ContentLoadException("Not an ANX.Framework version 1.0 XNB file or this is an unsupported past XNA 4.0 XNB file.");
                }

                anxExtensions = true;
            }
            else if (formatVersion != 5)
            {
                throw new ContentLoadException("Not an XNA Game Studio version 4.0 XNB file.");
            }

            byte flags = reader.ReadByte();

            graphicsProfile = ((flags & 0x01) == 0x01 ? GraphicsProfile.HiDef : GraphicsProfile.Reach);

            bool isCompressed = (flags & 0x80) != 0;

            int sizeOnDisk = reader.ReadInt32();

            if (input.CanSeek && ((sizeOnDisk - (anxExtensions ? 13 : 10)) > (input.Length - input.Position)))
            {
                throw new ContentLoadException("Bad XNB file size.");
            }

            if (isCompressed)
            {
                return Decompressor.DecompressStream(reader, input, sizeOnDisk);
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
            index -= 1;
            if (index >= this.typeReaders.Length)
            {
                throw new ContentLoadException("Bad Xnb");
            }
            ContentTypeReader reader = this.typeReaders[index];
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
            if (TypeHelper.IsValueType(typeReader.TargetType))
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
            if (TypeHelper.IsValueType(typeReader.TargetType))
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
                TypeHelper.IsValueType(existingInstance.GetType()) == false && 
                object.ReferenceEquals(existingInstance, asset) == false)
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
            // This is handeled with unsafe code in the original implementation.
            // The original implementation reads as UInt32 and does some pointer magic to copy the UInt bits into the float.
            // The same "pointer magic" is done by the ReadSingle method of the binary reader already.
            return base.ReadSingle();
        }

        public override double ReadDouble()
        {
            // This is handeled with unsafe code in the original implementation.
            // The original implementation reads as UInt64 and does some pointer magic to copy the UInt bits into the float.
            // The same "pointer magic" is done by the ReadDouble method of the binary reader already.
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

        internal string GetAbsolutePathToReference(string referenceName)
        {
            referenceName = GetPathToReference(referenceName);
            referenceName = Path.Combine(ContentManager.RootDirectory, referenceName);

            Assembly assembly = null;
            string titleLocationPath = null;

#if !WINDOWSMETRO
            assembly = Assembly.GetEntryAssembly();
            if (assembly == null)
            {
                assembly = Assembly.GetCallingAssembly();
            }

            titleLocationPath = Path.GetDirectoryName(assembly.Location);
#else
            throw new NotImplementedException();
            //TODO: find solution for metro
#endif

            referenceName = Path.Combine(titleLocationPath, referenceName);
            return TitleContainer.GetCleanPath(referenceName);
        }

        private string GetPathToReference(string referenceName)
        {
            return Path.Combine(Path.GetDirectoryName(AssetName), referenceName);
        }
    }
}
