#region Using Statements
using System;

#endregion // Using Statements

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Content
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public sealed class ContentSerializerAttribute : Attribute
    {
        public string ElementName { get; set; }

        public bool FlattenContent { get; set; }

        public bool Optional { get; set; }

        private bool allowNull = true;
        public bool AllowNull
        {
            get { return this.allowNull; }
            set { this.allowNull = value; }
        }

        public bool SharedResource { get; set; }

        private string collectionItemName;
        public string CollectionItemName
        {
            get
            {
                if (string.IsNullOrEmpty(this.collectionItemName))
                {
                    return "Item";
                }
                return this.collectionItemName;
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
                allowNull = this.allowNull,
                SharedResource = this.SharedResource,
                collectionItemName = this.collectionItemName
            };
        }
    }
}
