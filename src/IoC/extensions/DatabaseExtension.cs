using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

public static class DatabaseExtension
{
    public static IServiceCollection AddEntityFramework (this IServiceCollection service){
        service.AddDbContext<AppDbContext>(options => options.UseInMemoryDatabase("MemoryDatabase"));
        return service;
    }
}