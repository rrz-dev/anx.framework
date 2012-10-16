#region Using Statements

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using XNAViewport = Microsoft.Xna.Framework.Graphics.Viewport;
using ANXViewport = ANX.Framework.Graphics.Viewport;

using NUnit.Framework;

#endregion // Using Statements

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.TestCenter.Strukturen.Graphics
{
    [TestFixture]
    class ViewportTest
    {
        #region Testdata

        static object[] fourInteger =
        {
            new object[] { DataFactory.RandomInt, DataFactory.RandomInt, DataFactory.RandomInt, DataFactory.RandomInt },
            new object[] { DataFactory.RandomInt, DataFactory.RandomInt, DataFactory.RandomInt, DataFactory.RandomInt },
            new object[] { DataFactory.RandomInt, DataFactory.RandomInt, DataFactory.RandomInt, DataFactory.RandomInt },
            new object[] { DataFactory.RandomInt, DataFactory.RandomInt, DataFactory.RandomInt, DataFactory.RandomInt },
            new object[] { DataFactory.RandomInt, DataFactory.RandomInt, DataFactory.RandomInt, DataFactory.RandomInt }
        };

        #endregion

        #region Tests

        #region Constructor

        [Test, TestCaseSource("fourInteger")]
        public void Constructor(int x, int y, int width, int height)
        {
            XNAViewport xnaViewport = new XNAViewport(x, y, width, height);
            ANXViewport anxViewport = new ANXViewport(x, y, width, height);

            AssertHelper.ConvertEquals(xnaViewport, anxViewport, "Viewport Constructor");
        }

        #endregion

        #endregion
    }
}
