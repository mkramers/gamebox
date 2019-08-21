using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using GameCore;
using RenderCore.Render;
using RenderCore.Widget;
using SFML.System;
using TGUI;

namespace Games.Games
{
    public class MultiGame : GameBase
    {
        private readonly IEnumerable<Type> m_gameTypes;
        private IGame m_currentGame;

        public MultiGame(IEnumerable<Type> _games, ISceneProvider _sceneProvider) : base(_sceneProvider)
        {
            Type[] gameTypes = _games as Type[] ?? _games.ToArray();

            m_gameTypes = gameTypes;

            const int panelWidth = 400;
            const int panelHeight = 200;
            const int buttonWidth = 100;
            const int buttonHeight = 30;

            Panel panel = new Panel(panelWidth, panelHeight)
            {
                Position = new Vector2f(50, 50)
            };

            const float xPosition = panelWidth / 2.0f - buttonWidth / 2.0f;

            for (int i = 0; i < gameTypes.Length; i++)
            {
                Type gameType = gameTypes[i];

                float yPosition = panelHeight / 3.0f + i * buttonHeight;

                Button button = new Button(gameType.Name)
                {
                    Size = new Vector2f(buttonWidth, buttonHeight),
                    Position = new Vector2f(xPosition, yPosition)
                };

                button.Clicked += (_sender, _args) => SetCurrentGame(gameType);
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

            GuiWidget panelWidget = new GuiWidget(panel, new Vector2(0.25f, 0.25f));
            AddWidget(panelWidget);

            SetCurrentGame(m_gameTypes.First());
        }

        private void SetCurrentGame(Type _gameType)
        {
            if (m_currentGame != null)
            {
                SetViewProvider(null);
                RemoveGameProvider(m_currentGame);
                m_currentGame.Dispose();
            }
            
            if (_gameType == null)
            {
                m_currentGame = null;
                return;
            }

            Debug.Assert(m_gameTypes.Contains(_gameType));

            m_currentGame = Activator.CreateInstance(_gameType) as GameBase;
            
            AddGameProvider(m_currentGame);
            SetViewProvider(m_currentGame.GetViewProvider());
        }
    }
}