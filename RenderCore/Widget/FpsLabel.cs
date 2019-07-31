using System;
using RenderCore.Font;

namespace RenderCore.Widget
{
    public class FpsLabel : TGUI.Label, IWidget
    {
        private readonly int m_fpsBufferSize;
        private TimeSpan m_fpsBufferAccumulator;
        private int m_fpsBufferIndex;

        public FpsLabel(int _fpsBufferSize, FontSettings _fontSettings)
        {
            m_fpsBufferSize = _fpsBufferSize;
            m_fpsBufferIndex = 0;
            m_fpsBufferAccumulator = TimeSpan.Zero;

            TextSize = _fontSettings.Size;
            Renderer.TextColor = _fontSettings.FillColor;
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
                string message =
                    $"FPS: {1.0 / m_fpsBufferAccumulator.TotalSeconds:0.00}\tTick: {m_fpsBufferAccumulator.TotalMilliseconds:0.00} ms";
                Text = message;

                m_fpsBufferAccumulator = TimeSpan.Zero;
                m_fpsBufferIndex = 0;
            }
        }
    }
}