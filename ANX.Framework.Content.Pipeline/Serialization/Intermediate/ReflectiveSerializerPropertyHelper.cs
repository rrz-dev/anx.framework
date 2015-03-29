using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ANX.Framework.Content.Pipeline.Serialization.Intermediate
{
    class ReflectiveSerializerPropertyHelper : ReflectiveSerializerMemberHelper
    {
        IntermediateSerializer serializer;
        PropertyInfo propertyInfo;

        public ReflectiveSerializerPropertyHelper(IntermediateSerializer serializer, PropertyInfo propertyInfo)
            : base(serializer, propertyInfo, propertyInfo.PropertyType)
        {
            this.serializer = serializer;
            this.propertyInfo = propertyInfo;
        }

        public override object GetValue(object instance)
        {
            return propertyInfo.GetValue(instance, null);
        }

        public override void SetValue(object instance, object value)
        {
            propertyInfo.SetValue(instance, value, null);
        }

        public override bool CanSetValue()
        {
            return propertyInfo.CanWrite;
        }

        public override bool ShouldSerialize(Type declaringType)
        {
            if (propertyInfo.GetIndexParameters().Length > 0 || !propertyInfo.CanRead || propertyInfo.IsDefined(typeof(ContentSerializerIgnoreAttribute), false))
            {
                return false;
            }

            bool setterPublic = propertyInfo.GetSetMethod() != null;
            
            ContentTypeSerializer typeSerializer = this.serializer.GetTypeSerializer(propertyInfo.PropertyType);
            foreach (var accessor in propertyInfo.GetAccessors(true))
            {
                //We only want to serialize properties of the target type. If a property is overriden, the serializer for the base type should handle that.
                if (accessor.GetBaseDefinition() != accessor)
                {
                    return false;
                }
            }

            return ((serializer.GetTypeSerializer(propertyInfo.PropertyType).CanDeserializeIntoExistingObject) ||
                   (propertyInfo.CanWrite && (setterPublic || Attribute.GetCustomAttribute(propertyInfo, typeof(ContentSerializerAttribute)) != null))) &&
                   (!declaringType.IsValueType || !IsSharedResource(propertyInfo));

        }
    }
}
