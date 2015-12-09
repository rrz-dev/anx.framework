using ANX.ContentCompiler.GUI.Converters;
using ANX.Framework.Content.Pipeline;
using ANX.Framework.Content.Pipeline.Tasks;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;

namespace ANX.ContentCompiler.GUI.Nodes
{
    public class ContentProjectNodeProperties : NodeProperties, INotifyPropertyChanged
    {
        private ContentProject _contentProject;
        private string _activeConfiguration;
        private TargetPlatform _activePlatform;

        public ContentProjectNodeProperties(ContentProject contentProject)
        {
            if (contentProject == null)
                throw new ArgumentNullException("contentProject");

            this._contentProject = contentProject;
        }

        [Browsable(false)]
        internal ContentProject ContentProject
        {
            get { return _contentProject; }
        }

        public string Name
        {
            get { return _contentProject.Name; }
            set 
            {
                if (_contentProject.Name != value)
                {
                    if (value != null)
                    {
                        foreach (var c in Path.GetInvalidFileNameChars())
                        {
                            if (value.Contains(c))
                                return;
                        }
                    }

                    _contentProject.Name = value;
                    OnPropertyChanged(new PropertyChangedEventArgs("Name"));
                }
            }
        }

        [DisplayName("Output Directory")]
        public string OutputDirectory
        {
            get 
            { 
                Configuration config;
                if (!_contentProject.Configurations.TryGetConfiguration(ActiveConfiguration, ActivePlatform, out config))
                    return null;
                else
                    return config.OutputDirectory;
            }
            set
            {
                Configuration config;
                if (_contentProject.Configurations.TryGetConfiguration(ActiveConfiguration, ActivePlatform, out config))
                {
                    if (config.OutputDirectory != value)
                    {
                        config.OutputDirectory = value;
                        OnPropertyChanged(new PropertyChangedEventArgs("OutputDirectory"));
                    }
                }
            }
        }

        [TypeConverter(typeof(ActiveConfigurationConverter))]
        [DisplayName("Active Configuration")]
        public string ActiveConfiguration
        {
            get { return _activeConfiguration; }
            set
            {
                if (_activeConfiguration != value)
                {
                    _activeConfiguration = value;
                    OnPropertyChanged(new PropertyChangedEventArgs("ActiveConfiguration"));
                }
            }
        }

        [TypeConverter(typeof(ActivePlatformConverter))]
        [DisplayName("Active Platform")]
        public TargetPlatform ActivePlatform
        {
            get { return _activePlatform; }
            set
            {
                if (_activePlatform != value)
                {
                    _activePlatform = value;
                    OnPropertyChanged(new PropertyChangedEventArgs("ActivePlatform"));
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
