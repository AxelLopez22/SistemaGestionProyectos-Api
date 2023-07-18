using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;

namespace Api_ProjectManagement.Configurations
{
    public static class ConfigurationSwagger
    {
        public static void AddSwaggerConfig(this IServiceCollection services, string titleSwagger)
        {
            services.AddSwaggerGen(options =>
            {
                var groupName = "v1";

                options.SwaggerDoc(groupName, new OpenApiInfo
                {
                    Title = $"{titleSwagger} {groupName}.0.1",
                    Version = groupName,
                    Description = "API - ProjectManagement",
                    Contact = new OpenApiContact
                    {
                        Name = "Axel Lopez",
                        Email = "axellopezbrizuela3@gmail.com",
                    },
                });

                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "Encabezado de autorización de JWT usando el esquema Bearer. \r\n\r\n Ingrese 'Bearer' [espacio] y luego su token en la entrada de texto a continuación. \r\n\r\n Ejemplo: \" Bearer 12345abcdef \"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header,

                        },
                        new List<string>()
                    }
                });
            });
        }
    }
}
