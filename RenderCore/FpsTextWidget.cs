using System;
using SFML.Graphics;

namespace RenderCore
{
    public class FpsTextWidget : TickableDrawable<Text>
    {
        private readonly int m_fpsBufferSize;
        private TimeSpan m_fpsBufferAccumulator;
        private int m_fpsBufferIndex;

        public FpsTextWidget(int _fpsBufferSize, Text _text) : base(_text)
        {
            m_fpsBufferSize = _fpsBufferSize;
            m_fpsBufferIndex = 0;
            m_fpsBufferAccumulator = TimeSpan.Zero;
        }

        public override void Tick(TimeSpan _elapsed)
        {
            if (m_fpsBufferIndex < m_fpsBufferSize)
            {
                m_fpsBufferAccumulator = (m_fpsBufferAccumulator + _elapsed) / 2.0f;
                m_fpsBufferIndex++;
            }
            else
            {
                string message =
                    $"FPS: {1.0 / m_fpsBufferAccumulator.TotalSeconds:0.00}\tTick: {m_fpsBufferAccumulator.TotalMilliseconds:0.00} ms";
                m_renderObject.DisplayedString = message;

                m_fpsBufferAccumulator = TimeSpan.Zero;
                m_fpsBufferIndex = 0;
            }
        }
    }
}