#region Using Statements
using System;
using ANX.Framework;
using ANX.Framework.Graphics;

#endregion // Using Statements

namespace StencilBuffer
{
    public class Game1 : Game
    {
        private readonly GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private Texture2D crate;
        private Texture2D ground;

        protected static SamplerState SamplerState = new SamplerState()
        {
            AddressU = TextureAddressMode.Wrap,
            AddressV = TextureAddressMode.Wrap,
            AddressW = TextureAddressMode.Wrap,
            Filter = TextureFilter.Linear,
        };

        private static readonly DepthStencilState RenderGroundStencilState = new DepthStencilState()
        {
            DepthBufferEnable = false,
            DepthBufferWriteEnable = false,
            StencilEnable = true,
            ReferenceStencil = 1,
            StencilPass = StencilOperation.Replace,
            StencilFunction = CompareFunction.Always,
        };

        private static readonly DepthStencilState RenderObjectsStencilState = new DepthStencilState()
        {
            DepthBufferEnable = true,
            DepthBufferWriteEnable = true,
            DepthBufferFunction = CompareFunction.Always,
            ReferenceStencil = 1,
            StencilEnable = false,
            StencilPass = StencilOperation.Replace,
        };

        private static readonly DepthStencilState StencilStateRenderShadows = new DepthStencilState
        {
            DepthBufferEnable = false,
            StencilEnable = true,
            ReferenceStencil = 1,
            StencilPass = StencilOperation.Increment,
            StencilFunction = CompareFunction.LessEqual,
        };

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "SampleContent";
        }

        protected override void Initialize()
        {
            //graphics.PreferredDepthStencilFormat = DepthFormat.Depth24Stencil8;
            //graphics.ApplyChanges();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            crate = Content.Load<Texture2D>(@"Textures/chest");
            ground = Content.Load<Texture2D>(@"Textures/stone_tile");
        }

        private void RenderObjects()
        {
            spriteBatch.Begin(SpriteSortMode.Texture, null, SamplerState, RenderObjectsStencilState, null);
            spriteBatch.Draw(crate, new Vector2(100, 100), Color.White);
            spriteBatch.Draw(crate, new Vector2(-15, -15), Color.White);
            spriteBatch.End();
        }

        private void RenderGround()
        {
            spriteBatch.Begin(SpriteSortMode.Texture, null, SamplerState, RenderGroundStencilState, null);
            for (int y = 0; y < 2; y++)
            {
                for (int x = 0; x < 4; x++)
                {
                    spriteBatch.Draw(ground, new Vector2(x * 255, y * 255), Color.White);
                }
            }
            spriteBatch.End();
        }

        private void RenderShadows()
        {
            spriteBatch.Begin(SpriteSortMode.Texture, null, SamplerState, StencilStateRenderShadows, null);
            spriteBatch.Draw(crate, new Vector2(125, 125), new Color(0, 0, 0, 0.5f));
            spriteBatch.Draw(crate, new Vector2(10, 10), new Color(0, 0, 0, 0.5f));
            spriteBatch.End();
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(ClearOptions.Target | ClearOptions.Stencil, Color.CornflowerBlue, 1.0f, 0);

            RenderGround();
            RenderShadows();
            RenderObjects();
            
            base.Draw(gameTime);
        }
    }
}
