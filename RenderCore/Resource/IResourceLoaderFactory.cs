namespace RenderCore.Resource
{
    public interface IResourceLoaderFactory<out T>
    {
        IResourceLoader<T> CreateResourceLoader(string _resourcePath);
    }
}