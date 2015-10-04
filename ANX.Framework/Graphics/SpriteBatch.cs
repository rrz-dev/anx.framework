using System;
using System.Collections.Generic;
using System.Text;
using ANX.Framework.NonXNA;
using ANX.Framework.NonXNA.Development;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Graphics
{
	[PercentageComplete(100)]
	[TestState(TestStateAttribute.TestState.Untested)]
	[Developer("Glatzemann")]
    public class SpriteBatch : GraphicsResource
	{
		private const int InitialBatchSize = 1024;

        #region Private
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
        #endregion

        #region Constructor
        public SpriteBatch(GraphicsDevice graphicsDevice)
        {
            if (graphicsDevice == null)
                throw new ArgumentNullException("graphicsDevice");

            base.GraphicsDevice = graphicsDevice;

            var renderSystemCreator = AddInSystemFactory.Instance.GetDefaultCreator<IRenderSystemCreator>();
            this.spriteBatchEffect = new Effect(graphicsDevice,
				renderSystemCreator.GetShaderByteCode(NonXNA.PreDefinedShader.SpriteBatch),
				renderSystemCreator.GetStockShaderSourceLanguage);

            this.spriteInfos = new SpriteInfo[InitialBatchSize];
            this.InitializeIndexBuffer(InitialBatchSize);
            this.InitializeVertexBuffer();
        }
        #endregion

        #region Begin
        public void Begin()
        {
            Begin(SpriteSortMode.Texture, null, null, null, null, null);
        }

        public void Begin(SpriteSortMode sortMode, BlendState blendState)
        {
            Begin(sortMode, blendState, null, null, null, null);
        }

        public void Begin(SpriteSortMode sortMode, BlendState blendState, SamplerState samplerState,
			DepthStencilState depthStencilState, RasterizerState rasterizerState)
        {
            Begin(sortMode, blendState, samplerState, depthStencilState, rasterizerState, null);
        }

        public void Begin(SpriteSortMode sortMode, BlendState blendState, SamplerState samplerState,
			DepthStencilState depthStencilState, RasterizerState rasterizerState, Effect effect)
		{
			if (hasBegun == true)
				throw new Exception("End() has to be called before a new SpriteBatch can be started with Begin()");

			hasBegun = true;
			this.currentSortMode = sortMode;

			this.blendState = blendState;
			this.samplerState = samplerState;
			this.depthStencilState = depthStencilState;
			this.rasterizerState = rasterizerState;
			this.effect = effect;
			this.transformMatrix = Matrix.Identity;
        }

        public void Begin(SpriteSortMode sortMode, BlendState blendState, SamplerState samplerState,
			DepthStencilState depthStencilState, RasterizerState rasterizerState, Effect effect, Matrix transformMatrix)
        {
            if (hasBegun == true)
                throw new Exception("End() has to be called before a new SpriteBatch can be started with Begin()");

            hasBegun = true;

            this.currentSortMode = sortMode;

            this.blendState = blendState;
            this.samplerState = samplerState;
            this.depthStencilState = depthStencilState;
            this.rasterizerState = rasterizerState;
            this.effect = effect;
            this.transformMatrix = transformMatrix;
        }
        #endregion

		#region Draw
		public void Draw(Texture2D texture, Rectangle destinationRectangle, Color color)
		{
			Draw(texture, new Vector2(destinationRectangle.X, destinationRectangle.Y), new Vector2(destinationRectangle.Width,
				destinationRectangle.Height), null, color, Vector2.Zero, 0f, 0f, Vector2.One, SpriteEffects.None);
		}

		public void Draw(Texture2D texture, Rectangle destinationRectangle, Nullable<Rectangle> sourceRectangle, Color color)
		{
			Draw(texture, new Vector2(destinationRectangle.X, destinationRectangle.Y), new Vector2(destinationRectangle.Width,
				destinationRectangle.Height), sourceRectangle, color, Vector2.Zero, 0f, 0f, Vector2.One, SpriteEffects.None);
		}

        public void Draw(Texture2D texture, Rectangle destinationRectangle, Nullable<Rectangle> sourceRectangle, Color color,
			Single rotation, Vector2 origin, SpriteEffects effects, Single layerDepth)
        {
            Draw(texture, new Vector2(destinationRectangle.X, destinationRectangle.Y), new Vector2(destinationRectangle.Width,
				destinationRectangle.Height), sourceRectangle, color, origin, layerDepth, rotation, Vector2.One, effects);
        }

        public void Draw(Texture2D texture, Vector2 position, Color color)
        {
            Draw(texture, position, new Vector2(texture.Width, texture.Height), null, color, Vector2.Zero, 0f, 0f, Vector2.One,
				SpriteEffects.None);
        }

        public void Draw(Texture2D texture, Vector2 position, Nullable<Rectangle> sourceRectangle, Color color)
        {
            Vector2 size = sourceRectangle.HasValue ?
				new Vector2(sourceRectangle.Value.Width, sourceRectangle.Value.Height) :
				new Vector2(texture.Width, texture.Height);
			Draw(texture, position, size, sourceRectangle, color, Vector2.Zero, 0f, 0f, Vector2.One, SpriteEffects.None);
        }

        public void Draw(Texture2D texture, Vector2 position, Nullable<Rectangle> sourceRectangle, Color color, Single rotation,
			Vector2 origin, Single scale, SpriteEffects effects, Single layerDepth)
        {
            Vector2 size = sourceRectangle.HasValue ?
				new Vector2(sourceRectangle.Value.Width, sourceRectangle.Value.Height) :
				new Vector2(texture.Width, texture.Height);
            Draw(texture, position, size, sourceRectangle, color, origin, layerDepth, rotation, new Vector2(scale), effects);
        }

		public void Draw(Texture2D texture, Vector2 position, Nullable<Rectangle> sourceRectangle, Color color, Single rotation,
			Vector2 origin, Vector2 scale, SpriteEffects effects, Single layerDepth)
		{
			Vector2 size = sourceRectangle.HasValue ?
				new Vector2(sourceRectangle.Value.Width, sourceRectangle.Value.Height) :
				new Vector2(texture.Width, texture.Height);
			Draw(texture, position, size, sourceRectangle, color, origin, layerDepth, rotation, scale, effects);
		}
        #endregion

		#region DrawOptimizedText
		internal void DrawOptimizedText(Texture2D texture, Vector2 position, ref Rectangle sourceRectangle, ref Color color,
			float rotation, Vector2 scale, SpriteEffects effects, float layerDepth)
		{
			Vector2 size = new Vector2(sourceRectangle.Width, sourceRectangle.Height);
			Draw(texture, position, size, sourceRectangle, color, Vector2.Zero, layerDepth, rotation, scale, effects);
		}

		internal void DrawOptimizedText(Texture2D texture, Vector2 position, ref Rectangle sourceRectangle, ref Color color)
		{
			if (hasBegun == false)
				throw new InvalidOperationException("Begin() must be called before Draw()");

			if (texture == null)
				throw new ArgumentNullException("texture");

			ResizeIfNeeded();

			Vector2 bottomRight;
			bottomRight.X = position.X + sourceRectangle.Width;
			bottomRight.Y = position.Y + sourceRectangle.Height;

			SpriteInfo currentSprite = spriteInfos[currentBatchPosition];
			currentSprite.Corners = new Vector2[]
			{
				position,
				new Vector2(bottomRight.X, position.Y),
				bottomRight,
				new Vector2(position.X, bottomRight.Y)
			};

			currentSprite.Tint = color;
			currentSprite.texture = texture;

			currentSprite.topLeftUV.X = sourceRectangle.X * texture.OneOverWidth;
			currentSprite.topLeftUV.Y = sourceRectangle.Y * texture.OneOverHeight;
			currentSprite.bottomRightUV.X = (sourceRectangle.X + sourceRectangle.Width) * texture.OneOverWidth;
			currentSprite.bottomRightUV.Y = (sourceRectangle.Y + sourceRectangle.Height) * texture.OneOverHeight;

			currentSprite.origin = Vector2.Zero;
			currentSprite.rotation = 0f;
			currentSprite.layerDepth = 1f;

			spriteInfos[currentBatchPosition] = currentSprite;
			currentBatchPosition++;

			if (this.currentSortMode == SpriteSortMode.Immediate)
			{
				BatchRender(0, 1);
				Flush();
			}
		}
		#endregion

		#region DrawString
		public void DrawString(SpriteFont font, String text, Vector2 position, Color color)
		{
			if (font == null)
				throw new ArgumentNullException("font");
			if (text == null)
				throw new ArgumentNullException("text");

			font.DrawString(text, this, position, color);
        }

		public void DrawString(SpriteFont font, StringBuilder text, Vector2 position, Color color)
		{
			if (font == null)
				throw new ArgumentNullException("font");
			if (text == null)
				throw new ArgumentNullException("text");

			font.DrawString(text.ToString(), this, position, color);
		}

        public void DrawString(SpriteFont font, String text, Vector2 position, Color color, Single rotation, Vector2 origin,
			Single scale, SpriteEffects effects, Single layerDepth)
		{
			if (font == null)
				throw new ArgumentNullException("font");
			if (text == null)
				throw new ArgumentNullException("text");

			font.DrawString(text, this, position, color, new Vector2(scale), origin, rotation, layerDepth, effects);
        }

        public void DrawString(SpriteFont font, String text, Vector2 position, Color color, Single rotation, Vector2 origin,
			Vector2 scale, SpriteEffects effects, Single layerDepth)
		{
			if (font == null)
				throw new ArgumentNullException("font");
			if (text == null)
				throw new ArgumentNullException("text");

            font.DrawString(text, this, position, color, scale, origin, rotation, layerDepth, effects);
        }

		public void DrawString(SpriteFont font, StringBuilder text, Vector2 position, Color color, Single rotation,
			Vector2 origin, Single scale, SpriteEffects effects, Single layerDepth)
		{
			if (font == null)
				throw new ArgumentNullException("font");
			if (text == null)
				throw new ArgumentNullException("text");

			font.DrawString(text.ToString(), this, position, color, new Vector2(scale), origin, rotation, layerDepth, effects);
		}

		public void DrawString(SpriteFont font, StringBuilder text, Vector2 position, Color color, Single rotation,
			Vector2 origin, Vector2 scale, SpriteEffects effects, Single layerDepth)
		{
			if (font == null)
				throw new ArgumentNullException("font");
			if (text == null)
				throw new ArgumentNullException("text");

			font.DrawString(text.ToString(), this, position, color, scale, origin, rotation, layerDepth, effects);
		}
        #endregion

        #region End
        public void End()
        {
            if (hasBegun == false)
                throw new Exception("Begin() has to be called before End()");
            
            hasBegun = false;

            if (this.currentSortMode != SpriteSortMode.Immediate)
            {
                if (currentBatchPosition > 0)
                {
                    if (this.currentSortMode == SpriteSortMode.Texture)
                        Array.Sort<SpriteInfo>(spriteInfos, 0, currentBatchPosition, textureComparer);
                    else if (this.currentSortMode == SpriteSortMode.BackToFront)
                        Array.Sort<SpriteInfo>(spriteInfos, 0, currentBatchPosition, backToFrontComparer);
                    else if (this.currentSortMode == SpriteSortMode.FrontToBack)
                        Array.Sort<SpriteInfo>(spriteInfos, 0, currentBatchPosition, frontToBackComparer);

                    int startOffset = 0;
                    Texture2D lastTexture = spriteInfos[0].texture;
                    for (int i = 0; i <= currentBatchPosition; i++)
                    {
                        if (lastTexture != spriteInfos[i].texture || i == currentBatchPosition)
                        {
                            BatchRender(startOffset, i - startOffset);
                            startOffset = i;
                        }

                        lastTexture = spriteInfos[i].texture;
                    }
                }
            }

            Flush();
        }
		#endregion

		#region ResizeIfNeeded
		private void ResizeIfNeeded()
		{
			if (currentBatchPosition >= spriteInfos.Length)
			{
				int newSize = spriteInfos.Length * 2;
				Array.Resize<SpriteInfo>(ref spriteInfos, newSize);
				InitializeIndexBuffer(newSize);
			}
		}
		#endregion

		#region Draw
		private void Draw(Texture2D texture, Vector2 topLeft, Vector2 destinationSize, Rectangle? sourceRectangle,
			Color tint, Vector2 origin, float layerDepth, float rotation, Vector2 scale, SpriteEffects effects)
        {
            if (hasBegun == false)
                throw new InvalidOperationException("Begin() must be called before Draw()");

            if (texture == null)
                throw new ArgumentNullException("texture");

			ResizeIfNeeded();

            Vector2 bottomRight = new Vector2(topLeft.X + (destinationSize.X * scale.X),
				topLeft.Y + (destinationSize.Y * scale.Y));

			SpriteInfo currentSprite = spriteInfos[currentBatchPosition];
			currentSprite.Corners = new Vector2[]
			{
				topLeft,
				new Vector2(bottomRight.X, topLeft.Y),
				bottomRight,
				new Vector2(topLeft.X, bottomRight.Y)
			};

			currentSprite.Tint = tint;
			currentSprite.texture = texture;

            if (sourceRectangle.HasValue)
            {
				currentSprite.topLeftUV.X = sourceRectangle.Value.X * texture.OneOverWidth;
				currentSprite.topLeftUV.Y = sourceRectangle.Value.Y * texture.OneOverHeight;
				currentSprite.bottomRightUV.X = (sourceRectangle.Value.X + sourceRectangle.Value.Width) * texture.OneOverWidth;
				currentSprite.bottomRightUV.Y = (sourceRectangle.Value.Y + sourceRectangle.Value.Height) * texture.OneOverHeight;
            }
            else
            {
				currentSprite.topLeftUV = Vector2.Zero;
				currentSprite.bottomRightUV = Vector2.One;
            }

            if ((effects & SpriteEffects.FlipHorizontally) == SpriteEffects.FlipHorizontally)
            {
				float tempY = currentSprite.bottomRightUV.Y;
				currentSprite.bottomRightUV.Y = currentSprite.topLeftUV.Y;
				currentSprite.topLeftUV.Y = tempY;
            }

            if ((effects & SpriteEffects.FlipVertically) == SpriteEffects.FlipVertically)
            {
				float tempX = currentSprite.bottomRightUV.X;
				currentSprite.bottomRightUV.X = currentSprite.topLeftUV.X;
				currentSprite.topLeftUV.X = tempX;
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
				float offsetX = topLeft.X + origin.X;
				float offsetY = topLeft.Y + origin.Y;
                for (int i = 0; i < 4; i++)
                {
					transformVector.X = currentSprite.Corners[i].X - offsetX;
					transformVector.Y = currentSprite.Corners[i].Y - offsetY;
                    Vector2.Transform(ref transformVector, ref this.cachedRotationMatrix, out result);
					currentSprite.Corners[i].X = result.X + offsetX;
					currentSprite.Corners[i].Y = result.Y + offsetY;
                }
            }

			currentSprite.origin = origin;
			currentSprite.rotation = rotation;
			currentSprite.layerDepth = layerDepth;
			spriteInfos[currentBatchPosition] = currentSprite;

            currentBatchPosition++;

            if (this.currentSortMode == SpriteSortMode.Immediate)
            {
                BatchRender(0, 1);
                Flush();
            }
		}
		#endregion

		#region BatchRender
		private void BatchRender(int offset, int count)
        {
            int vertexCount = count * 4;

            if (this.vertices == null)
                this.vertices = new VertexPositionColorTexture[vertexCount];
            else if (this.vertices.Length < vertexCount)
                Array.Resize<VertexPositionColorTexture>(ref this.vertices, vertexCount);

            int vertexPos = 0;
            for (int i = offset; i < offset + count; i++)
            {
                SpriteInfo currentSprite = this.spriteInfos[i];

                vertices[vertexPos].Position = new Vector3(currentSprite.Corners[0], currentSprite.layerDepth);
                vertices[vertexPos].Color = currentSprite.Tint;
                vertices[vertexPos].TextureCoordinate = currentSprite.topLeftUV;
                vertexPos++;

                vertices[vertexPos].Position = new Vector3(currentSprite.Corners[1], currentSprite.layerDepth);
                vertices[vertexPos].Color = currentSprite.Tint;
                vertices[vertexPos].TextureCoordinate = new Vector2(currentSprite.bottomRightUV.X, currentSprite.topLeftUV.Y);
                vertexPos++;

                vertices[vertexPos].Position = new Vector3(currentSprite.Corners[2], currentSprite.layerDepth);
                vertices[vertexPos].Color = currentSprite.Tint;
                vertices[vertexPos].TextureCoordinate = currentSprite.bottomRightUV;
                vertexPos++;

                vertices[vertexPos].Position = new Vector3(currentSprite.Corners[3], currentSprite.layerDepth);
                vertices[vertexPos].Color = currentSprite.Tint;
                vertices[vertexPos].TextureCoordinate = new Vector2(currentSprite.topLeftUV.X, currentSprite.bottomRightUV.Y);
                vertexPos++;
            }

            this.vertexBuffer.SetData<VertexPositionColorTexture>(this.vertices, 0, vertexCount);

            SetRenderStates();

            spriteBatchEffect.Parameters["Texture"].SetValue(this.spriteInfos[offset].texture);

            GraphicsDevice.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, 0, vertexCount, 0, count * 2);
		}
		#endregion

		#region Flush
		private void Flush()
        {
            currentBatchPosition = 0;
        }
        #endregion

		#region InitializeIndexBuffer
		private void InitializeIndexBuffer(int size)
        {
            int indexCount = size * 6;

            if (this.indexBuffer == null)
            {
                this.indexBuffer = new DynamicIndexBuffer(this.GraphicsDevice, IndexElementSize.SixteenBits, indexCount,
					BufferUsage.WriteOnly);
                this.indexBuffer.ContentLost += new EventHandler<EventArgs>(indexBuffer_ContentLost);
            }

            SetIndexData(indexCount);
		}
		#endregion

		#region indexBuffer_ContentLost
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
		#endregion

		#region SetIndexData
		private void SetIndexData(int indexCount)
        {
            var indices = new ushort[indexCount];

            int baseIndex;
            int baseArrayIndex;
            for (int i = 0; i < InitialBatchSize; i++)
            {
                baseIndex = i * 4;
                baseArrayIndex = baseIndex + i + i;

				indices[baseArrayIndex] = (ushort)baseIndex;
				indices[baseArrayIndex + 1] = (ushort)(baseIndex + 1);
				indices[baseArrayIndex + 2] = (ushort)(baseIndex + 2);
				indices[baseArrayIndex + 3] = (ushort)baseIndex;
				indices[baseArrayIndex + 4] = (ushort)(baseIndex + 2);
				indices[baseArrayIndex + 5] = (ushort)(baseIndex + 3);
            }

            this.indexBuffer.SetData<ushort>(indices);
		}
		#endregion

		#region InitializeVertexBuffer
		private void InitializeVertexBuffer()
        {
            if (this.vertexBuffer == null)
            {
                this.vertexBuffer = new DynamicVertexBuffer(this.GraphicsDevice, typeof(VertexPositionColorTexture),
					InitialBatchSize * 4, BufferUsage.WriteOnly);
                this.vertexBuffer.ContentLost += vertexBuffer_ContentLost;
            }
        }
		#endregion

		#region vertexBuffer_ContentLost
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
		#endregion

		#region SetRenderStates
		private void SetRenderStates()
        {
            GraphicsDevice.BlendState = blendState != null ? blendState : BlendState.AlphaBlend;
            GraphicsDevice.DepthStencilState = depthStencilState != null ? depthStencilState : DepthStencilState.None;
            GraphicsDevice.RasterizerState = rasterizerState != null ? rasterizerState : RasterizerState.CullCounterClockwise;
            GraphicsDevice.SamplerStates[0] = samplerState != null ? samplerState : SamplerState.LinearClamp;

            if (cachedTransformMatrix == null || GraphicsDevice.Viewport.Width != viewportWidth ||
				GraphicsDevice.Viewport.Height != viewportHeight)
            {
                this.viewportWidth = GraphicsDevice.Viewport.Width;
                this.viewportHeight = GraphicsDevice.Viewport.Height;

                cachedTransformMatrix = new Matrix()
                {
                    M11 = 2f * (this.viewportWidth > 0 ? 1f / ((float)this.viewportWidth - 1f) : 0f),
                    M22 = 2f * (this.viewportHeight > 0 ? -1f / ((float)this.viewportHeight - 1f) : 0f),
                    M33 = 1f,
                    M44 = 1f,
                    M41 = -1f,
                    M42 = 1f
                };

                cachedTransformMatrix.M41 -= cachedTransformMatrix.M11;
                cachedTransformMatrix.M42 -= cachedTransformMatrix.M22;
            }

			Matrix result;
			Matrix.Multiply(ref transformMatrix, ref cachedTransformMatrix, out result);
			this.spriteBatchEffect.Parameters["MatrixTransform"].SetValue(result);
            spriteBatchEffect.NativeEffect.Apply(GraphicsDevice);

            GraphicsDevice.Indices = this.indexBuffer;
            GraphicsDevice.SetVertexBuffer(this.vertexBuffer);
		}
		#endregion

		#region Dispose

        protected override void Dispose(bool disposeManaged)
        {
           if (disposeManaged)
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

           base.Dispose(disposeManaged);
        }
		#endregion

		private class TextureComparer : IComparer<SpriteInfo>
        {
            public int Compare(SpriteInfo x, SpriteInfo y)
            {
				int hash1 = x.texture.GetHashCode();
				int hash2 = y.texture.GetHashCode();
				if (hash1 > hash2)
                    return -1;
				else if (hash1 < hash2)
                    return 1;

                return y.layerDepth.CompareTo(x.layerDepth);
            }
        }

        private class FrontToBackComparer : IComparer<SpriteInfo>
        {
            public int Compare(SpriteInfo x, SpriteInfo y)
            {
                if (x.layerDepth > y.layerDepth)
                    return 1;
                else if (x.layerDepth < y.layerDepth)
                    return -1;

                return 0;
            }
        }

        private class BackToFrontComparer : IComparer<SpriteInfo>
        {
            public int Compare(SpriteInfo x, SpriteInfo y)
            {
                if (x.layerDepth > y.layerDepth)
                    return -1;
                else if (x.layerDepth < y.layerDepth)
                    return 1;

                return 0;
            }
        }
    }
}
