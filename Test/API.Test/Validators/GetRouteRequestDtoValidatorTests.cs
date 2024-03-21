namespace API.Test.Validators;
using Xunit;
using FluentValidation.TestHelper;
using API.DTOs.Request;

public class GetRouteRequestDtoValidatorTests
{
    private readonly GetRouteRequestDtoValidator _validator;

    public GetRouteRequestDtoValidatorTests()
    {
        _validator = new GetRouteRequestDtoValidator();
    }

    [Fact]
    public void Should_Have_Error_When_Origin_Is_Null()
    {
        var model = new GetRouteRequestDto { Origin = null };
        var result = _validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(x => x.Origin);
    }

    [Fact]
    public void Should_Have_Error_When_Origin_Location_Is_Null()
    {
        var model = new GetRouteRequestDto { Origin = new LocationWrapperDto { Location = null } };
        var result = _validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(x => x.Origin.Location);
    }

    [Fact]
    public void Should_Have_Error_When_Destination_Is_Null()
    {
        var model = new GetRouteRequestDto { Destination = null };
        var result = _validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(x => x.Destination);
    }

    [Fact]
    public void Should_Have_Error_When_Destination_Location_Is_Null()
    {
        var model = new GetRouteRequestDto { Destination = new LocationWrapperDto { Location = null } };
        var result = _validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(x => x.Destination.Location);
    }

    [Fact]
    public void Should_Have_Error_When_TravelMode_Is_Invalid()
    {
        var model = new GetRouteRequestDto { TravelMode = "INVALID_MODE" };
        var result = _validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(x => x.TravelMode);
    }

    [Fact]
    public void Should_Not_Have_Error_When_TravelMode_Is_Valid()
    {
        var model = new GetRouteRequestDto { TravelMode = "DRIVE" };
        var result = _validator.TestValidate(model);
        result.ShouldNotHaveValidationErrorFor(x => x.TravelMode);
    }

    [Fact]
    public void Should_Have_Error_When_Origin_LatLng_Is_Null()
    {
        var model = new GetRouteRequestDto { Origin = new LocationWrapperDto { Location = new LocationDto { LatLng = null } } };
        var result = _validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(x => x.Origin.Location.LatLng);
    }

    [Fact]
    public void Should_Have_Error_When_Origin_Latitude_Is_Out_Of_Range()
    {
        var model = new GetRouteRequestDto { Origin = new LocationWrapperDto { Location = new LocationDto { LatLng = new LatLngDto { Latitude = 100 } } } };
        var result = _validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(x => x.Origin.Location.LatLng.Latitude);
    }

    [Fact]
    public void Should_Have_Error_When_Origin_Longitude_Is_Out_Of_Range()
    {
        var model = new GetRouteRequestDto { Origin = new LocationWrapperDto { Location = new LocationDto { LatLng = new LatLngDto { Longitude = 200 } } } };
        var result = _validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(x => x.Origin.Location.LatLng.Longitude);
    }

    [Fact]
    public void Should_Have_Error_When_Destination_LatLng_Is_Null()
    {
        var model = new GetRouteRequestDto { Destination = new LocationWrapperDto { Location = new LocationDto { LatLng = null } } };
        var result = _validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(x => x.Destination.Location.LatLng);
    }

    [Fact]
    public void Should_Have_Error_When_Destination_Latitude_Is_Out_Of_Range()
    {
        var model = new GetRouteRequestDto { Destination = new LocationWrapperDto { Location = new LocationDto { LatLng = new LatLngDto { Latitude = 100 } } } };
        var result = _validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(x => x.Destination.Location.LatLng.Latitude);
    }

    [Fact]
    public void Should_Have_Error_When_Destination_Longitude_Is_Out_Of_Range()
    {
        var model = new GetRouteRequestDto { Destination = new LocationWrapperDto { Location = new LocationDto { LatLng = new LatLngDto { Longitude = 200 } } } };
        var result = _validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(x => x.Destination.Location.LatLng.Longitude);
    }

    [Fact]
    public void Should_Not_Have_Error_When_All_Properties_Are_Valid()
    {
        var model = new GetRouteRequestDto
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
            TravelMode = "DRIVE"
        };
        var result = _validator.TestValidate(model);
        result.ShouldNotHaveAnyValidationErrors();
    }

}

