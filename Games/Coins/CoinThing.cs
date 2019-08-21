using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using Aether.Physics2D.Dynamics;
using Common.Extensions;
using Common.Tickable;
using GameCore;
using GameCore.Entity;
using PhysicsCore;
using RenderCore.Drawable;
using RenderCore.Widget;
using SFML.Graphics;
using TGUI;

namespace Games.Coins
{
    public class CoinThing : IGameProvider
    {
        private readonly IEntity m_captureEntity;
        private readonly List<Coin> m_coins;
        private readonly Label m_scoreLabel;
        private readonly List<IGuiWidget> m_widgets;
        private GuiWidget m_childWindowWidget;
        private float m_score;

        public CoinThing(IEntity _captureEntity, IEnumerable<Coin> _coins)
        {
            m_widgets = new List<IGuiWidget>();

            m_captureEntity = _captureEntity;

            m_coins = new List<Coin>();

            IEnumerable<Coin> coins = _coins as Coin[] ?? _coins.ToArray();
            foreach (Coin coin in coins)
            {
                IEntity entity = coin.Entity;

                entity.Collided += EntityOnCollided;
                entity.Separated += EntityOnSeparated;

                m_coins.Add(coin);
            }

            m_scoreLabel = new Label();
            m_scoreLabel.Renderer.TextColor = Color.White;

            GuiWidget widget = new GuiWidget(m_scoreLabel, new Vector2(0.02f, 0.02f));
            m_widgets.Add(widget);

            UpdateScoreLabel(m_score);
        }

        public IEnumerable<IDrawable> GetDrawables()
        {
            return m_coins.Select(_coin => _coin.Entity);
        }

        public event EventHandler PauseGame;
        public event EventHandler ResumeGame;

        public IEnumerable<ITickable> GetTickables()
        {
            return m_coins.Select(_coin => _coin.Entity);
        }

        public void Dispose()
        {
            m_captureEntity.Dispose();
            m_coins.DisposeAllAndClear();
            m_widgets.DisposeAllAndClear();
            m_scoreLabel.Dispose();
            m_childWindowWidget?.Dispose();
        }

        public IEnumerable<IGuiWidget> GetWidgets()
        {
            return m_widgets;
        }

        public IEnumerable<IBody> GetBodies()
        {
            return m_coins.Select(_coin => _coin.Entity);
        }

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
            childWindow.Closed += WinScreenOnClosed;

            Debug.Assert(m_childWindowWidget == null);
            m_childWindowWidget = new GuiWidget(childWindow, new Vector2(0.5f, 0.5f));
            m_widgets.Add(m_childWindowWidget);
        }

        private void WinScreenOnClosed(object _sender, EventArgs _e)
        {
            ChildWindow childWindow = _sender as ChildWindow;
            Debug.Assert(childWindow != null);
            Debug.Assert(childWindow == m_childWindowWidget.GetWidget());

            m_widgets.Remove(m_childWindowWidget);
            m_childWindowWidget.Dispose();
            m_childWindowWidget = null;

            ResumeGame?.Invoke(this, EventArgs.Empty);
        }

        private static void EntityOnSeparated(object _sender, SeparationEventArgs _e)
        {
        }
    }
}