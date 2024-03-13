using API.DTOs.Response;
using Mapster;

namespace API.MappingConfiguration
{
    public class RouteResponseMapping
    {
        public static void Configure()
        {
            TypeAdapterConfig<RouteDto, Domain.Models.Route>.NewConfig()
                .Map(dest => dest.Id, src => src.Id)
                .Map(dest => dest.Status, src => src.Status)
                .Map(dest => dest.RouteDetails, src => src.RouteDetails);

            TypeAdapterConfig<RouteDetailDto, Domain.Models.RouteDetail>.NewConfig()
                .Map(dest => dest.DistanceMeters, src => src.DistanceMeters)
                .Map(dest => dest.Duration, src => src.Duration)
                .Map(dest => dest.Polyline, src => src.Polyline);

            TypeAdapterConfig<PolylineDto, Domain.Models.Polyline>.NewConfig()
                .Map(dest => dest.DecodedPolyLine, src => src.DecodedPolyLine);

        }
    }
}
