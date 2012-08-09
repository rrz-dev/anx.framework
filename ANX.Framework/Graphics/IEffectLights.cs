#region Using Statements
using System;

#endregion // Using Statements

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Graphics
{
    public interface IEffectLights
    {
        void EnableDefaultLighting();

        Vector3 AmbientLightColor { get; set; }

        DirectionalLight DirectionalLight0 { get; }

        DirectionalLight DirectionalLight1 { get; }

        DirectionalLight DirectionalLight2 { get; }

        bool LightingEnabled { get; set; }
    }
}
