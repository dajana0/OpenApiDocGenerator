using Microsoft.OpenApi.Extensions;
using Microsoft.OpenApi.Models;
using System.Text;

namespace OpenApiDocGenerator.Helper;

public static class SchemaHelper
{
    public static string FormatSchema(OpenApiSchema schema, int indent = 0)
    {
        if (schema == null) return "N/A";

        var sb = new StringBuilder();
        var indentStr = new string(' ',   * 2);

        // Podstawowe informacje o schemacie
        if (!string.IsNullOrEmpty(schema.Type))
        {
            sb.AppendLine($"{indentStr}Type: {schema.Type}");
        }

        // Właściwości dla obiektów
        if (schema.Properties?.Any() == true)
        {
            sb.AppendLine($"{indentStr}Properties:");
            foreach (var prop in schema.Properties)
            {
                sb.AppendLine($"{indentStr}  {prop.Key}:");
                sb.Append(FormatSchema(prop.Value, indent + 2));
            }
        }

        // Informacje o tablicach
        if (schema.Items != null)
        {
            sb.AppendLine($"{indentStr}Items:");
            sb.Append(FormatSchema(schema.Items, indent + 1));
        }

        // Dodatkowe atrybuty
        if (schema.Required?.Any() == true)
        {
            sb.AppendLine($"{indentStr}Required: {string.Join(", ", schema.Required)}");
        }

        if (schema.Enum?.Any() == true)
        {

            sb.AppendLine($"{indentStr}Enum: {string.Join(", ", schema.Enum.ToString())}");
        }

        return sb.ToString();
    }
}
