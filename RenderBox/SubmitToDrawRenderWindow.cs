using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using Common.Extensions;
using RenderCore.Drawable;
using RenderCore.Render;
using SFML.Graphics;
using SFML.System;

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

    public interface IDrawableProvider
    {
        IEnumerable<IDrawable> GetDrawables();
    }

    public class SceneManager : IDrawableProvider
    {
        public SceneManager()
        {
        }

        public IEnumerable<IDrawable> GetDrawables()
        {
            return new IDrawable[]{ };
        }
    }

    public class SubmitToDrawRenderBox
    {
        private readonly SubmitToDrawRenderWindow m_submitToDrawRenderWindow;

        public SubmitToDrawRenderBox()
        {
            SceneManager sceneManager = new SceneManager();

            m_submitToDrawRenderWindow = new SubmitToDrawRenderWindow();
            m_submitToDrawRenderWindow.AddDrawableProvider(sceneManager);
        }

        public void StartLoop()
        {
            m_submitToDrawRenderWindow.StartLoop();
        }
    }

    public class SubmitToDrawRenderWindow
    {
        private readonly RenderWindow m_renderWindow;
        private readonly Scene m_scene;

        public SubmitToDrawRenderWindow()
        {
            m_renderWindow = RenderWindowFactory.CreateRenderWindow("", new Vector2u(800, 800));
            m_scene = new Scene();
        }
        
        public void AddDrawableProvider(IDrawableProvider _provider)
        {
            m_scene.AddDrawableProvider(_provider);
        }

        public void StartLoop()
        {
            Stopwatch stopwatch = Stopwatch.StartNew();

            while (m_renderWindow.IsOpen)
            {
                TimeSpan elapsed = stopwatch.GetElapsedAndRestart();
                
                m_renderWindow.DispatchEvents();
                
                m_renderWindow.Clear();

                m_scene.Draw(m_renderWindow, RenderStates.Default);

                m_renderWindow.Display();

                DelayLoop();
            }
        }

        private static void DelayLoop()
        {
            Thread.Sleep(30);
        }
    }
}