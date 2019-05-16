using SFML.Graphics;

namespace RenderCore
{
    public interface IRenderCoreViewWidget : IRenderCoreWidget
    {
        void SetView(View _view);
    }

    public interface IRenderCoreWidget : IDrawable, ITickable
    {
    }
}