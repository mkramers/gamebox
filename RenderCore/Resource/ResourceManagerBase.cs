using System;
using System.Diagnostics;
using Common.Cache;
using IOUtilities;

namespace RenderCore.Resource
{
    public class ResourceManagerBase<TType, TValue> : IResourceManager<TType, TValue> where TType : Enum
    {
        private readonly IPathConverter<TType> m_pathConverter;
        private readonly Cache<Resource<TValue>, TType> m_cache;
        private readonly Func<string, TValue> m_resourceLoader;

        public ResourceManagerBase(IPathConverter<TType> _pathConverter, Func<string, TValue> _resourceLoader)
        {
            m_pathConverter = _pathConverter;
            m_resourceLoader = _resourceLoader;

            m_cache = new Cache<Resource<TValue>, TType>();
        }
        
        public Resource<TValue> GetResource(TType _id)
        {
            Resource<TValue> resource;

            if (!m_cache.EntryExists(_id))
            {
                string path = m_pathConverter.GetPath(_id);
                
                resource = new Resource<TValue>(m_resourceLoader, path);

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