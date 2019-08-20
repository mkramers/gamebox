using System;

namespace RenderCore.Resource
{
    public class ResourceLoader<TValue> : IResourceLoader<TValue>
    {
        private readonly Func<string, TValue> m_loader;

        public ResourceLoader(Func<string, TValue> _loader)
        {
            m_loader = _loader;
        }

        public TValue Load(string _path)
        {
            return m_loader(_path);
        }
    }

    public interface IResourceLoader<out T>
    {
        T Load(string _path);
    }
}