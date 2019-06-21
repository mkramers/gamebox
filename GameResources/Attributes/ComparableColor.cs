extern alias CoreCompatSystemDrawing;
using System;
using System.Numerics;
using CoreCompatSystemDrawing::System.Drawing;

namespace GameResources.Attributes
{
    public class ComparableColor : IComparable
    {
        public ComparableColor(Color _color)
        {
            Color = _color;
        }

        private Color Color { get; }

        public int CompareTo(object _other)
        {
            if (!(_other is ComparableColor otherColor))
            {
                return -1;
            }

            Vector4 colorVector = GetColorVector(Color);
            float colorLength = colorVector.Length();

            Vector4 otherColorVector = GetColorVector(otherColor.Color);
            float otherColorLength = otherColorVector.Length();

            //return colorLength.CompareTo(otherColorLength);
            return Color.R.CompareTo(otherColor.Color.R);
        }

        private static Vector4 GetColorVector(Color _color)
        {
            Vector4 vector = new Vector4(_color.R, _color.G, _color.B, _color.A);
            return vector;
        }
    }
}