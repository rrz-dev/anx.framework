using System;
using ANX.Framework;
using ANX.Framework.Graphics;
using ANX.Framework.Input;

namespace AlphaTestEffectSample
{
    public class Game1 : Game
    {
        private enum AlphaTestMode
        {
            Fog,
            NoFog,
            VertexColorFog,
            VertexColorNoFog,
        }

        GraphicsDeviceManager graphics;
        private AlphaTestEffect effect;

        private Texture2D texture;

        VertexBuffer vertices;
        VertexBuffer verticesVertexColor;
        IndexBuffer indices;

        SpriteBatch spritebatch;
        SpriteFont font;

        AlphaTestMode mode = AlphaTestMode.Fog;

        KeyboardState lastState;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "SampleContent";
        }

        protected override void LoadContent()
        {
            effect = new AlphaTestEffect(GraphicsDevice);
            
            spritebatch = new SpriteBatch(GraphicsDevice);
            font = Content.Load<SpriteFont>("Fonts/Debug");

            texture = Content.Load<Texture2D>("Textures/alpha_test");

            vertices = new VertexBuffer(GraphicsDevice, VertexPositionTexture.VertexDeclaration, 4, BufferUsage.WriteOnly);
            vertices.SetData(new[]
			{
				new VertexPositionTexture(new Vector3(-5f, 0f, -5f), new Vector2(0f, 0f)),
				new VertexPositionTexture(new Vector3(-5f, 0f, 5f), new Vector2(1f, 0f)),
				new VertexPositionTexture(new Vector3(5f, 0f, 5f), new Vector2(1f, 1f)),
				new VertexPositionTexture(new Vector3(5f, 0f, -5f), new Vector2(0f, 1f)),
			});

            verticesVertexColor = new VertexBuffer(GraphicsDevice, VertexPositionColorTexture.VertexDeclaration,
                4, BufferUsage.WriteOnly);
            verticesVertexColor.SetData(new[]
			{
				new VertexPositionColorTexture(new Vector3(-5f, 0f, -5f), Color.Green, new Vector2(0f, 0f)),
				new VertexPositionColorTexture(new Vector3(-5f, 0f, 5f), Color.Green, new Vector2(1f, 0f)),
				new VertexPositionColorTexture(new Vector3(5f, 0f, 5f), Color.Green, new Vector2(1f, 1f)),
				new VertexPositionColorTexture(new Vector3(5f, 0f, -5f), Color.Green, new Vector2(0f, 1f)),
			});
            
            indices = new IndexBuffer(GraphicsDevice, IndexElementSize.ThirtyTwoBits, 6, BufferUsage.WriteOnly);
            indices.SetData(new uint[] { 0, 2, 1, 0, 3, 2 });
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            KeyboardState newState = Keyboard.GetState();

            if (newState.IsKeyDown(Keys.Space) && lastState.IsKeyDown(Keys.Space) == false)
            {
                int currentIndex = (int)mode;
                string[] names = Enum.GetNames(typeof(AlphaTestMode));
                currentIndex++;
                if (currentIndex == names.Length)
                    currentIndex = 0;
                mode = (AlphaTestMode)Enum.Parse(typeof(AlphaTestMode), names[currentIndex]);
            }

            if (newState.IsKeyDown(Keys.F) && lastState.IsKeyDown(Keys.F) == false)
            {
                int currentIndex = (int)effect.AlphaFunction;
                string[] names = Enum.GetNames(typeof(CompareFunction));
                currentIndex++;
                if (currentIndex == names.Length)
                    currentIndex = 0;
                effect.AlphaFunction = (CompareFunction)Enum.Parse(typeof(CompareFunction), names[currentIndex]);
            }

            float alpha = effect.Alpha;
            if(newState.IsKeyDown(Keys.Up))
            {
                alpha += (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            if (newState.IsKeyDown(Keys.Down))
            {
                alpha -= (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            effect.Alpha = MathHelper.Clamp(alpha, 0f, 1f);

            int refAlpha = effect.ReferenceAlpha;
            if (newState.IsKeyDown(Keys.Right))
            {
                refAlpha++;
            }
            if (newState.IsKeyDown(Keys.Left))
            {
                refAlpha--;
            }
            effect.ReferenceAlpha = (int)MathHelper.Clamp(refAlpha, 0, 255);

            lastState = newState;
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            DrawText();
            DrawMesh();
            base.Draw(gameTime);
        }

        private void DrawText()
        {
            spritebatch.Begin();
            spritebatch.DrawString(font, "AlphaTest sample - Current mode: " + mode + " -> Switch with Space\n" +
                "AlphaFunction: " + effect.AlphaFunction + " -> Switch with F\n" +
                "Alpha: " + effect.Alpha + " -> Change with Up/Down\n" +
                "ReferenceAlpha: " + effect.ReferenceAlpha + " -> Change with Left/Right\n",
                new Vector2(5, 5), Color.White);
            spritebatch.End();

            GraphicsDevice.BlendState = BlendState.Opaque;
            GraphicsDevice.DepthStencilState = DepthStencilState.Default;
        }

        private void DrawMesh()
        {
            effect.VertexColorEnabled =
                (mode == AlphaTestMode.VertexColorFog || mode == AlphaTestMode.VertexColorNoFog);

            effect.DiffuseColor = Color.White.ToVector3();

            effect.FogEnabled = (mode == AlphaTestMode.VertexColorFog || mode == AlphaTestMode.Fog);
            effect.FogColor = Color.Gray.ToVector3();
            effect.FogStart = 5f;
            effect.FogEnd = 15f;

            effect.Texture = texture;

            effect.World = Matrix.Identity;
            effect.View = Matrix.CreateLookAt(new Vector3(0f, 5f, -8f), Vector3.Zero, Vector3.Up);
            effect.Projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver4,
                GraphicsDevice.Viewport.AspectRatio, 1f, 100f);
            effect.CurrentTechnique.Passes[0].Apply();

            GraphicsDevice.Indices = indices;
            GraphicsDevice.SetVertexBuffer(effect.VertexColorEnabled ? verticesVertexColor : vertices);

            GraphicsDevice.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, 0, 4, 0, 2);
        }
    }
}
