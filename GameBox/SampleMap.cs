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
            IEntityCreator floorCreator = BuildFloorCreator(mass, bodyType, fillColor, outlineColor, outlineThickness, floorSize);

            IEntityCreator[] entityCreators = { floorCreator };

            return entityCreators.Select(_entityCreationArgs => _entityCreationArgs.CreateEntity(_physics)).ToList();
        }

        private static IEntityCreator BuildFloorCreator(float _mass, BodyType _bodyType, Color _fillColor, Color _outlineColor,
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

    public abstract class VertexObjectCreationArgsBase
    {
        protected VertexObjectCreationArgsBase(IVertexObject _vertexObject)
        {
            VertexObject = _vertexObject;
        }

        public IVertexObject VertexObject { get; }
    }

    public interface IDrawableCreator
    {
        IPositionDrawable CreateDrawable();
    }

    public abstract class VertexObjectDrawableCreationArgsBase : VertexObjectCreationArgsBase, IDrawableCreator
    {
        protected VertexObjectDrawableCreationArgsBase(IVertexObject _vertexObject) : base(_vertexObject)
        {
        }

        public abstract IPositionDrawable CreateDrawable();
    }

    public class VertexObjectShapeCreationArgs : VertexObjectDrawableCreationArgsBase
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

        public override IPositionDrawable CreateDrawable()
        {
            ConvexShape shape = ShapeFactory.GetConvexShape(VertexObject);
            shape.OutlineThickness = OutlineThickness;
            shape.FillColor = FillColor;
            shape.OutlineColor = OutlineColor;

            Drawable<ConvexShape> shapeDrawable = new Drawable<ConvexShape>(shape);
            return shapeDrawable;
        }
    }

    public interface IBodyCreator
    {
        IBody CreateBody(IPhysics _physics);
    }

    public class VertexObjectBodyCreationArgs : VertexObjectCreationArgsBase, IBodyCreator
    {
        public VertexObjectBodyCreationArgs(IVertexObject _vertexObject, float _mass, BodyType _bodyType) : base(_vertexObject)
        {
            Mass = _mass;
            BodyType = _bodyType;
        }
        public IBody CreateBody(IPhysics _physics)
        {
            IBody body = _physics.CreateVertexBody(VertexObject, Vector2.Zero, Mass, BodyType);
            return body;
        }

        public float Mass { get; }
        public BodyType BodyType { get; }
    }

    public interface IEntityCreator
    {
        IEntity CreateEntity(IPhysics _physics);
    }

    public class VertexObjectEntityCreationArgs : IEntityCreator
    {
        public VertexObjectEntityCreationArgs(IBodyCreator _bodyCreator, IDrawableCreator _drawableCreator)
        {
            BodyCreator = _bodyCreator;
            DrawableCreator = _drawableCreator;
        }
        
        public IEntity CreateEntity(IPhysics _physics)
        {
            IBody body = BodyCreator.CreateBody(_physics);
            IPositionDrawable drawable = DrawableCreator.CreateDrawable();

            Entity entity = new Entity(drawable, body);
            return entity;
        }

        public IBodyCreator BodyCreator { get; }
        public IDrawableCreator DrawableCreator { get; }
    }
}