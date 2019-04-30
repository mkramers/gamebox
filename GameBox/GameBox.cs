using System.Collections.Generic;
using System.Numerics;
using System.Windows.Input;
using RenderCore;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace GameBox
{
    public class GameBox
    {
        public static void Run()
        {
            const string windowTitle = "GameBox";
            Vector2u windowSize = new Vector2u(600, 600);
            
            Game game = new Game(windowTitle, windowSize);
            game.StartLoop();
        }
    }
}