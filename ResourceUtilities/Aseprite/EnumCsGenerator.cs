using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ResourceUtilities.Aseprite
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

            return stringBuilder.ToString().TrimEnd();
        }
    }
}