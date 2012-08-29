using System;
using ANX.Framework;
using ANX.Framework.Audio;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace AudioSample
{
	public class Game1 : Game
	{
		GraphicsDeviceManager graphics;
		SoundEffect sound;

		float timer;
		float duration;

		public Game1()
		{
			graphics = new GraphicsDeviceManager(this);
			Content.RootDirectory = "SampleContent";
		}

		protected override void Initialize()
		{
			base.Initialize();
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
				sound.Play();
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
