﻿using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using GameCore;
using SFML.Graphics;
using SFML.System;
using TGUI;

namespace Games.Games
{
    public class MultiGame : GameBase
    {
        private readonly IEnumerable<GameBase> m_games;
        private GameBase m_currentGame;

        public MultiGame(IEnumerable<GameBase> _games)
        {
            GameBase[] games = _games as GameBase[] ?? _games.ToArray();

            m_games = games;

            const int panelWidth = 400;
            const int panelHeight = 200;
            const int buttonWidth = 100;
            const int buttonHeight = 30;

            Panel panel = new Panel(panelWidth, panelHeight)
            {
                Position = new Vector2f(50, 50)
            };

            const float xPosition = panelWidth / 2.0f - buttonWidth / 2.0f;

            for (int i = 0; i < games.Length; i++)
            {
                GameBase gameBase = games[i];

                float yPosition = panelHeight / 3.0f + i * buttonHeight;

                Button button = new Button(gameBase.GetType().Name)
                {
                    Size = new Vector2f(buttonWidth, buttonHeight),
                    Position = new Vector2f(xPosition, yPosition)
                };

                button.Clicked += (_sender, _args) => SetCurrentGame(gameBase);
                panel.Add(button);
            }

            const float clearButtonY = panelHeight - 1.5f * buttonHeight;
            Button clearButton = new Button("Clear")
            {
                Size = new Vector2f(buttonWidth, buttonHeight),
                Position = new Vector2f(xPosition, clearButtonY)
            };
            clearButton.Clicked += (_sender, _args) => SetCurrentGame(null);
            panel.Add(clearButton);

            m_widgets.Add(panel);

            SetCurrentGame(m_games.First());
        }

        private void SetCurrentGame(GameBase _game)
        {
            if (m_currentGame != null)
            {
                RemoveGameProvider(m_currentGame);
            }

            m_currentGame = _game;

            if (m_currentGame != null)
            {
                Debug.Assert(m_games.Contains(_game));
                AddGameProvider(m_currentGame);
            }
        }

        public override View GetView()
        {
            return m_currentGame?.GetView();
        }
    }
}