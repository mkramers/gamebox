namespace RenderCore.Resource
{
    public interface IResourceLoaderFactory<out T>
    {
        IResourceLoader<T> CreateResourceLoader(string _resourcePath);
    }

    public abstract class ResourceLoaderBase<T> : IResourceLoader<T>
    {
        protected readonly string m_resourcePath;

        protected ResourceLoaderBase(string _resourcePath)
        {
            m_resourcePath = _resourcePath;
        }

        public abstract T Load();
    }
}