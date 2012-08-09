using System;
using System.Collections.Generic;
using System.Linq;
using ANX.Framework.NonXNA.InputSystem;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.NonXNA
{
	public class InputDeviceFactory
	{
		#region Constants
		internal static readonly Type[] ValidInputDeviceCreators =
		{
			typeof(IGamePadCreator),
			typeof(IKeyboardCreator),
			typeof(IMouseCreator),
#if XNAEXT
			typeof(IMotionSensingDeviceCreator),
#endif
		};
		#endregion

		#region Private
		private static InputDeviceFactory instance;

		private Dictionary<Type,
			Dictionary<string, IInputDeviceCreator>> deviceCreators;

		private IntPtr windowHandle;

		#endregion

		#region Public
		#region Instance
		public static InputDeviceFactory Instance
		{
			get
			{
				if (instance == null)
				{
					instance = new InputDeviceFactory();
					Logger.Info("Created InputDeviceFactory instance");
				}

				return instance;
			}
		}
		#endregion

		#region WindowHandle
		public IntPtr WindowHandle
		{
			get
			{
				return this.windowHandle;
			}
			internal set
			{
				this.windowHandle = value;
			}
		}
		#endregion
		#endregion

		#region Constructor
		private InputDeviceFactory()
		{
			deviceCreators =
				new Dictionary<Type, Dictionary<string, IInputDeviceCreator>>();

			deviceCreators.Add(typeof(IGamePadCreator),
				new Dictionary<string, IInputDeviceCreator>());
			deviceCreators.Add(typeof(IKeyboardCreator),
				new Dictionary<string, IInputDeviceCreator>());
			deviceCreators.Add(typeof(IMouseCreator),
				new Dictionary<string, IInputDeviceCreator>());
#if XNAEXT
			deviceCreators.Add(typeof(IMotionSensingDeviceCreator),
				new Dictionary<string, IInputDeviceCreator>());
#endif
		}
		#endregion

		#region AddCreator
		public void AddCreator(Type deviceType, IInputDeviceCreator creator)
		{
			string creatorName = creator.Name.ToLowerInvariant();

			Type deviceInterface = TypeHelper.GetInterfacesFrom(deviceType)[0];
			if (deviceCreators[deviceInterface].ContainsKey(creatorName))
			{
				throw new Exception("Duplicate " + deviceType.Name +
					" found. A GamePadCreator with the name '" + creator.Name +
					"' was already registered.");
			}

			deviceCreators[deviceInterface].Add(creatorName, creator);

			Logger.Info("Added GamePadCreator '{0}'. Total count of registered " +
				"GamePadCreators is now {1}.",
				creatorName, deviceCreators[deviceInterface].Count);
		}
		#endregion

		#region GetDefaultGamePad
		public IGamePad GetDefaultGamePad()
		{
			//TODO: this is a very basic implementation only which needs some more work

			if (this.deviceCreators[typeof(IGamePadCreator)].Count > 0)
			{
				IInputDeviceCreator creator = deviceCreators[
					typeof(IGamePadCreator)].Values.First<IInputDeviceCreator>();
				return ((IGamePadCreator)creator).CreateGamePadInstance();
			}

			throw new Exception("Unable to create instance of GamePad because no GamePadCreator was registered.");
		}
		#endregion

		#region GetDefaultMouse
		public IMouse GetDefaultMouse()
		{
			//TODO: this is a very basic implementation only which needs some more work

			if (this.WindowHandle == null ||
					this.WindowHandle == IntPtr.Zero)
			{
				throw new Exception("Unable to create a mouse instance because the WindowHandle was not set.");
			}

			if (this.deviceCreators[typeof(IMouseCreator)].Count > 0)
			{
				IInputDeviceCreator creator = deviceCreators[
					typeof(IMouseCreator)].Values.First<IInputDeviceCreator>();
				IMouse mouse = ((IMouseCreator)creator).CreateMouseInstance();
				mouse.WindowHandle = this.windowHandle;
				return mouse;
			}

			throw new Exception("Unable to create instance of Mouse because no MouseCreator was registered.");
		}
		#endregion

		#region GetDefaultKeyboard
		public IKeyboard GetDefaultKeyboard()
		{
			//TODO: this is a very basic implementation only which needs some more work

			if (this.WindowHandle == null ||
					this.WindowHandle == IntPtr.Zero)
			{
				throw new Exception("Unable to create a keyboard instance because the WindowHandle was not set.");
			}

			if (this.deviceCreators[typeof(IKeyboardCreator)].Count > 0)
			{
				IInputDeviceCreator creator = deviceCreators[
					typeof(IKeyboardCreator)].Values.First<IInputDeviceCreator>();
				IKeyboard keyboard =
					((IKeyboardCreator)creator).CreateKeyboardInstance();
				keyboard.WindowHandle = this.windowHandle;
				return keyboard;
			}

			throw new Exception("Unable to create instance of Keyboard because no KeyboardCreator was registered.");
		}
		#endregion

		#region GetDefaultMotionSensingDevice
#if XNAEXT
		public IMotionSensingDevice GetDefaultMotionSensingDevice()
		{
			//TODO: this is a very basic implementation only which needs some more work

			if (this.deviceCreators[typeof(IMotionSensingDeviceCreator)].Count > 0)
			{
				IInputDeviceCreator creator = deviceCreators[
					typeof(IMotionSensingDeviceCreator)].Values
					.First<IInputDeviceCreator>();
				return ((IMotionSensingDeviceCreator)creator)
					.CreateMotionSensingDeviceInstance();
			}

			throw new Exception("Unable to create instance of MotionSensingDevice because no MotionSensingDeviceCreator was registered.");
		}
#endif
		#endregion
	}
}
