using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using LibExtensions;
using SFML.Graphics;

namespace RenderCore.Drawable
{
    public class VertexArrayCreator : IVertexArrayCreator
    {
        private readonly IEnumerable<Vector2> m_vertices;
        private readonly Color m_color;

        public VertexArrayCreator(IEnumerable<Vector2> _vertices, Color _color)
        {
            m_vertices = _vertices;
            m_color = _color;
        }
        public VertexArray CreateVertexArray()
        {
            Vector2[] vertices = m_vertices as Vector2[] ?? m_vertices.ToArray();

            VertexArray vertexArray = new VertexArray(PrimitiveType.Lines, (uint)vertices.Length);

            foreach (Vector2 vertex in vertices)
            {
                vertexArray.Append(new Vertex(vertex.GetVector2F(), m_color));
            }

            return vertexArray;
        }
    }
}