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
    public class MaterialContent : ContentItem
    {
        public MaterialContent()
        {
            Textures = new TextureReferenceDictionary();
        }

        public TextureReferenceDictionary Textures
        {
            get;
            private set;
        }

        //description from msdn
        /// <summary>
        /// Gets a reference type from the <see cref="OpaqueDataDictionary"/> collection. 
        /// </summary>
        /// <typeparam name="T">Type of the related opaque data.</typeparam>
        /// <param name="key">Key of the property being retrieved.</param>
        /// <returns>The related opaque data.</returns>
        protected T GetReferenceTypeProperty<T>(string key) where T : class
        {
            object result;
            if (this.OpaqueData.TryGetValue(key, out result) && result is T)
                return (T)result;
            else
                return null;
        }

        /// <summary>
        /// Gets a value from the <see cref="Textures"/> collection. 
        /// </summary>
        /// <param name="key">Key of the texture being retrieved.</param>
        /// <returns>Reference to a texture from the collection.</returns>
        protected ExternalReference<TextureContent> GetTexture(String key)
        {
            ExternalReference<TextureContent> result;
            if (Textures.TryGetValue(key, out result))
                return result;
            else
                return null;
        }

        /// <summary>
        /// Gets a value type from the <see cref="OpaqueDataDictionary"/> collection. 
        /// </summary>
        /// <typeparam name="T">Type of the value being retrieved.</typeparam>
        /// <param name="key">Key of the value type being retrieved.</param>
        /// <returns>Index of the value type beng retrieved.</returns>
        protected Nullable<T> GetValueTypeProperty<T>(string key) where T : struct
        {
            object result;
            if (this.OpaqueData.TryGetValue(key, out result) && result is T)
                return (T?)result;
            else
                return null;
        }

        /// <summary>
        /// Sets a value in the contained <see cref="OpaqueDataDictionary"/> object.
        /// If null is passed, the value is removed.
        /// </summary>
        /// <typeparam name="T">Type of the element being set.</typeparam>
        /// <param name="key">Name of the key being modified.</param>
        /// <param name="value">Value being set.</param>
        protected void SetProperty<T>(string key, T value)
        {
            if (value == null)
                this.OpaqueData.Remove(key);
            else
                this.OpaqueData[key] = value;
        }

        /// <summary>
        /// Sets a value in the contained TextureReferenceDictionary object.
        /// If null is passed, the value is removed.
        /// </summary>
        /// <remarks>
        /// The <paramref name="key"/> value differs depending on the type of attached dictionary.
        /// 
        /// If attached to a <see cref="BasicMaterialContent"/> dictionary (which becomes a <see cref="BasicEffect"/> object at run time), 
        /// the value for the Texture key is used as the texture for the <see cref="BasicEffect"/> runtime object. Other keys are ignored.
        /// 
        /// If attached to a <see cref="EffectMaterialContent"/> dictionary, key names are the texture names used by the effect. These names are dependent upon the author of the effect object.
        /// </remarks>
        /// <param name="key">Name of the key being modified.</param>
        /// <param name="value">Value being set. </param>
        protected void SetTexture(String key, ExternalReference<TextureContent> value)
        {
            if (value == null)
                Textures.Remove(key);
            else
                Textures[key] = value;
        }
    }
}
