using System;
using GameCore;

namespace GameBox
{
    public class GameRunner
    {
        private readonly IGameBox m_gameBox;

        public GameRunner(IGameBox _gameBox)
        {
            m_gameBox = _gameBox;
        }

        public void RunGame(IGame _game)
        {
            m_gameBox.AddFpsWidget();

            _game.PauseGame += GameOnPauseGame;
            _game.ResumeGame += GameOnResumeGame;

            m_gameBox.AddTickableProvider(_game);
            m_gameBox.AddWidgetProvider(_game);
            m_gameBox.AddBodyProvider(_game);
            m_gameBox.SetTextureProvider(_game);

            m_gameBox.StartLoop();
            m_gameBox.Dispose();
        }

        private void GameOnResumeGame(object _sender, EventArgs _e)
        {
            m_gameBox.SetIsPaused(false);
        }

        private void GameOnPauseGame(object _sender, EventArgs _e)
        {
            m_gameBox.SetIsPaused(true);
        }
    }
}