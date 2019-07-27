using System.Collections.Generic;
using System.Diagnostics;
using System.Numerics;
using Aether.Physics2D.Dynamics;
using Common.Geometry;
using Common.VertexObject;
using LibExtensions;
using PhysicsCore;
using RenderCore.Drawable;
using RenderCore.ShapeUtilities;
using SFML.Graphics;
using SFML.System;

namespace GameCore.Entity
{
    public static class SpriteEntityFactory
    {
        public static IEntity CreateSpriteEntity(float _mass, Vector2 _position, IPhysics _physics, BodyType _bodyType,
            Sprite _sprite)
        {
            FloatRect spriteLocalBounds = _sprite.GetGlobalBounds();

            Vector2 spriteSize = spriteLocalBounds.GetSize();

            IVertexObject bodyVertexObject = ShapeFactory.CreateRectangle(spriteSize / 2);

            IEntity entity =
                CreateSpriteEntity(_mass, _position, _physics, _bodyType, _sprite, bodyVertexObject);
            return entity;
        }

        public static IEntity CreateSpriteEntity(float _mass, Vector2 _position, IPhysics _physics, BodyType _bodyType,
            Sprite _sprite, IVertexObject _bodyVertexObject)
        {
            Sprite sprite = FixSprite(_sprite);

            Drawable<Sprite> spriteDrawable = new Drawable<Sprite>(sprite);

            IBody body = _physics.CreateVertexBody(_bodyVertexObject, _position, _mass, _bodyType);

            Entity entity = new Entity(spriteDrawable, body);
            return entity;
        }

        public static IEntity CreateSpriteEdgeEntity(Vector2 _position, IPhysics _physics, Sprite _sprite,
            IEnumerable<LineSegment> _lineSegments)
        {
            Drawable<Sprite> spriteDrawable = new Drawable<Sprite>(_sprite);

            IBody body = _physics.CreateEdges(_lineSegments, _position);

            Entity entity = new Entity(spriteDrawable, body);
            return entity;
        }

        private static Sprite FixSprite(Sprite _sprite)
        {
            //the following sprite origin adjustment is required because for some reason, a rectangle dynamic body with only positive values causes weird behavior in the physics
            Debug.Assert(_sprite.Origin == new Vector2f(),
                $"Sprite origin should be {Vector2.Zero.GetDisplayString()}");

            Sprite sprite = new Sprite(_sprite);
            sprite.Origin = sprite.Texture.Size.GetVector2F() / 2.0f;
            return sprite;
        }
    }
}