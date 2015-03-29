using ANX.Framework.NonXNA.Development;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ANX.Framework.Content.Pipeline.Serialization.Intermediate
{
    /// <summary>
    /// Provides methods for serializing and deserializing a specific managed type.
    /// </summary>
    [Developer("KorsarNek")]
    [TestState(TestStateAttribute.TestState.Tested)]
    [PercentageComplete(100)]
    public abstract class ContentTypeSerializer
    {
        #region constructors

        /// <summary>
        /// Initializes a new instance of the ContentTypeSerializer class for serializing the specified type.
        /// </summary>
        /// <param name="targetType">The target type.</param>
        protected ContentTypeSerializer(Type targetType)
            : this(targetType, null)
        {

        }

        /// <summary>
        /// Initializes a new instance of the ContentTypeSerializer class for serializing the specified type using the specified XML shortcut name.
        /// </summary>
        /// <param name="targetType">The target type.</param>
        /// <param name="xmlTypeName">The XML shortcut name.</param>
        protected ContentTypeSerializer(Type targetType, string xmlTypeName)
        {
            if (targetType == null)
                throw new ArgumentNullException("targetType");
            
            this.TargetType = targetType;
            this.XmlTypeName = xmlTypeName;
        }

        #endregion

        #region properties 
        /// <summary>
        /// Gets the type handled by this serializer component.
        /// </summary>
        public Type TargetType
        {
            get;
            private set;
        }
 
        /// <summary>
        /// Gets a short-form XML name for the target type, or null if there is none.
        /// </summary>
        public string XmlTypeName
        {
            get;
            private set;
        }
    
        /// <summary>
        /// Gets a value indicating whether this component may load data into an existing object or if it must it construct a new instance of the object before loading the data.
        /// </summary>
        public virtual bool CanDeserializeIntoExistingObject
        {
            get
            {
                return false;
            }
        }

        /// <summary>
        /// Gets a value indicating whether this component writes the data only as string and not with additional XML elements.
        /// </summary>
        /// <remarks>Used for the serialization of collections, determines if the items can be written as flat content or need surrounding elements.</remarks>
        public virtual bool HasOnlyFlatContent
        {
            get
            {
                return false;
            }
        }

        #endregion

        #region methods

        /// <summary>
        /// Retrieves and caches any nested type serializers and allows reflection over the target data type.
        /// </summary>
        /// <param name="serializer">The content serializer.</param>
        protected internal virtual void Initialize(IntermediateSerializer serializer)
        {

        }

        /// <summary>
        /// Serializes an object to intermediate XML format.
        /// </summary>
        /// <param name="output">Specifies the intermediate XML location, and provides various serialization helpers.</param>
        /// <param name="value">The strongly typed object to be serialized.</param>
        /// <param name="format">Specifies the content format for this object.</param>
        protected internal abstract void Serialize(IntermediateWriter output, object value, ContentSerializerAttribute format);

        /// <summary>
        /// Deserializes an object from intermediate XML format.
        /// </summary>
        /// <param name="input">Location of the intermediate XML and various deserialization helpers.</param>
        /// <param name="format">Specifies the intermediate source XML format.</param>
        /// <param name="existingInstance">The object containing the received data, or null if the deserializer should construct a new instance.</param>
        /// <returns></returns>
        protected internal abstract object Deserialize(IntermediateReader input, ContentSerializerAttribute format, object existingInstance);

        /// <summary>
        /// Queries whether an object contains data to be serialized.
        /// </summary>
        /// <param name="value">The object to query.</param>
        /// <returns></returns>
        public virtual bool ObjectIsEmpty(object value)
        {
            return false;
        }

        #endregion
    }

    /// <summary>
    /// Provides a generic implementation of ContentTypeSerializer methods and properties for serializing and deserializing a specific managed type.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [Developer("KorsarNek")]
    [TestState(TestStateAttribute.TestState.Tested)]
    [PercentageComplete(100)]
    public abstract class ContentTypeSerializer<T> : ContentTypeSerializer
    {
        #region constructors

        /// <summary>
        /// Initializes a new instance of the ContentTypeSerializer class.
        /// </summary>
        protected ContentTypeSerializer()
            : base(typeof(T))
        {

        }

        /// <summary>
        /// Initializes a new instance of the ContentTypeSerializer class using the specified XML shortcut name.
        /// </summary>
        /// <param name="xmlTypeName">The XML shortcut name.</param>
        protected ContentTypeSerializer(string xmlTypeName)
            : base(typeof(T), xmlTypeName)
        {

        }

        #endregion

        #region methods

        /// <summary>
        /// Serializes an object to intermediate XML format.
        /// </summary>
        /// <param name="output">Specifies the intermediate XML location, and provides various serialization helpers.</param>
        /// <param name="value">The strongly typed object to be serialized.</param>
        /// <param name="format">Specifies the content format for this object.</param>
        protected abstract void Serialize(IntermediateWriter output, T value, ContentSerializerAttribute format);

        /// <summary>
        /// Serializes an object to intermediate XML format.
        /// </summary>
        /// <param name="output">Specifies the intermediate XML location, and provides various serialization helpers.</param>
        /// <param name="value">The strongly typed object to be serialized.</param>
        /// <param name="format">Specifies the content format for this object.</param>
        protected internal override void Serialize(IntermediateWriter output, object value, ContentSerializerAttribute format)
        {
            this.Serialize(output, (T)value, format);
        }

        /// <summary>
        /// Deserializes a strongly typed object from intermediate XML format.
        /// </summary>
        /// <param name="input">Location of the intermediate XML and various deserialization helpers.</param>
        /// <param name="format">Specifies the intermediate source XML format.</param>
        /// <param name="existingInstance">The strongly typed object containing the received data, or null if the deserializer should construct a new instance.</param>
        /// <returns></returns>
        protected abstract T Deserialize(IntermediateReader input, ContentSerializerAttribute format, T existingInstance);

        /// <summary>
        /// Deserializes an object from intermediate XML format.
        /// </summary>
        /// <param name="input">Location of the intermediate XML and various deserialization helpers.</param>
        /// <param name="format">Specifies the intermediate source XML format.</param>
        /// <param name="existingInstance">The object containing the received data, or null if the deserializer should construct a new instance.</param>
        /// <returns></returns>
        protected internal override object Deserialize(IntermediateReader input, ContentSerializerAttribute format, object existingInstance)
        {
            T tObject;
            if (existingInstance == null)
            {
                tObject = default(T);
            }
            else
            {
                tObject = (T)existingInstance;
            }
            return this.Deserialize(input, format, tObject);
        }

        /// <summary>
        /// Queries whether an object contains data to be serialized.
        /// </summary>
        /// <param name="value">The object to query.</param>
        /// <returns></returns>
        public virtual bool ObjectIsEmpty(T value)
        {
            return base.ObjectIsEmpty(value);
        }

        /// <summary>
        /// Queries whether an object contains data to be serialized.
        /// </summary>
        /// <param name="value">The object to query.</param>
        /// <returns></returns>
        public override bool ObjectIsEmpty(object value)
        {
            return this.ObjectIsEmpty((T)value);
        }

        #endregion
    }
}
