using System;
using System.Collections.Generic;
using ANX.Framework.Graphics;
using ANX.Framework.NonXNA;
using OpenTK.Graphics.OpenGL;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.RenderSystem.GL3
{
	/// <summary>
	/// Native OpenGL implementation of an effect technique.
	/// </summary>
	public class EffectTechniqueGL3 : INativeEffectTechnique
	{
		#region Private
		/// <summary>
		/// The native shader handle.
		/// </summary>
		internal int programHandle;

		/// <summary>
		/// The active attributes of this technique.
		/// </summary>
		internal ShaderAttributeGL3[] activeAttributes;

		/// <summary>
		/// We currently have only one pass per technique.
		/// </summary>
		private EffectPass pass;

		private Effect parentEffect;
		#endregion

		#region Public
	    /// <summary>
	    /// The name of the effect technique.
	    /// </summary>
	    public string Name { get; private set; }

	    /// <summary>
	    /// The passes of the technique.
	    /// </summary>
	    public IEnumerable<EffectPass> Passes
	    {
	        get { yield return pass; }
	    }

	    public EffectAnnotationCollection Annotations
        {
            get { throw new NotImplementedException(); }
        }
		#endregion

		#region Constructor
		/// <summary>
		/// Create a ne effect technique object.
		/// </summary>
		internal EffectTechniqueGL3(Effect setParentEffect, string setName, int setProgramHandle)
		{
			parentEffect = setParentEffect;
			Name = setName;
			programHandle = setProgramHandle;

			GetAttributes();

			pass = new EffectPass(parentEffect);
		}
		#endregion

		#region GetAttributes
		private void GetAttributes()
		{
			int attributeCount;
			GL.GetProgram(programHandle, GetProgramParameterName.ActiveAttributes,
				out attributeCount);
			activeAttributes = new ShaderAttributeGL3[attributeCount];
			for (int index = 0; index < attributeCount; index++)
			{
				activeAttributes[index] = new ShaderAttributeGL3(programHandle, index);
			}
		}
		#endregion
	}
}
