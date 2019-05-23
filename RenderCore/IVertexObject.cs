using System.Collections.Generic;
using System.Numerics;

namespace RenderCore
{
    public interface IVertexObject : IList<Vector2>
    {
        void Translate(Vector2 _translation);
    }
}