#region Using Statements
using System;
using System.Runtime.Serialization;
using ANX.Framework.NonXNA.Development;

#endregion // Using Statements

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Content
{
#if !WINDOWSMETRO      //TODO: search replacement for Win8
    [SerializableAttribute]
#endif
    [Developer("GinieDP")]
    public class ContentLoadException : Exception
    {
        /// <summary>
        /// Creates a new ContentLoadException.
        /// </summary>
        public ContentLoadException()
        {
        }

        /// <summary>
        /// Creates a new ContentLoadException.
        /// </summary>
        public ContentLoadException(string message)
            : base(message)
        {
        }
        /// <summary>
        /// Creates a new ContentLoadException.
        /// </summary>
        public ContentLoadException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

#if !WINDOWSMETRO      //TODO: search replacement for Win8
        /// <summary>
        /// Creates a new ContentLoadException.
        /// </summary>
        protected ContentLoadException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
#endif
    }
}
