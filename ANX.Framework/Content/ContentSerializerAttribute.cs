#region Using Statements
using System;
using ANX.Framework.NonXNA.Development;

#endregion // Using Statements

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Content
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    [PercentageComplete(100)]
    [Developer("GinieDP")]
    [TestState(TestStateAttribute.TestState.Untested)]
    public sealed class ContentSerializerAttribute : Attribute
    {
        public string ElementName { get; set; }

        public bool FlattenContent { get; set; }

        public bool Optional { get; set; }

        public bool AllowNull { get; set; }

        public bool SharedResource { get; set; }

        private string collectionItemName;

        public ContentSerializerAttribute()
        {
            AllowNull = true;
        }

        public string CollectionItemName
        {
            get
            {
                return string.IsNullOrEmpty(this.collectionItemName) ? "Item" : this.collectionItemName;
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentNullException("value");
                }
                this.collectionItemName = value;
            }
        }

        public bool HasCollectionItemName
        {
            get
            {
                return !string.IsNullOrEmpty(this.collectionItemName);
            }
        }

        public ContentSerializerAttribute Clone()
        {
            return new ContentSerializerAttribute
            {
                ElementName = this.ElementName,
                FlattenContent = this.FlattenContent,
                Optional = this.Optional,
                AllowNull = this.AllowNull,
                SharedResource = this.SharedResource,
                collectionItemName = this.collectionItemName
            };
        }
    }
}
