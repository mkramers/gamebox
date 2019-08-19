using System;
using System.Drawing;
using System.IO.Abstractions;
using IOUtilities;
using SFML.Graphics;

namespace RenderCore.Resource
{
    public class ResourceManagerFactory<T> where T : Enum
    {        
        public ResourceManager<T> Create(string _rootDirectory)
        {
            IFileSystem fileSystem = new FileSystem();

            IPathConverter<T> bitmapPathConverter = new PathFromEnumPathConverter<T>(_rootDirectory, ".png", fileSystem.Path);
            BitmapResourceLoaderFactory bitmapResourceLoaderFactory = new BitmapResourceLoaderFactory();
            ResourceManagerBase<T, Bitmap> bitmapResourceManager  = new ResourceManagerBase<T, Bitmap>(bitmapResourceLoaderFactory, bitmapPathConverter);

            IPathConverter<T> texturePathConverter = new PathFromEnumPathConverter<T>(_rootDirectory, ".png", fileSystem.Path);
            TextureResourceLoaderFactory textureResourceLoaderFactory = new TextureResourceLoaderFactory();
            ResourceManagerBase<T, Texture> textureResourceManager = new ResourceManagerBase<T, Texture>(textureResourceLoaderFactory, texturePathConverter);

            ResourceManager<T> resourceManager = new ResourceManager<T>(textureResourceManager, bitmapResourceManager);
            return resourceManager;
        }
    }
}