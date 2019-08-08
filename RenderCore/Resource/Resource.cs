namespace RenderCore.Resource
{
    public class Resource<T>
    {
        private readonly IResourceLoader<T> m_resourceLoader;

        public Resource(IResourceLoader<T> _resourceLoader)
        {
            m_resourceLoader = _resourceLoader;
        }

        public T Load()
        {
            return m_resourceLoader.Load();
        }
    }
}