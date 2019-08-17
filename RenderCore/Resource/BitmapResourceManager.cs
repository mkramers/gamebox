using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using Common.Cache;
using IOUtilities;

namespace RenderCore.Resource
{
    public class BitmapResourceManager<T> where T : Enum
    {
        private readonly Cache<Resource<Bitmap>, T> m_cache;
        private readonly string m_rootDirectory;

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
                string pathFromEnum = PathFromEnum<T>.GetPathFromEnum(_id, ".png");
                string bitmapFilePath = Path.Combine(m_rootDirectory, pathFromEnum);
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