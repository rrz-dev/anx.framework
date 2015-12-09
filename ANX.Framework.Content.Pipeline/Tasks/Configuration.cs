using ANX.Framework.Graphics;
using ANX.Framework.NonXNA.Development;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace ANX.Framework.Content.Pipeline.Tasks
{
    /// <summary>
    /// Represents a named collection of values.
    /// </summary>
    [Serializable]
    public class Configuration : ICloneable
    {
        public Configuration(string name, TargetPlatform platform)
        {
            this.Name = name;
            this.Platform = platform;
        }

        public Configuration(string name, TargetPlatform platform, Configuration original)
        {
            if (original == null)
                throw new ArgumentNullException("original");

            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("name must not be empty.");

            this.Name = name;
            this.Platform = platform;
            this.Profile = original.Profile;
            this.CompressContent = original.CompressContent;
            this.OutputDirectory = original.OutputDirectory;
        }

        /// <summary>
        /// Name of the configuration.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// The platform the content will be compiled for.
        /// </summary>
        public TargetPlatform Platform { get; private set; }

        /// <summary>
        /// Target Graphics Profile
        /// </summary>
        public GraphicsProfile Profile { get; set; }

        /// <summary>
        /// States if the content should be compressed.
        /// <remarks><see cref="ContentImporter"/>s can ignore this value if they state so.</remarks>
        /// </summary>
        public bool CompressContent { get; set; }

        /// <summary>
        /// The directory where the compiled output will be placed
        /// </summary>
        public string OutputDirectory { get; set; }

        /// <summary>
        /// Returns true if the configuration is empty.
        /// </summary>
        public virtual bool IsEmpty
        {
            get
            {
                //Don't check Name and Platform, they are not part of the content but part of the identification.
                return CompressContent == false && OutputDirectory == null;
            }
        }

        public virtual object Clone()
        {
            return new Configuration(this.Name, this.Platform, this);
        }

        public override string ToString()
        {
            return string.Format("Name: {0}; Platform: {1}", Name, Platform);
        }
    }
}
