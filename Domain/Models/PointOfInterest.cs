namespace Domain.Models;

public class PointOfInterest
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Type { get; set; }
    public Location Location { get; set; }
}
