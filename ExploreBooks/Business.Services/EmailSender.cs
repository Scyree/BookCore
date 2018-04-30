using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Business.Interfaces;

namespace Business.Services
{
    // This class is used by the application to send email for account confirmation and password reset.
    // For more details see https://go.microsoft.com/fwlink/?LinkID=532713
    public class EmailSender : IEmailSender
    {
        private const string Username = "exploreBooks.noreply@gmail.com";
        private const string Password = "exploreBooksForTheWin";

        // For another server provider search for the proper smtp
        private const string Server = "smtp.gmail.com";

        public Task SendEmailAsync(string email, string subject, string message)
        {
            var mail = new MailMessage(Username, email, subject, message);

            var client = new SmtpClient(Server)
            {
                Port = 587, // 465, 587 these are not random ports, search for them too acording to the server
                Credentials = new NetworkCredential(Username, Password),
                EnableSsl = true,
            };

            return client.SendMailAsync(mail);
        }
    }
}
