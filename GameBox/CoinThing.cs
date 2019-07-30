using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Numerics;
using Aether.Physics2D.Dynamics;
using Common.Grid;
using GameCore.Entity;
using GameResources.Attributes;
using GameResources.Converters;
using LibExtensions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PhysicsCore;
using SFML.Graphics;
using SFML.System;

namespace GameBox
{
    public class CoinThing
    {
        public List<IEntity> CoinEntities { get; }

        public CoinThing(IPhysics _physics)
        {
            const string coinsMetaFilePath = @"C:\dev\GameBox\RenderCore\Resources\meta\coins.json";
            string coinsMetaText = File.ReadAllText(coinsMetaFilePath);
            CoinLookupTable coinsLookupTable = JsonConvert.DeserializeObject<CoinLookupTable>(coinsMetaText, new CoinMetaConverter());

            List<CoinLookupEntry> coinDefinitions = coinsLookupTable.Coins;

            const string coinsMapFilePath = @"C:\dev\GameBox\RenderCore\Resources\art\coinmap-layout.png";

            Bitmap bitmap = new Bitmap(coinsMapFilePath);

            Grid<ComparableColor> grid = BitmapToGridConverter.GetColorGridFromBitmap(bitmap);

            Dictionary<string, Texture> coinTextures = new Dictionary<string, Texture>
            {
                {"yellow", RenderCore.TextureCache.TextureCache.Instance.GetTextureFromFile(@"C:\dev\GameBox\RenderCore\Resources\art\coin-yellow.png")},
                {"orange", RenderCore.TextureCache.TextureCache.Instance.GetTextureFromFile(@"C:\dev\GameBox\RenderCore\Resources\art\coin-orange.png")},
                {"red", RenderCore.TextureCache.TextureCache.Instance.GetTextureFromFile(@"C:\dev\GameBox\RenderCore\Resources\art\coin-red.png")},
            };

            const float coinSize = 2.0f;

            CoinEntities = new List<IEntity>();
            for (int x = 0; x < grid.Columns; x++)
            {
                for (int y = 0; y < grid.Rows; y++)
                {
                    ComparableColor comparableColor = grid[x, y];

                    List<CoinLookupEntry> coins = coinDefinitions.FindAll(_coin =>
                    {
                        int greyValue = (int)_coin.Grey;

                        ComparableColor gray = new ComparableColor(greyValue, greyValue, greyValue, 255);
                        return gray.CompareTo(comparableColor) == 0;
                    });

                    foreach (CoinLookupEntry coin in coins)
                    {
                        Debug.Assert(coinTextures.ContainsKey(coin.Sprite));

                        Texture coinTexture = coinTextures[coin.Sprite];

                        Vector2 coinPosition = new Vector2(x, y);
                        Sprite coinSprite = new Sprite(coinTexture)
                        {
                            Position = coinPosition.GetVector2F(),
                            Scale = new Vector2f(coinSize / coinTexture.Size.X, coinSize / coinTexture.Size.Y)
                        };

                        IEntity coinEntity =
                            SpriteEntityFactory.CreateSpriteEntity(0.01f, coinPosition, _physics, BodyType.Dynamic, coinSprite);

                        CoinEntities.Add(coinEntity);
                    }
                }
            }
        }

    }

    public class CoinLookupTable
    {
        public List<CoinLookupEntry> Coins { get; set; }
    }

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

    public class CoinMetaConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter _writer, object _value, JsonSerializer _serializer)
        {
            throw new NotImplementedException("Not implemented yet");
        }

        public override object ReadJson(JsonReader _reader, Type _objectType, object _existingValue,
            JsonSerializer _serializer)
        {
            switch (_reader.TokenType)
            {
                case JsonToken.Null:
                    return string.Empty;
                case JsonToken.String:
                    return _serializer.Deserialize(_reader, (Type)_objectType);
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

        public override bool CanWrite => false;

        public override bool CanConvert(Type _objectType)
        {
            return _objectType == typeof(CoinLookupEntry);
        }
    }
}