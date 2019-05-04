﻿using System;
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
        private IEntity m_manEntity;
        private ViewController m_viewController;

        public GameBox(string _windowTitle, Vector2u _windowSize) : base(_windowTitle, _windowSize)
        {
        }

        public void CreateMainCharacter()
        {
            const float mass = 0.1f;
            m_manEntity = EntityFactory.CreateEntity(mass, -5 * Vector2.UnitY, m_entityPhysics, ResourceId.MAN,
                BodyType.Dynamic);

            AddEntity(m_manEntity);

            IEnumerable<IEntity> woodEntities = CreateLandscape();

            foreach (IEntity woodEntity in woodEntities)
            {
                AddEntity(woodEntity);
            }

            Dictionary<Keyboard.Key, IKeyCommand> moveCommands = KeyCommandsFactory.GetMovementCommands(m_manEntity, 2f);
            KeyHandler moveExecutor = KeyHandlerFactory.CreateKeyHandler(moveCommands);

            m_keyHandlers.Add(moveExecutor);

            RenderCoreWindow renderCoreWindow = GetRenderCoreWindow();
            m_viewController = new ViewController(renderCoreWindow, new Vector2(20, 20));
            m_viewControllers.Add(m_viewController);
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

        public override void Tick(TimeSpan _elapsed)
        {
            Vector2 manPosition = m_manEntity.GetPosition();

            m_viewController.SetCenter(manPosition);
        }
    }
}