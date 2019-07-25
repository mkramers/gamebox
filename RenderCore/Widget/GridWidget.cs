using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Numerics;
using LibExtensions;
using RenderCore.Drawable;
using RenderCore.Font;
using RenderCore.Render;
using RenderCore.ShapeUtilities;
using RenderCore.ViewProvider;
using SFML.Graphics;
using SFML.System;

namespace RenderCore.Widget
{
    public class LabeledGridWidget : GridWidget
    {
        private readonly MultiDrawable<Text> m_labels;
        private readonly FontSettings m_fontSettings;

        public LabeledGridWidget(IViewProvider _viewProvider, float _lineThickness, Vector2 _cellSize, FontSettings _fontSettings) : base(_viewProvider, _lineThickness, _cellSize)
        {
            m_fontSettings = _fontSettings;
            m_labels = new MultiDrawable<Text>();
        }

        public override void Tick(TimeSpan _elapsed)
        {
            base.Tick(_elapsed);

            m_labels.DisposeItemsAndClear();

            View view = m_viewProvider.GetView();
            Vector2f size = view.Size + new Vector2f(2, 2);

            const float labelIncrement = 1.0f;
            Vector2f topLeft = view.Center - size / 2;

            int numVerticalLabels = (int)Math.Ceiling(size.Y / labelIncrement);
            for (int i = 0; i < numVerticalLabels; i++)
            {
                float labelValue = topLeft.Y + labelIncrement * i;
                Vector2f labelPosition = topLeft + new Vector2f(1, labelValue + 1.5f);

                Text text = TextFactory.GenerateText(m_fontSettings);
                text.DisplayedString = labelValue.ToString(CultureInfo.InvariantCulture);
                text.Position = labelPosition;

                m_labels.Add(text);
            }

            int numHorizontalLabels = (int)Math.Ceiling(size.X / labelIncrement);
            for (int i = 0; i < numHorizontalLabels; i++)
            {
                float labelValue = topLeft.X + labelIncrement * i;
                Vector2f labelPosition = topLeft + new Vector2f(labelValue + 1.5f, 1);

                Text text = TextFactory.GenerateText(m_fontSettings);
                text.DisplayedString = labelValue.ToString(CultureInfo.InvariantCulture);
                text.Position = labelPosition;

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

    public abstract class ViewWidgetBase : IWidget, IDrawable
    {
        protected readonly IViewProvider m_viewProvider;

        protected ViewWidgetBase(IViewProvider _viewProvider)
        {
            m_viewProvider = _viewProvider;
        }

        public abstract void Tick(TimeSpan _elapsed);
        public abstract void Draw(RenderTarget _target, RenderStates _states);
        public abstract void Dispose();
    }

    public class GridWidget : ViewWidgetBase
    {
        private readonly Vector2 m_cellSize;
        private readonly float m_lineThickness;
        private readonly MultiDrawable<VertexArrayShape> m_drawable;

        public GridWidget(IViewProvider _viewProvider, float _lineThickness, Vector2 _cellSize) : base(_viewProvider)
        {
            m_lineThickness = _lineThickness;
            m_cellSize = _cellSize;
            m_drawable = new MultiDrawable<VertexArrayShape>();

            UpdateDrawable();
        }

        public override void Tick(TimeSpan _elapsed)
        {
            UpdateDrawable();
        }

        private void UpdateDrawable()
        {
            IEnumerable<VertexArrayShape> gridDrawables = GetGridShapes(m_viewProvider.GetView());

            IEnumerable<VertexArrayShape> vertexArrayShapes = gridDrawables as VertexArrayShape[] ?? gridDrawables.ToArray();

            m_drawable.DisposeItemsAndClear();
            m_drawable.AddRange(vertexArrayShapes);
        }

        public override void Draw(RenderTarget _target, RenderStates _states)
        {
            m_drawable.Draw(_target, _states);
        }

        public override void Dispose()
        {
            m_drawable.Dispose();
        }

        private IEnumerable<VertexArrayShape> GetGridShapes(View _view)
        {
            View view = _view;
            Vector2f size = view.Size + new Vector2f(2, 2);

            Vector2 snappedCenter =
                new Vector2((float)Math.Round(view.Center.X), (float)Math.Round(view.Center.Y));
            View snappedView = new View(snappedCenter.GetVector2F(), size);

            IEnumerable<VertexArrayShape> gridDrawables =
                GridDrawingUtilities.GetGridDrawableFromView(snappedView, m_lineThickness, m_cellSize);
            return gridDrawables;
        }
    }
}