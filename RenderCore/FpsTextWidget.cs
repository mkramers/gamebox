using System;
using SFML.Graphics;

namespace RenderCore
{
    public class FpsTextWidget : TextWidget, ITickable
    {
        private readonly int m_fpsBufferSize;
        private int m_fpsBufferIndex;
        private TimeSpan m_fpsBufferAccumulator;

        public FpsTextWidget(Font _font, uint _fontSize, int _fpsBufferSize) : base(_font, _fontSize)
        {
            m_fpsBufferSize = _fpsBufferSize;
            m_fpsBufferIndex = 0;
            m_fpsBufferAccumulator = TimeSpan.Zero;
        }

        public void Tick(TimeSpan _elapsed)
        {
            if (m_fpsBufferIndex < m_fpsBufferSize)
            {
                m_fpsBufferAccumulator = (m_fpsBufferAccumulator + _elapsed) / 2.0f;
                m_fpsBufferIndex++;
            }
            else
            {
                string message = $"FPS: {1.0 / m_fpsBufferAccumulator.TotalSeconds:0.00}\tTick: {m_fpsBufferAccumulator.TotalMilliseconds:0.00} ms";
                SetMessage(message);

                m_fpsBufferAccumulator = TimeSpan.Zero;
                m_fpsBufferIndex = 0;
            }
        }
    }
}