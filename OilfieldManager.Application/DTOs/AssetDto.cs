namespace OilfieldManager.Application.DTOs;

public class AssetDto
{
    public Guid Id { get; set; }
    public string SerialNumber { get; set; } = string.Empty;
    public string Model { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public Guid? WellId { get; set; }
    public string? WellName { get; set; } // Flat property, no nested object loop!
}
