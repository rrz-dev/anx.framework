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

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace RecordingSample
{
    /// <summary>
    /// Sample, showing the use of the RecordingSystem.
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
            Window.Title = "Move the Mouse or press Enter. press R to record, P for playback and N for none";
            
            recMouse = RecordingHelper.GetMouse(); //((RecordingMouse)AddInSystemFactory.Instance.GetDefaultCreator<IInputSystemCreator>().Mouse);
            recMouse.Initialize(MouseRecordInfo.Position);
            recKeyboard = RecordingHelper.GetKeyboard();
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
            if (Keyboard.GetState().IsKeyDown(Keys.Enter)) //Keyboard
                spriteBatch.Draw(logo, Vector2.Zero, Color.White);

            spriteBatch.Draw(logo, new Rectangle(Mouse.GetState().X, Mouse.GetState().Y, 115, 30), Color.White); //Mouse
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
