using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ANX.Framework.Content.Pipeline.Serialization.Intermediate
{
    //No special handling for this class itself, it just makes it easier to follow the rules for ContentTypeSerializers that try to handle interfaces.
    /// <summary>
    /// Abstract base class for serializers that want to handle interfaces.
    /// </summary>
    /// <remarks>An interface serializer can be called several times for the same object, whenever a type implements the interface. Inherited implementations are not
    /// affected from this. That means, if a parent class implemented the interface, the serializer won't be called on the child classes.<br/>
    /// The serializer also must be able to deserialize into an existing object.</remarks>
    /// <typeparam name="T"></typeparam>
    public abstract class ContentInterfaceSerializer<T> : ContentTypeSerializer<T>
    {
        /// <summary>
        /// Creates a new <see cref="ContentInterfaceSerializer"/> instance.
        /// </summary>
        public ContentInterfaceSerializer()
            : base()
        {
            //There's no where-restriction to say that T must be an interface. So we check it in the constructor.
            if (!this.TargetType.IsInterface)
                throw new InvalidOperationException(string.Format("The generic argument for {0} in the type \"{1}\" isn't an interface.", typeof(ContentInterfaceSerializer<>).Name, this.GetType().FullName));
        }

        /// <summary>
        /// Returns true that this component can load data into an existing object.
        /// <remarks>
        /// <see cref="ContentInterfaceSerializer"/>s are obligated to always serialize into existing objects.</remarks>
        /// </summary>
        public sealed override bool CanDeserializeIntoExistingObject
        {
            get
            {
                return true;
            }
        }
    }
}
