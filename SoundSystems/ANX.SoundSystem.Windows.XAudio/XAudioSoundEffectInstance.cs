using System;
using ANX.Framework;
using ANX.Framework.Audio;
using ANX.Framework.NonXNA.SoundSystem;
using SharpDX.XAudio2;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.SoundSystem.Windows.XAudio
{
    public class XAudioSoundEffectInstance : ISoundEffectInstance
    {
        #region Private
        private SourceVoice source;
        private float currentPitch;
        private float currentPan;
        private bool currentIsLooped;
        private readonly XAudioSoundEffect parent;
        private float[] panMatrix;
        #endregion

        #region Public
        public bool IsLooped
        {
            get { return currentIsLooped; }
            set
            {
                currentIsLooped = value;
                // TODO: set real parameter
                if(value)
                    throw new NotImplementedException("IsLooped is currently not implemented for XAudio!");
            }
        }

        public float Pan
        {
            get { return currentPan; }
            set
            {
                currentPan = MathHelper.Clamp(value, -1f, 1f);
                UpdateSourcePan();
            }
        }

        public float Pitch
        {
            get { return currentPitch; }
            set
            {
                currentPitch = value;
                source.SetFrequencyRatio(value);
                // TODO: pitch <= 1 is working, but greater isn't
                if (value > 1f)
                    throw new NotImplementedException("Pitch greater than 1f is currently not implemented for XAudio!");
            }
        }

        public SoundState State { get; private set; }

        public float Volume
        {
            get { return source.Volume; }
            set { source.SetVolume(value, 0); }
        }
        #endregion

        #region Constructor
        internal XAudioSoundEffectInstance(XAudio2 device, XAudioSoundEffect setParent)
        {
            parent = setParent;
            currentIsLooped = false;
            currentPan = 0f;
            currentPitch = 1f;
            State = SoundState.Stopped;
            source = new SourceVoice(device, setParent.WaveFormat);
            source.SubmitSourceBuffer(setParent.AudioBuffer, setParent.DecodedPacketsInfo);
        }
        #endregion

        #region Play
        public void Play()
        {
            if (State == SoundState.Playing)
                return;

            State = SoundState.Playing;
            source.Start();
        }
        #endregion

        #region Pause
        public void Pause()
        {
            State = SoundState.Paused;
        }
        #endregion

        #region Stop
        public void Stop(bool immediate)
        {
            if (State == SoundState.Stopped)
                return;

            if (immediate == false)
                return;

            State = SoundState.Stopped;
            source.Stop();
        }
        #endregion

        #region Resume
        public void Resume()
        {
            State = SoundState.Playing;
        }
        #endregion

        #region UpdateSourcePan (TODO)
        private void UpdateSourcePan()
        {
            var sourceChannelCount = parent.WaveFormat.Channels;
            var destinationChannelCount = Creator.MasteringVoice.VoiceDetails.InputChannelCount;
            InitializePanMatrix(destinationChannelCount);

            var leftPanValue = 1f - currentPan;
            var rightPanValue = 1f + currentPan;
            panMatrix[0] = leftPanValue;
            panMatrix[1] = rightPanValue;
            
            // TODO: get the channel mask which is strangely only available on Windows8
            //switch (Creator.MasteringVoice.ChannelMask)
            //{
            //    case (int)Speakers.Quad:
            //        panMatrix[2] = leftPanValue;
            //        panMatrix[3] = rightPanValue;
            //        break;

            //    case (int)Speakers.FourPointOne:
            //        panMatrix[3] = leftPanValue;
            //        panMatrix[4] = rightPanValue;
            //        break;

            //    case (int)Speakers.FivePointOne:
            //    case (int)Speakers.SevenPointOne:
            //    case (int)Speakers.FivePointOneSurround:
            //        panMatrix[4] = leftPanValue;
            //        panMatrix[5] = rightPanValue;
            //        break;

            //    case (int)Speakers.SevenPointOneSurround:
            //        panMatrix[4] = panMatrix[6] = leftPanValue;
            //        panMatrix[5] = panMatrix[7] = rightPanValue;
            //        break;
            //}

            source.SetOutputMatrix(sourceChannelCount, destinationChannelCount, panMatrix);
        }
        #endregion

        #region InitializePanMatrix
        private void InitializePanMatrix(int size)
        {
            if (panMatrix == null || panMatrix.Length < size)
                panMatrix = new float[Math.Max(size, 8)];

            for (var index = 0; index < panMatrix.Length; index++)
                panMatrix[index] = 1f;
        }
        #endregion

        #region Apply3D (TODO)
        public void Apply3D(AudioListener[] listeners, AudioEmitter emitter)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Dispose
        public void Dispose()
        {
            if (source != null)
                source.Dispose();
            source = null;
        }
        #endregion
    }
}
