using System;
using Common.Tickable;
using GameCore;
using Games.Games;
using RenderCore.Render;
using SFML.System;

namespace RenderBox
{
    internal static class Program
    {
        private static void Main()
        {
            GameRenderWindow gameRenderWindow = GameRenderWindowFactory.CreateGameRenderWindow(new Vector2u(800, 800));
            TickLoop tickLoop = new TickLoop(TimeSpan.FromMilliseconds(30));
            IGameBox gameBox = new GameBoxCore(gameRenderWindow, tickLoop);
            gameBox.AddFpsWidget();

            Game3 game = new Game3();

            game.PauseGame += (_sender, _args) => gameBox.SetIsPaused(true);
            game.ResumeGame += (_sender, _args) => gameBox.SetIsPaused(false);

            gameBox.AddTickableProvider(game);

            gameBox.StartLoop();
            gameBox.Dispose();
        }
    }
}