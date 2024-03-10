namespace Domain.Models;

public class Road
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public Location StartLocation { get; set; }
    public Location EndLocation { get; set; }
    public double? Length { get; set; }
    public string Type { get; set; }
    public int SpeedLimit { get; set; }
    public string? Condition { get; set; }
    public string? TrafficLevel { get; set; }
}
