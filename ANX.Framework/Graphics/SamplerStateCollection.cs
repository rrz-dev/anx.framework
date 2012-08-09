#region Using Statements
using System;

#endregion // Using Statements

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Graphics
{
    public sealed class SamplerStateCollection
    {
        private SamplerState[] samplerStates;
        private GraphicsDevice graphics;

        public SamplerStateCollection(GraphicsDevice graphics, int maxSamplers)
        {
            this.graphics = graphics;
            this.samplerStates = new SamplerState[maxSamplers];

            for (int i = 0; i < samplerStates.Length; i++)
            {
                samplerStates[i] = SamplerState.LinearWrap;
            }
        }

        public SamplerState this[int index]
        {
            get
            {
                return samplerStates[index];
            }
            set
            {
                if (samplerStates[index] != value)
                {
                    samplerStates[index] = value;
                    samplerStates[index].NativeSamplerState.Apply(graphics, index);
                }
            }
        }
    }
}
