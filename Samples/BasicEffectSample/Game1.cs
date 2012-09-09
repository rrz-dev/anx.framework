using ANX.Framework;
using ANX.Framework.Graphics;
using ANX.Framework.Input;
using BasicEffectSample.Scenes;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace BasicEffectSample
{
	public class Game1 : Game
	{
		GraphicsDeviceManager graphics;

		SpriteBatch spriteBatch;
		SpriteFont font;

		KeyboardState lastState;

		BaseScene[] allScenes =
		{
			// Basic
			new DiffuseNoFogScene(),
			new DiffuseFogScene(),
			new VertexColorNoFogScene(),
			new VertexColorFogScene(),
			new TextureNoFogScene(),
			new TextureFogScene(),
			new VertexColorTextureNoFogScene(),
			new VertexColorTextureFogScene(),

			// Vertex Lighting
			new VertexLightingDiffuseNoFogScene(),
			new VertexLightingDiffuseFogScene(),
			//new VertexLightingVertexColorNoFogScene(),
			//new VertexLightingVertexColorFogScene(),
			//new VertexLightingTextureNoFogScene(),
			//new VertexLightingTextureFogScene(),
			//new VertexLightingVertexColorTextureNoFogScene(),
			//new VertexLightingVertexColorTextureFogScene(),
			
			// One Light
			//new OneLightNoFogScene(),
			//new OneLightFogScene(),
			//new OneLightVertexColorNoFogScene(),
			//new OneLightVertexColorFogScene(),
			//new OneLightTextureNoFogScene(),
			//new OneLightTextureFogScene(),
			//new OneLightVertexColorTextureNoFogScene(),
			//new OneLightVertexColorTextureFogScene(),
			
			// Pixel Lighting
			//new PixelLightingNoFogScene(),
			//new PixelLightingFogScene(),
			//new PixelLightingVertexColorNoFogScene(),
			//new PixelLightingVertexColorFogScene(),
			//new PixelLightingTextureNoFogScene(),
			//new PixelLightingTextureFogScene(),
			//new PixelLightingVertexColorTextureNoFogScene(),
			//new PixelLightingVertexColorTextureFogScene(),
		};

		int currentSceneIndex = 0;

		public Game1()
		{
			graphics = new GraphicsDeviceManager(this);
			Content.RootDirectory = "SampleContent";
		}

		protected override void Initialize()
		{
			Camera.Initialize(GraphicsDevice);
			base.Initialize();
		}

		protected override void LoadContent()
		{
			spriteBatch = new SpriteBatch(GraphicsDevice);
			font = Content.Load<SpriteFont>("Fonts/Debug");

			foreach (var scene in allScenes)
				scene.Initialize(Content, GraphicsDevice);
		}

		protected override void UnloadContent()
		{
		}

		protected override void Update(GameTime gameTime)
		{
			KeyboardState newState = Keyboard.GetState();
			if (newState.IsKeyDown(Keys.Left) && lastState.IsKeyDown(Keys.Left) == false)
			{
				currentSceneIndex--;
				if (currentSceneIndex == -1)
					currentSceneIndex = allScenes.Length - 1;
			}
			else if (newState.IsKeyDown(Keys.Right) && lastState.IsKeyDown(Keys.Right) == false)
			{
				currentSceneIndex++;
				if (currentSceneIndex == allScenes.Length)
					currentSceneIndex = 0;
			}
			lastState = newState;
			base.Update(gameTime);
		}

		protected override void Draw(GameTime gameTime)
		{
			GraphicsDevice.Clear(Color.CornflowerBlue);

			spriteBatch.Begin();
			spriteBatch.DrawString(font, "Current scene: " + allScenes[currentSceneIndex].Name + "\n<- Left | Right ->",
				new Vector2(5, 5), Color.White);
			spriteBatch.End();

			GraphicsDevice.BlendState = BlendState.Opaque;
			GraphicsDevice.DepthStencilState = DepthStencilState.Default;

			allScenes[currentSceneIndex].Draw(GraphicsDevice);

			base.Draw(gameTime);
		}
	}
}
