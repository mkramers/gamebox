using Aether.Physics2D.Common.Maths;
using SFML.Graphics;
using SFML.System;

namespace RenderCore
{
    public interface IViewController : ITickable
    {
        View GetView();
        void SetParentSize(Vector2u _parentSize);
    }
}