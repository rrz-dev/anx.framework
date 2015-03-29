using ANX.Framework.NonXNA.Development;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace ANX.Framework.Content.Pipeline.Helpers
{
    internal static class ExceptionHelper
    {
        [Developer("KorsarNek")]
        public static Exception CreateInvalidContentException(XmlReader xml, string basePath, Exception innerException, string message)
        {
            ContentIdentity contentIdentity = new ContentIdentity();
            if (basePath != null)
                contentIdentity.SourceFilename = basePath;
            else
                contentIdentity.SourceFilename = xml.BaseURI;

            IXmlLineInfo xmlLineInfo = xml as IXmlLineInfo;
            if (xmlLineInfo != null)
            {
                contentIdentity.FragmentIdentifier = string.Format("{0},{1}", xmlLineInfo.LineNumber,xmlLineInfo.LinePosition);
            }

            return new InvalidContentException(message, contentIdentity, innerException);
        }
    }
}
