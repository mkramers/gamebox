using GameCore;
using Games.Games;

namespace GameBox
{
    internal static class Program
    {
        private static void Main()
        {
            GameCore.GameBox gameBox = new GameCore.GameBox();
            gameBox.AddFpsWidget();

            Game2 game = new Game2(gameBox.GetPhysics(), gameBox.GetGui());

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