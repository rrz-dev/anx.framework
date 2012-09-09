using System;
using ANX.Framework;
using ANX.Framework.Input;
using ANX.Framework.Graphics;

namespace DualTextureSample
{
	public class Game1 : Game
	{
		private enum DualTextureMode
		{
			Fog,
			NoFog,
			VertexColorFog,
			VertexColorNoFog,
		}

		GraphicsDeviceManager graphics;

		DualTextureEffect dualTextureEffect;
		Texture2D texture1;
		Texture2D texture2;

		VertexBuffer vertices;
		VertexBuffer verticesVertexColor;
		IndexBuffer indices;

		SpriteBatch spritebatch;
		SpriteFont font;

		DualTextureMode mode = DualTextureMode.Fog;

		KeyboardState lastState;

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
			dualTextureEffect = new DualTextureEffect(GraphicsDevice);

			spritebatch = new SpriteBatch(GraphicsDevice);
			font = Content.Load<SpriteFont>("Fonts/Debug");

			texture1 = Content.Load<Texture2D>("Textures/stone_tile");
			texture2 = Content.Load<Texture2D>("Textures/lightmap");

			vertices = new VertexBuffer(GraphicsDevice, VertexDualTexture.VertexDeclaration, 4, BufferUsage.WriteOnly);
			vertices.SetData(new VertexDualTexture[]
			{
				new VertexDualTexture(new Vector3(-5f, 0f, -5f), new Vector2(0f, 0f), new Vector2(0f, 0f)),
				new VertexDualTexture(new Vector3(-5f, 0f, 5f), new Vector2(1f, 0f), new Vector2(1f, 0f)),
				new VertexDualTexture(new Vector3(5f, 0f, 5f), new Vector2(1f, 1f), new Vector2(1f, 1f)),
				new VertexDualTexture(new Vector3(5f, 0f, -5f), new Vector2(0f, 1f), new Vector2(0f, 1f)),
			});

			verticesVertexColor = new VertexBuffer(GraphicsDevice, VertexDualTextureColor.VertexDeclaration,
				4, BufferUsage.WriteOnly);
			verticesVertexColor.SetData(new VertexDualTextureColor[]
			{
				new VertexDualTextureColor(new Vector3(-5f, 0f, -5f), new Vector2(0f, 0f), new Vector2(0f, 0f), Color.Green),
				new VertexDualTextureColor(new Vector3(-5f, 0f, 5f), new Vector2(1f, 0f), new Vector2(1f, 0f), Color.Green),
				new VertexDualTextureColor(new Vector3(5f, 0f, 5f), new Vector2(1f, 1f), new Vector2(1f, 1f), Color.Green),
				new VertexDualTextureColor(new Vector3(5f, 0f, -5f), new Vector2(0f, 1f), new Vector2(0f, 1f), Color.Green),
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
			if(newState.IsKeyDown(Keys.Space) && lastState.IsKeyDown(Keys.Space) == false)
			{
				int currentIndex = (int)mode;
				string[] names = Enum.GetNames(typeof(DualTextureMode));
				currentIndex++;
				if(currentIndex == names.Length)
					currentIndex = 0;
				mode = (DualTextureMode)Enum.Parse(typeof(DualTextureMode), names[currentIndex]);
			}
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
			spritebatch.DrawString(font, "DualTexture sample - Current mode: " + mode + "\nSwitch with Space",
				new Vector2(5, 5), Color.White);
			spritebatch.End();

			GraphicsDevice.BlendState = BlendState.Opaque;
			GraphicsDevice.DepthStencilState = DepthStencilState.Default;
		}

		private void DrawMesh()
		{
			dualTextureEffect.VertexColorEnabled =
				(mode == DualTextureMode.VertexColorFog || mode == DualTextureMode.VertexColorNoFog);

			dualTextureEffect.DiffuseColor = Color.White.ToVector3();

			dualTextureEffect.FogEnabled = (mode == DualTextureMode.VertexColorFog || mode == DualTextureMode.Fog);
			dualTextureEffect.FogColor = Color.Gray.ToVector3();
			dualTextureEffect.FogStart = 5f;
			dualTextureEffect.FogEnd = 15f;
			dualTextureEffect.Alpha = 1f;

			dualTextureEffect.Texture = texture1;
			dualTextureEffect.Texture2 = texture2;

			dualTextureEffect.World = Matrix.Identity;
			dualTextureEffect.View = Matrix.CreateLookAt(new Vector3(0f, 5f, -8f), Vector3.Zero, Vector3.Up);
			dualTextureEffect.Projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver4,
				GraphicsDevice.Viewport.AspectRatio, 1f, 100f);
			dualTextureEffect.CurrentTechnique.Passes[0].Apply();

			GraphicsDevice.Indices = indices;
			if (dualTextureEffect.VertexColorEnabled)
				GraphicsDevice.SetVertexBuffer(verticesVertexColor);
			else
				GraphicsDevice.SetVertexBuffer(vertices);

			GraphicsDevice.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, 0, 4, 0, 2);
		}
	}
}
