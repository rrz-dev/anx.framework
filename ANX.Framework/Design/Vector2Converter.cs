using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Reflection;

namespace ANX.Framework.Design
{
    public class Vector2Converter : MathTypeConverter
    {
        public Vector2Converter()
        {
            base.propertyDescriptions = MathTypeConverter.CreateFieldDescriptor<Vector2>("X", "Y");
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            float[] values = MathTypeConverter.ConvertFromString<float>(context, culture, value as String);
            if (values != null && values.Length == 2)
            {
                return new Vector2(values[0], values[1]);
            }
            return base.ConvertFrom(context, culture, value);
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType == null)
            {
                throw new ArgumentNullException("destinationType");
            }
            if (value is Vector2)
            {
                Vector2 vecValue = (Vector2)value;

                if (destinationType == typeof(string))
                {
                    return MathTypeConverter.ConvertToString<float>(context, culture, new float[] { vecValue.X, vecValue.Y });
                }
                if (destinationType == typeof(InstanceDescriptor))
                {
                    var constructor = typeof(Vector2).GetConstructor(new Type[] { typeof(float), typeof(float) });
                    return new InstanceDescriptor(constructor, new object[] { vecValue.X, vecValue.Y });
                }
            }
            
            return base.ConvertTo(context, culture, value, destinationType);
        }

        public override object CreateInstance(ITypeDescriptorContext context, IDictionary propertyValues)
        {
            if (propertyValues == null)
            {
                throw new ArgumentNullException("propertyValues");
            }
            return new Vector2((float)propertyValues["X"], (float)propertyValues["Y"]);
        }
    }
}
