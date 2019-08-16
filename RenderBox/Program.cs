using System;
using Common.Tickable;
using GameCore;
using Games.Games;
using PhysicsCore;
using RenderCore.Render;
using SFML.System;

namespace RenderBox
{
    internal static class Program
    {
        private static void Main()
        {
            GameBoxCore gameBox = GameBoxCoreFactory.CreateGameBoxCore();
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