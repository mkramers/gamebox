namespace Common.Grid
{
    public class GridCell<T>
    {
        public GridCell(int _x, int _y, T _value)
        {
            X = _x;
            Y = _y;
            Value = _value;
        }

        public int X { get; }
        public int Y { get; }
        public T Value { get; }
    }
}