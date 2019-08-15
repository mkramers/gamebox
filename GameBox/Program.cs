using System;
using System.Collections.Generic;
using System.Numerics;
using System.Reflection;
using Common.ReflectionUtilities;
using GameCore;
using Games.Games;

namespace GameBox
{
    internal static class Program
    {
        private static void Main()
        {
            //MultiGame game = CreateMultiGame();

            Game2 game = new Game2();
            RunGame(game);
        }

        private static MultiGame CreateMultiGame()
        {
            List<GameBase> games = new List<GameBase>();
            Assembly executingAssembly = Assembly.Load("Games");
            List<Type> gameTypes = ReflectionUtilities.FindAllDerivedTypes<GameBase>(executingAssembly);
            foreach (Type gameType in gameTypes)
            {
                if (gameType == typeof(MultiGame))
                {
                    continue;
                }

                GameBase game = Activator.CreateInstance(gameType) as GameBase;
                games.Add(game);
            }

            MultiGame multiGame = new MultiGame(games);
            return multiGame;
        }

        private static void RunGame(GameBase _game)
        {
            IGameBox gameBox = new GameBoxCore();
            gameBox.AddFpsWidget();

            _game.PauseGame += (_sender, _args) => gameBox.SetIsPaused(true);
            _game.ResumeGame += (_sender, _args) => gameBox.SetIsPaused(false);

            //gameBox.AddDrawableProvider(_game);
            gameBox.AddTickableProvider(_game);
            gameBox.AddWidgetProvider(_game);
            gameBox.AddBodyProvider(_game);
            gameBox.SetViewProvider(_game);
            gameBox.SetTextureProvider(_game);

            gameBox.StartLoop();
            gameBox.Dispose();
        }
    }
}