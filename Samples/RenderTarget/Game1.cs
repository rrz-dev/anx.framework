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

namespace RenderTarget
{
    public class Game1 : ANX.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        RenderTarget2D renderTarget;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            this.Window.Title = "ANX.Framework - RenderTarget sample - you should see a green rectangle";
        }

        protected override void Initialize()
        {


            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            this.renderTarget = new RenderTarget2D(GraphicsDevice, 128, 128); //, false, SurfaceFormat.Color, DepthFormat.Depth24Stencil8);
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
            GraphicsDevice.SetRenderTarget(this.renderTarget);
            GraphicsDevice.Clear(ClearOptions.Target, Color.Green, 1.0f, 0);
            //GraphicsDevice.Clear(Color.Green);
            GraphicsDevice.SetRenderTarget(null);
            
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();
            spriteBatch.Draw(this.renderTarget, new Vector2(64, 64), Color.White);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
