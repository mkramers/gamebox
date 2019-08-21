using System;
using GameCore.Entity;

namespace Games.Coins
{
    public class Coin : IDisposable
    {
        public Coin(IEntity _entity, float _value)
        {
            Entity = _entity;
            Value = _value;
        }

        public IEntity Entity { get; }
        public float Value { get; }

        public void Dispose()
        {
            Entity.Dispose();
        }
    }
}