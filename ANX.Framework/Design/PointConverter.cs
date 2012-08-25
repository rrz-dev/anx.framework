using System;
using System.Collections;
using System.ComponentModel;
using System.Globalization;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Design
{
#if !WINDOWSMETRO      //TODO: search replacement for Win8

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
                throw new ArgumentNullException("destinationType");

            if (value is Point)
            {
                Point instance = (Point)value;

                if (destinationType == typeof(string))
                    return MathTypeConverter.ConvertToString<int>(context, culture, new int[] { instance.X, instance.Y });

				if (IsTypeInstanceDescriptor(destinationType))
					return CreateInstanceDescriptor<Point>(new object[] { instance.X, instance.Y });
            }

            return base.ConvertTo(context, culture, value, destinationType);
        }

        public override object CreateInstance(ITypeDescriptorContext context, IDictionary propertyValues)
        {
            if (propertyValues == null)
                throw new ArgumentNullException("propertyValues");

            return new Point((int)propertyValues["X"], (int)propertyValues["Y"]);
        }
    }

#endif
}
