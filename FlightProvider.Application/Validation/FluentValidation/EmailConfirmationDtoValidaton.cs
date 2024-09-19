using FlightProvider.Entity.Dto;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightProvider.Application.Validation.FluentValidation
{
    public class EmailConfirmationDtoValidaton:AbstractValidator<EmailConfirmationDto>
    {
        public EmailConfirmationDtoValidaton()
        {
            RuleFor(x => x.Email).NotEmpty().NotNull().WithMessage($"{nameof(EmailConfirmationDto.Email)} alani boş birakilamaz !.");
            RuleFor(x => x.ConfirmationLink).NotEmpty().NotNull().WithMessage($"{nameof(EmailConfirmationDto.ConfirmationLink)} alani boş birakilamaz !.");
            RuleFor(x => x.User).NotEmpty().NotNull().WithMessage($"{nameof(EmailConfirmationDto.User)} alani boş birakilamaz !.");
        }


    }
}
