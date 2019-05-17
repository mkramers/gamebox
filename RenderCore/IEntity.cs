using System;
using SFML.Graphics;

namespace RenderCore
{
    public interface IEntity : IBody, ITickable, IDrawable
    {
    }
}