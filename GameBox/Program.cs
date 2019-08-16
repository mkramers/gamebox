using System;
using System.Collections.Generic;
using System.Reflection;
using Common.ReflectionUtilities;
using GameCore;
using Games.Games;
using RenderCore.Render;

namespace GameBox
{
    internal static class Program
    {
        private static void Main()
        {
            IGameBox gameBox = GameBoxCoreFactory.CreateGameBoxCore();

            ISceneProvider sceneProvider = SceneProviderFactory.CreateSceneProvider();
            IGame game = CreateMultiGame(sceneProvider);
            //IGame game = new Game2(sceneProvider);
            //IGame game = new Game3(sceneProvider);

            GameRunner gameRunner = new GameRunner(gameBox);
            gameRunner.RunGame(game);
        }

        private static MultiGame CreateMultiGame(ISceneProvider _sceneProvider)
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

            MultiGame multiGame = new MultiGame(games, _sceneProvider);
            return multiGame;
        }
    }
}