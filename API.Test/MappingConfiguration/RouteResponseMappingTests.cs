using Xunit;
using API.DTOs.Response;
using Domain.Models;
using API.MappingConfiguration;
using Mapster;
using Route = Domain.Models.Route;

namespace API.Test.MappingConfiguration;
public class RouteResponseMappingTests
{
    [Fact]
    public void RouteDto_Should_Map_To_Route()
    {
        // Arrange
        RouteResponseMapping.Configure();
        var routeDto = new RouteDto
        {
            Id = Guid.NewGuid(),
            Status = "Test Status",
            RouteDetails = new List<RouteDetailDto>()
        };

        // Act
        var route = routeDto.Adapt<Route>();

        // Assert
        Assert.Equal(routeDto.Id, route.Id);
        Assert.Equal(routeDto.Status, route.Status);
        Assert.Equal(routeDto.RouteDetails.Count, route.RouteDetails.Count);
    }

    [Fact]
    public void RouteDetailDto_Should_Map_To_RouteDetail()
    {
        // Arrange
        RouteResponseMapping.Configure();
        var routeDetailDto = new RouteDetailDto
        {
            DistanceMeters = 1000,
            Duration = "Test",
            Polyline = new PolylineDto { DecodedPolyLine = new List<LatLng>() }
        };

        // Act
        var routeDetail = routeDetailDto.Adapt<RouteDetail>();

        // Assert
        Assert.Equal(routeDetailDto.DistanceMeters, routeDetail.DistanceMeters);
        Assert.Equal(routeDetailDto.Duration, routeDetail.Duration);
        Assert.Equal(routeDetailDto.Polyline.DecodedPolyLine, routeDetail.Polyline.DecodedPolyLine);
    }

    [Fact]
    public void PolylineDto_Should_Map_To_Polyline()
    {
        // Arrange
        RouteResponseMapping.Configure();
        var polylineDto = new PolylineDto { DecodedPolyLine = new List<LatLng>() };

        // Act
        var polyline = polylineDto.Adapt<Polyline>();

        // Assert
        Assert.Equal(polylineDto.DecodedPolyLine, polyline.DecodedPolyLine);
    }
}
