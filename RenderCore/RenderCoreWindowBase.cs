using System;
using System.Diagnostics;
using SFML.Graphics;
using SFML.Window;

namespace RenderCore
{
    public abstract class RenderCoreWindowBase : IRenderCoreWindow
    {
        private readonly RenderWindow m_renderWindow;
        private readonly RenderCoreWindowKeyboardHandler m_coreWindowInputHandler;

        protected RenderCoreWindowBase(RenderWindow _renderWindow)
        {
            m_renderWindow = _renderWindow;
            m_renderWindow.Closed += RenderWindowOnClosed;

            //m_coreWindowInputHandler = new RenderCoreWindowKeyboardHandler(m_renderWindow);
        }

        private void RenderWindowOnClosed(object _sender, EventArgs _e)
        {
            RenderWindow window = _sender as RenderWindow;
            Debug.Assert(window != null);

            window.Close();
        }

        public void StartRenderLoop()
        {
            while (m_renderWindow.IsOpen)
            {
                m_renderWindow.DispatchEvents();

                DrawScene(m_renderWindow);
            }
        }

        public abstract void DrawScene(RenderWindow _renderWindow);
    }

    public interface IRenderCoreWindowInputHandler
    {
    }

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

    public class KeyTranslator : RenderCoreWindowKeyboardHandler
    {
        public KeyTranslator(Window _renderWindow) : base(_renderWindow)
        {
        }

        public override void KeyPressed(KeyEventArgs _e)
        {
            
        }
    }
}