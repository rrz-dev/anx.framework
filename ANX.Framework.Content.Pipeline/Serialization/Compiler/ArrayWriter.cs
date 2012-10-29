#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

#endregion

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Content.Pipeline.Serialization.Compiler
{
    internal class ArrayWriter<T> : ContentTypeWriter<T[]>
    {
        private ContentTypeWriter elementWriter;

        protected override void Initialize(ContentCompiler compiler)
        {
            this.elementWriter = compiler.GetTypeWriter(typeof(T));
        }

        protected internal override void Write(ContentWriter output, T[] value)
        {
            output.Write(value.Length);
            for (int i = 0; i < value.Length; i++)
            {
                T value2 = value[i];
                output.WriteObject<T>(value2, this.elementWriter);
            }
        }

        public override string GetRuntimeReader(TargetPlatform targetPlatform)
        {
            return string.Concat(new object[]
			{
				typeof(ContentTypeReader).Namespace,
				'.',
				"ArrayReader`1[[",
				this.elementWriter.GetRuntimeType(targetPlatform),
				"]]"
			});
        }

        public override string GetRuntimeType(TargetPlatform targetPlatform)
        {
            string runtimeType = this.elementWriter.GetRuntimeType(targetPlatform);
            int num = 0;
            for (int i = 0; i < runtimeType.Length; i++)
            {
                char c = runtimeType[i];
                if (c != ',')
                {
                    switch (c)
                    {
                        case '[':
                            num++;
                            break;
                        case ']':
                            num--;
                            break;
                    }
                }
                else
                {
                    if (num == 0)
                    {
                        return runtimeType.Insert(i, "[]");
                    }
                }
            }
            return runtimeType + "[]";
        }

        protected internal override bool ShouldCompressContent(TargetPlatform targetPlatform, object value)
        {
            return this.elementWriter.ShouldCompressContent(targetPlatform, null);
        }
    }
}
