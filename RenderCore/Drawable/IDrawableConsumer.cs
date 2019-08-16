namespace RenderCore.Drawable
{
    public interface IDrawableConsumer
    {
        void AddDrawableProvider(IDrawableProvider _drawableProvider);
        void RemoveDrawableProvider(IDrawableProvider _provider);
    }
}