using ANX.Framework.NonXNA.Development;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ANX.Framework.Content.Pipeline.Tasks
{
    [Serializable]
    public class ConfigurationCollection : IList<Configuration>
    {
        private List<Configuration> items = new List<Configuration>();

        public int IndexOf(Configuration item)
        {
            return items.IndexOf(item);
        }

        public int IndexOf(string name, TargetPlatform platform)
        {
            for (int i = 0; i < Count; i++)
            {
                var currentPlatform = items[i].Platform;
                if (items[i].Name == name && currentPlatform == platform)
                    return i;
            }
            return -1;
        }

        public void Insert(int index, Configuration item)
        {
            if (item == null)
                throw new ArgumentNullException("item");

            if (this.Contains(item.Name, item.Platform))
                throw new ArgumentException(string.Format("Duplicate entry for name \"{0}\" and platform \"{1}\"", item.Name, item.Platform));

            items.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            items.RemoveAt(index);
        }

        public Configuration this[int index]
        {
            get
            {
                return items[index];
            }
            set
            {
                if (value == null)
                    throw new ArgumentNullException("value");

                if (this.Contains(value.Name, value.Platform))
                    throw new ArgumentException(string.Format("Duplicate entry for name \"{0}\" and platform \"{1}\"", value.Name, value.Platform));

                items[index] = value;
            }
        }

        public bool TryGetConfiguration(string name, TargetPlatform platform, out Configuration configuration)
        {
            int index = this.IndexOf(name, platform);
            if (index == -1)
            {
                configuration = null;
                return false;
            }
            else
            {
                configuration = items[index];
                return true;
            }
        }

        public void Add(Configuration item)
        {
            if (item == null)
                throw new ArgumentNullException("item");

            if (this.Contains(item.Name, item.Platform))
                throw new ArgumentException(string.Format("Duplicate entry for name \"{0}\" and platform \"{1}\"", item.Name, item.Platform));

            items.Add(item);
        }

        public void Clear()
        {
            items.Clear();
        }

        public bool Contains(Configuration item)
        {
            return items.Contains(item);
        }

        public bool Contains(string name, TargetPlatform platform)
        {
            return this.IndexOf(name, platform) != -1;
        }

        public void CopyTo(Configuration[] array, int arrayIndex)
        {
            items.CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get { return items.Count; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public bool Remove(Configuration item)
        {
            return items.Remove(item);
        }

        public IEnumerator<Configuration> GetEnumerator()
        {
            return items.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return items.GetEnumerator();
        }

        public string[] GetUniqueNames()
        {
            HashSet<string> result = new HashSet<string>();
            items.ForEach((x) => result.Add(x.Name));

            return result.ToArray();
        }

        public TargetPlatform[] GetUniquePlatforms()
        {
            HashSet<TargetPlatform> result = new HashSet<TargetPlatform>();
            items.ForEach((x) => result.Add(x.Platform));

            return result.ToArray();
        }
    }
}
