extern alias CoreCompatSystemDrawing;
using System;
using CoreCompatSystemDrawing::System.Drawing;

namespace GameResources.Attributes
{
    public class ComparableColor : IComparable
    {
        public ComparableColor(Color _color)
        {
            Color = _color;
        }

        public ComparableColor(int _r, int _g, int _b, int _a)
        {
            Color = Color.FromArgb(_r, _g, _b, _a);
        }

        private Color Color { get; }

        public int CompareTo(object _other)
        {
            if (!(_other is ComparableColor otherColor))
            {
                return -1;
            }
            
            return Color.R.CompareTo(otherColor.Color.R);
        }
    }
}