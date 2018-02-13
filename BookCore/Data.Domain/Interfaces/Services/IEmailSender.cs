using System.Threading.Tasks;

namespace Data.Domain.Interfaces.Services
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}
