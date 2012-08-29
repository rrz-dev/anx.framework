// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

using ANX.Framework.NonXNA.Development;
namespace ANX.Framework
{
	[PercentageComplete(100)]
    [TestState(TestStateAttribute.TestState.Tested)]
    [Developer("Glatzemann")]
	public interface IGameComponent
	{
		void Initialize();
	}
}
