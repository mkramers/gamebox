using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using Aether.Physics2D.Common;
using Aether.Physics2D.Dynamics;

namespace RenderCore
{
    public class Physics : IPhysics, IDisposable
    {
        public Physics(Vector2 _gravity)
        {
            World = new World(_gravity.GetVector2());
        }

        private World World { get; }

        public void Dispose()
        {
            World.Clear();
        }

        public void Tick(TimeSpan _elapsed)
        {
            World.Step(_elapsed);
        }

        public IBody CreateVertexBody(IVertexObject _vertexObject, Vector2 _position, float _mass, BodyType _bodyType)
        {
            Aether.Physics2D.Dynamics.Body physicsBody = World.CreatePolygon(_vertexObject.GetVertices(), _mass, _position.GetVector2(), 0, _bodyType);

            physicsBody.SetRestitution(0.3f);
            physicsBody.SetFriction(0.33f);

            World.Add(physicsBody);

            Body body = new Body(physicsBody);
            return body;
        }

        public IBody CreateBody(Vector2 _position, float _mass, BodyType _bodyType)
        {
            Aether.Physics2D.Dynamics.Body physicsBody =
                World.CreateRectangle(1, 1, _mass, _position.GetVector2(), 0, _bodyType);
            
            physicsBody.SetRestitution(0.3f);
            physicsBody.SetFriction(0.33f);

            World.Add(physicsBody);

            Body body = new Body(physicsBody);
            return body;
        }

        public void SetGravity(Vector2 _gravity)
        {
            World.Gravity = _gravity.GetVector2();
        }
    }

    public interface IVertexObject : IList<Vector2>
    {
        void Translate(Vector2 _translation);
    }

    public static class VerticesExtensions
    {
        public static Polygon GetPolygon(this Vertices _vertices)
        {
            Polygon vertices = new Polygon();

            foreach (Aether.Physics2D.Common.Maths.Vector2 vertex in _vertices)
            {
                vertices.Add(vertex.GetVector2());
            }

            return vertices;
        }
    }

    public static class VertexObjectExtensions
    {
        public static Vertices GetVertices(this IVertexObject _polygon)
        {
            Vertices vertices = new Vertices(_polygon.Count());

            foreach (Vector2 vertex in _polygon)
            {
                vertices.Add(vertex.GetVector2());
            }

            return vertices;
        }
    }

    public class Polygon : List<Vector2>, IVertexObject
    {
        public Polygon()
        {
        }

        public Polygon(IEnumerable<Vector2> _collection) : base(_collection)
        {
        }

        public Polygon(int _capacity) : base(_capacity)
        {
        }

        public void Translate(Vector2 _translation)
        {
            for (int i = 0; i < this.Count; i++)
            {
                this[i] += _translation;
            }
        }
    }
}