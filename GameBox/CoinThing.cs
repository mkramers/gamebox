using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Numerics;
using Aether.Physics2D.Dynamics;
using Aether.Physics2D.Dynamics.Contacts;
using Common.Cache;
using Common.Grid;
using GameCore;
using GameCore.Entity;
using GameResources.Attributes;
using GameResources.Converters;
using LibExtensions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PhysicsCore;
using RenderCore.Render;
using RenderCore.Resource;
using ResourceUtilities.Aseprite;
using SFML.Graphics;
using SFML.System;
using TGUI;
using Color = SFML.Graphics.Color;

namespace GameBox
{
    public class CoinEntitiesFactory
    {
        public static IEnumerable<Coin> GetCoins(string _resourceRootDirectory, IPhysics _physics)
        {
            ResourceManager<SpriteResources> resourceManager = new ResourceManager<SpriteResources>(_resourceRootDirectory);

            const string coinsMetaFilePath = @"C:\dev\GameBox\RenderCore\Resources\meta\coins.json";
            string coinsMetaText = File.ReadAllText(coinsMetaFilePath);
            CoinLookupTable coinsLookupTable =
                JsonConvert.DeserializeObject<CoinLookupTable>(coinsMetaText, new CoinMetaConverter());

            List<CoinLookupEntry> coinDefinitions = coinsLookupTable.Coins;

            Bitmap bitmap = resourceManager.GetBitmapResource(SpriteResources.MAP_COINMAP_LAYOUT).Load();
            
            Grid<ComparableColor> grid = BitmapToGridConverter.GetColorGridFromBitmap(bitmap);

            Dictionary<string, Texture> coinTextures = new Dictionary<string, Texture>
            {
                {
                    "yellow",
                    resourceManager.GetTextureResource(SpriteResources.OBJECT_COIN_YELLOW).Load()
                },
                {
                    "orange",
                    resourceManager.GetTextureResource(SpriteResources.OBJECT_COIN_ORANGE).Load()
                },
                {
                    "red",
                    resourceManager.GetTextureResource(SpriteResources.OBJECT_COIN_RED).Load()
                },
            };

            const float coinSize = 2.0f;

            List<Coin> coins = new List<Coin>();

            for (int x = 0; x < grid.Columns; x++)
            {
                for (int y = 0; y < grid.Rows; y++)
                {
                    ComparableColor comparableColor = grid[x, y];

                    List<CoinLookupEntry> coinEntries = coinDefinitions.FindAll(_coin =>
                    {
                        int greyValue = (int)_coin.Grey;

                        ComparableColor gray = new ComparableColor(greyValue, greyValue, greyValue, 255);
                        return gray.CompareTo(comparableColor) == 0;
                    });

                    foreach (CoinLookupEntry coinEntry in coinEntries)
                    {
                        Debug.Assert(coinTextures.ContainsKey(coinEntry.Sprite));

                        Texture coinTexture = coinTextures[coinEntry.Sprite];

                        Vector2 coinPosition = new Vector2(x, y);
                        Sprite coinSprite = new Sprite(coinTexture)
                        {
                            Position = coinPosition.GetVector2F(),
                            Scale = new Vector2f(coinSize / coinTexture.Size.X, coinSize / coinTexture.Size.Y)
                        };

                        IEntity coinEntity =
                            SpriteEntityFactory.CreateSpriteEntity(0.01f, coinPosition, _physics, BodyType.Dynamic, coinSprite);

                        Coin coin = new Coin(coinEntity, coinEntry.Value);
                        coins.Add(coin);
                    }
                }
            }

            return coins;
        }
    }

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

    public class CoinThing : IGameModule
    {
        private readonly IEntity m_captureEntity;
        private readonly IRenderCoreTarget m_target;
        private readonly Gui m_gui;
        private readonly List<Coin> m_coins;
        private float m_score;
        private readonly Label m_scoreLabel;

        public CoinThing(IEntity _captureEntity, IEnumerable<Coin> _coins, IRenderCoreTarget _target, Gui _gui)
        {
            m_captureEntity = _captureEntity;
            m_target = _target;
            m_gui = _gui;

            m_coins = new List<Coin>();

            IEnumerable<Coin> coins = _coins as Coin[] ?? _coins.ToArray();
            foreach (Coin coin in coins)
            {
                IEntity entity = coin.Entity;

                entity.Collided += EntityOnCollided;
                entity.Separated += EntityOnSeparated;

                _target.AddDrawable(entity);

                m_coins.Add(coin);
            }

            m_scoreLabel = new Label();
            m_scoreLabel.SetPosition(new Layout2d(10, 10));
            m_scoreLabel.Renderer.TextColor = Color.White;
            m_gui.Add(m_scoreLabel);

            UpdateScoreLabel(m_score);
        }

        private void EntityOnCollided(object _sender, CollisionEventArgs _e)
        {
            Fixture coinFixture = _e.Sender;

            Coin coin = m_coins.FirstOrDefault(_coin => _coin.Entity.ContainsFixture(coinFixture));
            if (coin == null)
            {
                return;
            }

            bool collidedWithCaptureEntity = m_captureEntity.ContainsFixture(_e.Other);
            if (!collidedWithCaptureEntity)
            {
                return;
            }

            m_score += coin.Value;

            UpdateScoreLabel(m_score);

            m_coins.Remove(coin);
            m_target.RemoveDrawable(coin.Entity);

            if (m_score > 0)
            {
                ShowWinScreen();
            }
        }

        private void UpdateScoreLabel(float _score)
        {
            m_scoreLabel.Text = $"Score: {_score}";
        }

        private void ShowWinScreen()
        {
            PauseGame?.Invoke(this, EventArgs.Empty);

            ChildWindow childWindow = new ChildWindow("Winner!");
            childWindow.SetSize(new Layout2d(300, 100));
            childWindow.SetPosition(new Layout2d(50, 50));
            childWindow.Closed += WinScreenOnClosed;

            m_gui.Add(childWindow);
        }

        private void WinScreenOnClosed(object _sender, EventArgs _e)
        {
            ChildWindow childWindow = _sender as ChildWindow;
            Debug.Assert(childWindow != null);

            m_gui.Remove(childWindow);

            ResumeGame?.Invoke(this, EventArgs.Empty);
        }

        private void EntityOnSeparated(object _sender, SeparationEventArgs _e)
        {
        }

        public void Tick(TimeSpan _elapsed)
        {
            foreach (Coin coin in m_coins)
            {
                coin.Entity.Tick(_elapsed);
            }
        }

        public event EventHandler PauseGame;
        public event EventHandler ResumeGame;
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