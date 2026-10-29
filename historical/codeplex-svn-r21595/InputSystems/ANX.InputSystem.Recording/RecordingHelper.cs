using ANX.Framework.NonXNA;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.InputSystem.Recording
{
    public enum RecordingState
    {
        /// <summary>
        /// This device is recording input.
        /// </summary>
        Recording,
        /// <summary>
        /// This device plays back recorded input.
        /// </summary>
        Playback,
        /// <summary>
        /// Playback and Recording paused, the current values will be passed through.
        /// </summary>
        None
    }

    //0-1 are reserved for the recording Engine, 2-255 can be used using WriteUserState().
    enum PacketType : byte
    {
        NullFrameCounter = 0,
        InputData = 1
    }
    
    /// <summary>
    /// Static Helper-class containing some recording-related stuff.
    /// </summary>
    public static class RecordingHelper
    {
        /// <summary>
        /// Returns the RecordingMouse of the RecordingInput-System.
        /// </summary>
        public static RecordingMouse GetMouse()
        {
            return ((RecordingMouse)AddInSystemFactory.Instance.GetCreator<IInputSystemCreator>("Recording").Mouse);
        }

        /// <summary>
        /// Returns the RecordingKeyboard of the RecordingInput-System.
        /// </summary>
        public static RecordingKeyboard GetKeyboard()
        {
            return ((RecordingKeyboard)AddInSystemFactory.Instance.GetCreator<IInputSystemCreator>("Recording").Keyboard);
        }

        /// <summary>
        /// Returns the RecordingGamePad of the RecordingInput-System.
        /// </summary>
        public static RecordingGamePad GetGamepad()
        {
            return ((RecordingGamePad)AddInSystemFactory.Instance.GetCreator<IInputSystemCreator>("Recording").GamePad);
        }
    }
}
