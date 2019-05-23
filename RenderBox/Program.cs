using SFML.System;

namespace RenderBox
{
    internal static class Program
    {
        private static void Main()
        {
            RenderBox renderBox = new RenderBox("RenderBox", new Vector2u(800, 800));
            renderBox.StartLoop();
        }
    }
}