﻿using System;
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
            //IGame game = CreateMultiGame();
            IGame game = new Game2();
            //IGame game = new Game3();
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

        private static void RunGame(IGame _game)
        {
            IGameBox gameBox = new GameBoxCore();
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