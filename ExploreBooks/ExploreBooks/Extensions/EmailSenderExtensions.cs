using System.Threading.Tasks;
using Business.Interfaces;

namespace ExploreBooks.Extensions
{
    public static class EmailSenderExtensions
    {
        public static Task SendEmailConfirmationAsync(this IEmailSender emailSender, string email)
        {
            return emailSender.SendEmailAsync(email, "Hello there!",
                $"\tHello and welcome!\n" +
                $"You received this mail as a confirmation for your registration to ExploreBooks!\n" +
                $"Enjoy and share a great experience with it!");
        }
    }
}
