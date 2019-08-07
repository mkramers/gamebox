using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace ResourceUtilities.Aseprite
{
    public static class SpriteSheetLoader
    {
        public static SpriteSheet LoadFromFile(string _path)
        {
            DefaultContractResolver contractResolver = new DefaultContractResolver
            {
                NamingStrategy = new CamelCaseNamingStrategy
                {
                    OverrideSpecifiedNames = false
                }
            };

            JsonSerializerSettings settings = new JsonSerializerSettings
            {
                ContractResolver = contractResolver,
                Formatting = Formatting.Indented
            };

            string fileContents =
                File.ReadAllText(_path);

            SpriteSheet spriteSheet = JsonConvert.DeserializeObject<SpriteSheet>(fileContents, settings);
            spriteSheet.SpriteName = Path.GetFileNameWithoutExtension(_path);
            return spriteSheet;
        }
    }
}