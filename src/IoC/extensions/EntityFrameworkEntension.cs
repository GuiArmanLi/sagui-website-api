using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

public static class EntityFrameworkEntension
{
    public static IServiceCollection AddEntityFramework(this IServiceCollection service, IConfiguration configuration)
    {
        var connection = configuration.GetConnectionString("MySqlConnection");

        service.AddDbContext<DataContext>(options => options.UseInMemoryDatabase("MemoryDatabase"));
        service.AddDbContext<IdentityDataContext>(options => options.UseInMemoryDatabase("MemoryEntity"));
        
        return service;
    }
}