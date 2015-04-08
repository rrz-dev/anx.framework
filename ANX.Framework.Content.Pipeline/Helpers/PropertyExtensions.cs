using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ANX.Framework.Content.Pipeline.Helpers
{
    public static class PropertyExtensions
    {
        public static TypeConverter GetConverter(this PropertyInfo property)
        {
            TypeConverterAttribute typeConverterAttribute = (TypeConverterAttribute)property.GetCustomAttributes(typeof(TypeConverterAttribute), true).FirstOrDefault();
            if (typeConverterAttribute != null && typeConverterAttribute.ConverterTypeName != null && typeConverterAttribute.ConverterTypeName.Length > 0)
            {
                Type converterType = TypeHelper.GetType(typeConverterAttribute.ConverterTypeName);
                if (converterType != null && typeof(TypeConverter).IsAssignableFrom(converterType))
                {
                    return (TypeConverter)CreateInstance(converterType, property.PropertyType);
                }
            }

            return TypeDescriptor.GetConverter(property.PropertyType);
        }

        private static object CreateInstance(Type targetType, Type typeParam)
        {
            var parameterTypes = new Type[] { typeof(Type) };

            ConstructorInfo constructor = targetType.GetConstructor(parameterTypes);
            if (constructor != null)
            {
                return TypeDescriptor.CreateInstance(null, targetType, parameterTypes, new object[] { typeParam });
            }
            return TypeDescriptor.CreateInstance(null, targetType, null, null);
        }
    }
}
