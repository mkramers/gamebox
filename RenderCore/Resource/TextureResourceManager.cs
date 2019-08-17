using System;
using System.Diagnostics;
using System.IO;
using Common.Cache;
using IOUtilities;
using SFML.Graphics;

namespace RenderCore.Resource
{
    public class TextureResourceManager<T> where T : Enum
    {
        private readonly Cache<Resource<Texture>, T> m_cache;
        private readonly string m_rootDirectory;

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
                string pathFromEnum = PathFromEnum<T>.GetPathFromEnum(_id, ".png");
                string textureFilePath = Path.Combine(m_rootDirectory, pathFromEnum);
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