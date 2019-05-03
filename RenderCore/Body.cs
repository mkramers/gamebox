﻿using System.Numerics;
using Aether.Physics2D.Dynamics;

namespace RenderCore
{
    public class Body : IBody
    {
        private readonly Aether.Physics2D.Dynamics.Body m_body;

        public Body(Aether.Physics2D.Dynamics.Body _body)
        {
            m_body = _body;
        }

        public Vector2 GetPosition()
        {
            return m_body.Position.GetVector2();
        }

        public void ApplyForce(Vector2 _force)
        {
            m_body.ApplyForce(_force.GetVector2());
        }

        public void RemoveFromWorld()
        {
            World world = m_body.World;
            world.Remove(m_body);
        }
    }
}