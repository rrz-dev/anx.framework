using System;
using System.ComponentModel;
using System.Reflection;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Design
{
#if !WINDOWSMETRO
	public class ANXFieldDescriptor : PropertyDescriptor
	{
		private FieldInfo field;

		public ANXFieldDescriptor(FieldInfo field)
			: base(field.Name, (Attribute[])field.GetCustomAttributes(typeof(Attribute), true))
		{
			if (field == null)
				throw new ArgumentNullException("field");

			this.field = field;
		}

		public override bool CanResetValue(object component)
		{
			return false;
		}

		public override Type ComponentType
		{
			get
			{
				return field.DeclaringType;
			}
		}

		public override object GetValue(object component)
		{
			return field.GetValue(component);
		}

		public override bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		public override Type PropertyType
		{
			get
			{
				return field.FieldType;
			}
		}

		public override void ResetValue(object component)
		{
		}

		public override void SetValue(object component, object value)
		{
			this.field.SetValue(component, value);
			this.OnValueChanged(component, EventArgs.Empty);
		}

		public override bool ShouldSerializeValue(object component)
		{
			return true;
		}
	}
#endif
}
