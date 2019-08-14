namespace Games.Coins
{
    public class CoinLookupEntry
    {
        public CoinLookupEntry(uint _grey, string _sprite, float _value)
        {
            Grey = _grey;
            Sprite = _sprite;
            Value = _value;
        }

        public uint Grey { get; }
        public string Sprite { get; }
        public float Value { get; }
    }
}