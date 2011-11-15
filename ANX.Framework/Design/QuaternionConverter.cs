using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Reflection;

namespace ANX.Framework.Design
{
    public class QuaternionConverter : MathTypeConverter
    {
        public QuaternionConverter()
        {
            base.propertyDescriptions = MathTypeConverter.CreateFieldDescriptor<Quaternion>("X", "Y", "Z", "W");
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            float[] values = MathTypeConverter.ConvertFromString<float>(context, culture, value as String);
            if (values != null && values.Length == 4)
            {
                return new Quaternion(values[0], values[1], values[2], values[3]);
            }
            return base.ConvertFrom(context, culture, value);
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType == null)
            {
                throw new ArgumentNullException("destinationType");
            }
            if (value is Quaternion)
            {
                Quaternion instance = (Quaternion)value;

                if (destinationType == typeof(string))
                {
                    return MathTypeConverter.ConvertToString<float>(context, culture, new float[] { instance.X, instance.Y, instance.Z, instance.W });
                }
                if (destinationType == typeof(InstanceDescriptor))
                {
                    var constructor = typeof(Quaternion).GetConstructor(new Type[] { typeof(float), typeof(float), typeof(float), typeof(float) });
                    return new InstanceDescriptor(constructor, new object[] { instance.X, instance.Y, instance.Z, instance.W });
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
            return new Quaternion((float)propertyValues["X"], (float)propertyValues["Y"], (float)propertyValues["Z"], (float)propertyValues["W"]);
        }
    }
}
