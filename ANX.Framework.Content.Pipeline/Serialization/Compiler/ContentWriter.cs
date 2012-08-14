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

        //TODO: implement -> ExternalReference<T> needed first
        //public void WriteExternalReference<T>(ExternalReference<T> reference)
        //{

        //}

        public void WriteObject<T>(T value)
        {
            throw new NotImplementedException();
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
                return TargetProfile;
            }
        }
    }
}
