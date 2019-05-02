using System;

namespace RenderCore
{
    public interface IEntity : IBody, IDrawable, ITickable, IDisposable
    {
    }
}