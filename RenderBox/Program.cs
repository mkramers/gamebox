using GameCore;
using Games.Games;
using RenderCore.Render;
using RenderCore.ViewProvider;

namespace RenderBox
{
    internal static class Program
    {
        private static void Main()
        {
            GameBoxCore gameBox = GameBoxCoreFactory.CreateGameBoxCore();
            gameBox.AddFpsWidget();

            IRenderTextureWrapper renderTextureWrapper = new RenderTextureWrapper(400, 400);
            ISceneProvider sceneProvider = new SceneProvider(renderTextureWrapper, new ViewProviderBase());
            Game3 game = new Game3(sceneProvider);

            game.PauseGame += (_sender, _args) => gameBox.SetIsPaused(true);
            game.ResumeGame += (_sender, _args) => gameBox.SetIsPaused(false);

            gameBox.AddTickableProvider(game);

            gameBox.StartLoop();
            gameBox.Dispose();
        }
    }
}