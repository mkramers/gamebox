using System.Numerics;
using SFML.Graphics;
using SFML.System;

namespace RenderCore
{
    public class ViewSpaceConverter : ISpaceConverter
    {
        private readonly IViewProvider m_viewProvider;

        public ViewSpaceConverter(IViewProvider _viewProvider)
        {
            m_viewProvider = _viewProvider;
        }

        public Vector2 Transform(Vector2 _vector)
        {
            View view = m_viewProvider.GetView();

            Vector2 scale = TransformScale(_vector);
            Vector2f position = view.Center - scale.GetVector2F() / 2;

            Vector2 screenSpaceVector = scale + position.GetVector2();
            return screenSpaceVector;
        }

        public Vector2 TransformScale(Vector2 _vector)
        {
            View view = m_viewProvider.GetView();

            Vector2f size = view.Size;

            Vector2 screenSpaceVector = new Vector2(size.X * _vector.X, size.Y * _vector.Y);
            return screenSpaceVector;
        }
    }
}