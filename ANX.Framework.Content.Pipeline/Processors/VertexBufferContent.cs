#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

#endregion

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Content.Pipeline.Processors
{
    public class VertexBufferContent : ContentItem
    {
        public VertexBufferContent()
        {

        }

        public VertexBufferContent(int size)
        {
            throw new NotImplementedException();
        }

        public byte[] VertexData
        {
            get;
            private set;
        }

        public VertexDeclarationContent VertexDeclaration
        {
            get;
            set;
        }

        public static int SizeOf(Type type)
        {
            throw new NotImplementedException();
        }

        public void Write<T>(int offset, int stride, IEnumerable<T> data)
        {
            throw new NotImplementedException();
        }

        public void Write(int offset, int stride, Type dataType, IEnumerable data)
        {
            throw new NotImplementedException();
        }


    }
}
