namespace Common.Extensions
{
    public static class FloatExtensions
    {
        public static float ToDegrees(this float _radians)
        {
            return 180.0f * _radians / (float) System.Math.PI;
        }
    }
}