using Microsoft.OpenApi.Models;

namespace API.extensions;

public static class CorsExtension
{
    public static IServiceCollection AddCorsExtension(this IServiceCollection service)
    {
        service.AddCors(options =>
            options.AddPolicy("DefaultPolicy", builder =>
                builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader())); //Configurar depois para somente acesso ao angular

        return service;
    }
}