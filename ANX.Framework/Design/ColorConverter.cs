using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Reflection;

namespace ANX.Framework.Design
{
    public class ColorConverter : MathTypeConverter
    {
        public ColorConverter()
        {
            base.propertyDescriptions = MathTypeConverter.CreateFieldDescriptor<Color>("R", "G", "B", "A");
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            byte[] values = MathTypeConverter.ConvertFromString<byte>(context, culture, value as String);
            if (values != null && values.Length == 4)
            {
                return new Color(values[0], values[1], values[2], values[3]);
            }
            return base.ConvertFrom(context, culture, value);
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType == null)
            {
                throw new ArgumentNullException("destinationType");
            }
            if (value is Color)
            {
                Color instance = (Color)value;

                if (destinationType == typeof(string))
                {
                    return MathTypeConverter.ConvertToString<float>(context, culture, new float[] { instance.R, instance.G, instance.B, instance.A });
                }
                if (destinationType == typeof(InstanceDescriptor))
                {
                    var constructor = typeof(Color).GetConstructor(new Type[] { typeof(float), typeof(float), typeof(float), typeof(float) });
                    return new InstanceDescriptor(constructor, new object[] { instance.R, instance.G, instance.B, instance.A });
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
            return new Color((float)propertyValues["R"], (float)propertyValues["G"], (float)propertyValues["B"], (float)propertyValues["A"]);
        }
    }
}
