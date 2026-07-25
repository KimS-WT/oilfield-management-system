using OilfieldManager.Domain.Entities;
using OilfieldManager.Domain.Enums;


namespace OilfieldManager.Infrastructure.Data;

public static class DbInitializer
{
    public static async Task SeedAsync(OilfieldDbContext context)
    {
        // 1. Ensure the database is physically created
        await context.Database.EnsureCreatedAsync();

        // 2. Look for any existing data. If found, stop seeding.
        if (context.WellSites.Any() || context.Assets.Any())
        {
            return;
        }

        // 3. Create Mock Well Sites (Simulating active Permian Basin rigs)
        var wellSites = new List<WellSite>
        {
            new() {
                Id = Guid.NewGuid(),
                WellName = "Permian-Alpha-01",
                LeaseNumber = "TX-EF-84729",
                Latitude = 31.9686,
                Longitude = -102.0779
            },
            new() {
                Id = Guid.NewGuid(),
                WellName = "Bakken-Beta-14",
                LeaseNumber = "ND-BK-11043",
                Latitude = 48.1924,
                Longitude = -103.6211
            }
        };

        await context.WellSites.AddRangeAsync(wellSites);

        // 4. Create Mock Heavy Assets (Some assigned to wells, some in storage)
        var assets = new List<Asset>
        {
            new() {
                Id = Guid.NewGuid(),
                SerialNumber = "HAL-DP-9001",
                Model = "Premium 5-Inch Drill Pipe",
                Status = AssetStatus.Active,
                CurrentWell = wellSites[0] // Assigned to Permian-Alpha-01
            },
            new() {
                Id = Guid.NewGuid(),
                SerialNumber = "HAL-ESP-4412",
                Model = "Centrilift Electric Submersible Pump",
                Status = AssetStatus.Active,
                CurrentWell = wellSites[1] // Assigned to Bakken-Beta-14
            },
            new() {
                Id = Guid.NewGuid(),
                SerialNumber = "HAL-BIT-7731",
                Model = "Tricone Diamond Drill Bit",
                Status = AssetStatus.Inactive,
                CurrentWell = null // Sitting in a warehouse warehouse
            },
            new() {
                Id = Guid.NewGuid(),
                SerialNumber = "HAL-MTR-0082",
                Model = "Downhole Mud Motor",
                Status = AssetStatus.Maintenance,
                CurrentWell = null // Undergoing repairs
            }
        };

        await context.Assets.AddRangeAsync(assets);

        // 5. Commit changes to SQLite
        await context.SaveChangesAsync();
    }

}
