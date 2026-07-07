using OilfieldManager.Domain.Enums;
namespace OilfieldManager.Domain.Entities
{
    public class Asset
    {
        public Guid Id { get; set; }
        public string SerialNumber { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public AssetStatus Status { get; set; } // Enum: Active, Inactive, Maintenance, Retired
        public Guid? CurrentWellId { get; set; } // Nullable, as an asset may not be assigned to a well
        public WellSite? CurrentWell { get; set; } // Navigation property to the WellSite entity
    }
}