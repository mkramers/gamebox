﻿using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using Aether.Physics2D.Dynamics;
using Common.Geometry;
using Common.Grid;
using Common.VertexObject;
using GameCore.Entity;
using GameResources;
using GameResources.Attributes;
using GameResources.Converters;
using LibExtensions;
using MarchingSquares;
using PhysicsCore;
using RenderCore.Drawable;
using RenderCore.TextureCache;
using ResourceUtilities.Aseprite;
using SFML.Graphics;
using Color = SFML.Graphics.Color;

namespace GameCore.Maps
{
    public class SampleMap2 : IMap
    {
        private readonly List<IEntity> m_entities;
        private readonly List<IDrawable> m_drawables;

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
            MarchingSquaresGenerator<ComparableColor> m = new MarchingSquaresGenerator<ComparableColor>(collisionGrid, colorThreshold);
            IEnumerable<LineSegment> lineSegments = m.GetLineSegments();

            MultiDrawable<VertexArrayShape> lineDrawables = CreateLineSegmentsDrawable(lineSegments, mapPosition);

            m_drawables.Add(lineDrawables);

            IEnumerable<IVertexObject> polygons = GetVertexObjects(collisionGrid);
            
            IEnumerable<VertexArrayShape> lineShapes = CreateShapesFromVertexObjects(polygons, mapPosition);
            
            IEntity entity =
                SpriteEntityFactory.CreateSpriteEntity(0, mapPosition, _physics, BodyType.Static, sprite/*, bodyVertexObject.First()*/);

            m_entities = new List<IEntity>
            {
                entity
            };
        }

        private static IEnumerable<IVertexObject> GetVertexObjects(Grid<ComparableColor> _grid)
        {
            ComparableColor colorThreshold = new ComparableColor(0, 0, 0, 0);
            
            MarchingSquaresGenerator<ComparableColor> marchingSquares =
                new MarchingSquaresGenerator<ComparableColor>(_grid, colorThreshold);

            IVertexObjectsGenerator generator = new HeadToTailGenerator();
            IEnumerable<IVertexObject> polygons = marchingSquares.Generate(generator);
            return polygons;
        }
        
        private static IEnumerable<VertexArrayShape> CreateShapesFromVertexObjects(IEnumerable<IVertexObject> _vertexObjects, Vector2 _position)
        {
            List<VertexArrayShape> lineShapes = new List<VertexArrayShape>();
            foreach (IVertexObject bodyVertexObject in _vertexObjects)
            {
                for (int i = 0; i < bodyVertexObject.Count; i++)
                {
                    Vector2 offset = _position;
                    Vector2 start = bodyVertexObject[i] + offset;

                    if (i + 1 >= bodyVertexObject.Count)
                    {
                        continue;
                    }

                    Vector2 end = bodyVertexObject[(i + 1) % bodyVertexObject.Count] + offset;

                    //VertexArrayShape vertexArrayShape =
                    //    VertexArrayShape.Factory.CreateLineShape(new LineSegment(start, end), Color.Cyan);
                }

                VertexArrayShape vertexArrayShape =
                    VertexArrayShape.Factory.CreateLineShape(bodyVertexObject, Color.Cyan);

                lineShapes.Add(vertexArrayShape);
            }

            return lineShapes;
        }

        private static MultiDrawable<VertexArrayShape> CreateLineSegmentsDrawable(IEnumerable<LineSegment> _lineSegments, Vector2 _position)
        {
            List<VertexArrayShape> shapes = new List<VertexArrayShape>();

            LineSegment[] lineSegments = _lineSegments as LineSegment[] ?? _lineSegments.ToArray();
            foreach (LineSegment lineSegment in lineSegments)
            {
                VertexArrayShape vertexArrayShape = VertexArrayShape.Factory.CreateLineShape(lineSegment, Color.Yellow);
                vertexArrayShape.Position = _position.GetVector2F();
                shapes.Add(vertexArrayShape);
            }

            MultiDrawable<VertexArrayShape> drawable = new MultiDrawable<VertexArrayShape>(shapes);
            return drawable;
        }

        public IEnumerable<IEntity> GetEntities(IPhysics _physics)
        {
            return m_entities;
        }

        public IEnumerable<IDrawable> GetDrawables()
        {
            return m_drawables;
        }
    }
}