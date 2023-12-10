using Repositories.IRepository; 
using System.Net.Mail;
using System.Net;
using Domain.Models;

namespace Repositories.Repository
{
    public class PatientEmailSenderRepository : IEmailSenderRepository
    {
        public Task SendEmailAsync(SignUpModel model)
        {
            var client = new SmtpClient("smtp.gmail.com", 587)
            {
                EnableSsl = true,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential("seeifeldina@gmail.com", "jhqw npdy phkp qyvw")
            };

            return client.SendMailAsync(
                new MailMessage(from: "seeifeldina@gmail.com",
                                to: model.Email,
                                "Vezeeta Email Confirmed",
                                "Thanks for being a new member with Vezeeta"
                                ));
        }
    }
}
