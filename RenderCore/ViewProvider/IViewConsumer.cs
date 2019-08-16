using SFML.Graphics;

namespace RenderCore.ViewProvider
{
    public interface IViewConsumer
    {
        void SetView(View _view);
    }
}