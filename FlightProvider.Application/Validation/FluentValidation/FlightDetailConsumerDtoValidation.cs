using FlightProvider.Entity.Dto;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightProvider.Application.Validation.FluentValidation
{
    public class FlightDetailConsumerDtoValidation : AbstractValidator<FlightDetailConsumerDto>
    {
        public FlightDetailConsumerDtoValidation()
        {
            RuleFor(x => x.DepartureDate).NotEmpty().NotNull().WithMessage($"{nameof(FlightDetailConsumerDto.DepartureDate)} alani boş birakilamaz !.");
            RuleFor(x => x.DepartureTime).NotEmpty().NotNull().WithMessage($"{nameof(FlightDetailConsumerDto.DepartureTime)} alani boş birakilamaz !.");
            RuleFor(x => x.ArrivalCity).NotEmpty().NotNull().WithMessage($"{nameof(FlightDetailConsumerDto.ArrivalCity)} alani boş birakilamaz !.");
            RuleFor(x => x.ArrivalDate).NotEmpty().NotNull().WithMessage($"{nameof(FlightDetailConsumerDto.ArrivalDate)} alani boş birakilamaz !.");
            RuleFor(x => x.ArrivalTime).NotEmpty().NotNull().WithMessage($"{nameof(FlightDetailConsumerDto.ArrivalTime)} alani boş birakilamaz !.");

        }

    }
}
