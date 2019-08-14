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
using RenderCore.Resource;
using ResourceUtilities.Aseprite;
using SFML.Graphics;
using SFML.System;

namespace Games.Coins
{
    public static class CoinEntitiesFactory
    {
        public static IEnumerable<Coin> GetCoins(string _resourceRootDirectory)
        {
            ResourceManager<SpriteResources> resourceManager =
                new ResourceManager<SpriteResources>(_resourceRootDirectory);

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
                }
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
                        int greyValue = (int) _coin.Grey;

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
                            SpriteEntityFactory.CreateSpriteEntity(0.01f, coinPosition, BodyType.Dynamic,
                                coinSprite);

                        Coin coin = new Coin(coinEntity, coinEntry.Value);
                        coins.Add(coin);
                    }
                }
            }

            return coins;
        }
    }
}