#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using ANX.Framework.Graphics;

#endregion

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Content.Pipeline.Serialization.Compiler
{
    public sealed class ContentWriter : BinaryWriter
    {
        private TargetPlatform targetPlatform;
        private GraphicsProfile targetProfile;
        private ContentCompiler compiler;
        private MemoryStream headerData;
        private MemoryStream contentData;
        private Stream finalOutput;
        private Boolean compressContent;
        private string rootDirectory;
        private string referenceRelocationPath;

        const byte xnbFormatVersion = (byte)5;
        char[] xnbMagicWord = new char[] { 'X', 'N', 'B' };

        internal ContentWriter(ContentCompiler compiler, Stream output, TargetPlatform targetPlatform, GraphicsProfile targetProfile, bool compressContent, string rootDirectory, string referenceRelocationPath)
        {
            this.compiler = compiler;
            this.targetPlatform = targetPlatform;
            this.targetProfile = targetProfile;
            this.compressContent = compressContent;
            this.rootDirectory = rootDirectory;
            this.referenceRelocationPath = referenceRelocationPath;
            this.finalOutput = output;
            this.headerData = new MemoryStream();
            this.contentData = new MemoryStream();
            this.OutStream = this.contentData;
        }

        public void Write(Color value)
        {
            base.Write(value.PackedValue);
        }

        public void Write(Matrix value)
        {
            base.Write(value.M11);
            base.Write(value.M12);
            base.Write(value.M13);
            base.Write(value.M14);
            base.Write(value.M21);
            base.Write(value.M22);
            base.Write(value.M23);
            base.Write(value.M24);
            base.Write(value.M31);
            base.Write(value.M32);
            base.Write(value.M33);
            base.Write(value.M34);
            base.Write(value.M41);
            base.Write(value.M42);
            base.Write(value.M43);
            base.Write(value.M44);
        }

        public void Write(Quaternion value)
        {
            base.Write(value.X);
            base.Write(value.Y);
            base.Write(value.Z);
            base.Write(value.W);
        }

        public void Write(Vector2 value)
        {
            base.Write(value.X);
            base.Write(value.Y);
        }

        public void Write(Vector3 value)
        {
            base.Write(value.X);
            base.Write(value.Y);
            base.Write(value.Z);
        }

        public void Write(Vector4 value)
        {
            base.Write(value.X);
            base.Write(value.Y);
            base.Write(value.Z);
            base.Write(value.W);
        }

        public void WriteExternalReference<T>(ExternalReference<T> reference)
        {
            throw new NotImplementedException();
        }

        public void WriteObject<T>(T value)
        {
            if (value == null)
            {
                base.Write7BitEncodedInt(0);
                return;
            }

            ContentTypeWriter typeWriter = this.compiler.GetTypeWriter(value.GetType());

            base.Write7BitEncodedInt(1);
            //if (this.recurseDetector.ContainsKey(value))
            //{
            //    throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, Resources.FoundCyclicReference, new object[]
            //    {
            //        value
            //    }));
            //}
            //this.recurseDetector.Add(value, true);
            this.InvokeWriter<T>(value, typeWriter);
            //this.recurseDetector.Remove(value);
        }

        private void InvokeWriter<T>(T value, ContentTypeWriter writer)
        {
            ContentTypeWriter<T> contentTypeWriter = writer as ContentTypeWriter<T>;
            if (contentTypeWriter != null)
            {
                contentTypeWriter.Write(this, value);
                return;
            }
            writer.Write(this, value);
        }

        public void WriteObject<T>(T value, ContentTypeWriter typeWriter)
        {
            throw new NotImplementedException();
        }

        public void WriteRawObject<T>(T value)
        {
            throw new NotImplementedException();
        }

        public void WriteRawObject<T>(T value, ContentTypeWriter typeWriter)
        {
            throw new NotImplementedException();
        }

        public void WriteSharedResource<T>(T value)
        {
            throw new NotImplementedException();
        }

        internal void FlushOutput()
        {
            //TODO: implement

            //this.WriteSharedResources();
            this.WriteHeader();
            this.WriteFinalOutput();
        }

        private void WriteHeader()
        {
            this.OutStream = this.headerData;
            //TODO: implement
            //base.Write7BitEncodedInt(this.typeWriters.Count);
            //foreach (ContentTypeWriter current in this.typeWriters)
            //{
            //    this.Write(current.GetRuntimeReader(this.targetPlatform));
            //    this.Write(current.TypeVersion);
            //}
            //base.Write7BitEncodedInt(this.sharedResourceNames.Count);
        }

        private void WriteFinalOutput()
        {
            this.OutStream = this.finalOutput;
            this.Write(xnbMagicWord);
            this.Write((byte)this.targetPlatform);
            if (this.compressContent)
            {
                throw new NotImplementedException();
                //this.WriteCompressedOutput();
            }
            else
            {
                this.WriteUncompressedOutput();
            }
        }

        private void WriteUncompressedOutput()
        {
            this.Write(xnbFormatVersion);    // Version

            byte flags = 0;
            if (TargetProfile == GraphicsProfile.HiDef)
            {
                flags |= 0x01;
            }
            this.Write(flags);

            this.Write(10 + this.headerData.Length + this.contentData.Length);
            this.OutStream.Write(this.headerData.GetBuffer(), 0, (int)this.headerData.Length);
            this.OutStream.Write(this.contentData.GetBuffer(), 0, (int)this.contentData.Length);
        }

        public TargetPlatform TargetPlatform
        {
            get
            {
                return targetPlatform;
            }
        }

        public GraphicsProfile TargetProfile
        {
            get
            {
                return targetProfile;
            }
        }
    }
}
