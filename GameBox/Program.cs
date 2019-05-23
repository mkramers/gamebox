using System.Numerics;
using SFML.System;

namespace GameBox
{
    internal static class Program
    {
        private static void Main()
        {
            const string windowTitle = "GameBox";
            Vector2u windowSize = new Vector2u(600, 600);

            Vector2 gravity = new Vector2(0, 9);
            GameBox gameBox = new GameBox(windowTitle, windowSize, gravity);
            gameBox.StartLoop();
            gameBox.Dispose();
        }
    }
}