using System;
using System.Numerics;
using SFML.Graphics;

namespace RenderCore
{
    public class FpsTextWidget : TextWidget
    {
        private readonly int m_fpsBufferSize;
        private TimeSpan m_fpsBufferAccumulator;
        private int m_fpsBufferIndex;

        public FpsTextWidget(int _fpsBufferSize, Vector2 _renderObjectScale,
            ISpaceConverter _spaceConverter, Text _text) : base(_renderObjectScale, _spaceConverter, _text)
        {
            m_fpsBufferSize = _fpsBufferSize;
            m_fpsBufferIndex = 0;
            m_fpsBufferAccumulator = TimeSpan.Zero;
        }

        public override void Tick(TimeSpan _elapsed)
        {
            base.Tick(_elapsed);

            if (m_fpsBufferIndex < m_fpsBufferSize)
            {
                m_fpsBufferAccumulator = (m_fpsBufferAccumulator + _elapsed) / 2.0f;
                m_fpsBufferIndex++;
            }
            else
            {
                string message =
                    $"FPS: {1.0 / m_fpsBufferAccumulator.TotalSeconds:0.00}\tTick: {m_fpsBufferAccumulator.TotalMilliseconds:0.00} ms";
                SetMessage(message);

                m_fpsBufferAccumulator = TimeSpan.Zero;
                m_fpsBufferIndex = 0;
            }
        }
    }
}