#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
using ANX.Framework;
using ANX.Framework.Audio;
using ANX.Framework.Content;
using ANX.Framework.GamerServices;
using ANX.Framework.Graphics;
using ANX.Framework.Input;
using ANX.Framework.Media;

#endregion

namespace KeyboardSample
{
    public class Game1 : ANX.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        KeyboardState lastKeyState;
        SpriteFont debug;
        Texture2D texture;
        bool showTexture;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "SampleContent";

            this.Window.Title = "ANX.Framework Keyboard Sample - Press Enter to display ANX logo (ESC to exit)";
        }

        protected override void Initialize()
        {
            //TODO: see CodePlex issue #454
            this.lastKeyState = Keyboard.GetState();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            debug = Content.Load<SpriteFont>(@"Fonts/Debug");
            texture = Content.Load<Texture2D>(@"Textures/ANX.Framework.Logo_459x121");
        }

        protected override void UnloadContent()
        {

        }

        protected override void Update(GameTime gameTime)
        {
            KeyboardState keyState = Keyboard.GetState();

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
                keyState.IsKeyDown(Keys.Escape))
                this.Exit();

            if (keyState.IsKeyDown(Keys.Enter) && lastKeyState.IsKeyUp(Keys.Enter))
            {
                showTexture = true;
            }
            else if (keyState.IsKeyUp(Keys.Enter) && lastKeyState.IsKeyDown(Keys.Enter))
            {
                showTexture = false;
            }

            lastKeyState = keyState;

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();

            if (showTexture)
            {
                spriteBatch.Draw(texture, new Vector2(50, 50), Color.White);
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
