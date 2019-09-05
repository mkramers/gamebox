using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Abstractions;
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

            Console.WriteLine(
                $"Starting ResourceGenerator with args:\n\tResource Directory: {resourceDirectory,-30}\n\tOutput File Path: {outputFilePath,-30}");

            try
            {
                Export(resourceDirectory);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return 1;
            }

            string[] pngFilePaths = Directory.GetFiles(resourceDirectory, "*.png", SearchOption.AllDirectories);

            IEnumerable<string> fileNames = pngFilePaths.Select(_pngFilePath => GetModifiedFileName(_pngFilePath, resourceDirectory));

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

        private static string GetModifiedFileName(string _pngFilePath, string _rootDirectory)
        {
            IFileSystem fileSystem = new FileSystem();

            string relativePath = fileSystem.Path.GetRelativePath(_rootDirectory, _pngFilePath);

            return relativePath;
        }

        private static string GetUsageText()
        {
            return "Usage:\n\tResourceGenerator.exe <resourcedir> <outputdir>";
        }

        private static void FindAndWriteEnumsToCs(IEnumerable<string> _asepriteFiles, string _resourceDirectory,
            string _outputDirectory)
        {
            Console.WriteLine("Generating enum cs file...");

            EnumFromPath enumFromPath = new EnumFromPath();
            IEnumerable<string> enumNames = _asepriteFiles.Select(enumFromPath.GetEnumFromPath);

            const string enumName = "SpriteResources";

            string enumCs = EnumCsGenerator.GenerateEnumCs(enumNames, enumName, "ResourceUtilities.Aseprite");

            string outputFilePath = Path.Combine(_outputDirectory, $"{enumName}.cs");

            Console.WriteLine($"\n<{outputFilePath}>:\n{enumCs}");

            File.WriteAllText(outputFilePath, enumCs);
        }

        private static void Export(string _resourceDirectory)
        {
            string[] asepriteFiles = SpriteResourceEnumGenerator.GetAsepriteFiles(_resourceDirectory).ToArray();

            Console.WriteLine($"Found {asepriteFiles.Length} aseprite files.");

            foreach (string asepriteFile in asepriteFiles)
            {
                Console.WriteLine($"Exporting {asepriteFile}...");
                AsepriteExporter.Export(asepriteFile);
            }
        }
    }
}