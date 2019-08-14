using GameCore;
using Games.Games;

namespace RenderBox
{
    internal static class Program
    {
        private static void Main()
        {
            IGameBox gameBox = new GameBoxCore();
            gameBox.AddFpsWidget();

            Game3 game = new Game3();

            game.PauseGame += (_sender, _args) => gameBox.SetIsPaused(true);
            game.ResumeGame += (_sender, _args) => gameBox.SetIsPaused(false);

            gameBox.AddDrawableProvider(game);
            gameBox.AddTickableProvider(game);
            gameBox.SetViewProvider(game);

            gameBox.StartLoop();
            gameBox.Dispose();
        }
    }
}