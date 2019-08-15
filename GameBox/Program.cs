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

        private static void RunGame(GameBase _multiGame)
        {
            IGameBox gameBox = new GameBoxCore();
            gameBox.AddFpsWidget();

            _multiGame.PauseGame += (_sender, _args) => gameBox.SetIsPaused(true);
            _multiGame.ResumeGame += (_sender, _args) => gameBox.SetIsPaused(false);

            gameBox.AddDrawableProvider(_multiGame);
            gameBox.AddTickableProvider(_multiGame);
            gameBox.AddWidgetProvider(_multiGame);
            gameBox.AddBodyProvider(_multiGame);
            gameBox.SetViewProvider(_multiGame);

            gameBox.StartLoop();
            gameBox.Dispose();
        }
    }
}