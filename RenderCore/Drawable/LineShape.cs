using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using LibExtensions;
using SFML.Graphics;

namespace RenderCore.Drawable
{
    public class LineShape : Transformable, SFML.Graphics.Drawable
    {
        private readonly VertexArray m_vertexArray;

        public LineShape(IEnumerable<Vector2> _vertices, Color _color)
        {
            Vector2[] vertices = _vertices as Vector2[] ?? _vertices.ToArray();

            m_vertexArray = new VertexArray(PrimitiveType.Lines, (uint)vertices.Count());

            foreach (Vector2 vertex in vertices)
            {
                m_vertexArray.Append(new Vertex(vertex.GetVector2F(), _color));
            }
        }

        public void Draw(RenderTarget _target, RenderStates _states)
        {
            _target.Draw(m_vertexArray, _states);
        }
    }
}