using System;
using System.Collections.Generic;
using System.Linq;
using IOUtilities;
using ResourceUtilities.Aseprite;

namespace ResourceGenerator
{
    internal static class Program
    {
        private static int Main(string[] _args)
        {
            const string resourceDirectory = @"C:\dev\GameBox\Resources\sprite";
            const string outputFilePath = @"C:\dev\GameBox\Resources\out\SpriteResources.cs";

            IEnumerable<string> asepriteFiles;
            try
            {
                asepriteFiles = SpriteResourceEnumGenerator.Export(resourceDirectory);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return 1;
            }

            try
            {
                FindAndWriteEnumsToCs(asepriteFiles, resourceDirectory, outputFilePath);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return 2;
            }
            
            return 0;
        }

        private static void FindAndWriteEnumsToCs(IEnumerable<string> _asepriteFiles, string _resourceDirectory, string _outputFilePath)
        {
            IEnumerable<string> enumNames = _asepriteFiles.Select(_asepriteFile =>
                SpriteResourceEnumGenerator.GenerateEnumNames(_asepriteFile, _resourceDirectory));

            string enumCs = EnumCsGenerator.GenerateEnumCs(enumNames, "SpriteResources", "Resources");

            FileWriter writer = new FileWriter();
            writer.WriteFile(_outputFilePath, enumCs);
        }
    }
}
