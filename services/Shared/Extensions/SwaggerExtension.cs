using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace SharedKernel.Extensions
{
    public static class SwaggerExtension
    {
        public static void ConfigureSwagger(this IServiceCollection services, string domainName = "")
        {
            services.AddSwaggerGen(c =>
            {
                c.SchemaFilter<EnumSchemaFilter>();
                c.DocumentFilter<EnumTypesDocumentFilter>();
                c.EnableAnnotations();

                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = $"Let's Code {domainName}",
                    Description = $"API Let's Code {domainName}",
                    Contact = new OpenApiContact
                    {
                        Name = $"Let's Code {domainName}",
                        Email = string.Empty,
                    }
                });
            });
        }

        public static IApplicationBuilder UseSwaggerUICustom(this IApplicationBuilder app)
        {
            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "LET'S CODE v1");
                //c.RoutePrefix = ;
            });

            return app;
        }
    }
}