using Repositories.IRepository; 
using System.Net.Mail;
using System.Net;
using Domain.Models;

namespace Repositories.Repository
{
    public class DoctorEmailSenderRepository : IEmailSenderRepository
    {
        public Task SendEmailAsync(SignUpModel signUpModel)
        {
            var client = new SmtpClient("smtp.gmail.com", 587)
            {
                EnableSsl = true,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential("seeifeldina@gmail.com", "jhqw npdy phkp qyvw")
            };

            return client.SendMailAsync(
                new MailMessage(from: "seeifeldina@gmail.com",
                                to: signUpModel.Email,
                                "Vezeeta Email Confirmed",
                                "Thanks for being a new member with Vezeeta" +
                                $"\nThis is your email: {signUpModel.Email}\nPassword:{
                                    signUpModel.Password}\n" + 
                                $"This is confidential. Please don't show it to anyone."
                                ));
        }
    }
}
