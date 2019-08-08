using System.Collections.Generic;
using System.IO;

namespace ResourceUtilities.Aseprite
{
    public static class SpriteResourceEnumGenerator
    {
        public static IEnumerable<string> GetAsepriteFiles(string _resourceDirectory)
        {
            string[] asepriteFiles = Directory.GetFiles(_resourceDirectory, "*.aseprite", SearchOption.AllDirectories);
            return asepriteFiles;
        }
    }
}