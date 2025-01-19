public static class DepedencyInjectionExtension
{
    public static IServiceCollection AddDependencyInjectionExtension(this IServiceCollection service)
    {
        service.AddScoped<IUserService, UserService>();

        return service;
    }
}