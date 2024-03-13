using Domain.Models;

namespace API.DTOs.Response
{
    public class PolylineDto
    {
        public List<LatLng> DecodedPolyLine { get; set; }
    }
}
