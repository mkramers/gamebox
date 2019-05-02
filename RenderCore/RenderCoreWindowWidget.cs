﻿using SFML.Graphics;

namespace RenderCore
{
    public abstract class RenderCoreWindowWidget : IRenderCoreWindowWidget
    {
        public bool IsDrawEnabled { protected get; set; }

        public abstract void Draw(RenderTarget _target, RenderStates _states);

        public abstract void Dispose();
    }
}