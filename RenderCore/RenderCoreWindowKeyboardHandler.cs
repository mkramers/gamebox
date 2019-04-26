using SFML.Window;

namespace RenderCore
{
    public abstract class RenderCoreWindowKeyboardHandler : IRenderCoreWindowInputHandler
    {
        protected RenderCoreWindowKeyboardHandler(Window _renderWindow)
        {
            _renderWindow.KeyPressed += OnKeyPressed;
        }

        private void OnKeyPressed(object _sender, KeyEventArgs _e)
        {
            KeyPressed(_e);
        }

        public abstract void KeyPressed(KeyEventArgs _e);
    }
}