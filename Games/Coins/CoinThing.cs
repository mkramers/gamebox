using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Aether.Physics2D.Dynamics;
using GameCore;
using GameCore.Entity;
using PhysicsCore;
using RenderCore.Drawable;
using SFML.Graphics;
using TGUI;

namespace Games.Coins
{
    public class CoinThing : IGameModule, IDrawableProvider
    {
        private readonly IEntity m_captureEntity;
        private readonly List<Coin> m_coins;
        private readonly List<IDrawable> m_drawables;
        private readonly Gui m_gui;
        private readonly Label m_scoreLabel;
        private float m_score;

        public CoinThing(IEntity _captureEntity, IEnumerable<Coin> _coins, Gui _gui)
        {
            m_captureEntity = _captureEntity;
            m_gui = _gui;

            m_coins = new List<Coin>();
            m_drawables = new List<IDrawable>();

            IEnumerable<Coin> coins = _coins as Coin[] ?? _coins.ToArray();
            foreach (Coin coin in coins)
            {
                IEntity entity = coin.Entity;

                entity.Collided += EntityOnCollided;
                entity.Separated += EntityOnSeparated;

                m_drawables.Add(entity);

                m_coins.Add(coin);
            }

            m_scoreLabel = new Label();
            m_scoreLabel.SetPosition(new Layout2d(10, 10));
            m_scoreLabel.Renderer.TextColor = Color.White;
            m_gui.Add(m_scoreLabel);

            UpdateScoreLabel(m_score);
        }

        public IEnumerable<IDrawable> GetDrawables()
        {
            return m_drawables;
        }

        public void Tick(TimeSpan _elapsed)
        {
            foreach (Coin coin in m_coins)
            {
                coin.Entity.Tick(_elapsed);
            }
        }

        public event EventHandler PauseGame;
        public event EventHandler ResumeGame;

        private void EntityOnCollided(object _sender, CollisionEventArgs _e)
        {
            Fixture coinFixture = _e.Sender;

            Coin coin = m_coins.FirstOrDefault(_coin => _coin.Entity.ContainsFixture(coinFixture));
            if (coin == null)
            {
                return;
            }

            bool collidedWithCaptureEntity = m_captureEntity.ContainsFixture(_e.Other);
            if (!collidedWithCaptureEntity)
            {
                return;
            }

            m_score += coin.Value;

            UpdateScoreLabel(m_score);

            m_coins.Remove(coin);
            m_drawables.Remove(coin.Entity);

            if (m_score > 0)
            {
                ShowWinScreen();
            }
        }

        private void UpdateScoreLabel(float _score)
        {
            m_scoreLabel.Text = $"Score: {_score}";
        }

        private void ShowWinScreen()
        {
            PauseGame?.Invoke(this, EventArgs.Empty);

            ChildWindow childWindow = new ChildWindow("Winner!");
            childWindow.SetSize(new Layout2d(300, 100));
            childWindow.SetPosition(new Layout2d(50, 50));
            childWindow.Closed += WinScreenOnClosed;

            m_gui.Add(childWindow);
        }

        private void WinScreenOnClosed(object _sender, EventArgs _e)
        {
            ChildWindow childWindow = _sender as ChildWindow;
            Debug.Assert(childWindow != null);

            m_gui.Remove(childWindow);

            ResumeGame?.Invoke(this, EventArgs.Empty);
        }

        private static void EntityOnSeparated(object _sender, SeparationEventArgs _e)
        {
        }
    }
}