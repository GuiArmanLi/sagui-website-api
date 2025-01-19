using Microsoft.Extensions.DependencyInjection;

public static class DepedencyInjection {
    public static IServiceCollection AddDependencies (this IServiceCollection service){
        service.AddScoped<IUserService, UserService>(); 

        return service;
    }
}