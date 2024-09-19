
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


            var decodedLink = Uri.UnescapeDataString(context.Message.ConfirmationLink);

            var htmlBody = $@"
        <html>
        <body>
            <p>Mail Onaylama linki:</p>
            <a href='{decodedLink}'>Onaylama Linkine Git</a>
        </body>
        </html>";

            await _fluentEmail.To(context.Message.Email)
                .Subject("Mail Onaylama H.K.")
                .Body(htmlBody, isHtml: true)
                .SendAsync();
        }
    }
}
