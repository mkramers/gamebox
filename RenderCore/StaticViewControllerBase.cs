using System;
using SFML.Graphics;

namespace RenderCore
{
    public class StaticViewControllerBase : ViewControllerBase
    {
        public StaticViewControllerBase(View _view, float _windowRatio) : base(_view, _windowRatio)
        {
        }

        public override void Tick(TimeSpan _elapsed)
        {
        }
    }
}