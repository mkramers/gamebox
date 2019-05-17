using System;
using System.Diagnostics;
using System.Numerics;
using SFML.Graphics;

namespace RenderCore
{
    public abstract class ScreenRenderCoreWidget<T> : RenderWidget<T> where T : Transformable, Drawable
    {
        private readonly Vector2 m_renderObjectScale;
        private readonly ISpaceConverter m_spaceConverter;

        protected ScreenRenderCoreWidget(T _renderObject, Vector2 _renderObjectScale, ISpaceConverter _spaceConverter) : base(_renderObject)
        {
            Debug.Assert(_spaceConverter != null);

            m_renderObjectScale = _renderObjectScale;
            m_spaceConverter = _spaceConverter;
        }

        public override void Tick(TimeSpan _elapsed)
        {
            T renderObject = GetRenderObject();

            renderObject.Position = m_spaceConverter.Transform(m_position).GetVector2F();
            renderObject.Scale = m_spaceConverter.TransformScale(m_renderObjectScale).GetVector2F();
        }
    }
}