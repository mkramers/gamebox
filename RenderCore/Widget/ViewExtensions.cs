using System.Numerics;
using LibExtensions;
using SFML.Graphics;
using SFML.System;

namespace RenderCore.Widget
{
    public static class ViewExtensions
    {
        public static Vector2 GetTopLeft(this View _view)
        {
            Vector2f vector2F = _view.Center.Subtract(_view.Size / 2.0f);
            return vector2F.GetVector2();
        }

        public static Vector2 GetSize(this View _view)
        {
            return _view.Size.GetVector2();
        }

        public static Vector2 GetAbsolutePosition(this View _view, Vector2 _relativePosition)
        {
            Vector2 position = _view.GetTopLeft() + _relativePosition * _view.GetSize();
            return position;
        }
    }
}