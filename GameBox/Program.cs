using GameBox.Games;

namespace GameBox
{
    internal static class Program
    {
        private static void Main()
        {
            Game2 renderBox = new Game2();
            renderBox.StartLoop();
            renderBox.Dispose();
        }
    }
}