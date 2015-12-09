#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ANX.Framework.Content.Pipeline.Processors;

#endregion

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Content.Pipeline.Graphics
{
    /// <summary>
    /// Provides support for representing DirectX <see cref="Effect"/> materials. 
    /// </summary>
    public class EffectMaterialContent : MaterialContent
    {
        public const string CompiledEffectKey = "CompiledEffect";
        public const string EffectKey = "Effect";

        public EffectMaterialContent()
        {
        }

        [ContentSerializerIgnore]
        public ExternalReference<CompiledEffectContent> CompiledEffect
        {
            get { return this.GetReferenceTypeProperty<ExternalReference<CompiledEffectContent>>(CompiledEffectKey); }
            set { this.SetProperty(CompiledEffectKey, value); }
        }

        [ContentSerializerIgnore]
        public ExternalReference<EffectContent> Effect
        {
            get { return this.GetReferenceTypeProperty<ExternalReference<EffectContent>>(EffectKey); }
            set { this.SetProperty(EffectKey, value); }
        }
    }
}
