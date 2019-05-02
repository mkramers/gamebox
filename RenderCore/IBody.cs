﻿using System.Numerics;

namespace RenderCore
{
    public interface IBody
    {
        Vector2 GetPosition();
        void ApplyForce(Vector2 _force);

        void RemoveFromWorld();
    }
}