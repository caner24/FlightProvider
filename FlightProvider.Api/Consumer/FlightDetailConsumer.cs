using FlightProvider.Entity.Dto;
using FluentEmail.Core;
using MassTransit;

namespace FlightProvider.Api.Consumer
{
    public class FlightDetailConsumer : IConsumer<FlightDetailConsumerDto>
    {
        private readonly IFluentEmail _fluentEmail;
        public FlightDetailConsumer(IFluentEmail fluentEmail)
        {
            _fluentEmail = fluentEmail;
        }
        public async Task Consume(ConsumeContext<FlightDetailConsumerDto> context)
        {
            await _fluentEmail.To(context.Message.Email)
          .Subject("Biletiniz H.K.")
          .Body($"Satın alımınız için teşekkürler !." +
          $"Gidiş Tarihi :{context.Message.DepartureTime}" +
          $"Gidiş Yeri : {context.Message.DepartureCity}" +
          $"Varış Tarihi : {context.Message.ArrivalTime}" +
          $"Varış Yeri : {context.Message.ArrivalCity}" +
          $"Total Ücret : {context.Message.TotalPrice}")
          .SendAsync();
            await Task.CompletedTask;
        }
    }
}
