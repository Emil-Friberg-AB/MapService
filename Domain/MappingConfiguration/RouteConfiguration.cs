using Domain.Clients.DTOs.Response;
using Mapster;
using Route = Domain.Models.Route;

namespace Domain.MappingConfiguration
{
    public static class RouteConfiguration
    {
        public static void Configure()
        {
            TypeAdapterConfig<GetRouteResponseDto, Route>.NewConfig()
                .Map(dest => dest.RouteDetails, src => src.Routes)
                .Map(dest => dest.Status, src => src.Status);

            // Add other mapping configurations related to 'Route' here...
        }
    }
}
