﻿#region Using Statements
using System;
using System.Collections.Generic;
using System.Collections;
using System.IO;

#endregion // Using Statements

#region License

//
// This file is part of the ANX.Framework created by the "ANX.Framework developer group".
//
// This file is released under the Ms-PL license.
//
//
//
// Microsoft Public License (Ms-PL)
//
// This license governs use of the accompanying software. If you use the software, you accept this license. 
// If you do not accept the license, do not use the software.
//
// 1.Definitions
//   The terms "reproduce," "reproduction," "derivative works," and "distribution" have the same meaning 
//   here as under U.S. copyright law.
//   A "contribution" is the original software, or any additions or changes to the software.
//   A "contributor" is any person that distributes its contribution under this license.
//   "Licensed patents" are a contributor's patent claims that read directly on its contribution.
//
// 2.Grant of Rights
//   (A) Copyright Grant- Subject to the terms of this license, including the license conditions and limitations 
//       in section 3, each contributor grants you a non-exclusive, worldwide, royalty-free copyright license to 
//       reproduce its contribution, prepare derivative works of its contribution, and distribute its contribution
//       or any derivative works that you create.
//   (B) Patent Grant- Subject to the terms of this license, including the license conditions and limitations in 
//       section 3, each contributor grants you a non-exclusive, worldwide, royalty-free license under its licensed
//       patents to make, have made, use, sell, offer for sale, import, and/or otherwise dispose of its contribution 
//       in the software or derivative works of the contribution in the software.
//
// 3.Conditions and Limitations
//   (A) No Trademark License- This license does not grant you rights to use any contributors' name, logo, or trademarks.
//   (B) If you bring a patent claim against any contributor over patents that you claim are infringed by the software, your 
//       patent license from such contributor to the software ends automatically.
//   (C) If you distribute any portion of the software, you must retain all copyright, patent, trademark, and attribution 
//       notices that are present in the software.
//   (D) If you distribute any portion of the software in source code form, you may do so only under this license by including
//       a complete copy of this license with your distribution. If you distribute any portion of the software in compiled or 
//       object code form, you may only do so under a license that complies with this license.
//   (E) The software is licensed "as-is." You bear the risk of using it. The contributors give no express warranties, guarantees,
//       or conditions. You may have additional consumer rights under your local laws which this license cannot change. To the
//       extent permitted under your local laws, the contributors exclude the implied warranties of merchantability, fitness for a 
//       particular purpose and non-infringement.

#endregion // License

namespace ANX.Framework.GamerServices
{
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
