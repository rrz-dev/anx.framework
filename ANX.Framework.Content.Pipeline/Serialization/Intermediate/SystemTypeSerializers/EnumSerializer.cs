#region Using Statements
using ANX.Framework.Content.Pipeline.Helpers;
using ANX.Framework.NonXNA.Development;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
#endregion

namespace ANX.Framework.Content.Pipeline.Serialization.Intermediate.SystemTypeSerializers
{
    [Developer("KorsarNek")]
    [PercentageComplete(100)]
    [TestState(TestStateAttribute.TestState.Untested)]
    internal class EnumSerializer : ContentTypeSerializer
    {
        public EnumSerializer(Type targetType)
            : base(targetType)
        {

        }

        protected internal override void Serialize(IntermediateWriter output, object value, ContentSerializerAttribute format)
        {
            if (output == null)
                throw new ArgumentNullException("output");
            
            if (value == null)
                throw new ArgumentNullException("value");

            if (value == null || !TargetType.IsAssignableFrom(value.GetType()))
                throw new ArgumentException(string.Format("The parameter \"{0}\" must be of type {1}", "value", TargetType));

            output.Xml.WriteString(value.ToString());
        }

        protected internal override object Deserialize(IntermediateReader input, ContentSerializerAttribute format, object existingInstance)
        {
            if (input == null)
                throw new ArgumentNullException("input");

            string text = input.Xml.ReadContentAsString();
            object result;
            try
            {
                result = Enum.Parse(base.TargetType, text);
            }
            catch (ArgumentException innerException)
            {
                throw ExceptionHelper.CreateInvalidContentException(input.Xml, input.BasePath, innerException, string.Format("XML contains invalid value \"{0}\" for enum {1}.", text, TargetType));
            }
            return result;
        }
    }
}
