using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.IO.Abstractions;
using System.Linq;
using Common.Cache;
using GameResources;
using IOUtilities;
using SFML.Graphics;

namespace ResourceUtilities.Aseprite
{
    /// <summary>
    /// https://gamedev.stackexchange.com/a/2246/31371
    /// </summary>
    public class ResourceManager<T> where T : Enum
    {
        private readonly TextureResourceManager<T> m_textureResourceManager;
        private readonly BitmapResourceManager<T> m_bitmapResourceManager;

        public ResourceManager(string _resourceRootDirectory)
        {
            m_textureResourceManager = new TextureResourceManager<T>(_resourceRootDirectory);
            m_bitmapResourceManager = new BitmapResourceManager<T>(_resourceRootDirectory);
        }

        public Resource<Texture> GetTextureResource(T _id)
        {
            return m_textureResourceManager.GetTextureResource(_id);
        }
        public Resource<Bitmap> GetBitmapResource(T _id)
        {
            return m_bitmapResourceManager.GetBitmapResource(_id);
        }
    }

    public class TextureResourceManager<T> where T : Enum
    {
        private readonly string m_rootDirectory;
        private readonly Cache<Resource<Texture>, T> m_cache;

        public TextureResourceManager(string _rootDirectory)
        {
            m_rootDirectory = _rootDirectory;
            m_cache = new Cache<Resource<Texture>, T>();
        }

        public Resource<Texture> GetTextureResource(T _id)
        {
            Resource<Texture> resource;

            if (!m_cache.EntryExists(_id))
            {
                string textureFilePath = PathFromEnum<T>.GetPathFromEnum(_id, m_rootDirectory, ".png");
                TextureFileLoader fileLoader = new TextureFileLoader(textureFilePath);
                resource = new Resource<Texture>(fileLoader);

                m_cache.AddObject(_id, resource);
            }
            else
            {
                resource = m_cache.GetObject(_id);
                Debug.Assert(resource != null);
            }


            return resource;
        }
    }

    public class BitmapResourceManager<T> where T : Enum
    {
        private readonly string m_rootDirectory;
        private readonly Cache<Resource<Bitmap>, T> m_cache;

        public BitmapResourceManager(string _rootDirectory)
        {
            m_rootDirectory = _rootDirectory;
            m_cache = new Cache<Resource<Bitmap>, T>();
        }

        public Resource<Bitmap> GetBitmapResource(T _id)
        {
            Resource<Bitmap> resource;

            if (!m_cache.EntryExists(_id))
            {
                string textureFilePath = PathFromEnum<T>.GetPathFromEnum(_id, m_rootDirectory, ".png");
                BitmapFileLoader fileLoader = new BitmapFileLoader(textureFilePath);
                resource = new Resource<Bitmap>(fileLoader);

                m_cache.AddObject(_id, resource);
            }
            else
            {
                resource = m_cache.GetObject(_id);
                Debug.Assert(resource != null);
            }


            return resource;
        }
    }


    public class Resource<T>
    {
        private readonly IResourceLoader<T> m_resourceLoader;

        public Resource(IResourceLoader<T> _resourceLoader)
        {
            m_resourceLoader = _resourceLoader;
        }

        public T Load()
        {
            return m_resourceLoader.Load();
        }
    }

    public class TextureFileLoader : IResourceLoader<Texture>
    {
        private readonly string m_textureFilePath;

        public TextureFileLoader(string _textureFilePath)
        {
            m_textureFilePath = _textureFilePath;
        }

        public Texture Load()
        {
            return new Texture(m_textureFilePath);
        }
    }

    public class BitmapFileLoader : IResourceLoader<Bitmap>
    {
        private readonly string m_bitmapFilePath;

        public BitmapFileLoader(string _bitmapFilePath)
        {
            m_bitmapFilePath = _bitmapFilePath;
        }

        public Bitmap Load()
        {
            return new Bitmap(m_bitmapFilePath);
        }
    }

    public interface IResourceLoader<out T>
    {
        T Load();
    }
}

