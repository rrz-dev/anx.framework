using System;
using ANX.Framework.Audio;
using ANX.Framework.GamerServices;
using ANX.Framework.Graphics;
using ANX.Framework.Input;
using ANX.Framework.Media;
using ANX.Framework.Net;
using NUnit.Framework;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.TestCenter.Strukturen
{
    class EnumValidationTest
    {
        private static readonly object[] EnumPairs =
        {
            new object[] { typeof(Microsoft.Xna.Framework.ContainmentType), typeof(ContainmentType) },
            new object[] { typeof(Microsoft.Xna.Framework.CurveContinuity), typeof(CurveContinuity) },
            new object[] { typeof(Microsoft.Xna.Framework.CurveLoopType), typeof(CurveLoopType) },
            new object[] { typeof(Microsoft.Xna.Framework.CurveTangent), typeof(CurveTangent) },
            new object[] { typeof(Microsoft.Xna.Framework.DisplayOrientation), typeof(DisplayOrientation) },
            new object[] { typeof(Microsoft.Xna.Framework.PlaneIntersectionType), typeof(PlaneIntersectionType) },
            new object[] { typeof(Microsoft.Xna.Framework.PlayerIndex), typeof(PlayerIndex) },
            
            new object[] { typeof(Microsoft.Xna.Framework.Audio.AudioChannels), typeof(AudioChannels) },
            new object[] { typeof(Microsoft.Xna.Framework.Audio.MicrophoneState), typeof(MicrophoneState) },
            new object[] { typeof(Microsoft.Xna.Framework.Audio.SoundState), typeof(SoundState) },
            
            new object[] { typeof(Microsoft.Xna.Framework.Graphics.Blend), typeof(Blend) },
            new object[] { typeof(Microsoft.Xna.Framework.Graphics.BlendFunction), typeof(BlendFunction) },
            new object[] { typeof(Microsoft.Xna.Framework.Graphics.BufferUsage), typeof(BufferUsage) },
            new object[] { typeof(Microsoft.Xna.Framework.Graphics.ClearOptions), typeof(ClearOptions) },
            new object[] { typeof(Microsoft.Xna.Framework.Graphics.ColorWriteChannels), typeof(ColorWriteChannels) },
            new object[] { typeof(Microsoft.Xna.Framework.Graphics.CompareFunction), typeof(CompareFunction) },
            new object[] { typeof(Microsoft.Xna.Framework.Graphics.CubeMapFace), typeof(CubeMapFace) },
            new object[] { typeof(Microsoft.Xna.Framework.Graphics.CullMode), typeof(CullMode) },
            new object[] { typeof(Microsoft.Xna.Framework.Graphics.DepthFormat), typeof(DepthFormat) },
            new object[] { typeof(Microsoft.Xna.Framework.Graphics.EffectParameterClass), typeof(EffectParameterClass) },
            new object[] { typeof(Microsoft.Xna.Framework.Graphics.EffectParameterType), typeof(EffectParameterType) },
            new object[] { typeof(Microsoft.Xna.Framework.Graphics.FillMode), typeof(FillMode) },
            new object[] { typeof(Microsoft.Xna.Framework.Graphics.GraphicsDeviceStatus), typeof(GraphicsDeviceStatus) },
            new object[] { typeof(Microsoft.Xna.Framework.Graphics.GraphicsProfile), typeof(GraphicsProfile) },
            new object[] { typeof(Microsoft.Xna.Framework.Graphics.IndexElementSize), typeof(IndexElementSize) },
            new object[] { typeof(Microsoft.Xna.Framework.Graphics.PresentInterval), typeof(PresentInterval) },
            new object[] { typeof(Microsoft.Xna.Framework.Graphics.PrimitiveType), typeof(PrimitiveType) },
            new object[] { typeof(Microsoft.Xna.Framework.Graphics.RenderTargetUsage), typeof(RenderTargetUsage) },
            new object[] { typeof(Microsoft.Xna.Framework.Graphics.SetDataOptions), typeof(SetDataOptions) },
            new object[] { typeof(Microsoft.Xna.Framework.Graphics.SpriteEffects), typeof(SpriteEffects) },
            new object[] { typeof(Microsoft.Xna.Framework.Graphics.SpriteSortMode), typeof(SpriteSortMode) },
            new object[] { typeof(Microsoft.Xna.Framework.Graphics.StencilOperation), typeof(StencilOperation) },
            new object[] { typeof(Microsoft.Xna.Framework.Graphics.SurfaceFormat), typeof(SurfaceFormat) },
            new object[] { typeof(Microsoft.Xna.Framework.Graphics.TextureAddressMode), typeof(TextureAddressMode) },
            new object[] { typeof(Microsoft.Xna.Framework.Graphics.TextureFilter), typeof(TextureFilter) },
            new object[] { typeof(Microsoft.Xna.Framework.Graphics.VertexElementFormat), typeof(VertexElementFormat) },
            new object[] { typeof(Microsoft.Xna.Framework.Graphics.VertexElementUsage), typeof(VertexElementUsage) },
            
            new object[] { typeof(Microsoft.Xna.Framework.Input.ButtonState), typeof(ButtonState) },
            new object[] { typeof(Microsoft.Xna.Framework.Input.Buttons), typeof(Buttons) },
            new object[] { typeof(Microsoft.Xna.Framework.Input.GamePadType), typeof(GamePadType) },
            new object[] { typeof(Microsoft.Xna.Framework.Input.KeyState), typeof(KeyState) },
            new object[] { typeof(Microsoft.Xna.Framework.Input.Keys), typeof(Keys) },
            new object[] { typeof(Microsoft.Xna.Framework.Media.MediaSourceType), typeof(MediaSourceType) },
            new object[] { typeof(Microsoft.Xna.Framework.Media.MediaState), typeof(MediaState) },
            
            new object[] { typeof(Microsoft.Xna.Framework.Net.NetworkSessionEndReason), typeof(NetworkSessionEndReason) },
            new object[] { typeof(Microsoft.Xna.Framework.Net.NetworkSessionJoinError), typeof(NetworkSessionJoinError) },
            new object[] { typeof(Microsoft.Xna.Framework.Net.NetworkSessionState), typeof(NetworkSessionState) },
            new object[] { typeof(Microsoft.Xna.Framework.Net.NetworkSessionType), typeof(NetworkSessionType) },
            new object[] { typeof(Microsoft.Xna.Framework.Net.SendDataOptions), typeof(SendDataOptions) },
            
            new object[] { typeof(Microsoft.Xna.Framework.GamerServices.ControllerSensitivity), typeof(ControllerSensitivity) },
            new object[] { typeof(Microsoft.Xna.Framework.GamerServices.GameDifficulty), typeof(GameDifficulty) },
            new object[] { typeof(Microsoft.Xna.Framework.GamerServices.GamerPresenceMode), typeof(GamerPresenceMode) },
            new object[] { typeof(Microsoft.Xna.Framework.GamerServices.GamerPrivilegeSetting), typeof(GamerPrivilegeSetting) },
            new object[] { typeof(Microsoft.Xna.Framework.GamerServices.GamerZone), typeof(GamerZone) },
            new object[] { typeof(Microsoft.Xna.Framework.GamerServices.LeaderboardKey), typeof(LeaderboardKey) },
            new object[] { typeof(Microsoft.Xna.Framework.GamerServices.LeaderboardOutcome), typeof(LeaderboardOutcome) },
            new object[] { typeof(Microsoft.Xna.Framework.GamerServices.MessageBoxIcon), typeof(MessageBoxIcon) },
            new object[] { typeof(Microsoft.Xna.Framework.GamerServices.NotificationPosition), typeof(NotificationPosition) },
            new object[] { typeof(Microsoft.Xna.Framework.GamerServices.RacingCameraAngle), typeof(RacingCameraAngle) },
        };
        
        [TestCaseSource("EnumPairs")]
        public void CompareEnums(Type first, Type second)
        {
            string[] xnaNames = Enum.GetNames(first);
            string[] anxNames = Enum.GetNames(second);

            for(int index = 0; index < xnaNames.Length; index++)
            {
                Assert.AreEqual(xnaNames[index], anxNames[index]);
                Type xnaUnderlyingType = Enum.GetUnderlyingType(first);
                Type anxUnderlyingType = Enum.GetUnderlyingType(second);
                Assert.AreEqual(xnaUnderlyingType, anxUnderlyingType);

                object xnaValue = Enum.Parse(first, xnaNames[index]);
                object anxValue = Enum.Parse(second, anxNames[index]);
                if (xnaUnderlyingType == typeof(byte))
                    Assert.AreEqual((byte)xnaValue, (byte)anxValue);
                else
                    Assert.AreEqual((int)xnaValue, (int)anxValue);
            }
        }
    }
}
