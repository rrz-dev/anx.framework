using System;
using System.Collections.Generic;
using System.Linq;
using ANX.Framework.NonXNA.InputSystem;
using ANX.Framework.NonXNA.Reflection;

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
			deviceCreators = new Dictionary<Type, Dictionary<string, IInputDeviceCreator>>();
			RegisterCreatorType<IGamePadCreator>();
			RegisterCreatorType<IKeyboardCreator>();
			RegisterCreatorType<IMouseCreator>();
			RegisterCreatorType<ITouchPanelCreator>();
#if XNAEXT
			RegisterCreatorType<IMotionSensingDeviceCreator>();
#endif
		}
		#endregion

		#region RegisterCreatorType
		private void RegisterCreatorType<T>()
		{
			deviceCreators.Add(typeof(T), new Dictionary<string, IInputDeviceCreator>());
		}
		#endregion

		#region AddCreator
		public void AddCreator(Type deviceType, IInputDeviceCreator creator)
		{
			string creatorName = creator.Name.ToLowerInvariant();

			Type deviceInterface = TypeHelper.GetInterfacesFrom(deviceType)[0];
			if (deviceCreators[deviceInterface].ContainsKey(creatorName))
			{
				throw new Exception("Duplicate " + deviceType.Name + " found. A " + deviceType.Name +
					" with the name '" + creator.Name + "' was already registered.");
			}

			deviceCreators[deviceInterface].Add(creatorName, creator);

			Logger.Info("Added GamePadCreator '{0}'. Total count of registered " +
				"GamePadCreators is now {1}.",
				creatorName, deviceCreators[deviceInterface].Count);
		}
		#endregion

		#region GetDefaultTouchPanel
		public ITouchPanel GetDefaultTouchPanel()
		{
			ValidateWindowHandle();

			var touchPanel = GetDefaultCreator<ITouchPanelCreator>().CreateTouchPanelInstance();
			touchPanel.WindowHandle = this.windowHandle;
			return touchPanel;
		}
		#endregion

		#region GetDefaultGamePad
		public IGamePad GetDefaultGamePad()
		{
			return GetDefaultCreator<IGamePadCreator>().CreateGamePadInstance();
		}
		#endregion

		#region GetDefaultMouse
		public IMouse GetDefaultMouse()
		{
			ValidateWindowHandle();

			var mouse = GetDefaultCreator<IMouseCreator>().CreateMouseInstance();
			mouse.WindowHandle = this.windowHandle;
			return mouse;
		}
		#endregion

		#region GetDefaultKeyboard
		public IKeyboard GetDefaultKeyboard()
		{
			ValidateWindowHandle();

			var keyboard = GetDefaultCreator<IKeyboardCreator>().CreateKeyboardInstance();
			keyboard.WindowHandle = this.windowHandle;
			return keyboard;
		}
		#endregion

		#region GetDefaultMotionSensingDevice
#if XNAEXT
		public IMotionSensingDevice GetDefaultMotionSensingDevice()
		{
			return GetDefaultCreator<IMotionSensingDeviceCreator>().CreateMotionSensingDeviceInstance();
		}
#endif
		#endregion

		#region GetDefaultCreator
		private T GetDefaultCreator<T>() where T : IInputDeviceCreator
		{
			Type creatorType = typeof(T);
			if (deviceCreators.ContainsKey(creatorType))
			{
				var creators = deviceCreators[creatorType];
				if (creators.Count > 0)
				{
					return (T)creators.Values.First<IInputDeviceCreator>();
				}
			}

			throw new Exception("Unable to find a default creator for type " + creatorType);
		}
		#endregion

		#region ValidateWindowHandle
		private void ValidateWindowHandle()
		{
			if (windowHandle == IntPtr.Zero)
				throw new Exception("Unable to create a mouse instance because the WindowHandle was not set.");
		}
		#endregion
	}
}
