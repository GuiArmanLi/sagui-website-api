using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

public static class EntityFrameworkEntension
{
    public static IServiceCollection AddEntityFramework(this IServiceCollection service)
    {
        service.AddDbContext<AppDbContext>(options => options.UseInMemoryDatabase("MemoryDatabase"));
        return service;
    }
}