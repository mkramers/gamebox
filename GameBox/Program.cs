using SFML.System;

namespace GameBox
{
    internal class Program
    {
        private static void Main()
        {
            const string windowTitle = "GameBox";
            Vector2u windowSize = new Vector2u(600, 600);

            GameBox gameBox = new GameBox(windowTitle, windowSize);
            gameBox.CreateMainCharacter();
            gameBox.StartLoop();
        }
    }
}