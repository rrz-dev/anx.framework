using ANX.Framework.Content.Pipeline.Helpers;
using ANX.Framework.NonXNA.Development;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace ANX.Framework.Content.Pipeline.Serialization.Intermediate
{
    internal class ReflectiveSerializer : ContentTypeSerializer
    {
        //If have to use a reflective serializer, we want to check if atleast a serializer for an interface can handle some of the properties.
        //That way, we handle the default behaviour of collections.
        private Dictionary<ContentTypeSerializer, Type> interfaceSerializers = new Dictionary<ContentTypeSerializer, Type>();
        private ContentTypeSerializer baseSerializer;
        private List<ReflectiveSerializerMemberHelper> memberHelpers = new List<ReflectiveSerializerMemberHelper>();
        private string collectionItemName;

        public override bool CanDeserializeIntoExistingObject
        {
            get
            {
                return base.TargetType.IsClass && ((this.baseSerializer != null && this.baseSerializer.CanDeserializeIntoExistingObject) || this.memberHelpers.Count > 0 || interfaceSerializers.Count > 0);
            }
        }

        public ReflectiveSerializer(Type targetType)
            : base(targetType)
        {
            
        }

        protected internal override void Initialize(IntermediateSerializer serializer)
        {
            if (base.TargetType.BaseType != null && base.TargetType.BaseType != typeof(object))
            {
                this.baseSerializer = serializer.GetTypeSerializer(base.TargetType.BaseType);
            }

            object[] customAttributes = base.TargetType.GetCustomAttributes(typeof(ContentSerializerCollectionItemNameAttribute), true);
            if (customAttributes.Length == 1)
            {
                this.collectionItemName = ((ContentSerializerCollectionItemNameAttribute)customAttributes[0]).CollectionItemName;
            }
            
            List<InterfaceMapping> handledInterfaces = new List<InterfaceMapping>();
            foreach (Type @interface in base.TargetType.GetInterfaces())
            {
                //interface has been implemented by the current type and no other serializer cared to handle that.
                //TODO: Die Kontrolle ob das Ziel-interface das momentane beinhält funktioniert nicht.
                if (base.TargetType != @interface && (!base.TargetType.IsInterface || base.TargetType.GetInterface(@interface.Name) == null))
                {
                    var interfaceMap = base.TargetType.GetInterfaceMap(@interface);
                    if (interfaceMap.IsDefinedBy(base.TargetType))
                    {
                        ContentTypeSerializer iSerializer = serializer.GetTypeSerializer(@interface);
                        if (iSerializer != null && iSerializer.GetType() != typeof(ReflectiveSerializer))
                        {
                            if (!iSerializer.CanDeserializeIntoExistingObject)
                            {
                                throw new InvalidOperationException(string.Format("Tried to serialize the type \"{0}\" with intermediate serializer for interface \"{1}\", but the serializer can't deserialize into an existing object.", this.TargetType, @interface));
                            }

                            interfaceSerializers.Add(iSerializer, @interface);
                            handledInterfaces.Add(interfaceMap);
                        }
                    }
                }
            }

            //Create the member helpers.
            BindingFlags bindingAttr = BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;
            foreach (var propertyInfo in this.TargetType.GetProperties(bindingAttr))
            {
                bool skipProperty = false;
                foreach (InterfaceMapping mapping in handledInterfaces)
                {
                    if (mapping.TargetMethods.Intersect(propertyInfo.GetAccessors()).Count() > 0)
                        skipProperty = true;
                }

                if (skipProperty == false)
                {
                    ReflectiveSerializerMemberHelper item = new ReflectiveSerializerPropertyHelper(serializer, propertyInfo);
                    if (item.ShouldSerialize(this.TargetType))
                    {
                        item.Initialize();
                        memberHelpers.Add(item);
                    }
                }
            }

            foreach (var fieldInfo in this.TargetType.GetFields(bindingAttr))
            {
                ReflectiveSerializerMemberHelper item = new ReflectiveSerializerFieldHelper(serializer, fieldInfo);
                if (item.ShouldSerialize(this.TargetType))
                {
                    item.Initialize();
                    memberHelpers.Add(item);
                }
            }
        }

        protected internal override void Serialize(IntermediateWriter output, object value, ContentSerializerAttribute format)
        {
            if (output == null)
                throw new ArgumentNullException("output");
            
            if (value == null)
                throw new ArgumentNullException("value");
            
            if (!base.TargetType.IsAssignableFrom(value.GetType()))
                throw new ArgumentException(string.Format("Invalid argument type. Expecting a subclass of {0}, but {1} was passed.", base.TargetType, value.GetType()));
            

            if (this.baseSerializer != null)
            {
                this.baseSerializer.Serialize(output, value, format);
            }
            foreach (ReflectiveSerializerMemberHelper current in this.memberHelpers)
            {
                current.Serialize(output, value);
            }

            if (interfaceSerializers.Count > 0)
            {
                //We have already written the own element i.e. <Keys> on the call to this serializer. We don't need it write it again with the call 
                var interfaceFormat = format.Clone();
                interfaceFormat.FlattenContent = true;
                foreach (var keyValuePair in interfaceSerializers)
                {
                    output.GetType().GetGenericMethod("WriteRawObject", new Type[] { keyValuePair.Value, typeof(ContentSerializerAttribute), typeof(ContentTypeSerializer) }).MakeGenericMethod(keyValuePair.Value).
                        Invoke(output, new object[] { value, interfaceFormat, keyValuePair.Key });
                }
            }
        }

        protected internal override object Deserialize(IntermediateReader input, ContentSerializerAttribute format, object existingInstance)
        {
            if (input == null)
                throw new ArgumentNullException("input");
            
            object obj = existingInstance;
            if (obj == null)
            {
                obj = Activator.CreateInstance(this.TargetType, nonPublic: true);
            }

            if (this.baseSerializer != null)
            {
                object baseObject = this.baseSerializer.Deserialize(input, format, obj);
                if (baseObject != obj)
                {
                    throw new InvalidOperationException(string.Format("Intermediate ContentTypeSerializer {0} (handling type {1}) returned a new object instance from its Deserialize method. This should have loaded data into the existingInstance parameter.",
                        this.baseSerializer.GetType(),
                        this.baseSerializer.TargetType
                    ));
                }
            }

            foreach (ReflectiveSerializerMemberHelper current in this.memberHelpers)
            {
                current.Deserialize(input, obj);
            }

            foreach (var keyValuePair in interfaceSerializers)
            {
                object interfaceObj = keyValuePair.Key.Deserialize(input, format, obj);

                if (interfaceObj != obj)
                {
                    throw new InvalidOperationException(string.Format("Intermediate ContentTypeSerializer {0} (handling type {1}) returned a new object instance from its Deserialize method. This should have loaded data into the existingInstance parameter.",
                        keyValuePair.Key.GetType(),
                        keyValuePair.Key.TargetType
                    ));
                }
            }
            return obj;
        }

        public override bool ObjectIsEmpty(object value)
        {
            if (value == null)
                throw new ArgumentNullException("value");
            
            if (this.baseSerializer != null && !this.baseSerializer.ObjectIsEmpty(value))
            {
                return false;
            }
            foreach (ReflectiveSerializerMemberHelper current in this.memberHelpers)
            {
                if (!current.ObjectIsEmpty(value))
                {
                    return false;
                }
            }
            foreach (var keyValuePair in interfaceSerializers)
            {
                if (!keyValuePair.Key.ObjectIsEmpty(value))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
