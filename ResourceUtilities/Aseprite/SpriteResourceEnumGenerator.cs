using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Common;
using IOUtilities;

namespace ResourceUtilities.Aseprite
{
    public static class SpriteResourceEnumGenerator
    {
        public static string GenerateEnumNames(string _asepriteFilePath, string _rootSpriteDirectory)
        {
            string spriteDirectory = Path.GetDirectoryName(_asepriteFilePath);
            string subDirectory = PathUtilities.GetRelativePath(_rootSpriteDirectory, spriteDirectory);

            string[] names = subDirectory.TrimStart('.').Split(new[] { "\\" }, StringSplitOptions.RemoveEmptyEntries);

            string enumName = string.Join("_", names.Select(_name => _name.ToUpper()));

            return enumName;
        }

        private static IEnumerable<string> GetAsepriteFiles(string _resourceDirectory)
        {
            string[] asepriteFiles = Directory.GetFiles(_resourceDirectory, "*.aseprite", SearchOption.AllDirectories);
            return asepriteFiles;
        }

        public static IEnumerable<string> Export(string _resourceDirectory)
        {
            string[] asepriteFiles = GetAsepriteFiles(_resourceDirectory).ToArray();

            foreach (string asepriteFile in asepriteFiles)
            {
                AsepriteExporter.Export(asepriteFile);
            }

            return asepriteFiles;
        }
    }
}