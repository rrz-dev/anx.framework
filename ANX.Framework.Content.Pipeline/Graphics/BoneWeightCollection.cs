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
            : base(new List<BoneWeight>())
        {

        }

        /// <summary>
        /// Normalizes the contents of the bone weights list. 
        /// </summary>
        /// <remarks>
        /// Normalization does the following:
        /// <list type="bullet">
        ///     <item>Sorts weights such that the most significant weight is first.</item>
        ///     <item>Removes zero-value entries.</item>
        ///     <item>Discards weights with the smallest value until there are maxWeights or less in the list.</item>
        ///     <item>Adjusts values so the sum equals one.</item>
        /// </list>
        /// </remarks>
        /// <exception cref="System.InvalidContentException">Gets thrown if all weights are zero.</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">Gets thrown if maxWeights less or equal to zero.</exception>
        public void NormalizeWeights()
        {
            this.NormalizeWeights(int.MaxValue);
        }
        
        /// <summary>
        /// Normalizes the contents of the bone weights list. 
        /// </summary>
        /// <remarks>
        /// Normalization does the following:
        /// <list type="bullet">
        ///     <item>Sorts weights such that the most significant weight is first.</item>
        ///     <item>Removes zero-value entries.</item>
        ///     <item>Discards weights with the smallest value until there are maxWeights or less in the list.</item>
        ///     <item>Adjusts values so the sum equals one.</item>
        /// </list>
        /// </remarks>
        /// <param name="maxWeights">Maximum number of weights allowed.</param>
        /// <exception cref="System.InvalidContentException">Gets thrown if all weights are zero.</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">Gets thrown if maxWeights less or equal to zero.</exception>
        public void NormalizeWeights(int maxWeights)
        {
            if (maxWeights <= 0)
                throw new ArgumentOutOfRangeException("maxWeights");

            ((List<BoneWeight>)Items).Sort((left, right) => right.Weight.CompareTo(left.Weight));

            float accumulatedValue = 0f;
            for (int i = 0; i < Count; i++)
            {
                var weight = Items[i].Weight;

                if (weight <= 0f || i >= maxWeights)
                {
                    this.RemoveAt(i);
                    i--;
                }
                else
                {
                    accumulatedValue += weight;
                }
            }

            if (accumulatedValue == 0f)
            {
                throw new InvalidContentException(string.Format("All weights are zero."));
            }

            if (accumulatedValue != 0f)
            {
                for (int i = 0; i < Count; i++)
                {
                    this[i] = new BoneWeight(this[i].BoneName, accumulatedValue / this[i].Weight);
                }
            }
        }
    }
}
