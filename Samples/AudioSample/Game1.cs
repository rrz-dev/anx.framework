//#define UseMusicPlayback

using System.IO;
using ANX.Framework;
using ANX.Framework.Audio;
using ANX.Framework.Media;
using System;
using ANX.Framework.Input;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace AudioSample
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager graphics;
        private SoundEffect sound;
        private Song song;
        private float timer;
        private float duration;
        private bool useMusicPlayback;
        private KeyboardState previousState;

        //private const bool UseMusicPlayback = false;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "SampleContent";
        }

        protected override void LoadContent()
        {
            sound = Content.Load<SoundEffect>("Sounds\\testsound");
            string testmusicPath = Path.GetFullPath("../../../../../media/testmusic.ogg");
            song = Song.FromUri(testmusicPath, new Uri(testmusicPath));
            timer = duration = (float)sound.Duration.TotalSeconds;
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Enter) && previousState.IsKeyUp(Keys.Enter))
            {
                useMusicPlayback = !useMusicPlayback;
                timer = duration; //Reset playback
            }

            timer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (timer >= duration)
            {
                timer -= duration;
                if (useMusicPlayback)
                {
                    MediaPlayer.Play(song);
                }
                else
                {
                    sound.Play(1f, 1f, 0f);
                }
            }

            if (useMusicPlayback)
            {
                Window.Title = "MusicPlayback: PlayTime = " + MediaPlayer.PlayPosition;

                if (Keyboard.GetState().IsKeyDown(Keys.A))
                    MediaPlayer.Pause();

                if (Keyboard.GetState().IsKeyDown(Keys.S))
                    MediaPlayer.Resume();
            }
            else
            {
                Window.Title = "SoundPlayback";
            }

            base.Update(gameTime);

            previousState = Keyboard.GetState();
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            base.Draw(gameTime);
        }
    }
}
