#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
using ANX.Framework;
using ANX.Framework.Graphics;
using ANX.Framework.Input;

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

namespace TextRendering
{
    public class Game1 : ANX.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        SpriteFont debugFont;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreparingDeviceSettings += new EventHandler<PreparingDeviceSettingsEventArgs>(graphics_PreparingDeviceSettings);
            Content.RootDirectory = "SampleContent";
        }

        void graphics_PreparingDeviceSettings(object sender, PreparingDeviceSettingsEventArgs e)
        {
            e.GraphicsDeviceInformation.PresentationParameters.BackBufferWidth = 1280;
            e.GraphicsDeviceInformation.PresentationParameters.BackBufferHeight = 720;
        }

        protected override void Initialize()
        {
            //GraphicsDevice.PresentationParameters.BackBufferWidth = 1280;
            //GraphicsDevice.PresentationParameters.BackBufferHeight = 720;
            //GraphicsDevice.Reset();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            this.debugFont = Content.Load<SpriteFont>(@"Fonts/Debug");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin(SpriteSortMode.FrontToBack, null);
            spriteBatch.DrawString(this.debugFont, "Hello World!", new Vector2(100, 100), Color.White);

            spriteBatch.DrawString(this.debugFont, "This screen is powered by the ANX.Framework!\r\nsecond line", new Vector2(100, 100 + this.debugFont.LineSpacing), Color.Black, 0.0f, new Vector2(1, -1), Vector2.One, SpriteEffects.None, 0.0f);
            spriteBatch.DrawString(this.debugFont, "This screen is powered by the ANX.Framework!\r\nsecond line", new Vector2(100, 100 + this.debugFont.LineSpacing), Color.Red, 0.0f, Vector2.Zero, Vector2.One, SpriteEffects.None, 1.0f);
            spriteBatch.DrawString(this.debugFont, "Mouse X: " + Mouse.GetState().X, new Vector2(100, 100 + this.debugFont.LineSpacing * 3), Color.DarkOrange);
            spriteBatch.DrawString(this.debugFont, "Mouse Y: " + Mouse.GetState().Y, new Vector2(100, 100 + this.debugFont.LineSpacing * 4), Color.DarkOrange);
            spriteBatch.DrawString(this.debugFont, "rotated Text", new Vector2(100, 150), Color.Green, MathHelper.ToRadians(90), Vector2.Zero, 1.0f, SpriteEffects.None, 1.0f);

            GamePadState state = GamePad.GetState(PlayerIndex.One);
            spriteBatch.DrawString(this.debugFont, String.Format("GamePad Left: {0} Right: {1}", state.ThumbSticks.Left, state.ThumbSticks.Right) , new Vector2(100, 100 + this.debugFont.LineSpacing * 5), Color.Red, 0.0f, Vector2.Zero, Vector2.One, SpriteEffects.None, 1.0f);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
