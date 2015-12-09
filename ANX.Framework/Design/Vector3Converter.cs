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
    public class Vector3Converter : MathTypeConverter
    {
        public Vector3Converter()
        {
            base.propertyDescriptions = MathTypeConverter.CreateFieldDescriptor<Vector3>("X", "Y", "Z");
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            float[] values = ConvertFromString<float>(context, culture, value as String);
            if (values != null && values.Length == 3)
            {
                return new Vector3(values[0], values[1], values[2]);
            }
            return base.ConvertFrom(context, culture, value);
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType == null)
                throw new ArgumentNullException("destinationType");

            if (value is Vector3)
            {
                Vector3 vecValue = (Vector3)value;

                if (destinationType == typeof(string))
                    return ConvertToString<float>(context, culture,
                        new float[] { vecValue.X, vecValue.Y, vecValue.Z });

                if (IsTypeInstanceDescriptor(destinationType))
                    return CreateInstanceDescriptor<Vector3>(new object[] { vecValue.X, vecValue.Y, vecValue.Z });
            }

            return base.ConvertTo(context, culture, value, destinationType);
        }

        public override object CreateInstance(ITypeDescriptorContext context, IDictionary propertyValues)
        {
            if (propertyValues == null)
                throw new ArgumentNullException("propertyValues");

            return new Vector3((float)propertyValues["X"], (float)propertyValues["Y"], (float)propertyValues["Z"]);
        }
    }

#endif
}
