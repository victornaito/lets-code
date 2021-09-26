using System;
using System.Linq;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace SharedKernel.Extensions
{
    public class EnumSchemaFilter : ISchemaFilter
    {
        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        {
            if(schema.Enum != null && schema.Enum.Count > 0 &&
                context.Type != null && context.Type.IsEnum)
            {
                schema.Description += "<p>Membros:</p><ul>";

                var fullTypeName = context.Type.FullName;

                foreach (var enumMemberName in schema.Enum.OfType<OpenApiInteger>().Select(_ => _.Value))
                    schema.Description += @$"<li><i>{Convert.ToInt64(Enum.Parse(context.Type, enumMemberName.ToString()))}</i> - 
                    {EnumExtension.ObterDescricaoEnum((Enum)Enum.Parse(context.Type, enumMemberName.ToString()))}</li>";

                schema.Description += "</ul>";
            }
        }
    }
}