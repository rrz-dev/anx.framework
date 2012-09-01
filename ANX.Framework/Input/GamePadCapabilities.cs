using ANX.Framework.NonXNA.Development;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Input
{
	[PercentageComplete(100)]
	[TestState(TestStateAttribute.TestState.Tested)]
    public struct GamePadCapabilities
    {
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
