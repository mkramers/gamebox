using System.Collections.Generic;
using System.Numerics;
using Aether.Physics2D.Dynamics;
using RenderCore;
using SFML.System;
using SFML.Window;

namespace GameBox
{
    public class GameBox : Game
    {
        public GameBox(string _windowTitle, Vector2u _windowSize) : base(_windowTitle, _windowSize)
        {
        }

        public void CreateMainCharacter()
        {
            const float mass = 0.1f;
            IEntity man = EntityFactory.CreateEntity(mass, -5 * Vector2.UnitY, m_entityPhysics, ResourceId.MAN,
                BodyType.Dynamic);

            AddEntity(man);

            IEnumerable<IEntity> woodEntities = CreateLandscape();

            foreach (IEntity woodEntity in woodEntities)
            {
                AddEntity(woodEntity);
            }

            Dictionary<Keyboard.Key, IKeyCommand> moveCommands = KeyCommandsFactory.GetMovementCommands(man, 2f);
            KeyHandler moveExecutor = KeyHandlerFactory.CreateKeyHandler(moveCommands);

            m_keyHandlers.Add(moveExecutor);
        }

        private IEnumerable<IEntity> CreateLandscape()
        {
            const int range = 20;

            IEnumerable<Vector2> positions = GetPyramid(new Vector2(-10, 5), range);

            List<IEntity> entities = new List<IEntity>();

            foreach (Vector2 pyramidPosition in positions)
            {
                IEntity wood =
                    EntityFactory.CreateEntity(1, pyramidPosition, m_entityPhysics, ResourceId.WOOD, BodyType.Static);

                entities.Add(wood);
            }

            return entities;
        }

        private static IEnumerable<Vector2> GetPyramid(Vector2 _position, int _size)
        {
            List<Vector2> positions = new List<Vector2>();

            for (int i = 0; i < _size; i++)
            {
                int height = i + 1;
                for (int j = 0; j < height; j++)
                {
                    Vector2 position = _position + new Vector2(i, -j);
                    positions.Add(position);
                }
            }

            return positions;
        }
    }
}