using RenderCore.Drawable;
using RenderCore.ViewProvider;

namespace GameCore
{
    public interface IGame : IGameProvider, ITextureProvider, IViewProviderProvider, IViewProviderConsumer
    {
    }
}