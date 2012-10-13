using System;
using System.Collections;
using System.ComponentModel;
using System.Globalization;
using ANX.Framework.NonXNA.Development;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Design
{
#if !WINDOWSMETRO      //TODO: search replacement for Win8
    [Developer("GinieDP")]
    [TestState(TestStateAttribute.TestState.Tested)]
    public class BoundingBoxConverter : MathTypeConverter
    {
        public BoundingBoxConverter()
        {
            base.propertyDescriptions = MathTypeConverter.CreateFieldDescriptor<BoundingBox>("Min", "Max");
            supportStringConvert = false;
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            return base.ConvertFrom(context, culture, value);
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType == null)
                throw new ArgumentNullException("destinationType");

            if (value is BoundingBox)
            {
                BoundingBox instance = (BoundingBox)value;

				if (IsTypeInstanceDescriptor(destinationType))
					return CreateInstanceDescriptor<BoundingBox>(new object[] { instance.Min, instance.Max });
            }

            return base.ConvertTo(context, culture, value, destinationType);
        }

        public override object CreateInstance(ITypeDescriptorContext context, IDictionary propertyValues)
        {
            if (propertyValues == null)
                throw new ArgumentNullException("propertyValues");

            return new BoundingBox((Vector3)propertyValues["Min"], (Vector3)propertyValues["Max"]);
        }
    }

#endif
}
