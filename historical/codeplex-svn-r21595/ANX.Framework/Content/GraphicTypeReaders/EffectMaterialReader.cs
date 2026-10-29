#region Using Statements
using System;
using System.Collections.Generic;
using ANX.Framework.Graphics;
using ANX.Framework.NonXNA.Development;

#endregion // Using Statements

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Content
{
    [PercentageComplete(100)]
    [Developer("GinieDP")]
    [TestState(TestStateAttribute.TestState.Untested)]
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
