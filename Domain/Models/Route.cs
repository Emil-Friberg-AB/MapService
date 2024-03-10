namespace Domain.Models;

public class Route
{
    public Guid Id { get; set; }
    public Location StartLocation { get; set; }
    public Location EndLocation { get; set; }
    public List<Road> Roads { get; set; }
    public List<PointOfInterest>? PointsOfInterest { get; set; }
    public string? Polyline { get; set; }
}
