using ANX.Framework.Content.Pipeline.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ANX.Framework.Content.Pipeline.Serialization.Intermediate
{
    internal abstract class ReflectiveSerializerMemberHelper
    {
        private ContentTypeSerializer typeSerializer;
        private ContentSerializerAttribute formatAttribute;

        private IntermediateSerializer serializer;
        private MemberInfo memberInfo;
        private Type memberType;

        public ReflectiveSerializerMemberHelper(IntermediateSerializer serializer, MemberInfo memberInfo, Type memberType)
        {
            this.serializer = serializer;
            this.memberInfo = memberInfo;
            this.memberType = memberType;
        }

        public virtual void Initialize()
        {
            this.typeSerializer = serializer.GetTypeSerializer(memberType);

            formatAttribute = Attribute.GetCustomAttribute(memberInfo, typeof(ContentSerializerAttribute)) as ContentSerializerAttribute;
            if (formatAttribute == null)
            {
                this.formatAttribute = new ContentSerializerAttribute();
                this.formatAttribute.ElementName = memberInfo.Name;
            }
            else if (this.formatAttribute.ElementName == null)
            {
                this.formatAttribute = this.formatAttribute.Clone();
                this.formatAttribute.ElementName = memberInfo.Name;
            }
        }

        public abstract object GetValue(object instance);

        public abstract void SetValue(object instance, object value);

        public abstract bool CanSetValue();

        public abstract bool ShouldSerialize(Type declaringType);

        public void Serialize(IntermediateWriter output, object parentInstance)
        {
            object value = this.GetValue(parentInstance);

            if (value == null && !this.formatAttribute.AllowNull)
                throw new InvalidOperationException(string.Format("XML element \"{0}\" is not allowed to be null.", this.formatAttribute.ElementName));
            

            if (this.formatAttribute.Optional)
            {
                if (value == null)
                {
                    return;
                }
                if (this.CanSetValue() == false && this.typeSerializer.ObjectIsEmpty(value))
                {
                    return;
                }
            }

            if (this.formatAttribute.SharedResource)
            {
                output.WriteSharedResource<object>(value, this.formatAttribute);
            }
            else
            {
                output.WriteObject<object>(value, this.formatAttribute, this.typeSerializer);
            }
        }

        public void Deserialize(IntermediateReader input, object parentInstance)
        {
            if (!this.formatAttribute.FlattenContent)
            {
                if (this.formatAttribute.Optional)
                {
                    if (!input.Xml.CheckForElement(this.formatAttribute.ElementName))
                    {
                        return;
                    }
                }
                else
                {
                    input.Xml.MoveToContent();
                }
            }

            if (this.formatAttribute.SharedResource)
            {
                this.DeserializeSharedResource(input, parentInstance);
            }
            else
            {
                this.DeserializeRegularObject(input, parentInstance);
            }
        }

        private void DeserializeRegularObject(IntermediateReader input, object parentInstance)
        {
            if (this.CanSetValue())
            {
                object value = input.ReadObject<object>(this.formatAttribute, this.typeSerializer);
                this.SetValue(parentInstance, value);
            }
            else
            {
                object obj = this.GetValue(parentInstance);
                if (obj == null)
                    throw ExceptionHelper.CreateInvalidContentException(input.Xml, input.BasePath, null, string.Format("Cannot deserialize XML element \"{0}\" because this member is both null and read-only.", this.formatAttribute.ElementName));


                input.ReadObject<object>(this.formatAttribute, this.typeSerializer, obj);
            }
        }

        private void DeserializeSharedResource(IntermediateReader input, object parentInstance)
        {
            if (this.CanSetValue() == false)
                throw new InvalidOperationException("Members marked with ContentSerializerAttribute.SharedResource cannot be read-only.");


            input.ReadSharedResource<object>(this.formatAttribute, (object value) =>
            {
                if (value == null || !this.typeSerializer.TargetType.IsAssignableFrom(value.GetType()))
                {
                    throw ExceptionHelper.CreateInvalidContentException(input.Xml, input.BasePath, null, string.Format("XML specifies invalid type for shared resource. Expecting a subclass of \"{0}\", but XML contains \"{1}\".",
                        this.typeSerializer.TargetType,
                        value.GetType()
                    ));
                }

                this.SetValue(parentInstance, value);
            });
        }

        public bool ObjectIsEmpty(object parent)
        {
            object obj = this.GetValue(parent);
            return obj == null || this.typeSerializer.ObjectIsEmpty(obj);
        }

        protected static bool IsSharedResource(MemberInfo memberInfo)
        {
            Attribute customAttribute = Attribute.GetCustomAttribute(memberInfo, typeof(ContentSerializerAttribute));
            return customAttribute != null && ((ContentSerializerAttribute)customAttribute).SharedResource;
        }
    }
}
