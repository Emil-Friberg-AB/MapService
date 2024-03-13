namespace Domain.Clients.DTOs.Request;

public class GetRouteRequestDto
{
    public LocationWrapperDto Origin { get; set; }
    public LocationWrapperDto Destination { get; set; }
    public string TravelMode { get; set; }
    public string RoutingPreference { get; set; }
    public string DepartureTime { get; set; }
    public bool ComputeAlternativeRoutes { get; set; }
    public RouteModifiersDto RouteModifiers { get; set; }
    public string LanguageCode { get; set; }
    public string Units { get; set; }
}
