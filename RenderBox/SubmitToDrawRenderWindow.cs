﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using Common.Extensions;
using Common.Tickable;
using RenderCore.Drawable;
using RenderCore.Render;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using TGUI;

namespace RenderBox
{
    public class Scene
    {
        private readonly List<IDrawableProvider> m_drawableProviders;

        public Scene()
        {
            m_drawableProviders = new List<IDrawableProvider>();
        }

        public void AddDrawableProvider(IDrawableProvider _provider)
        {
            m_drawableProviders.Add(_provider);
        }

        public void Draw(RenderTarget _target, RenderStates _states)
        {
            IEnumerable<IDrawable> drawables = m_drawableProviders.SelectMany(_provider => _provider.GetDrawables());
            foreach (IDrawable drawable in drawables)
            {
                _target.Draw(drawable, _states);
            }
        }
    }

    public class GuiManager
    {
        private readonly Gui m_gui;

        public GuiManager(Gui _gui)
        {
            m_gui = _gui;
        }

        public void AddWidget(Widget _guiWidget)
        {
            m_gui.Add(_guiWidget);
        }

        public void RemoveWidget(Widget _guiWidget)
        {
            m_gui.Remove(_guiWidget);
        }
    }

    public interface IDrawableProvider
    {
        IEnumerable<IDrawable> GetDrawables();
    }

    public class GameBox2
    {
        private readonly SubmitToDrawRenderWindow m_submitToDrawRenderWindow;
        private readonly TickLoop m_tickLoop;

        public GameBox2()
        {
            m_submitToDrawRenderWindow = new SubmitToDrawRenderWindow(1.0f, new Vector2u(800, 800));
            m_submitToDrawRenderWindow.Closed += (_sender, _e) => m_tickLoop.StopLoop();

            m_tickLoop = new TickLoop(TimeSpan.FromMilliseconds(30));
            m_tickLoop.Tick += OnTick;
        }

        private void OnTick(object _sender, TimeElapsedEventArgs _e)
        {
            m_submitToDrawRenderWindow.Tick(_e.Elapsed);
        }

        public void StartLoop()
        {
            m_tickLoop.StartLoop();
        }
    }

    public class TickLoop
    {
        private bool m_isRunning;
        private readonly Stopwatch m_stopwatch;

        public TickLoop(TimeSpan _interval)
        {
            m_stopwatch = Stopwatch.StartNew();
            m_isRunning = false;
        }

        public void StartLoop()
        {
            m_isRunning = true;
            while (m_isRunning)
            {
                TimeSpan elapsed = m_stopwatch.GetElapsedAndRestart();

                Tick?.Invoke(this, new TimeElapsedEventArgs(elapsed));

                Thread.Sleep(30);
            }
        }

        public void StopLoop()
        {
            m_isRunning = false;
        }

        public event EventHandler<TimeElapsedEventArgs> Tick;
    }

    public class TimeElapsedEventArgs : EventArgs
    {
        public TimeElapsedEventArgs(TimeSpan _elapsed)
        {
            Elapsed = _elapsed;
        }

        public TimeSpan Elapsed { get; }
    }

    public static class WindowResizer
    {
        public static FloatRect Resize(Vector2u _windowSize, float _aspectRatio)
        {
            float windowAspectRatio = (float) _windowSize.X / _windowSize.Y;
            if (windowAspectRatio <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(windowAspectRatio), "negative aspect ratio not supported");
            }

            FloatRect viewPort = new FloatRect(0, 0, 1, 1);

            if (windowAspectRatio > _aspectRatio)
            {
                float xPadding = (windowAspectRatio - _aspectRatio) / 2.0f;
                viewPort = new FloatRect(xPadding / 2.0f, 0, 1 - xPadding, 1);
            }
            else if (windowAspectRatio < _aspectRatio)
            {
                float yPadding = (_aspectRatio - windowAspectRatio) / 2.0f;
                viewPort = new FloatRect(0, yPadding / 2.0f, 0, 1 - yPadding);
            }
            else if (Math.Abs(windowAspectRatio - _aspectRatio) < 0.0001f)
            {
                viewPort = new FloatRect(0, 0, 1, 1);
            }

            return viewPort;
        }
    }

    public class SubmitToDrawRenderWindow : ITickable
    {
        private readonly float m_aspectRatio;
        private readonly RenderWindow m_renderWindow;
        private readonly Scene m_scene;
        private readonly Gui m_gui;

        public SubmitToDrawRenderWindow(float _aspectRatio, Vector2u _windowSize)
        {
            m_aspectRatio = _aspectRatio;
            m_renderWindow = RenderWindowFactory.CreateRenderWindow("", _windowSize);
            m_renderWindow.Resized += (_sender, _e) => Resize(new Vector2u(_e.Width, _e.Height));

            m_scene = new Scene();

            m_gui = new Gui(m_renderWindow);

            Resize(_windowSize);
        }

        public void AddDrawableProvider(IDrawableProvider _provider)
        {
            m_scene.AddDrawableProvider(_provider);
        }

        public GuiManager CreateGuiManager()
        {
            GuiManager guiManager = new GuiManager(m_gui);
            return guiManager;
        }

        private void Resize(Vector2u _windowSize)
        {
            float aspectRatio = m_aspectRatio;

            FloatRect viewPort = WindowResizer.Resize(_windowSize, aspectRatio);

            View renderWindowView = m_renderWindow.GetView();
            renderWindowView.Viewport = viewPort;

            m_renderWindow.SetView(renderWindowView);
            m_gui.View = renderWindowView;
        }

        public void Tick(TimeSpan _elapsed)
        {
            m_renderWindow.DispatchEvents();

            m_renderWindow.Clear();

            m_scene.Draw(m_renderWindow, RenderStates.Default);

            m_gui.Draw();

            m_renderWindow.Display();
        }

        public event EventHandler Closed
        {
            add => m_renderWindow.Closed += value;
            remove => m_renderWindow.Closed -= value;
        }
    }
}