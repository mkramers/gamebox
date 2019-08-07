using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Abstractions;
using System.Linq;
using System.Text;
using Common;

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

    public class FileWriter
    {
        private readonly FileSystem m_fileSystem;

        protected FileWriter(FileSystem _fileSystem)
        {
            m_fileSystem = _fileSystem;
        }

        public FileWriter() : this(new FileSystem())
        {
        }

        public void WriteFile(string _filePath, string _contents)
        {
            m_fileSystem.File.WriteAllText(_filePath, _contents);
        }
    }

    public static class SpriteResourceEnumGenerator
    {
        public static string GenerateEnumNames(string _asepriteFilePath, string _rootSpriteDirectory)
        {
            string spriteDirectory = Path.GetDirectoryName(_asepriteFilePath);
            string subDirectory = PathUtilities.GetRelativePath(_rootSpriteDirectory, spriteDirectory);

            string[] names = subDirectory.TrimStart('.').Split("\\", StringSplitOptions.RemoveEmptyEntries);

            string enumName = string.Join("_", names.Select(_name => _name.ToUpper()));

            return enumName;
        }

        public static IEnumerable<string> GetAsepriteFiles(string _resourceDirectory)
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

    public static class EnumCsGenerator
    {
        public static string GenerateEnumCs(IEnumerable<string> _enumNames, string _enumName, string _namespace)
        {
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.AppendLine($"namespace {_namespace}");
            stringBuilder.AppendLine("{");
            stringBuilder.AppendLine($"\tpublic enum {_enumName}");
            stringBuilder.AppendLine("\t{");

            stringBuilder.AppendLine(string.Join("\n", _enumNames.Select(_enum => $"\t\t{_enum},")).TrimEnd(','));

            stringBuilder.AppendLine("\t}");
            stringBuilder.AppendLine("}");

            return stringBuilder.ToString();
        }
    }

    public static class AsepriteExporter
    {
        public static void Export(string _asepriteFilePath)
        {
            string fileName = Path.GetFileNameWithoutExtension(_asepriteFilePath);
            string workingDirectory = Path.GetDirectoryName(_asepriteFilePath);
            string outputDirectory = Path.Combine(workingDirectory, "gen");

            Directory.CreateDirectory(outputDirectory);

            string outputPngFormat = Path.Combine(outputDirectory, $"{fileName}-{{layer}}.png");
            string outputJsonFormat = Path.Combine(outputDirectory, $"{fileName}.json");

            string arguments =
                $@"-b {fileName}.aseprite --save-as {outputPngFormat} --data {outputJsonFormat} --list-layers --format json-array";

            ProcessStartInfo processStartInfo = new ProcessStartInfo
            {
                FileName = "aseprite",
                Arguments = arguments,
                WorkingDirectory = workingDirectory,
            };

            Process process = Process.Start(processStartInfo);
            Debug.Assert(process != null);

            process.WaitForExit();

            if (process.ExitCode != 0)
            {
                throw new Exception($"Failed to export Asesprite. Exited with error code = {process.ExitCode}");
            }
        }
    }
}
