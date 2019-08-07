using System;
using System.Diagnostics;
using System.IO;

namespace ResourceUtilities.Aseprite
{
    public static class AsepriteExporter
    {
        public static void Export(string _asepriteFilePath)
        {
            string fileName = Path.GetFileNameWithoutExtension(_asepriteFilePath);
            string workingDirectory = Path.GetDirectoryName(_asepriteFilePath);
            string outputDirectory = "gen";

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