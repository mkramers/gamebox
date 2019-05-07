using SFML.System;

namespace AetherBox
{
    internal static class Program
    {
        private static void Main()
        {
            const string windowTitle = "AetherBox";
            Vector2u windowSize = new Vector2u(600, 600);

            AetherBox gameBox = new AetherBox(windowTitle, windowSize);
            gameBox.StartLoop();
            gameBox.Dispose();
        }
    }
}
