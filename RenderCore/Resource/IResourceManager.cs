using System;
using System.Diagnostics;
using System.IO.Abstractions;
using Common.Cache;
using IOUtilities;

namespace RenderCore.Resource
{
    public interface IResourceManager<in TType, TValue> where TType : Enum
    {
        Resource<TValue> GetResource(TType _id);
    }

    public interface IPathConverter<in T> where T : Enum
    {
        string GetPath(T _id);
    }

    public class PathFromEnumPathConverter<T> : IPathConverter<T> where T : Enum
    {
        private readonly string m_rootDirectory;
        private readonly string m_fileExtension;
        private readonly IFileSystem m_fileSystem;

        public PathFromEnumPathConverter(string _rootDirectory, string _fileExtension, IFileSystem _fileSystem)
        {
            m_rootDirectory = _rootDirectory;
            m_fileSystem = _fileSystem;
            m_fileExtension = _fileExtension;
        }
        public string GetPath(T _id)
        {
            PathFromEnum<T> pathFromEnum = new PathFromEnum<T>();

            string fileName = pathFromEnum.GetPathFromEnum(_id);
            string fullFileName = m_fileSystem.Path.ChangeExtension(fileName, m_fileExtension);
            return m_fileSystem.Path.Combine(m_rootDirectory, fullFileName);
        }
    }

    public abstract class ResourceManagerBase<TType, TValue> : IResourceManager<TType, TValue> where TType : Enum
    {
        private readonly IResourceLoaderFactory<TValue> m_resourceLoaderFactory;
        private readonly IPathConverter<TType> m_pathConverter;
        private readonly Cache<Resource<TValue>, TType> m_cache;

        protected ResourceManagerBase(IResourceLoaderFactory<TValue> _resourceLoaderFactory, IPathConverter<TType> _pathConverter)
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