using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Reflection;

namespace ANX.Framework.Design
{
    public class Vector3Converter : MathTypeConverter
    {
        public Vector3Converter()
        {
            base.propertyDescriptions = MathTypeConverter.CreateFieldDescriptor<Vector3>("X", "Y", "Z");
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            float[] values = MathTypeConverter.ConvertFromString<float>(context, culture, value as String);
            if (values != null && values.Length == 3)
            {
                return new Vector3(values[0], values[1], values[2]);
            }
            return base.ConvertFrom(context, culture, value);
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType == null)
            {
                throw new ArgumentNullException("destinationType");
            }
            if (value is Vector3)
            {
                Vector3 vecValue = (Vector3)value;

                if (destinationType == typeof(string))
                {
                    return MathTypeConverter.ConvertToString<float>(context, culture, new float[] { vecValue.X, vecValue.Y, vecValue.Z });
                }
                if (destinationType == typeof(InstanceDescriptor))
                {
                    var constructor = typeof(Vector3).GetConstructor(new Type[] { typeof(float), typeof(float), typeof(float) });
                    return new InstanceDescriptor(constructor, new object[] { vecValue.X, vecValue.Y, vecValue.Z });
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
            return new Vector3((float)propertyValues["X"], (float)propertyValues["Y"], (float)propertyValues["Z"]);
        }
    }
}
