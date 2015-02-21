using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AnxSampleBrowser
{
    
    public struct SampleData : IEquatable<SampleData>
    {
        public String Name { get; set; }
        public String Description { get; set; }
        public String ExecPath { get; set; }
        public String ProjectPath { get; set; }
        public String ImagePath { get; set; }
        public String Category { get; set; }
        public String[] Tags { get; set; }


        internal void Validate()
        {
        
               
        }

        public bool Equals(SampleData other)
        {
            if (this.Name == other.Name && this.Description == other.Description && this.ExecPath == other.ExecPath &&
                this.ProjectPath == other.ProjectPath && this.ImagePath == other.ImagePath && this.Category == other.Category)
            {
                if (this.Tags != null && other.Tags != null)
                {
                    if (this.Tags.Length == other.Tags.Length)
                    {
                        for (int i = 0, length = this.Tags.Length; i < length; i++)
                            if (this.Tags[i] != other.Tags[i])
                                return false;

                        return true;
                    }
                }
                else if (this.Tags == null && other.Tags == null)
                    return true;
            }
            return false;
        }

        public override bool Equals(object obj)
        {
            if (obj is SampleData)
                return this.Equals((SampleData)obj);
            return false;
        }

        public static bool operator ==(SampleData left, SampleData right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(SampleData left, SampleData right)
        {
            return !left.Equals(right);
        }
    }
}
