using System;
using ANX.Framework.NonXNA.Development;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Input.Touch
{
    [PercentageComplete(100)]
    [TestState(TestStateAttribute.TestState.Tested)]
	[Flags]
	public enum GestureType
	{
		None = 0,
		Tap = 1,
		DoubleTap = 2,
		Hold = 4,
		HorizontalDrag = 8,
		VerticalDrag = 16,
		FreeDrag = 32,
		Pinch = 64,
		Flick = 128,
		DragComplete = 256,
		PinchComplete = 512,
	}
}
