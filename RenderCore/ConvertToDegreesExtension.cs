using System;

namespace RenderCore
{
    public static class ConvertToDegreesExtension
    {
        public static float ToDegrees(this float _radians)
        {
            return 180.0f * _radians / (float) Math.PI;
        }
    }
}