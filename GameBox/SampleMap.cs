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
            const float outlineThickness = 0.0f;
            Color outlineColor = Color.Black;
            Color fillColor = Color.Red;

            Vector2 floorSize = new Vector2(10, 0.5f);
            Vector2 floorPosition = -floorSize / 2;

            IEntityCreator floorCreator = BuildFloorCreator(mass, bodyType, fillColor, outlineColor, outlineThickness,
                floorSize, floorPosition);

            IEntityCreator[] entityCreators = {floorCreator};

            return entityCreators.Select(_entityCreationArgs => _entityCreationArgs.CreateEntity(_physics)).ToList();
        }

        private static IEntityCreator BuildFloorCreator(float _mass, BodyType _bodyType, Color _fillColor,
            Color _outlineColor,
            float _outlineThickness, Vector2 _size, Vector2 _position)
        {
            IVertexObject floor = ShapeFactory.CreateRectangle(_size / 2);

            VertexObjectBodyCreationArgs bodyCreationArgs =
                new VertexObjectBodyCreationArgs(floor, _mass, _bodyType, _position + _size / 2);

            VertexObjectShapeCreationArgs shapeCreationArgs =
                new VertexObjectShapeCreationArgs(floor, _fillColor, _outlineColor, _outlineThickness);

            VertexObjectEntityCreationArgs entityCreationArgs =
                new VertexObjectEntityCreationArgs(bodyCreationArgs, shapeCreationArgs);

            return entityCreationArgs;
        }
    }
}