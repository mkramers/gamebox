using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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

            string[] asepriteFiles = Directory.GetFiles(resourceDirectory, "*.aseprite", SearchOption.AllDirectories);
            foreach (string asepriteFile in asepriteFiles)
            {
                int exitCode = AsepriteExporter.Export(asepriteFile);
                if (exitCode != 0)
                {
                    return exitCode;
                }
            }

            List<string> enumNames = new List<string>();
            foreach (string asepriteFile in asepriteFiles)
            {
                string enumName = SpriteResourceEnumGenerator.GenerateEnumNames(asepriteFile, resourceDirectory);
                enumNames.Add(enumName);
            }

            string enumCs = EnumCsGenerator.GenerateEnumCs(enumNames, "SpriteResources", "Resources");

            return 0;
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
        public static int Export(string _asepriteFilePath)
        {
            string fileName = Path.GetFileNameWithoutExtension(_asepriteFilePath);
            string workingDirectory = Path.GetDirectoryName(_asepriteFilePath);
            string outputDirectory = Path.Combine(workingDirectory, "gen");

            Directory.CreateDirectory(outputDirectory);

            string outputPngFormat = Path.Combine(outputDirectory, $"{fileName}-{{layer}}.png");
            string outputJsonFormat = Path.Combine(outputDirectory, $"{fileName}.json");

            string arguments =
                $@"aseprite -b {fileName}.aseprite --save-as {outputPngFormat} --data {outputJsonFormat} --list-layers --format json-array";

            ProcessStartInfo processStartInfo = new ProcessStartInfo
            {
                FileName = "aseprite",
                Arguments = arguments,
                WorkingDirectory = workingDirectory,
            };

            Process process = Process.Start(processStartInfo);
            Debug.Assert(process != null);

            process.WaitForExit();

            return process.ExitCode;
        }
    }
}
