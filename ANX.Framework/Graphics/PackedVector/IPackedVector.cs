// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Graphics.PackedVector
{
	[ANX.Framework.NonXNA.Development.PercentageComplete(100)]
	public interface IPackedVector
	{
		void PackFromVector4(Vector4 vector);

		Vector4 ToVector4();
	}

	[ANX.Framework.NonXNA.Development.PercentageComplete(100)]
	public interface IPackedVector<TPacked> : IPackedVector
	{
		TPacked PackedValue
		{
			get;
			set;
		}
	}
}
