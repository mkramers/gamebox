using SFML.Graphics;

namespace RenderCore.ViewProvider
{
    public interface IViewConsumer
    {
        void SetView(View _view);
    }

    public interface IViewProviderConsumer
    {
        void SetViewProvider(IViewProvider _viewProvider);
    }
}