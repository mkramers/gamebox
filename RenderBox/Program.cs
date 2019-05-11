using SFML.System;

namespace RenderBox
{
    internal static class Program
    {
        private static void Main(string[] _args)
        {
            RenderBox renderBox = new RenderBox("RenderBox", new Vector2u(800, 800));
        }
    }
}