#region Using Statements
using System;

#endregion

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Content.Pipeline.Serialization.Compiler
{
    public abstract class ContentTypeWriter
    {
        private bool canDeserializeIntoExistingObject;
        private Type targetType;

        protected ContentTypeWriter(Type targetType)
        {
            this.targetType = targetType;

            throw new NotImplementedException();
        }

        public abstract string GetRuntimeReader(TargetPlatform targetPlatform);

        public virtual string GetRuntimeType(TargetPlatform targetPlatform)
        {
            throw new NotImplementedException();
        }

        protected virtual void Initialize(ContentCompiler compiler)
        {
            throw new NotImplementedException();
        }

        protected internal virtual bool ShouldCompressContent(TargetPlatform targetPlatform, Object value)
        {
            throw new NotImplementedException();
        }

        protected internal abstract void Write(ContentWriter output, Object value);

        public virtual bool CanDeserializeIntoExistingObject 
        {
            get
            {
                return canDeserializeIntoExistingObject;
            }
        }

        public Type TargetType
        {
            get
            {
                return this.targetType;
            }
        }

        public virtual int TypeVersion
        {
            get
            {
                throw new NotImplementedException();
            }
        }
    }
}
