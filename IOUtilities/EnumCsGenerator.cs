using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace IOUtilities
{
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

            string normalizedPath = Regex.Replace(stringBuilder.ToString().TrimEnd(), @"\r\n|\n\r|\n|\r", "\r\n");

            return normalizedPath;
        }
    }
}