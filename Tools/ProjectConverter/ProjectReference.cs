using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ProjectConverter
{
    class ProjectReference
    {
        public string Name
        {
            get;
            set;
        }

        public Guid Guid
        {
            get;
            set;
        }

        public Uri RelativeProjectPath
        {
            get;
            set;
        }

        public ProjectReference()
        {

        }

        public ProjectReference(string name, string guid, string relativeSubPath, string postfix)
        {
            this.Name = name;
            this.Guid = new Guid(guid);

            string path = Path.Combine(relativeSubPath, name, name + "_" + postfix + ".csproj");

            this.RelativeProjectPath = new Uri(path, UriKind.Relative);
        }
    }
}
