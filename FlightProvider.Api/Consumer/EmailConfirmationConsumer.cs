
using FlightProvider.Entity.Dto;
using FluentEmail.Core;
using FluentEmail.Core.Models;
using MassTransit;

namespace FlightProvider.Api.Consumer
{
    public class EmailConfirmationConsumer : IConsumer<EmailConfirmationDto>
    {
        private readonly IFluentEmail _fluentEmail;
        public EmailConfirmationConsumer(IFluentEmail fluentEmail)
        {
            _fluentEmail = fluentEmail;
        }
        public async Task Consume(ConsumeContext<EmailConfirmationDto> context)
        {
                await _fluentEmail.To(context.Message.Email)
              .Subject("Mail Onaylama H.K.")
              .Body($"Mail Onaylama linki :{context.Message.ConfirmationLink}")
              .SendAsync();
                await Task.CompletedTask;
        }
    }
}
