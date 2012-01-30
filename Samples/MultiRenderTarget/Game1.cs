#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
#endregion

#region License

//
// This file is part of the ANX.Framework created by the "ANX.Framework developer group".
//
// This file is released under the Ms-PL license.
//
//
//
// Microsoft Public License (Ms-PL)
//
// This license governs use of the accompanying software. If you use the software, you accept this license. 
// If you do not accept the license, do not use the software.
//
// 1.Definitions
//   The terms "reproduce," "reproduction," "derivative works," and "distribution" have the same meaning 
//   here as under U.S. copyright law.
//   A "contribution" is the original software, or any additions or changes to the software.
//   A "contributor" is any person that distributes its contribution under this license.
//   "Licensed patents" are a contributor's patent claims that read directly on its contribution.
//
// 2.Grant of Rights
//   (A) Copyright Grant- Subject to the terms of this license, including the license conditions and limitations 
//       in section 3, each contributor grants you a non-exclusive, worldwide, royalty-free copyright license to 
//       reproduce its contribution, prepare derivative works of its contribution, and distribute its contribution
//       or any derivative works that you create.
//   (B) Patent Grant- Subject to the terms of this license, including the license conditions and limitations in 
//       section 3, each contributor grants you a non-exclusive, worldwide, royalty-free license under its licensed
//       patents to make, have made, use, sell, offer for sale, import, and/or otherwise dispose of its contribution 
//       in the software or derivative works of the contribution in the software.
//
// 3.Conditions and Limitations
//   (A) No Trademark License- This license does not grant you rights to use any contributors' name, logo, or trademarks.
//   (B) If you bring a patent claim against any contributor over patents that you claim are infringed by the software, your 
//       patent license from such contributor to the software ends automatically.
//   (C) If you distribute any portion of the software, you must retain all copyright, patent, trademark, and attribution 
//       notices that are present in the software.
//   (D) If you distribute any portion of the software in source code form, you may do so only under this license by including
//       a complete copy of this license with your distribution. If you distribute any portion of the software in compiled or 
//       object code form, you may only do so under a license that complies with this license.
//   (E) The software is licensed "as-is." You bear the risk of using it. The contributors give no express warranties, guarantees,
//       or conditions. You may have additional consumer rights under your local laws which this license cannot change. To the
//       extent permitted under your local laws, the contributors exclude the implied warranties of merchantability, fitness for a 
//       particular purpose and non-infringement.

#endregion // License

namespace MultiRenderTarget
{
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Effect effect;

        RenderTarget2D[] renderTargets;
        Color[] backgroundColors = new Color[] { Color.Red, Color.Green, Color.Blue, Color.DarkMagenta };

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
