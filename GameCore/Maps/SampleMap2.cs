using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using Aether.Physics2D.Dynamics;
using Common.Geometry;
using Common.VertexObject;
using GameCore.Entity;
using GameResources;
using PhysicsCore;
using RenderCore.Drawable;
using RenderCore.ShapeUtilities;
using RenderCore.TextureCache;
using ResourceUtilities.Aseprite;
using SFML.Graphics;

namespace GameCore.Maps
{
    public class SampleMap2 : IMap
    {
        private readonly List<IEntity> m_entities;
        public MultiDrawable<Shape> LineDrawable { get; }

        public SampleMap2(string _mapFilePath, IPhysics _physics)
        {
            SpriteSheetFile spriteSheet = SpriteSheetFileLoader.LoadFromFile(_mapFilePath);

            MapFileLoader loader = new MapFileLoader();
            Map map = loader.LoadMapFromFile(spriteSheet);

            Texture texture = TextureCache.Instance.GetTextureFromFile(map.SceneLayer.FileName);

            Sprite sprite = new Sprite(texture);

            IVertexObject[] bodyVertexObjects = map.GetCollisionVertexObjects().ToArray();

            Vector2 mapPosition = -8 * Vector2.One;

            List<Shape> lineShapes = new List<Shape>();
            foreach (IVertexObject bodyVertexObject in bodyVertexObjects)
            {
                for (int i = 0; i < bodyVertexObject.Count; i++)
                {
                    //    LineSegment lineSegment = new LineSegment(bodyVertexObject[i], bodyVertexObject[(i + 1) % bodyVertexObject.Count]);
                    //    RectangleShape lineShape = ShapeFactory.GetLineShape(
                    //        lineSegment, 0.1f);
                    const float thickness = 0.5f;
                    Vector2 pos = bodyVertexObject[i] + mapPosition + new Vector2(0.5f, 0.5f);
                    LineSegment lineSegment = new LineSegment(pos, pos + new Vector2(0, -thickness/2.0f));
                    RectangleShape lineShape = ShapeFactory.GetLineShape(
                        lineSegment, thickness);

                    lineShapes.Add(lineShape);
                }
            }

            LineDrawable = new MultiDrawable<Shape>(lineShapes);

            IEntity entity =
                SpriteEntityFactory.CreateSpriteEntity(0, mapPosition, _physics, BodyType.Static, sprite/*, bodyVertexObject.First()*/);

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