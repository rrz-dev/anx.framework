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

        BasicEffect basicEffect;
        Matrix viewMatrix;
        Matrix projectionMatrix;
        Matrix worldMatrix;

        VertexBuffer cubeNoIndicesBuffer;

        #region Corners of cube
        static Vector3 topLeftFront = new Vector3( -1.0f, 1.0f, 1.0f );
        static Vector3 bottomLeftFront = new Vector3(-1.0f, -1.0f, 1.0f);
        static Vector3 topRightFront = new Vector3(1.0f, 1.0f, 1.0f);
        static Vector3 bottomRightFront = new Vector3(1.0f, -1.0f, 1.0f);
        static Vector3 topLeftBack = new Vector3(-1.0f, 1.0f, -1.0f);
        static Vector3 topRightBack = new Vector3(1.0f, 1.0f, -1.0f);
        static Vector3 bottomLeftBack = new Vector3(-1.0f, -1.0f, -1.0f);
        static Vector3 bottomRightBack = new Vector3(1.0f, -1.0f, -1.0f);

        #endregion

        VertexPositionColor[] cubeNoIndices = new VertexPositionColor[] { new VertexPositionColor(topLeftFront, Color.White),
                                                                          new VertexPositionColor(bottomRightFront, Color.White),
                                                                          new VertexPositionColor(bottomLeftFront, Color.White),

                                                                          new VertexPositionColor(topLeftFront, Color.White),
                                                                          new VertexPositionColor(topRightFront, Color.White),
                                                                          new VertexPositionColor(bottomRightFront, Color.White),

                                                                          new VertexPositionColor(topLeftBack, Color.White),
                                                                          new VertexPositionColor(bottomLeftBack, Color.White),
                                                                          new VertexPositionColor(bottomRightBack, Color.White),

                                                                          new VertexPositionColor(topLeftBack, Color.White),
                                                                          new VertexPositionColor(bottomRightBack, Color.White),
                                                                          new VertexPositionColor(topRightBack, Color.White)        
                                                                        };

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "SampleContent";

            graphics.PreparingDeviceSettings += new EventHandler<PreparingDeviceSettingsEventArgs>(graphics_PreparingDeviceSettings);
        }

        void graphics_PreparingDeviceSettings(object sender, PreparingDeviceSettingsEventArgs e)
        {
            e.GraphicsDeviceInformation.PresentationParameters.BackBufferWidth = 600;
            e.GraphicsDeviceInformation.PresentationParameters.BackBufferHeight = 600;
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            this.basicEffect = new BasicEffect(GraphicsDevice);
            
            this.worldMatrix = Matrix.Identity;
            this.projectionMatrix = Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver4, GraphicsDevice.Viewport.AspectRatio, 0.1f, 10.0f);
            this.viewMatrix = Matrix.CreateLookAt(new Vector3(5, 5, 5), new Vector3(0, 0, 0), Vector3.Up);

            this.font = Content.Load<SpriteFont>(@"Fonts/Debug");

            this.bgTexture = new Texture2D(GraphicsDevice, 1, 1);
            this.bgTexture.SetData<Color>(new Color[] { Color.White });

            //
            // create a VertexBuffer for a cube without indices
            //

            this.cubeNoIndicesBuffer = new VertexBuffer(GraphicsDevice, typeof(VertexPositionColor), cubeNoIndices.Length, BufferUsage.None);
            this.cubeNoIndicesBuffer.SetData<VertexPositionColor>(cubeNoIndices);
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

            this.GraphicsDevice.RasterizerState = new RasterizerState() { CullMode = CullMode.None, FillMode = FillMode.WireFrame };

            this.basicEffect.World = this.worldMatrix;
            this.basicEffect.View = this.viewMatrix;
            this.basicEffect.Projection = this.projectionMatrix;

            this.basicEffect.CurrentTechnique.Passes[0].Apply();
            GraphicsDevice.SetVertexBuffer(this.cubeNoIndicesBuffer);
            GraphicsDevice.DrawPrimitives(PrimitiveType.TriangleList, 0, cubeNoIndices.Length / 3);

            base.Draw(gameTime);
        }

        private void DrawShadowText(SpriteBatch spriteBatch, SpriteFont font, String text, Vector2 position, Color foreground, Color shadow)
        {
            spriteBatch.DrawString(font, text, position + new Vector2(2, 2), shadow);
            spriteBatch.DrawString(font, text, position, foreground);
        }
    }
}
