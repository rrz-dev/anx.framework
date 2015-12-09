using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;

namespace ANX.Framework.Content.Pipeline
{
    [ServiceContract]
    public interface IContentBuildLogger
    {
        /// <summary>
        /// Gets or sets the base reference path used when reporting errors during the content build process. 
        /// </summary>
        string LoggerRootDirectory
        {
            [OperationContract]
            get;
            [OperationContract]
            set;
        }

        [OperationContract(Name = "LogImportantMessage")]
        void LogImportantMessage(string message, params Object[] messageArgs);

        [OperationContract(Name = "LogImportantContentMessage")]
        void LogImportantMessage(string helpLink, ContentIdentity contentIdentity, string message, params Object[] messageArgs);

        [OperationContract]
        void LogMessage(string message, params Object[] messageArgs);

        [OperationContract]
        void LogWarning(string helpLink, ContentIdentity contentIdentity, string message, params Object[] messageArgs);

        [OperationContract]
        void PopFile();

        [OperationContract]
        void PushFile(string filename);
    }
}
