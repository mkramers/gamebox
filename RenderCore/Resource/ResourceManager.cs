using System;
using System.Drawing;
using System.IO.Abstractions;
using SFML.Graphics;

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

    /// <summary>
    ///     https://gamedev.stackexchange.com/a/2246/31371
    /// </summary>
    public class ResourceManager<T> where T : Enum
    {
        private readonly BitmapResourceManager<T> m_bitmapResourceManager;
        private readonly TextureResourceManager<T> m_textureResourceManager;

        public ResourceManager(TextureResourceManager<T> _textureResourceManager, BitmapResourceManager<T> _bitmapResourceManager)
        {
            m_textureResourceManager = _textureResourceManager;
            m_bitmapResourceManager = _bitmapResourceManager;
        }

        public Resource<Texture> GetTextureResource(T _id)
        {
            return m_textureResourceManager.GetResource(_id);
        }

        public Resource<Bitmap> GetBitmapResource(T _id)
        {
            return m_bitmapResourceManager.GetResource(_id);
        }
    }
}