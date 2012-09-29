using ANX.Framework;
using ANX.Framework.Audio;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace AudioSample
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager graphics;
        private SoundEffect sound;
        private float timer;
        private float duration;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "SampleContent";
        }

        protected override void LoadContent()
        {
            sound = Content.Load<SoundEffect>("Sounds\\testsound");
            timer = duration = (float)sound.Duration.TotalSeconds;
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            timer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (timer >= duration)
            {
                timer -= duration;
                sound.Play(1f, 1f, 0f);
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            base.Draw(gameTime);
        }
    }
}
