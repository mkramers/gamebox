using Games.Games;

namespace RenderBox
{
    internal static class Program
    {
        private static void Main()
        {
            Game3 renderBox = new Game3();
            renderBox.StartLoop();
            renderBox.Dispose();
        }
    }
}