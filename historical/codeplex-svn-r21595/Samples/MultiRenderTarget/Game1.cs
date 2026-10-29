#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
using ANX.Framework;
using ANX.Framework.Content;
using ANX.Framework.Graphics;
using ANX.Framework.Input;
#endregion

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace MultiRenderTarget
{
    public class Game1 : ANX.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Effect effect;

        RenderTarget2D[] renderTargets;
        VertexPositionColor[] primitives;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "SampleContent";
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            effect = Content.Load<Effect>(@"Effects/MRT");

            renderTargets = new RenderTarget2D[] 
            {
                new RenderTarget2D(GraphicsDevice, GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Height / 2),
                new RenderTarget2D(GraphicsDevice, GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Height / 2),
                new RenderTarget2D(GraphicsDevice, GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Height / 2),
                new RenderTarget2D(GraphicsDevice, GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Height / 2),
            };

            primitives = new VertexPositionColor[]
            {
                new VertexPositionColor(new Vector3( 0.5f,  0.5f, 0.0f), Color.White),
                new VertexPositionColor(new Vector3( 0.5f, -0.5f, 0.0f), Color.White),
                new VertexPositionColor(new Vector3(-0.5f,  0.5f, 0.0f), Color.White),

                new VertexPositionColor(new Vector3( 0.5f, -0.5f, 0.0f), Color.White),
                new VertexPositionColor(new Vector3(-0.5f, -0.5f, 0.0f), Color.White),
                new VertexPositionColor(new Vector3(-0.5f,  0.5f, 0.0f), Color.White),
            };
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
            GraphicsDevice.SetRenderTargets(renderTargets[0], renderTargets[1], renderTargets[2], renderTargets[3]);
            GraphicsDevice.Clear(Color.Black);
            effect.CurrentTechnique.Passes[0].Apply();
            GraphicsDevice.DrawUserPrimitives<VertexPositionColor>(PrimitiveType.TriangleList, primitives, 0, 2);

            GraphicsDevice.SetRenderTarget(null);

            spriteBatch.Begin();
            spriteBatch.Draw(renderTargets[0], new Rectangle(0, 0, GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Height / 2), Color.White);
            spriteBatch.Draw(renderTargets[1], new Rectangle(GraphicsDevice.Viewport.Width / 2, 0, GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Height / 2), Color.White);
            spriteBatch.Draw(renderTargets[2], new Rectangle(0, GraphicsDevice.Viewport.Height / 2, GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Height / 2), Color.White);
            spriteBatch.Draw(renderTargets[3], new Rectangle(GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Height / 2, GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Height / 2), Color.White);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
