using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.IO;

namespace ANX.ContentCompiler.GUI.Nodes
{
    class FolderNodeProperties : NodeProperties, INotifyPropertyChanged
    {
        public FolderNodeProperties(string folderName)
        {
            this._name = folderName;
        }

        private string _name;

        public string Name
        {
            get { return _name; }
            set
            {
                if (_name != value)
                {
                    if (value != null)
                    {
                        foreach (var c in Path.GetInvalidFileNameChars())
                        {
                            if (value.Contains(c))
                                return;
                        }
                    }

                    _name = value;
                    OnPropertyChanged(new PropertyChangedEventArgs("Name"));
                }
            }
        }

        protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, e);
        }

        [Browsable(false)]
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
