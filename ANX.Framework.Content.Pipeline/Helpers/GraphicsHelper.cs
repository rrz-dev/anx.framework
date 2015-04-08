using ANX.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ANX.Framework.Content.Pipeline.Helpers
{
    static class GraphicsHelper
    {
        private static GraphicsDevice _refDevice = null;
        private static short[] _indices = new short[] { 0, 1, 2, 1, 3, 2 };

        public static GraphicsDevice ReferenceDevice
        {
            get
            {
                if (_refDevice == null)
                {
                    bool value = GraphicsAdapter.UseReferenceDevice;
                    GraphicsAdapter.UseReferenceDevice = true;

                    IntPtr handle = System.Diagnostics.Process.GetCurrentProcess().MainWindowHandle;
                    _refDevice = new GraphicsDevice(GraphicsAdapter.DefaultAdapter, GraphicsProfile.HiDef, new PresentationParameters() { DeviceWindowHandle = new NonXNA.WindowHandle(handle) });

                    GraphicsAdapter.UseReferenceDevice = value;
                }
                return _refDevice;
            }
        }

        public static void DrawQuad(GraphicsDevice graphicsDevice, Texture2D sourceTexture, Rectangle sourceRegion, RenderTarget2D destinationTexture, Rectangle destinationRegion, TextureFilter filter)
        {
            if (graphicsDevice == null)
                throw new ArgumentNullException("graphicsDevice");

            if (sourceTexture == null)
                throw new ArgumentNullException("sourceTexture");

            if (destinationTexture == null)
                throw new ArgumentNullException("destinationTexture");

            graphicsDevice.SetRenderTarget(destinationTexture);

            using (BasicEffect effect = new BasicEffect(graphicsDevice))
            {
                effect.FogEnabled = false;
                effect.LightingEnabled = false;
                effect.Projection = Matrix.CreateOrthographicOffCenter(destinationRegion.Left, destinationRegion.Right, destinationRegion.Bottom, destinationRegion.Top, 0f, 1f);
                effect.TextureEnabled = true;

                VertexPositionTexture[] vertices = new VertexPositionTexture[] 
                    { 
                        new VertexPositionTexture(new Vector3(0, 0, 0), new Vector2((float)sourceRegion.Left / sourceTexture.Width, (float)sourceRegion.Top / sourceTexture.Height)), 
                        new VertexPositionTexture(new Vector3(1, 0, 0), new Vector2((float)sourceRegion.Right / sourceTexture.Width, (float)sourceRegion.Top / sourceTexture.Height)),
                        new VertexPositionTexture(new Vector3(0, 1, 0), new Vector2((float)sourceRegion.Left / sourceTexture.Width, (float)sourceRegion.Bottom / sourceTexture.Height)),
                        new VertexPositionTexture(new Vector3(1, 1, 0), new Vector2((float)sourceRegion.Right / sourceTexture.Width, (float)sourceRegion.Bottom / sourceTexture.Height))
                    };

                foreach (var pass in effect.CurrentTechnique.Passes)
                {
                    pass.Apply();

                    graphicsDevice.SamplerStates[0].Filter = filter;
                    graphicsDevice.DrawUserIndexedPrimitives(PrimitiveType.TriangleList, vertices, 0, 4, _indices, 0, 2);
                }
            }
        }

        public static void Convert(GraphicsDevice graphicsDevice, byte[] sourcePixels, SurfaceFormat destinationFormat)
        {
            //convert from not compressed to compressed.
            throw new NotImplementedException();
        }
    }
}
