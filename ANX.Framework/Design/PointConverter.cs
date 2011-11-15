using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Reflection;

namespace ANX.Framework.Design
{
    public class PointConverter : MathTypeConverter
    {
        public PointConverter()
        {
            base.propertyDescriptions = MathTypeConverter.CreateFieldDescriptor<Point>("X", "Y");
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            int[] values = MathTypeConverter.ConvertFromString<int>(context, culture, value as String);
            if (values != null && values.Length == 2)
            {
                return new Point(values[0], values[1]);
            }
            return base.ConvertFrom(context, culture, value);
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType == null)
            {
                throw new ArgumentNullException("destinationType");
            }
            if (value is Point)
            {
                Point instance = (Point)value;

                if (destinationType == typeof(string))
                {
                    return MathTypeConverter.ConvertToString<int>(context, culture, new int[] { instance.X, instance.Y });
                }
                if (destinationType == typeof(InstanceDescriptor))
                {
                    var constructor = typeof(Point).GetConstructor(new Type[] { typeof(int), typeof(int) });
                    return new InstanceDescriptor(constructor, new object[] { instance.X, instance.Y });
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
            return new Point((int)propertyValues["X"], (int)propertyValues["Y"]);
        }
    }
}
