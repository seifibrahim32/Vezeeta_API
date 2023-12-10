using Domain.Models;


namespace Repositories.IRepository
{
    public interface IEmailSenderRepository
    {
        public Task SendEmailAsync(SignUpModel signUpModel); 
    }
}
