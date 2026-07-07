namespace OilfieldManager.Domain.Entities
{
    public class WellSite
    {
        public Guid Id { get; set; }
        public string WellName { get; set; } = string.Empty;
        public string LeaseNumber { get; set; } = string.Empty;
        // public string Location { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public List<Asset> DeployedAssets { get; set; } = []; // Navigation property to the Asset entity
    }
}