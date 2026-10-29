using System;
using NUnit.Framework;

using XNAPreparingDeviceSettingsEventArgs = Microsoft.Xna.Framework.PreparingDeviceSettingsEventArgs;
using ANXPreparingDeviceSettingsEventArgs = ANX.Framework.PreparingDeviceSettingsEventArgs;

using XNAGameComponentCollectionEventArgs = Microsoft.Xna.Framework.GameComponentCollectionEventArgs;
using ANXGameComponentCollectionEventArgs = ANX.Framework.GameComponentCollectionEventArgs;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.TestCenter.Strukturen
{
    class EventArgsTest
    {
        [Test]
        public void PreparingDeviceSettingsEventArgs()
        {
            var xna = new XNAPreparingDeviceSettingsEventArgs(null);
            var anx = new ANXPreparingDeviceSettingsEventArgs(null);

            Assert.AreEqual(xna.GraphicsDeviceInformation, anx.GraphicsDeviceInformation);
        }

        [Test]
        public void GameComponentCollectionEventArgs()
        {
            var xna = new XNAGameComponentCollectionEventArgs(null);
            var anx = new ANXGameComponentCollectionEventArgs(null);
            
            Assert.AreEqual(xna.GameComponent, anx.GameComponent);
        }
    }
}
