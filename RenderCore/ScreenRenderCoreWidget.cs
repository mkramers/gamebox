using System.Diagnostics;
using System.Numerics;
using SFML.Graphics;

namespace RenderCore
{
    public abstract class ScreenRenderCoreWidget : RenderCoreWidgetBase
    {
        private readonly ISpaceConverter m_spaceConverter;

        protected ScreenRenderCoreWidget(ISpaceConverter _spaceConverter)
        {
            Debug.Assert(_spaceConverter != null);

            m_spaceConverter = _spaceConverter;
        }

        protected Vector2 GetScreenSpacePosition()
        {
            return m_spaceConverter.ConvertTo(m_position);
        }
    }
}