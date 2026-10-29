using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AnxSampleBrowser
{
    
    public struct SampleData
    {
        public String Name { get; set; }
        public String Description { get; set; }
        public String ExecPath { get; set; }
        public String ImagePath { get; set; }
        public String Categorie { get; set; }
        public String[] Tags { get; set; }


        internal void Validate()
        {
        
               
        }
    }
}
