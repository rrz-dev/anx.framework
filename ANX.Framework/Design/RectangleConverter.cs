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

    public class RectangleConverter : MathTypeConverter
    {
        public RectangleConverter()
        {
            base.propertyDescriptions = MathTypeConverter.CreateFieldDescriptor<Rectangle>("X", "Y", "Width", "Height");
            supportStringConvert = false;
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType == null)
                throw new ArgumentNullException("destinationType");

            if (value is Rectangle)
            {
                Rectangle instance = (Rectangle)value;

				if (IsTypeInstanceDescriptor(destinationType))
					return CreateInstanceDescriptor<Rectangle>(new object[] { instance.X, instance.Y, instance.Width,
						instance.Height });
            }

            return base.ConvertTo(context, culture, value, destinationType);
        }

        public override object CreateInstance(ITypeDescriptorContext context, IDictionary propertyValues)
        {
            if (propertyValues == null)
                throw new ArgumentNullException("propertyValues");

            return new Rectangle((int)propertyValues["X"], (int)propertyValues["Y"], (int)propertyValues["Width"], (int)propertyValues["Height"]);
        }
    }

#endif
}
