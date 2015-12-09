using System;
using System.Collections.Generic;
using System.Linq;
using ANX.Framework.NonXNA.InputSystem;
using ANX.Framework.NonXNA.Reflection;
using System.Collections.ObjectModel;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.NonXNA
{
    public class InputDeviceFactory
    {
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

        public WindowHandle WindowHandle
        {
            get;
            internal set;
        }

        public ILookup<string, IInputDeviceCreator> Providers
        {
            get { return deviceCreators.SelectMany((x) => x.Value).Select((x) => x.Value).ToLookup((x) => x.Provider); }
        }

        public string PrefferedProvider
        {
            get;
            set;
        }
        #endregion

        #region Constructor
        private InputDeviceFactory()
        {
            deviceCreators = new Dictionary<Type, Dictionary<string, IInputDeviceCreator>>();
        }
        #endregion

        #region AddCreator
        internal void AddCreator(Type deviceType, IInputDeviceCreator creator)
        {
            string creatorName = creator.Name.ToLowerInvariant();
            Type deviceInterface = TypeHelper.GetInterfacesFrom(deviceType)[0];

            if (!deviceCreators.ContainsKey(deviceInterface))
                deviceCreators.Add(deviceInterface, new Dictionary<string, IInputDeviceCreator>());
            else if (deviceCreators[deviceInterface].ContainsKey(creatorName))
                throw new ArgumentException("Duplicate " + deviceType.Name + " found. A " + deviceType.Name +
                    " with the name '" + creator.Name + "' was already registered.");

            deviceCreators[deviceInterface].Add(creatorName, creator);

            Logger.Info("Added InputDeviceCreator '{0}'. Registered creators: {1}.", creatorName,
                deviceCreators[deviceInterface].Count);
        }
        #endregion

        #region GetDefaultTouchPanel
        public ITouchPanel CreateDefaultTouchPanel()
        {
            ValidateWindowHandle("TouchPanel");
            var touchPanel = GetDefaultCreator<ITouchPanelCreator>().CreateDevice();
            touchPanel.WindowHandle = WindowHandle;

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
            ValidateWindowHandle("Mouse");
            var mouse = GetDefaultCreator<IMouseCreator>().CreateDevice();
            mouse.WindowHandle = WindowHandle;

            return mouse;
        }
        #endregion

        #region CreateDefaultKeyboard
        public IKeyboard CreateDefaultKeyboard()
        {
            ValidateWindowHandle("Keyboard");

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
                {
                    var creator = (T)creators.Values.FirstOrDefault((x) => x.Provider == PrefferedProvider);
                    if (creator != null)
                        return creator;
                    else
                        return (T)creators.Values.First();
                }
            }

            throw new ArgumentException("Unable to find a default creator for type " + creatorType);
        }
        #endregion

        #region ValidateWindowHandle
        private void ValidateWindowHandle(string deviceName)
        {
            if (!WindowHandle.IsValid)
                throw new InvalidOperationException(string.Format("Unable to create a {0} instance because the WindowHandle was not set.", deviceName));
        }
        #endregion
    }
}
