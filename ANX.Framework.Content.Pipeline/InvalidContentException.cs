#region Using Statements
using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

#endregion

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Content.Pipeline
{
    [Serializable]
    public class InvalidContentException : Exception
    {
        public InvalidContentException()
        {

        }

        public InvalidContentException(string message)
            : base(message)
        {
        }

        public InvalidContentException(string message, Exception innerException)
            : base(message, innerException)
        {

        }

        public InvalidContentException(string message, ContentIdentity contentIdentity)
            : base(message)
        {
            this.ContentIdentity = contentIdentity;
        }

        public InvalidContentException(string message, ContentIdentity contentIdentity, Exception innerException)
            : base(message, innerException)
        {
            this.ContentIdentity = contentIdentity;
        }

        protected InvalidContentException(SerializationInfo serializationInfo, StreamingContext streamingContext)
            : base(serializationInfo, streamingContext)
        {
            if (serializationInfo == null)
            {
                throw new ArgumentNullException("serializationInfo");
            }
            this.ContentIdentity = (ContentIdentity)serializationInfo.GetValue("ContentIdentity", typeof(ContentIdentity));
        }

        public ContentIdentity ContentIdentity
        {
            get;
            set;
        }

        [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            if (info == null)
                throw new ArgumentNullException("info");

            base.GetObjectData(info, context);
            info.AddValue("ContentIdentity", this.ContentIdentity, typeof(ContentIdentity));
        }
    }
}
