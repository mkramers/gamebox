using System;
using System.IO.Abstractions;

namespace RenderCore.Resource
{
    public class ResourceManagerFactory<T> where T : Enum
    {        
        public ResourceManager<T> Create(string _rootDirectory)
        {
            IFileSystem fileSystem = new FileSystem();

            IPathConverter<T> bitmapPathConverter = new PathFromEnumPathConverter<T>(_rootDirectory, ".png", fileSystem);
            BitmapResourceLoaderFactory bitmapResourceLoaderFactory = new BitmapResourceLoaderFactory();
            BitmapResourceManager<T> bitmapResourceManager  = new BitmapResourceManager<T>(bitmapResourceLoaderFactory, bitmapPathConverter);

            IPathConverter<T> texturePathConverter = new PathFromEnumPathConverter<T>(_rootDirectory, ".png", fileSystem);
            TextureResourceLoaderFactory textureResourceLoaderFactory = new TextureResourceLoaderFactory();
            TextureResourceManager<T> textureResourceManager = new TextureResourceManager<T>(textureResourceLoaderFactory, texturePathConverter);

            ResourceManager<T> resourceManager = new ResourceManager<T>(textureResourceManager, bitmapResourceManager);
            return resourceManager;
        }
    }
}