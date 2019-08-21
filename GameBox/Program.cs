using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Common.Extensions;
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
            IEnumerable<Type> gameTypes = executingAssembly.FindAllDerivedTypes<GameBase>().Where(_gameType => _gameType != typeof(MultiGame));

            MultiGame multiGame = new MultiGame(gameTypes, _sceneProvider);
            return multiGame;
        }
    }
}