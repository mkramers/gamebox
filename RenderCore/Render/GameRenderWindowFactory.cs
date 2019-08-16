using SFML.System;

namespace RenderCore.Render
{
    public static class GameRenderWindowFactory
    {
        public static GameRenderWindow CreateGameRenderWindow(Vector2u _windowSize)
        {
            SFML.Graphics.RenderWindow window = RenderWindowFactory.CreateRenderWindow("", _windowSize);
            GuiWrapper gui = new GuiWrapper(window);

            RenderWindow renderWindow = new RenderWindow(window);

            GameRenderWindow gameRenderWindow = new GameRenderWindow(renderWindow, gui, _windowSize);
            return gameRenderWindow;
        }
    }
}