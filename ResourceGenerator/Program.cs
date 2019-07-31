using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace ResourceGenerator
{
    internal static class Program
    {
        private static int Main(string[] _args)
        {
            const string resourceDirectory = @"C:\dev\GameBox\Resources";

            string[] asepriteFiles = Directory.GetFiles(resourceDirectory, "*.aseprite", SearchOption.AllDirectories);
            foreach (string asepriteFile in asepriteFiles)
            {
                int exitCode = AsepriteExporter.Export(asepriteFile);
                if (exitCode != 0)
                {
                    return exitCode;
                }
            }

            return 0;
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
