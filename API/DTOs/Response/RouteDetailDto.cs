namespace API.DTOs.Response
{
    public class RouteDetailDto
    {
        public int DistanceMeters { get; set; }
        public string Duration { get; set; }
        public PolylineDto Polyline { get; set; }
    }
}
