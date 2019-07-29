using System.Numerics;
using SFML.System;

namespace GameBox
{
    internal static class Program
    {
        private static void Main()
        {
            const string windowTitle = "GameBox";
            Vector2u windowSize = new Vector2u(800, 800);
            const float aspectRatio = 1.0f;

            Vector2 gravity = new Vector2(0, 9);
            GameRunnerBox gameRunnerBox = new GameRunnerBox(windowTitle, windowSize, gravity, aspectRatio);
            gameRunnerBox.StartLoop();
            gameRunnerBox.Dispose();
        }
    }
}