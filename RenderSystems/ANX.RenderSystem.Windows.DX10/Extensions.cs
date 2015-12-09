using SharpDX;
using SharpDX.D3DCompiler;
using SharpDX.Direct3D10;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dx10 = SharpDX.Direct3D10;

namespace ANX.RenderSystem.Windows.DX10
{
    static class Extensions
    {
        public static Dx10.CpuAccessFlags ToCpuAccessFlags(this ResourceMapping mapping)
        {
            Dx10.CpuAccessFlags flags = Dx10.CpuAccessFlags.None;
            if ((mapping & ResourceMapping.Read) != 0)
                flags |= Dx10.CpuAccessFlags.Read;
            if ((mapping & ResourceMapping.Write) != 0)
                flags |= Dx10.CpuAccessFlags.Write;

            return flags;
        }

        public static Dx10.MapMode ToMapMode(this ResourceMapping mapping)
        {
            return (Dx10.MapMode)mapping;
        }

        public static ResourceRegion ToResourceRegion(this Framework.Rectangle rect)
        {
            return new ResourceRegion()
                {
                    Back = 1,
                    Bottom = rect.Bottom,
                    Front = 0,
                    Left = rect.Left,
                    Right = rect.Right,
                    Top = rect.Top,
                };
        }

        public static ANX.Framework.Graphics.EffectParameterClass ToParameterClass(this ShaderVariableClass variableClass)
        {
            return DxFormatConverter.Translate(variableClass);
        }

        public static ANX.Framework.Graphics.EffectParameterType ToParameterType(this ShaderVariableType variableType)
        {
            return DxFormatConverter.Translate(variableType);
        }
    }
}
