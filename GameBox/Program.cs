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
            IGameBox gameBox = new GameBoxCore();
            gameBox.AddFpsWidget();
            
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