using Common.Tickable;
using RenderCore.Drawable;
using RenderCore.ViewProvider;

namespace RenderCore.Render
{
    public interface IRenderCoreTarget : IDrawable, ITickable, IRenderObjectContainer
    {
        void SetViewProvider(IViewProvider _viewProvider);
    }
}