#region Using Statements
using System;
using System.IO;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using ANX.Framework.Input;
using NLog;
using System.Collections;
using System.Resources;
using ANX.Framework.NonXNA.InputSystem;

#endregion // Using Statements

#region License

//
// This file is part of the ANX.Framework created by the "ANX.Framework developer group".
//
// This file is released under the Ms-PL license.
//
//
//
// Microsoft Public License (Ms-PL)
//
// This license governs use of the accompanying software. If you use the software, you accept this license. 
// If you do not accept the license, do not use the software.
//
// 1.Definitions
//   The terms "reproduce," "reproduction," "derivative works," and "distribution" have the same meaning 
//   here as under U.S. copyright law.
//   A "contribution" is the original software, or any additions or changes to the software.
//   A "contributor" is any person that distributes its contribution under this license.
//   "Licensed patents" are a contributor's patent claims that read directly on its contribution.
//
// 2.Grant of Rights
//   (A) Copyright Grant- Subject to the terms of this license, including the license conditions and limitations 
//       in section 3, each contributor grants you a non-exclusive, worldwide, royalty-free copyright license to 
//       reproduce its contribution, prepare derivative works of its contribution, and distribute its contribution
//       or any derivative works that you create.
//   (B) Patent Grant- Subject to the terms of this license, including the license conditions and limitations in 
//       section 3, each contributor grants you a non-exclusive, worldwide, royalty-free license under its licensed
//       patents to make, have made, use, sell, offer for sale, import, and/or otherwise dispose of its contribution 
//       in the software or derivative works of the contribution in the software.
//
// 3.Conditions and Limitations
//   (A) No Trademark License- This license does not grant you rights to use any contributors' name, logo, or trademarks.
//   (B) If you bring a patent claim against any contributor over patents that you claim are infringed by the software, your 
//       patent license from such contributor to the software ends automatically.
//   (C) If you distribute any portion of the software, you must retain all copyright, patent, trademark, and attribution 
//       notices that are present in the software.
//   (D) If you distribute any portion of the software in source code form, you may do so only under this license by including
//       a complete copy of this license with your distribution. If you distribute any portion of the software in compiled or 
//       object code form, you may only do so under a license that complies with this license.
//   (E) The software is licensed "as-is." You bear the risk of using it. The contributors give no express warranties, guarantees,
//       or conditions. You may have additional consumer rights under your local laws which this license cannot change. To the
//       extent permitted under your local laws, the contributors exclude the implied warranties of merchantability, fitness for a 
//       particular purpose and non-infringement.

#endregion // License

namespace ANX.Framework.NonXNA
{
    public class InputDeviceFactory
    {
        #region Private Members
        private static InputDeviceFactory instance;

        private Dictionary<string, IGamePadCreator> gamePadCreators;
        private Dictionary<string, IKeyboardCreator> keyboardCreators;
        private Dictionary<string, IMouseCreator> mouseCreators;
#if XNAEXT
        private Dictionary<string, IMotionSensingDeviceCreator> motionSensingDeviceCreators;
#endif 

        private static Logger logger = LogManager.GetCurrentClassLogger();

        #endregion // Private Members

        public static InputDeviceFactory Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new InputDeviceFactory();
                    logger.Debug("Created InputDeviceFactory instance");
                }

                return instance;
            }
        }

        private InputDeviceFactory()
        {
            this.gamePadCreators = new Dictionary<string, IGamePadCreator>();
            this.keyboardCreators = new Dictionary<string, IKeyboardCreator>();
            this.mouseCreators = new Dictionary<string, IMouseCreator>();
#if XNAEXT
            this.motionSensingDeviceCreators = new Dictionary<string, IMotionSensingDeviceCreator>();
#endif
        }

        #region AddCreator
        public void AddCreator(IGamePadCreator creator)
        {
            string creatorName = creator.Name.ToLowerInvariant();

            if (gamePadCreators.ContainsKey(creatorName))
            {
                throw new Exception("Duplicate GamePadCreator found. A GamePadCreator with the name '" + creator.Name + "' was already registered.");
            }

            gamePadCreators.Add(creatorName, creator);

            logger.Debug("Added GamePadCreator '{0}'. Total count of registered GamePadCreators is now {1}.", creatorName, gamePadCreators.Count);
        }

        public void AddCreator(IKeyboardCreator creator)
        {
            string creatorName = creator.Name.ToLowerInvariant();

            if (keyboardCreators.ContainsKey(creatorName))
            {
                throw new Exception("Duplicate KeyboardCreator found. A KeyboardCreator with the name '" + creator.Name + "' was already registered.");
            }

            keyboardCreators.Add(creatorName, creator);

            logger.Debug("Added KeyboardCreator '{0}'. Total count of registered KeyboardCreators is now {1}.", creatorName, keyboardCreators.Count);
        }

        public void AddCreator(IMouseCreator creator)
        {
            string creatorName = creator.Name.ToLowerInvariant();

            if (mouseCreators.ContainsKey(creatorName))
            {
                throw new Exception("Duplicate MouseCreator found. A MouseCreator with the name '" + creator.Name + "' was already registered.");
            }

            mouseCreators.Add(creatorName, creator);

            logger.Debug("Added MouseCreator '{0}'. Total count of registered MouseCreators is now {1}.", creatorName, mouseCreators.Count);
        }

#if XNAEXT
        public void AddCreator(IMotionSensingDeviceCreator creator)
        {
            string creatorName = creator.Name.ToLowerInvariant();

            if (motionSensingDeviceCreators.ContainsKey(creatorName))
            {
                throw new Exception("Duplicate MotionSensingDeviceCreator found. A MotionSensingDeviceCreator with the name '" + creator.Name + "' was already registered.");
            }

            motionSensingDeviceCreators.Add(creatorName, creator);

            logger.Debug("Added MotionSensingDeviceCreator '{0}'. Total count of registered MotionSensingDeviceCreators is now {1}.", creatorName, motionSensingDeviceCreators.Count);
        }
#endif

        #endregion // AddCreator

        public IGamePad GetDefaultGamePad()
        {
            //TODO: this is a very basic implementation only which needs some more work

            if (this.gamePadCreators.Count > 0)
            {
                return this.gamePadCreators.Values.First<IGamePadCreator>().CreateGamePadInstance();
            }

            throw new Exception("Unable to create instance of GamePad because no GamePadCreator was registered.");
        }

        public IMouse GetDefaultMouse()
        {
            //TODO: this is a very basic implementation only which needs some more work

            if (this.mouseCreators.Count > 0)
            {
                return this.mouseCreators.Values.First<IMouseCreator>().CreateMouseInstance();
            }

            throw new Exception("Unable to create instance of Mouse because no MouseCreator was registered.");
        }

        public IKeyboard GetDefaultKeyboard()
        {
            //TODO: this is a very basic implementation only which needs some more work

            if (this.keyboardCreators.Count > 0)
            {
                return this.keyboardCreators.Values.First<IKeyboardCreator>().CreateKeyboardInstance();
            }

            throw new Exception("Unable to create instance of Keyboard because no KeyboardCreator was registered.");
        }

#if XNAEXT
        public IMotionSensingDevice GetDefaultMotionSensingDevice()
        {
            //TODO: this is a very basic implementation only which needs some more work

            if (this.motionSensingDeviceCreators.Count > 0)
            {
                return this.motionSensingDeviceCreators.Values.First<IMotionSensingDeviceCreator>().CreateMotionSensingDeviceInstance();
            }

            throw new Exception("Unable to create instance of MotionSensingDevice because no MotionSensingDeviceCreator was registered.");
        }
#endif

    }
}
