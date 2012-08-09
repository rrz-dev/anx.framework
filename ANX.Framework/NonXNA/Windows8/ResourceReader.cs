#region Using Statements
using System;
using System.IO;

#endregion

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

#if WINDOWSMETRO
namespace ANX.Framework
{
    public class ResourceReader : IDisposable
    {

        public ResourceReader(Stream resourceStream)
        {
            //TODO: implement
        }

        public IDictionaryEnumerator GetEnumerator()
        {
            //TODO: implement
            throw new NotImplementedException();
        }

        public void Close()
        {
            //TODO: implement
        }

        public void Dispose()
        {
            //TODO: implement
        }
    }
}
#endif