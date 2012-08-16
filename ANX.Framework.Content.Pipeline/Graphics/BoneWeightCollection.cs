#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

#endregion

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Content.Pipeline.Graphics
{
    public class BoneWeightCollection : Collection<BoneWeight>
    {
        public BoneWeightCollection()
        {

        }

        public void NormalizeWeights()
        {
            throw new NotImplementedException();
        }

        public void NormalizeWeights(int maxWeights)
        {
            throw new NotImplementedException();
        }
    }
}
