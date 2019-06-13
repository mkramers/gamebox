using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace ResourceUtilities.Aseprite
{
    public class SpriteSheetLoader
    {
        public SpriteSheet LoadFromFile(string _path)
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
                System.IO.File.ReadAllText(_path);

            SpriteSheet dFile2 = JsonConvert.DeserializeObject<SpriteSheet>(fileContents, settings);
            return dFile2;
        }
    }
}
