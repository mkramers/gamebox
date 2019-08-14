using GameCore;
using Games.Games;

namespace RenderBox
{
    internal static class Program
    {
        private static void Main()
        {
            GameBox gameBox = new GameBox();

            Game3 game = new Game3(gameBox.GetPhysics());

            gameBox.AddDrawableProvider(game);
            gameBox.AddTickableProvider(game);
            gameBox.SetViewProvider(game);

            gameBox.StartLoop();
            gameBox.Dispose();
        }
    }
}