namespace API.Test.Controllers;

using API.Controllers;
using API.DTOs.Request;
using API.DTOs.Response;
using API.Exceptions;
using Application.Queries;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System.Net;
using System.Threading;
using Xunit;

public class RouteControllerTests
{
    private readonly Mock<IMediator> _mediatorMock;
    private readonly Mock<ILogger<RouteController>> _loggerMock;
    private readonly RouteController _controller;

    public RouteControllerTests()
    {
        _mediatorMock = new Mock<IMediator>();
        _loggerMock = new Mock<ILogger<RouteController>>();
        _controller = new RouteController(_mediatorMock.Object, _loggerMock.Object);
    }

    [Fact]
    public void Get_Should_Return_OkResult()
    {
        // Act
        var result = _controller.Get();

        // Assert
        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task GetRoute_Should_Return_Ok_When_Request_Is_Valid()
    {
        // Arrange
        GetRouteRequestDto request = MockedGetRouteRequestDto();

        Route route = MockedRoute();

        _mediatorMock.Setup(m => m.Send(It.IsAny<GetRouteQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(route);

        // Act
        var result = await _controller.GetRoute(request);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var response = Assert.IsType<BaseResponseDto<RouteDto>>(okResult.Value);
        Assert.Equal(route.Id, response.Result.Id);
        Assert.Equal(route.Status, response.Result.Status);
        Assert.Equal(route.RouteDetails.Count, response.Result.RouteDetails.Count);

        for (int i = 0; i < route.RouteDetails.Count; i++)
        {
            Assert.Equal(route.RouteDetails[i].DistanceMeters, response.Result.RouteDetails[i].DistanceMeters);
            Assert.Equal(route.RouteDetails[i].Duration, response.Result.RouteDetails[i].Duration);
            Assert.NotEmpty(response.Result.RouteDetails[i].Polyline.DecodedPolyLine);
            Assert.IsType<List<LatLng>>(response.Result.RouteDetails[i].Polyline.DecodedPolyLine);

        }
    }

    [Fact]
    public async Task GetRoute_Should_Return_ErrorResponse_When_BadRequestException_Is_Thrown()
    {
        // Arrange
        var formValidationError = new List<KeyValuePair<string, object>>();
        GetRouteRequestDto request = MockedGetRouteRequestDto();
        _mediatorMock.Setup(m => m.Send(It.IsAny<GetRouteQuery>(), It.IsAny<CancellationToken>()))
            .ThrowsAsync(new BadRequestException("Test exception", formValidationError));

        // Act
        var result = await _controller.GetRoute(request);

        // Assert
        var objectResult = Assert.IsType<ObjectResult>(result);
        Assert.Equal((int)HttpStatusCode.BadRequest, objectResult.StatusCode);
    }

    [Fact]
    public async Task GetRoute_Should_Return_ErrorResponse_When_Exception_Is_Thrown()
    {
        // Arrange
        GetRouteRequestDto request = MockedGetRouteRequestDto();
        _mediatorMock.Setup(m => m.Send(It.IsAny<GetRouteQuery>(), It.IsAny<CancellationToken>()))
            .ThrowsAsync(new Exception());

        // Act
        var result = await _controller.GetRoute(request);

        // Assert
        var objectResult = Assert.IsType<ObjectResult>(result);
        Assert.Equal((int)HttpStatusCode.InternalServerError, objectResult.StatusCode);
    }


    private static Route MockedRoute()
    {
        return new Route
        {
            Id = Guid.NewGuid(),
            RouteDetails = new List<RouteDetail>
    {
        new RouteDetail
        {
            DistanceMeters = 1000,
            Duration = "10 mins",
            Polyline = new Polyline
            {
                EncodedPolyline = "}~kvHmzrr@ba\\hnc@jiu@r{Zqx~@hjp@pwEhnc@zhu@zflAbxn@fhjBvqHroaAgcnAp}gAeahAtqGkngAinc@_h|@r{Zad\\y|_D}_y@swg@ysg@}llBpoZqa{@xrw@~eBaaX}{uAero@uqGadY}nr@`dYs_NquNgbjAf{l@|yh@bfc@}nr@z}q@i|i@zgz@r{ZhjFr}gApob@ff}@laIsen@dgYhdPvbIren@"
            }
        },
        new RouteDetail
        {
            DistanceMeters = 2000,
            Duration = "20 mins",
            Polyline = new Polyline
            {
                EncodedPolyline = "}~kvHmzrr@ba\\hnc@jiu@r{Zqx~@hjp@pwEhnc@zhu@zflAbxn@fhjBvqHroaAgcnAp}gAeahAtqGkngAinc@_h|@r{Zad\\y|_D}_y@swg@ysg@}llBpoZqa{@xrw@~eBaaX}{uAero@uqGadY}nr@`dYs_NquNgbjAf{l@|yh@bfc@}nr@z}q@i|i@zgz@r{ZhjFr}gApob@ff}@laIsen@dgYhdPvbIren@"
            }
        }
    },
            Status = "Active"
        };
    }

    private static GetRouteRequestDto MockedGetRouteRequestDto()
    {
        var request = new GetRouteRequestDto
        {
            Origin = new LocationWrapperDto
            {
                Location = new LocationDto
                {
                    LatLng = new LatLngDto { Latitude = 50, Longitude = 50 }
                }
            },
            Destination = new LocationWrapperDto
            {
                Location = new LocationDto
                {
                    LatLng = new LatLngDto { Latitude = 60, Longitude = 60 }
                }
            },
            TravelMode = "DRIVING",
            RoutingPreference = "FASTEST",
            DepartureTime = "2022-12-31T23:59:59",
            ComputeAlternativeRoutes = true,
            RouteModifiers = new RouteModifiersDto
            {
                // Set properties here
            },
            LanguageCode = "en-US",
            Units = "IMPERIAL"
        };
        return request;
    }
}


