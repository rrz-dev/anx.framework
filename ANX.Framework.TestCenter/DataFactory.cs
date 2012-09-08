#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

#endregion // Using Statements

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.TestCenter
{
    class DataFactory
    {
        static Random r = new Random();

        public static object[] createFullTestSet<T>(int numberOfElements) where T : struct, IComparable<T>, IEquatable<T>, IConvertible
        {
            return createFullTestSet<T>(numberOfElements, 0);
        }
        public static object[] createFullTestSet<T>(int numberOfElements, int numberOfRandomSets) where T : struct, IComparable<T>, IEquatable<T>, IConvertible
        {
            T maxValue = ReadStaticField<T>("MaxValue");
            T MinValue = ReadStaticField<T>("MinValue");
            object[] random = new object[numberOfRandomSets];
            for (int i = 0; i < numberOfRandomSets; ++i)
            {

            }
            return createLinear(0, numberOfElements).Concat(
                createLinear(1, numberOfElements).Concat(
                createPermutation(maxValue, MinValue, numberOfElements).Concat(
                createPermutation(maxValue, 0, numberOfElements).Concat(
                createPermutation(0, MinValue, numberOfElements))))).ToArray();
        }

        /// <summary>
        /// Create a test dataset filled with constant numbers
        /// </summary>
        /// <param name="value">Number which should fill the test set</param>
        /// <param name="numberOfElements">How many values are needed</param>
        /// <returns></returns>
        public static object[] createLinear(object value, int numberOfElements)
        {
            object[] result = new object[numberOfElements];
            for (int i = 0; i < numberOfElements; ++i)
                result[i] = value;
            return result;
        }


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

        private static T ReadStaticField<T>(string name)
        {
            FieldInfo field = typeof(T).GetField(name,
           BindingFlags.Public | BindingFlags.Static);
            if (field == null)
            {
                // There's no TypeArgumentException, unfortunately. You might want    
                // to create one :)        
                throw new InvalidOperationException("Invalid type argument for NumericUpDown<T>: " + typeof(T).Name);
            }
            return (T)field.GetValue(null);
        }

        #region OLD
        
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
        //*/
        #endregion
    }
}
