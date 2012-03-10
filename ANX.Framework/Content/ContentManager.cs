#region Using Statements
using System;
using System.Collections.Generic;
using System.IO;

#endregion // Using Statements

#region License

//
// This file is part of the ANX.Framework created by the "ANX.Framework developer group".
//
// This file is released under the Ms-PL license.
//
//
//
// Microsoft Public License (Ms-PL)
//
// This license governs use of the accompanying software. If you use the software, you accept this license. 
// If you do not accept the license, do not use the software.
//
// 1.Definitions
//   The terms "reproduce," "reproduction," "derivative works," and "distribution" have the same meaning 
//   here as under U.S. copyright law.
//   A "contribution" is the original software, or any additions or changes to the software.
//   A "contributor" is any person that distributes its contribution under this license.
//   "Licensed patents" are a contributor's patent claims that read directly on its contribution.
//
// 2.Grant of Rights
//   (A) Copyright Grant- Subject to the terms of this license, including the license conditions and limitations 
//       in section 3, each contributor grants you a non-exclusive, worldwide, royalty-free copyright license to 
//       reproduce its contribution, prepare derivative works of its contribution, and distribute its contribution
//       or any derivative works that you create.
//   (B) Patent Grant- Subject to the terms of this license, including the license conditions and limitations in 
//       section 3, each contributor grants you a non-exclusive, worldwide, royalty-free license under its licensed
//       patents to make, have made, use, sell, offer for sale, import, and/or otherwise dispose of its contribution 
//       in the software or derivative works of the contribution in the software.
//
// 3.Conditions and Limitations
//   (A) No Trademark License- This license does not grant you rights to use any contributors' name, logo, or trademarks.
//   (B) If you bring a patent claim against any contributor over patents that you claim are infringed by the software, your 
//       patent license from such contributor to the software ends automatically.
//   (C) If you distribute any portion of the software, you must retain all copyright, patent, trademark, and attribution 
//       notices that are present in the software.
//   (D) If you distribute any portion of the software in source code form, you may do so only under this license by including
//       a complete copy of this license with your distribution. If you distribute any portion of the software in compiled or 
//       object code form, you may only do so under a license that complies with this license.
//   (E) The software is licensed "as-is." You bear the risk of using it. The contributors give no express warranties, guarantees,
//       or conditions. You may have additional consumer rights under your local laws which this license cannot change. To the
//       extent permitted under your local laws, the contributors exclude the implied warranties of merchantability, fitness for a 
//       particular purpose and non-infringement.

#endregion // License

namespace ANX.Framework.Content
{
    public class ContentManager : IDisposable
    {
        private Dictionary<string, object> loadedAssets = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);
        private List<IDisposable> disposableAssets = new List<IDisposable>();
        private string rootDirectory;
        private string rootDirectoryAbsolute;
        private bool disposed;

        /// <summary>
        /// Gets the service provider associated with the ContentManager.
        /// </summary>
        public IServiceProvider ServiceProvider { get; private set; }

        /// <summary>
        /// Gets and sets the root directory associated with this ContentManager.
        /// </summary>
        public string RootDirectory 
        {
            get { return rootDirectory; }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("value");
                }
                if (this.loadedAssets.Count > 0)
                {
                    throw new InvalidOperationException("Cannot change directory with assets loaded.");
                }

                this.rootDirectory = value;
                this.rootDirectoryAbsolute = value;

                if (!Path.IsPathRooted(value))
                {
                    var location = System.Reflection.Assembly.GetExecutingAssembly().Location;
                    var assemblyFile = new FileInfo(location);
                    this.rootDirectoryAbsolute = Path.Combine(assemblyFile.Directory.FullName, this.rootDirectory);
                }
            }
        }

        /// <summary>
        /// The file extension used by the content pipeline
        /// </summary>
        public const string Extension = ".xnb";

        /// <summary>
        /// Creates a new instance of <see cref="ContentManager"/>
        /// </summary>
        /// <param name="serviceProvider">The serive provider to associate with this <see cref="ContentManager"/></param>
        public ContentManager(IServiceProvider serviceProvider)
            : this(serviceProvider, string.Empty)
        {
        }

        /// <summary>
        /// Creates a new instance of <see cref="ContentManager"/>
        /// </summary>
        /// <param name="serviceProvider">The serive provider to associate with this ContentManager</param>
        /// <param name="rootDirectory">The root to search for content.</param>
        public ContentManager(IServiceProvider serviceProvider, string rootDirectory)
        {
            if (serviceProvider == null)
            {
                throw new ArgumentNullException("serviceProvider");
            }
            if (rootDirectory == null)
            {
                throw new ArgumentNullException("rootDirectory");
            }
            this.RootDirectory = rootDirectory;
            this.ServiceProvider = serviceProvider;
        }

        /// <summary>
        /// Releases all resources used by the ContentManager class.
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Releases all resources used by the ContentManager class.
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            try
            {
                if (disposing && !disposed)
                {
                    this.Unload();
                }
            }
            finally
            {
                this.disposed = true;
                this.loadedAssets.Clear();
                this.disposableAssets.Clear();
            }
        }

        /// <summary>
        /// Disposes all data that was loaded by this ContentManager
        /// </summary>
        public virtual void Unload()
        {
            ThrowExceptionIfDisposed();

            foreach (var asset in this.disposableAssets)
            {
                try
                {
                    asset.Dispose();
                }
                catch { /* just swallow and keep going */ }
            }
            this.loadedAssets.Clear();
            this.disposableAssets.Clear();
        }

        /// <summary>
        /// Loads an asset that has been processed by the Content Pipeline.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="assetName"></param>
        /// <returns></returns>
        public virtual T Load<T>(string assetName)
        {
            ThrowExceptionIfDisposed();

            if (assetName == null)
            {
                throw new ArgumentNullException("assetName");
            }

            assetName = TitleContainer.GetCleanPath(assetName);

            object result;
            if (this.loadedAssets.TryGetValue(assetName, out result))
            {
                if (!(result is T))
                {
                    throw new ContentLoadException("requested asset type and loaded asset are from different type");
                }
                return (T)result;
            }

            T asset = ReadAsset<T>(assetName, null);
            this.loadedAssets.Add(assetName, asset);
            if (asset is IDisposable)
            {
                disposableAssets.Add(asset as IDisposable);
            }
            return asset;
        }

        /// <summary>
        /// Low-level worker method that reads asset data.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="assetName"></param>
        /// <param name="recordDisposableObject"></param>
        /// <returns></returns>
        protected T ReadAsset<T>(string assetName, Action<IDisposable> recordDisposableObject)
        {
            if (assetName == null)
            {
                throw new ArgumentNullException("assetName");
            }

            using (Stream stream = this.OpenStream(assetName))
            {
                using (ContentReader reader = ContentReader.Create(this, stream, assetName))
                {
                    return reader.ReadAsset<T>();
                }
            }
        }

        /// <summary>
        /// Opens a stream for reading the specified asset.
        /// </summary>
        /// <param name="assetName"></param>
        /// <returns></returns>
        protected virtual Stream OpenStream(string assetName)
        {
            try
            {
                string path = Path.Combine(rootDirectoryAbsolute, assetName + Extension);
                return new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);
            }
            catch (Exception ex)
            {
                throw new ContentLoadException(String.Format("failed to open stream for '{0}'", assetName), ex);
            }
        }

        protected void ThrowExceptionIfDisposed()
        {
            if (disposed)
            {
                throw new ObjectDisposedException(this.ToString());
            }
        }
    }
}
