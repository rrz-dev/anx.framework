#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
using ANX.Framework;
using ANX.Framework.Graphics;
using ANX.Framework.Input;
#endregion // Using Statements

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace WindowsGame1
{
	/// <summary>
	/// This is the main type for your game
	/// </summary>
	public class Game1 : ANX.Framework.Game
	{
		GraphicsDeviceManager graphics;
		SpriteBatch spriteBatch;

		Texture2D texture;
		Texture2D alternateTexture;

		Color[] color = new Color[] { Color.White, Color.Green, Color.Blue, Color.Black, Color.White, Color.DarkMagenta };
		float[] y = new float[] { 10f, 10f, 10f, 10f, 10f, 10f };
		Random r = new Random();


		private float elapsedLastSecond = 0f;
		private int fpsCount = 0;
		private int lastFps = 60;

		public Game1()
		{
			graphics = new GraphicsDeviceManager(this);
			graphics.PreparingDeviceSettings += graphics_PreparingDeviceSettings;
			Content.RootDirectory = "SampleContent";
		}

		void graphics_PreparingDeviceSettings(object sender, PreparingDeviceSettingsEventArgs e)
		{
			e.GraphicsDeviceInformation.PresentationParameters.BackBufferWidth = 800;
			e.GraphicsDeviceInformation.PresentationParameters.BackBufferHeight = 600;
		}

		/// <summary>
		/// Allows the game to perform any initialization it needs to before starting to run.
		/// This is where it can query for any required services and load any non-graphic
		/// related content.  Calling base.Initialize will enumerate through any components
		/// and initialize them as well.
		/// </summary>
		protected override void Initialize()
		{
			base.Initialize();
		}

		/// <summary>
		/// LoadContent will be called once per game and is the place to load
		/// all of your content.
		/// </summary>
		protected override void LoadContent()
		{
			// Create a new SpriteBatch, which can be used to draw textures.
			spriteBatch = new SpriteBatch(GraphicsDevice);

			//this.alternateTexture = Content.Load<Texture2D>(@"Textures/DotColor4x4");
			this.alternateTexture = Content.Load<Texture2D>(@"Textures/DotWhiteTopLeft5x5");
			this.texture = Content.Load<Texture2D>(@"Textures/ANX.logo");

			//this.alternateTexture = new Texture2D(GraphicsDevice, 64, 64);
			//Color[] color = new Color[this.alternateTexture.Width * this.alternateTexture.Height];
			//for (int i = 0; i < color.Length; i++)
			//{
			//    color[i] = new Color(1.0f, 1.0f, 0, 0.5f);
			//}
			//this.alternateTexture.SetData<Color>(color);
		}

		/// <summary>
		/// UnloadContent will be called once per game and is the place to unload
		/// all content.
		/// </summary>
		protected override void UnloadContent()
		{
			// TODO: Unload any non ContentManager content here
		}

		/// <summary>
		/// Allows the game to run logic such as updating the world,
		/// checking for collisions, gathering input, and playing audio.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Update(GameTime gameTime)
		{
			elapsedLastSecond += (float)gameTime.ElapsedGameTime.TotalSeconds;
			fpsCount++;
			if (elapsedLastSecond >= 1f)
			{
				elapsedLastSecond -= 1f;
				lastFps = fpsCount;
				fpsCount = 0;

				Window.Title = "FPS=" + lastFps;
			}

			// Allows the game to exit
			if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
				this.Exit();

			for (int i = 0; i < y.Length; i++)
			{
				y[i] += this.r.Next(100) * (float)gameTime.ElapsedGameTime.TotalSeconds;
				y[i] = MathHelper.Clamp(y[i], 0, 536);
			}

			base.Update(gameTime);
		}

		/// <summary>
		/// This is called when the game should draw itself.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Draw(GameTime gameTime)
		{
			if (GamePad.GetState(PlayerIndex.One).Buttons.A == ButtonState.Pressed)
			{
				GraphicsDevice.Clear(Color.Green);
			}
			else
			{
				if (Mouse.GetState().XButton1 == ButtonState.Pressed)
				{
					GraphicsDevice.Clear(Color.Chocolate);
				}
				else
				{
					GraphicsDevice.Clear(Color.CornflowerBlue);
				}
			}

			if (Keyboard.GetState().IsKeyDown(Keys.Space))
			{
				if (GraphicsDevice.PresentationParameters.BackBufferWidth != 420)
				{
					PresentationParameters newParams = GraphicsDevice.PresentationParameters.Clone();
					newParams.BackBufferWidth = 420;
					newParams.BackBufferHeight = 360;
					GraphicsDevice.Reset(newParams);
				}
			}
			else
			{
				if (GraphicsDevice.PresentationParameters.BackBufferWidth != 800)
				{
					PresentationParameters newParams = GraphicsDevice.PresentationParameters.Clone();
					newParams.BackBufferWidth = 800;
					newParams.BackBufferHeight = 600;
					GraphicsDevice.Reset(newParams);
				}
			}

			spriteBatch.Begin();

			for (int x = 0; x < y.Length; x++)
			{
				spriteBatch.Draw(texture, new Vector2(x * texture.Width + 32, y[x]), new Rectangle(0, 0, 120, 60),
					color[x], 0.0f, Vector2.Zero, 1.0f, SpriteEffects.None, 0.0f);
			}

			spriteBatch.Draw(alternateTexture, new Vector2(32, 32), Color.White);

			spriteBatch.End();

			base.Draw(gameTime);
		}
	}
}
