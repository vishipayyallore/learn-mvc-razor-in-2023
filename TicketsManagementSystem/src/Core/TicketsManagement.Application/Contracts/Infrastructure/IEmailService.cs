using TicketsManagement.Application.Models.Mail;
using System.Threading.Tasks;

namespace TicketsManagement.Application.Contracts.Infrastructure
{
    public interface IEmailService
    {
        Task<bool> SendEmail(Email email);
    }
}
