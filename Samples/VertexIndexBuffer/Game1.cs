#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
using ANX.Framework;
using ANX.Framework.Graphics;
using ANX.Framework.Input;
using ANX.Framework.NonXNA;

#endregion // Using Statements

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace VertexIndexBuffer
{
    public class Game1 : ANX.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Effect miniTriEffect;
        VertexBuffer vb;
        VertexBuffer vb2;
        IndexBuffer ib;

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
            spriteBatch = new SpriteBatch(GraphicsDevice);

            miniTriEffect = Content.Load<Effect>(@"Effects/MiniTri");
            miniTriEffect.Parameters["world"].SetValue(Matrix.CreateTranslation(0.5f, 0.0f, 0.0f));

            vb = new VertexBuffer(GraphicsDevice, VertexPositionColor.VertexDeclaration, 6, BufferUsage.None);
            VertexPositionColor[] vertices = new[] { new VertexPositionColor( new Vector3(-0.75f,  0.25f, 0.5f), new Color(1.0f, 0.0f, 0.0f, 1.0f)),
                                                     new VertexPositionColor( new Vector3(-0.25f,  0.25f, 0.5f), new Color(0.0f, 1.0f, 0.0f, 1.0f)),
                                                     new VertexPositionColor( new Vector3(-0.75f, -0.25f, 0.5f), new Color(0.0f, 0.0f, 1.0f, 1.0f)),

                                                     new VertexPositionColor( new Vector3(-0.25f,  0.25f, 0.5f), new Color(0.0f, 1.0f, 0.0f, 1.0f)),
                                                     new VertexPositionColor( new Vector3(-0.25f, -0.25f, 0.5f), new Color(1.0f, 0.0f, 0.0f, 1.0f)),
                                                     new VertexPositionColor( new Vector3(-0.75f, -0.25f, 0.5f), new Color(0.0f, 0.0f, 1.0f, 1.0f)),
                                                   };
            vb.SetData<VertexPositionColor>(vertices);

            vb2 = new VertexBuffer(GraphicsDevice, VertexPositionColor.VertexDeclaration, 4, BufferUsage.None);
            VertexPositionColor[] vertices2 = new[] { new VertexPositionColor( new Vector3(0.25f,  0.25f, 0.5f), new Color(1.0f, 0.0f, 0.0f, 1.0f)),
                                                      new VertexPositionColor( new Vector3(0.75f,  0.25f, 0.5f), new Color(0.0f, 1.0f, 0.0f, 1.0f)),
                                                      new VertexPositionColor( new Vector3(0.25f, -0.25f, 0.5f), new Color(0.0f, 0.0f, 1.0f, 1.0f)),
                                                      new VertexPositionColor( new Vector3(0.75f, -0.25f, 0.5f), new Color(1.0f, 0.0f, 0.0f, 1.0f)),
                                                   };
            vb2.SetData<VertexPositionColor>(vertices2);

            ib = new IndexBuffer(GraphicsDevice, IndexElementSize.ThirtyTwoBits, 6, BufferUsage.None);
            int[] indices = new[] { 0, 1, 2, 1, 3, 2 };
            ib.SetData<int>(indices);

            graphics.PreferredBackBufferWidth = 810;
            graphics.PreferredBackBufferHeight = 486;
            graphics.PreferredDepthStencilFormat = DepthFormat.Depth24Stencil8;
            graphics.ApplyChanges();

            VertexPositionColor[] vertices23 = new VertexPositionColor[vb.VertexCount];
            vb.GetData<VertexPositionColor>(vertices23);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            miniTriEffect.CurrentTechnique.Passes[0].Apply();

            GraphicsDevice.SetVertexBuffer(vb);
            GraphicsDevice.DrawPrimitives(PrimitiveType.TriangleList, 0, 6);

            GraphicsDevice.SetVertexBuffer(vb2);
            GraphicsDevice.Indices = ib;
            GraphicsDevice.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, 0, 4, 0, 2);

            base.Draw(gameTime);
        }
    }
}
