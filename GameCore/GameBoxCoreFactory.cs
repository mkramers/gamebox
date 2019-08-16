using System;
using Common.Tickable;
using PhysicsCore;
using RenderCore.Render;
using SFML.System;

namespace GameCore
{
    public static class GameBoxCoreFactory
    {
        public static GameBoxCore CreateGameBoxCore()
        {
            GameRenderWindow gameRenderWindow = GameRenderWindowFactory.CreateGameRenderWindow(new Vector2u(800, 800));
            TickLoop tickLoop = new TickLoop(TimeSpan.FromMilliseconds(30));
            IPhysics physics = new Physics(0, 5.5f);

            GameBoxCore gameBoxCore = new GameBoxCore(gameRenderWindow, tickLoop, physics);
            return gameBoxCore;
        }
    }
}