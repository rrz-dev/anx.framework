#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

#endregion // Using Statements

namespace Primitives
{
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        SpriteFont font;
        Texture2D bgTexture;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "SampleContent";
        }

        protected override void Initialize()
        {
            graphics.PreferredBackBufferWidth = 600;
            graphics.PreferredBackBufferHeight = 600;
            graphics.ApplyChanges();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            this.font = Content.Load<SpriteFont>(@"Fonts/Debug");

            this.bgTexture = new Texture2D(GraphicsDevice, 1, 1);
            this.bgTexture.SetData<Color>(new Color[] { Color.White });
        }

        protected override void UnloadContent()
        {

        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
                Keyboard.GetState().IsKeyDown(Keys.Escape))
                this.Exit();


            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();

            spriteBatch.Draw(bgTexture, new Rectangle(0, 0, 600, 200), Color.Blue);
            spriteBatch.Draw(bgTexture, new Rectangle(0, 200, 300, 200), Color.Red);
            spriteBatch.Draw(bgTexture, new Rectangle(300, 200, 300, 200), Color.Orange);
            spriteBatch.Draw(bgTexture, new Rectangle(0, 400, 300, 200), Color.Green);
            spriteBatch.Draw(bgTexture, new Rectangle(300, 400, 300, 200), Color.LightSeaGreen);

            DrawShadowText(spriteBatch, this.font, "DrawInstancedPrimitives", new Vector2(10, 10), Color.White, Color.Black);
            DrawShadowText(spriteBatch, this.font, "DrawPrimitives", new Vector2(10, 210), Color.White, Color.Black);
            DrawShadowText(spriteBatch, this.font, "DrawIndexedPrimitives", new Vector2(310, 210), Color.White, Color.Black);
            DrawShadowText(spriteBatch, this.font, "DrawUserPrimitives", new Vector2(10, 410), Color.White, Color.Black);
            DrawShadowText(spriteBatch, this.font, "DrawUserIndexedPrimitives", new Vector2(310, 410), Color.White, Color.Black);

            spriteBatch.End();

            base.Draw(gameTime);
        }

        private void DrawShadowText(SpriteBatch spriteBatch, SpriteFont font, String text, Vector2 position, Color foreground, Color shadow)
        {
            spriteBatch.DrawString(font, text, position + new Vector2(2, 2), shadow);
            spriteBatch.DrawString(font, text, position, foreground);
        }
    }
}
