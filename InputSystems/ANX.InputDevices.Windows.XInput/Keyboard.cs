using System;
using ANX.Framework;
using ANX.Framework.Input;
using ANX.Framework.NonXNA;
using DInput = SharpDX.DirectInput;
using ANX.Framework.NonXNA.Development;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.InputDevices.Windows.XInput
{
	[PercentageComplete(100)]
	[TestState(TestStateAttribute.TestState.InProgress)]
	[Developer("AstrorEnales")]
	public class Keyboard : IKeyboard
	{
		#region Private
		private DInput.DirectInput directInput;
		private DInput.Keyboard nativeKeyboard;
		private DInput.KeyboardState nativeState;
		private IntPtr windowHandle;
		private KeyboardState emptyState;
		#endregion

		public IntPtr WindowHandle
		{
			get { return windowHandle; }
			set
			{
				if (windowHandle != value)
				{
					windowHandle = value;
					nativeKeyboard.Unacquire();
					nativeKeyboard.SetCooperativeLevel(WindowHandle,
						DInput.CooperativeLevel.NonExclusive | DInput.CooperativeLevel.Background);
					nativeKeyboard.Acquire();
				}
			}
		}

		public Keyboard()
		{
			emptyState = new KeyboardState(new Keys[0]);

			nativeState = new DInput.KeyboardState();
			directInput = new DInput.DirectInput();
			nativeKeyboard = new DInput.Keyboard(directInput);
			nativeKeyboard.Acquire();
		}

		/// <summary>
		/// Only available on XBox, behaviour regarding MSDN: empty keystate
		/// </summary>
		public KeyboardState GetState(PlayerIndex playerIndex)
		{
			return emptyState;
		}

		public KeyboardState GetState()
		{
			if (WindowHandle == IntPtr.Zero)
				return emptyState;

			nativeKeyboard.GetCurrentState(ref nativeState);

			int keyCount = nativeState.PressedKeys.Count;
			Keys[] keys = new Keys[keyCount];
			for (int i = 0; i < keyCount; i++)
				keys[i] = FormatConverter.Translate(nativeState.PressedKeys[i]);

			return new KeyboardState(keys);
		}

		public void Dispose()
		{
			if (nativeKeyboard != null)
			{
				nativeKeyboard.Unacquire();
				nativeKeyboard.Dispose();
				nativeKeyboard = null;
			}

			if (directInput != null)
			{
				directInput.Dispose();
				directInput = null;
			}
		}
	}
}
