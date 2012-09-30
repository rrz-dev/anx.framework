using System;
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
    public sealed class DynamicSoundEffectInstance : SoundEffectInstance
    {
        private IDynamicSoundEffectInstance nativeDynamicInstance;
        private readonly AudioChannels channels;
        private readonly int sampleRate;

        public event EventHandler<EventArgs> BufferNeeded;

        #region Public
        public int PendingBufferCount
        {
            get { return nativeDynamicInstance.PendingBufferCount; }
        }
        #endregion

        #region Constructor
        public DynamicSoundEffectInstance(int sampleRate, AudioChannels channels)
        {
            this.sampleRate = sampleRate;
            this.channels = channels;
            var creator = AddInSystemFactory.Instance.GetDefaultCreator<ISoundSystemCreator>();
            nativeDynamicInstance = creator.CreateDynamicSoundEffectInstance();
            nativeDynamicInstance.BufferNeeded += OnBufferNeeded;
            SetNativeInstance(nativeDynamicInstance);
        }
        #endregion

        private void OnBufferNeeded(object sender, EventArgs args)
        {
            BufferNeeded.Invoke(this, EventArgs.Empty);
        }

        #region GetSampleDuration
        public TimeSpan GetSampleDuration(int sizeInBytes)
        {
            float sizeMulBlockAlign = (float)sizeInBytes / ((int)channels * 2);
            return TimeSpan.FromMilliseconds(sizeMulBlockAlign * 1000f / sampleRate);
        }
        #endregion

        #region GetSampleSizeInBytes
        public int GetSampleSizeInBytes(TimeSpan duration)
        {
            int timeMulSamples = (int)(duration.TotalMilliseconds * (sampleRate / 1000f));
            return (timeMulSamples + timeMulSamples % (int)channels) * ((int)channels * 2);
        }
        #endregion
        
        public void SubmitBuffer(byte[] buffer)
        {
            nativeDynamicInstance.SubmitBuffer(buffer);
        }

        public void SubmitBuffer(byte[] buffer, int offset, int count)
        {
            nativeDynamicInstance.SubmitBuffer(buffer, offset, count);
        }

        protected override void Dispose(bool disposing)
        {
            if (nativeDynamicInstance != null)
            {
                nativeDynamicInstance.BufferNeeded -= OnBufferNeeded;
            }
            nativeDynamicInstance = null;
            base.Dispose(true);
        }
    }
}
