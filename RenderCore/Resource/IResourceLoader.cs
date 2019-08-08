namespace RenderCore.Resource
{
    public interface IResourceLoader<out T>
    {
        T Load();
    }
}