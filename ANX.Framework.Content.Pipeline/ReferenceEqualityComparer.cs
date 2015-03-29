using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ANX.Framework.Content.Pipeline
{
    /// <summary>
    /// Makes sure that the custom GetHashCode method of the type to compare is ignored and absolutely compare only references.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    internal class ReferenceEqualityComparer<T> : IEqualityComparer<T>
    {
        public bool Equals(T x, T y)
        {
            return object.ReferenceEquals(x, y);
        }

        public int GetHashCode(T obj)
        {
            return obj.GetHashCode();
        }
    }
}
