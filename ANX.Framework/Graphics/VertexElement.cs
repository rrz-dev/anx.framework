using System;
using ANX.Framework.NonXNA.Development;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Graphics
{
	[PercentageComplete(95)]
    [TestState(TestStateAttribute.TestState.Untested)]
    public struct VertexElement
    {
        #region Private
        private int offset;
        private VertexElementFormat elementFormat;
        private VertexElementUsage elementUsage;
        private int usageIndex;
		#endregion

		#region Public
		public int Offset
		{
			get
			{
				return this.offset;
			}
			set
			{
				this.offset = value;
			}
		}

		public VertexElementFormat VertexElementFormat
		{
			get
			{
				return this.elementFormat;
			}
			set
			{
				this.elementFormat = value;
			}
		}

		public VertexElementUsage VertexElementUsage
		{
			get
			{
				return this.elementUsage;
			}
			set
			{
				this.elementUsage = value;
			}
		}

		public int UsageIndex
		{
			get
			{
				return this.usageIndex;
			}
			set
			{
				this.usageIndex = value;
			}
		}
		#endregion

        public VertexElement(int offset, VertexElementFormat elementFormat, VertexElementUsage elementUsage, int usageIndex)
        {
            this.offset = offset;
            this.elementFormat = elementFormat;
            this.elementUsage = elementUsage;
            this.usageIndex = usageIndex;
        }

        public override int GetHashCode()
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            return String.Format("{{Offset:{0} Format:{1} Usage:{2} UsageIndex:{3}}}", offset, elementFormat, elementUsage,
				usageIndex);
        }

        public override bool Equals(object obj)
        {
            return obj is VertexElement && this == (VertexElement)obj;
        }

	    public static bool operator ==(VertexElement lhs, VertexElement rhs)
        {
            return lhs.offset == rhs.offset && lhs.elementFormat == rhs.elementFormat && lhs.elementUsage == rhs.elementUsage &&
				lhs.usageIndex == rhs.usageIndex;
        }

        public static bool operator !=(VertexElement lhs, VertexElement rhs)
        {
            return lhs.offset != rhs.offset || lhs.elementFormat != rhs.elementFormat || lhs.elementUsage != rhs.elementUsage ||
				lhs.usageIndex == rhs.usageIndex;
        }
    }
}
