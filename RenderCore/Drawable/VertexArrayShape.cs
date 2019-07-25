using System.Collections.Generic;
using System.Numerics;
using SFML.Graphics;

namespace RenderCore.Drawable
{
    public class VertexArrayShape : Transformable, SFML.Graphics.Drawable
    {
        private readonly VertexArray m_vertexArray;

        private VertexArrayShape(IVertexArrayCreator _creator)
        {
            m_vertexArray = _creator.CreateVertexArray();
        }

        public void Draw(RenderTarget _target, RenderStates _states)
        {
            _states.Transform *= Transform;
            _target.Draw(m_vertexArray, _states);
        }

        public static class Factory
        {
            public static VertexArrayShape CreateLinesShape(IEnumerable<Vector2> _vertices, Color _color)
            {
                LineVertexArrayCreator creator = new LineVertexArrayCreator(_vertices, PrimitiveType.Lines, _color);
                return new VertexArrayShape(creator);
            }
            public static VertexArrayShape CreateLineStripShape(IEnumerable<Vector2> _vertices, Color _color)
            {
                LineVertexArrayCreator creator = new LineVertexArrayCreator(_vertices, PrimitiveType.LineStrip, _color);
                return new VertexArrayShape(creator);
            }
        }
    }
}