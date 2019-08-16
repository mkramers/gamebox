using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Games.Coins
{
    // ReSharper disable once ClassNeverInstantiated.Global

    public class CoinMetaConverter : JsonConverter
    {
        public override bool CanWrite => false;

        public override void WriteJson(JsonWriter _writer, object _value, JsonSerializer _serializer)
        {
            throw new NotImplementedException("Not implemented yet");
        }

        public override object ReadJson(JsonReader _reader, Type _objectType, object _existingValue,
            JsonSerializer _serializer)
        {
            // ReSharper disable once SwitchStatementMissingSomeCases
            switch (_reader.TokenType)
            {
                case JsonToken.Null:
                    return string.Empty;
                case JsonToken.String:
                    return _serializer.Deserialize(_reader, _objectType);
                default:
                {
                    JObject obj = JObject.Load(_reader);
                    string greyText = obj["grey"].ToString();

                    uint grey = uint.Parse(greyText);
                    string valueText = obj["value"].ToString();

                    float value = float.Parse(valueText);
                    string spriteText = obj["sprite"].ToString();

                    CoinLookupEntry coinEntry = new CoinLookupEntry(grey, spriteText, value);
                    return coinEntry;
                }
            }
        }

        public override bool CanConvert(Type _objectType)
        {
            return _objectType == typeof(CoinLookupEntry);
        }
    }
}