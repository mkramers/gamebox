using System.Numerics;
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

            Game2 game2 = new Game2();
            Game3 game3 = new Game3();

            GameBase[] games = {game2, game3};

            MultiGame multiGame = new MultiGame(games);
            multiGame.PauseGame += (_sender, _args) => gameBox.SetIsPaused(true);
            multiGame.ResumeGame += (_sender, _args) => gameBox.SetIsPaused(false);

            gameBox.AddDrawableProvider(multiGame);
            gameBox.AddTickableProvider(multiGame);
            gameBox.AddWidgetProvider(multiGame);
            gameBox.AddBodyProvider(multiGame);
            gameBox.SetViewProvider(multiGame);

            gameBox.StartLoop();
            gameBox.Dispose();
        }
    }
}