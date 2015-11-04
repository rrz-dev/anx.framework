using ANX.Framework;
using ANX.Framework.Graphics;
using ANX.Framework.Input;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ModelSample
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Model cubeModel;
        Matrix cubeWorld;
        Model anxLogo;
        Matrix anxLogoWorld;
        Effect effect;
        Texture2D texture;
        bool overrideWithSimpleEffect = true;

        Matrix view;
        Matrix projection;

        Matrix worldViewProj;
        Matrix worldInverseTranspose;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "SampleContent";
        }

        protected override void Initialize()
        {
            float aspect = GraphicsDevice.Viewport.AspectRatio;

            view = Matrix.CreateLookAt(Vector3.Backward * 10, Matrix.Identity.Translation, Vector3.Up);
            projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver4, aspect, 1f, 100f);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            effect = Content.Load<Effect>("Effects/SimpleEffect");
            
            cubeModel = Content.Load<Model>("Models/SphereAndCube");
            cubeWorld = Matrix.Identity;

            anxLogo = Content.Load<Model>("Models/ANX_vertex_color");
            anxLogoWorld = Matrix.CreateWorld(new Vector3(10, 10, 10), Vector3.Forward, Vector3.Up);

            texture = Content.Load<Texture2D>("Textures/Test_100x100");

            // Ovrride the basic effect in the model for testing
            foreach (var mesh in cubeModel.Meshes)
            {
                foreach (var part in mesh.MeshParts)
                {
                    if (overrideWithSimpleEffect)
                    {
                        part.Effect = effect;
                    } 
                    else 
                    {
                        ((BasicEffect)part.Effect).TextureEnabled = true;
                        ((BasicEffect)part.Effect).EnableDefaultLighting();
                    }
                }
            }
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                this.Exit();
            }

            const float speed = 0.25f;
            float rotation = (float)gameTime.TotalGameTime.TotalSeconds * speed;
            cubeWorld = Matrix.CreateRotationX(rotation) * Matrix.CreateRotationY(rotation) * Matrix.CreateRotationZ(rotation);

            worldViewProj = cubeWorld * view * projection;
            worldInverseTranspose = Matrix.Transpose(Matrix.Invert(cubeWorld));

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            if (overrideWithSimpleEffect)
            {
                this.effect.Parameters["World"].SetValue(this.cubeWorld);
                this.effect.Parameters["View"].SetValue(this.view);
                this.effect.Parameters["Projection"].SetValue(this.projection);
                this.effect.Parameters["WorldViewProj"].SetValue(this.worldViewProj);
                this.effect.Parameters["WorldInverseTranspose"].SetValue(this.worldInverseTranspose);
                this.effect.Parameters["Texture"].SetValue(this.texture);
            }
			
            cubeModel.Draw(cubeWorld, view, projection);
            base.Draw(gameTime);
        }
    }
}
