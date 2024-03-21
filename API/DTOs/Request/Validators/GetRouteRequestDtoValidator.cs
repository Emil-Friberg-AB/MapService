using API.DTOs.Request;
using FluentValidation;
using MediatR;

public class GetRouteRequestDtoValidator : AbstractValidator<GetRouteRequestDto>
{
    public GetRouteRequestDtoValidator()
    {
        RuleFor(x => x.Origin).NotNull();
        When(x => x.Origin != null, () =>
        {
            RuleFor(x => x.Origin.Location).NotNull();
            When(x => x.Origin.Location != null, () =>
            {
                RuleFor(x => x.Origin.Location.LatLng).NotNull();
                When(x => x.Origin.Location.LatLng != null, () =>
                {
                    RuleFor(x => x.Origin.Location.LatLng.Latitude).InclusiveBetween(-90, 90);
                    RuleFor(x => x.Origin.Location.LatLng.Longitude).InclusiveBetween(-180, 180);
                });
            });
        });

        RuleFor(x => x.Destination).NotNull();
        When(x => x.Destination != null, () =>
        {
            RuleFor(x => x.Destination.Location).NotNull();
            When(x => x.Destination.Location != null, () =>
            {
                RuleFor(x => x.Destination.Location.LatLng).NotNull();
                When(x => x.Destination.Location.LatLng != null, () =>
                {
                    RuleFor(x => x.Destination.Location.LatLng.Latitude).InclusiveBetween(-90, 90);
                    RuleFor(x => x.Destination.Location.LatLng.Longitude).InclusiveBetween(-180, 180);
                });
            });
        });

        RuleFor(x => x.TravelMode).Must(x => x == "DRIVE" || x == "WALKING" || x == "BICYCLING" || x == "TRANSIT");
    }

}
