#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
using ANX.Framework;
using ANX.Framework.Graphics;
using ANX.Framework.Input;
using ANX.Framework.NonXNA;
using ANX.InputSystem.Recording;

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

namespace RecordingSample
{
    /// <summary>
    /// Sample, showing the use of the RecordingSystem (currently only the Mouse).
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Texture2D logo;
        KeyboardState oldState;

        RecordingMouse recMouse;
        RecordingKeyboard recKeyboard;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "SampleContent";
        }

        protected override void Initialize()
        {
            Window.Title = "Use Mouse to move arround, press r to record, p for playback and n for none";

            //this is quite ugly... could this be improved?
            recMouse = ((RecordingMouse)AddInSystemFactory.Instance.GetDefaultCreator<IInputSystemCreator>().Mouse);
            recMouse.Initialize(MouseRecordInfo.Position);
            recKeyboard = ((RecordingKeyboard)AddInSystemFactory.Instance.GetDefaultCreator<IInputSystemCreator>().Keyboard);
            recKeyboard.Initialize(Keys.Enter);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            logo = Content.Load<Texture2D>(@"Textures/ANX.Framework.Logo_459x121");
            oldState = Keyboard.GetState();
        }

        protected override void Update(GameTime gameTime)
        {
            KeyboardState newState = Keyboard.GetState();

            if (oldState.IsKeyUp(Keys.R) && newState.IsKeyDown(Keys.R))
            {
                recMouse.StartRecording();
                recKeyboard.StartRecording();
            }

            if (oldState.IsKeyUp(Keys.P) && newState.IsKeyDown(Keys.P))
            {
                if (recMouse.RecordingState == RecordingState.Recording)
                    recMouse.StopRecording();
                recMouse.StartPlayback();

                if (recKeyboard.RecordingState == RecordingState.Recording)
                    recKeyboard.StopRecording();
                recKeyboard.StartPlayback();
            }

            if (oldState.IsKeyUp(Keys.N) && newState.IsKeyDown(Keys.N))
            {
                if (recMouse.RecordingState == RecordingState.Recording)
                    recMouse.StopRecording();
                recMouse.StopPlayback();

                if (recKeyboard.RecordingState == RecordingState.Recording)
                    recKeyboard.StopRecording();
                recKeyboard.StopPlayback();
            }

            oldState = newState;

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();
            if(Keyboard.GetState().IsKeyDown(Keys.Enter))
                spriteBatch.Draw(logo, Vector2.Zero, Color.White);
            spriteBatch.End();
            spriteBatch.Begin();
            spriteBatch.Draw(logo, new Rectangle(Mouse.GetState().X, Mouse.GetState().Y, 115, 30), Color.White);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
