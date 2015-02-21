using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AnxSampleBrowser
{
    class SampleDataCategoryComparer : IComparer<SampleData>
    {
        private static SampleDataCategoryComparer _instance;

        public static SampleDataCategoryComparer Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new SampleDataCategoryComparer();

                return _instance;
            }
        }

        public int Compare(SampleData x, SampleData y)
        {
            return x.Category.CompareTo(y.Category);
        }
    }

    class SampleDataNameComparer : IComparer<SampleData>
    {
        private static SampleDataNameComparer _instance;

        public static SampleDataNameComparer Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new SampleDataNameComparer();

                return _instance;
            }
        }

        public int Compare(SampleData x, SampleData y)
        {
            return x.Name.CompareTo(y.Name);
        }
    }
}
