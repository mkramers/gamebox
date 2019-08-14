using System.Collections.Generic;
using GameCore.Entity;
using RenderCore.Drawable;

namespace GameCore.Maps
{
    public interface IMap
    {
        IEnumerable<IEntity> GetEntities();
        IEnumerable<IDrawable> GetDrawables();
    }
}