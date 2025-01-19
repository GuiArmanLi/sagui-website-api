using Microsoft.OpenApi.Models;

namespace athletic.WebAPI.extensions;

public static class SwaggerExtension
{
    public static void AddSwaggerExtension(this IServiceCollection services)
    {
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1",
                new OpenApiInfo
                {
                    Title = "athletic API",
                    Version = "v1",
                    Description = "API to manage an app in angular",
                    Contact = new OpenApiContact
                    {
                        Name = "Guilherme Santana Pessa",
                        Email = "guilherme.pessa@fatec.sp.gov.br",
                        Url = new Uri("https://www.guilhermepessa.dev.br")
                    },
                    License = new OpenApiLicense
                    {
                        Name = "MIT License",
                        Url = new Uri("https://opensource.org/licenses/MIT")
                    }
                });
            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Description = @"JWT Authorization header using the Bearer scheme. 
                                Enter 'Bearer' [space] and then your token in the text input below. 
                                Example: 'Bearer 12345abcdef'",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                Scheme = "Bearer"
            });

            options.AddSecurityRequirement(new OpenApiSecurityRequirement()
            {
                {
                    new OpenApiSecurityScheme()
                    {
                        Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" },
                        Scheme = "JWT",
                        Name = "Bearer",
                        In = ParameterLocation.Header
                    },
                    new List<string> { "admin", "contributor", "common" }
                }
            });
        });
    }

    public static void UseSwaggerExtension(this IApplicationBuilder app)
    {
        app.UseSwagger();
        app.UseSwaggerUI(s => s.SwaggerEndpoint("/swagger/v1/swagger.json", "v1"));
    }
}