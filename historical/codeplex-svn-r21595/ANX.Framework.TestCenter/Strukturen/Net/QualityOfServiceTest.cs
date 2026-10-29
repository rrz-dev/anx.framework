using System;
using System.Globalization;
using System.Reflection;
using NUnit.Framework;

using XNAQualityOfService = Microsoft.Xna.Framework.Net.QualityOfService;
using ANXQualityOfService = ANX.Framework.Net.QualityOfService;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.TestCenter.Strukturen.Net
{
    class QualityOfServiceTest
    {
        [Test]
        public void Constructor1()
        {
            var xna = (XNAQualityOfService)Activator.CreateInstance(typeof(XNAQualityOfService), true);
            var anx = (ANXQualityOfService)Activator.CreateInstance(typeof(ANXQualityOfService), true);

            Assert.AreEqual(xna.IsAvailable, anx.IsAvailable);
            Assert.AreEqual(xna.BytesPerSecondUpstream, anx.BytesPerSecondUpstream);
            Assert.AreEqual(xna.BytesPerSecondDownstream, anx.BytesPerSecondDownstream);
            Assert.AreEqual(xna.AverageRoundtripTime, anx.AverageRoundtripTime);
            Assert.AreEqual(xna.MinimumRoundtripTime, anx.MinimumRoundtripTime);
        }

        [Test]
        public void Constructor2()
        {
            var parameters = new object[] { 1024, 512, TimeSpan.FromSeconds(10.0), TimeSpan.FromSeconds(1.0) };
            var xna = (XNAQualityOfService)Activator.CreateInstance(typeof(XNAQualityOfService),
                BindingFlags.NonPublic | BindingFlags.Instance, null, parameters, CultureInfo.InvariantCulture);
            var anx = (ANXQualityOfService)Activator.CreateInstance(typeof(ANXQualityOfService),
                BindingFlags.NonPublic | BindingFlags.Instance, null, parameters, CultureInfo.InvariantCulture);

            Assert.AreEqual(xna.IsAvailable, anx.IsAvailable);
            Assert.AreEqual(xna.BytesPerSecondUpstream, anx.BytesPerSecondUpstream);
            Assert.AreEqual(xna.BytesPerSecondDownstream, anx.BytesPerSecondDownstream);
            Assert.AreEqual(xna.AverageRoundtripTime, anx.AverageRoundtripTime);
            Assert.AreEqual(xna.MinimumRoundtripTime, anx.MinimumRoundtripTime);
        }
    }
}
