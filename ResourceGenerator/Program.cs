using System;
using System.Collections.Generic;
using System.IO;
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

            string[] pngFilePaths = Directory.GetFiles(resourceDirectory, "*.png", SearchOption.AllDirectories);

            List<string> fileNames = new List<string>();
            foreach (string pngFilePath in pngFilePaths)
            {
                string directory = Path.GetDirectoryName(pngFilePath);
                string fileName = Path.GetFileName(pngFilePath);
                string[] splitName = fileName.Split(new[] {"-"}, StringSplitOptions.RemoveEmptyEntries);
                string spriteName = splitName.First();
                string layerName = splitName.Last();

                string parentDirectory = Directory.GetParent(directory).FullName;

                string modifiedFileName = Path.Combine(parentDirectory, spriteName, layerName);
                fileNames.Add(modifiedFileName);
            }

            try
            {
                FindAndWriteEnumsToCs(fileNames, resourceDirectory, outputFilePath);
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
            return "Usage:\n\tResourceGenerator.exe <resourcedir> <outputdir>";
        }

        private static void FindAndWriteEnumsToCs(IEnumerable<string> _asepriteFiles, string _resourceDirectory, string _outputDirectory)
        {
            Console.WriteLine($"Generating enum cs file...");

            IEnumerable<string> enumNames = _asepriteFiles.Select(_asepriteFile =>
                EnumFromPath.GetEnumFromPath(_asepriteFile, _resourceDirectory));

            const string enumName = "SpriteResources";

            string enumCs = EnumCsGenerator.GenerateEnumCs(enumNames, enumName, "ResourceUtilities.Aseprite");

            string outputFilePath = Path.Combine(_outputDirectory, $"{enumName}.cs");

            Console.WriteLine($"\n<{outputFilePath}>:\n{enumCs}");

            FileWriter writer = new FileWriter();
            writer.WriteFile(outputFilePath, enumCs);
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
