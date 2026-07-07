using Microsoft.EntityFrameworkCore;
using OilfieldManager.Domain.Entities;

namespace OilfieldManager.Infrastructure.Data;

public class OilfieldDbContext : DbContext
{
    public OilfieldDbContext(DbContextOptions<OilfieldDbContext> options) : base(options) { }

    public DbSet<Asset> Assets => Set<Asset>();
    public DbSet<WellSite> WellSites => Set<WellSite>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Define a 1-to-Many relationship (A Well Site has many Assets)
        modelBuilder.Entity<WellSite>()
            .HasMany(w => w.DeployedAssets)
            .WithOne(a => a.CurrentWell)
            .HasForeignKey(a => a.CurrentWellId)
            .OnDelete(DeleteBehavior.SetNull); // If well closes, assets return to inventory
    }
}
