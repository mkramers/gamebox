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

        public Color Color { get; }
        
        public int CompareTo(object _other)
        {
            if (!(_other is Color otherColor))
            {
                return -1;
            }

            Vector4 otherColorVector = GetColorVector(otherColor);
            Vector4 colorVector = GetColorVector(Color);

            return colorVector.Length().CompareTo(otherColorVector.Length());
        }

        private static Vector4 GetColorVector(Color _color)
        {
            Vector4 vector = new Vector4(_color.R, _color.G, _color.B, _color.A);
            return vector;
        }
    }
}