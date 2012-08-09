#region Using Statements
using System;
using System.Reflection;
#endregion // Using Statements

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Content
{
#if !WINDOWSMETRO

    public class ReflectiveReader<T> : ContentTypeReader
    {
        private int typeVersion;
        public override int TypeVersion { get { return typeVersion; } }
        public ContentTypeReader TargetTypeReader { get; private set; }
        private ConstructorInfo constructor;

        public ReflectiveReader()
            : base(typeof(T))
        {
            BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;
            this.constructor = TargetType.GetConstructor(bindingFlags, null, Type.EmptyTypes, null);

            object[] attr = TargetType.GetCustomAttributes(typeof(ContentSerializerTypeVersionAttribute), false);
            if (attr.Length == 1)
            {
                this.typeVersion = (attr[0] as ContentSerializerTypeVersionAttribute).TypeVersion;
            }
        }

        protected internal override void Initialize(ContentTypeReaderManager manager)
        {
            if (TargetType != null && TargetType != typeof(object) && !TargetType.IsValueType)
            {
                this.TargetTypeReader = manager.GetTypeReader(TargetType);
            }

            BindingFlags bindingFlags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.DeclaredOnly | BindingFlags.Instance;
            PropertyInfo[] propInfos = TargetType.GetProperties(bindingFlags);
            FieldInfo[] fieldInfos = TargetType.GetFields(bindingFlags);
        }

        protected internal override object Read(ContentReader input, object existingInstance)
        {
            if (input == null)
            {
                throw new ArgumentNullException("input");
            }

            if (existingInstance == null)
            {
                if (constructor != null)
                {
                    existingInstance = constructor.Invoke(null);
                }
                else
                {
                    if (this.TargetType.IsValueType)
                    {
                        existingInstance = default(T);
                    }

                    throw new ContentLoadException(String.Format("No default constructor given for type {0}", TargetType));
                }
            }
            if (TargetTypeReader != null)
            {
                existingInstance = TargetTypeReader.Read(input, existingInstance);
            }
            return existingInstance;    
        }
    }

#endif
}
