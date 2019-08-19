using System;
using System.Diagnostics;
using System.IO;
using System.IO.Abstractions;
using Common.Cache;
using IOUtilities;
using SFML.Graphics;

namespace RenderCore.Resource
{
    public class TextureResourceManager<T> where T : Enum
    {
        private readonly Cache<Resource<Texture>, T> m_cache;
        private readonly string m_rootDirectory;
        private readonly IFileSystem m_fileSystem;

        public TextureResourceManager(string _rootDirectory) : this(_rootDirectory, new FileSystem())
        {
        }

        public TextureResourceManager(string _rootDirectory, IFileSystem _fileSystem)
        {
            m_rootDirectory = _rootDirectory;
            m_fileSystem = _fileSystem;
            m_cache = new Cache<Resource<Texture>, T>();
        }

        public Resource<Texture> GetTextureResource(T _id)
        {
            Resource<Texture> resource;

            if (!m_cache.EntryExists(_id))
            {
                PathFromEnum<T> pathFromEnum = new PathFromEnum<T>(m_fileSystem);
                string path = pathFromEnum.GetPathFromEnum(_id, ".png");
                string textureFilePath = Path.Combine(m_rootDirectory, path);
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
}