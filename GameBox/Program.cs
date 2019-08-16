using System;
using System.Collections.Generic;
using System.Reflection;
using Common.ReflectionUtilities;
using GameCore;
using Games.Games;
using RenderCore.Render;
using RenderCore.ViewProvider;

namespace GameBox
{
    internal static class Program
    {
        private static void Main()
        {
            ISceneProvider sceneProvider = new SceneProvider(400, 400, new ViewProviderBase());
            //IGame game = CreateMultiGame(sceneProvider);
            IGame game = new Game2(sceneProvider);
            //IGame game = new Game3(sceneProvider);

            RunGame(game);
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

        private static void RunGame(IGame _game)
        {
            GameBoxCore gameBox = GameBoxCoreFactory.CreateGameBoxCore();
            gameBox.AddFpsWidget();

            _game.PauseGame += (_sender, _args) => gameBox.SetIsPaused(true);
            _game.ResumeGame += (_sender, _args) => gameBox.SetIsPaused(false);

            gameBox.AddTickableProvider(_game);
            gameBox.AddWidgetProvider(_game);
            gameBox.AddBodyProvider(_game);
            gameBox.SetTextureProvider(_game);

            gameBox.StartLoop();
            gameBox.Dispose();
        }
    }
}