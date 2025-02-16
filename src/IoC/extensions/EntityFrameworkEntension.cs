using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

public static class EntityFrameworkEntension
{
    public static IServiceCollection AddEntityFramework(this IServiceCollection service, IConfiguration configuration)
    {
        var connection = configuration.GetConnectionString("MySql");

        service.AddDbContext<DataContext>(options => options.UseInMemoryDatabase("MemoryDatabase"));
        service.AddDbContext<IdentityDataContext>(options => options.UseInMemoryDatabase("MemoryDatabase"));

        // service.AddDbContext<DataContext>(o => o.UseMySql(connection, new MySqlServerVersion(new Version(9, 0, 1))));
        // service.AddDbContext<IdentityDataContext>(o => o.UseMySql(connection, new MySqlServerVersion(new Version(9, 0, 1))));

        return service;
    }
}