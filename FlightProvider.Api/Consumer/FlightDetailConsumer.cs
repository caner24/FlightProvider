using FlightProvider.Entity.Dto;
using FluentEmail.Core;
using MassTransit;

namespace FlightProvider.Api.Consumer
{
    public class FlightDetailConsumer : IConsumer<FlightTicketDto>
    {
        private readonly IFluentEmail _fluentEmail;
        public FlightDetailConsumer(IFluentEmail fluentEmail)
        {
            _fluentEmail = fluentEmail;
        }
        public async Task Consume(ConsumeContext<FlightTicketDto> context)
        {
            await _fluentEmail.To(context.Message.Email)
          .Subject("Biletiniz H.K.")
          .Body($"Satın alımınız için teşekkürler sitemiz üzerinden bilet detaylarınızı sorgulayabilirsiniz. !." +
          $"Bilet Numarasi :{context.Message.FlightNumber}") 
          .SendAsync();
            await Task.CompletedTask;
        }
    }
}
