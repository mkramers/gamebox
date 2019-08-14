using GameCore.Entity;

namespace Games.Coins
{
    public class Coin
    {
        public Coin(IEntity _entity, float _value)
        {
            Entity = _entity;
            Value = _value;
        }

        public IEntity Entity { get; }
        public float Value { get; }
    }
}