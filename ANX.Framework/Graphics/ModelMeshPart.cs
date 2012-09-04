using System;
using ANX.Framework.NonXNA.Development;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Graphics
{
	[PercentageComplete(100)]
	[TestState(TestStateAttribute.TestState.Untested)]
	public sealed class ModelMeshPart
	{
		#region Private
		internal ModelMesh parentMesh;
		private Effect effect;
		private IndexBuffer indexBuffer;
		private int numVertices;
		private int primitiveCount;
		private int startIndex;
		private Object tag;
		private VertexBuffer vertexBuffer;
		private int vertexOffset;
		#endregion

		#region Public
		public Effect Effect
		{
			get { return effect; }
			set
			{
				if (this.effect != value)
				{
					var old = this.effect;
					this.effect = value;
					this.parentMesh.EffectChangedOnMeshPart(this, old, value);
				}
			}
		}

		public IndexBuffer IndexBuffer
		{
			get { return indexBuffer; }
			internal set { this.indexBuffer = value; }
		}

		public int NumVertices
		{
			get { return numVertices; }
		}

		public int PrimitiveCount
		{
			get { return primitiveCount; }
		}

		public int StartIndex
		{
			get { return startIndex; }
		}

		public Object Tag
		{
			get { return tag; }
			set { tag = value; }
		}


		public VertexBuffer VertexBuffer
		{
			get { return vertexBuffer; }
			internal set { this.vertexBuffer = value; }
		}


		public int VertexOffset
		{
			get { return vertexOffset; }
		}
		#endregion

		internal ModelMeshPart(int vertexOffset, int numVertices, int startIndex, int primitiveCount, object tag)
		{
			this.vertexOffset = vertexOffset;
			this.numVertices = numVertices;
			this.startIndex = startIndex;
			this.primitiveCount = primitiveCount;
			this.tag = tag;
		}
	}
}
