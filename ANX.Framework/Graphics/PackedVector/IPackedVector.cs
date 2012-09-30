// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

using ANX.Framework.NonXNA.Development;
namespace ANX.Framework.Graphics.PackedVector
{
    [PercentageComplete(100)]
    [Developer("Glatzemann")]
    [TestState(TestStateAttribute.TestState.Tested)]
	public interface IPackedVector
	{
		void PackFromVector4(Vector4 vector);

		Vector4 ToVector4();
	}

    [PercentageComplete(100)]
    [Developer("Glatzemann")]
    [TestState(TestStateAttribute.TestState.Tested)]
	public interface IPackedVector<TPacked> : IPackedVector
	{
		TPacked PackedValue
		{
			get;
			set;
		}
	}
}
