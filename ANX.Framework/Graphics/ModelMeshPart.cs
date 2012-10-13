using System;
using ANX.Framework.NonXNA.Development;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Graphics
{
	[PercentageComplete(100)]
    [Developer("???")]
	[TestState(TestStateAttribute.TestState.Untested)]
	public sealed class ModelMeshPart
	{
		#region Private
		internal ModelMesh parentMesh;
		private Effect effect;
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

	    public IndexBuffer IndexBuffer { get; internal set; }
	    public int NumVertices { get; private set; }
	    public int PrimitiveCount { get; private set; }
	    public int StartIndex { get; private set; }
	    public object Tag { get; set; }
	    public VertexBuffer VertexBuffer { get; internal set; }
	    public int VertexOffset { get; private set; }
	    #endregion

		internal ModelMeshPart(int vertexOffset, int numVertices, int startIndex, int primitiveCount, object tag)
		{
			this.VertexOffset = vertexOffset;
			this.NumVertices = numVertices;
			this.StartIndex = startIndex;
			this.PrimitiveCount = primitiveCount;
			this.Tag = tag;
		}
	}
}
