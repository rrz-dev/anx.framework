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
        Effect hardwareInstanceEffect;

        Matrix viewMatrix;
        Matrix projectionMatrix;
        Matrix instancedProjectionMatrix;
        Matrix worldMatrix;

        float rotation = 0.0f;

        DynamicVertexBuffer instanceVertexBuffer;
        VertexBuffer cubeNoIndicesBuffer;
        VertexBuffer cubeVertexBuffer;
        IndexBuffer cubeIndexBuffer;

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

        Matrix[] instanceTransformMatrices = new Matrix[] { Matrix.CreateTranslation(-10.0f, 0.0f, 0.0f),
                                                            Matrix.CreateTranslation(-5.0f, 0.0f, 0.0f),
                                                            Matrix.CreateTranslation(0.0f, 0.0f, 0.0f),
                                                            Matrix.CreateTranslation(5.0f, 0.0f, 0.0f),
                                                            Matrix.CreateTranslation(10.0f, 0.0f, 0.0f),
                                                          };

        VertexDeclaration instanceDecl = new VertexDeclaration
        (
            new VertexElement(0, VertexElementFormat.Vector4, VertexElementUsage.BlendWeight, 0),
            new VertexElement(16, VertexElementFormat.Vector4, VertexElementUsage.BlendWeight, 1),
            new VertexElement(32, VertexElementFormat.Vector4, VertexElementUsage.BlendWeight, 2),
            new VertexElement(48, VertexElementFormat.Vector4, VertexElementUsage.BlendWeight, 3)
        );

        VertexPositionColor[] cubeVertices = new VertexPositionColor[] { new VertexPositionColor(topLeftFront, Color.White),
                                                                         new VertexPositionColor(bottomLeftFront, Color.White),
                                                                         new VertexPositionColor(topRightFront, Color.White),
                                                                         new VertexPositionColor(bottomRightFront, Color.White),
                                                                         new VertexPositionColor(topLeftBack, Color.White),
                                                                         new VertexPositionColor(topRightBack, Color.White),
                                                                         new VertexPositionColor(bottomLeftBack, Color.White),
                                                                         new VertexPositionColor(bottomRightBack, Color.White)
                                                                       };

        short[] cubeIndices = new short[] { 0, 3, 1,
                                            0, 2, 3,
                                            4, 6, 7,
                                            4, 7, 5,
                                            0, 4, 2,
                                            2, 4, 5,
                                            1, 3, 6,
                                            3, 7, 6,
                                            0, 1, 6,
                                            6, 4, 0,
                                            2, 7, 3,
                                            7, 2, 5 };

        VertexPositionColor[] cubeNoIndices = new VertexPositionColor[] { new VertexPositionColor(topLeftFront, Color.White),       // 0
                                                                          new VertexPositionColor(bottomRightFront, Color.White),   // 3
                                                                          new VertexPositionColor(bottomLeftFront, Color.White),    // 1

                                                                          new VertexPositionColor(topLeftFront, Color.White),       // 0
                                                                          new VertexPositionColor(topRightFront, Color.White),      // 2
                                                                          new VertexPositionColor(bottomRightFront, Color.White),   // 3

                                                                          new VertexPositionColor(topLeftBack, Color.White),        // 4
                                                                          new VertexPositionColor(bottomLeftBack, Color.White),     // 6
                                                                          new VertexPositionColor(bottomRightBack, Color.White),    // 7

                                                                          new VertexPositionColor(topLeftBack, Color.White),        // 4
                                                                          new VertexPositionColor(bottomRightBack, Color.White),    // 7
                                                                          new VertexPositionColor(topRightBack, Color.White),       // 5
        
                                                                          new VertexPositionColor(topLeftFront, Color.White),       // 0
                                                                          new VertexPositionColor(topLeftBack, Color.White),        // 4
                                                                          new VertexPositionColor(topRightFront, Color.White),      // 2

                                                                          new VertexPositionColor(topRightFront, Color.White),      // 2
                                                                          new VertexPositionColor(topLeftBack, Color.White),        // 4
                                                                          new VertexPositionColor(topRightBack, Color.White),       // 5

                                                                          new VertexPositionColor(bottomLeftFront, Color.White),    // 1
                                                                          new VertexPositionColor(bottomRightFront, Color.White),   // 3
                                                                          new VertexPositionColor(bottomLeftBack, Color.White),     // 6

                                                                          new VertexPositionColor(bottomRightFront, Color.White),   // 3
                                                                          new VertexPositionColor(bottomRightBack, Color.White),    // 7
                                                                          new VertexPositionColor(bottomLeftBack, Color.White),     // 6

                                                                          new VertexPositionColor(topLeftFront, Color.White),       // 0
                                                                          new VertexPositionColor(bottomLeftFront, Color.White),    // 1
                                                                          new VertexPositionColor(bottomLeftBack, Color.White),     // 6

                                                                          new VertexPositionColor(bottomLeftBack, Color.White),     // 6
                                                                          new VertexPositionColor(topLeftBack, Color.White),        // 4
                                                                          new VertexPositionColor(topLeftFront, Color.White),       // 0

                                                                          new VertexPositionColor(topRightFront, Color.White),      // 2
                                                                          new VertexPositionColor(bottomRightBack, Color.White),    // 7
                                                                          new VertexPositionColor(bottomRightFront, Color.White),   // 3

                                                                          new VertexPositionColor(bottomRightBack, Color.White),    // 7
                                                                          new VertexPositionColor(topRightFront, Color.White),      // 2
                                                                          new VertexPositionColor(topRightBack, Color.White),       // 5

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
            this.hardwareInstanceEffect = Content.Load<Effect>(@"Effects/HardwareInstancing");

            this.worldMatrix = Matrix.Identity;
            this.projectionMatrix = Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver4, 300f/200f, 0.1f, 50.0f);
            this.instancedProjectionMatrix = Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver4, 600f / 200f, 0.1f, 50.0f);
            this.viewMatrix = Matrix.CreateLookAt(new Vector3(5, 5, 5), new Vector3(0, 0, 0), Vector3.Up);

            this.font = Content.Load<SpriteFont>(@"Fonts/Debug");

            this.bgTexture = new Texture2D(GraphicsDevice, 1, 1);
            this.bgTexture.SetData<Color>(new Color[] { Color.White });

            //
            // create a VertexBuffer for a cube without indices
            //

            this.cubeNoIndicesBuffer = new VertexBuffer(GraphicsDevice, typeof(VertexPositionColor), cubeNoIndices.Length, BufferUsage.None);
            this.cubeNoIndicesBuffer.SetData<VertexPositionColor>(cubeNoIndices);

            //
            // create a Vertex- and IndexBuffer for a cube with indexed primitives
            //
            this.cubeVertexBuffer = new VertexBuffer(GraphicsDevice, typeof(VertexPositionColor), cubeVertices.Length, BufferUsage.None);
            this.cubeVertexBuffer.SetData<VertexPositionColor>(cubeVertices);

            this.cubeIndexBuffer = new IndexBuffer(GraphicsDevice, typeof(short), this.cubeIndices.Length, BufferUsage.None);
            this.cubeIndexBuffer.SetData<short>(this.cubeIndices);

            //
            // create a VertexBuffer for the transformation matrices used by DrawInstancedPrimitives
            //
            this.instanceVertexBuffer = new DynamicVertexBuffer(GraphicsDevice, instanceDecl, 5, BufferUsage.WriteOnly);
        }

        protected override void UnloadContent()
        {

        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
                Keyboard.GetState().IsKeyDown(Keys.Escape))
                this.Exit();

            rotation += 1.0f * (float)gameTime.ElapsedGameTime.TotalSeconds;

            this.worldMatrix = Matrix.CreateRotationY(rotation);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            this.GraphicsDevice.Viewport = new Viewport(0, 0, 600, 600);

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

            this.GraphicsDevice.RasterizerState = new RasterizerState() { CullMode = CullMode.CullCounterClockwiseFace, FillMode = FillMode.WireFrame };

            this.basicEffect.VertexColorEnabled = true;
            this.basicEffect.View = this.viewMatrix;
            this.basicEffect.World = this.worldMatrix;
            this.basicEffect.Projection = this.projectionMatrix;

            this.basicEffect.CurrentTechnique.Passes[0].Apply();

            #region DrawPrimitives
            GraphicsDevice.Viewport = new Viewport(0, 200, 300, 200);

            GraphicsDevice.SetVertexBuffer(this.cubeNoIndicesBuffer);
            GraphicsDevice.DrawPrimitives(PrimitiveType.TriangleList, 0, cubeNoIndices.Length / 3);

            #endregion // DrawPrimitives

            #region DrawIndexedPrimitives
            GraphicsDevice.Viewport = new Viewport(300, 200, 300, 200);

            GraphicsDevice.SetVertexBuffer(this.cubeVertexBuffer);
            GraphicsDevice.Indices = this.cubeIndexBuffer;
            GraphicsDevice.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, 0, cubeVertices.Length, 0, cubeNoIndices.Length / 3);

            #endregion

            #region DrawUserPrimitives
            GraphicsDevice.Viewport = new Viewport(0, 400, 300, 200);

            GraphicsDevice.DrawUserPrimitives<VertexPositionColor>(PrimitiveType.TriangleList, this.cubeNoIndices, 0, this.cubeNoIndices.Length / 3);

            #endregion

            #region DrawUserIndexedPrimitives
            GraphicsDevice.Viewport = new Viewport(300, 400, 300, 200);

            GraphicsDevice.DrawUserIndexedPrimitives<VertexPositionColor>(PrimitiveType.TriangleList, this.cubeVertices, 0, this.cubeVertices.Length, this.cubeIndices, 0, this.cubeIndices.Length / 3);

            #endregion

            #region DrawInstancedPrimitives
            GraphicsDevice.Viewport = new Viewport(0, 0, 600, 200);

            this.hardwareInstanceEffect.Parameters["View"].SetValue(Matrix.CreateLookAt(new Vector3(0.0f, 5.0f, -10.0f), Vector3.Zero, Vector3.Up));
            this.hardwareInstanceEffect.Parameters["Projection"].SetValue(this.instancedProjectionMatrix);
            this.hardwareInstanceEffect.Parameters["World"].SetValue(this.worldMatrix);
            this.hardwareInstanceEffect.CurrentTechnique.Passes[0].Apply();

            instanceVertexBuffer.SetData<Matrix>(this.instanceTransformMatrices, 0, this.instanceTransformMatrices.Length, SetDataOptions.Discard);
            GraphicsDevice.SetVertexBuffers(cubeVertexBuffer, new VertexBufferBinding(instanceVertexBuffer, 0, 1));
            GraphicsDevice.Indices = this.cubeIndexBuffer;
            GraphicsDevice.DrawInstancedPrimitives(PrimitiveType.TriangleList, 0, 0, this.cubeVertices.Length, 0, this.cubeIndices.Length / 3, 5);

            #endregion

            base.Draw(gameTime);

        }

        private void DrawShadowText(SpriteBatch spriteBatch, SpriteFont font, String text, Vector2 position, Color foreground, Color shadow)
        {
            spriteBatch.DrawString(font, text, position + new Vector2(2, 2), shadow);
            spriteBatch.DrawString(font, text, position, foreground);
        }
    }
}
