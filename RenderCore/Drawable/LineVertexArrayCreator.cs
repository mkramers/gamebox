using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using LibExtensions;
using SFML.Graphics;

namespace RenderCore.Drawable
{
    public class LineVertexArrayCreator : IVertexArrayCreator
    {
        private readonly Color m_color;
        private readonly PrimitiveType m_primitiveType;
        private readonly IEnumerable<Vector2> m_vertices;

        public LineVertexArrayCreator(IEnumerable<Vector2> _vertices, PrimitiveType _primitiveType, Color _color)
        {
            m_vertices = _vertices;
            m_color = _color;
            m_primitiveType = _primitiveType;
        }

        public VertexArray CreateVertexArray()
        {
            Vector2[] vertices = m_vertices as Vector2[] ?? m_vertices.ToArray();

            VertexArray vertexArray = new VertexArray(m_primitiveType, (uint) vertices.Length);

            foreach (Vector2 vertex in vertices)
            {
                vertexArray.Append(new Vertex(vertex.GetVector2F(), m_color));
            }

            return vertexArray;
        }
    }
}