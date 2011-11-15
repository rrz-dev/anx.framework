using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Reflection;

namespace ANX.Framework.Design
{
    public class PlaneConverter : MathTypeConverter
    {
        public PlaneConverter()
        {
            base.propertyDescriptions = MathTypeConverter.CreateFieldDescriptor<Plane>("Normal", "D");
            supportStringConvert = false;
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType == null)
            {
                throw new ArgumentNullException("destinationType");
            }
            if (value is Plane)
            {
                Plane instance = (Plane)value;

                if (destinationType == typeof(InstanceDescriptor))
                {
                    var constructor = typeof(Plane).GetConstructor(new Type[] { typeof(Vector3), typeof(float) });
                    return new InstanceDescriptor(constructor, new object[] { instance.Normal, instance.D });
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
            return new Vector4((Vector3)propertyValues["Normal"], (float)propertyValues["D"]);
        }
    }
}
