using ANX.Framework.NonXNA;
using ANX.Framework.NonXNA.Development;
using ANX.Framework.NonXNA.SoundSystem;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Audio
{
    [PercentageComplete(100)]
    [Developer("AstrorEnales")]
    [TestState(TestStateAttribute.TestState.Untested)]
    public class AudioListener
    {
        private readonly IAudioListener nativeListener;

        public Vector3 Forward
        {
            get { return nativeListener.Forward; }
            set { nativeListener.Forward = value; }
        }

        public Vector3 Position
        {
            get { return nativeListener.Position; }
            set { nativeListener.Position = value; }
        }

        public Vector3 Up
        {
            get { return nativeListener.Up; }
            set { nativeListener.Up = value; }
        }

        public Vector3 Velocity
        {
            get { return nativeListener.Velocity; }
            set { nativeListener.Velocity = value; }
        }

        public AudioListener()
        {
            var creator = AddInSystemFactory.Instance.GetDefaultCreator<ISoundSystemCreator>();
            nativeListener = creator.CreateAudioListener();
        }
    }
}
