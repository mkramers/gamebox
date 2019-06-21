using System.Collections.Generic;

namespace Common.Grid
{
    public class GridCell<T>
    {
        private bool Equals(GridCell<T> _other)
        {
            return X == _other.X && Y == _other.Y && EqualityComparer<T>.Default.Equals(Value, _other.Value);
        }

        public override bool Equals(object _obj)
        {
            if (ReferenceEquals(null, _obj))
            {
                return false;
            }

            if (ReferenceEquals(this, _obj))
            {
                return true;
            }

            if (_obj.GetType() != this.GetType())
            {
                return false;
            }

            return Equals((GridCell<T>) _obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = X;
                hashCode = (hashCode * 397) ^ Y;
                hashCode = (hashCode * 397) ^ EqualityComparer<T>.Default.GetHashCode(Value);
                return hashCode;
            }
        }

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