using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

public class IdentityDataContext : IdentityDbContext<User>
{
    public IdentityDataContext(DbContextOptions<IdentityDataContext> options) : base(options) { }
}