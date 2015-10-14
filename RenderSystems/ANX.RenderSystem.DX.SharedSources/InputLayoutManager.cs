using ANX.Framework.Graphics;
using SharpDX.D3DCompiler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Anx = ANX.Framework.Graphics;

#if DX10
using SharpDX.Direct3D10;
namespace ANX.RenderSystem.Windows.DX10
#elif DX11
using SharpDX.Direct3D11;
namespace ANX.RenderSystem.Windows.DX11
#endif
{
    class InputLayoutManager : IDisposable
    {
#if DEBUG
        private static int layoutCount = 0;
#endif

        private Dictionary<InputLayoutBinding[], InputLayout> layouts = new Dictionary<InputLayoutBinding[], InputLayout>(new InputLayoutBindingCompararer());

        /// <summary>
        /// This method creates a InputLayout which is needed by DirectX 10 for rendering primitives.
        /// The VertexDeclaration of ANX/XNA needs to be mapped to the DirectX 10 types.
        /// </summary>
        public InputLayout GetInputLayout(Device device, ShaderBytecode passSignature, VertexDeclaration vertexDeclaration)
        {
            if (device == null) throw new ArgumentNullException("device");
            if (passSignature == null) throw new ArgumentNullException("passSignature");
            if (vertexDeclaration == null) throw new ArgumentNullException("vertexDeclaration");

            var inputLayoutBinding = new InputLayoutBinding[] { new InputLayoutBinding() { vertexElements = vertexDeclaration.GetVertexElements() } };

            InputLayout layout;
            var vertexElements = vertexDeclaration.GetVertexElements();
            if (!layouts.TryGetValue(inputLayoutBinding, out layout))
            {
                InputElement[] inputElements = new InputElement[vertexElements.Length];
                for (int i = 0; i < vertexElements.Length; i++)
                {
                    inputElements[i] = CreateInputElementFromVertexElement(vertexElements[i]);
                }

                layout = CreateInputLayout(device, passSignature, inputElements);

                layouts.Add(inputLayoutBinding, layout);
            }
            
            return layout;
        }

        public InputLayout GetInputLayout(Device device, ShaderBytecode passSignature, params ANX.Framework.Graphics.VertexBufferBinding[] vertexBufferBindings)
        {
            if (device == null) throw new ArgumentNullException("device");
            if (passSignature == null) throw new ArgumentNullException("passSignature");
            if (vertexBufferBindings == null) throw new ArgumentNullException("vertexBufferBindings");

            var inputLayoutBindings = vertexBufferBindings.Select((x) => new InputLayoutBinding() { instanceFrequency = x.InstanceFrequency, vertexElements = x.VertexBuffer.VertexDeclaration.GetVertexElements() }).ToArray();

            InputLayout layout;
            if (!layouts.TryGetValue(inputLayoutBindings, out layout))
            {
                List<InputElement> inputElements = new List<InputElement>();
                int slot = 0;
                foreach (ANX.Framework.Graphics.VertexBufferBinding binding in vertexBufferBindings)
                {
                    foreach (VertexElement vertexElement in binding.VertexBuffer.VertexDeclaration.GetVertexElements())
                    {
                        inputElements.Add(CreateInputElementFromVertexElement(vertexElement, binding.InstanceFrequency, slot));
                    }
                    slot++;
                }

                // Layout from VertexShader input signature
                layout = CreateInputLayout(device, passSignature, inputElements.ToArray());

                layouts.Add(inputLayoutBindings, layout);
            }

            return layout;
        }

        private InputLayout CreateInputLayout(Device device, ShaderBytecode passSignature, InputElement[] inputElements)
        {
            var layout = new InputLayout(device, passSignature, inputElements);
#if DEBUG
            layout.DebugName = "InputLayout_" + layoutCount++;
#endif

            return layout;
        }

        private InputElement CreateInputElementFromVertexElement(VertexElement vertexElement, int instanceFrequency, int slot)
        {
            string elementName = DxFormatConverter.Translate(ref vertexElement);
            SharpDX.DXGI.Format elementFormat = DxFormatConverter.ConvertVertexElementFormat(vertexElement.VertexElementFormat);
            return new InputElement(elementName, vertexElement.UsageIndex, elementFormat, vertexElement.Offset, slot, instanceFrequency == 0 ? InputClassification.PerVertexData : InputClassification.PerInstanceData, instanceFrequency);
        }

        private InputElement CreateInputElementFromVertexElement(VertexElement vertexElement)
        {
            string elementName = DxFormatConverter.Translate(ref vertexElement);
            SharpDX.DXGI.Format elementFormat = DxFormatConverter.ConvertVertexElementFormat(vertexElement.VertexElementFormat);
            return new InputElement(elementName, vertexElement.UsageIndex, elementFormat, vertexElement.Offset, 0);
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposeManaged)
        {
            if (disposeManaged)
            {
                foreach (var layout in layouts.Values)
                {
                    layout.Dispose();
                }

                layouts.Clear();
            }
        }

        class InputLayoutBinding : IEquatable<InputLayoutBinding>
        {
            public int instanceFrequency = 0;
            public VertexElement[] vertexElements;

            public override int GetHashCode()
            {
                int hash = this.instanceFrequency;
                foreach (var element in this.vertexElements)
                    hash ^= element.GetHashCode();

                return hash;
            }

            public override bool Equals(object obj)
            {
                if (obj is InputLayoutBinding)
                    return this.Equals((InputLayoutBinding)obj);

                return false;
            }

            public bool Equals(InputLayoutBinding other)
            {
                return other.instanceFrequency == this.instanceFrequency && other.vertexElements.SequenceEqual(this.vertexElements);
            }
        }

        class InputLayoutBindingCompararer : IEqualityComparer<InputLayoutBinding[]>
        {
            public bool Equals(InputLayoutBinding[] x, InputLayoutBinding[] y)
            {
                if (x.Length == y.Length)
                {
                    for (int i = 0; i < x.Length; i++)
                        if (!x[i].Equals(y[i]))
                            return false;

                    return true;
                }

                return false;
            }

            public int GetHashCode(InputLayoutBinding[] obj)
            {
                int hash = 0;
                foreach (var binding in obj)
                    hash ^= binding.GetHashCode();

                return hash;
            }
        }
    }
}
