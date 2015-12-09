using System;
using ANX.Framework.NonXNA;
using OpenTK.Graphics.OpenGL;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.RenderSystem.GL3
{
	public class EffectPassGL3 : INativeEffectPass
	{
        private EffectTechniqueGL3 parentTechnique;

		public string Name
		{
			get
			{
				return "p0";
			}
		}

        public EffectPassGL3(EffectTechniqueGL3 parentTechnique)
        {
            this.parentTechnique = parentTechnique;
        }

        public Framework.Graphics.EffectAnnotationCollection Annotations
        {
            get { throw new NotImplementedException(); }
        }

        public void Apply()
        {
            GL.UseProgram(parentTechnique.programHandle);
            GraphicsDeviceWindowsGL3.activeEffect = this.parentTechnique.Parent;
            ErrorHelper.Check("UseProgram");
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
