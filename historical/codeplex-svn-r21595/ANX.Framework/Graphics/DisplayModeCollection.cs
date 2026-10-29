#region Using Statements
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ANX.Framework.NonXNA.Development;

#endregion // Using Statements

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Graphics
{
    [PercentageComplete(100)]
    [Developer("AstrorEnales")]
    [TestState(TestStateAttribute.TestState.Tested)]
    public class DisplayModeCollection : IEnumerable<DisplayMode>, IEnumerable
    {
        private readonly List<DisplayMode> displayModes;

        public IEnumerable<DisplayMode> this[SurfaceFormat format]
        {
            get { return displayModes.Where(current => current.Format == format).ToList(); }
        }

        internal DisplayModeCollection(List<DisplayMode> displayModes)
        {
            this.displayModes = displayModes;
        }

        public IEnumerator<DisplayMode> GetEnumerator()
        {
            return displayModes.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
