using FlightProvider.Entity;
using FlightProvider.Entity.Dto;
using MassTransit;
using Microsoft.AspNetCore.Identity;

namespace FlightProvider.Api.Mail
{
    public class MailSender : IEmailSender<User>
    {
        private readonly IServiceProvider _serviceProvider;

        public MailSender(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        public async Task SendConfirmationLinkAsync(User user, string email, string confirmationLink)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var publishEndpoint = scope.ServiceProvider.GetRequiredService<IPublishEndpoint>();
                var encodedLink = Uri.EscapeDataString(confirmationLink);
                await publishEndpoint.Publish(new EmailConfirmationDto { Email = email, User = user, ConfirmationLink = encodedLink });
            }
        }

        public Task SendPasswordResetCodeAsync(User user, string email, string resetCode)
        {
            throw new NotImplementedException();
        }

        public Task SendPasswordResetLinkAsync(User user, string email, string resetLink)
        {
            throw new NotImplementedException();
        }
    }
}
