#region Using Statements
using System;
using System.Collections;
using System.Collections.Generic;
using ANX.Framework.NonXNA.Development;

#endregion // Using Statements

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Graphics
{
    [PercentageComplete(30)]
    [Developer("Glatzemann")]
    [TestState(TestStateAttribute.TestState.Untested)]
    public class DisplayModeCollection : IEnumerable<DisplayMode>, IEnumerable
    {
        private List<DisplayMode> displayModes = new List<DisplayMode>();

        public IEnumerator<DisplayMode> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<DisplayMode> this[SurfaceFormat format]
        {
            get
            {
                foreach (DisplayMode mode in displayModes)
                {
                    if (mode.Format == format)
                    {
                        yield return mode;
                    }
                }
            }
            set
            {
                foreach (DisplayMode mode in value)
                {
                    displayModes.Add(mode);
                }
            }
        }

    }
}
