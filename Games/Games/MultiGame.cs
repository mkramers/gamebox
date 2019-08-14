using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using GameCore;
using PhysicsCore;
using SFML.Graphics;

namespace Games.Games
{
    public class MultiGame : GameBase
    {
        private readonly IEnumerable<GameBase> m_games;
        private GameBase m_currentGame;

        public MultiGame(IEnumerable<GameBase> _games, IPhysics _physics) : base(_physics)
        {
            m_games = _games;

            SetCurrentGame(m_games.First());
        }

        private void SetCurrentGame(GameBase _game)
        {
            Debug.Assert(m_games.Contains(_game));

            if (m_currentGame != null)
            {
                RemoveGameProvider(m_currentGame);
            }

            m_currentGame = _game;

            AddGameProvider(m_currentGame);
        }

        public override View GetView()
        {
            return m_currentGame?.GetView();
        }
    }
}