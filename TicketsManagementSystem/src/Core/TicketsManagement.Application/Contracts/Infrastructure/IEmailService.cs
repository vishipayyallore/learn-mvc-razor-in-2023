using TicketsManagement.Application.Models.Mail;

namespace TicketsManagement.Application.Contracts.Infrastructure
{
    public interface IEmailService
    {
        Task<bool> SendEmail(Email email);
    }
}
