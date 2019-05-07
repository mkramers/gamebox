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
        public GameBox(string _windowTitle, Vector2u _windowSize) : base(_windowTitle, _windowSize)
        {
            IPhysics physics = Physics;

            const float mass = 0.1f;
            const float force = 0.666f;

            IEntity manEntity = EntityFactory.CreateEntity(mass, -5 * Vector2.UnitY, physics, ResourceId.MAN,
                BodyType.Dynamic);

            AddEntity(manEntity);

            SampleMap map = new SampleMap();
            foreach (IEntity woodEntity in map.GetEntities(physics))
            {
                AddEntity(woodEntity);
            }

            KeyHandler moveExecutor = KeyHandlerFactory.CreateEntityKeyHandler(manEntity, force);

            AddKeyHandler(moveExecutor);

            ViewController viewController = new EntityCenterFollowerViewController(new Vector2(20, 20), manEntity);

            RenderCoreWindow.SetViewController(viewController);
        }
    }
}