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

    public class MatrixConverter : MathTypeConverter
    {
        public MatrixConverter()
        {
            base.propertyDescriptions = MathTypeConverter.CreateFieldDescriptor<Matrix>(
                "M11", "M12", "M13", "M14",
                "M21", "M22", "M23", "M24",
                "M31", "M32", "M33", "M34",
                "M41", "M42", "M43", "M44");
            supportStringConvert = false;
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType == null)
            {
                throw new ArgumentNullException("destinationType");
            }
            if (value is Matrix)
            {
                Matrix instance = (Matrix)value;

                if (destinationType == typeof(InstanceDescriptor))
                {
                    var constructor = typeof(Matrix).GetConstructor(new Type[] { 
                        typeof(float), typeof(float), typeof(float), typeof(float),
                        typeof(float), typeof(float), typeof(float), typeof(float),
                        typeof(float), typeof(float), typeof(float), typeof(float),
                        typeof(float), typeof(float), typeof(float), typeof(float)
                    });
                    return new InstanceDescriptor(constructor, new object[] { 
                        instance.M11, instance.M12, instance.M13, instance.M14, 
                        instance.M21, instance.M22, instance.M23, instance.M24,
                        instance.M31, instance.M32, instance.M33, instance.M34,
                        instance.M41, instance.M42, instance.M43, instance.M44
                    });
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
            return new Matrix(
                (float)propertyValues["M11"], (float)propertyValues["M12"], (float)propertyValues["M13"], (float)propertyValues["M14"],
                (float)propertyValues["M21"], (float)propertyValues["M22"], (float)propertyValues["M23"], (float)propertyValues["M24"],
                (float)propertyValues["M31"], (float)propertyValues["M32"], (float)propertyValues["M33"], (float)propertyValues["M34"],
                (float)propertyValues["M41"], (float)propertyValues["M42"], (float)propertyValues["M43"], (float)propertyValues["M44"]);
        }
    }

#endif
}
