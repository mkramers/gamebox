using System;
using System.Collections.Generic;
using System.Numerics;
using BepuPhysics;
using BepuPhysics.Collidables;
using BepuUtilities.Memory;
using RenderCore.Physics;

namespace RenderCore
{
    public class EntityPhysics : Physics2
    {
        private readonly List<IEntity> m_entities;

        public EntityPhysics(BufferPool _bufferPool) : base(_bufferPool)
        {
            m_entities = new List<IEntity>();
        }

        public override void Tick(long _elapsedMs)
        {
            base.Tick(_elapsedMs);

            foreach (IEntity entity in m_entities)
            {
                entity.Tick(_elapsedMs);
            }
        }

        public void Add(IDynamicEntity _dynamicEntity)
        {
            m_entities.Add(_dynamicEntity);
        }
    }

    public class Physics2 : ITickable, IDisposable
    {
        private readonly Simulation m_simulation;

        public Physics2(BufferPool _bufferPool)
        {
            Vector3 gravity = new Vector3(0, 10, 0);

            m_simulation = Simulation.Create(_bufferPool, new NarrowPhaseCallbacks(),
                new PoseIntegratorCallbacks(gravity));
        }

        public virtual void Tick(long _elapsedMs)
        {
            //m_simulation.Timestep(_elapsedMs / 100.0f);
            m_simulation.Timestep(0.001f);
        }

        public void Dispose()
        {
            m_simulation?.Dispose();
        }

        public IDynamicBody CreateDynamicBody(float _mass)
        {
            Sphere sphere = new Sphere(0.5f);
            sphere.ComputeInertia(1, out BodyInertia sphereInertia);

            Vector3 position = new Vector3(0, -5, 0);

            TypedIndex shapeIndex = m_simulation.Shapes.Add(sphere);

            CollidableDescription collidableDescription = new CollidableDescription(shapeIndex, 0.1f);
            BodyActivityDescription bodyActivityDescription = new BodyActivityDescription(0.01f);

            BodyDescription bodyDescription = BodyDescription.CreateDynamic(position, sphereInertia,
                collidableDescription, bodyActivityDescription);

            int bodyIndex = m_simulation.Bodies.Add(bodyDescription);

            DynamicBody dynamicBody = new DynamicBody(shapeIndex, bodyIndex, m_simulation);
            return dynamicBody;
        }

        public IStaticBody CreateStaticBody(Vector3 _position)
        {
            Box staticBox = new Box(1, 1, 1);
            TypedIndex boxShapeReference = m_simulation.Shapes.Add(staticBox);
            CollidableDescription staticCollidable = new CollidableDescription(boxShapeReference, 0.1f);
            StaticDescription staticDescription = new StaticDescription(_position, staticCollidable);

            int handle = m_simulation.Statics.Add(staticDescription);

            StaticBody staticBody = new StaticBody(handle, m_simulation);
            return staticBody;
        }
    }
}