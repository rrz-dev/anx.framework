#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

#endregion // Using Statements

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.TestCenter
{
    class DataFactory
    {
        static Random r=new Random();

        static object[] generateTestData(int numOfTests, int numOfElements,int min,int max)
        {
            object[] result = new object[numOfTests];

            object[] testRun = new object[numOfElements];
            for (int run = 0; run < numOfTests; ++run)
            {
                for (int data = 0; data < numOfElements; ++data)
                {
                    testRun[data] = r.Next(min, max);
                }
                result[run] = testRun;
            }

            return result;
        }

        public static float RandomNormalizedFloat
        {
            get
            {
                return 1.0f / MathHelper.Clamp(RandomFloat, float.Epsilon, float.MaxValue);
            }
        }

        public static float RandomFloat
        {
            get { return (float)(r.NextDouble() * (float.MaxValue - 1) - r.NextDouble() * (float.MinValue + 1)); }
        }

        public static int RandomBitPlus
        {
            get { return r.Next(3) - 1; }
        }

        public static float RandomValue
        {
            get { return r.Next(1000) * RandomBitPlus; }
        }

        public static float RandomValueMinMax(float min, float max)
        {
            return (float)r.Next((int)min, (int)max) * (float)r.NextDouble();
        }

        public static int RandomIntValueMinMax(int min, int max)
        {
            return r.Next(min, max);
        }
    }
}
