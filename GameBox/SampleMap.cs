using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using Aether.Physics2D.Dynamics;
using RenderCore;
using SFML.Graphics;

namespace GameBox
{
    public class SampleMap : IMap
    {
        public IEnumerable<IEntity> GetEntities(IPhysics _physics, Vector2 _position)
        {
            const float mass = 1.0f;
            const BodyType bodyType = BodyType.Static;
            const float outlineThickness = 0.05f;
            Color outlineColor = Color.Black;
            Color fillColor = Color.Red;

            Vector2 floorSize = new Vector2(10, 0.5f);
            VertexObjectEntityCreationArgs floor = CreateFloor(mass, bodyType, fillColor, outlineColor, outlineThickness, floorSize);

            VertexObjectEntityCreationArgs[] items = {floor};

            return items.Select(_entityCreationArgs => _entityCreationArgs.CreateEntity(_physics)).ToList();
        }

        private static VertexObjectEntityCreationArgs CreateFloor(float _mass, BodyType _bodyType, Color _fillColor, Color _outlineColor,
            float _outlineThickness, Vector2 _size)
        {
            Polygon floor = ShapeFactory.CreateRectangle(_size / 2);
            VertexObjectBodyCreationArgs bodyCreationArgs = new VertexObjectBodyCreationArgs(floor, _mass, _bodyType);
            VertexObjectShapeCreationArgs shapeCreationArgs =
                new VertexObjectShapeCreationArgs(floor, _fillColor, _outlineColor, _outlineThickness);
            VertexObjectEntityCreationArgs entityCreationArgs =
                new VertexObjectEntityCreationArgs(bodyCreationArgs, shapeCreationArgs);
            return entityCreationArgs;
        }
    }

    public static class VertexObjectShapeCreationArgsExtensions
    {
        public static ConvexShape CreateConvexShape(this VertexObjectShapeCreationArgs _creationArgs)
        {
            ConvexShape shape = ShapeFactory.GetConvexShape(_creationArgs.VertexObject);
            shape.OutlineThickness = _creationArgs.OutlineThickness;
            shape.FillColor = _creationArgs.FillColor;
            shape.OutlineColor = _creationArgs.OutlineColor;
            return shape;
        }
    }

    public static class VertexObjectBodyCreationArgsExtensions
    {
        public static IBody CreateBody(this VertexObjectBodyCreationArgs _creationArgs, IPhysics _physics)
        {
            IBody body = _physics.CreateVertexBody(_creationArgs.VertexObject, Vector2.Zero, _creationArgs.Mass, _creationArgs.BodyType);
            return body;
        }
    }

    public abstract class VertexObjectCreationArgsBase
    {
        protected VertexObjectCreationArgsBase(IVertexObject _vertexObject)
        {
            VertexObject = _vertexObject;
        }

        public IVertexObject VertexObject { get; }
    }

    public class VertexObjectShapeCreationArgs : VertexObjectCreationArgsBase
    {
        public VertexObjectShapeCreationArgs(IVertexObject _vertexObject, Color _fillColor, Color _outlineColor, float _outlineThickness) : base(_vertexObject)
        {
            FillColor = _fillColor;
            OutlineColor = _outlineColor;
            OutlineThickness = _outlineThickness;
        }

        public Color FillColor { get; }
        public Color OutlineColor { get; }
        public float OutlineThickness { get; }
    }
    public class VertexObjectBodyCreationArgs : VertexObjectCreationArgsBase
    {
        public VertexObjectBodyCreationArgs(IVertexObject _vertexObject, float _mass, BodyType _bodyType) : base(_vertexObject)
        {
            Mass = _mass;
            BodyType = _bodyType;
        }

        public float Mass { get; }
        public BodyType BodyType { get; }
    }

    public static class VertexObjectEntityCreationArgsExtensions
    {
        public static IEntity CreateEntity(this VertexObjectEntityCreationArgs _creationArgs, IPhysics _physics)
        {
            IBody body = _creationArgs.BodyCreationArgs.CreateBody(_physics);
            ConvexShape shape = _creationArgs.ShapeCreationArgs.CreateConvexShape();

            Drawable<ConvexShape> shapeDrawable = new Drawable<ConvexShape>(shape);
            Entity entity = new Entity(shapeDrawable, body);
            return entity;
        }
    }

    public class VertexObjectEntityCreationArgs
    {
        public VertexObjectEntityCreationArgs(VertexObjectBodyCreationArgs _bodyCreationArgs, VertexObjectShapeCreationArgs _shapeCreationArgs)
        {
            BodyCreationArgs = _bodyCreationArgs;
            ShapeCreationArgs = _shapeCreationArgs;
        }

        public VertexObjectBodyCreationArgs BodyCreationArgs { get; }
        public VertexObjectShapeCreationArgs ShapeCreationArgs { get; }
    }
}