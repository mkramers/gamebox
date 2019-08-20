using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using Common.Geometry;
using Common.Grid;
using GameCore.Entity;
using GameCore.Maps;
using GameResources.Attributes;
using MarchingSquares;
using PhysicsCore;
using RenderCore.Drawable;
using SFML.Graphics;

namespace Games.Maps
{
    public class SampleMap2 : IMap
    {
        private readonly List<IDrawable> m_drawables;
        private readonly List<IEntity> m_entities;

        public SampleMap2(Texture _sceneTexture, Grid<ComparableColor> _collisionGrid)
        {
            Vector2 mapPosition = -0 * Vector2.One;

            m_drawables = new List<IDrawable>();

            Sprite sprite = new Sprite(_sceneTexture);

            ComparableColor colorThreshold = new ComparableColor(0, 0, 0, 0);
            MarchingSquaresGenerator<ComparableColor> marchingSquares =
                new MarchingSquaresGenerator<ComparableColor>(_collisionGrid, colorThreshold);

            LineSegment[] lines = marchingSquares.GetLineSegments().ToArray();

            IEntity entity = SpriteEntityFactory.CreateSpriteEdgeEntity(mapPosition, sprite, lines);

            m_entities = new List<IEntity>
            {
                entity
            };

            //debug
            MultiDrawable<VertexArrayShape> lineShapes = CreateLineSegmentsDrawable(lines, mapPosition);
            m_drawables.Add(lineShapes);

            LineSegment floorLineSegment = new LineSegment(-100, 0, 100, 0);
            IBody edgeBody = BodyFactory.CreateEdges(new[] {floorLineSegment}, mapPosition - -20 * Vector2.UnitY);

            Entity edgeEntity = new Entity(null, edgeBody);
            m_entities.Add(edgeEntity);
        }

        public IEnumerable<IEntity> GetEntities()
        {
            return m_entities;
        }

        public IEnumerable<IDrawable> GetDrawables()
        {
            return m_drawables;
        }

        private static MultiDrawable<VertexArrayShape> CreateShapesFromVertexObjects(
            IEnumerable<IVertexObject> _vertexObjects, Vector2 _position)
        {
            IEnumerable<VertexArrayShape> lineShapes = _vertexObjects.Select(_vertexObject =>
                VertexArrayShape.Factory.CreateLineStripShape(_vertexObject, Color.Cyan));

            MultiDrawable<VertexArrayShape> drawable = new MultiDrawable<VertexArrayShape>(lineShapes);
            drawable.SetPosition(_position);

            return drawable;
        }

        private static MultiDrawable<VertexArrayShape> CreateLineSegmentsDrawable(
            IEnumerable<LineSegment> _lineSegments, Vector2 _position)
        {
            LineSegment[] lineSegments = _lineSegments as LineSegment[] ?? _lineSegments.ToArray();
            IEnumerable<VertexArrayShape> shapes = lineSegments.Select(_lineSegment =>
                VertexArrayShape.Factory.CreateLinesShape(_lineSegment, Color.Yellow));

            MultiDrawable<VertexArrayShape> drawable = new MultiDrawable<VertexArrayShape>(shapes);
            drawable.SetPosition(_position);
            return drawable;
        }
    }
}