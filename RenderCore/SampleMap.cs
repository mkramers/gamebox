using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using Aether.Physics2D.Dynamics;
using Newtonsoft.Json;
using SFML.Graphics;

namespace RenderCore
{
    public class SampleMap : IMap
    {
        public IEnumerable<IEntity> GetEntities(IPhysics _physics)
        {
            const float mass = 1.0f;
            const BodyType bodyType = BodyType.Static;
            const float outlineThickness = 0.0f;
            Color outlineColor = Color.Black;
            Color fillColor = Color.Red;

            Vector2 floorSize = new Vector2(20, 1f);
            Vector2 floorPosition = new Vector2(-floorSize.X / 2, 0);

            IEntityCreator floorCreator = BuildFloorCreator(mass, bodyType, fillColor, outlineColor, outlineThickness,
                floorSize, floorPosition);

            IEntityCreator[] entityCreators = {floorCreator};

            JsonSerializerSettings settings = new JsonSerializerSettings
                {TypeNameHandling = TypeNameHandling.Auto, Formatting = Formatting.Indented};
            string serializedMap = JsonConvert.SerializeObject(entityCreators, settings);
            File.WriteAllText("sample-map.json", serializedMap);

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