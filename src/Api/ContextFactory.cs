using Alibabooow.Api.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Alibabooow.Api;

// Used for creating the Migrations. Doesn't work for some reason otherwise.
public class ContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
{
    public ApplicationDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
        optionsBuilder.UseSqlServer();

        return new ApplicationDbContext(optionsBuilder.Options);
    }
}
