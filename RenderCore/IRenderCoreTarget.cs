namespace RenderCore
{
    public interface IRenderCoreTarget : IDrawable, ITickable, IRenderObjectContainer
    {
        void SetViewProvider(IViewProvider _viewProvider);
    }
}