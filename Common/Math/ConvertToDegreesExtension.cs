namespace Common.Math
{
    public static class ConvertToDegreesExtension
    {
        public static float ToDegrees(this float _radians)
        {
            return 180.0f * _radians / (float) System.Math.PI;
        }
    }
}