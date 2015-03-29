using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ANX.Framework.Content.Pipeline.Serialization.Intermediate
{
    class ReflectiveSerializerFieldHelper : ReflectiveSerializerMemberHelper
    {
        FieldInfo fieldInfo;
        IntermediateSerializer serializer;

        public ReflectiveSerializerFieldHelper(IntermediateSerializer serializer, FieldInfo fieldInfo)
            : base(serializer, fieldInfo, fieldInfo.FieldType)
        {
            this.serializer = serializer;
            this.fieldInfo = fieldInfo;
        }

        public override object GetValue(object instance)
        {
            return fieldInfo.GetValue(instance);
        }

        public override void SetValue(object instance, object value)
        {
            fieldInfo.SetValue(instance, value);
        }

        public override bool CanSetValue()
        {
            return !this.fieldInfo.IsInitOnly && !fieldInfo.IsLiteral;
        }

        public override bool ShouldSerialize(Type declaringType)
        {
            return !fieldInfo.IsDefined(typeof(ContentSerializerIgnoreAttribute), false) && 
                (fieldInfo.IsPublic || Attribute.GetCustomAttribute(fieldInfo, typeof(ContentSerializerAttribute)) != null) &&
                (CanSetValue() || serializer.GetTypeSerializer(fieldInfo.FieldType).CanDeserializeIntoExistingObject) && 
                (!declaringType.IsValueType || !IsSharedResource(fieldInfo));
       
        }
    }
}
