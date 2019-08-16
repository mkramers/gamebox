using System;
using SFML.Graphics;
using SFML.Window;

namespace RenderCore.Render
{
    public class RenderWindow : IRenderWindow
    {
        private readonly SFML.Graphics.RenderWindow m_renderWindow;

        public RenderWindow(SFML.Graphics.RenderWindow _renderWindow)
        {
            m_renderWindow = _renderWindow;
        }

        public void Display()
        {
            m_renderWindow.Display();
        }

        public void Draw(Texture _texture, RenderStates _states)
        {
            m_renderWindow.Draw(_texture, _states);
        }

        public void Clear(Color _color)
        {
            m_renderWindow.Clear(_color);
        }

        public void DispatchEvents()
        {
            m_renderWindow.DispatchEvents();
        }

        public View GetView()
        {
            return m_renderWindow.GetView();
        }

        public void SetView(View _view)
        {
            m_renderWindow.SetView(_view);
        }

        public event EventHandler<SizeEventArgs> Resized
        {
            add => m_renderWindow.Resized += value;
            remove => m_renderWindow.Resized -= value;
        }
        public event EventHandler Closed
        {
            add => m_renderWindow.Closed += value;
            remove => m_renderWindow.Closed -= value;
        }

        public void Dispose()
        {
            m_renderWindow.Dispose();
        }
    }
}