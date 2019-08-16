using RenderCore.ViewProvider;

namespace RenderCore.Render
{
    public static class SceneProviderFactory
    {
        public static ISceneProvider CreateSceneProvider()
        {
            IRenderTexture renderTexture = new RenderTexture(400, 400);
            ISceneProvider sceneProvider = new SceneProvider(renderTexture, new ViewProviderBase());
            return sceneProvider;
        }
    }
}