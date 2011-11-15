using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Reflection;

namespace ANX.Framework.Design
{
    public class BoundingSphereConverter : MathTypeConverter
    {
        public BoundingSphereConverter()
        {
            base.propertyDescriptions = MathTypeConverter.CreateFieldDescriptor<BoundingSphere>("Center", "Radius");
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
            if (value is BoundingSphere)
            {
                BoundingSphere instance = (BoundingSphere)value;

                if (destinationType == typeof(InstanceDescriptor))
                {
                    var constructor = typeof(BoundingSphere).GetConstructor(new Type[] { typeof(Vector3), typeof(float) });
                    return new InstanceDescriptor(constructor, new object[] { instance.Center, instance.Radius });
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
            return new BoundingSphere((Vector3)propertyValues["Center"], (float)propertyValues["Radius"]);
        }
    }
}
