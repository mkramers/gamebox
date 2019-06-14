using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using Aether.Physics2D.Dynamics;
using Common.VertexObject;
using GameResources;
using Newtonsoft.Json;
using ResourceUtilities.Aseprite;
using SFML.Graphics;

namespace RenderCore
{
    public class SampleMap2 : IMap
    {
        private readonly List<IEntity> m_entities;

        public SampleMap2(string _mapFilePath, IPhysics _physics)
        {
            SpriteSheetFileLoader spriteSheetLoader = new SpriteSheetFileLoader();
            SpriteSheetFile spriteSheet = spriteSheetLoader.LoadFromFile(_mapFilePath);

            MapFileLoader loader = new MapFileLoader();
            Map map = loader.LoadMapFromFile(spriteSheet);
            
            Texture texture = TextureFileCache.Instance.GetTextureFromFile(map.SceneLayer.FileName);

            Sprite sprite = new Sprite(texture);

            //IVertexObject bodyVertexObject = map.GetCollisionVertexObject();

            IEntity entity = SpriteEntityFactory.CreateSpriteEntity(0, -8 * Vector2.One, _physics, BodyType.Static, sprite);

            m_entities = new List<IEntity>
            {
                entity
            };
        }

        public IEnumerable<IEntity> GetEntities(IPhysics _physics)
        {
            return m_entities;
        }
    }

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

            IEntityCreator[] entityCreators = { floorCreator };

            JsonSerializerSettings settings = new JsonSerializerSettings
            { TypeNameHandling = TypeNameHandling.Auto, Formatting = Formatting.Indented };
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