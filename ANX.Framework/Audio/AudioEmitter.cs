using ANX.Framework.NonXNA;
using ANX.Framework.NonXNA.SoundSystem;
using ANX.Framework.NonXNA.Development;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Audio
{
    [PercentageComplete(100)]
    [Developer("AstrorEnales")]
    [TestState(TestStateAttribute.TestState.Untested)]
    public class AudioEmitter
    {
        private readonly IAudioEmitter nativeEmitter;

        public float DopplerScale
        {
            get { return nativeEmitter.DopplerScale; }
            set { nativeEmitter.DopplerScale = value; }
        }

        public Vector3 Forward
        {
            get { return nativeEmitter.Forward; }
            set { nativeEmitter.Forward = value; }
        }

        public Vector3 Position
        {
            get { return nativeEmitter.Position; }
            set { nativeEmitter.Position = value; }
        }

        public Vector3 Up
        {
            get { return nativeEmitter.Up; }
            set { nativeEmitter.Up = value; }
        }

        public Vector3 Velocity
        {
            get { return nativeEmitter.Velocity; }
            set { nativeEmitter.Velocity = value; }
        }

        public AudioEmitter()
        {
            var creator = AddInSystemFactory.Instance.GetDefaultCreator<ISoundSystemCreator>();
            nativeEmitter = creator.CreateAudioEmitter();
        }
    }
}
