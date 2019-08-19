using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.IO.Abstractions;
using Common.Cache;
using IOUtilities;

namespace RenderCore.Resource
{
    public class BitmapResourceManager<T> where T : Enum
    {
        private readonly Cache<Resource<Bitmap>, T> m_cache;
        private readonly string m_rootDirectory;
        private readonly IFileSystem m_fileSystem;

        public BitmapResourceManager(string _rootDirectory) : this(_rootDirectory, new FileSystem())
        {
        }

        public BitmapResourceManager(string _rootDirectory, IFileSystem _fileSystem)
        {
            m_rootDirectory = _rootDirectory;
            m_fileSystem = _fileSystem;
            m_cache = new Cache<Resource<Bitmap>, T>();
        }

        public Resource<Bitmap> GetBitmapResource(T _id)
        {
            Resource<Bitmap> resource;

            if (!m_cache.EntryExists(_id))
            {
                PathFromEnum<T> pathFromEnum = new PathFromEnum<T>(m_fileSystem);
                string path = pathFromEnum.GetPathFromEnum(_id, ".png");
                string bitmapFilePath = Path.Combine(m_rootDirectory, path);
                BitmapFileLoader fileLoader = new BitmapFileLoader(bitmapFilePath);
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
}