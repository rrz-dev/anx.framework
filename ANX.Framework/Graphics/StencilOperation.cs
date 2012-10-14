using ANX.Framework.NonXNA.Development;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Graphics
{
    [PercentageComplete(100)]
    [Developer("AstrorEnales")]
    [TestState(TestStateAttribute.TestState.Tested)]
	public enum StencilOperation
	{
		Keep,
		Zero,
		Replace,
		Increment,
		Decrement,
		IncrementSaturation,
		DecrementSaturation,
		Invert
	}
}
