using System;
using System.Collections.Generic;
using System.IO.Abstractions;
using System.Linq;
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
}
