using System;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.NonXNA.InputSystem
{
	public interface IInputDeviceCreator
	{
		string Name { get; }
		int Priority { get; }
	}

	public interface IInputDeviceCreator<T> : IInputDeviceCreator
    {
		T CreateDevice();
    }
}
