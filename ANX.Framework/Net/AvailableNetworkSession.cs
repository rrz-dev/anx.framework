#region Using Statements
using System;
using System.Net;

#endregion // Using Statements

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Net
{
	public sealed class AvailableNetworkSession
	{
        private NetworkSessionType sessionType;
        private int openPublicGamerSlots;
        private QualityOfService qualityOfService;
        private NetworkSessionProperties sessionProperties;
#if !WINDOWSMETRO      //TODO: search replacement for Win8
        private IPEndPoint endPoint;
        private IPEndPoint internalendPoint;
#endif
        private int _currentGameCount;
        private string hostGamertag;
        private int openPrivateGamerSlots;

        public AvailableNetworkSession() 
        { 
            qualityOfService = new QualityOfService(); 
        }		
        
        public int CurrentGamerCount 
        { 
            get 
            { 
                return _currentGameCount; 
            } 
            internal set 
            { 
                _currentGameCount = value; 
            } 
        }		
        
        public string HostGamertag 
        { 
            get 
            { 
                return hostGamertag; 
            } 
            internal set 
            { 
                hostGamertag = value; 
            } 
        }		
        
         public int OpenPrivateGamerSlots 
         { 
             get 
             { 
                 return openPrivateGamerSlots; 
             } 
             internal set 
             { 
                 openPrivateGamerSlots = value; 
             } 
         }		
        
        public int OpenPublicGamerSlots 
        { 
            get 
            { 
                return openPublicGamerSlots; 
            } 
            internal set 
            { 
                openPublicGamerSlots = value; 
            } 
        }

        public QualityOfService QualityOfService 
        { 
            get 
            { 
                return qualityOfService; 
            } 
            internal set 
            { 
                qualityOfService = value; 
            } 
        }		
        
        public NetworkSessionProperties SessionProperties 
        { 
            get 
            { 
                return sessionProperties; 
            } 
            internal set 
            { 
                sessionProperties = value; 
            } 
        }
        
#if !WINDOWSMETRO      //TODO: search replacement for Win8
        internal IPEndPoint EndPoint 
        { 
            get 
            { 
                return endPoint; 
            } 
            set 
            { 
                endPoint = value; 
            } 
        }
        
        internal IPEndPoint InternalEndpont 
        { 
            get 
            { 
                return internalendPoint; 
            } 
            set 
            { 
                internalendPoint = value; 
            } 
        }        
#endif
        
        internal NetworkSessionType SessionType 
        {
            get
            {
                return this.sessionType;
            }
            set
            {
                this.sessionType = value;
            }
        }
    }
}
