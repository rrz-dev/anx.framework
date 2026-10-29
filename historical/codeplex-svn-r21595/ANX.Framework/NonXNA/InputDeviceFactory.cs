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
			typeof(ITouchPanelCreator),
#if XNAEXT
			typeof(IMotionSensingDeviceCreator),
#endif
		};
		#endregion

		#region Private
		private static InputDeviceFactory instance;
		private Dictionary<Type, Dictionary<string, IInputDeviceCreator>> deviceCreators;
		#endregion

		#region Public
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

		public IntPtr WindowHandle
		{
			get;
			internal set;
		}
		#endregion

		#region Constructor
		private InputDeviceFactory()
		{
			deviceCreators = new Dictionary<Type, Dictionary<string, IInputDeviceCreator>>();
			foreach (Type creatorType in ValidInputDeviceCreators)
				deviceCreators.Add(creatorType, new Dictionary<string, IInputDeviceCreator>());
		}
		#endregion

		#region AddCreator
		internal void AddCreator(Type deviceType, IInputDeviceCreator creator)
		{
			string creatorName = creator.Name.ToLowerInvariant();
			Type deviceInterface = TypeHelper.GetInterfacesFrom(deviceType)[0];

			if (deviceCreators[deviceInterface].ContainsKey(creatorName))
				throw new Exception("Duplicate " + deviceType.Name + " found. A " + deviceType.Name +
					" with the name '" + creator.Name + "' was already registered.");

			deviceCreators[deviceInterface].Add(creatorName, creator);

			Logger.Info("Added InputDeviceCreator '{0}'. Registered creators: {1}.", creatorName,
				deviceCreators[deviceInterface].Count);
		}
		#endregion

		#region GetDefaultTouchPanel
		public ITouchPanel GetDefaultTouchPanel()
		{
			ValidateWindowHandle();
			var touchPanel = GetDefaultCreator<ITouchPanelCreator>().CreateDevice();
#if !WINDOWSMETRO
            touchPanel.WindowHandle = WindowHandle;
#endif
			return touchPanel;
		}
		#endregion

		#region CreateDefaultGamePad
		public IGamePad CreateDefaultGamePad()
		{
			return GetDefaultCreator<IGamePadCreator>().CreateDevice();
		}
		#endregion

		#region CreateDefaultMouse
		public IMouse CreateDefaultMouse()
		{
            ValidateWindowHandle();
            var mouse = GetDefaultCreator<IMouseCreator>().CreateDevice();
#if !WINDOWSMETRO
            mouse.WindowHandle = WindowHandle;
#endif
			return mouse;
		}
		#endregion

		#region CreateDefaultKeyboard
		public IKeyboard CreateDefaultKeyboard()
		{
			ValidateWindowHandle();

			var keyboard = GetDefaultCreator<IKeyboardCreator>().CreateDevice();
			keyboard.WindowHandle = WindowHandle;
			return keyboard;
		}
		#endregion

		#region CreateDefaultMotionSensingDevice
#if XNAEXT
		public IMotionSensingDevice CreateDefaultMotionSensingDevice()
		{
			return GetDefaultCreator<IMotionSensingDeviceCreator>().CreateDevice();
		}
#endif
		#endregion

		#region GetDefaultCreator
		private T GetDefaultCreator<T>()
		{
			Type creatorType = typeof(T);
			if (deviceCreators.ContainsKey(creatorType))
			{
				var creators = deviceCreators[creatorType];
				if (creators.Count > 0)
					return (T)creators.Values.First();
			}

			throw new Exception("Unable to find a default creator for type " + creatorType);
		}
		#endregion

		#region ValidateWindowHandle
		private void ValidateWindowHandle()
		{
#if !WINDOWSMETRO
			if (WindowHandle == IntPtr.Zero)
                throw new Exception("Unable to create a mouse instance because the WindowHandle was not set.");
#endif
		}
		#endregion
	}
}
