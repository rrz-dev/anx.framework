using System;
using System.Collections.Generic;
using System.IO;
using ANX.Framework.NonXNA;
using ANX.Framework.NonXNA.PlatformSystem;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Content
{
	public class ContentManager : IDisposable
	{
		#region Constants
		public const string Extension = ".xnb";
		#endregion

		#region Private
		private Dictionary<string, object> loadedAssets;
		private List<IDisposable> disposableAssets;
		private string rootDirectory;
		private string rootDirectoryAbsolute;
		private bool disposed;

		private INativeContentManager nativeImplementation;
		#endregion

		#region Public
		public IServiceProvider ServiceProvider
		{
			get;
			private set;
		}

		public string RootDirectory
		{
			get
			{
				return rootDirectory;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				if (this.loadedAssets.Count > 0)
				{
					throw new InvalidOperationException(
						"Cannot change directory with assets loaded.");
				}

				rootDirectory = value;
				rootDirectoryAbsolute = value;

				if (Path.IsPathRooted(value) == false)
				{
					rootDirectoryAbsolute =
						nativeImplementation.MakeRootDirectoryAbsolute(rootDirectory);
				}
			}
		}
		#endregion

		#region Constructor
		private ContentManager()
		{
			nativeImplementation = PlatformSystem.Instance.CreateContentManager();

			loadedAssets =
			 new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);
			disposableAssets = new List<IDisposable>();
		}

		public ContentManager(IServiceProvider serviceProvider)
			: this(serviceProvider, String.Empty)
		{
		}

		public ContentManager(IServiceProvider serviceProvider, string rootDirectory)
			: this()
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
		#endregion

		#region Dispose
		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposing)
		{
			try
			{
				if (disposing && disposed == false)
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
		#endregion

		#region Load
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
					throw new ContentLoadException(
						"requested asset type and loaded asset are from different type");
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
		#endregion

		#region ReadAsset
		protected T ReadAsset<T>(string assetName,
			Action<IDisposable> recordDisposableObject)
		{
			if (assetName == null)
			{
				throw new ArgumentNullException("assetName");
			}

			using (Stream stream = OpenStream(assetName))
			{
				using (ContentReader reader = ContentReader.Create(this, stream, assetName))
				{
					return reader.ReadAsset<T>();
				}
			}
		}
		#endregion

		#region OpenStream
		protected virtual Stream OpenStream(string assetName)
		{
			try
			{
				string path = Path.Combine(rootDirectoryAbsolute, assetName + Extension);
				return nativeImplementation.OpenStream(path);
			}
			catch (Exception ex)
			{
				throw new ContentLoadException(String.Format("failed to open stream for '{0}'", assetName), ex);
			}
		}
		#endregion

		#region ThrowExceptionIfDisposed
		protected void ThrowExceptionIfDisposed()
		{
			if (disposed)
			{
				throw new ObjectDisposedException(ToString());
			}
		}
		#endregion
	}
}
