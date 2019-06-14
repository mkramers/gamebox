using RenderCore.Drawable;

namespace RenderCore.Render
{
    public interface IRenderObjectContainer
    {
        void AddDrawable(IDrawable _drawable);
    }
}