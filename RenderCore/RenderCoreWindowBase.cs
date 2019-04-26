﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using SFML.Graphics;
using SFML.Window;

namespace RenderCore
{
    public abstract class RenderCoreWindowBase : IRenderCoreWindow
    {
        protected readonly RenderWindow m_renderWindow;
        private readonly List<IKeyHandler> m_keyHandlers;

        protected RenderCoreWindowBase(RenderWindow _renderWindow)
        {
            m_renderWindow = _renderWindow;
            m_renderWindow.Closed += RenderWindowOnClosed;
            m_renderWindow.KeyPressed+= OnKeyPressed;

            m_keyHandlers = new List<IKeyHandler>();
        }

        public void ClearKeyHandlers()
        {
            m_keyHandlers.Clear();
        }

        public void AddKeyHandler(IKeyHandler _keyHandler)
        {
            m_keyHandlers.Add(_keyHandler);
        }

        private void OnKeyPressed(object _sender, KeyEventArgs _e)
        {
            m_keyHandlers.ForEach(_keyHandler => _keyHandler.KeyPressed(_e));
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
}