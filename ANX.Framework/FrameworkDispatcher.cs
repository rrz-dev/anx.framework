using System;
using ANX.Framework.NonXNA.Development;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework
{
	[PercentageComplete(100)]
	[TestState(TestStateAttribute.TestState.Tested)]
	[Developer("AstrorEnales")]
	public static class FrameworkDispatcher
	{
		internal static event Action OnUpdate;

		public static void Update()
		{
			if (OnUpdate != null)
				OnUpdate();
		}
	}
}
