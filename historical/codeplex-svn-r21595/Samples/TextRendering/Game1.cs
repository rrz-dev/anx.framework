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

namespace TextRendering
{
    public class Game1 : ANX.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        SpriteFont debugFont;

		int fps = 60;
		int fpsCount = 0;
		float fpsTimer = 0f;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreparingDeviceSettings += new EventHandler<PreparingDeviceSettingsEventArgs>(graphics_PreparingDeviceSettings);
            Content.RootDirectory = "SampleContent";

			IsFixedTimeStep = false;
			graphics.SynchronizeWithVerticalRetrace = false;
        }

        void graphics_PreparingDeviceSettings(object sender, PreparingDeviceSettingsEventArgs e)
        {
            e.GraphicsDeviceInformation.PresentationParameters.BackBufferWidth = 1280;
            e.GraphicsDeviceInformation.PresentationParameters.BackBufferHeight = 720;
        }

        protected override void Initialize()
        {
            //GraphicsDevice.PresentationParameters.BackBufferWidth = 1280;
            //GraphicsDevice.PresentationParameters.BackBufferHeight = 720;
            //GraphicsDevice.Reset();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            this.debugFont = Content.Load<SpriteFont>(@"Fonts/Debug");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

			fpsCount++;
			fpsTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
			if (fpsTimer >= 1f)
			{
				fpsTimer -= 1f;
				fps = fpsCount;
				fpsCount = 0;
			}

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin(SpriteSortMode.FrontToBack, null);
			spriteBatch.DrawString(this.debugFont, "Hello World! FPS: " + fps, new Vector2(100, 100), Color.White);

            spriteBatch.DrawString(this.debugFont, "This screen is powered by the ANX.Framework!\r\nsecond line", new Vector2(100, 100 + this.debugFont.LineSpacing), Color.Black, 0.0f, new Vector2(1, -1), Vector2.One, SpriteEffects.None, 0.0f);
            spriteBatch.DrawString(this.debugFont, "This screen is powered by the ANX.Framework!\r\nsecond line", new Vector2(100, 100 + this.debugFont.LineSpacing), Color.Red, 0.0f, Vector2.Zero, Vector2.One, SpriteEffects.None, 1.0f);
            spriteBatch.DrawString(this.debugFont, "Mouse X: " + Mouse.GetState().X, new Vector2(100, 100 + this.debugFont.LineSpacing * 3), Color.DarkOrange);
            spriteBatch.DrawString(this.debugFont, "Mouse Y: " + Mouse.GetState().Y, new Vector2(100, 100 + this.debugFont.LineSpacing * 4), Color.DarkOrange);
            spriteBatch.DrawString(this.debugFont, "rotated Text", new Vector2(100, 150), Color.Green, MathHelper.ToRadians(90), Vector2.Zero, 1.0f, SpriteEffects.None, 1.0f);

            GamePadState state = GamePad.GetState(PlayerIndex.One);
            spriteBatch.DrawString(this.debugFont, String.Format("GamePad Left: {0} Right: {1}", state.ThumbSticks.Left, state.ThumbSticks.Right) , new Vector2(100, 100 + this.debugFont.LineSpacing * 5), Color.Red, 0.0f, Vector2.Zero, Vector2.One, SpriteEffects.None, 1.0f);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
