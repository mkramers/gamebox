using System;
using System.Collections.Generic;
using System.Diagnostics;
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

        public ResourceManager(string _resourceRootDirectory)
        {
            m_textureResourceManager = new TextureResourceManager<T>(_resourceRootDirectory);
        }

        public Resource<Texture> GetTextureResource(T _id)
        {
            return m_textureResourceManager.GetTextureResource(_id);
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

    public class Resource<T>
    {
        private readonly ResourceLoader<T> m_resourceLoader;

        public Resource(ResourceLoader<T> _resourceLoader)
        {
            m_resourceLoader = _resourceLoader;
        }

        public T Load()
        {
            return m_resourceLoader.Load();
        }
    }

    public class TextureFileLoader : ResourceLoader<Texture>
    {
        private readonly string m_textureFilePath;

        public TextureFileLoader(string _textureFilePath)
        {
            m_textureFilePath = _textureFilePath;
        }

        public override Texture Load()
        {
            return new Texture(m_textureFilePath);
        }
    }

    public interface IResourceLoadArgs
    {
    }

    public class TextureLoadArgs : IResourceLoadArgs
    {

    }

    public abstract class ResourceLoader<T>
    {
        public abstract T Load();
    }
}

