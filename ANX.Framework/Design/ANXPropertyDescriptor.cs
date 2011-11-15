using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Reflection;

namespace ANX.Framework.Design
{
    public class ANXPropertyDescriptor : PropertyDescriptor
    {
        private PropertyInfo property;

        public ANXPropertyDescriptor(PropertyInfo property)
            : base(property.Name, (Attribute[])property.GetCustomAttributes(typeof(Attribute), true))
        {
            if (property == null)
            {
                throw new ArgumentNullException("property");
            }
            this.property = property;
        }

        public override bool CanResetValue(object component)
        {
            return false;
        }

        public override Type ComponentType
        {
            get { return property.DeclaringType; }
        }

        public override object GetValue(object component)
        {
            return property.GetValue(component, null);
        }

        public override bool IsReadOnly
        {
            get { return false; }
        }

        public override Type PropertyType
        {
            get { return property.PropertyType; }
        }

        public override void ResetValue(object component)
        {
            
        }

        public override void SetValue(object component, object value)
        {
            this.property.SetValue(component, value, null);
            this.OnValueChanged(component, EventArgs.Empty);
        }

        public override bool ShouldSerializeValue(object component)
        {
            return true;
        }
    }
}
