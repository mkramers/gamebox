using System;

namespace RenderCore.Resource
{
    public class Resource<T>
    {
        private readonly Func<string, T> m_resourceLoader;
        private readonly string m_path;

        public Resource(Func<string, T> _resourceLoader, string _path)
        {
            m_resourceLoader = _resourceLoader;
            m_path = _path;
        }

        public T Load()
        {
            return m_resourceLoader(m_path);
        }
    }
}