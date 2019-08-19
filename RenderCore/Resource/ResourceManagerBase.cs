using System;
using System.Diagnostics;
using Common.Cache;
using IOUtilities;

namespace RenderCore.Resource
{
    public class ResourceManagerBase<TType, TValue> : IResourceManager<TType, TValue> where TType : Enum
    {
        private readonly IResourceLoaderFactory<TValue> m_resourceLoaderFactory;
        private readonly IPathConverter<TType> m_pathConverter;
        private readonly Cache<Resource<TValue>, TType> m_cache;

        public ResourceManagerBase(IResourceLoaderFactory<TValue> _resourceLoaderFactory, IPathConverter<TType> _pathConverter)
        {
            m_resourceLoaderFactory = _resourceLoaderFactory;
            m_pathConverter = _pathConverter;
            m_cache = new Cache<Resource<TValue>, TType>();
        }

        public Resource<TValue> GetResource(TType _id)
        {
            Resource<TValue> resource;

            if (!m_cache.EntryExists(_id))
            {
                string path = m_pathConverter.GetPath(_id);

                IResourceLoader<TValue> resourceLoader = m_resourceLoaderFactory.CreateResourceLoader(path);
                resource = new Resource<TValue>(resourceLoader);

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