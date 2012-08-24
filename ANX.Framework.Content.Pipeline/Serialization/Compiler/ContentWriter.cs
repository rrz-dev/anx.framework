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
        #region Private Members
        private ContentCompiler compiler;
        private Boolean compressContent;
        private string rootDirectory;
        private string referenceRelocationPath;
        private Dictionary<Type, int> typeTable = new Dictionary<Type, int>();
        private List<ContentTypeWriter> typeWriters = new List<ContentTypeWriter>();

        private Stream outputStream;
        private MemoryStream header = new MemoryStream();
        private MemoryStream content = new MemoryStream();

        #endregion

        const byte xnbFormatVersion = (byte)5;
        char[] xnbMagicWord = new char[] { 'X', 'N', 'B' };

        internal ContentWriter(ContentCompiler compiler, Stream output, bool compressContent, string rootDirectory, string referenceRelocationPath)
        {
            this.compiler = compiler;
            this.compressContent = compressContent;
            this.rootDirectory = rootDirectory;
            this.referenceRelocationPath = referenceRelocationPath;

            this.outputStream = output;
            this.OutStream = content;
        }

        #region Write value types
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

        #endregion

        public void WriteExternalReference<T>(ExternalReference<T> reference)
        {
            if (reference == null || String.IsNullOrEmpty(reference.Filename))
            {
                Write(String.Empty);
                return;
            }

            Write(Path.GetFileNameWithoutExtension(reference.Filename));
        }

        public void WriteObject<T>(T value)
        {
            if (value == null)
            {
                base.Write7BitEncodedInt(0);
                return;
            }

            int typeIndex;
            ContentTypeWriter typeWriter = this.GetTypeWriter(value.GetType(), out typeIndex);
            base.Write7BitEncodedInt(typeIndex + 1);

            //TODO: test for recursive cyclic calls
            this.InvokeWriter<T>(value, typeWriter);
        }

        public void WriteObject<T>(T value, ContentTypeWriter typeWriter)
        {
            if (value == null)
            {
                base.Write7BitEncodedInt(0);
                return;
            }

            if (typeWriter.TargetIsValueType)
            {
                this.InvokeWriter<T>(value, typeWriter);
                return;
            }

            this.WriteObject<T>(value);            
        }

        public void WriteRawObject<T>(T value)
        {
            if (value == null)
            {
                throw new ArgumentNullException("value");
            }

            int typeIndex;
            ContentTypeWriter typeWriter = this.GetTypeWriter(typeof(T), out typeIndex);
            this.InvokeWriter<T>(value, typeWriter);
        }

        public void WriteRawObject<T>(T value, ContentTypeWriter typeWriter)
        {
            if (value == null)
            {
                throw new ArgumentNullException("value");
            }
            if (typeWriter == null)
            {
                throw new ArgumentNullException("typeWriter");
            }
            this.InvokeWriter<T>(value, typeWriter);
        }

        public void WriteSharedResource<T>(T value)
        {
            throw new NotImplementedException();
        }

        public TargetPlatform TargetPlatform
        {
            get;
            set;
        }

        public GraphicsProfile TargetProfile
        {
            get;
            set;
        }

        protected override void Dispose(bool disposing)
        {
            WriteData();

            base.Dispose(disposing);
        }

        private void WriteData()
        {
            OutStream = header;
            Write7BitEncodedInt(this.typeWriters.Count);
            foreach (ContentTypeWriter current in this.typeWriters)
            {
                Write(current.GetRuntimeReader(TargetPlatform));
                Write(current.TypeVersion);
            }
            Write7BitEncodedInt((int)0); // number of shared resources
            //TODO: write shared resources


            OutStream = outputStream;

            Write(xnbMagicWord);                                                       // magic bytes for file recognition     -   03 bytes
            Write((byte)TargetPlatform);                                               // target platform of content file      -   01 byte
            Write((byte)xnbFormatVersion);                                             // version of this file                 -   01 byte
            Write((byte)(TargetProfile == GraphicsProfile.HiDef ? 0x01 : 0x00));       // flags                                -   01 byte
            Write((int)header.Length + (int)content.Length + 10);                      // size of file                         -   04 byte
            if (compressContent)
            {
                //TODO: write compressed size
            }

            OutStream.Write(header.GetBuffer(), 0, (int)header.Length);
            OutStream.Write(content.GetBuffer(), 0, (int)content.Length);   //TODO: write compressed stream if compressedContent is true
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

        private ContentTypeWriter GetTypeWriter(Type type, out int typeIndex)
        {
            if (this.typeTable.TryGetValue(type, out typeIndex))
            {
                return this.typeWriters[typeIndex];
            }

            IEnumerable<Type> enumerable = null;
            ContentTypeWriter typeWriter = this.compiler.GetTypeWriter(type); //TODO:, out enumerable);
            typeIndex = this.typeWriters.Count;
            this.typeWriters.Add(typeWriter);
            this.typeTable.Add(type, typeIndex);

            //TODO: what is this for?
            //foreach (Type current in enumerable)
            //{
            //    if (!(current == typeof(object)))
            //    {
            //        int num;
            //        this.GetTypeWriter(current, out num);
            //    }
            //}

            return typeWriter;
        }
    }
}
