﻿using System;
using System.Text;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace ANX.Framework.Design
{
    public class MathTypeConverter : ExpandableObjectConverter
    {
        protected PropertyDescriptorCollection propertyDescriptions;

        protected bool supportStringConvert = true;

        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            if (this.supportStringConvert && sourceType == typeof(String))
            {
                return true;
            }
            return base.CanConvertFrom(context, sourceType);
        }

        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            if (destinationType == typeof(InstanceDescriptor))
            {
                return true;
            }
            return base.CanConvertTo(context, destinationType);
        }

        public override bool GetCreateInstanceSupported(ITypeDescriptorContext context)
        {
            return true;
        }

        public override bool GetPropertiesSupported(ITypeDescriptorContext context)
        {
            return true;
        }

        public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
        {
            return propertyDescriptions;
        }

        protected static PropertyDescriptorCollection CreateFieldDescriptor<T>(params string[] fields)
        {
            Type type = typeof(T);
            PropertyDescriptor[] descriptor = new PropertyDescriptor[fields.Length];
            for (int i = 0; i < fields.Length; i++)
            {
                descriptor[i] = new ANXFieldDescriptor(type.GetField(fields[i]));
            }
            return new PropertyDescriptorCollection(descriptor);
        }

        protected static PropertyDescriptorCollection CreatePropertyDescriptor<T>(params string[] fields)
        {
            Type type = typeof(T);
            PropertyDescriptor[] descriptor = new PropertyDescriptor[fields.Length];
            for (int i = 0; i < fields.Length; i++)
            {
                descriptor[i] = new ANXPropertyDescriptor(type.GetProperty(fields[i]));
            }
            return new PropertyDescriptorCollection(descriptor);
        }

        protected static string ConvertToString<T>(ITypeDescriptorContext context, CultureInfo culture, T[] values)
        {
            string separator = "; ";
            TypeConverter converter = TypeDescriptor.GetConverter(typeof(T));
            StringBuilder builder = new StringBuilder();
            foreach (var item in values)
            {
                builder.Append(converter.ConvertToString(context, culture, item));
                builder.Append(separator);
            }
            return builder.ToString();
        }

        protected static T[] ConvertFromString<T>(ITypeDescriptorContext context, CultureInfo culture, string value)
        {
            if (value == null)
            {
                return null;
            }
            if (culture == null)
            {
                throw new ArgumentNullException("culture");
            }
            value = value.Trim();

            string[] values = value.Split(';');
            T[] result = new T[values.Length];
            TypeConverter converter = TypeDescriptor.GetConverter(typeof(T));
            for (int i = 0; i < values.Length; i++)
            {
                result[i] = (T)converter.ConvertFromString(context, culture, values[i]);
            }
            return result;
        }
    }
}