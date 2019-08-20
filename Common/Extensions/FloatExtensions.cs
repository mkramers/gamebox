using System;

namespace Common.Extensions
{
    public static class FloatExtensions
    {
        public static float ToDegrees(this float _radians)
        {
            float fullDegrees = 180.0f * _radians / (float) Math.PI;

            float degrees = fullDegrees % 360;
            if (degrees < 0)
            {
                degrees += 360;
            }
            return degrees;
        }
    }
}