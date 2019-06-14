using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace ResourceUtilities.Aseprite
{
    public class SpriteSheetLoader
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

            SpriteSheet dFile2 = JsonConvert.DeserializeObject<SpriteSheet>(fileContents, settings);
            return dFile2;
        }
    }
}