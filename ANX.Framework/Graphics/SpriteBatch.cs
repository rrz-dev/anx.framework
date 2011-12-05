#region Using Statements
using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using ANX.Framework.NonXNA;

#endregion // Using Statements

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

namespace ANX.Framework.Graphics
{
    public class SpriteBatch : GraphicsResource
    {
        #region Private Members
        private const int InitialBatchSize = 1024;

        private Effect spriteBatchEffect;
        private bool hasBegun;
        private SpriteSortMode currentSortMode;
        private SpriteInfo[] spriteInfos;
        private int currentBatchPosition;
        private DynamicIndexBuffer indexBuffer;
        private VertexPositionColorTexture[] vertices;
        private DynamicVertexBuffer vertexBuffer;

        private BlendState blendState;
        private SamplerState samplerState;
        private DepthStencilState depthStencilState;
        private RasterizerState rasterizerState;
        private Effect effect;
        private Matrix transformMatrix;

        private float lastRotation;
        private Matrix cachedRotationMatrix;

        private int viewportWidth;
        private int viewportHeight;
        private Matrix cachedTransformMatrix;

        private static TextureComparer textureComparer = new TextureComparer();
        private static FrontToBackComparer frontToBackComparer = new FrontToBackComparer();
        private static BackToFrontComparer backToFrontComparer = new BackToFrontComparer();

        #endregion // Private Members

        public SpriteBatch(GraphicsDevice graphicsDevice)
        {
            if (graphicsDevice == null)
            {
                throw new ArgumentNullException("graphicsDevice");
            }

            base.GraphicsDevice = graphicsDevice;

            this.spriteBatchEffect = new Effect(graphicsDevice, AddInSystemFactory.Instance.GetDefaultCreator<IRenderSystemCreator>().GetShaderByteCode(NonXNA.PreDefinedShader.SpriteBatch));

            this.spriteInfos = new SpriteInfo[InitialBatchSize];

            this.InitializeIndexBuffer(InitialBatchSize);

            this.InitializeVertexBuffer();
        }

        #region Begin-Method
        public void Begin()
        {
            Begin(SpriteSortMode.Texture, null, null, null, null, null, Matrix.Identity);
        }

        public void Begin(SpriteSortMode sortMode, BlendState blendState)
        {
            Begin(sortMode, blendState, null, null, null, null, Matrix.Identity);
        }

        public void Begin(SpriteSortMode sortMode, BlendState blendState, SamplerState samplerState, DepthStencilState depthStencilState, RasterizerState rasterizerState)
        {
            Begin(sortMode, blendState, samplerState, depthStencilState, rasterizerState, null, Matrix.Identity);
        }

        public void Begin(SpriteSortMode sortMode, BlendState blendState, SamplerState samplerState, DepthStencilState depthStencilState, RasterizerState rasterizerState, Effect effect)
        {
            Begin(sortMode, blendState, samplerState, depthStencilState, rasterizerState, effect, Matrix.Identity);
        }

        public void Begin(SpriteSortMode sortMode, BlendState blendState, SamplerState samplerState, DepthStencilState depthStencilState, RasterizerState rasterizerState, Effect effect, Matrix transformMatrix)
        {
            if (this.hasBegun == true)
            {
                throw new Exception("End() has to be called before a new SpriteBatch can be started with Begin()");
            }

            hasBegun = true;

            this.currentSortMode = sortMode;

            this.blendState = blendState;
            this.samplerState = samplerState;
            this.depthStencilState = depthStencilState;
            this.rasterizerState = rasterizerState;
            this.effect = effect;
            this.transformMatrix = transformMatrix;
        }

        #endregion // Begin-Method

        #region Draw-Method
        public void Draw (Texture2D texture, Rectangle destinationRectangle, Color color)
        {
            Draw(texture, new Vector2(destinationRectangle.X, destinationRectangle.Y), new Vector2(destinationRectangle.Width, destinationRectangle.Height), null, color, Vector2.Zero, 0.0f, 0.0f, Vector2.One, SpriteEffects.None);
        }

        public void Draw (Texture2D texture, Rectangle destinationRectangle, Nullable<Rectangle> sourceRectangle, Color color)
        {
            Draw(texture, new Vector2(destinationRectangle.X, destinationRectangle.Y), new Vector2(destinationRectangle.Width, destinationRectangle.Height), sourceRectangle, color, Vector2.Zero, 0.0f, 0.0f, Vector2.One, SpriteEffects.None);
        }

        public void Draw(Texture2D texture, Rectangle destinationRectangle, Nullable<Rectangle> sourceRectangle, Color color, Single rotation, Vector2 origin, SpriteEffects effects, Single layerDepth)
        {
            Draw(texture, new Vector2(destinationRectangle.X, destinationRectangle.Y), new Vector2(destinationRectangle.Width, destinationRectangle.Height), sourceRectangle, color, origin, layerDepth, rotation, Vector2.One, effects);
        }

        public void Draw(Texture2D texture, Vector2 position, Color color)
        {
            Draw(texture, position, new Vector2(texture.Width, texture.Height), null, color, Vector2.Zero, 0.0f, 0.0f, Vector2.One, SpriteEffects.None);
        }

        public void Draw(Texture2D texture, Vector2 position, Nullable<Rectangle> sourceRectangle, Color color)
        {
            Vector2 size = sourceRectangle.HasValue ? new Vector2(sourceRectangle.Value.Width, sourceRectangle.Value.Height) : new Vector2(texture.Width, texture.Height);
            Draw(texture, position, size, sourceRectangle, color, Vector2.Zero, 0.0f, 0.0f, Vector2.One, SpriteEffects.None);
        }

        public void Draw(Texture2D texture, Vector2 position, Nullable<Rectangle> sourceRectangle, Color color, Single rotation, Vector2 origin, Single scale, SpriteEffects effects, Single layerDepth)
        {
            Vector2 size = sourceRectangle.HasValue ? new Vector2(sourceRectangle.Value.Width, sourceRectangle.Value.Height) : new Vector2(texture.Width, texture.Height);
            Draw(texture, position, size, sourceRectangle, color, origin, layerDepth, rotation, new Vector2(scale), effects);
        }

        public void Draw(Texture2D texture, Vector2 position, Nullable<Rectangle> sourceRectangle, Color color, Single rotation, Vector2 origin, Vector2 scale, SpriteEffects effects, Single layerDepth)
        {
            Vector2 size = sourceRectangle.HasValue ? new Vector2(sourceRectangle.Value.Width, sourceRectangle.Value.Height) : new Vector2(texture.Width, texture.Height);
            Draw(texture, position, size, sourceRectangle, color, origin, layerDepth, rotation, scale, effects);
        }

        #endregion // Draw-Method

        #region DrawString-Method
        public void DrawString(SpriteFont font, String text, Vector2 position, Color color)
        {
            if (font == null)
            {
                throw new ArgumentNullException("font");
            }

            if (text == null)
            {
                throw new ArgumentNullException("text");
            }

            font.DrawString(ref text, this, position, color, Vector2.One, Vector2.Zero, 0.0f, 1.0f, SpriteEffects.None);
        }

        public void DrawString(SpriteFont font, String text, Vector2 position, Color color, Single rotation, Vector2 origin, Single scale, SpriteEffects effects, Single layerDepth)
        {
            if (font == null)
            {
                throw new ArgumentNullException("font");
            }

            if (text == null)
            {
                throw new ArgumentNullException("text");
            }

            font.DrawString(ref text, this, position, color, new Vector2(scale), origin, rotation, layerDepth, effects);
        }

        public void DrawString(SpriteFont font, String text, Vector2 position, Color color, Single rotation, Vector2 origin, Vector2 scale, SpriteEffects effects, Single layerDepth)
        {
            if (font == null)
            {
                throw new ArgumentNullException("font");
            }

            if (text == null)
            {
                throw new ArgumentNullException("text");
            }

            font.DrawString(ref text, this, position, color, scale, origin, rotation, layerDepth, effects);
        }

        public void DrawString (SpriteFont font, StringBuilder text, Vector2 position, Color color)
        {
            if (font == null)
            {
                throw new ArgumentNullException("font");
            }

            if (text == null)
            {
                throw new ArgumentNullException("text");
            }

            DrawString(font, text.ToString(), position, color);
        }

        public void DrawString (SpriteFont font, StringBuilder text, Vector2 position, Color color, Single rotation, Vector2 origin, Single scale, SpriteEffects effects, Single layerDepth)
        {
            if (font == null)
            {
                throw new ArgumentNullException("font");
            }

            if (text == null)
            {
                throw new ArgumentNullException("text");
            }

            DrawString(font, text.ToString(), position, color, rotation, origin, scale, effects, layerDepth);
        }

        public void DrawString (SpriteFont font, StringBuilder text, Vector2 position, Color color, Single rotation, Vector2 origin, Vector2 scale, SpriteEffects effects, Single layerDepth)
        {
            if (font == null)
            {
                throw new ArgumentNullException("font");
            }

            if (text == null)
            {
                throw new ArgumentNullException("text");
            }

            DrawString(font, text.ToString(), position, color, rotation, origin, scale, effects, layerDepth);
        }

        #endregion // DrawString-Method

        public void End()
        {
            if (hasBegun == false)
            {
                throw new Exception("Begin() has to be called before End()");
            }
            
            hasBegun = false;

            if (this.currentSortMode != SpriteSortMode.Immediate)
            {
                if (this.currentSortMode == SpriteSortMode.Texture)
                {
                    Array.Sort<SpriteInfo>(spriteInfos, 0, currentBatchPosition, textureComparer);
                }
                else if (this.currentSortMode == SpriteSortMode.BackToFront)
                {
                    Array.Sort<SpriteInfo>(spriteInfos, 0, currentBatchPosition, backToFrontComparer);
                }
                else if (this.currentSortMode == SpriteSortMode.FrontToBack)
                {
                    Array.Sort<SpriteInfo>(spriteInfos, 0, currentBatchPosition, frontToBackComparer);
                }

                int startOffset = 0;
                Texture2D lastTexture = null;
                for (int i = 0; i <= currentBatchPosition; i++)
                {
                    if ((lastTexture != null && lastTexture != spriteInfos[i].texture) || i == currentBatchPosition - 1)
                    {
                        BatchRender(startOffset, i - startOffset);
                        startOffset = i;
                    }

                    lastTexture = spriteInfos[i].texture;
                }
            }

            Flush();
        }

        private void Draw(Texture2D texture, Vector2 topLeft, Vector2 destinationSize, Rectangle? sourceRectangle, Color tint, Vector2 origin, float layerDepth, float rotation, Vector2 scale, SpriteEffects effects)
        {
            if (!hasBegun)
            {
                throw new InvalidOperationException("Begin() must be called before Draw()");
            }

            if (texture == null)
            {
                throw new ArgumentNullException("texture");
            }

            if (currentBatchPosition >= spriteInfos.Length)
            {
                int newSize = spriteInfos.Length * 2;
                Array.Resize<SpriteInfo>(ref spriteInfos, newSize);
                InitializeIndexBuffer(newSize);
            }

            Vector2 bottomRight = topLeft + (destinationSize * scale);

            spriteInfos[currentBatchPosition].Corners = new Vector2[] { topLeft, new Vector2(bottomRight.X, topLeft.Y), bottomRight, new Vector2(topLeft.X, bottomRight.Y) };

            spriteInfos[currentBatchPosition].Tint = tint;
            spriteInfos[currentBatchPosition].texture = texture;

            if (sourceRectangle.HasValue)
            {
                spriteInfos[currentBatchPosition].topLeftUV = new Vector2(sourceRectangle.Value.X / (float)texture.Width, sourceRectangle.Value.Y / (float)texture.Height);
                spriteInfos[currentBatchPosition].bottomRightUV = new Vector2((sourceRectangle.Value.X + sourceRectangle.Value.Width) / (float)texture.Width, (sourceRectangle.Value.Y + sourceRectangle.Value.Height) / (float)texture.Height);
            }
            else
            {
                spriteInfos[currentBatchPosition].topLeftUV = Vector2.Zero;
                spriteInfos[currentBatchPosition].bottomRightUV = Vector2.One;
            }

            if ((effects & SpriteEffects.FlipHorizontally) == SpriteEffects.FlipHorizontally)
            {
                float tempY = spriteInfos[currentBatchPosition].bottomRightUV.Y;
                spriteInfos[currentBatchPosition].bottomRightUV.Y = spriteInfos[currentBatchPosition].topLeftUV.Y;
                spriteInfos[currentBatchPosition].topLeftUV.Y = tempY;
            }

            if ((effects & SpriteEffects.FlipVertically) == SpriteEffects.FlipVertically)
            {
                float tempX = spriteInfos[currentBatchPosition].bottomRightUV.X;
                spriteInfos[currentBatchPosition].bottomRightUV.X = spriteInfos[currentBatchPosition].topLeftUV.X;
                spriteInfos[currentBatchPosition].topLeftUV.X = tempX;
            }

            if (rotation != 0f)
            {
                if (lastRotation != rotation || cachedRotationMatrix == null)
                {
                    this.cachedRotationMatrix = Matrix.CreateRotationZ(rotation);
                    this.lastRotation = rotation;
                }

                Vector2 transformVector;
                Vector2 result;
                for (int i = 0; i < 4; i++)
                {
                    transformVector = spriteInfos[currentBatchPosition].Corners[i] - topLeft - origin;
                    Vector2.Transform(ref transformVector, ref this.cachedRotationMatrix, out result);
                    spriteInfos[currentBatchPosition].Corners[i] = result + topLeft + origin;
                }
            }

            spriteInfos[currentBatchPosition].origin = origin;
            spriteInfos[currentBatchPosition].rotation = rotation;
            spriteInfos[currentBatchPosition].layerDepth = layerDepth;

            currentBatchPosition++;

            if (this.currentSortMode == SpriteSortMode.Immediate)
            {
                BatchRender(0, 1);
                Flush();
            }
        }

        private void BatchRender(int offset, int count)
        {
            int vertexCount = count * 4;

            if (this.vertices == null)
            {
                this.vertices = new VertexPositionColorTexture[vertexCount];
            }
            else if (this.vertices.Length < vertexCount)
            {
                Array.Resize<VertexPositionColorTexture>(ref this.vertices, vertexCount);
            }

            int vertexPos = 0;
            for (int i = offset; i < offset + count; i++)
            {
                SpriteInfo currentSprite = this.spriteInfos[i];

                this.vertices[vertexPos + 0] = new VertexPositionColorTexture() 
                { 
                    Position = new Vector3(currentSprite.Corners[0], currentSprite.layerDepth), 
                    Color = currentSprite.Tint, 
                    TextureCoordinate = currentSprite.topLeftUV 
                };

                this.vertices[vertexPos + 1] = new VertexPositionColorTexture() 
                {
                    Position = new Vector3(currentSprite.Corners[1], currentSprite.layerDepth), 
                    Color = currentSprite.Tint, 
                    TextureCoordinate = new Vector2(currentSprite.bottomRightUV.X, currentSprite.topLeftUV.Y) 
                };

                this.vertices[vertexPos + 2] = new VertexPositionColorTexture() 
                {
                    Position = new Vector3(currentSprite.Corners[2], currentSprite.layerDepth), 
                    Color = currentSprite.Tint, 
                    TextureCoordinate = currentSprite.bottomRightUV
                };

                this.vertices[vertexPos + 3] = new VertexPositionColorTexture() 
                {
                    Position = new Vector3(currentSprite.Corners[3], currentSprite.layerDepth), 
                    Color = currentSprite.Tint, 
                    TextureCoordinate = new Vector2(currentSprite.topLeftUV.X, currentSprite.bottomRightUV.Y) 
                };

                vertexPos += 4;
            }

            this.vertexBuffer.SetData<VertexPositionColorTexture>(this.vertices, 0, vertexCount);

            SetRenderStates();

            spriteBatchEffect.Parameters["Texture"].SetValue(this.spriteInfos[offset].texture);

            GraphicsDevice.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, 0, vertexCount, 0, count * 2);
        }

        private void Flush()
        {
            currentBatchPosition = 0;
        }

        private void InitializeIndexBuffer(int size)
        {
            int indexCount = size * 6;

            if (this.indexBuffer == null)
            {
                this.indexBuffer = new DynamicIndexBuffer(this.GraphicsDevice, IndexElementSize.SixteenBits, indexCount, BufferUsage.WriteOnly);
                this.indexBuffer.ContentLost += new EventHandler<EventArgs>(indexBuffer_ContentLost);
            }

            SetIndexData(indexCount);
        }

        private void indexBuffer_ContentLost(object sender, EventArgs e)
        {
            if (this.indexBuffer != null)
            {
                this.indexBuffer.ContentLost -= indexBuffer_ContentLost;
                this.indexBuffer.Dispose();
                this.indexBuffer = null;
            }

            InitializeIndexBuffer(InitialBatchSize);
        }

        private void SetIndexData(int indexCount)
        {
            short[] indices = new short[indexCount];

            int baseIndex;
            int baseArrayIndex;
            for (int i = 0; i < InitialBatchSize; i++)
            {
                baseIndex = i * 4;
                baseArrayIndex = baseIndex + i + i;

                indices[baseArrayIndex + 0] = (short)(baseIndex + 0);
                indices[baseArrayIndex + 1] = (short)(baseIndex + 1);
                indices[baseArrayIndex + 2] = (short)(baseIndex + 2);
                indices[baseArrayIndex + 3] = (short)(baseIndex + 0);
                indices[baseArrayIndex + 4] = (short)(baseIndex + 2);
                indices[baseArrayIndex + 5] = (short)(baseIndex + 3);
            }

            this.indexBuffer.SetData<short>(indices);
        }

        private void InitializeVertexBuffer()
        {
            if (this.vertexBuffer == null)
            {
                this.vertexBuffer = new DynamicVertexBuffer(this.GraphicsDevice, typeof(VertexPositionColorTexture), InitialBatchSize * 4, BufferUsage.WriteOnly);
                this.vertexBuffer.ContentLost += new EventHandler<EventArgs>(vertexBuffer_ContentLost);
            }
        }

        private void vertexBuffer_ContentLost(object sender, EventArgs e)
        {
            this.currentBatchPosition = 0;

            if (this.vertexBuffer != null)
            {
                this.vertexBuffer.ContentLost -= vertexBuffer_ContentLost;
                this.vertexBuffer.Dispose();
                this.vertexBuffer = null;
            }

            InitializeVertexBuffer();
        }

        private void SetRenderStates()
        {
            GraphicsDevice.BlendState = this.blendState != null ? this.blendState : BlendState.AlphaBlend;
            GraphicsDevice.DepthStencilState = this.depthStencilState != null ? this.depthStencilState : DepthStencilState.None;
            GraphicsDevice.RasterizerState = this.rasterizerState != null ? this.rasterizerState : RasterizerState.CullCounterClockwise;
            GraphicsDevice.SamplerStates[0] = this.samplerState != null ? this.samplerState : SamplerState.LinearClamp;

            if (cachedTransformMatrix == null || GraphicsDevice.Viewport.Width != viewportWidth || GraphicsDevice.Viewport.Height != viewportHeight)
            {
                this.viewportWidth = GraphicsDevice.Viewport.Width;
                this.viewportHeight = GraphicsDevice.Viewport.Height;

                cachedTransformMatrix = new Matrix()
                {
                    M11 = 2f * (this.viewportWidth > 0 ? 1f / (float)this.viewportWidth : 0f),
                    M22 = 2f * (this.viewportHeight > 0 ? -1f / (float)this.viewportHeight : 0f),
                    M33 = 1f,
                    M44 = 1f,
                    M41 = -1f,
                    M42 = 1f
                };

                cachedTransformMatrix.M41 -= cachedTransformMatrix.M11;
                cachedTransformMatrix.M42 -= cachedTransformMatrix.M22;
            }

            this.spriteBatchEffect.Parameters["MatrixTransform"].SetValue(this.transformMatrix * cachedTransformMatrix);
            spriteBatchEffect.NativeEffect.Apply(GraphicsDevice);

            GraphicsDevice.Indices = this.indexBuffer;
            GraphicsDevice.SetVertexBuffer(this.vertexBuffer);
        }

        public override void Dispose()
        {
            if (this.spriteBatchEffect != null)
            {
                this.spriteBatchEffect.Dispose();
                this.spriteBatchEffect = null;
            }

            if (this.indexBuffer != null)
            {
                this.indexBuffer.Dispose();
                this.indexBuffer = null;
            }

            if (this.vertexBuffer != null)
            {
                this.vertexBuffer.Dispose();
                this.vertexBuffer = null;
            }
        }

        protected override void Dispose(Boolean disposeManaged)
        {
            throw new NotImplementedException();
        }

        private class TextureComparer : IComparer<SpriteInfo>
        {
            public int Compare(SpriteInfo x, SpriteInfo y)
            {
                if (x.texture.GetHashCode() > y.texture.GetHashCode())
                {
                    return -1;
                }
                else if (x.texture.GetHashCode() < y.texture.GetHashCode())
                {
                    return 1;
                }

                return 0;
            }
        }

        private class FrontToBackComparer : IComparer<SpriteInfo>
        {
            public int Compare(SpriteInfo x, SpriteInfo y)
            {
                if (x.layerDepth > y.layerDepth)
                {
                    return 1;
                }
                else if (x.layerDepth < y.layerDepth)
                {
                    return -1;
                }

                return 0;
            }
        }

        private class BackToFrontComparer : IComparer<SpriteInfo>
        {
            public int Compare(SpriteInfo x, SpriteInfo y)
            {
                if (x.layerDepth > y.layerDepth)
                {
                    return -1;
                }
                else if (x.layerDepth < y.layerDepth)
                {
                    return 1;
                }

                return 0;
            }
        }

    }
}
