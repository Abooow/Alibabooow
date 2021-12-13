using Alibabooow.Api.DataAccessModels;
using Microsoft.EntityFrameworkCore;

namespace Alibabooow.Api.Data;

public class ApplicationDbContext : DbContext
{
    public DbSet<UserRecord> Users { get; set; }
    public DbSet<OrderRecord> Orders { get; set; }
    public DbSet<OrderDetailRecord> OrderDetails { get; set; }
    public DbSet<ProductRecord> Products { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<OrderDetailRecord>()
            .HasOne(x => x.Order)
            .WithMany()
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<OrderDetailRecord>()
            .HasOne(x => x.Product)
            .WithMany()
            .OnDelete(DeleteBehavior.NoAction);
    }
}
