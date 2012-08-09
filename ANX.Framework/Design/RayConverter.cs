#region Using Statements
using System;
using System.Collections;
using System.ComponentModel;
#if !WINDOWSMETRO
using System.ComponentModel.Design.Serialization;
#endif
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Reflection;

#endregion // Using Statements

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Design
{
#if !WINDOWSMETRO      //TODO: search replacement for Win8

    public class RayConverter : MathTypeConverter
    {
        public RayConverter()
        {
            base.propertyDescriptions = MathTypeConverter.CreateFieldDescriptor<Ray>("Position", "Direction");
            supportStringConvert = false;
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            return base.ConvertFrom(context, culture, value);
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType == null)
            {
                throw new ArgumentNullException("destinationType");
            }
            if (value is Ray)
            {
                Ray instance = (Ray)value;

                if (destinationType == typeof(InstanceDescriptor))
                {
                    var constructor = typeof(Ray).GetConstructor(new Type[] { typeof(Vector3), typeof(Vector3) });
                    return new InstanceDescriptor(constructor, new object[] { instance.Position, instance.Direction });
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
            return new Ray((Vector3)propertyValues["Position"], (Vector3)propertyValues["Direction"]);
        }
    }

#endif
}
