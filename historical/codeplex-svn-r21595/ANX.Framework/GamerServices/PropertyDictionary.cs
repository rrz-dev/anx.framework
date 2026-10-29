#region Using Statements
using System;
using System.Collections.Generic;
using System.Collections;
using System.IO;
using ANX.Framework.NonXNA.Development;

#endregion // Using Statements

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.GamerServices
{
    [PercentageComplete(10)]
    [Developer("Glatzemann")]
    [TestState(TestStateAttribute.TestState.Untested)]
    public sealed class PropertyDictionary : IDictionary<string, object>, ICollection<KeyValuePair<string, object>>, IEnumerable<KeyValuePair<string, object>>, IEnumerable
    {
        public void Add(string key, object value)
        {
            throw new NotImplementedException();
        }

        public bool ContainsKey(string key)
        {
            throw new NotImplementedException();
        }

        public ICollection<string> Keys
        {
            get { throw new NotImplementedException(); }
        }

        public bool Remove(string key)
        {
            throw new NotImplementedException();
        }

        public bool TryGetValue(string key, out object value)
        {
            throw new NotImplementedException();
        }

        public ICollection<object> Values
        {
            get { throw new NotImplementedException(); }
        }

        public object this[string key]
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public void Add(KeyValuePair<string, object> item)
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public bool Contains(KeyValuePair<string, object> item)
        {
            throw new NotImplementedException();
        }

        public void CopyTo(KeyValuePair<string, object>[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public int Count
        {
            get { throw new NotImplementedException(); }
        }

        public bool IsReadOnly
        {
            get { throw new NotImplementedException(); }
        }

        public bool Remove(KeyValuePair<string, object> item)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<KeyValuePair<string, object>> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
			
        public int GetValueInt32(string key)
        {
            throw new NotImplementedException();
        }
			
        public Int64 GetValueInt64(string key)
        {
            throw new NotImplementedException();
        }
			
        public float GetValueSingle(string key)
        {
            throw new NotImplementedException();
        }
			
        public double GetValueDouble(string key)
        {
            throw new NotImplementedException();
        }
			
        public string GetValueString(string key)
        {
            throw new NotImplementedException();
        }
			
        public LeaderboardOutcome GetValueOutcome(string key)
        {
            throw new NotImplementedException();
        }
			
        public DateTime GetValueDateTime(string key)
        {
            throw new NotImplementedException();
        }
			
        public TimeSpan GetValueTimeSpan(string key)
        {
            throw new NotImplementedException();
        }
			
        public Stream GetValueStream(string key)
        {
            throw new NotImplementedException();
        }
			
        public void SetValue(string key, int value)
        {
            throw new NotImplementedException();
        }
			
        public void SetValue(string key, Int64 value)
        {
            throw new NotImplementedException();
        }
			
        public void SetValue(string key, float value)
        {
            throw new NotImplementedException();
        }
			
        public void SetValue(string key, double value)
        {
            throw new NotImplementedException();
        }
			
        public void SetValue(string key, string value)
        {
            throw new NotImplementedException();
        }
			
        public void SetValue(string key, LeaderboardOutcome value)
        {
            throw new NotImplementedException();
        }
			
        public void SetValue(string key, DateTime value)
        {
            throw new NotImplementedException();
        }
			
        public void SetValue(string key, TimeSpan value)
        {
            throw new NotImplementedException();
        }
    }
}
