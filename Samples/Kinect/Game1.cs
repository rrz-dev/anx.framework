using System;
using System.Collections.Generic;
using System.Linq;
using ANX.Framework;
using ANX.Framework.Content;
using ANX.Framework.Graphics;
using ANX.Framework.Input;
using ANX.Framework.Input.MotionSensing;

namespace Kinect
{
    public class Game1 : ANX.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        MotionSensingDeviceState kinectState;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "SampleContent";
        }

        protected override void Initialize()
        {
            MotionSensingDevice.GraphicsDevice = GraphicsDevice;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            kinectState = MotionSensingDevice.GetState();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            if (kinectState.Depth != null)
            {
                spriteBatch.Begin();

                spriteBatch.Draw(kinectState.RGB, Vector2.Zero, Color.White);

                spriteBatch.End();
            }

            base.Draw(gameTime);
        }
    }
}
