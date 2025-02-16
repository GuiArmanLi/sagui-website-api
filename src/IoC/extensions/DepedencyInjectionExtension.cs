using Microsoft.Extensions.DependencyInjection;

public static class DepedencyInjectionExtension
{
    public static IServiceCollection AddDependencyInjectionExtension(this IServiceCollection services)
    {
        services.AddScoped<IIdentityService, IdentityService>();

        return services;
    }
}