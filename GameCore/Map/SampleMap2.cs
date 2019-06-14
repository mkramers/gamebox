using System.Collections.Generic;
using System.Numerics;
using Aether.Physics2D.Dynamics;
using GameCore.Entity;
using PhysicsCore;
using RenderCore.TextureCache;
using ResourceUtilities.Aseprite;
using SFML.Graphics;

namespace GameCore.Map
{
    public class SampleMap2 : IMap
    {
        private readonly List<IEntity> m_entities;

        public SampleMap2(string _mapFilePath, IPhysics _physics)
        {
            SpriteSheetFile spriteSheet = SpriteSheetFileLoader.LoadFromFile(_mapFilePath);

            MapFileLoader loader = new MapFileLoader();
            GameResources.Map map = loader.LoadMapFromFile(spriteSheet);

            Texture texture = TextureFileCache.Instance.GetTextureFromFile(map.SceneLayer.FileName);

            Sprite sprite = new Sprite(texture);

            //IVertexObject bodyVertexObject = map.GetCollisionVertexObject();

            IEntity entity =
                SpriteEntityFactory.CreateSpriteEntity(0, -8 * Vector2.One, _physics, BodyType.Static, sprite);

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
}