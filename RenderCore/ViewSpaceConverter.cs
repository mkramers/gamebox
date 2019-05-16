using System;
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

        public Vector2 ConvertTo(Vector2 _vector)
        {
            View view = m_viewProvider.GetView();

            Vector2f size = view.Size;
            Vector2f position = view.Center - size / 2;
            
            Vector2 screenSpaceVector= new Vector2(size.X * _vector.X, size.Y * _vector.Y) + position.GetVector2();
            return screenSpaceVector;
        }

        public Vector2 ConvertFrom(Vector2 _vector)
        {
            throw new NotImplementedException();
        }
    }
}