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
            
            IPathConverter<T> pathConverter = new PathFromEnumPathConverter<T>(_rootDirectory, ".png", fileSystem.Path);

            ResourceManagerBase<T, Bitmap> bitmapResourceManager  = new ResourceManagerBase<T, Bitmap>(pathConverter, _path => new Bitmap(_path));
            
            ResourceManagerBase<T, Texture> textureResourceManager = new ResourceManagerBase<T, Texture>(pathConverter, _path => new Texture(_path));

            ResourceManager<T> resourceManager = new ResourceManager<T>(textureResourceManager, bitmapResourceManager);
            return resourceManager;
        }
    }
}