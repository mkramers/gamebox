using System;
using System.Globalization;
using System.Numerics;
using LibExtensions;
using RenderCore.Drawable;
using RenderCore.Font;
using RenderCore.Render;
using RenderCore.ViewProvider;
using SFML.Graphics;
using SFML.System;

namespace RenderCore.Widget
{
    public class LabeledGridWidget : GridWidget
    {
        private readonly FontSettings m_fontSettings;
        private readonly MultiDrawable<Text> m_labels;

        public LabeledGridWidget(IViewProvider _viewProvider, Vector2 _cellSize,
            FontSettings _fontSettings) : base(_viewProvider, _cellSize)
        {
            m_fontSettings = _fontSettings;
            m_labels = new MultiDrawable<Text>();
        }

        public override void Tick(TimeSpan _elapsed)
        {
            base.Tick(_elapsed);

            m_labels.DisposeItemsAndClear();

            View view = m_viewProvider.GetView();

            Vector2f snappedOffset = new Vector2f(1, 1);
            Vector2f size = view.Size + snappedOffset;

            Vector2 snappedCenter =
                new Vector2((float) Math.Round(view.Center.X), (float) Math.Round(view.Center.Y));
            View snappedView = new View(snappedCenter.GetVector2F(), size);

            const float labelIncrement = 1.0f;
            Vector2f topLeft = snappedView.Center - snappedView.Size / 2 + snappedOffset;

            int numVerticalLabels = (int) Math.Ceiling(snappedView.Size.Y / labelIncrement);
            for (int i = -1; i < numVerticalLabels; i++)
            {
                float labelValue = (float) Math.Floor(topLeft.Y + labelIncrement * i);
                Vector2f labelPosition = new Vector2f((float) Math.Ceiling(topLeft.X), labelValue);

                Text text = TextFactory.GenerateText(m_fontSettings);
                text.DisplayedString = labelValue.ToString(CultureInfo.InvariantCulture);

                text.SetTextCenter(labelPosition);

                m_labels.Add(text);
            }

            int numHorizontalLabels = (int) Math.Ceiling(snappedView.Size.X / labelIncrement);
            for (int i = -1; i < numHorizontalLabels; i++)
            {
                float labelValue = (float) Math.Floor(topLeft.X + labelIncrement * i);
                Vector2f labelPosition = new Vector2f(labelValue, (float) Math.Ceiling(topLeft.Y));

                Text text = TextFactory.GenerateText(m_fontSettings);
                text.DisplayedString = labelValue.ToString(CultureInfo.InvariantCulture);

                text.SetTextCenter(labelPosition);

                m_labels.Add(text);
            }
        }

        public override void Draw(RenderTarget _target, RenderStates _states)
        {
            base.Draw(_target, _states);

            m_labels.Draw(_target, _states);
        }

        public override void Dispose()
        {
            base.Dispose();

            m_labels.Dispose();
        }
    }
}