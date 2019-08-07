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
            if (_args?.Length != 2)
            {
                Console.WriteLine(GetUsageText());
                return -1;
            }

            //const string resourceDirectory = @"C:\dev\GameBox\Resources\sprite";
            //const string outputFilePath = @"C:\dev\GameBox\Resources\out\SpriteResources.cs";
            string resourceDirectory = _args[0];
            string outputFilePath = _args[1];

            Console.WriteLine($"Starting ResourceGenerator with args:\n\tResource Directory: {resourceDirectory, -30}\n\tOutput File Path: {outputFilePath, -30}");

            IEnumerable<string> asepriteFiles;
            try
            {
                asepriteFiles = Export(resourceDirectory);
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

            Console.WriteLine("Success");
            return 0;
        }

        private static string GetUsageText()
        {
            return "Usage:\n\tResourceGenerator.exe <resourcedir> <outputfilepath>";
        }

        private static void FindAndWriteEnumsToCs(IEnumerable<string> _asepriteFiles, string _resourceDirectory, string _outputFilePath)
        {
            Console.WriteLine($"Generating enum cs file...");

            IEnumerable<string> enumNames = _asepriteFiles.Select(_asepriteFile =>
                EnumFromPath.GetEnumFromPath(_asepriteFile, _resourceDirectory));

            string enumCs = EnumCsGenerator.GenerateEnumCs(enumNames, "SpriteResources", "Resources");

            Console.WriteLine($"\n<{_outputFilePath}>:\n{enumCs}");

            FileWriter writer = new FileWriter();
            writer.WriteFile(_outputFilePath, enumCs);
        }

        private static IEnumerable<string> Export(string _resourceDirectory)
        {
            string[] asepriteFiles = SpriteResourceEnumGenerator.GetAsepriteFiles(_resourceDirectory).ToArray();

            Console.WriteLine($"Found {asepriteFiles.Length} aseprite files.");

            foreach (string asepriteFile in asepriteFiles)
            {
                Console.WriteLine($"Exporting {asepriteFile}...");
                AsepriteExporter.Export(asepriteFile);
            }

            return asepriteFiles;
        }
    }
}
