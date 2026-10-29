#region Using Statements
using System;

#endregion

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

#if WINDOWSMETRO
namespace ANX.Framework
{
    public class ExternalException : Exception
    {
        private int errorCode;

        public ExternalException()
            : base()
        {
        }

        public ExternalException(String message)
            : base(message)
        {
        }

        public ExternalException(String message, Exception inner)
            : base(message, inner)
        {
        }

        public ExternalException(String message, int errorCode)
            : base(message)
        {
            this.errorCode = errorCode;
        }

        public int ErrorCode
        {
            get
            {
                return this.errorCode;
            }
            set
            {
                this.errorCode = value;
            }
        }
    }
}
#endif