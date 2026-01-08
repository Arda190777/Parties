using System.Net;
using System.Net.Mail;

namespace Partys.Services
{
    public interface IEmailService
    {
        Task SendInvitationEmail(string toEmail, string guestName, string partyDescription, DateTime eventDate, int invitationId);
    }

    public class EmailService : IEmailService
    {
        private readonly ILogger<EmailService> _logger;

        public EmailService( ILogger<EmailService> logger)
        {
            _logger = logger;
        }

        public async Task SendInvitationEmail(string toEmail, string guestName, string partyDescription, DateTime eventDate, int invitationId)
        {
            _logger.LogInformation($"EMAIL SENT to {toEmail}: You're invited to {partyDescription} on {eventDate:MMMM dd, yyyy}");
            _logger.LogInformation($"Accept link: http://localhost:5083/Invitations/Respond?id={invitationId}&response=accept");
            _logger.LogInformation($"Decline link: http://localhost:5083/Invitations/Respond?id={invitationId}&response=decline");

            await Task.Delay(100);
        }
    }
}
