﻿using System.Numerics;
using Aether.Physics2D.Dynamics;

namespace RenderCore
{
    public class VertexObjectBodyCreationArgs : VertexObjectCreationArgsBase, IBodyCreator
    {
        public VertexObjectBodyCreationArgs(IVertexObject _vertexObject, float _mass, BodyType _bodyType,
            Vector2 _position) : base(_vertexObject)
        {
            Mass = _mass;
            BodyType = _bodyType;
            Position = _position;
        }

        private float Mass { get; }
        private BodyType BodyType { get; }
        private Vector2 Position { get; }

        public IBody CreateBody(IPhysics _physics)
        {
            IBody body = _physics.CreateVertexBody(VertexObject, Position, Mass, BodyType);
            return body;
        }
    }
}