#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

#endregion // Using Statements

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Content
{
    public abstract class ContentTypeReader
    {
        /// <summary>
        /// Gets the type handeled by this reader
        /// </summary>
        public Type TargetType { get; private set; }

        /// <summary>
        /// Gets a format version number for this type
        /// </summary>
        public virtual int TypeVersion 
        { 
            get { return 0; } 
        }

        /// <summary>
        /// Gets a value indicating whether deserialization into an existion object is possible
        /// </summary>
        public virtual bool CanDeserializeIntoExistingObject
        {
            get { return false; }
        }

        /// <summary>
        /// Creates a new instance of <see cref="ContentTypeReader"/>
        /// </summary>
        /// <param name="targetType">The target type that is handeled by this reader.</param>
        protected ContentTypeReader(Type targetType)
        {
            if (targetType == null)
            {
                throw new ArgumentNullException("targetType");
            }
            this.TargetType = targetType;
        }

        protected internal virtual void Initialize(ContentTypeReaderManager manager)
        {
        }

        protected internal abstract object Read(ContentReader input, object existingInstance);
    }

    public abstract class ContentTypeReader<T> : ContentTypeReader
    {
        /// <summary>
        /// Creates a new instance of <see cref="ContentTypeReader"/>
        /// </summary>
        protected ContentTypeReader()
            : base(typeof(T))
        {
        }

        protected internal override object Read(ContentReader input, object existingInstance)
        {
            if (existingInstance == null)
            {
                return this.Read(input, default(T));
            }

            if (!(existingInstance is T))
            {
                throw new ContentLoadException("wrong type");
            }

            return this.Read(input, (T)existingInstance);
        }

        protected internal abstract T Read(ContentReader input, T existingInstance);
    }
}
