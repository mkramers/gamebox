﻿using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using Common.Geometry;
using Common.Grid;
using Common.VertexObject;
using GameCore.Entity;
using GameResources;
using GameResources.Attributes;
using MarchingSquares;
using PhysicsCore;
using RenderCore.Drawable;
using RenderCore.TextureCache;
using ResourceUtilities.Aseprite;
using SFML.Graphics;

namespace GameCore.Maps
{
    public class SampleMap2 : IMap
    {
        private readonly List<IDrawable> m_drawables;
        private readonly List<IEntity> m_entities;

        public SampleMap2(string _mapFilePath, IPhysics _physics)
        {
            Vector2 mapPosition = -0 * Vector2.One;

            m_drawables = new List<IDrawable>();

            SpriteSheetFile spriteSheet = SpriteSheetFileLoader.LoadFromFile(_mapFilePath);

            MapFileLoader loader = new MapFileLoader();
            Map map = loader.LoadMapFromFile(spriteSheet);

            Texture texture = TextureCache.Instance.GetTextureFromFile(map.SceneLayer.FileName);

            Sprite sprite = new Sprite(texture);

            Grid<ComparableColor> collisionGrid = map.GetCollisionGrid();

            ComparableColor colorThreshold = new ComparableColor(0, 0, 0, 0);
            MarchingSquaresGenerator<ComparableColor> marchingSquares =
                new MarchingSquaresGenerator<ComparableColor>(collisionGrid, colorThreshold);

            IEnumerable<LineSegment> lines = marchingSquares.GetLineSegments();

            IEntity entity = SpriteEntityFactory.CreateSpriteEdgeEntity(mapPosition, _physics, sprite, lines);

            m_entities = new List<IEntity>
            {
                entity
            };

            //debug
            MultiDrawable<VertexArrayShape> lineShapes = CreateLineSegmentsDrawable(lines, mapPosition);
            m_drawables.Add(lineShapes);
        }

        public IEnumerable<IEntity> GetEntities(IPhysics _physics)
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
            List<VertexArrayShape> shapes = new List<VertexArrayShape>();

            LineSegment[] lineSegments = _lineSegments as LineSegment[] ?? _lineSegments.ToArray();
            foreach (LineSegment lineSegment in lineSegments)
            {
                VertexArrayShape vertexArrayShape =
                    VertexArrayShape.Factory.CreateLinesShape(lineSegment, Color.Yellow);
                shapes.Add(vertexArrayShape);
            }

            MultiDrawable<VertexArrayShape> drawable = new MultiDrawable<VertexArrayShape>(shapes);
            drawable.SetPosition(_position);
            return drawable;
        }
    }
}