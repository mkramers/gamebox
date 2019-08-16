using SFML.Graphics;
using SFML.System;

namespace RenderCore.Render
{
    public static class GameRenderWindowFactory
    {
        public static GameRenderWindow CreateGameRenderWindow(Vector2u _windowSize)
        {
            RenderWindow renderWindow = RenderWindowFactory.CreateRenderWindow("", _windowSize);
            GuiWrapper gui = new GuiWrapper(renderWindow);

            RenderWindowWrapper renderWindowWrapper = new RenderWindowWrapper(renderWindow);

            GameRenderWindow gameRenderWindow = new GameRenderWindow(renderWindowWrapper, gui, _windowSize);
            return gameRenderWindow;
        }
    }
}