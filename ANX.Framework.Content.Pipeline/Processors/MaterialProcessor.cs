#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ANX.Framework.Content.Pipeline.Graphics;
using System.ComponentModel;

#endregion

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Content.Pipeline.Processors
{
    [ContentProcessor]
    public class MaterialProcessor : ContentProcessor<MaterialContent, MaterialContent>
    {
        [DefaultValue(typeof(Color), "255; 0; 255; 255")]
        public virtual Color ColorKeyColor { get; set; }

        [DefaultValue(true)]
        public virtual bool ColorKeyEnabled { get; set; }

        [DefaultValue(MaterialProcessorDefaultEffect.BasicEffect)]
        public virtual MaterialProcessorDefaultEffect DefaultEffect { get; set; }

        [DefaultValue(true)]
        public virtual bool GenerateMipmaps { get; set; }

        [DefaultValue(true)]
        public virtual bool PremultiplyTextureAlpha { get; set; }

        [DefaultValue(false)]
        public virtual bool ResizeTexturesToPowerOfTwo { get; set; }

        [DefaultValue(TextureProcessorOutputFormat.DxtCompressed)]
        public virtual TextureProcessorOutputFormat TextureFormat { get; set; }

        public MaterialProcessor()
        {
            ColorKeyColor = Color.FromNonPremultiplied(255, 0, 255, 255);
            ColorKeyEnabled = true;
            DefaultEffect = MaterialProcessorDefaultEffect.BasicEffect;
            GenerateMipmaps = true;
            PremultiplyTextureAlpha = true;
            ResizeTexturesToPowerOfTwo = false;
            TextureFormat = TextureProcessorOutputFormat.DxtCompressed;
        }

        /// <summary>
        /// Builds the texture and effect content for the material. 
        /// </summary>
        /// <remarks>
        /// If the MaterialContent is of type EffectMaterialContent, a build is requested for Effect.
        /// Process requests builds for all textures in Textures.
        /// </remarks>
        /// <param name="input">The material content to build.</param>
        /// <param name="context">Context for the specified processor.</param>
        /// <returns>The built material.</returns>
        public override MaterialContent Process(MaterialContent input, ContentProcessorContext context)
        {
            if (input == null)
                throw new ArgumentNullException("input");
            
            if (context == null)
                throw new ArgumentNullException("context");
            
            //If no specific material seems to be specified, try to convert it to the default effect.
            if (input is BasicMaterialContent)
            {
                switch (this.DefaultEffect)
                {
                    case MaterialProcessorDefaultEffect.AlphaTestEffect:
                        input = ConvertMaterial<AlphaTestMaterialContent>(input);
                        break;
                    case MaterialProcessorDefaultEffect.DualTextureEffect:
                        input = ConvertMaterial<DualTextureMaterialContent>(input);
                        break;
                    case MaterialProcessorDefaultEffect.EnvironmentMapEffect:
                        input = ConvertMaterial<EnvironmentMapMaterialContent>(input);
                        break;
                    case MaterialProcessorDefaultEffect.SkinnedEffect:
                        input = ConvertMaterial<SkinnedMaterialContent>(input);
                        break;
                }
            }

            foreach (var pair in input.Textures)
            {
                this.BuildTexture(pair.Key, pair.Value, context);
            }

            if (input is EffectMaterialContent)
            {
                var effectMaterialContent = (EffectMaterialContent)input;
                if (effectMaterialContent.Effect == null)
                    throw new InvalidContentException(string.Format("EffectMaterialContent doesn't contain an effect."), effectMaterialContent.Identity);

                effectMaterialContent.CompiledEffect = this.BuildEffect(effectMaterialContent.Effect, context);
            }

            return input;
        }

        protected virtual T ConvertMaterial<T>(MaterialContent material) where T : MaterialContent, new()
        {
            if (material == null)
                throw new ArgumentNullException("material");

            T instance = new T()
            {
                Name = material.Name,
                Identity = material.Identity,
            };

            foreach (var pair in material.OpaqueData)
            {
                instance.OpaqueData.Add(pair.Key, pair.Value);
            }

            foreach (var pair in material.Textures)
            {
                instance.Textures.Add(pair.Key, pair.Value);
            }

            return instance;
        }

        /// <summary>
        /// Builds effect content.
        /// </summary>
        /// <remarks>
        /// If the input to process is of type EffectMaterialContent, this function will be called to request that the EffectContent be built. 
        /// The EffectProcessor is used to process the EffectContent. 
        /// Subclasses of MaterialProcessor can override this function to modify the parameters used to build EffectContent. 
        /// For example, a different version of this function could request a different processor for the EffectContent. 
        /// </remarks>
        /// <param name="effect">An external reference to the effect content. </param>
        /// <param name="context">Context for the specified processor.</param>
        /// <returns>A compiled binary effect.</returns>
        protected virtual ExternalReference<CompiledEffectContent> BuildEffect(ExternalReference<EffectContent> effect, ContentProcessorContext context)
        {
            return context.BuildAsset<EffectContent, CompiledEffectContent>(effect, typeof(EffectProcessor).Name);
        }

        protected virtual ExternalReference<TextureContent> BuildTexture(string textureName, ExternalReference<TextureContent> texture, ContentProcessorContext context)
        {
            OpaqueDataDictionary opaqueDataDictionary = new OpaqueDataDictionary();
            opaqueDataDictionary.Add("ColorKeyColor", this.ColorKeyColor);
            opaqueDataDictionary.Add("ColorKeyEnabled", this.ColorKeyEnabled);
            opaqueDataDictionary.Add("TextureFormat", this.TextureFormat);
            opaqueDataDictionary.Add("GenerateMipmaps", this.GenerateMipmaps);
            opaqueDataDictionary.Add("PremultiplyAlpha", this.PremultiplyTextureAlpha);
            opaqueDataDictionary.Add("ResizeToPowerOfTwo", this.ResizeTexturesToPowerOfTwo);

            return context.BuildAsset<TextureContent, TextureContent>(texture, typeof(TextureProcessor).Name, opaqueDataDictionary, null, null);
        }
    }
}
