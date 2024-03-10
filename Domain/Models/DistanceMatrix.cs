namespace Domain.Models;

public class DistanceMatrix
{
    public Guid Id { get; set; }
    public Location Origin { get; set; }
    public Location Destination { get; set; }
    public string? Distance { get; set; }
    public string? Duration { get; set; }
    public string? TrafficModel { get; set; }
    public DateTime? DepartureTime { get; set; }
}
