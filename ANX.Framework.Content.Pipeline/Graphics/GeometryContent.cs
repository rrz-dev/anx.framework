#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

#endregion

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Content.Pipeline.Graphics
{
    /// <summary>
    /// Provides properties that define various aspects of a geometry batch. 
    /// </summary>
    /// <remarks>
    /// A geometry batch is a subcomponent of a mesh, representing a single piece of homogeneous geometry 
    /// that can be submitted to the GPU in a single draw call. It contains an indexed triangle list 
    /// (using a single material) where all vertices share the same set of data channels. Vertices are made 
    /// unique if there are differences in any of their data channels. Coordinates that require unique vertices on 
    /// either side of a join create unique vertices.
    /// </remarks>
    public class GeometryContent : ContentItem
    {
        /// <summary>
        /// Gets the list of triangle indices for this geometry batch.
        /// </summary>
        /// <remarks>
        /// Use these indices to reference into the collection of position indices and vertex channels of the geometry batch.
        /// </remarks>
        public IndexCollection Indices
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets or sets the material of the parent mesh. 
        /// </summary>
        public MaterialContent Material
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the parent <see cref="MeshContent"/> for this object. 
        /// </summary>
        public MeshContent Parent
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the set of vertex batches for the geometry batch. 
        /// </summary>
        public VertexContent Vertices
        {
            get;
            private set;
        }

        public GeometryContent()
        {
            Indices = new IndexCollection();
            Vertices = new VertexContent(this);
        }
    }
}
