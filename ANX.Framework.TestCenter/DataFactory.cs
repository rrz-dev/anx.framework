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
        /// <summary>
        /// Create a test-dataset with a permutation of extreme value
        /// </summary>
        /// <param name="maxValue">max. extreme</param>
        /// <param name="minValue">min. extreme</param>
        /// <param name="numberOfElements">how many elements are used in test</param>
        /// <returns>test bundle with numberOfElements*numberOfElements test sets</returns>
        public static object[] createPermutation(object maxValue, object minValue, int numberOfElements)
        {
            object[] valuePair = { maxValue, minValue };
            int testCaseCount = (int)Math.Pow(numberOfElements, 2);
            object[,] matrix = new object[numberOfElements, testCaseCount];
            for (int x = 0; x < numberOfElements; ++x)
            {
                int counter = 0;
                int value = 0;
                for (int y = 0; y < testCaseCount; ++y)
                {

                    matrix[x, y] = valuePair[value];
                    counter++;
                    if (counter > x)
                    {
                        counter = 0;
                        if (value == 0)
                            value = 1;
                        else
                            value = 0;
                    }
                }
            }
            object[] result = new object[testCaseCount];
            for (int y = 0; y < testCaseCount; ++y)
            {
                object[] line = new object[numberOfElements];
                for (int x = 0; x < numberOfElements; ++x)
                {
                    line[x] = matrix[x, y];
                }
                result[y] = line;
            }
            return result;

        }
        static object[] generateTestData(int numOfTests, int numOfElements, int min, int max)
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
