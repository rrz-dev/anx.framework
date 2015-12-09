#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Reflection;

#endregion

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Content.Pipeline.Graphics
{
    public sealed class VertexChannelCollection : IList<VertexChannel>, ICollection<VertexChannel>, IEnumerable<VertexChannel>, IEnumerable
    {
        private List<VertexChannel> channels = new List<VertexChannel>();
        private VertexContent container;

        internal VertexChannelCollection(VertexContent container)
        {
            this.container = container;
        }

        public VertexChannel this[string name]
        {
            get
            {
                if (string.IsNullOrEmpty(name))
                    throw new ArgumentNullException("name");

                int index = this.IndexOf(name);
                if (index < 0)
                    throw new KeyNotFoundException(string.Format("Vertex channel \"{0}\" was not found.", name));

                return this.channels[index];
            }
        }

        public VertexChannel this[int index]
        {
            get
            {
                return this.channels[index];
            }
            set
            {
                throw new NotSupportedException("VertexChannelCollection does not support adding channels via the ICollection or IList interfaces. Use the VertexChannelCollection Add or Insert methods instead.");
            }
        }

        public int Count
        {
            get
            {
                return this.channels.Count;
            }
        }

        bool ICollection<VertexChannel>.IsReadOnly
        {
            get
            {
                return false;
            }
        }

        public VertexChannel<ElementType> Add<ElementType>(string name, IEnumerable<ElementType> channelData)
        {
            return this.Insert<ElementType>(this.channels.Count, name, channelData);
        }

        public VertexChannel Add(string name, Type elementType, IEnumerable channelData)
        {
            return this.Insert(this.channels.Count, name, elementType, channelData);
        }

        public VertexChannel<ElementType> Insert<ElementType>(int index, string name, IEnumerable<ElementType> channelData)
        {
            if (index < 0 || index > this.channels.Count)
                throw new ArgumentOutOfRangeException("index");
            
            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException("name");
            
            if (this.Contains(name))
                throw new ArgumentException(string.Format("VertexChannelCollection already contains a channel with name \"{0}\".", name));
            

            VertexChannel<ElementType> vertexChannel = new VertexChannel<ElementType>(name);
            if (channelData != null)
            {
                vertexChannel.AddRange(channelData);
                if (vertexChannel.Count != this.container.VertexCount)
                {
                    throw new ArgumentException(string.Format("Wrong number of VertexChannel entries in \"{0}\". Channel size is {1}, but the parent VertexContent has a count of {2}.", name, vertexChannel.Count, this.container.VertexCount));
                }
            }
            else
            {
                vertexChannel.AddRange(this.container.VertexCount);
            }

            this.channels.Insert(index, vertexChannel);
            return vertexChannel;
        }

        public VertexChannel Insert(int index, string name, Type elementType, IEnumerable channelData)
        {
            if (index < 0 || index > this.channels.Count)
                throw new ArgumentOutOfRangeException("index");
            
            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException("name");
            
            if (elementType == null)
                throw new ArgumentNullException("elementType");
            
            if (this.Contains(name))
                throw new ArgumentException(string.Format("VertexChannelCollection already contains a channel with name \"{0}\".", name));
            

            Type type = typeof(VertexChannel<>).MakeGenericType(elementType);

            VertexChannel vertexChannel = (VertexChannel)Activator.CreateInstance(type, BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public, null, new [] { name }, null);

            if (channelData != null)
            {
                vertexChannel.AddRange(channelData);
                if (vertexChannel.Count != this.container.VertexCount)
                {
                    throw new ArgumentException(string.Format("Wrong number of VertexChannel entries in \"{0}\". Channel size is {1}, but the parent VertexContent has a count of {2}.", name, vertexChannel.Count, this.container.VertexCount));
                }
            }
            else
            {
                vertexChannel.AddRange(this.container.VertexCount);
            }

            this.channels.Insert(index, vertexChannel);

            return vertexChannel;
        }

        internal void Add(VertexChannel channel)
        {
            if (this.Contains(channel.Name))
                throw new ArgumentException(string.Format("VertexChannelCollection already contains a channel with name \"{0}\".", channel.Name));
            
            if (channel.Count != this.container.VertexCount)
                throw new ArgumentException(string.Format("Wrong number of VertexChannel entries in \"{0}\". Channel size is {1}, but the parent VertexContent has a count of {2}.", channel.Name, channel.Count, this.container.VertexCount));

            this.channels.Add(channel);
        }

        /// <summary>
        /// Converts the specified channel to the target content type.
        /// </summary>
        /// <typeparam name="TargetType"></typeparam>
        /// <param name="index"></param>
        /// <returns></returns>
        public VertexChannel<TargetType> ConvertChannelContent<TargetType>(int index)
        {
            VertexChannel vertexChannel = this[index];
            this.RemoveAt(index);
            VertexChannel<TargetType> result;
            try
            {
                result = this.Insert<TargetType>(index, vertexChannel.Name, vertexChannel.ReadConvertedContent<TargetType>());
            }
            catch
            {
                this.Insert(index, vertexChannel.Name, vertexChannel.ElementType, vertexChannel);
                throw;
            }
            return result;
        }

        public VertexChannel<TargetType> ConvertChannelContent<TargetType>(string name)
        {
            int index = this.IndexOf(name);
            if (index < 0)
                throw new KeyNotFoundException(string.Format("Vertex channel \"{0}\" was not found.", name));

            return this.ConvertChannelContent<TargetType>(index);
        }

        public bool Contains(string name)
        {
            return this.IndexOf(name) >= 0;
        }

        public int IndexOf(string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException("name");
            
            return this.channels.FindIndex((VertexChannel channel) => channel.Name == name);
        }

        public bool Remove(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException("name");
            }

            int index = this.IndexOf(name);
            if (index < 0)
                return false;
            
            this.channels.RemoveAt(index);
            return true;
        }

        public VertexChannel<T> Get<T>(string name)
        {
            VertexChannel vertexChannel = this[name];
            if (vertexChannel.ElementType != typeof(T))
            {
                throw new InvalidOperationException(string.Format("Vertex channel \"{0}\" is the wrong type. It has element type {1}. Type {2} is expected.", name, vertexChannel.ElementType, typeof(T)));
            }
            return (VertexChannel<T>)vertexChannel;
        }

        /// <summary>
        /// Returns the vertex channel at the specified index. If the element type is not equal to <paramref name="T"/> an <see cref="InvalidOperationException"/> gets thrown.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="index"></param>
        /// <returns></returns>
        /// <exception cref="System.InvalidOperationException"></exception>
        public VertexChannel<T> Get<T>(int index)
        {
            VertexChannel vertexChannel = this[index];
            if (vertexChannel.ElementType != typeof(T))
            {
                throw new InvalidOperationException(string.Format("Vertex channel \"{0}\" is the wrong type. It has element type {1}. Type {2} is expected.", this[index].Name, vertexChannel.ElementType, typeof(T)));
            }
            return (VertexChannel<T>)vertexChannel;
        }

        public IEnumerator<VertexChannel> GetEnumerator()
        {
            return this.channels.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.channels.GetEnumerator();
        }

        public void Clear()
        {
            this.channels.Clear();
        }

        public bool Contains(VertexChannel item)
        {
            return this.channels.Contains(item);
        }

        public int IndexOf(VertexChannel item)
        {
            return this.channels.IndexOf(item);
        }

        public bool Remove(VertexChannel item)
        {
            return this.channels.Remove(item);
        }

        public void RemoveAt(int index)
        {
            this.channels.RemoveAt(index);
        }

        void ICollection<VertexChannel>.CopyTo(VertexChannel[] array, int arrayIndex)
        {
            this.channels.CopyTo(array, arrayIndex);
        }

        void ICollection<VertexChannel>.Add(VertexChannel item)
        {
            throw new NotSupportedException("VertexChannelCollection does not support adding channels via the ICollection or IList interfaces. Use the VertexChannelCollection Add or Insert methods instead.");
        }

        void IList<VertexChannel>.Insert(int index, VertexChannel item)
        {
            throw new NotSupportedException("VertexChannelCollection does not support adding channels via the ICollection or IList interfaces. Use the VertexChannelCollection Add or Insert methods instead.");
        }
    }
}
