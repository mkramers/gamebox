using System;
using SFML.Graphics;
using SFML.System;

namespace RenderCore
{
    public class ViewProviderBase : View, IViewProvider
    {
        public ViewProviderBase(View _copy) : base(_copy)
        {
        }

        public ViewProviderBase()
        {
        }

        public View GetView()
        {
            return this;
        }

        public void SetSize(Vector2f _size)
        {
            Size = _size;
        }

        public void SetCenter(Vector2f _center)
        {
            Center = _center;
        }
    }
}