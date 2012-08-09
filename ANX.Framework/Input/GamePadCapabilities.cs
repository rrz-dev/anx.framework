#region Using Statements
using System;
using System.IO;
using ANX.Framework.NonXNA;

#endregion // Using Statements

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Input
{
    public struct GamePadCapabilities
    {
        //public GamePadCapabilities(GamePadType gamePadType, bool hasAButton, bool hasBackButton, bool hasBButton, bool hasBigButton, bool hasDPadDownButton, bool hasDPadLeftButton, bool hasDPadRightButton, bool hasDPadUpButton, bool hasLeftShoulderButton, bool hasLeftStickButton, bool hasLeftTrigger, bool hasLeftVibrationMotor, bool hasLeftXThumbStick, bool hasLeftYThumbStick, bool hasRightShoulderButton, bool hasRightStickButton, bool hasRightVibrationMotor, bool hasRightTrigger, bool hasRightXThumbStick, bool hasRightYThumbStick, bool hasStartButton, bool hasVoiceSupport, bool hasXButton, bool hasYButton, bool isConnected)
        //{
        //    this.gamePadType = gamePadType;                                                                                                                                                                                                                                                                                                                                                                                                 
        //    this.hasAButton = hasAButton;                                                                                                                                                                                                                                                                                                                                                                                                   
        //    this.hasBackButton = hasBackButton;                                                                                                                                                                                                                                                                                                                                                                                             
        //    this.hasBButton = hasBButton;                                                                                                                                                                                                                                                                                                                                                                                                   
        //    this.hasBigButton = hasBigButton;
        //    this.hasDPadDownButton = hasDPadDownButton;                                                                                                                                                                                                                                                                                                                                                                                                        
        //    this.hasDPadLeftButton= hasDPadLeftButton;
        //    this.hasDPadRightButton = hasDPadRightButton;
        //    this.hasDPadUpButton = hasDPadUpButton;                                                                                                                                                                                                                        
        //    this.hasLeftShoulderButton=hasLeftShoulderButton;                                                                                                                                                                                                                  
        //    this.hasLeftStickButton=hasLeftStickButton;                                                                                                                                                                                                                     
        //    this.hasLeftTrigger=hasLeftTrigger;                                                                                                                                                                                                                         
        //    this.hasLeftVibrationMotor=hasLeftVibrationMotor;                                                                                                                                                                                                                  
        //    this.hasLeftXThumbStick=hasLeftXThumbStick;                                                                                               
        //    this.hasLeftYThumbStick=hasLeftYThumbStick;                                                                                               
        //    this.hasRightShoulderButton=hasRightShoulderButton;                                                                                           
        //    this.hasRightStickButton=hasRightStickButton;                                                                                              
        //    this.hasRightVibrationMotor=hasRightVibrationMotor;                                                                                           
        //    this.hasRightTrigger=hasRightTrigger;
        //    this.hasRightXThumbStick=hasRightXThumbStick;
        //    this.hasRightYThumbStick=hasRightYThumbStick;
        //    this.hasStartButton=hasStartButton;
        //    this.hasVoiceSupport=hasVoiceSupport;
        //    this.hasXButton=hasXButton;
        //    this.hasYButton=hasYButton;
        //    this.isConnected=isConnected;

        //}

        public GamePadType GamePadType
        {
            get;
            internal set;
        }

        public bool HasAButton
        {
            get;
            internal set;

        }

        public bool HasBackButton
        {
            get;
            internal set;
        }

        public bool HasBButton
        {
            get;
            internal set;
        }
        
        public bool HasBigButton 
        { 
            get; 
            internal set;
        }

        public bool HasDPadDownButton 
        { 
            get; 
            internal set; 
        }

        public bool HasDPadLeftButton 
        { 
            get; 
            internal set; 
        }

        public bool HasDPadRightButton 
        { 
            get; 
            internal set; 
        }

        public bool HasDPadUpButton 
        { 
            get; 
            internal set; 
        }

        public bool HasLeftShoulderButton 
        { 
            get; 
            internal set; 
        }

        public bool HasLeftStickButton 
        { 
            get; 
            internal set; 
        }

        public bool HasLeftTrigger 
        { 
            get; 
            internal set; 
        }

        public bool HasLeftVibrationMotor 
        { 
            get; 
            internal set; 
        }

        public bool HasLeftXThumbStick 
        { 
            get; 
            internal set; 
        }

        public bool HasLeftYThumbStick 
        { 
            get; 
            internal set; 
        }

        public bool HasRightShoulderButton 
        { 
            get; 
            internal set; 
        }

        public bool HasRightStickButton 
        { 
            get; 
            internal set; 
        }

        public bool HasRightVibrationMotor 
        { 
            get; 
            internal set; 
        }

        public bool HasRightTrigger 
        { 
            get; 
            internal set; 
        }

        public bool HasRightXThumbStick 
        { 
            get; 
            internal set; 
        }

        public bool HasRightYThumbStick 
        { 
            get; 
            internal set; 
        }

        public bool HasStartButton 
        { 
            get; 
            internal set; 
        }

        public bool HasVoiceSupport 
        { 
            get; 
            internal set; 
        }

        public bool HasXButton 
        { 
            get; 
            internal set; 
        }

        public bool HasYButton 
        { 
            get; 
            internal set; 
        }

        public bool IsConnected 
        { 
            get; 
            internal set; 
        }
    }
}
