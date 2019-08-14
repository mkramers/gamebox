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
            MultiGame multiGame = new MultiGame(game, gameBox.GetPhysics(), gameBox.GetGui());

            multiGame.PauseGame += (_sender, _args) => gameBox.SetIsPaused(true);
            multiGame.ResumeGame += (_sender, _args) => gameBox.SetIsPaused(false);

            gameBox.AddDrawableProvider(multiGame);
            gameBox.AddTickableProvider(multiGame);
            gameBox.SetViewProvider(multiGame);

            gameBox.StartLoop();
            gameBox.Dispose();
        }
    }
}