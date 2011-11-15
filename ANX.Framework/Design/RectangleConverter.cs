using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Reflection;

namespace ANX.Framework.Design
{
    public class RectangleConverter : MathTypeConverter
    {
        public RectangleConverter()
        {
            base.propertyDescriptions = MathTypeConverter.CreateFieldDescriptor<Rectangle>("X", "Y", "Width", "Height");
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            int[] values = MathTypeConverter.ConvertFromString<int>(context, culture, value as String);
            if (values != null && values.Length == 4)
            {
                return new Rectangle(values[0], values[1], values[2], values[3]);
            }
            return base.ConvertFrom(context, culture, value);
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType == null)
            {
                throw new ArgumentNullException("destinationType");
            }
            if (value is Rectangle)
            {
                Rectangle instance = (Rectangle)value;

                if (destinationType == typeof(string))
                {
                    return MathTypeConverter.ConvertToString<int>(context, culture, new int[] { instance.X, instance.Y, instance.Width, instance.Height });
                }
                if (destinationType == typeof(InstanceDescriptor))
                {
                    var constructor = typeof(Rectangle).GetConstructor(new Type[] { typeof(int), typeof(int), typeof(int), typeof(int) });
                    return new InstanceDescriptor(constructor, new object[] { instance.X, instance.Y, instance.Width, instance.Height });
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
            return new Rectangle((int)propertyValues["X"], (int)propertyValues["Y"], (int)propertyValues["Width"], (int)propertyValues["Height"]);
        }
    }
}
