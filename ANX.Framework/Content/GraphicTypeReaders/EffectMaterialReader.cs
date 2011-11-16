#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ANX.Framework.NonXNA;
using ANX.Framework.Graphics;

#endregion // Using Statements

#region License

//
// This file is part of the ANX.Framework created by the "ANX.Framework developer group".
//
// This file is released under the Ms-PL license.
//
//
//
// Microsoft Public License (Ms-PL)
//
// This license governs use of the accompanying software. If you use the software, you accept this license. 
// If you do not accept the license, do not use the software.
//
// 1.Definitions
//   The terms "reproduce," "reproduction," "derivative works," and "distribution" have the same meaning 
//   here as under U.S. copyright law.
//   A "contribution" is the original software, or any additions or changes to the software.
//   A "contributor" is any person that distributes its contribution under this license.
//   "Licensed patents" are a contributor's patent claims that read directly on its contribution.
//
// 2.Grant of Rights
//   (A) Copyright Grant- Subject to the terms of this license, including the license conditions and limitations 
//       in section 3, each contributor grants you a non-exclusive, worldwide, royalty-free copyright license to 
//       reproduce its contribution, prepare derivative works of its contribution, and distribute its contribution
//       or any derivative works that you create.
//   (B) Patent Grant- Subject to the terms of this license, including the license conditions and limitations in 
//       section 3, each contributor grants you a non-exclusive, worldwide, royalty-free license under its licensed
//       patents to make, have made, use, sell, offer for sale, import, and/or otherwise dispose of its contribution 
//       in the software or derivative works of the contribution in the software.
//
// 3.Conditions and Limitations
//   (A) No Trademark License- This license does not grant you rights to use any contributors' name, logo, or trademarks.
//   (B) If you bring a patent claim against any contributor over patents that you claim are infringed by the software, your 
//       patent license from such contributor to the software ends automatically.
//   (C) If you distribute any portion of the software, you must retain all copyright, patent, trademark, and attribution 
//       notices that are present in the software.
//   (D) If you distribute any portion of the software in source code form, you may do so only under this license by including
//       a complete copy of this license with your distribution. If you distribute any portion of the software in compiled or 
//       object code form, you may only do so under a license that complies with this license.
//   (E) The software is licensed "as-is." You bear the risk of using it. The contributors give no express warranties, guarantees,
//       or conditions. You may have additional consumer rights under your local laws which this license cannot change. To the
//       extent permitted under your local laws, the contributors exclude the implied warranties of merchantability, fitness for a 
//       particular purpose and non-infringement.

#endregion // License

namespace ANX.Framework.Content.GraphicTypeReaders
{
    public class EffectMaterialReader : ContentTypeReader<EffectMaterial>
    {
        protected internal override EffectMaterial Read(ContentReader input, EffectMaterial existingInstance)
        {
            Effect clonedEffect = input.ReadExternalReference<Effect>();
            EffectMaterial effectMaterial = new EffectMaterial(clonedEffect);
            Dictionary<string, object> dictionary = input.ReadObject<Dictionary<string, object>>();
            foreach (KeyValuePair<string, object> pair in dictionary)
            {
                SetMaterialParameter(effectMaterial, pair.Key, pair.Value);
            }
            return effectMaterial;
        }

        private void SetMaterialParameter(EffectMaterial material, string key, object value)
        {
            EffectParameter parameter = material.Parameters[key];
            if (parameter != null)
            {
                Type type = value.GetType();
                try
                {
                    if (type == typeof(int[]))
                    {
                        parameter.SetValue((int[])value);
                    }
                    else if (type == typeof(bool[]))
                    {
                        parameter.SetValue((bool[])value);
                    }
                    else if (type == typeof(float[]))
                    {
                        parameter.SetValue((float[])value);
                    }
                    else if (type == typeof(Vector2[]))
                    {
                        parameter.SetValue((Vector2[])value);
                    }
                    else if (type == typeof(Vector3[]))
                    {
                        parameter.SetValue((Vector3[])value);
                    }
                    else if (type == typeof(Vector4[]))
                    {
                        parameter.SetValue((Vector4[])value);
                    }
                    else if (type == typeof(Matrix[]))
                    {
                        parameter.SetValue((Matrix[])value);
                    }
                    else if (type == typeof(int))
                    {
                        parameter.SetValue((int)value);
                    }
                    else if (type == typeof(bool))
                    {
                        parameter.SetValue((bool)value);
                    }
                    else if (type == typeof(float))
                    {
                        parameter.SetValue((float)value);
                    }
                    else if (type == typeof(Vector2))
                    {
                        parameter.SetValue((Vector2)value);
                    }
                    else if (type == typeof(Vector3))
                    {
                        parameter.SetValue((Vector3)value);
                    }
                    else if (type == typeof(Vector4))
                    {
                        parameter.SetValue((Vector4)value);
                    }
                    else if (type == typeof(Matrix))
                    {
                        parameter.SetValue((Matrix)value);
                    }
                    else if (type == typeof(string))
                    {
                        parameter.SetValue((string)value);
                    }
                    else
                    {
                        Texture texture = value as Texture;
                        if (texture != null)
                        {
                            parameter.SetValue(texture);
                        }
                    }
                }
                catch (InvalidCastException)
                {
                    if (parameter.ColumnCount == 1)
                    {
                        if (type == typeof(Vector2))
                        {
                            parameter.SetValue(((Vector2)value).X);
                        }
                        else if (type == typeof(Vector3))
                        {
                            parameter.SetValue(((Vector3)value).X);
                        }
                        else if (type == typeof(Vector4))
                        {
                            parameter.SetValue(((Vector4)value).X);
                        }
                    }
                    if (parameter.ColumnCount == 2)
                    {
                        if (type == typeof(Vector2))
                        {
                            parameter.SetValue(((Vector2)value));
                        }
                        else if (type == typeof(Vector3))
                        {
                            Vector3 v = (Vector3)value;
                            parameter.SetValue(new Vector2(v.X, v.Y));
                        }
                        else if (type == typeof(Vector4))
                        {
                            Vector4 v = (Vector4)value;
                            parameter.SetValue(new Vector2(v.X, v.Y));
                        }
                    }
                    if (parameter.ColumnCount == 3)
                    {
                        if (type == typeof(Vector2))
                        {
                            Vector2 v = (Vector2)value;
                            parameter.SetValue(new Vector3(v.X, v.Y, 0));
                        }
                        else if (type == typeof(Vector3))
                        {
                            Vector3 v = (Vector3)value;
                            parameter.SetValue(v);
                        }
                        else if (type == typeof(Vector4))
                        {
                            Vector4 v = (Vector4)value;
                            parameter.SetValue(new Vector3(v.X, v.Y, v.Z));
                        }
                    }
                    if (parameter.ColumnCount == 4)
                    {
                        if (type == typeof(Vector2))
                        {
                            Vector2 v = (Vector2)value;
                            parameter.SetValue(new Vector4(v.X, v.Y, 0, 1));
                        }
                        else if (type == typeof(Vector3))
                        {
                            Vector3 v = (Vector3)value;
                            parameter.SetValue(new Vector4(v, 1));
                        }
                        else if (type == typeof(Vector4))
                        {
                            Vector4 v = (Vector4)value;
                            parameter.SetValue(v);
                        }
                    }
                }
            }
        }
    }
}
