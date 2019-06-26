using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using Aether.Physics2D.Dynamics;
using Common.Geometry;
using Common.VertexObject;
using GameCore.Entity;
using GameResources;
using LibExtensions;
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
        public MultiDrawable<Shape> PointDrawable { get; }

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
            List<Shape> pointShapes = new List<Shape>();
            foreach (IVertexObject bodyVertexObject in bodyVertexObjects)
            {
                for (int i = 0; i < bodyVertexObject.Count; i++)
                {
                    const float thickness = 0.33f;

                    Vector2 offset = mapPosition + new Vector2(0.5f, 0.5f);
                    Vector2 start = bodyVertexObject[i] + offset;

                    RectangleShape pointShape = new RectangleShape((1.5f * thickness * Vector2.One).GetVector2F())
                    {
                        FillColor = Color.Red,
                        Position = start.GetVector2F(),
                    };
                    pointShapes.Add(pointShape);

                    if (i + 1 >= bodyVertexObject.Count)
                    {
                        continue;
                    }

                    Vector2 end = bodyVertexObject[(i + 1) % bodyVertexObject.Count] + offset;

                    LineSegment lineSegment = new LineSegment(start, end);
                    RectangleShape lineShape = ShapeFactory.GetLineShape(
                        lineSegment, thickness);

                    lineShapes.Add(lineShape);
                }
            }

            LineDrawable = new MultiDrawable<Shape>(lineShapes);
            PointDrawable = new MultiDrawable<Shape>(pointShapes);

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