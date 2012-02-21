#region Using Statements
using System;
using System.Collections;
using System.ComponentModel;
#if !WIN8
using System.ComponentModel.Design.Serialization;
#endif
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Reflection;
using System.Text;

#endregion // Using Statements

#region License

//
// This file is part of the ANX.Framework created by the "ANX.Framework developer group".
//
// This file is released under the Ms-PL license.
//
//
//
// Microsoft Public License (Ms-PL)
//
// This license governs use of the accompanying software. If you use the software, you accept this license. 
// If you do not accept the license, do not use the software.
//
// 1.Definitions
//   The terms "reproduce," "reproduction," "derivative works," and "distribution" have the same meaning 
//   here as under U.S. copyright law.
//   A "contribution" is the original software, or any additions or changes to the software.
//   A "contributor" is any person that distributes its contribution under this license.
//   "Licensed patents" are a contributor's patent claims that read directly on its contribution.
//
// 2.Grant of Rights
//   (A) Copyright Grant- Subject to the terms of this license, including the license conditions and limitations 
//       in section 3, each contributor grants you a non-exclusive, worldwide, royalty-free copyright license to 
//       reproduce its contribution, prepare derivative works of its contribution, and distribute its contribution
//       or any derivative works that you create.
//   (B) Patent Grant- Subject to the terms of this license, including the license conditions and limitations in 
//       section 3, each contributor grants you a non-exclusive, worldwide, royalty-free license under its licensed
//       patents to make, have made, use, sell, offer for sale, import, and/or otherwise dispose of its contribution 
//       in the software or derivative works of the contribution in the software.
//
// 3.Conditions and Limitations
//   (A) No Trademark License- This license does not grant you rights to use any contributors' name, logo, or trademarks.
//   (B) If you bring a patent claim against any contributor over patents that you claim are infringed by the software, your 
//       patent license from such contributor to the software ends automatically.
//   (C) If you distribute any portion of the software, you must retain all copyright, patent, trademark, and attribution 
//       notices that are present in the software.
//   (D) If you distribute any portion of the software in source code form, you may do so only under this license by including
//       a complete copy of this license with your distribution. If you distribute any portion of the software in compiled or 
//       object code form, you may only do so under a license that complies with this license.
//   (E) The software is licensed "as-is." You bear the risk of using it. The contributors give no express warranties, guarantees,
//       or conditions. You may have additional consumer rights under your local laws which this license cannot change. To the
//       extent permitted under your local laws, the contributors exclude the implied warranties of merchantability, fitness for a 
//       particular purpose and non-infringement.

#endregion // License

namespace ANX.Framework.Design
{
#if !WIN8      //TODO: search replacement for Win8

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
            for (int i = 0; i < values.Length; i++)
            {
                builder.Append(converter.ConvertToString(context, culture, values[i]));
                if (i < values.Length - 1)
                {
                    builder.Append(separator);
                }
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

#endif
}
