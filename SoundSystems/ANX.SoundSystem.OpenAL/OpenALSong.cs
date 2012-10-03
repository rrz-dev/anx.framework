using System;
using System.IO;
using ANX.Framework.Audio;
using ANX.Framework.Media;
using ANX.Framework.NonXNA.Development;
using ANX.Framework.NonXNA.SoundSystem;
using OggUtils;
using OpenTK.Audio.OpenAL;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.SoundSystem.OpenAL
{
    [Developer("AstrorEnales")]
    public class OpenALSong : ISong
    {
        private Song parent;
        private FileStream oggFileStream;
        private OggInputStream oggStream;
        private int[] bufferHandles;
        private int sourceHandle = InvalidHandle;
        private const int InvalidHandle = -1;
        private static readonly byte[] streamReadBuffer = new byte[4096 * 8];

        public TimeSpan Duration { get; private set; }
        public TimeSpan PlayPosition { get; private set; }
        public MediaState State { get; private set; }

        public OpenALSong(Song setParent, Uri uri)
        {
            parent = setParent;
            Init(uri.AbsolutePath);
            // TODO: duration
        }

        public OpenALSong(Song setParent, string filepath, int duration)
        {
            parent = setParent;
            Init(filepath);
            Duration = new TimeSpan(0, 0, 0, 0, duration);
        }

        private void Init(string filepath)
        {
            PlayPosition = TimeSpan.Zero;

            State = MediaState.Stopped;
            oggFileStream = File.Open(filepath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            oggStream = new OggInputStream(oggFileStream);
            bufferHandles = AL.GenBuffers(2);
            sourceHandle = AL.GenSource();
        }

        public void Play()
        {
            if (State == MediaState.Playing)
                return;

            if (State == MediaState.Stopped)
            {
                Rewind();
                if (Stream(bufferHandles[0]) == false || Stream(bufferHandles[1]) == false)
                    return;

                AL.SourceQueueBuffers(sourceHandle, bufferHandles.Length, bufferHandles);
            }

            AL.SourcePlay(sourceHandle);
            State = MediaState.Playing;
        }

        public void Stop()
        {
            if (State == MediaState.Stopped)
                return;

            State = MediaState.Stopped;
            AL.SourceStop(sourceHandle);
            AL.SourceUnqueueBuffers(sourceHandle, bufferHandles.Length);
        }

        public void Pause()
        {
            if (State == MediaState.Paused)
                return;

            State = MediaState.Paused;
            AL.SourcePause(sourceHandle);
        }

        public void Resume()
        {
            Play();
        }

        public void Update()
        {
            if (sourceHandle == InvalidHandle || State == MediaState.Paused)
                return;

            int processed;
            AL.GetSource(sourceHandle, ALGetSourcei.BuffersProcessed, out processed);
            while (processed-- != 0)
            {
                int buffer = AL.SourceUnqueueBuffer(sourceHandle);
                if (Stream(buffer) == false)
                {
                    Stop();
                    return;
                }

                AL.SourceQueueBuffer(sourceHandle, buffer);
            }

            int state;
            AL.GetSource(sourceHandle, ALGetSourcei.SourceState, out state);
            switch ((ALSourceState)state)
            {
                case ALSourceState.Stopped:
                case ALSourceState.Initial:
                    State = MediaState.Stopped;
                    break;
                case ALSourceState.Playing:
                    State = MediaState.Playing;
                    break;
                default:
                    State = MediaState.Paused;
                    break;
            }
        }

        internal void Rewind()
        {
            PlayPosition = TimeSpan.Zero;
            oggFileStream.Position = 0;
            oggStream = new OggInputStream(oggFileStream);
        }

        internal bool Stream(int bufferHandle)
        {
            int size = oggStream.Read(streamReadBuffer);
            bool dataAvailable = size > 0;
            if (dataAvailable)
            {
                var channels = (AudioChannels)oggStream.Channels;
                PlayPosition = PlayPosition.Add(SoundEffect.GetSampleDuration(size, oggStream.SampleRate, channels));
                ALFormat format = oggStream.Channels > 1 ? ALFormat.Stereo16 : ALFormat.Mono16;
                AL.BufferData(bufferHandle, format, streamReadBuffer, size, oggStream.SampleRate);
            }

            return dataAvailable;
        }

        public void GetVisualizationData(VisualizationData data)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            if (oggFileStream != null)
                oggFileStream.Close();

            oggFileStream = null;
            oggStream = null;

            if (sourceHandle == InvalidHandle)
                return;

            AL.SourceStop(sourceHandle);
            AL.SourceUnqueueBuffers(sourceHandle, bufferHandles.Length);
            AL.DeleteSource(sourceHandle);
            sourceHandle = InvalidHandle;
            AL.DeleteBuffers(bufferHandles);
        }
    }
}
